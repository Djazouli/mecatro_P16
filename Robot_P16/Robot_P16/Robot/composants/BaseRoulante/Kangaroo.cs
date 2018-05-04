using System;
using Microsoft.SPOT;
using System.IO.Ports;
using System.Threading;

using GT = Gadgeteer;
using Robot_P16.Map;
using Robot_P16.Robot.composants;

namespace Robot_P16.Robot.composants.BaseRoulante
{
    public class Kangaroo : Composant
    {
        private SerialPort m_portCOM;
        /*private ushort m_tailleRoues;
        private float m_nombreLigneDrive;       // Nombre de ligne pour 1 mm
        private float m_nombreLigneRotation;    // Nombre de ligne pour 1 mm*/
        public AutoResetEvent MoveCompleted = new AutoResetEvent(false);
        PointOriente position = new PointOriente(0, 0, 0);

        string currentMode = "D";

        public Kangaroo(int socket) : base(socket)
        {
            string COMPort = GT.Socket.GetSocket(socket, true, null, null).SerialPortName;

            Debug.Print("Tring to open serial port on COMPORt :" + COMPort);
            m_portCOM = new SerialPort(COMPort, 9600, Parity.None, socket, StopBits.One);
            Debug.Print("Opening OK !");

            m_portCOM.ReadTimeout = 500;
            m_portCOM.WriteTimeout = 500;
            m_portCOM.Open();
            
            Init();

            /*m_tailleRoues = 0;
            m_nombreLigneDrive = 0;
            m_nombreLigneRotation = 0;*/
        }

        private bool EnvoyerCommande(string commande)
        {
            if (!m_portCOM.IsOpen) return false;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(commande);
            m_portCOM.Write(buffer, 0, buffer.Length);
            Debug.Print(commande);
            return true;
        }

        private bool Init()
        {
            if (!m_portCOM.IsOpen) return false;

            string commande = "T,start\r\n";
            EnvoyerCommande(commande);

            commande = "D,start\r\n";
            EnvoyerCommande(commande);


            commande = "T, UNITS 36000 millidegrees = 388400 lines\r\n";
            EnvoyerCommande(commande);


            commande = "D, UNITS 182 mm = 1024 lines\r\n";
            EnvoyerCommande(commande);


            commande = "T,p0s0\r\n";
            EnvoyerCommande(commande);


            commande = "D,p0s0\r\n";
            EnvoyerCommande(commande);

            return true;
        }

        public void CheckMovingStatus()
        {
            if (this.currentMode == null) return;
            string feedback = sendAndReceiveUpdate(this.currentMode);
            Informations.printInformations(Priority.VERY_LOW, "Kangaroo - CheckMovingStatus - feedback : " + feedback);
            char upperCased = feedback[2].ToUpper();
            if (upperCased == feedback[2])
            {
                Informations.printInformations(Priority.MEDIUM, "Kangaroo - CheckMovingStatus detected end of move, updating position");
                // isMoving = false, done moving
                this.position = PositionFromFeedback(feedback);
                this.currentMode = null;
                this.MoveCompleted.Set();
            }
        }

        public PointOriente GetDynamicPosition()
        {
            if (this.currentMode == null) return position;
            return PositionFromFeedback(sendAndReceiveUpdate(this.currentMode));
        }

        private PointOriente PositionFromFeedback(string feedback)
        {

            Informations.printInformations(Priority.VERY_LOW, "Kangaroo - PositionFromFeedback - mode : " + this.currentMode + "; feedback : " + feedback);
            //T,P100
            char status = feedback[2];
            Debug.Print("Status : " + status);
            Debug.Print("Deplacement read : " + feedback.Substring(3));
            double deplacement = Int32.Parse(feedback.Substring(3));
            if (this.currentMode == "D")
            {
                double angle = System.Math.PI * position.theta / 180.0;
                return new PointOriente(
                    position.x + deplacement * System.Math.Cos(angle),
                    position.y + deplacement * System.Math.Sin(angle),
                    position.theta);
            }
            else
            {
                return new PointOriente(
                    position.x,
                    position.y,
                    position.theta + deplacement);
            }
        }


        private string sendAndReceiveUpdate(string prefix)
        {
            string commande = prefix + ",getpi\r\n";
            EnvoyerCommande(commande);

            Thread.Sleep(10);
            byte[] bytesRead = new byte[m_portCOM.BytesToRead];
            m_portCOM.Read(bytesRead, 0, m_portCOM.BytesToRead);
            char[] feedback_chars = System.Text.Encoding.UTF8.GetChars(bytesRead);
            return new string(feedback_chars);

        }

        public bool drive(int distance, int vitesse)
        {
            string commande;

            Init();
            currentMode = "D";
            commande = "D,pi" + distance + "s" + vitesse + "\r\n";
            if (!EnvoyerCommande(commande)) return false;
            Informations.printInformations(Priority.MEDIUM, "Kangaroo - Drive : sent command, waiting for move completion");
            MoveCompleted.WaitOne();
            return true;
        }

        public bool rotate(int angle, int vitesse)
        {
            string commande;

            Init();
            currentMode = "T";
            commande = "T,pi" + angle + "s" + vitesse + "\r\n";
            if (!EnvoyerCommande(commande)) return false;
            Informations.printInformations(Priority.MEDIUM, "Kangaroo - Turn : sent command, waiting for move completion");
            MoveCompleted.WaitOne();
            return true;
        }

        public void stop()
        {
            this.EnvoyerCommande("T,powerdown\r\n");
            this.EnvoyerCommande("D,powerdown\r\n");
        }
    }
}

