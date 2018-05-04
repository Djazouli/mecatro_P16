using System;
using Microsoft.SPOT;
using System.IO.Ports;
using System.Threading;

using GT = Gadgeteer;
using Robot_P16.Map;
using Robot_P16.Robot.composants;

namespace Robot_P16.Robot.composants.BaseRoulante
{


    /*
     * 

IsMoving : False
    #### Exception System.Exception - 0x00000000 (5) ####
    #### Message: 
    #### System.Convert::ToInt64 [IP: 00af] ####
    #### System.Convert::ToInt32 [IP: 0011] ####
    #### Robot_P16.Robot.composants.BaseRoulante.Kangaroo::getDataSinceLastReset [IP: 00fc] ####
    #### Robot_P16.Robot.composants.BaseRoulante.Kangaroo::updatePosition [IP: 007a] ####
    #### Robot_P16.Robot.composants.BaseRoulante.Kangaroo::allerEn [IP: 007a] ####
    #### Robot_P16.Robot.composants.BaseRoulante.BaseRoulante::AvanceAndSleep [IP: 000d] ####
    #### Robot_P16.Robot.composants.BaseRoulante.BaseRoulante::GoToOrientedPoint [IP: 0141] ####
    #### Robot_P16.Robot.composants.BaseRoulante.BaseRoulante::GoToOrientedPoint [IP: 0143] ####
    #### Robot_P16.Actions.ActionBaseRoulante::Execute [IP: 004b] ####
    #### Robot_P16.Actions.Action::OnStatusChange [IP: 0020] ####
    #### Robot_P16.Actions.ActionWait::Execute [IP: 000e] ####
    #### Robot_P16.Actions.Action::OnStatusChange [IP: 0020] ####
    #### Robot_P16.Actions.Action::set_Status [IP: 0010] ####
    #### Robot_P16.Robot.composants.BaseRoulante.BaseRoulante::LaunchMovingStatusChangeEvent [IP: 0015] ####
    #### Robot_P16.Robot.composants.BaseRoulante.BaseRoulante::GoToOrientedPoint [IP: 01df] ####
    #### Robot_P16.Robot.composants.BaseRoulante.BaseRoulante::GoToOrientedPoint [IP: 0143] ####
    #### Robot_P16.Actions.ActionBaseRoulante::Execute [IP: 004b] ####
    #### Robot_P16.Actions.Action::OnStatusChange [IP: 0020] ####
    #### Robot_P16.Actions.Action::set_Status [IP: 0010] ####
    #### Robot_P16.Actions.ActionEnSerie::Execute [IP: 004b] ####
    #### Robot_P16.Robot.Robot::<Start>b__1 [IP: 000b] ####
A first chance exception of type 'System.Exception' occurred in mscorlib.dll
An unhandled exception of type 'System.Exception' occurred in mscorlib.dll
     * */

    public enum mode
        {
            #region
            /*
         * Choix du mode ?sélectionner avec le switch du kangaroo
         * 
         * Mode mixte (drive[Avancer ou reculer] et turn[tourner ?droite/gauche]) :
         * Controle des deux moteurs en même temps
         * 
         * Mode Independant :
         * Le moteur est control?indépendament de l'autre (S1 = moteur 1 et S2 = moteur 2)
        */
            #endregion
            moteur1 = 0x31, moteur2 = 0x32, drive = 0x44, turn = 0x54
        };
     
        public enum unite
        {
            //Coefficient ?déterminer pour obtenir la bonne conversion d'unit?
            mm = 1, cm = 10, m = 1000, degre = 1
        };

        public class Kangaroo : Composant
        {
            public AutoResetEvent MoveCompleted = new AutoResetEvent(false);
            SerialPort m_port;
            public GT.Timer timerCheckMovingStatus = new GT.Timer(300);

            public bool blockMoveCheck = false;

            private bool lastMovingStatus = false;

            // TO DO : sauvegarder coordonnées : position & angle 
            // l'info des positions sera mieux dans BaseRoulante au lieu dans Kangaroo ???
            PointOriente position = new PointOriente(0,0,0);

            private mode current_mode = mode.drive;


            public Kangaroo(int socket)
                : base(socket)
            {
                /*
                 * Instanciation de la Kangaroo sur le socket de la Spider donn?en paramètre
                 * Utilit?de Read et Write TimeOut ?déterminer
                */
                string COMPort = GT.Socket.GetSocket(socket, true, null, null).SerialPortName;

                Debug.Print("Tring to open serial port on COMPORt :" + COMPort);
                m_port = new SerialPort(COMPort, 9600, Parity.None, socket, StopBits.One);
                Debug.Print("Opening OK !");

                m_port.ReadTimeout = 500;
                m_port.WriteTimeout = 500;
                m_port.Open();
                init();

                timerCheckMovingStatus.Tick += (t) => checkIsMoving();
            }

