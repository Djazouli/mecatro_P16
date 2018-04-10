using System;
using Microsoft.SPOT;
using System.Collections;
using System.IO.Ports;
using GT = Gadgeteer;
using Microsoft.SPOT.Hardware;
using GHI.Pins;

using System.Threading;

namespace Robot_P16.Robot.composants.Servomoteurs
{

    public enum ServoMoteurTypes
    {
        PR_CLAPET1,
        GR_CLAPET1
    }

    public enum ServoCommandTypes
    {
        ABSOLUTE_ROTATION,
        RELATIVE_ROTATION
    }

    public class AX12
    {

        private static SerialPort serialPort = null;
        private static OutputPort direction_TX = null;

        private CAX_12 cax12;

        public AX12(int socket, int id)
        {
            if (serialPort == null)
            {
                string COMSerie = GT.Socket.GetSocket(socket, true, null, null).SerialPortName; //permet d'associer le nom de communication série au socket (ici 'COMSerie' au socket11)
                SerialPort PortCOM = new SerialPort(COMSerie, 937500, Parity.None, 8, StopBits.One);
                PortCOM.ReadTimeout = 500;     // temps de réception max limité à 500ms
                PortCOM.WriteTimeout = 500;    // temps d'émission max limité à 500ms

                PortCOM.Open();
                if (PortCOM.IsOpen)
                {
                    PortCOM.Flush();
                    Informations.printInformations(Priority.HIGH,"SERVOS : Port COM ouvert : "+COMSerie);
                }
                else
                {
                    Informations.printInformations(Priority.HIGH, "ERREUR : SERVOS : Port COM fermé !!!!!! Port : " + COMSerie);

                }

                //direction_TX = new OutputPort((Cpu.Pin)GT.Socket.GetSocket(socket, true, null, null).CpuPins[3], false);   // ligne de direction de data au NLB (Spider en réception de l'interface AX12) 
                direction_TX = new OutputPort((Cpu.Pin)EMX.IO26, false);   // ligne de direction de data au NLB (Spider en réception de l'interface AX12) 
                
                serialPort = PortCOM;
            }
            this.cax12 = new CAX_12((byte)id, serialPort, direction_TX);
            //GHI.Pins.FEZSpiderII.Socket11
        }

        /// <summary>
        /// Sert simplement à exécuter la méthode associé au type de rotation demandé dans l'énum
        /// </summary>
        /// <param name="type"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public int ExecuteCommand(ServoCommandTypes type, float angle)
        {
            switch (type)
            {
                case ServoCommandTypes.ABSOLUTE_ROTATION:
                    return SetAngle(angle);
                case ServoCommandTypes.RELATIVE_ROTATION:
                    return RotateOf(angle);
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Rotation RELATIVE par rapport à la position actuelle du servo.
        /// (Passe en mode wheel si nécessaire)
        /// </summary>
        /// <param name="angle">En degrès</param>
        /// <returns>Durée en ms estimée de la rotation</returns>
        public int RotateOf(float angle)
        {
            // TODO
            string a = angle.ToString();
            Informations.printInformations(Priority.HIGH, "rotation du servo de " + a + "degrés");
            return 1;
        }

        /// <summary>
        /// Rotation ABSOLUE : défini l'angle de manière directe.
        /// </summary>
        /// <param name="angle">En degrès</param>
        /// <returns>Durée en ms estimée de la rotation</returns>
        public int SetAngle(float angle)
        {
            // http://folk.uio.no/matsh/inf4500/files/datasheets/Dynamixel%20-%20AX-12_files/dx_series_goal.png
            // http://folk.uio.no/matsh/inf4500/files/datasheets/Dynamixel%20-%20AX-12.htm

            Informations.printInformations(Priority.LOW, "l'angle absolu de l'AX-12 est désormais de " + angle + "degrés");

            int steps = 512 + (int)(angle / 300.0 * 1024.0);

            this.cax12.setMode(AX12Mode.joint);
            Thread.Sleep(100);
            this.cax12.move(steps);
            Debug.Print("Steps : "+steps);
            return 2000;
        }

        public int GetDurationOfRotation(float angle)
        {
            // Récupère la vitesse (et le mode de fonctionnement ?) / dépend de la vitesse et du mode (à rajouter en param dans ce cas)
            // Pour renvoyer le nombre de ms à attendre pour effectuer la rotation
            Informations.printInformations(Priority.LOW, "la durée de la rotation a été de _ secondes");
            return 0;
        }

        public void Stop()
        {
            // Stop simplement la rotation actuelle, quelqu'elle soit (wheel/Joint)
        }



    }
}
