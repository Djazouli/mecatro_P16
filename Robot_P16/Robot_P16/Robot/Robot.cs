using System; // test
using Microsoft.SPOT;
using Gadgeteer;
using Robot_P16.Robot.composants;
using System.Threading;


using Gadgeteer.Modules.GHIElectronics;

namespace Robot_P16.Robot
{
    public class Robot
    {

        public int score;
        public bool isStopped = false;

        public static Robot robot;
        private ModeOperatoire modeOperatoire = ModeOperatoire.UNDEFINED;
        public ModeOperatoire Mode
        {
            get { return modeOperatoire; }
            protected set
            {
                modeOperatoire = value;
            }
        }
        private CouleurEquipe couleurEquipe = CouleurEquipe.UNDEFINED;
        public CouleurEquipe Couleur
        {
            get { return couleurEquipe; }
            protected set
            {
                couleurEquipe = value;
            }
        }
        private TypeRobot typeRobot = TypeRobot.UNDEFINED;
        public TypeRobot TypeRobot
        {
            get { return typeRobot; }
            protected set
            {
                typeRobot = value;
            }
        }


        public string codeCouleur = "B-N-J";

        /// <summary>
        /// Liste des composants
        /// Pas élégant mais efficace pour pas avoir ?convertir les composants de tous les côtés...
        /// </summary>


        public bool isPRSpiderI = true;
        public bool isGRSpiderI = true;


        public readonly int GR_SOCKET_SERVOS = 11;
        public readonly int PR_SOCKET_SERVOS = 11;

        public readonly int GR_SOCKET_INFRAROUGE = 9;
        public readonly int PR_SOCKET_INFRAROUGE = 9;
        public readonly int PR_SOCKET_ULTRASON = 6;

        public readonly int PR_PORT_INFRAROUGE_1 = 5; // Arriere
        public readonly int PR_PORT_INFRAROUGE_2 = 7;// Arriere
        public readonly int PR_PORT_INFRAROUGE_3 = 3;// Arriere
        public readonly int PR_PORT_INFRAROUGE_4 = 4;// Arriere


        public readonly int GR_PORT_INFRAROUGE_1 = 3;
        public readonly int GR_PORT_INFRAROUGE_2 = 4;
        public readonly int GR_PORT_INFRAROUGE_3 = 5;
        public readonly int GR_PORT_INFRAROUGE_4 = 7;

        public readonly int PR_SOCKET_JACK = 9;
        public readonly int GR_SOCKET_JACK = 9;


        public readonly int PR_PORT_JACK = 8;
        public readonly int GR_PORT_JACK = 8;


        public readonly int GR_SOCKET_LANCEUR = 8;
        public readonly int PR_SOCKET_RECEPTEUR_CODE_COULEUR = 4;

        public readonly int PR_SOCKET_VENTOUZES = 5;
        public readonly int PR_PORT_VENTOUZES = 3;

        public readonly int GR_SOCKET_BASE_ROUlANTE = 4;
        public readonly int PR_SOCKET_BASE_ROUlANTE = 8;

        public readonly int PR_SOCKET_SWITCH_GLOBAL = 8;
        public readonly int GR_SOCKET_SWITCH_GLOBAL = 4;
        public readonly int PR_PORT_SWITCH_GLOBAL = 3;
        public readonly int GR_PORT_SWITCH_GLOBAL = 3;


        /* ****** COMPOSANTS COMMUNS ****** */

        public readonly DisplayTE35 ecranTactile = new DisplayTE35(14, 13, 12, 10);

        public composants.BaseRoulante.BaseRoulante BASE_ROULANTE;
        public composants.JackInterrupt JACK;
        public composants.CapteursObstacle.CapteurObstacleManager OBSTACLE_MANAGER;
        public composants.IHM.C_IHM IHM;
        public composants.RelaisMoteur SWITCH_GLOBAL;


        /* ********************************** GRAND ROBOT ****************************** */

