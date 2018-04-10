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

        /// <summary>
        /// Liste des composants
        /// Pas élégant mais efficace pour pas avoir ?convertir les composants de tous les côtés...
        /// </summary>

        public readonly int GR_SOCKET_SERVOS = 11;
        public readonly int PR_SOCKET_SERVOS = 11;

        public readonly int GR_SOCKET_INFRAROUGE = 5;
        public readonly int PR_SOCKET_INFRAROUGE = 5;
        public readonly int PR_SOCKET_ULTRASON = 6;

        public readonly int PR_SOCKET_JACK = 5;
        public readonly int GR_SOCKET_JACK = 5;

        public readonly int PR_PORT_INFRAROUGE_1 = 6;
        public readonly int PR_PORT_INFRAROUGE_2 = 7;
        public readonly int PR_PORT_JACK = 8;


        public readonly int GR_SOCKET_LANCEUR = 0;

        public readonly int GR_SOCKET_BASE_ROUlANTE = 4;
        public readonly int PR_SOCKET_BASE_ROUlANTE = 8;

        /* ****** COMPOSANTS COMMUNS ****** */

        public readonly DisplayTE35 ecranTactile = new DisplayTE35(14, 13, 12, 10);

        public composants.BaseRoulante.BaseRoulante BASE_ROULANTE;
        public composants.Jack JACK;
        public composants.CapteursObstacle.CapteurObstacleManager OBSTACLE_MANAGER;
        public composants.IHM.C_IHM IHM;


        /* ********************************** GRAND ROBOT ****************************** */

        public composants.Servomoteurs.AX12 GR_SERVO_PLATEAU;
        public composants.Servomoteurs.AX12 GR_SERVO_TRAPPE;

        /* ********************************** FIN GRAND ROBOT ****************************** */

        /* ********************************** PETIT ROBOT ****************************** */

        public composants.Servomoteurs.AX12 PR_SERVO_ASCENSEUR_BRAS_DROIT; // ID = 5
        public composants.Servomoteurs.AX12 PR_SERVO_ASCENSEUR_BRAS_GAUCHE; // ID = 4
        public composants.Servomoteurs.AX12 PR_SERVO_ROTATION_BRAS_GAUCHE; // ID = 1
        public composants.Servomoteurs.AX12 PR_SERVO_ROTATION_BRAS_DROIT; // ID = 2
        public composants.Servomoteurs.AX12 PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE; // ID = 3
        public composants.Servomoteurs.AX12 PR_SERVO_POUSSOIRJOKER; // ID = 7 ?
        public composants.Servomoteurs.AX12 PR_SERVO_AIGUILLAGE; // ID = 6 ?

        private int PR_SERVO_ID_ASCENSEUR_BRAS_DROIT = 5;
        private int PR_SERVO_ID_ASCENSEUR_BRAS_GAUCHE = 4;
        private int PR_SERVO_ID_ROTATION_BRAS_GAUCHE = 1;
        private int PR_SERVO_ID_ROTATION_BRAS_DROIT = 2;
        private int PR_SERVO_ID_DEPLOIEMENT_BRAS_GAUCHE = 3;
        private int PR_SERVO_ID_POUSSOIRJOKER = 7;
        private int PR_SERVO_ID_AIGUILLAGE = 6;

        public composants.CapteursObstacle.Infrarouge PR_INFRAROUGE_1;
        public composants.CapteursObstacle.Infrarouge PR_INFRAROUGE_2;
        public composants.CapteursObstacle.Ultrason PR_ULTRASON;



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

            /*Debug.Print("Starting ROBOT !!!");
            parametrization.startMethod();*/
        }

        public void Start()
        {
            Debug.Print("Called robot.Start()");
            //GestionnaireAction.startActions(this.modeOperatoire, this.typeRobot);
            new Thread(() => GestionnaireAction.startActions(this.modeOperatoire, this.typeRobot)).Start();
        }

        public void loadComponents()
        {
            switch (TypeRobot)
            {
                case TypeRobot.GRAND_ROBOT:

                    break;

                case TypeRobot.PETIT_ROBOT:
                    // Port 5 Spider : Breakout => 4 = IR AVG, 5 = IR AVD, 8 = Jack
                    // Port 6 Spider : Ultrason
                    // Port 1 Spider : USB Client
                    // Port 11 Spider : AX-12 => ID ?
                    // Port 8 : Kangaroo
                    // Quel socket pour les pompes ?
                    
                    PR_SERVO_ASCENSEUR_BRAS_DROIT = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_ASCENSEUR_BRAS_DROIT);
                    PR_SERVO_ASCENSEUR_BRAS_GAUCHE = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_ASCENSEUR_BRAS_GAUCHE);
                    PR_SERVO_ROTATION_BRAS_GAUCHE = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_ROTATION_BRAS_GAUCHE);
                    PR_SERVO_ROTATION_BRAS_DROIT = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_ROTATION_BRAS_DROIT);
                    PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_DEPLOIEMENT_BRAS_GAUCHE);
                    PR_SERVO_POUSSOIRJOKER = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_POUSSOIRJOKER);
                    PR_SERVO_AIGUILLAGE = new composants.Servomoteurs.AX12(PR_SOCKET_SERVOS, PR_SERVO_ID_AIGUILLAGE);

                    BASE_ROULANTE = new composants.BaseRoulante.BaseRoulante(PR_SOCKET_BASE_ROUlANTE);

                    PR_INFRAROUGE_1 = new composants.CapteursObstacle.Infrarouge(PR_SOCKET_INFRAROUGE, PR_PORT_INFRAROUGE_1, composants.CapteursObstacle.OBSTACLE_DIRECTION.ARRIERE);
                    PR_INFRAROUGE_2 = new composants.CapteursObstacle.Infrarouge(PR_SOCKET_INFRAROUGE, PR_PORT_INFRAROUGE_2, composants.CapteursObstacle.OBSTACLE_DIRECTION.ARRIERE);
                    PR_ULTRASON = new composants.CapteursObstacle.Ultrason(PR_SOCKET_ULTRASON, composants.CapteursObstacle.OBSTACLE_DIRECTION.AVANT);
                    
                    JACK = new composants.Jack(PR_SOCKET_JACK, PR_PORT_JACK);

                    composants.CapteursObstacle.CapteurObstacle[] capteurs = {PR_INFRAROUGE_1, PR_INFRAROUGE_2, PR_ULTRASON};
                    OBSTACLE_MANAGER = new composants.CapteursObstacle.CapteurObstacleManager(capteurs);

                    break;

                case TypeRobot.TEST_ROBOT_1:
                    TR1_BOUTON_1 = new Button(TR1_SOCKET_BOUTON1);
                    //TR1_BOUTON_2 = new Button(TR1_SOCKET_BOUTON2);
                    BASE_ROULANTE = new composants.BaseRoulante.BaseRoulante(GR_SOCKET_BASE_ROUlANTE);
                    break;
            }
            Informations.printInformations(Priority.MEDIUM, "composés du robot chargés");
        }

    }
}
