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
        }

        public static void PRCubeRecolteReplierBras()
        {

        }

        public void PRCubeRecolteActionnerVentouse(int numeroPRCube)
        {

        }

        public void PRCubeRecolteLeverBras(int numeroPRCube)
        {

        }
        public void PRCubeRecolteDescendreBras()
        {

        }

        public void PRCubeRecolteRelacherVentouse()
        {

        }

        public void PRCubeRecolteSerrerPatins()
        {

        }

        public void PRCubeBasculeJoker(Boolean actionner)
        {

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
