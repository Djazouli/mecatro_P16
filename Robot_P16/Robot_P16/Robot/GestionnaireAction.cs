using System;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Actions;

using Robot_P16.Map;
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
          
            Informations.printInformations(Priority.MEDIUM, "Robot.GestionnaireAction.loadActions : actions chargées");
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
            /*Action MOTHER_ACTION = new ActionBuilder("Action mère Test1").Add(
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

            setMotherAction(ModeOperatoire.TEST1, MOTHER_ACTION);*/

            PointOriente pt1 = new PointOriente(100, 100, 50);
            PointOriente pt2 = new PointOriente(100, 200, 50);
            PointOriente pt3 = new PointOriente(100, 0, 50);

            Action MOTHER_ACTION = new ActionBuilder("Action mère Test1").Add(
                    new Actions.ActionsIHM.ActionBouton(Robot.robot.TR1_BOUTON_1)
                )
                .Add(
                    new Actions.ActionBaseRoulante("Point1 ",pt1)
                ).Add(
                    new ActionBuilder("Wait a bit...").BuildActionWait(2000)
                )
                .Add(
                   new Actions.ActionBaseRoulante("Point2 ", pt2)
                ).Add(
                    new ActionBuilder("Wait a bit...").BuildActionWait(2000)
                )
                .Add(
                   new Actions.ActionBaseRoulante("Point3 ", pt3)
                )
                .BuildActionEnSerieRepeated(-1); // Envois infinis

            setMotherAction(ModeOperatoire.TEST1, MOTHER_ACTION);
            Informations.printInformations(Priority.LOW, "Robot. GestionnaireAction.loadActionTest1 : l'action mère est définie à l'action test 1");
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
            Informations.printInformations(Priority.MEDIUM, "Robot.GestionnaireAction.setMotherAction : action mère redéfinie");
        }


    }
}
