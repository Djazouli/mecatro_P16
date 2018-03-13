using System;
using Microsoft.SPOT;
using Gadgeteer;
using Gadgeteer.Modules.GHIElectronics;
using Robot_P16.Robot.Composants;

namespace Robot_P16.Robot
{
    public class Robot
    {
        public static readonly Robot robot;
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

        public readonly DisplayTE35 ecranTactile;

        //public readonly GHIElectronics.Gadgeteer.FEZSpider GR_MAINBOARD;
        //public readonly GHIElectronics.Gadgeteer.FEZSpider PR_MAINBOARD;

        public readonly Composants.Servomoteurs.AX12 GR_SERVO_CLAPET_RESERVOIR;

        public readonly Composants.Servomoteurs.AX12 GR_SERVO_PLATEAU_BALLES;
        public readonly Composants.Servomoteurs.AX12 GR_SERVO_PLATEAU_FILTRE;

        /// <summary>
        /// Fin de la liste des composants
        /// </summary>


        public Robot()
        {
             ecranTactile = new DisplayTE35(14, 13, 12, 10); // L'écran tactile est présent sur chaque robot
        }

        public void loadComponents()
        {
            switch (TypeRobot)
            {
                case TypeRobot.GRAND_ROBOT:

                    break;

                case TypeRobot.PETIT_ROBOT:

                    break;
            }
            Informations.printInformations(Priority.MEDIUM, "composés du robot chargés");
        }

    }
}
