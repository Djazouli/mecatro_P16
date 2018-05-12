using System;
using System.Threading;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot.composants.Servomoteurs;

using Robot_P16.Map;

namespace Robot_P16.Robot
{
    public class StrategieGrosRobot
    {

        public static double dimensionGR_X = 280;
        public static double dimensionGR_Y = 300;


        public Action GetMotherActionForCouleurVerte()
        {

            ActionBuilder MotherActionBuilder = new ActionBuilder("Gros robot - mother action couleur verte");

            //double positionInitialeGR_X = dimensionGR_X / 2;
            double positionInitialeGR_X = 390 - dimensionGR_Y / 2;
            double positionInitialeGR_Y = -(630 - dimensionGR_X / 2);
            double positionInitialieTheta = -90;

            PointOriente point_tube_uni_intermediaire_1 = new PointOriente(positionInitialeGR_X + 200, positionInitialeGR_Y, 0);
            PointOriente point_tube_uni_intermediaire_2 = new PointOriente(200 + dimensionGR_X / 2, -845, 0);
            PointOriente point_tube_uni_final = new PointOriente(100, -845, 0); // direction arriere, adjust to angle
            PointOriente point_tube_uni_lancement = new PointOriente(710 - 168, -845, 110 - 90);

            PointOriente point_abeille_intermediaire_1 = new PointOriente(1060, -845, -90);
            PointOriente point_abeille_intermediaire_2 = new PointOriente(400, -2000 + 400, -130);
            PointOriente point_abeille_final = new PointOriente(dimensionGR_Y / 2 + 40, 10 -2000 + dimensionGR_X / 2, -90);


            PointOriente point_tube_mixte_intermediaire_1 = new PointOriente(400, -2000 + 820, 0); // Parcourir la map
            PointOriente point_tube_mixte_intermediaire_2 = new PointOriente(2400, -2000 + 820, 180); // Parcourir la map => IR = on
            PointOriente point_tube_mixte_intermediaire_3 = new PointOriente(2400, -1600, 90); // Parcourir la map => IR = on
            PointOriente point_tube_mixte_final = new PointOriente(2400, -2000 + dimensionGR_X / 2 - 10 - 100, 90); // direction arriere, adjust to angle
            //PointOriente point_tube_mixte_final = new PointOriente(3000 - 2400 /*- 80*/ /* 7 cm de trop */, -2000 + dimensionGR_X / 2 - 10, 90+180); // direction avant, adjust to angle
            PointOriente point_tube_mixte_envoi_balles_vertes = new PointOriente(2390, -2000 + 400, 50); // adjust to angle, (direction avant ?) 45 avannt

            GestionnaireServosGR gestio = new GestionnaireServosGR();

            ActionEnSerie ViderTubeUni = new ActionBuilder("Vider le tube uni")
                .Add(new ActionBuilder("Ajouter score").BuildActionAddScore(10))
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 300))
                .Add(new ActionBuilder("Desactiver IR").BuildActionSetDetecteurObstacle(false))
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_tube_uni_intermediaire_2))
                .Add(gestio.GR_PLATEAU_AVANT_VERT)
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 300))
                .Add(new ActionBuilder("Deplacement 2").BuildActionBaseRoulante_GOTO_ANGLE(point_tube_uni_final, OBSTACLE_DIRECTION.ARRIERE))
                //.Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(3, 100))
                .Add(gestio.GR_TRAPPE_FERMER)
                .Add(gestio.GR_PLATEAU_RECOLTE)
                .Add(gestio.GR_PLATEAU_SLOT0)
                //.Add(new ActionWait("Wait a bit...", 500))
                .Add(new ActionBuilder("piche").BuildActionBaseRoulante_GOTO_ANGLE(point_tube_uni_lancement, OBSTACLE_DIRECTION.AVANT))
                .Add(gestio.GR_PLATEAU_LIBERATION_TUBE_UNICOULEUR())
                .Add(new ActionBuilder("Ajouter score").BuildActionAddScore(45))
                .BuildActionEnSerie();

            ActionEnSerie ActiverAbeille = new ActionBuilder("Activer l'abeille")
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 800))
                //.Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(200, 100 * 200))
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(true))
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_abeille_intermediaire_1))
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_abeille_intermediaire_2, OBSTACLE_DIRECTION.AVANT)) // change orange
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(false))
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 350))
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ANGLE(point_abeille_final, OBSTACLE_DIRECTION.AVANT))
                //.Add(new ActionDelegate("Reinit kangaroo", () => { Robot.robot.SWITCH_GLOBAL.Activate(); Thread.Sleep(300); Robot.robot.SWITCH_GLOBAL.Desactivate(); Thread.Sleep(300); }))
                //.Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(-200, 100 * 100))
                .Add(new ActionBuilder("Deplacement").BuildActionRecallageAxeX(-2000+dimensionGR_X / 2, 400, 300*100, 600*100, -90))
                //.Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_abeille_final, OBSTACLE_DIRECTION.AVANT))
                //.Add(new ActionDelegate("Reinit kangaroo", () => { Thread.Sleep(50);  Robot.robot.SWITCH_GLOBAL.Activate(); Thread.Sleep(50); Robot.robot.SWITCH_GLOBAL.Desactivate(); Thread.Sleep(1500); }))
                .Add(gestio.GR_ABEILLE_DECLENCHER)
                .Add(new ActionBuilder("Ajouter score").BuildActionAddScore(50))
                .Add(gestio.GR_PLATEAU_AVANT_VERT)
                .Add(gestio.GR_ABEILLE_REPLIER)

                .Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(-200, 300))
                /* Ajouter servoMoteur */
                .BuildActionEnSerie();

            ActionEnSerie ViderTubeMixte = new ActionBuilder("Vider le tube mixte")
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(false))
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 800)) // 700-800

                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_tube_mixte_intermediaire_1))

                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(true))
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_tube_mixte_intermediaire_2, OBSTACLE_DIRECTION.ARRIERE))

                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 600))
                

                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_tube_mixte_intermediaire_3, OBSTACLE_DIRECTION.ARRIERE))
                
                .Add(new ActionBuilder("Desactiver IR").BuildActionSetDetecteurObstacle(false))
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 200))
                .Add(gestio.GR_TRAPPE_FERMER)
                //.Add(gestio.GR_PLATEAU_AVANT_VERT)
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_tube_mixte_final, OBSTACLE_DIRECTION.ARRIERE))
                //.Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(3, 100))

                .Add(gestio.GR_PLATEAU_RECOLTE)
                .Add(new ActionBuilder("ouverture recolte mixte").BuildActionAddScore(10))
                .Add(gestio.GR_PLATEAU_LIBERATION_BALLES_COULEUR_OPPOSEE())

                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ANGLE(point_tube_mixte_envoi_balles_vertes, OBSTACLE_DIRECTION.AVANT))
                .Add(gestio.GR_PLATEAU_LIBERATION_BALLES_TUBE_MIXTE_NOTRE_COULEUR())
                .BuildActionEnSerie();

            Action MOTHER_ACTION = new ActionBuilder("Mother action - competition Gros Robot")
                .Add(new ActionJack())
                .Add(new ActionBuilder("Position initiale GR Vert").BuildActionSetPositionInitiale(positionInitialeGR_X, positionInitialeGR_Y, positionInitialieTheta))
                //.Add(new ActionBuilder("Position initiale GR Vert").BuildActionSetPositionInitiale(point_tube_uni_final.x, point_tube_uni_final.y, point_tube_uni_final.theta))
                //.Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(50, 100 * 200))
                .Add(ViderTubeUni)
                .Add(ActiverAbeille)
                .Add(ViderTubeMixte)
                .BuildActionEnSerie();

            return MOTHER_ACTION;
        }

        public Action GetMotherActionForCouleurOrange()
        {


            ActionBuilder MotherActionBuilder = new ActionBuilder("Gros robot - mother action couleur orange");

            //double positionInitialeGR_X = dimensionGR_X / 2;
            double positionInitialeGR_X = 390 - dimensionGR_Y / 2;
            double positionInitialeGR_Y = -(630 - dimensionGR_X / 2);
            double positionInitialieTheta = 90;

            //.Add(new ActionBuilder("Position initiale GR Orange").BuildActionSetPositionInitiale(3000 - positionInitialeGR_X, positionInitialeGR_Y, positionInitialieTheta))



            PointOriente point_tube_uni_intermediaire_1 = new PointOriente(3000 - (positionInitialeGR_X + 200), positionInitialeGR_Y, 0);
            PointOriente point_tube_uni_intermediaire_2 = new PointOriente(3000 - (200 + dimensionGR_X / 2), -845, 0);
            PointOriente point_tube_uni_final = new PointOriente(3000 - 100, -845 /*- 30*/, 0); // direction arriere, adjust to angle
            PointOriente point_tube_uni_lancement = new PointOriente(3000 - (710 - 168), -845, -(110 - 90 ));

            PointOriente point_abeille_intermediaire_1 = new PointOriente(3000-1060, -845, -90);
            PointOriente point_abeille_intermediaire_2 = new PointOriente(3000-400, -2000 + 400, -130);
            PointOriente point_abeille_final = new PointOriente(3000-(dimensionGR_Y / 2 + 40), 10 - 2000 + dimensionGR_X / 2, -90);

            PointOriente point_tube_mixte_intermediaire_1 = new PointOriente(3000 - 400, -2000 + 820, 0); // Parcourir la map
            PointOriente point_tube_mixte_intermediaire_2 = new PointOriente(3000 - 2400, -2000 + 820, 180); // Parcourir la map => IR = on
            PointOriente point_tube_mixte_intermediaire_3 = new PointOriente(3000 - 2400, -1600, 90); // Parcourir la map => IR = on
            PointOriente point_tube_mixte_final = new PointOriente(3000 - 2400 /*- 80*/ /* 7 cm de trop */, -2000 + dimensionGR_X / 2 - 10 - 50, 90+180); // direction avant, adjust to angle
            PointOriente point_tube_mixte_envoi_balles_vertes = new PointOriente(3000 - 2400, -2000 + 400, -50); // adjust to angle, (direction avant ?) 45 degre avant

            GestionnaireServosGR gestio = new GestionnaireServosGR();


            ActionEnSerie ViderTubeUni = new ActionBuilder("Vider le tube uni")
                .Add(new ActionBuilder("Ajouter score").BuildActionAddScore(10))
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 300))
                .Add(new ActionBuilder("Desactiver IR").BuildActionSetDetecteurObstacle(false))
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_tube_uni_intermediaire_2))
                .Add(gestio.GR_PLATEAU_AVANT_ORANGE)
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 300))
                .Add(new ActionBuilder("Deplacement 2").BuildActionBaseRoulante_GOTO_ANGLE(point_tube_uni_final, OBSTACLE_DIRECTION.AVANT))
                .Add(gestio.GR_TRAPPE_FERMER)
                .Add(gestio.GR_PLATEAU_RECOLTE)
                .Add(gestio.GR_PLATEAU_SLOT0)
                .Add(new ActionWait("Wait a bit...", 500))
                .Add(new ActionBuilder("piche").BuildActionBaseRoulante_GOTO_ANGLE(point_tube_uni_lancement, OBSTACLE_DIRECTION.ARRIERE))
                .Add(gestio.GR_PLATEAU_LIBERATION_TUBE_UNICOULEUR())
                .Add(new ActionBuilder("Ajouter score").BuildActionAddScore(45))
                .BuildActionEnSerie();


            ActionEnSerie ActiverAbeille = new ActionBuilder("Activer l'abeille")
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 800))
                //.Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(200, 100 * 200))
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(true))
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_abeille_intermediaire_1))
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_abeille_intermediaire_2, OBSTACLE_DIRECTION.AVANT)) // change orange
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(false))
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 350))
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ANGLE(point_abeille_final, OBSTACLE_DIRECTION.AVANT))
                //.Add(new ActionDelegate("Reinit kangaroo", () => { Robot.robot.SWITCH_GLOBAL.Activate(); Thread.Sleep(300); Robot.robot.SWITCH_GLOBAL.Desactivate(); Thread.Sleep(300); }))
                //.Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(-200, 100 * 100))
                .Add(new ActionBuilder("Deplacement").BuildActionRecallageAxeX(-2000 + dimensionGR_X / 2, 400, 300 * 100, 600 * 100, -90))
                //.Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_abeille_final, OBSTACLE_DIRECTION.AVANT))
                //.Add(new ActionDelegate("Reinit kangaroo", () => { Thread.Sleep(50); Robot.robot.SWITCH_GLOBAL.Activate(); Thread.Sleep(50); Robot.robot.SWITCH_GLOBAL.Desactivate(); Thread.Sleep(100); }))
                .Add(gestio.GR_ABEILLE_DECLENCHER)
                .Add(new ActionBuilder("Ajouter score").BuildActionAddScore(50))
                .Add(gestio.GR_PLATEAU_AVANT_ORANGE)
                .Add(gestio.GR_ABEILLE_REPLIER)

                .Add(new ActionBuilder("Recule un peu").BuildActionBaseRoulante_DRIVE(-200, 300))
                /* Ajouter servoMoteur */
                .BuildActionEnSerie();


            ActionEnSerie ViderTubeMixte = new ActionBuilder("Vider le tube mixte")
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(false))
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 800))

                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_tube_mixte_intermediaire_1))

                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(true))
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_tube_mixte_intermediaire_2, OBSTACLE_DIRECTION.AVANT))

                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 600))


                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_tube_mixte_intermediaire_3, OBSTACLE_DIRECTION.AVANT))

                .Add(new ActionBuilder("Desactiver IR").BuildActionSetDetecteurObstacle(false))
                .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 200))
                .Add(gestio.GR_TRAPPE_FERMER)
                //.Add(gestio.GR_PLATEAU_AVANT_ORANGE)
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(point_tube_mixte_final, OBSTACLE_DIRECTION.AVANT))
                

                .Add(gestio.GR_PLATEAU_RECOLTE)
                .Add(new ActionBuilder("ouverture recolte mixte").BuildActionAddScore(10))
                .Add(gestio.GR_PLATEAU_LIBERATION_BALLES_COULEUR_OPPOSEE())
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ANGLE(point_tube_mixte_envoi_balles_vertes, OBSTACLE_DIRECTION.ARRIERE))
                .Add(gestio.GR_PLATEAU_LIBERATION_BALLES_TUBE_MIXTE_NOTRE_COULEUR())
                .BuildActionEnSerie();

            Action MOTHER_ACTION = new ActionBuilder("Mother action - competition Gros Robot")
                .Add(new ActionJack())
                .Add(new ActionBuilder("Position initiale GR Orange").BuildActionSetPositionInitiale(3000 - positionInitialeGR_X, positionInitialeGR_Y, positionInitialieTheta))
                .Add(ViderTubeUni)
                .Add(ActiverAbeille)
                .Add(ViderTubeMixte)
                .BuildActionEnSerie();

            return MOTHER_ACTION;
        }
    }
}
