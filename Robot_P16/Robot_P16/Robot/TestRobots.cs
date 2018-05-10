using System;
using System.Threading;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot.composants.Servomoteurs;

using Robot_P16.Map;

namespace Robot_P16.Robot
{
    public class TestRobots
    {


        public Action TestRecallage()
        {
            Action MOTHER_ACTION = new ActionBuilder("Test recallage")
            .Add(new ActionBuilder("Afficher score homol").BuildActionBaseRoulante_DRIVE(500,50))
            .Add(new ActionDelegate("Reinit kangaroo", () => { Thread.Sleep(50); Robot.robot.SWITCH_GLOBAL.Activate(); Thread.Sleep(50); Robot.robot.SWITCH_GLOBAL.Desactivate(); Thread.Sleep(1500); }))
            .Add(new ActionBuilder("Afficher score homol").BuildActionBaseRoulante_DRIVE(-500, 50))
            .Add(new ActionBuilder("Afficher score homol").BuildActionBaseRoulante_DRIVE(300, 50))
            .BuildActionEnSerie();
            return MOTHER_ACTION;
        }

        public Action GetActionTestScore()
        {
            Action MOTHER_ACTION = new ActionBuilder("Test IR")
            .Add(new ActionBuilder("Afficher score homol").BuildActionDelegate(() => Robot.robot.IHM.AfficherInformation("Score : 0", false)))
            .Add(new ActionBuilder("Start robot countdown").BuildActionDelegate(() => Robot.robot.StartCountdown()))
            .BuildActionEnSerie();
            return MOTHER_ACTION;
        }


        public Action GetActionCalibragePR()
        {
            Action MOTHER_ACTION = new ActionBuilder("Test IR")
            //.Add(new ActionBuilder("Tourne 90").BuildActionBaseRoulante_DRIVE(1000,100*600))
            .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 50))
           .Add(new ActionBuilder("Deplacement bidon").BuildActionBaseRoulante_GOTO_ONLY(new PointOriente(100, 0)))
           .Add(new ActionWait("Wait a bit...", 10000))
           .Add(new ActionBuilder("Deplacement bidon").BuildActionBaseRoulante_GOTO_ONLY(new PointOriente(100, -100)))
           .Add(new ActionWait("Wait a bit...", 10000))
           .Add(new ActionBuilder("Deplacement bidon").BuildActionBaseRoulante_GOTO_ONLY(new PointOriente(0, -100)))
           .Add(new ActionWait("Wait a bit...", 10000))
           .Add(new ActionBuilder("Deplacement bidon").BuildActionBaseRoulante_GOTO_ONLY(new PointOriente(0, 0)))
           .Add(new ActionWait("Wait a bit...", 10000))
           .BuildActionEnSerieRepeated(-1);

            return MOTHER_ACTION;
        }

        public Action GetActionTestIR()
        {
            Action MOTHER_ACTION = new ActionBuilder("Test IR")
            .Add(new ActionBuilder("Regler vitese drive").BuildActionDelegate(() => Robot.robot.BASE_ROULANTE.speedDrive = 200))
                .Add(new ActionBuilder("Activer IR").BuildActionSetDetecteurObstacle(true))
                .Add(new ActionBuilder("Deplacement").BuildActionBaseRoulante_GOTO_ONLY(new PointOriente(1000, 0), OBSTACLE_DIRECTION.AVANT))
           .BuildActionEnSerieRepeated(1);

            return MOTHER_ACTION;
        }

        public Action GetActionTestJack()
        {
            Action MOTHER_ACTION = new ActionBuilder("Test jack")
            .Add(new ActionJack())
            .Add(new ActionBuilder("Deplacement bidon").BuildActionBaseRoulante_DRIVE(1000, 100))
            .BuildActionEnSerie();

            return MOTHER_ACTION;
        }

        public Action GetActionTestTimerExtinctionPR()
        {
            Action MOTHER_ACTION = new ActionBuilder("Test timer switch global PR")
            .Add(new ActionDelegate("Rotate forever", () => Robot.robot.PR_SERVO_AIGUILLAGE.RotateForever(400)))
            .Add(new ActionWait("Wait a bit...", 5000))
            .Add(new ActionDelegate("Switch off", () => Robot.robot.SWITCH_GLOBAL.Activate()))
            .Add(new ActionBuilder("Deplacement bidon").BuildActionBaseRoulante_DRIVE(1000, 100))
            .BuildActionEnSerie();
            return MOTHER_ACTION;
        }

        public Action GetActionTestTimerExtinctionGR()
        {
            Action MOTHER_ACTION = new ActionBuilder("Test timer switch global PR")
                //.Add(new ActionDelegate("Switch off", () => Robot.robot.SWITCH_GLOBAL.Activate()))
            .Add(new ActionBuilder("Activer moteurs").BuildActionLanceurBalle(0.6))
            .Add(new ActionBuilder("Deplacement bidon").BuildActionBaseRoulante_DRIVE(50, 100))
            .Add(new ActionWait("Wait a bit...", 2000))
            //
            .Add(new ActionBuilder("Deplacement bidon").BuildActionBaseRoulante_DRIVE(50, 100))
            .Add(new ActionWait("Wait a bit...", 2000))
            //.Add(new ActionDelegate("Switch off", () => Robot.robot.SWITCH_GLOBAL.Desactivate()))
            .Add(new ActionBuilder("Deplacement bidon").BuildActionBaseRoulante_DRIVE(50, 100))
            .BuildActionEnSerie();
            return MOTHER_ACTION;
        }
    }
}
