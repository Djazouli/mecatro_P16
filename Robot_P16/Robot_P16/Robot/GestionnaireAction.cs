using System;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Actions;

namespace Robot_P16.Robot
{
    public class GestionnaireAction
    {

        private static Hashtable ACTION_PER_TYPE = new Hashtable();

        private static readonly int PERIOD_SEND_POSITION = 1000; // En ms
        private static readonly Action ACTION_SEND_POSITION =
            new ActionBuilder("Action send Position mère").Add(
                    new ActionBuilder("Action send position - Action get position").BuildActionGetPosition()
                )
                .Add(
                    new ActionBuilder("Action send position - Période du signal").BuildActionWait(PERIOD_SEND_POSITION)
                ).BuildActionEnSerieRepeated(-1); // Envois infinis

        public static void loadActions()
        {
            ACTION_PER_TYPE.Clear();
            loadActionHomologation();
            loadActionTest1();
        }

        public static void startActions(ModeOperatoire mode)
        {
            if (ACTION_PER_TYPE.Contains(mode))
            {
                Debug.Print("Lancement de l'action mère avec le mode " + mode.ToString());
                ((Action)ACTION_PER_TYPE[mode]).Execute();
            }
            else
            {
                Debug.Print("Impossible de lancer l'action mère (introuvable) pour le mode " + mode.ToString());
            }
        }

        private static void loadActionHomologation()
        {
            // Blablabla
        }

        private static void loadActionTest1()
        {
            Action MOTHER_ACTION = new ActionBuilder("Action mère Test1").Add(
                    new Actions.ActionsIHM.ActionBouton(Robot.robot.TR1_BOUTON_1)
                )
                .Add(
                    new ActionBuilder("Allumer LED").BuildActionDelegate(Robot.robot.TR1_BOUTON_1.TurnLedOn)
                ).Add(
                    new ActionBuilder("Wait a bit...").BuildActionWait(2000)
                )
                .Add(
                    new ActionBuilder("Eteindre LED").BuildActionDelegate(Robot.robot.TR1_BOUTON_1.TurnLedOff)
                ).BuildActionEnSerieRepeated(-1); // Envois infinis

            setMotherAction(ModeOperatoire.TEST1, MOTHER_ACTION);

        }

        private static void setMotherAction(ModeOperatoire mode, Action a)
        {
            if (ACTION_PER_TYPE.Contains(mode))
            {
                ACTION_PER_TYPE[mode] = a;
            }
            else
            {
                ACTION_PER_TYPE.Add(mode, a);
            }
        }


    }
}
