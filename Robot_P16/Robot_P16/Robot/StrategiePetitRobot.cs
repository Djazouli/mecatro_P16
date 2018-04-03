using System;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot.composants.Servomoteurs;
using Robot_P16.Robot.composants.BaseRoulante;

namespace Robot_P16.Robot
{
    public class StrategiePetitRobot {

        public void loadActionsPetitRobot()
        {
            GestionnaireServosPR gestionnaireServo = null;
            Action MOTHER_ACTION = new ActionBuilder("Action mère Test1").Add(
                    new ActionBuilder("Action en série pipot").Add(
                        new Actions.ActionsIHM.ActionBouton(Robot.robot.TR1_BOUTON_1)
                    ).BuildActionEnSerie()
               )
               .Add(
                   new ActionBuilder("Allumer LED").BuildActionDelegate(Robot.robot.TR1_BOUTON_1.TurnLedOn)
               ).Add(
                   new ActionBuilder("Wait a bit...").BuildActionWait(2000)
               )
               .Add(
                   new ActionBuilder("Eteindre LED").BuildActionDelegate(Robot.robot.TR1_BOUTON_1.TurnLedOff)
               )
               .Add(
                    gestionnaireServo.GR_CLAPET_RESERVOIR_OUVRIR
                ).BuildActionEnSerie(); // Envois infinis
        }
    }
}