            public PointOriente getPosition()
            {
                Informations.printInformations(Priority.VERY_LOW, "la position orientée a été récupérée");
                return position;
            }

            public void setPosition(PointOriente pt)
            {
                position = pt;
                Informations.printInformations(Priority.LOW, "la position a été redéfinie");
            }


            public void checkIsMoving()
            {
                bool currentlyMoving = isCurrentlyMoving();
                if (currentlyMoving != this.lastMovingStatus)
                {
                    this.lastMovingStatus = currentlyMoving;
                    if (currentlyMoving == false)
                    {
                        Informations.printInformations(Priority.MEDIUM, "CheckIsmoving : calls moveCompleted");
                        MoveCompleted.Set();
                    }
                }
            }

            public bool isCurrentlyMoving()
            {
                /*
                 * 
                 *  Dans baseRoulante : check des IR pour interromptre mouvement : PAS DANS ACTION_BASEROULANTE
                 *  
                 *  Dans baseRoulante : trigger d'un event "Done Doing movement" => pour le point de vue extérieur
                 *  
                 * 
                 * */
                String commande, sPosition, sErreur;
                byte[] reponse = new byte[100];
                char[] tempo = new char[10];
                int codeErreur = 0;
                byte[] buffer = new byte[100];

                if (m_port.IsOpen)
                {
                    buffer[0] = (byte)this.current_mode;
                    commande = BitConverter.ToChar(buffer, 0).ToString() + ",getp\r\n";
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    int t = commande.Length;
                    m_port.Write(buffer, 0, commande.Length);

                    int i = 0;
                    do
                    {
                        reponse[i++] = (byte)m_port.ReadByte();
                    } while (reponse[i - 1] != '\n' && i < 99);
                    reponse[i] = (byte)'\0';
                    if (reponse[2] != 'E') // OK
                    {
                        //Informations.printInformations(Priority.HIGH, Convert.ToChar(reponse[2]).ToString());

                        char toChar = Convert.ToChar(reponse[2]);
                        if (toChar.ToUpper() == toChar)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                        
                    }
                    else
                    {
                        tempo[0] = (char)reponse[2];
                        tempo[1] = (char)reponse[3];
                        sErreur = new string(tempo);
                        codeErreur = Convert.ToInt32(sErreur, 16);

                        Informations.printInformations(Priority.HIGH, "Kangaroo - isCurrentlyMoving - Error code : " + codeErreur);
                    }
                }
                return false;

            }


            //on met a jour

            /*public void test()
            {
                Debug.Print("test is starteeeeeeeeed");
                String commande;
                byte[] buffer = new byte[100];
                bool retour = false;
                commande = "T,start\r\n";
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                m_port.Write(buffer, 0, commande.Length);
                commande = "D,start\r\n";
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                m_port.Write(buffer, 0, commande.Length);
                commande = "T,units 1000 thousandths = 1024 lines\r\n";
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                m_port.Write(buffer, 0, commande.Length);               
                commande = "D,units 1000 thousandths = 1024 lines\r\n";
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                m_port.Write(buffer, 0, commande.Length);
                commande = "T,home\r\n";
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                m_port.Write(buffer, 0, commande.Length);
                commande = "D,home\r\n";
                Thread.Sleep(5000); 
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                m_port.Write(buffer, 0, commande.Length);
                commande = "D,p1000s5000\r\n";
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                m_port.Write(buffer, 0, commande.Length);
                commande = "T,p1000s5000\r\n";
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                m_port.Write(buffer, 0, commande.Length);
                commande = "D,p0s5000\r\n";
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                m_port.Write(buffer, 0, commande.Length);
                commande = "T,p0s5000\r\n";
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                m_port.Write(buffer, 0, commande.Length);
                }*/

            
            private void updatePosition(mode m) {    
                switch(m){
                    case mode.turn:   
                        int angleDeplacement = 0;
                        double newTheta = position.theta;
                        string errorCodeAngle=null;
                        errorCodeAngle = getDataSinceLastReset(mode.turn, ref angleDeplacement); 
                        //if(errorCodeAngle == 0){
                            newTheta += angleDeplacement;
                            position = new PointOriente(position.x,position.y,newTheta);
                        //}
                        break;
                    case mode.drive:
                        int deplacement = 0;
                        double theta = position.theta;
                        double X=position.x;
                        double Y=position.y;
                        string errorCode = getDataSinceLastReset(mode.drive, ref deplacement); 
                        //if(errorCode == null){
                        double angle = System.Math.PI * theta / 180.0;
                        if (Robot.robot.TypeRobot == TypeRobot.PETIT_ROBOT)
                        {
                            X -= deplacement * System.Math.Cos(angle);
                            Y -= deplacement * System.Math.Sin(angle);
                        }
                        else
                        {
                            X += deplacement * System.Math.Cos(angle);
                            Y += deplacement * System.Math.Sin(angle);
                        }
                            position = new PointOriente(X,Y,theta);
                        //}                           
                        break;
                }
                
                Informations.printInformations(Priority.LOW, "l'information sur la position a été mise à jour");
                Informations.printInformations(Priority.LOW,"Nouvelle position : " + position.x.ToString() + "," + position.y.ToString() + "," + position.theta.ToString());
            }

