using System;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot.composants.Servomoteurs;

using Robot_P16.Map;
namespace Robot_P16.Robot
{
    public class StrategiePetitRobot {

        public Action GetMotherActionPRCompetitionForCouleurVerte()
        {
            int dis = 93; //distance entre le milieu du robot (la ou on veut les cubes, et lecentre de rotation)
            //signe y a changer selon le cote 
            PointOriente ptInit = new PointOriente(125 - dis, -108, 0);
            PointOriente pt0 = new PointOriente(125, -108, 0);
            PointOriente pt1 = new PointOriente(1130 - dis + 90, -350, 50);
            PointOriente pt2 = new PointOriente(1130 - dis + 90, -5/*-(-15 + dis)*/, -90);

            // only 0-2 used for homol

            PointOriente pt3 = new PointOriente(1230 + dis + 90, -530+10, -180); //+610
            //PointOriente ptIntermediaire = new PointOriente(1100+dis, 620, -180);
            PointOriente pt4 = new PointOriente(890 + dis, -530+10, -180);
            PointOriente zone = new PointOriente(600, -100, 0);
            PointOriente pt5 = new PointOriente(500, -200, 0);

            PointOriente pt_homol_1 = new PointOriente(1230 + dis + 90, -1500, -180); //+610
            PointOriente pt_homol_2 = new PointOriente(1230 + dis + 90, -600, -180); //+610

            /*
             * 
             * PointOriente ptInit = new PointOriente(3000 - (125 - dis), -108, 180);
            PointOriente pt0 = new PointOriente(3000 - 125, -108, 0);
            PointOriente pt1 = new PointOriente(3000 - (1130 - dis + 90), -350, 50);
            PointOriente pt2 = new PointOriente(3000 - (1130 - dis + 90), -10, 90);

            // only 0-2 used for homol

            PointOriente pt3 = new PointOriente(3000 - (1230 + dis + 90), -530, -180); //+610
            //PointOriente ptIntermediaire = new PointOriente(1100+dis, 620, -180);
            PointOriente pt4 = new PointOriente(3000 - (890 + dis), -530, -180);
            PointOriente zone = new PointOriente(3000 - 600, -100, 0);
            PointOriente pt5 = new PointOriente(3000 - 500, -200, 0);
            */

            Action MOTHER_ACTION = new ActionBuilder("test")
                //.Add(new ActionBuilder("Afficher score homol").BuildActionDelegate(() => Robot.robot.IHM.AfficherInformation("Score : 0", false)))
                .Add(new ActionJack())
                .Add(new ActionBuilder("set init").BuildActionSetPositionInitiale(ptInit.x, ptInit.y, ptInit.theta))
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 200))
                .Add(new ActionBuilder("pt0").BuildActionBaseRoulante_GOTO_ONLY(pt0))
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(false))
                .Add(new ActionBuilder("pt1").BuildActionBaseRoulante_GOTO_ONLY(pt1))
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(false))
                .Add(new ActionBuilder("pt2").BuildActionBaseRoulante_GOTO_ONLY(pt2, OBSTACLE_DIRECTION.ARRIERE))
                .Add(new ActionBuilder("Ajouter score").BuildActionAddScore(25))
                .Add(new ActionBuilder("Recallage").BuildActionRecallageAxeX(-30, 10,1,1,-90))
                /* POINTS BIDON */
                /*
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(true))

                .Add(new ActionBuilder("pt2").BuildActionBaseRoulante_GOTO_ONLY(pt_homol_1, OBSTACLE_DIRECTION.AVANT))
                .Add(new ActionBuilder("pt2").BuildActionBaseRoulante_GOTO_ONLY(pt_homol_2, OBSTACLE_DIRECTION.ARRIERE))


                .Add(new ActionBuilder("pt2").BuildActionBaseRoulante_GOTO_ONLY(pt_homol_1, OBSTACLE_DIRECTION.AVANT))
                .Add(new ActionBuilder("pt2").BuildActionBaseRoulante_GOTO_ONLY(pt_homol_2, OBSTACLE_DIRECTION.ARRIERE))
                */
                //.Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(true))
                .Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(-100, 100))
                .Add(new ActionBuilder("pt3").BuildActionBaseRoulante_GOTO_ONLY(pt3, OBSTACLE_DIRECTION.AVANT))
                //).Add(new ActionBuilder("ptInter").BuildActionBaseRoulante_GOTO_ONLY(ptIntermediaire, OBSTACLE_DIRECTION.AVANT)
                .Add(new ActionBuilder("pt4").BuildActionBaseRoulante_GOTO_ONLY(pt4, OBSTACLE_DIRECTION.AVANT))
                .Add(new ActionRamasseCube())
                .Add(new ActionBuilder("Zone").BuildActionBaseRoulante_GOTO_ONLY(zone, OBSTACLE_DIRECTION.AVANT))
                .Add(new ActionBuilder("ajouter point").BuildActionAddScore(40))
                .Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(100,100))
                
                //.Add(new ActionReleaseCube())
                //.Add(new ActionBuilder("pt5").BuildActionBaseRoulante_GOTO_ONLY(pt5, OBSTACLE_DIRECTION.ARRIERE))
                .BuildActionEnSerie();

            return MOTHER_ACTION;
        }

        public Action GetMotherActionPRCompetitionForCouleurOrange()
        {
            int dis = 93; //distance entre le milieu du robot (la ou on veut les cubes, et lecentre de rotation)
            //signe y a changer selon le cote 
            PointOriente ptInit = new PointOriente(3000 - (125 - dis), -108, 180);
            PointOriente pt0 = new PointOriente(3000 - 125, -108, 0);
            PointOriente pt1 = new PointOriente(3000 - (1130 - dis + 90), -350, 50);
            PointOriente pt2 = new PointOriente(3000 - (1130 - dis + 90), -5/*-(-15 + dis)*/, 90);

            // only 0-2 used for homol

            PointOriente pt3 = new PointOriente(3000 - (1230 + dis + 90), -530, -180); //+610
            //PointOriente ptIntermediaire = new PointOriente(1100+dis, 620, -180);
            PointOriente pt4 = new PointOriente(3000 - (890 + dis), -530, -180);
            PointOriente zone = new PointOriente(3000 - 600, -100, 0);
            PointOriente pt5 = new PointOriente(3000 - 500, -200, 0);

            /* 
            PointOriente pt3 = new PointOriente(1230 + dis + 90, -530, -180); //+610
            //PointOriente ptIntermediaire = new PointOriente(1100+dis, 620, -180);
            PointOriente pt4 = new PointOriente(890 + dis, -530, -180);
            PointOriente zone = new PointOriente(500, -100, 0);
            PointOriente pt5 = new PointOriente(500, -200, 0); */

            PointOriente pt_homol_1 = new PointOriente(3000 - (1230 + dis + 90), -1500, -180); //+610
            PointOriente pt_homol_2 = new PointOriente(3000 - (1230 + dis + 90), -600, -180); //+610

            Action MOTHER_ACTION = new ActionBuilder("test")
                //.Add(new ActionBuilder("Afficher score homol").BuildActionDelegate(() => Robot.robot.IHM.AfficherInformation("Score : 0", false)))
                .Add(new ActionJack())
                .Add(new ActionBuilder("set init").BuildActionSetPositionInitiale(ptInit.x, ptInit.y, ptInit.theta))
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 200))
                .Add(new ActionBuilder("pt0").BuildActionBaseRoulante_GOTO_ONLY(pt0))
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(true))
                .Add(new ActionBuilder("pt1").BuildActionBaseRoulante_GOTO_ONLY(pt1))
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(false))
                .Add(new ActionBuilder("pt2").BuildActionBaseRoulante_GOTO_ONLY(pt2, OBSTACLE_DIRECTION.ARRIERE)).Add(new ActionBuilder("Ajouter score").BuildActionAddScore(25))
                .Add(new ActionBuilder("Recallage").BuildActionRecallageAxeX(-30, 10, 1, 1, -90))

                /* POINTS BIDON */ /*
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(true))

                .Add(new ActionBuilder("pt2").BuildActionBaseRoulante_GOTO_ONLY(pt_homol_1, OBSTACLE_DIRECTION.AVANT))
                .Add(new ActionBuilder("pt2").BuildActionBaseRoulante_GOTO_ONLY(pt_homol_2, OBSTACLE_DIRECTION.ARRIERE))


                .Add(new ActionBuilder("pt2").BuildActionBaseRoulante_GOTO_ONLY(pt_homol_1, OBSTACLE_DIRECTION.AVANT))
                .Add(new ActionBuilder("pt2").BuildActionBaseRoulante_GOTO_ONLY(pt_homol_2, OBSTACLE_DIRECTION.ARRIERE))
                */
                .Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(-100, 100))
                .Add(new ActionBuilder("pt3").BuildActionBaseRoulante_GOTO_ONLY(pt3, OBSTACLE_DIRECTION.AVANT))
                //).Add(new ActionBuilder("ptInter").BuildActionBaseRoulante_GOTO_ONLY(ptIntermediaire, OBSTACLE_DIRECTION.AVANT)
                .Add(new ActionBuilder("pt4").BuildActionBaseRoulante_GOTO_ONLY(pt4, OBSTACLE_DIRECTION.AVANT))
                .Add(new ActionRamasseCube())
                .Add(new ActionBuilder("Zone").BuildActionBaseRoulante_GOTO_ONLY(zone, OBSTACLE_DIRECTION.AVANT))
                //.Add(new ActionReleaseCube())
                .Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(100, 100))
                .BuildActionEnSerie();

            return MOTHER_ACTION;
        }


    }
}
