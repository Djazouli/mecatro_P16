using System;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot.composants.Servomoteurs;

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
            //loadActionHomologation();
            loadActionTest1();
            loadActionPRCompete();

            loadActionPRServos();

            //loadActionTestGR();
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

        private static void loadActionTestGR()
        {
            Action MOTHER_ACTION = new ActionBuilder("Action test GR").Add(
                new ActionBuilder("Lanceur test 1").BuildActionDelegate(() => Robot.robot.GR_LANCEUR_BALLE.launchSpeed(0.5d))
                ).Add(
                new ActionBuilder("Wait a bit").BuildActionWait(3000)
                ).Add(
                new ActionBuilder("Lanceur test 1").BuildActionDelegate(() => Robot.robot.GR_LANCEUR_BALLE.stop())
                ).BuildActionEnSerie();
            setMotherAction(ModeOperatoire.COMPETITION, TypeRobot.GRAND_ROBOT, MOTHER_ACTION);

            /*Action TEST_ACTION = new ActionJack();
            setMotherAction(ModeOperatoire.COMPETITION, TypeRobot.PETIT_ROBOT, TEST_ACTION);*/
        }

        private static void loadActionHomologation()
        {
            // Blablabla
            Action MOTHER_ACTION = StrategiePetitRobot.loadActionsPetitRobot();

            setMotherAction(ModeOperatoire.HOMOLOGATION, TypeRobot.PETIT_ROBOT, MOTHER_ACTION);
        }

        private static void loadActionPRCompete()
        {
            PointOriente pt1 = new PointOriente(800, 0, 180);
            PointOriente pt4 = new PointOriente(600, 0, 180);
            PointOriente pt5 = new PointOriente(700, 0, 180);
            PointOriente pt2 = new PointOriente(300, 900, 180);
            PointOriente pt3 = new PointOriente(200, 600, 180);
            Action MOTHER_ACTION = new ActionBuilder("Action test PR").Add(
                    new ActionBuilder("Deplacement 1").BuildActionBaseRoulante_GOTO_ONLY(pt4)
                ).Add(
                    new ActionBuilder("Deplacement 1").BuildActionBaseRoulante_GOTO_ONLY(pt5)
                ).Add(
                    new ActionBuilder("Deplacement 1").BuildActionBaseRoulante_GOTO_ONLY(pt1, OBSTACLE_DIRECTION.AVANT)
                ).Add(
                    new ActionRamasseCube()
                ).BuildActionEnSerie();
            setMotherAction(ModeOperatoire.HOMOLOGATION,TypeRobot.PETIT_ROBOT, MOTHER_ACTION);
        }


        private static void loadActionPRServos()
        {

            GestionnaireServosPR gestio = new GestionnaireServosPR();
            Action MOTHER_ACTION = new ActionBuilder("Action mère Test1").Add(
                    gestio.PR_BRAS_DROIT_GOTO1000
                ).Add(
                    gestio.PR_BRAS_DROIT_ROTATION_MILIEU
                ).Add(
                     new ActionBuilder("Ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                ).Add(
                    gestio.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                ).Add(
                    gestio.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                ).Add(
                    gestio.PR_BRAS_DROIT_ROTATION_INTERIEUR
                ).Add(
                    new ActionBuilder("Ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                )
                .BuildActionEnSerie();

            setMotherAction(ModeOperatoire.COMPETITION, TypeRobot.PETIT_ROBOT, MOTHER_ACTION);
        }

        private static void loadActionTest1()
        {
            /*Action MOTHER_ACTION = new ActionBuilder("Action mere Test1").Add(
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

            PointOriente pt1 = new PointOriente(1000, 0, 50);
            PointOriente pt2 = new PointOriente(1000, 500, 50);
            PointOriente pt3 = new PointOriente(0, 500, -50);
            PointOriente pt4 = new PointOriente(0, 0, 50);

            /*Action MOTHER_ACTION = new ActionBuilder("Action mere Test1").Add(
                    new ActionBuilder("Pt1").BuildActionBaseRoulante_GOTO_ONLY(pt1, OBSTACLE_DIRECTION.AVANT)
                ).Add(
                    new ActionBuilder("Wait a bit...").BuildActionWait(1)
                )
                .Add(
                   new ActionBuilder("Pt2").BuildActionBaseRoulante_GOTO_ANGLE(pt2, OBSTACLE_DIRECTION.AVANT)
                ).Add(
                    new ActionBuilder("Wait a bit...").BuildActionWait(1)
                )
                .Add(
                   new ActionBuilder("Pt3").BuildActionBaseRoulante_GOTO_ANGLE(pt3, OBSTACLE_DIRECTION.AVANT)
                )/*.Add(
                    new ActionBuilder("Recallage").BuildActionRecallageAxeY(0, 1000, 100*100, 100*50, 0)
                )
                .Add(
                new ActionBuilder("Wait a bit...").BuildActionWait(2000)
                ).Add(
                new ActionBuilder("pt4").BuildActionBaseRoulante_GOTO_ONLY(pt4))
                .BuildActionEnSerieRepeated(-1); // Envois infinis*/
            Action MOTHER_ACTION = new ActionRamasseCube();
            //Action MOTHER_ACTION = new ActionBuilder("test").BuildActionWait(10000);
            setMotherAction(ModeOperatoire.TEST1, TypeRobot.PETIT_ROBOT, MOTHER_ACTION);
            setMotherAction(ModeOperatoire.TEST1, TypeRobot.GRAND_ROBOT, MOTHER_ACTION);
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
