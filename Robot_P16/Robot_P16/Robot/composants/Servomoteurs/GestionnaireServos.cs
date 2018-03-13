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
            Informations.printInformations(Priority.HIGH, "le bras pour le cube " + ncube + " a été déplié");
        }

        public static void PRCubeRecolteReplierBras()
        {
            Informations.printInformations(Priority.HIGH, "le bras a été replié");
        }

        public void PRCubeRecolteActionnerVentouse(int numeroPRCube)
        {
            string ncube = numeroPRCube.ToString();
            Informations.printInformations(Priority.HIGH, "la ventouse pour le cube " + ncube + " a été activée");
        }

        public void PRCubeRecolteLeverBras(int numeroPRCube)
        {
            string ncube = numeroPRCube.ToString();
            Informations.printInformations(Priority.HIGH, "le bras pour le cube " + ncube + " a été relevé");
        }
        public void PRCubeRecolteDescendreBras()
        {
            Informations.printInformations(Priority.HIGH, "le bras a été baissé");
        }

        public void PRCubeRecolteRelacherVentouse()
        {
            Informations.printInformations(Priority.HIGH, "la ventouse a été relâchée");
        }

        public void PRCubeRecolteSerrerPatins()
        {
            Informations.printInformations(Priority.HIGH, "pantins serrés");
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