            public bool start(mode m)
            {
                //Envoie de la commande Start précédée du mode (Drive ou Turn)
                //Obligatoire ?envoyer avant chaque commande !
                String commande;
                byte[] buffer = new byte[100];
                bool retour = false;
                buffer[0] = (byte)m;
                commande = BitConverter.ToChar(buffer, 0).ToString() + ",start\r\n";
                buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                if (m_port.IsOpen)
                {
                    m_port.Write(buffer, 0, commande.Length);
                    retour = true;
                    Informations.printInformations(Priority.LOW, "Init written : " + commande);
                }
                return retour;
            }

            public bool init()
            {
                Debug.Print("On est dans le init()");
                //emission des parametres
                //Obligatoire avant chaque action demandee !
                bool retour = false;
                String commande;
                byte[] buffer = new byte[100];

                if (m_port.IsOpen)
                {
                    //Envoie de la commande start             
                    start(mode.turn);
                    start(mode.drive);

                    //Preparation des parametres ?envoyer
                    //commande = "T, UNITS 36000 millidegrees = 26453 lines\r\n"; //petit robot
                    commande = "T, UNITS 36000 millidegrees = 388400 lines\r\n";
                    //Conversion des parametres en tableau d'octet
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    //Ecriture du tableau d'octets sur la ligne TX
                    m_port.Write(buffer, 0, commande.Length);

                    //commande = "D, UNITS 1 mm = 102400 lines\r\n";//petit robot      
                    commande = "D, UNITS 182 mm = 1024 lines\r\n";//petit robot
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    m_port.Write(buffer, 0, commande.Length);

                    //Reinitialiser la position et la vitesse a 0
                    commande = "T,p0s0\r\n";
                    //Conversion en tableau d'octets
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    //Envoie du tableau sur la ligne TX
                    m_port.Write(buffer, 0, commande.Length);

                    commande = "D,p0s0\r\n";
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    m_port.Write(buffer, 0, commande.Length);

                }
                return retour;
            }

            //retourne un code erreur
            //0 pas d'erreur
            private string getDataSinceLastReset(mode m, ref int deplacement)
            {
                blockMoveCheck = true;
                //Détermine la position actuel du robot
                String commande, sPosition, sErreur;
                byte[] reponse = new byte[100];
                char[] tempo = new char[10];
                string codeErreur = null;
                byte[] buffer = new byte[100];

                if (m_port.IsOpen)
                {
                    buffer[0] = (byte)m;
                    commande = BitConverter.ToChar(buffer, 0).ToString() + ",getpi\r\n";
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    int t = commande.Length;
                    m_port.Write(buffer, 0, commande.Length);

                    int i = 0;
                    Informations.printInformations(Priority.LOW, "Bits recus depuis la Kangaroo");
                    do
                    {
                        reponse[i++] = (byte)m_port.ReadByte();
                        Informations.printInformations(Priority.LOW, ((char)reponse[i-1]).ToString());
                    } while (reponse[i - 1] != '\n' && i < 99);
                    reponse[i] = (byte)'\0';
                    if (reponse[2] != 'E')
                    {
                        int j = 0;

                        int taille = 0;
                        do
                        {
                        } while (reponse[taille++] != 0x00);
                        taille--;
                        
                        for (i = 3; i < taille - 2; i++)
                        {
                            
                            tempo[j++] = (char)reponse[i];
                        }
                        sPosition = new string(tempo);
                        Informations.printInformations(Priority.LOW, "sPosition = " + sPosition);
                        deplacement = Convert.ToInt32(sPosition);
                    }
                    else
                    {
                        tempo[0] = (char)reponse[2];
                        tempo[1] = (char)reponse[3];
                        codeErreur = new string(tempo);
                        deplacement = 0;
                        Informations.printInformations(Priority.MEDIUM,"Il y a eu une erreur dans la reponse de la Kangaroo");
                    }
                }
                blockMoveCheck = false;
                return codeErreur;
            }

