using System; // test
using Microsoft.SPOT;
using Gadgeteer;
using Robot_P16.Robot.composants;


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

        public readonly int GR_SOCKET_SERVOS = 0;
        public readonly int PR_SOCKET_SERVOS = 0;

        public readonly int GR_SOCKET_INFRAROUGE = 0;
        public readonly int PR_SOCKET_INFRAROUGE = 0;


        public readonly int GR_SOCKET_LANCEUR = 0;

        public readonly int GR_SOCKET_BASE_ROUlANTE = 4;
        public readonly int PR_SOCKET_BASE_ROUlANTE = 8;

        public readonly DisplayTE35 ecranTactile = new DisplayTE35(14, 13, 12, 10);

        public composants.BaseRoulante.BaseRoulante BASE_ROULANTE;

        //public readonly GHIElectronics.Gadgeteer.FEZSpider GR_MAINBOARD;
        //public readonly GHIElectronics.Gadgeteer.FEZSpider PR_MAINBOARD;


        /* ********************************** GRAND ROBOT ****************************** */

        public composants.Servomoteurs.AX12 GR_SERVO_PLATEAU;
        public composants.Servomoteurs.AX12 GR_SERVO_TRAPPE;

        /* ********************************** FIN GRAND ROBOT ****************************** */

        /* ********************************** PETIT ROBOT ****************************** */

        public composants.Servomoteurs.AX12 PR_SERVO_ASCENSEUR_BRAS_DROIT;
        public composants.Servomoteurs.AX12 PR_SERVO_ASCENSEUR_BRAS_GAUCHE;
        public composants.Servomoteurs.AX12 PR_SERVO_ROTATION_BRAS_GAUCHE;
        public composants.Servomoteurs.AX12 PR_SERVO_ROTATION_BRAS_DROIT;
        public composants.Servomoteurs.AX12 PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE;
        public composants.Servomoteurs.AX12 PR_SERVO_POUSSOIRJOKER;
        public composants.Servomoteurs.AX12 PR_SERVO_AIGUILLAGE;

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


        public Robot(composants.IHM.Parametrization parametrization)
        {


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

            Debug.Print("Starting ROBOT !!!");
            parametrization.startMethod();
        }

        public void Start()
        {
            Debug.Print("Called robot.Start()");
            GestionnaireAction.startActions(this.modeOperatoire, this.typeRobot);
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
                    /*
                    PR_SERVO_ASCENSEUR_BRAS_DROIT;
                    PR_SERVO_ASCENSEUR_BRAS_GAUCHE;
                    PR_SERVO_ROTATION_BRAS_GAUCHE;
                    PR_SERVO_ROTATION_BRAS_DROIT;
                    PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE;
                    PR_SERVO_POUSSOIRJOKER;
                    PR_SERVO_AIGUILLAGE;*/

                    BASE_ROULANTE = new composants.BaseRoulante.BaseRoulante(PR_SOCKET_BASE_ROUlANTE);

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
