using System;
using Microsoft.SPOT;
using System.IO.Ports;

using GT = Gadgeteer;
using Robot_P16.Map;
using Robot_P16.Robot.composants;

namespace Robot_P16.Robot.composants.BaseRoulante
{
    
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
            SerialPort m_port;

            // TO DO : sauvegarder coordonnées : position & angle 
            // l'info des positions sera mieux dans BaseRoulante au lieu dans Kangaroo ???
            PointOriente position = new PointOriente(0,0,0);
            public PointOriente getPosition()
            {
                Informations.printInformations(Priority.LOW, "la position orientée a été récupérée");
                return position;
            }

            public void setPosition(PointOriente pt)
            {
                position = pt;
                Informations.printInformations(Priority.LOW, "la position a été redéfinie");
            }            

            public Kangaroo(int socket) : base(socket)
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
            }

            //on met a jour
            private void updatePosition(mode m) {    
                switch(m){
                    case mode.turn:   
                        int angleDeplacement = 0;
                        double newTheta = position.theta;
                        int errorCodeAngle=0;
                        errorCodeAngle = getDataSinceLastReset(mode.turn, ref angleDeplacement); 
                        if(errorCodeAngle == 0){
                            newTheta += angleDeplacement;
                            position = new PointOriente(position.x,position.y,newTheta);
                        }
                        break;
                    case mode.drive:
                        int deplacement = 0;
                        double theta = position.theta;
                        double X=position.x;
                        double Y=position.y;
                        int errorCode = getDataSinceLastReset(mode.drive, ref deplacement); 
                        if(errorCode == 0){
                            double angle = System.Math.PI * theta / 180.0;
                            X += deplacement * System.Math.Cos(angle);
                            Y += deplacement * System.Math.Sin(angle);
                            position = new PointOriente(X,Y,theta);
                        }                           
                        break;
                }
                
                Informations.printInformations(Priority.LOW, "l'information sur la position a été mise à jour");
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
                }
                return retour;
            }

            public bool init()
            {
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
                    commande = "T, UNITS 36000 millidegrees = 26453 lines\r\n"; //petit robot           
                    //Conversion des parametres en tableau d'octet
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    //Ecriture du tableau d'octets sur la ligne TX
                    m_port.Write(buffer, 0, commande.Length);

                    commande = "D, UNITS 150 mm = 102400 lines\r\n";//petit robot      
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
            private int getDataSinceLastReset(mode m, ref int deplacement)
            {
                //Détermine la position actuel du robot
                String commande, sPosition, sErreur;
                byte[] reponse = new byte[100];
                char[] tempo = new char[10];
                int codeErreur = 0;
                byte[] buffer = new byte[100];

                if (m_port.IsOpen)
                {
                    buffer[0] = (byte)m;
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
                        deplacement = Convert.ToInt32(sPosition);
                    }
                    else
                    {
                        tempo[0] = (char)reponse[2];
                        tempo[1] = (char)reponse[3];
                        sErreur = new string(tempo);
                        codeErreur = Convert.ToInt32(sErreur, 16);
                    }
                }
                return codeErreur;
            }

            public bool allerEn(int distance, int speed)//, unite u
            {
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
                updatePosition(mode.drive);
                return retour;
            }

            public bool tourner(int angle, int speed)
            {
                //Même principe que AllerEn sans la vitesse
                String commande;
                bool retour = false;
                byte[] buffer = new byte[100];

                //angle = angle * (int)unite.degre;
                //speed = speed * (int)unite.kmh;
                init();

                if (m_port.IsOpen)
                {
                    commande = "T,pi" + angle.ToString() + "s" + speed.ToString() + "\r\n";
                    buffer = System.Text.Encoding.UTF8.GetBytes(commande);
                    Debug.Print(commande+"written");
                    m_port.Write(buffer, 0, commande.Length);
                    Debug.Print(commande + "executed");
                    retour = true;
                }
                updatePosition(mode.turn);
                return retour;
            }

            public bool powerdown(mode m)
            {
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