        public composants.Servomoteurs.AX12 GR_SERVO_PLATEAU;
        public composants.Servomoteurs.AX12 GR_SERVO_TRAPPE;
        public composants.Servomoteurs.AX12 GR_SERVO_ABEILLE;

        private int GR_SERVO_ID_PLATEAU = 1;
        private int GR_SERVO_ID_TRAPPE = 2;
        private int GR_SERVO_ID_ABEILLE = 3;

        public composants.LanceurBalle GR_LANCEUR_BALLE;

        public composants.CapteursObstacle.Infrarouge GR_INFRAROUGE_1; // AVANT
        public composants.CapteursObstacle.Infrarouge GR_INFRAROUGE_2; // AVANT
        public composants.CapteursObstacle.Infrarouge GR_INFRAROUGE_3; // ARRIERE
        public composants.CapteursObstacle.Infrarouge GR_INFRAROUGE_4; // ARRIERE

        /* ********************************** FIN GRAND ROBOT ****************************** */

        /* ********************************** PETIT ROBOT ****************************** */

        public composants.Servomoteurs.AscenseurDroite PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW; // ID = 5
        public composants.Servomoteurs.AscenseurGauche PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW; // ID = 4

        public composants.Servomoteurs.AX12 PR_SERVO_ASCENSEUR_BRAS_DROIT; // ID = 5
        public composants.Servomoteurs.AX12 PR_SERVO_ASCENSEUR_BRAS_GAUCHE; // ID = 4
        public composants.Servomoteurs.AX12 PR_SERVO_ROTATION_BRAS_GAUCHE; // ID = 1
        public composants.Servomoteurs.AX12 PR_SERVO_ROTATION_BRAS_DROIT; // ID = 2
        public composants.Servomoteurs.AX12 PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE; // ID = 3
        public composants.Servomoteurs.AX12 PR_SERVO_POUSSOIRJOKER; // ID = 7 ?
        public composants.Servomoteurs.AX12 PR_SERVO_AIGUILLAGE; // ID = 6 ?

        private int PR_SERVO_ID_ASCENSEUR_BRAS_DROIT = 4;
        private int PR_SERVO_ID_ASCENSEUR_BRAS_GAUCHE = 7;
        private int PR_SERVO_ID_ROTATION_BRAS_GAUCHE = 2;
        private int PR_SERVO_ID_ROTATION_BRAS_DROIT = 1;
        private int PR_SERVO_ID_DEPLOIEMENT_BRAS_GAUCHE = 5;
        private int PR_SERVO_ID_POUSSOIRJOKER = 3;
        private int PR_SERVO_ID_AIGUILLAGE = 6;

        public composants.CapteursObstacle.Infrarouge PR_INFRAROUGE_1;
        public composants.CapteursObstacle.Infrarouge PR_INFRAROUGE_2;
        public composants.CapteursObstacle.Infrarouge PR_INFRAROUGE_3;
        public composants.CapteursObstacle.Infrarouge PR_INFRAROUGE_4;
        public composants.CapteursObstacle.Ultrason PR_ULTRASON;

        public composants.RelaisMoteur PR_RELAIS_VENTOUZES;
        public composants.RecepteurCodeCouleur PR_RECEPTEUR_CODE_COULEUR;


        /* ********************************** FIN PETIT ROBOT ****************************** */

        /* ********************************** TEST ROBOT 1 ****************************** */

        public readonly int TR1_SOCKET_BOUTON1 = 8;
        public readonly int TR1_SOCKET_BOUTON2 = 8;

        public Button TR1_BOUTON_1;
        public Button TR1_BOUTON_2;

        /* ********************************** FIN TEST ROBOT 1 ****************************** */

        /// <summary>
        /// Fin de la liste des composants
        /// </summary>

