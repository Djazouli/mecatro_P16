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
        public Object readLocker = new Object();
        public AutoResetEvent UnblockRead = new AutoResetEvent(false);
        PointOriente position = new PointOriente(0, 0, 0);

        private int distanceIncrementale = 0;
        private int angleIncremental = 0;

        private const int RAPPORT_ANGLE_DEGRE_VERS_CODEUR = 100; //100; // * 720 / 550
        private const int RAPPORT_DISTANCE_CODEUR_VERS_MM = 1;
        private const int RAPPORT_DISTANCE_MM_VERS_CODEUR = 100; //100;

        private int CODEUR_LINES_ANGLE_360_DEGRES;// = 4017;
        private int CODEUR_VAL_ANGLE_360_DEGRES;// = 360;

        private int CODEUR_LINES_DISTANCE_1MM;// = 10240;
        private int CODEUR_VAL_DISTANCE_1MM;// = 1634;


        private const int CODEUR_LINES_ANGLE_360_DEGRES_PR = 550000;//538194;//520833;
        private const int CODEUR_VAL_ANGLE_360_DEGRES_PR = 10000;

        private const int CODEUR_LINES_DISTANCE_1MM_PR = 1024; //102400;
        private const int CODEUR_VAL_DISTANCE_1MM_PR = 170; // 1293;//

        private const int CODEUR_LINES_ANGLE_360_DEGRES_GR = 5122;
        private const int CODEUR_VAL_ANGLE_360_DEGRES_GR = 360;

        private const int CODEUR_LINES_DISTANCE_1MM_GR = 1024;
        private const int CODEUR_VAL_DISTANCE_1MM_GR = 176;

        private double ratioPointOrienteVersKangarooANGLE;// = 1.0 * 0.957;// / 2.66; //125.0 / 360.0;//617.0 / 360.0 / 5.0;//550.0 / 720.0;
        private double ratioPointOrienteVersKangarooDIST;// = 1.0 * 170.0 / 179.0;// * 0.67; //57.0/10.0;//688.0 / 1000.0;

        private const double ratioPointOrienteVersKangarooANGLE_PR = 1.0 * 1497.0 / (360*4);// * 0.957;// / 2.66; //125.0 / 360.0;//617.0 / 360.0 / 5.0;//550.0 / 720.0;
        private const double ratioPointOrienteVersKangarooDIST_PR = 1.0;// * 170.0 / 179.0;// * 0.67; //57.0/10.0;//688.0 / 1000.0;

        private const double ratioPointOrienteVersKangarooANGLE_GR = 1.0 * 0.977;// * 0.957;// / 2.66; //125.0 / 360.0;//617.0 / 360.0 / 5.0;//550.0 / 720.0;
        private const double ratioPointOrienteVersKangarooDIST_GR = 1.0 * 0.9743;// * 170.0 / 179.0;// * 0.67; //57.0/10.0;//688.0 / 1000.0;

        private const bool ROUE_LIBRE = false;

        public string currentMode = null;

        public DateTime lastInstructionSent = DateTime.Now;
        private static TimeSpan TIMESPAN_AFTER_INSTUCTION = new TimeSpan(0, 0, 0, 0, 500);

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

            if (Robot.robot.TypeRobot == TypeRobot.GRAND_ROBOT)
            {
                Informations.printInformations(Priority.HIGH, "New Kangaroo, grand robot deteted.");
                CODEUR_LINES_ANGLE_360_DEGRES = CODEUR_LINES_ANGLE_360_DEGRES_GR;
                CODEUR_VAL_ANGLE_360_DEGRES = CODEUR_VAL_ANGLE_360_DEGRES_GR;

                CODEUR_LINES_DISTANCE_1MM = CODEUR_LINES_DISTANCE_1MM_GR;
                CODEUR_VAL_DISTANCE_1MM = CODEUR_VAL_DISTANCE_1MM_GR;

                ratioPointOrienteVersKangarooANGLE = ratioPointOrienteVersKangarooANGLE_GR;
                ratioPointOrienteVersKangarooDIST = ratioPointOrienteVersKangarooDIST_GR;
            }
            else
            {
                Informations.printInformations(Priority.HIGH, "New Kangaroo, petit robot deteted.");
                CODEUR_LINES_ANGLE_360_DEGRES = CODEUR_LINES_ANGLE_360_DEGRES_PR;
                CODEUR_VAL_ANGLE_360_DEGRES = CODEUR_VAL_ANGLE_360_DEGRES_PR;
                
                CODEUR_LINES_DISTANCE_1MM = CODEUR_LINES_DISTANCE_1MM_PR;
                CODEUR_VAL_DISTANCE_1MM = CODEUR_VAL_DISTANCE_1MM_PR;

                ratioPointOrienteVersKangarooANGLE = ratioPointOrienteVersKangarooANGLE_PR;
                ratioPointOrienteVersKangarooDIST = ratioPointOrienteVersKangarooDIST_PR;
            }

            Init();
            Thread.Sleep(50);

            if (ROUE_LIBRE)
            {

                string commande = "T,p0s0\r\n";
                EnvoyerCommande(commande);


                commande = "D,p0s0\r\n";
                //EnvoyerCommande(commande);
            }
            Thread.Sleep(100);

            /*m_tailleRoues = 0;
            m_nombreLigneDrive = 0;
            m_nombreLigneRotation = 0;*/
        }

        
        public void setPosition(double x, double y, double theta)
        {
            this.position.x = x;
            this.position.y = y;
            this.position.theta = theta;
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
            //commande = "T, UNITS 1 mm ,1 lines\r\n";
            EnvoyerCommande(commande);
            //Debug.Print(commande);

            commande = "D, UNITS " + CODEUR_VAL_DISTANCE_1MM * RAPPORT_DISTANCE_MM_VERS_CODEUR + " mm = " + CODEUR_LINES_DISTANCE_1MM * RAPPORT_DISTANCE_CODEUR_VERS_MM + "\r\n";
            //commande = "D, UNITS 1 mm ,1 lines\r\n";
            EnvoyerCommande(commande);
            //Debug.Print(commande);

            if (!ROUE_LIBRE)
            {

                commande = "T,p0s0\r\n";
                EnvoyerCommande(commande);


                commande = "D,p0s0\r\n";
                EnvoyerCommande(commande);
                angleIncremental = 0;
                distanceIncrementale = 0;

            }


            return true;
        }
        public void CheckMovingStatus()
        {
            if (this.currentMode == null)
            {
                GetDynamicPosition();
                //Debug.Print("POSITION : "+this.position);
                return;
            }


            //Informations.printInformations(Priority.VERY_LOW, "Received : "+sendAndReceiveUpdate("T"));

            //Thread.Sleep(100);


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
            DateTime now = DateTime.Now;
            if (upperCased == feedback[2]) {
                if( (now - lastInstructionSent).CompareTo(TIMESPAN_AFTER_INSTUCTION) < 0)
                    Informations.printInformations(Priority.HIGH, "Kangaroo - UPDATE TOO SHORT AFTER INSTRUCTION. NOT TRIGGERING MoveCompleted");
                else {
                    Informations.printInformations(Priority.HIGH, "Kangaroo - CheckMovingStatus detected end of move");
                    // isMoving = false, done moving
                    this.currentMode = null;
                    this.MoveCompleted.Set();
            
                }
            }


            Informations.printInformations(Priority.HIGH, "Current position : " + this.position);
        }

        public PointOriente GetStaticPosition()
        {
            return position;
        }

        public PointOriente GetDynamicPosition()
        {
            //if (this.currentMode == null) return position;
            UpdatePositionFromFeedback(sendAndReceiveUpdate("T"));
            //Thread.Sleep(100);
            UpdatePositionFromFeedback(sendAndReceiveUpdate("D"));
            if (ROUE_LIBRE)
                stop(); // TODO : remove this shit
            //return PositionFromFeedback(sendAndReceiveUpdate(this.currentMode));
            //Debug.Print(position.ToString());
            return this.position;
        }


        private void UpdatePositionFromFeedback(string feedback)
        {

            Informations.printInformations(Priority.VERY_LOW, "Kangaroo - UpdatePositionFromFeedback - mode : " + this.currentMode + "; feedback : " + feedback);
            //T,P100
            if (/*this.currentMode == null || */ feedback == null || feedback.Length < 4) return;// this.position;
            char status = feedback[2];
            Informations.printInformations(Priority.LOW, "FEED : "+feedback);

            if (feedback[2] == 'E')
            {
                return;
            }
            //Debug.Print("Status : " + status);
            string sub_str = feedback.Substring(3, feedback.Length - 3);
            //Debug.Print("Deplacement read : " + sub_str);
            int deplacementFromFeedback = Int32.Parse(sub_str);
            if (feedback[0] == 'D')
            {
                double deplacement = deplacementFromFeedback - distanceIncrementale;
                distanceIncrementale = deplacementFromFeedback;
                deplacement *= RAPPORT_DISTANCE_CODEUR_VERS_MM;
                deplacement /= RAPPORT_DISTANCE_MM_VERS_CODEUR;
                deplacement = deplacement / ratioPointOrienteVersKangarooDIST;
                if (Robot.robot.TypeRobot == TypeRobot.PETIT_ROBOT)
                {
                    deplacement = -deplacement;
                }
                double angle = System.Math.PI * position.theta / 180.0;
                position.x = position.x + deplacement * System.Math.Cos(angle);
                position.y = position.y + deplacement * System.Math.Sin(angle);
                //Debug.Print("MAJ Position = " + position.x + "," + position.y + "," + position.theta);
            }
            else
            {
                double deplacement = deplacementFromFeedback - angleIncremental;
                angleIncremental = deplacementFromFeedback;
                deplacement /= RAPPORT_ANGLE_DEGRE_VERS_CODEUR;
                deplacement = deplacement / ratioPointOrienteVersKangarooANGLE;
                if (Robot.robot.TypeRobot == TypeRobot.GRAND_ROBOT)
                {
                    deplacement = -deplacement;
                }
                position.theta += deplacement;
                //Debug.Print("MAJ Position = " + position.x + "," + position.y + "," + position.theta);
            }
        }


        private string sendAndReceiveUpdate(string prefix)
        {
            lock (readLocker)
            {
                string commande = prefix + ",getp\r\n";
                EnvoyerCommande(commande);

                string feedback = "";
                int suffixLeftToRead = 2;
                while (suffixLeftToRead > 0)
                {
                    char charRead = (char)m_portCOM.ReadByte();
                    if (charRead == '\r' || charRead == '\n')
                        suffixLeftToRead--;
                    else
                        feedback += charRead;
                }
                /*byte[] bytesRead = new byte[m_portCOM.BytesToRead];
                m_portCOM.Read(bytesRead, 0, m_portCOM.BytesToRead);

                blockRead = false;*
                //UnblockRead.Set();

                char[] feedback_chars = System.Text.Encoding.UTF8.GetChars(bytesRead);

                string feedback = new string(feedback_chars);*/
                if (feedback != null && feedback.Length >= 2)
                {
                    //if(feedback.Substring(feedback.Length - 4) == "\r\n") {
                    //feedback = feedback.Substring(0, feedback.Length - 2);
                    //}

                }

                Informations.printInformations(Priority.VERY_LOW, (feedback+",len:"+feedback.Length));
                return feedback;
            }
        }

        public bool drive(int distance, int vitesse)
        {
            lastInstructionSent = DateTime.Now;
            Informations.printInformations(Priority.HIGH, "Drive, Distance demande : " + distance );
            string commande;
            vitesse *= RAPPORT_DISTANCE_MM_VERS_CODEUR;
            distance = (int)((double)(distance) / (double)(RAPPORT_DISTANCE_CODEUR_VERS_MM) * (double)(RAPPORT_DISTANCE_MM_VERS_CODEUR) * ratioPointOrienteVersKangarooDIST);
            Init();
            currentMode = "D";
            commande = "D,p" + distance + "s" + vitesse + "\r\n";
            if (!EnvoyerCommande(commande)) return false;
            Informations.printInformations(Priority.MEDIUM, commande);
            Informations.printInformations(Priority.MEDIUM, "Kangaroo - Drive : sent command, waiting for move completion");
            MoveCompleted.WaitOne();
            currentMode = null;
            //Thread.Sleep(100);
            return true;
        }

        public bool rotate(double angle, int vitesse)
        {
            lastInstructionSent = DateTime.Now;
            string commande;
            Informations.printInformations(Priority.HIGH, "Rotate, Angle demande : " + angle);
            vitesse *= RAPPORT_ANGLE_DEGRE_VERS_CODEUR;
            angle = RAPPORT_ANGLE_DEGRE_VERS_CODEUR * angle * ratioPointOrienteVersKangarooANGLE;
            Init();
            currentMode = "T";
            commande = "T,p" + (int)(angle) + "s" + vitesse + "\r\n";
            Informations.printInformations(Priority.MEDIUM, "Command sent: " + commande);
            if (!EnvoyerCommande(commande)) return false;
            Informations.printInformations(Priority.MEDIUM, "Kangaroo - Turn : sent command, waiting for move completion");
            MoveCompleted.WaitOne();
            currentMode = null;
            //Thread.Sleep(100);
            return true;
        }

        public void stop()
        {
            
            //Init();
            //this.CheckMovingStatus();
            Informations.printInformations(Priority.MEDIUM, "Kangaroo - called stop()");
            //
            //Init();
            if (currentMode != null)
            {
                this.EnvoyerCommande("T,powerdown\r\n");
                this.EnvoyerCommande("D,powerdown\r\n");
                //this.EnvoyerCommande(currentMode+",p"+distanceIncrementale+"\r\n");
                MoveCompleted.Set();
                
            }
            //MoveCompleted.Set();
        }

        public double RecallageX(double newY, int timeSleep, int speed, int distance, double angle){
            Init();
            string commande = "D,p" + distance + "s" + speed + "\r\n";
            EnvoyerCommande(commande);
            Thread.Sleep(timeSleep);
            stop();
            position.y = newY;
            position.theta = angle;
            return newY;
        }
        public double RecallageY(double newX, int timeSleep, int speed, int distance, double angle)
        {
            Init();
            string commande = "D,p" + distance + "s" + speed + "\r\n";
            EnvoyerCommande(commande);
            Thread.Sleep(timeSleep);
            stop();
            position.x = newX;
            position.theta = angle;
            return newX;
        }
           
    }
}