            public bool allerEn(int distance, int speed)//, unite u
            {
                if (Robot.robot.TypeRobot == TypeRobot.PETIT_ROBOT)
                {
                    distance = -distance;
                }
                blockMoveCheck = true;
                current_mode = mode.drive;

                //Déplacer le robot ?une distance et une vitesse donnée
                String commande;
                bool retour = false;
                byte[] buffer = new byte[100];

                //Conversion de la distance avec les constantes données plus haut
                //distance = (int)u * distance;
                //Conversion de la vitesse avec les constantes données plus haut
                //speed = speed * (int)unite.kmh;

                //Initialisation obligatoire
                init();

                if (m_port.IsOpen)
                {
                    //Préparation de la commande ?envoyer
                    commande = "D,pi" + distance.ToString() + "s" + speed.ToString() + "\r\n";
                    Debug.Print(commande);
                    //Conversion de la commande
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    //Envoie de la commande sur la ligne TX
                    m_port.Write(buffer, 0, commande.Length);
                }
                timerCheckMovingStatus.Start();
                MoveCompleted.WaitOne();
                timerCheckMovingStatus.Stop();

                updatePosition(mode.drive);
                blockMoveCheck = false;
                return retour;
            }

            public bool tourner(int angle, int speed)
            {
                if (Robot.robot.TypeRobot == TypeRobot.PETIT_ROBOT)
                {
                    angle = -angle;
                }
                if (angle == 0) return true;
                blockMoveCheck = true;
                current_mode = mode.turn;

                //Même principe que AllerEn sans la vitesse
                String commande;
                bool retour = false;
                byte[] buffer = new byte[100];

                //angle = angle * (int)unite.degre;
                //speed = speed * (int)unite.kmh;
                init();
                if (Robot.robot.TypeRobot == TypeRobot.PETIT_ROBOT)
                {
                    angle = -angle;
                }
                if (m_port.IsOpen)
                {
                    commande = "T,pi" + angle.ToString() + "s" + speed.ToString() + "\r\n";
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    Debug.Print(commande+"written");
                    m_port.Write(buffer, 0, commande.Length);
                    retour = true;
                }
                timerCheckMovingStatus.Start();
                MoveCompleted.WaitOne();
                timerCheckMovingStatus.Stop();
                Debug.Print(" done waiting");
                updatePosition(mode.turn);
                blockMoveCheck = false;
                return retour;
            }


            public bool stop()
            {
                blockMoveCheck = true;
                Informations.printInformations(Priority.MEDIUM, "Kangaro - Called stop() method. Calling powerdowns D/T now;");
                //this.updatePosition(this.current_mode);

                powerdown(mode.drive);
                powerdown(mode.turn);

                MoveCompleted.Set();

                //this.init(); // Resetting increment, position has been updated
                blockMoveCheck = false;
                return true;
            }

            private bool powerdown(mode m)
            {
                blockMoveCheck = true;
                //Envoie de la commande pour arrêter les moteurs en mode drive ou Turn
                String commande;
                bool retour = false;
                byte[] buffer = new byte[100];

                if (m_port.IsOpen)
                {
                    commande = m.ToString() + ",powerdown\r\n";
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    m_port.Write(buffer, 0, commande.Length);
                    retour = true;
                }
                blockMoveCheck = false;
                return retour;

            }

            [Obsolete]
            public bool resetCodeur()
            {
                //Émission supplémentaire des paramètres initiales
                //Utilit??déterminer
                bool retour = false;
                String commande;
                byte[] buffer = new byte[100];

                if (m_port.IsOpen)
                {
                    //Envoie de la commande start
                    start(mode.drive);
                    //Préparation des paramètres ?envoyer
                    commande = "D, UNITS 1822 mm = 10240 lines\r\n";
                    Debug.Print("reset de Drive est fait.\r\n");
                    //Conversion des paramètres en tableau d'octet
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    //Écriture du tableau d'octets sur la ligne TX
                    m_port.Write(buffer, 0, commande.Length);
                    //Réinitialiser la position et la vitesse ?0
                    start(mode.drive);
                    commande = "D,p0s0\r\n";
                    //Conversion en tableau d'octets
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    //Envoie du tableau sur la ligne TX
                    m_port.Write(buffer, 0, commande.Length);

                    //Même procédure que précédemment, cette fois-ci pour le mode Turn
                    start(mode.turn);
                    commande = "T, UNITS 360 degrees = 5032 lines\r\n";
                    Debug.Print("reset de Turn est fait.\r\n");
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    m_port.Write(buffer, 0, commande.Length);
                    start(mode.turn);
                    commande = "T,p0s0\r\n";
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    m_port.Write(buffer, 0, commande.Length);
                }
                return retour;
            }
        }

    }

