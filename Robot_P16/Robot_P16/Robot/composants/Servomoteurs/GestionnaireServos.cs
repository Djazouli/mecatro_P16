using System;
using Microsoft.SPOT;

using Robot_P16.Actions;

namespace Robot_P16.Robot.Composants.Servomoteurs
{
    public class GestionnaireServos
    {

        public const ActionServo GR_CLAPET_RESERVOIR_OUVRIR =
            new ActionBuilder("ServoGR - ouvrir clapet reservoir").BuildActionServo(
                Robot.robot.GR_SERVO_CLAPET_RESERVOIR,
                ServoCommandTypes.ABSOLUTE_ROTATION,
                DonneesServo.ANGLE_GR_CLAPET_RESERVOIR_OUVRIR);

        // Exemple d'action delegate
        public const ActionDelegate GR_CLAPET_RESERVOIR_STOP = 
            new ActionBuilder("ServoGR - stop rotation clapet")
            .BuildActionDelegate(Robot.robot.GR_SERVO_CLAPET_RESERVOIR.Stop);


        public void PRCubeRecolteDeplierBras(int numeroPRCube)
        {
            switch (numeroPRCube)
            {
                // PLACER LE BON BRAS EN FONCTION DU PRCube
            }
            string ncube = numeroPRCube.ToString();
            Informations.printInformations(Priority.HIGH, "le bras pour le cube " + ncube + " a �t� d�pli�");
        }

        public static void PRCubeRecolteReplierBras()
        {
            Informations.printInformations(Priority.HIGH, "le bras a �t� repli�");
        }

        public void PRCubeRecolteActionnerVentouse(int numeroPRCube)
        {
            string ncube = numeroPRCube.ToString();
            Informations.printInformations(Priority.HIGH, "la ventouse pour le cube " + ncube + " a �t� activ�e");
        }

        public void PRCubeRecolteLeverBras(int numeroPRCube)
        {
            string ncube = numeroPRCube.ToString();
            Informations.printInformations(Priority.HIGH, "le bras pour le cube " + ncube + " a �t� relev�");
        }
        public void PRCubeRecolteDescendreBras()
        {
            Informations.printInformations(Priority.HIGH, "le bras a �t� baiss�");
        }

        public void PRCubeRecolteRelacherVentouse()
        {
            Informations.printInformations(Priority.HIGH, "la ventouse a �t� rel�ch�e");
        }

        public void PRCubeRecolteSerrerPatins()
        {
            Informations.printInformations(Priority.HIGH, "pantins serr�s");
        }

        public void PRCubeBasculeJoker(Boolean actionner)
        {
            Informations.printInformations(Priority.HIGH, "Joker bascule");
        }
        
        /*
         * 
         * GRAND ROBOT
         * tournerPlateauBalle(int emplacement)
         * tournerPlateauBalleEmplacementSuivant(boolean sens)
         * tournerPlateauFiltre()
         * ouvrirLoquetDeColonne
         * allumer moteurs canon
         * declencherLanguetteAbeille
         * 
         * */
        





    }
}
