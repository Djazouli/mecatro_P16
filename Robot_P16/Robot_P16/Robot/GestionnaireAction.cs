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
            new ActionBuilder("Action send Position mÃ¨re").Add(
                    new ActionBuilder("Action send position - Action get position").BuildActionGetPosition()
                )
                .Add(
                    new ActionBuilder("Action send position - PÃ©riode du signal").BuildActionWait(PERIOD_SEND_POSITION)
                ).BuildActionEnSerieRepeated(-1); // Envois infinis

        public static void loadActions()
        {
            ACTION_PER_TYPE.Clear();
            loadActionHomologation();
            loadActionTest1();
            //loadActionPRCompete();
            loadActionPRServos();
            Informations.printInformations(Priority.HIGH, "actions chargees");
        }

        public static void startActions(ModeOperatoire mode, TypeRobot type)
        {
            Debug.Print("Starting actions with mod : " + mode.ToString() + " & type : " + type.ToString());
            if (ACTION_PER_TYPE.Contains(mode))
            {
                if (((Hashtable)ACTION_PER_TYPE[mode]).Contains(type))
                {
                    Debug.Print("Lancement de l'action mère avec le mode " + mode.ToString() + " & type : " + type.ToString());
                    ((Action)((Hashtable)ACTION_PER_TYPE[mode])[type]).Execute();
                    return;
                }
                else
                {
                    Debug.Print("MODE INTROUVABLE : Impossible de lancer l'action mère (introuvable) pour le mode : " + mode.ToString());
                }
            }
            else
            {
                Debug.Print("TYPE INTROUVABLE : Impossible de lancer l'action mère (introuvable) pour le type : " + type.ToString());
            }
        }

        private static void loadActionHomologation()
        {
            // Blablabla
            Action MOTHER_ACTION = StrategiePetitRobot.loadActionsPetitRobot();

            setMotherAction(ModeOperatoire.HOMOLOGATION, TypeRobot.PETIT_ROBOT, MOTHER_ACTION);
        }

        private static void loadActionPRCompete()
        {
            PointOriente pt1 = new PointOriente(100, 100, 50);
            PointOriente pt2 = new PointOriente(100, 200, 50);
            PointOriente pt3 = new PointOriente(100, 0, 50);
            PointOriente pt4 = new PointOriente(0, 0, 0);

            Action MOTHER_ACTION = new ActionBuilder("Action mère Test1").Add(
                    new Actions.ActionBaseRoulante("Point1 ", pt1)
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
                .Add(
                   new Actions.ActionBaseRoulante("Point4 ", pt4)
                )
                .BuildActionEnSerieRepeated(-1); // Envois infinis

            setMotherAction(ModeOperatoire.TEST1,TypeRobot.PETIT_ROBOT, MOTHER_ACTION);
        }


        private static void loadActionPRServos()
        {

            Action MOTHER_ACTION = new ActionBuilder("Action mère Test1").Add(
                    new ActionJack()
                    ).Add(
                     new ActionBuilder("Action servo test").BuildActionServo(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,
                     Robot_P16.Robot.composants.Servomoteurs.ServoCommandTypes.ABSOLUTE_ROTATION, 45)
                   ).Add(
                     new ActionBuilder("Action servo test").BuildActionServo(Robot.robot.PR_SERVO_ROTATION_BRAS_GAUCHE,
                     Robot_P16.Robot.composants.Servomoteurs.ServoCommandTypes.ABSOLUTE_ROTATION, -45)
                   ).Add(
                     new ActionBuilder("Action servo test").BuildActionServo(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT,
                     Robot_P16.Robot.composants.Servomoteurs.ServoCommandTypes.ABSOLUTE_ROTATION, 0)
                   ).Add(
                     new ActionBuilder("Action servo test").BuildActionServo(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE,
                     Robot_P16.Robot.composants.Servomoteurs.ServoCommandTypes.ABSOLUTE_ROTATION, -45)
                   ).Add(
                     new ActionBuilder("Action servo test").BuildActionServo(Robot.robot.PR_SERVO_AIGUILLAGE,
                     Robot_P16.Robot.composants.Servomoteurs.ServoCommandTypes.ABSOLUTE_ROTATION, -45)
                   ).Add(
                     new ActionBuilder("Action servo test").BuildActionServo(Robot.robot.PR_SERVO_POUSSOIRJOKER,
                     Robot_P16.Robot.composants.Servomoteurs.ServoCommandTypes.ABSOLUTE_ROTATION, -45)
                   )
                .BuildActionEnSerieRepeated(1);

            setMotherAction(ModeOperatoire.COMPETITION, TypeRobot.PETIT_ROBOT, MOTHER_ACTION);
        }

        private static void loadActionTest1()
        {
            /*Action MOTHER_ACTION = new ActionBuilder("Action mÃ¨re Test1").Add(
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

            setMotherAction(ModeOperatoire.TEST1, TypeRobot.TEST_ROBOT_1, MOTHER_ACTION);
        }

        private static void setMotherAction(ModeOperatoire mode, TypeRobot type, Action a)
        {
            if (!ACTION_PER_TYPE.Contains(mode))
            {
                ACTION_PER_TYPE.Add(mode, new Hashtable());
            }
            
            if (((Hashtable)ACTION_PER_TYPE[mode]).Contains(type))
            {
                ((Hashtable)ACTION_PER_TYPE[mode])[type] = a;
            }
            else
            {
                ((Hashtable)ACTION_PER_TYPE[mode]).Add(type, a);
            }
            Informations.printInformations(Priority.MEDIUM, "actions mere set pour le type "+type+" & le mode "+mode);
        }


    }
}
