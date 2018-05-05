using System;
using Microsoft.SPOT;
using System.Threading;

using System.IO.Ports;
using Microsoft.SPOT.Hardware;
using Gadgeteer;
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
        public bool blockRead = false;
        public AutoResetEvent UnblockRead = new AutoResetEvent(false);
        PointOriente position = new PointOriente(0, 0, 0);

        private int distanceIncrementale = 0;
        private int angleIncremental = 0;

        private const int RAPPORT_ANGLE_DEGRE_VERS_CODEUR = 10;
        private const int RAPPORT_DISTANCE_CODEUR_VERS_MM = 2;

        private const int CODEUR_LINES_ANGLE_360_DEGRES = 424100;
        private const int CODEUR_VAL_ANGLE_360_DEGRES = 36000;

        private const int CODEUR_LINES_DISTANCE_1MM = 102400;
        private const int CODEUR_VAL_DISTANCE_1MM = 15550;

        string currentMode = null;

        public Kangaroo(int socket) : base(socket)
        {
            string COMPort = Socket.GetSocket(socket, true, null, null).SerialPortName;

            Informations.printInformations(Priority.HIGH, "Tring to open serial port on COMPORt :" + COMPort +", socket : "+socket);
            m_portCOM = new SerialPort(COMPort, 9600, Parity.None, 8, StopBits.One);

            m_portCOM.ReadTimeout = 500;
            m_portCOM.WriteTimeout = 500;

            if (!m_portCOM.IsOpen)
            {
                Informations.printInformations(Priority.HIGH, "New Kangaroo, port com not opened, opening.");
                m_portCOM.Open();
                Informations.printInformations(Priority.HIGH, "New Kangaroo, port com Opening OK !");
            }
            else
            {
                Informations.printInformations(Priority.HIGH, "New Kangaroo, port com ALREADY OPENED !!!!!");
            }
            
            Init();
            Thread.Sleep(100);

            /*m_tailleRoues = 0;
            m_nombreLigneDrive = 0;
            m_nombreLigneRotation = 0;*/
        }

        private bool EnvoyerCommande(string commande)
        {
            if (!m_portCOM.IsOpen) return false;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(commande);
            m_portCOM.Write(buffer, 0, buffer.Length);
            //Debug.Print(commande);
            return true;
        }

        private bool Init()
        {
            if (!m_portCOM.IsOpen) return false;

            string commande = "T,start\r\n";
            EnvoyerCommande(commande);

            commande = "D,start\r\n";
            EnvoyerCommande(commande);


            commande = "T, UNITS "+ CODEUR_VAL_ANGLE_360_DEGRES * RAPPORT_ANGLE_DEGRE_VERS_CODEUR+"  millidegrees = "+CODEUR_LINES_ANGLE_360_DEGRES+" lines\r\n";
            EnvoyerCommande(commande);
            Debug.Print(commande);

            commande = "D, UNITS "+CODEUR_VAL_DISTANCE_1MM+" mm = "+CODEUR_LINES_DISTANCE_1MM * RAPPORT_DISTANCE_CODEUR_VERS_MM+"\r\n";
            EnvoyerCommande(commande);
            Debug.Print(commande);

            commande = "T,p0s0\r\n";
            EnvoyerCommande(commande);


            commande = "D,p0s0\r\n";
            EnvoyerCommande(commande);

            angleIncremental = 0;
            distanceIncrementale = 0;

            return true;
        }
        public void CheckMovingStatus()
        {
            if (this.currentMode == null)
            {
                GetDynamicPosition();
                Debug.Print("POSITION : "+this.position);
                return;
            }


            Informations.printInformations(Priority.VERY_LOW, "Received : "+sendAndReceiveUpdate("T"));

            Thread.Sleep(100);


            string feedback;
            if (this.currentMode == "T")
            {
                feedback = sendAndReceiveUpdate(this.currentMode);
                UpdatePositionFromFeedback(feedback);
                UpdatePositionFromFeedback(sendAndReceiveUpdate("D"));
            }
            else
            {
                feedback = sendAndReceiveUpdate(this.currentMode);
                UpdatePositionFromFeedback(sendAndReceiveUpdate("T"));
                UpdatePositionFromFeedback(feedback);
            }

            //string feedback = sendAndReceiveUpdate(this.currentMode);
            Informations.printInformations(Priority.LOW, "Kangaroo - CheckMovingStatus - feedback : " + feedback);
            if (feedback == null || feedback.Length < 4) return;

            //this.position = PositionFromFeedback(feedback);

            char upperCased = feedback[2].ToUpper();
            if (upperCased == feedback[2])
            {
                Informations.printInformations(Priority.VERY_LOW, "Kangaroo - CheckMovingStatus detected end of move");
                // isMoving = false, done moving
                this.currentMode = null;
                this.MoveCompleted.Set();
            }


            Informations.printInformations(Priority.VERY_LOW, "Current position : " + this.position);
        }

        public PointOriente GetDynamicPosition()
        {
            //if (this.currentMode == null) return position;
            UpdatePositionFromFeedback(sendAndReceiveUpdate("T"));
            UpdatePositionFromFeedback(sendAndReceiveUpdate("D"));
            Init();
            //return PositionFromFeedback(sendAndReceiveUpdate(this.currentMode));
            return this.position;
        }


        private void UpdatePositionFromFeedback(string feedback)
        {

            Informations.printInformations(Priority.VERY_LOW, "Kangaroo - UpdatePositionFromFeedback - mode : " + this.currentMode + "; feedback : " + feedback);
            //T,P100
            if (/*this.currentMode == null || */ feedback == null || feedback.Length < 4) return;// this.position;
            char status = feedback[2];
            //Debug.Print("Status : " + status);
            string sub_str = feedback.Substring(3, feedback.Length - 3);
            //Debug.Print("Deplacement read : " + sub_str);
            int deplacementFromFeedback = Int32.Parse(sub_str);
            if (feedback[0] == 'D')
            {
                double deplacement = deplacementFromFeedback - distanceIncrementale;
                distanceIncrementale = deplacementFromFeedback;
                deplacement *= RAPPORT_DISTANCE_CODEUR_VERS_MM;
                if (Robot.robot.TypeRobot == TypeRobot.PETIT_ROBOT)
                {
                    deplacement = -deplacement;
                }
                double angle = System.Math.PI * position.theta / 180.0;
                position = new PointOriente(
                    position.x + deplacement * System.Math.Cos(angle),
                    position.y + deplacement * System.Math.Sin(angle),
                    position.theta);
                Debug.Print("MAJ Position = " + position.x + "," + position.y + "," + position.theta);
            }
            else
            {
                double deplacement = deplacementFromFeedback - angleIncremental;
                angleIncremental = deplacementFromFeedback;
                deplacement /= RAPPORT_ANGLE_DEGRE_VERS_CODEUR;
                if (Robot.robot.TypeRobot == TypeRobot.PETIT_ROBOT)
                {
                    //deplacement = -deplacement;
                }
                position = new PointOriente(
                    position.x,
                    position.y,
                    position.theta + deplacement);
                Debug.Print("MAJ Position = " + position.x + "," + position.y + "," + position.theta);
            }
        }


        private string sendAndReceiveUpdate(string prefix)
        {
            /*while (blockRead)
            {
                UnblockRead.WaitOne();
            }*/
            blockRead = true;
            string commande = prefix + ",getp\r\n";
            EnvoyerCommande(commande);

            Thread.Sleep(100);
            byte[] bytesRead = new byte[m_portCOM.BytesToRead];
            m_portCOM.Read(bytesRead, 0, m_portCOM.BytesToRead);

            blockRead = false;
            UnblockRead.Set();

            char[] feedback_chars = System.Text.Encoding.UTF8.GetChars(bytesRead);
            
            string feedback = new string(feedback_chars);
            if (feedback != null && feedback.Length >= 2) {
                //if(feedback.Substring(feedback.Length - 4) == "\r\n") {
                    feedback = feedback.Substring(0, feedback.Length - 2);
                //}

            }
            return feedback;
        }

        public bool drive(int distance, int vitesse)
        {
            string commande;
            distance = distance / RAPPORT_DISTANCE_CODEUR_VERS_MM;
            Init();
            currentMode = "D";
            commande = "D,p" + distance + "s" + vitesse + "\r\n";
            if (!EnvoyerCommande(commande)) return false;
            Informations.printInformations(Priority.MEDIUM, commande);
            Informations.printInformations(Priority.MEDIUM, "Kangaroo - Drive : sent command, waiting for move completion");
            MoveCompleted.WaitOne();
            currentMode = null;
            Thread.Sleep(100);
            return true;
        }

        public bool rotate(double angle, int vitesse)
        {
            string commande;
            angle = RAPPORT_ANGLE_DEGRE_VERS_CODEUR * angle;
            Init();
            currentMode = "T";
            commande = "T,p" + (int)(angle) + "s" + vitesse + "\r\n";
            Informations.printInformations(Priority.MEDIUM, "Command sent: " + commande);
            if (!EnvoyerCommande(commande)) return false;
            Informations.printInformations(Priority.MEDIUM, "Kangaroo - Turn : sent command, waiting for move completion");
            MoveCompleted.WaitOne();
            currentMode = null;
            Thread.Sleep(100);
            return true;
        }

        public void stop()
        {
            this.EnvoyerCommande("T,powerdown\r\n");
            this.EnvoyerCommande("D,powerdown\r\n");
        }

        public double RecallageX(double newY, int timeSleep, int speed, int distance, double angle){
            drive(distance, speed);
            position = new PointOriente(position.x, newY, angle);
            return newY;
        }
        public double RecallageY(double newX, int timeSleep, int speed, int distance, double angle)
        {
            drive(distance, speed);
            position = new PointOriente(newX, position.y, angle);
            return newX;
        }
           
    }
}

