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
                string COMSerie = GT.Socket.GetSocket(socket, true, null, null).SerialPortName; //permet d'associer le nom de communication s�rie au socket (ici 'COMSerie' au socket11)
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

                PortCOM.ReadTimeout = 500;     // temps de r�ception max limit� � 500ms
                PortCOM.WriteTimeout = 500;    // temps d'�mission max limit� � 500ms

                PortCOM.Open();
                if (PortCOM.IsOpen)
                {
                    PortCOM.Flush();
                    Informations.printInformations(Priority.HIGH,"SERVOS : Port COM ouvert : "+COMSerie);
                }
                else
                {
                    Informations.printInformations(Priority.HIGH, "ERREUR : SERVOS : Port COM ferm� !!!!!! Port : " + COMSerie);
                }

                //direction_TX = new OutputPort((Cpu.Pin)GT.Socket.GetSocket(socket, true, null, null).CpuPins[3], false);   // ligne de direction de data au NLB (Spider en r�ception de l'interface AX12) 
                //direction_TX = new OutputPort((Cpu.Pin)EMX.IO26, false);   // ligne de direction de data au NLB (Spider en r�ception de l'interface AX12) 

                serialPort = PortCOM;
                //direction_TX = new OutputPort((Cpu.Pin)GT.Socket.GetSocket(socket, true, null, null)., false);
                //OutputPort direction_TX = new OutputPort(GHI.Pins.G120E.Gpio.P2_30, false);       //�quivalente � la pr�c�dente
                //   OutputPort direction_TX = new OutputPort(GHI.Pins.FEZSpiderII.Socket11.Pin3,false);   �quivalente � la pr�c�dente

                /*// configure le port de communication s�rie, ligne half-duplex, pin IO3/TXD0 du socket 11 
                string COMSerie = GT.Socket.GetSocket(11, true, null, null).SerialPortName; //permet d'associer le nom de communication s�rie au socket (ici 'COMSerie' au socket11)
                // string COMSerie = GHI.Pins.G120E.SerialPort.Com1;
                Debug.Print(COMSerie);
                SerialPort PortCOM = new SerialPort(COMSerie, 937500, Parity.None, 8, StopBits.One);
                PortCOM.ReadTimeout = 500;     // temps de r�ception max limit� � 500ms
                PortCOM.WriteTimeout = 500;    // temps d'�mission max limit� � 500ms

                // ouverture du port de communication s�rie et vider son buffer
                PortCOM.Open();
                if (PortCOM.IsOpen) PortCOM.Flush();

                serialPort = PortCOM;*/

            }
            this.cax12 = new CAX_12((byte)id, serialPort, direction_TX);
            //GHI.Pins.FEZSpiderII.Socket11
        }

        /// <summary>
        /// Sert simplement � ex�cuter la m�thode associ� au type de rotation demand� dans l'�num
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
        /// Rotation RELATIVE par rapport � la position actuelle du servo.
        /// (Passe en mode wheel si n�cessaire)
        /// </summary>
        /// <param name="angle">En degr�s</param>
        /// <returns>Dur�e en ms estim�e de la rotation</returns>
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
        /// Rotation ABSOLUE : d�fini l'angle de mani�re directe.
        /// </summary>
        /// <param name="angle">En degr�s</param>
        /// <returns>Dur�e en ms estim�e de la rotation</returns>
        public int SetAngle(int steps)
        {
            return SetAngle(steps, 2000);
        }
        public int SetAngle(int steps, int sleepDuration)
        {
            // http://folk.uio.no/matsh/inf4500/files/datasheets/Dynamixel%20-%20AX-12_files/dx_series_goal.png
            // http://folk.uio.no/matsh/inf4500/files/datasheets/Dynamixel%20-%20AX-12.htm

            Informations.printInformations(Priority.LOW, "l'angle absolu de l'AX-12 est d�sormais de " + steps + "steps");

            this.cax12.setMode(AX12Mode.joint);
            //this.cax12.setMovingSpeed(speed.forward);

            this.cax12.move(steps);
            return sleepDuration; // TODO : change depending of the previous angle
        }

        public int GetDurationOfRotation(float angle)
        {
            // R�cup�re la vitesse (et le mode de fonctionnement ?) / d�pend de la vitesse et du mode (� rajouter en param dans ce cas)
            // Pour renvoyer le nombre de ms � attendre pour effectuer la rotation
            Informations.printInformations(Priority.LOW, "la dur�e de la rotation a �t� de _ secondes");
            return 0;
        }

        public void Stop()
        {
            // Stop simplement la rotation actuelle, quelqu'elle soit (wheel/Joint)
        }



    }
}