        public Robot(composants.IHM.Parametrization parametrization, composants.IHM.C_IHM IHM)
        {
            this.IHM = IHM;

            Debug.Print("Querying type...");
            this.typeRobot = parametrization.GetTypeRobot();
            Debug.Print("Got type : " + typeRobot.ToString());

            Debug.Print("Querying mode operatoire...");
            this.modeOperatoire = parametrization.GetModeOperatoire();
            Debug.Print("Got mode : " + modeOperatoire.ToString());

            Debug.Print("Querying couleur...");
            this.couleurEquipe = parametrization.GetCouleurEquipe();
            Debug.Print("Got couleur : " + Couleur.ToString());
             //ecranTactile = new DisplayTE35(14, 13, 12, 10); // L'écran tactile est présent sur chaque robot

            parametrization.startMethod += this.Start;

            robot = this;

            loadComponents();

            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(this.BASE_ROULANTE.REFRESH_RATE_KANGAROO);
                    this.BASE_ROULANTE.kangaroo.CheckMovingStatus();

                }
            }).Start();
            /*
            if (this.TypeRobot == TypeRobot.PETIT_ROBOT)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        //Thread.Sleep(PR_ULTRASON.REFRESH_RATE);
                        PR_ULTRASON.detectObstacle(null);
                    }
                }).Start();
            }*/


            /*Debug.Print("Starting ROBOT !!!");
            parametrization.startMethod();*/
        }

        public void Start()
        {
            Debug.Print("Called robot.Start()");
            
            //GestionnaireAction.startActions(this.modeOperatoire, this.typeRobot

            new Thread(() => GestionnaireAction.startActions(this.modeOperatoire, this.typeRobot, this.couleurEquipe)).Start();
            //new Thread(() => { Thread.Sleep(10000); Stop(); }).Start();
        }

        public void StartCountdown()
        {
            new Thread(() => { Thread.Sleep(1000 * 100); Stop(); }).Start();

            if (this.typeRobot == TypeRobot.PETIT_ROBOT)
            {
                this.PR_RECEPTEUR_CODE_COULEUR.sendStart();
                new Thread(() =>
                {
                    string code = null;
                    while (code == null)
                    {
                        Thread.Sleep(1000);
                        code = PR_RECEPTEUR_CODE_COULEUR.reception();
                        Debug.Print("code :" + code);
                    }
                    Robot.robot.codeCouleur = code;
                    Debug.Print("Code present dans le robot" + Robot.robot.codeCouleur);
                }).Start();
            }
        }

        public void Stop()
        {
            Informations.printInformations(Priority.HIGH, "Robot - called stop()");
            isStopped = true;
            this.SWITCH_GLOBAL.Activate();
            if (this.typeRobot == TypeRobot.GRAND_ROBOT)
            {
                this.GR_LANCEUR_BALLE.stop(); 
                this.GR_SERVO_PLATEAU.Stop();
                Thread.Sleep(50);
                this.GR_SERVO_ABEILLE.Stop();
                Thread.Sleep(50);
                this.GR_SERVO_TRAPPE.Stop();
                Thread.Sleep(50);
            }
            else
            {
                this.PR_RELAIS_VENTOUZES.Desactivate();
                this.PR_SERVO_AIGUILLAGE.Stop();
                Thread.Sleep(50);
                this.PR_SERVO_ASCENSEUR_BRAS_DROIT.Stop();
                Thread.Sleep(50);
                this.PR_SERVO_ASCENSEUR_BRAS_GAUCHE.Stop();
                Thread.Sleep(50);
                 this.PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE.Stop();
                Thread.Sleep(50);
                this.PR_SERVO_POUSSOIRJOKER.Stop();
                Thread.Sleep(20);
                 this.PR_SERVO_ROTATION_BRAS_DROIT.Stop();
                Thread.Sleep(50);
                this.PR_SERVO_ROTATION_BRAS_GAUCHE.Stop();
                Thread.Sleep(50);
            }
            IHM.AfficherInformation("Score : " + score, true);
        }

        public void loadComponents()
        {
            switch (TypeRobot)
            {
                case TypeRobot.GRAND_ROBOT:
                    Informations.printInformations(Priority.HIGH, "Robot : loadComponents : GRAND Robot selected !");

                    /*if (isPRSpiderI)
                        Program.Mainboard = new GHIElectronics.Gadgeteer.FEZSpider();
                    else
                        Program.Mainboard = new GHIElectronics.Gadgeteer.FEZSpiderII();*/

                    SWITCH_GLOBAL = new composants.RelaisMoteur(GR_SOCKET_SWITCH_GLOBAL, GR_PORT_SWITCH_GLOBAL);
                    //SWITCH_GLOBAL.Desactivate();

                    BASE_ROULANTE = new composants.BaseRoulante.BaseRoulante(GR_SOCKET_BASE_ROUlANTE);
                    
                    GR_SERVO_PLATEAU= new composants.Servomoteurs.AX12(GR_SOCKET_SERVOS, GR_SERVO_ID_PLATEAU);
                    GR_SERVO_TRAPPE = new composants.Servomoteurs.AX12(GR_SOCKET_SERVOS, GR_SERVO_ID_TRAPPE);
                    GR_SERVO_ABEILLE = new composants.Servomoteurs.AX12(GR_SOCKET_SERVOS, GR_SERVO_ID_ABEILLE);
                    
                    GR_LANCEUR_BALLE = new LanceurBalle(GR_SOCKET_LANCEUR);

                     
                    GR_INFRAROUGE_1 = new composants.CapteursObstacle.Infrarouge(GR_SOCKET_INFRAROUGE, GR_PORT_INFRAROUGE_1, OBSTACLE_DIRECTION.AVANT);
                    GR_INFRAROUGE_2 = new composants.CapteursObstacle.Infrarouge(GR_SOCKET_INFRAROUGE, GR_PORT_INFRAROUGE_2, OBSTACLE_DIRECTION.AVANT);
                    GR_INFRAROUGE_3 = new composants.CapteursObstacle.Infrarouge(GR_SOCKET_INFRAROUGE, GR_PORT_INFRAROUGE_3, OBSTACLE_DIRECTION.ARRIERE);
                    GR_INFRAROUGE_4 = new composants.CapteursObstacle.Infrarouge(GR_SOCKET_INFRAROUGE, GR_PORT_INFRAROUGE_4, OBSTACLE_DIRECTION.ARRIERE);


                    JACK = new composants.JackInterrupt(GR_SOCKET_JACK, GR_PORT_JACK);

                    composants.CapteursObstacle.CapteurObstacle[] capteurs_GR = { GR_INFRAROUGE_1, GR_INFRAROUGE_2, GR_INFRAROUGE_3, GR_INFRAROUGE_4 };
                    OBSTACLE_MANAGER = new composants.CapteursObstacle.CapteurObstacleManager(capteurs_GR);
                    
                    break;

                case TypeRobot.PETIT_ROBOT:
                    Informations.printInformations(Priority.HIGH, "Robot : loadComponents : PETiT Robot selected !");
                    // Port 5 Spider : Breakout => 4 = IR AVG, 5 = IR AVD, 8 = Jack
                    // Port 6 Spider : Ultrason
                    // Port 1 Spider : USB Client
                    // Port 11 Spider : AX-12 => ID ?
                    // Port 8 : Kangaroo
                    // Quel socket pour les pompes ?
                    
                    /*if (isPRSpiderI)
                        Program.Mainboard = new GHIElectronics.Gadgeteer.FEZSpider();
                    else
                        Program.Mainboard = new GHIElectronics.Gadgeteer.FEZSpiderII();*/
                    
                    SWITCH_GLOBAL = new composants.RelaisMoteur(PR_SOCKET_SWITCH_GLOBAL, PR_PORT_SWITCH_GLOBAL);
                    //SWITCH_GLOBAL.Activate();

                    PR_SERVO_ASCENSEUR_BRAS_DROIT = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_ASCENSEUR_BRAS_DROIT);
                    PR_SERVO_ASCENSEUR_BRAS_GAUCHE = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_ASCENSEUR_BRAS_GAUCHE);
                    PR_SERVO_ROTATION_BRAS_GAUCHE = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_ROTATION_BRAS_GAUCHE);
                    PR_SERVO_ROTATION_BRAS_DROIT = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_ROTATION_BRAS_DROIT);
                    PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_DEPLOIEMENT_BRAS_GAUCHE);
                    PR_SERVO_POUSSOIRJOKER = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_POUSSOIRJOKER);
                    PR_SERVO_AIGUILLAGE = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_AIGUILLAGE);
                    PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW = new composants.Servomoteurs.AscenseurDroite(PR_SOCKET_SERVOS, PR_SERVO_ID_ASCENSEUR_BRAS_DROIT);
                    PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW = new composants.Servomoteurs.AscenseurGauche(PR_SOCKET_SERVOS, PR_SERVO_ID_ASCENSEUR_BRAS_GAUCHE);
                    
                    BASE_ROULANTE = new composants.BaseRoulante.BaseRoulante(PR_SOCKET_BASE_ROUlANTE);

                    PR_INFRAROUGE_1 = new composants.CapteursObstacle.Infrarouge(PR_SOCKET_INFRAROUGE, PR_PORT_INFRAROUGE_1, OBSTACLE_DIRECTION.ARRIERE);
                    PR_INFRAROUGE_2 = new composants.CapteursObstacle.Infrarouge(PR_SOCKET_INFRAROUGE, PR_PORT_INFRAROUGE_2, OBSTACLE_DIRECTION.ARRIERE);
                    
                    PR_INFRAROUGE_3 = new composants.CapteursObstacle.Infrarouge(PR_SOCKET_INFRAROUGE, PR_PORT_INFRAROUGE_3, OBSTACLE_DIRECTION.AVANT);
                    PR_INFRAROUGE_4 = new composants.CapteursObstacle.Infrarouge(PR_SOCKET_INFRAROUGE, PR_PORT_INFRAROUGE_4, OBSTACLE_DIRECTION.AVANT);
                    //PR_ULTRASON = new composants.CapteursObstacle.Ultrason(PR_SOCKET_ULTRASON, OBSTACLE_DIRECTION.AVANT);
                    
                    //JACK = new composants.Jack(PR_SOCKET_JACK, PR_PORT_JACK);
                    JACK = new composants.JackInterrupt(PR_SOCKET_JACK, PR_PORT_JACK);

                    PR_RELAIS_VENTOUZES = new composants.RelaisMoteur(PR_SOCKET_VENTOUZES, PR_PORT_VENTOUZES);

                    composants.CapteursObstacle.CapteurObstacle[] capteurs_PR = {PR_INFRAROUGE_1, PR_INFRAROUGE_2, PR_INFRAROUGE_3, PR_INFRAROUGE_4};
                    OBSTACLE_MANAGER = new composants.CapteursObstacle.CapteurObstacleManager(capteurs_PR);

                    PR_RECEPTEUR_CODE_COULEUR = new composants.RecepteurCodeCouleur(PR_SOCKET_RECEPTEUR_CODE_COULEUR);

                    break;

                /*case TypeRobot.TEST_ROBOT_1:
                    TR1_BOUTON_1 = new Button(TR1_SOCKET_BOUTON1);
                    //TR1_BOUTON_2 = new Button(TR1_SOCKET_BOUTON2);
                    BASE_ROULANTE = new composants.BaseRoulante.BaseRoulante(GR_SOCKET_BASE_ROUlANTE);
                    break;*/
            }
            Informations.printInformations(Priority.MEDIUM, "composants du robot chargés");
        }

    }
}
