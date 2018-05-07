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

            loadActionTestGR();
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
            double dimensionGR_X = 280;
            double dimensionGR_Y = 300;
            // dimension Y espace de depart : 63cm, dimension Y tube : 85cm
            // dimension X deuxieme tube : 240cm
            double positionInitialeGR_X = dimensionGR_X / 2;
            double positionInitialeGR_Y = -(630 - dimensionGR_Y / 2);
            PointOriente pt1 = new PointOriente(200 + dimensionGR_X / 2, -845, 0);
            PointOriente pt2 = new PointOriente(100, -845, 0);
            PointOriente pt3 = new PointOriente(2400, -845, 180);
            PointOriente pt4 = new PointOriente(2380, -1700, 90);
            PointOriente pt5 = new PointOriente(2380, -2000 - dimensionGR_X / 2, 90);
            GestionnaireServosGR gestio = new GestionnaireServosGR();
            Action MOTHER_ACTION = new ActionBuilder("Action test PR").Add(
                    new ActionBuilder("Position initiale GR Vert").BuildActionSetPositionInitiale(positionInitialeGR_X, positionInitialeGR_Y, 0)
                ).Add(
                    new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 300)
                ).Add(
                    new ActionBuilder("Deplacement 1").BuildActionBaseRoulante_GOTO_ONLY(pt1)
                )
                .Add(
                    gestio.GR_TRAPPE_FERMER
                ).Add(
                    gestio.GR_PLATEAU_AVANT_VERT
                ).Add(
                    new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 200)
                )
                .Add(
                    new ActionBuilder("Deplacement 2").BuildActionBaseRoulante_GOTO_ANGLE(pt2, OBSTACLE_DIRECTION.ARRIERE)
                ).Add(
                    gestio.GR_PLATEAU_RECOLTE_1
                ).Add(
                    gestio.GR_PLATEAU_SLOT0
                ).Add(
                    new ActionBuilder("Demarrer lanceur").BuildActionLanceurBalle(0.715)
                ).Add(
                    gestio.GR_TRAPPE_OUVRIR
                ).Add(
                    gestio.GR_PLATEAU_LIBERATION_BALLES_COTE_VERT_ARRIERE()
                ).Add(new ActionWait("Wait a bit", 1000)).Add(
                    gestio.GR_TRAPPE_FERMER
                )
                .Add(
                    new ActionBuilder("Stopper lanceur").BuildActionLanceurBalleStop()
                ).Add(
                    new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 600)
                ).Add(
                    new ActionBuilder("Deplacement 3").BuildActionBaseRoulante_GOTO_ONLY(pt3)
                ).Add(
                    new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 300)
                ).Add(
                    new ActionBuilder("Deplacement 4").BuildActionBaseRoulante_GOTO_ONLY(pt4, OBSTACLE_DIRECTION.ARRIERE)
                ).Add(
                    new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 200)
                ).Add(
                    gestio.GR_TRAPPE_FERMER
                ).Add(
                    gestio.GR_PLATEAU_AVANT_VERT
                ).Add(
                    new ActionBuilder("Deplacement 4").BuildActionBaseRoulante_GOTO_ANGLE(pt5, OBSTACLE_DIRECTION.ARRIERE)
                ).Add(
                    gestio.GR_PLATEAU_RECOLTE_1
                ).Add(
                    gestio.GR_PLATEAU_SLOT0
                ).Add(
                    new ActionBuilder("Demarrer lanceur").BuildActionLanceurBalle(0.45)
                ).Add(
                    gestio.GR_PLATEAU_LIBERATION_BALLES_ORANGE_EQUIPE_VERTE()
                ).Add(new ActionWait("Wait a bit", 1000)).Add(
                    gestio.GR_TRAPPE_FERMER
                )
                .Add(
                    new ActionBuilder("Stopper lanceur").BuildActionLanceurBalleStop()
                ).BuildActionEnSerie();
            setMotherAction(ModeOperatoire.HOMOLOGATION,TypeRobot.PETIT_ROBOT, MOTHER_ACTION);
            setMotherAction(ModeOperatoire.HOMOLOGATION, TypeRobot.GRAND_ROBOT, MOTHER_ACTION);
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

            MOTHER_ACTION = new ActionBuilder("Test gros robot").Add(
                    new ActionBuilder("Piche").BuildActionBaseRoulante_DRIVE(10, 100)
                    ).Add(

                    new ActionBuilder("Paralelle 1").Add(
                        new ActionBuilder("Wait and roll").Add(new ActionWait("piche",0)).Add(
                            new ActionBuilder("Servo tourne 1").BuildActionServoAbsolue(Robot.robot.GR_SERVO_ABEILLE, 0)
                        ).BuildActionEnSerie()).Add(
                            new ActionBuilder("Servo tourne 1").BuildActionServoAbsolue(Robot.robot.GR_SERVO_PLATEAU, 0)
                        )
                        .BuildActionEnSerie()
                        
                ).Add(
                    new ActionBuilder("Paralelle 2").Add(
                            new ActionBuilder("Servo tourne 1").BuildActionServoAbsolue(Robot.robot.GR_SERVO_ABEILLE, 1023)
                        ).Add(
                            new ActionBuilder("Servo tourne 1").BuildActionServoAbsolue(Robot.robot.GR_SERVO_PLATEAU, 1023)
                        )
                        .BuildActionEnSerie()
                ).Add(
                    new ActionBuilder("Paralelle 3").Add(
                            new ActionBuilder("Servo tourne 1").BuildActionServoAbsolue(Robot.robot.GR_SERVO_ABEILLE, 500)
                        ).Add(
                            new ActionBuilder("Servo tourne 1").BuildActionServoAbsolue(Robot.robot.GR_SERVO_PLATEAU, 500)
                        )
                        .BuildActionEnSerie()
                ).BuildActionEnSerie();

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
