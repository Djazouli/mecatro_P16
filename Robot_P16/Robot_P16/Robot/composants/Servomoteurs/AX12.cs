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
                int baudRate;

                if ((Robot.robot.TypeRobot == TypeRobot.GRAND_ROBOT && Robot.robot.isGRSpiderI)
                    || (Robot.robot.TypeRobot == TypeRobot.PETIT_ROBOT && Robot.robot.isPRSpiderI))
                {
                    baudRate = 1000000;
                    direction_TX = new OutputPort((Cpu.Pin)GHI.Pins.FEZSpider.Socket11.Pin3, false);
                }
                else
                {
                    baudRate = 937500;
                    direction_TX = new OutputPort((Cpu.Pin)GHI.Pins.FEZSpiderII.Socket11.Pin3, false);

                }
                SerialPort PortCOM = new SerialPort(COMSerie, baudRate, Parity.None, 8, StopBits.One);

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
                //direction_TX = new OutputPort((Cpu.Pin)EMX.IO26, false);   // ligne de direction de data au NLB (Spider en réception de l'interface AX12) 

                serialPort = PortCOM;
                //direction_TX = new OutputPort((Cpu.Pin)GT.Socket.GetSocket(socket, true, null, null)., false);
                //OutputPort direction_TX = new OutputPort(GHI.Pins.G120E.Gpio.P2_30, false);       //équivalente à la précédente
                //   OutputPort direction_TX = new OutputPort(GHI.Pins.FEZSpiderII.Socket11.Pin3,false);   équivalente à la précédente

                /*// configure le port de communication série, ligne half-duplex, pin IO3/TXD0 du socket 11 
                string COMSerie = GT.Socket.GetSocket(11, true, null, null).SerialPortName; //permet d'associer le nom de communication série au socket (ici 'COMSerie' au socket11)
                // string COMSerie = GHI.Pins.G120E.SerialPort.Com1;
                Debug.Print(COMSerie);
                SerialPort PortCOM = new SerialPort(COMSerie, 937500, Parity.None, 8, StopBits.One);
                PortCOM.ReadTimeout = 500;     // temps de réception max limité à 500ms
                PortCOM.WriteTimeout = 500;    // temps d'émission max limité à 500ms

                // ouverture du port de communication série et vider son buffer
                PortCOM.Open();
                if (PortCOM.IsOpen) PortCOM.Flush();

                serialPort = PortCOM;*/

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
        public int ExecuteCommand(ServoCommandTypes type, float angle) // OBSOLETE !!!
        {/*
            switch (type)
            {
                case ServoCommandTypes.ABSOLUTE_ROTATION:
                    return SetAngle(angle);
                case ServoCommandTypes.RELATIVE_ROTATION:
                    return RotateOf(angle);
                default:
                    return -1;
            }*/
            return -11;
        }

        /// <summary>
        /// Rotation RELATIVE par rapport à la position actuelle du servo.
        /// (Passe en mode wheel si nécessaire)
        /// </summary>
        /// <param name="angle">En degrès</param>
        /// <returns>Durée en ms estimée de la rotation</returns>
        public void Rotate(speed vitesse, int duration)
        {
            Rotate((int)vitesse, duration);
        }
        public void Rotate(int vitesse, int duration)
        {
            this.cax12.setMode(AX12Mode.wheel);
            this.cax12.setMovingSpeed(vitesse);
            Thread.Sleep(duration);
            this.cax12.setMovingSpeed(speed.stop);
        }

        /// <summary>
        /// Rotation ABSOLUE : défini l'angle de manière directe.
        /// </summary>
        /// <param name="angle">En degrès</param>
        /// <returns>Durée en ms estimée de la rotation</returns>
        public int SetAngle(int steps)
        {
            return SetAngle(steps, 2000);
        }
        public int SetAngle(int steps, int sleepDuration)
        {
            // http://folk.uio.no/matsh/inf4500/files/datasheets/Dynamixel%20-%20AX-12_files/dx_series_goal.png
            // http://folk.uio.no/matsh/inf4500/files/datasheets/Dynamixel%20-%20AX-12.htm

            Informations.printInformations(Priority.LOW, "l'angle absolu de l'AX-12 est désormais de " + steps + "steps");

            this.cax12.setMode(AX12Mode.joint);
            //this.cax12.setMovingSpeed(speed.forward);

            this.cax12.move(steps);
            return sleepDuration; // TODO : change depending of the previous angle
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
