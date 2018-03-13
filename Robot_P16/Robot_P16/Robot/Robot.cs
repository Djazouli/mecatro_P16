using System;
using Microsoft.SPOT;
using Gadgeteer;
using Gadgeteer.Modules.GHIElectronics;
using Robot_P16.Robot.composants;

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
        /// Pas élégant mais efficace pour pas avoir à convertir les composants de tous les côtés...
        /// </summary>

        public readonly int GR_SOCKET_SERVOS = 0;
        public readonly int PR_SOCKET_SERVOS = 0;

        public readonly int GR_SOCKET_INFRAROUGE = 0;
        public readonly int PR_SOCKET_INFRAROUGE = 0;

        public readonly int GR_SOCKET_LANCEUR = 0;

        public readonly int GR_SOCKET_BASE_ROUlANTE = 0;
        public readonly int PR_SOCKET_BASE_ROUlANTE = 0;

        //public readonly DisplayTE35 ecranTactile;

        public composants.BaseRoulante.BaseRoulante BASE_ROULANTE;

        //public readonly GHIElectronics.Gadgeteer.FEZSpider GR_MAINBOARD;
        //public readonly GHIElectronics.Gadgeteer.FEZSpider PR_MAINBOARD;


        /* ********************************** GRAND ROBOT ****************************** */

        public composants.Servomoteurs.AX12 GR_SERVO_CLAPET_RESERVOIR;

        public composants.Servomoteurs.AX12 GR_SERVO_PLATEAU_BALLES;
        public composants.Servomoteurs.AX12 GR_SERVO_PLATEAU_FILTRE;

        /* ********************************** FIN GRAND ROBOT ****************************** */

        /* ********************************** PETIT ROBOT ****************************** */

        /* ********************************** FIN PETIT ROBOT ****************************** */

        /* ********************************** TEST ROBOT 1 ****************************** */

        public readonly int TR1_SOCKET_BOUTON1 = 4;
        public readonly int TR1_SOCKET_BOUTON2 = 8;

        public Button TR1_BOUTON_1;
        public Button TR1_BOUTON_2;

        /* ********************************** FIN TEST ROBOT 1 ****************************** */

        /// <summary>
        /// Fin de la liste des composants
        /// </summary>


        public Robot(composants.IHM.Parametrization parametrization)
        {
            this.typeRobot = parametrization.GetTypeRobot();
            this.modeOperatoire = parametrization.GetModeOperatoire();
            this.couleurEquipe = parametrization.GetCouleurEquipe();
             //ecranTactile = new DisplayTE35(14, 13, 12, 10); // L'écran tactile est présent sur chaque robot

            parametrization.startMethod += this.Start;
            robot = this;

            loadComponents();
        }

        public void Start()
        {
            Debug.Print("PICHE STARTED BIATCH");
            GestionnaireAction.startActions(this.modeOperatoire);
        }

        public void loadComponents()
        {
            switch (TypeRobot)
            {
                case TypeRobot.GRAND_ROBOT:

                    break;

                case TypeRobot.PETIT_ROBOT:

                    break;

                case TypeRobot.TEST_ROBOT_1:
                    TR1_BOUTON_1 = new Button(TR1_SOCKET_BOUTON1);
                    TR1_BOUTON_1 = new Button(TR1_SOCKET_BOUTON2);
                    BASE_ROULANTE = new composants.BaseRoulante.BaseRoulante(GR_SOCKET_BASE_ROUlANTE);
                    break;
            }
        }

    }
}
