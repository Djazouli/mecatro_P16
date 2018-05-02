using System;
using Microsoft.SPOT;
using Robot_P16.Map;
using Robot_P16.Robot;
namespace Robot_P16.Actions
{
    public class ActionBaseRoulante : Action
    {

        public readonly PointOriente destination;
        private int stop_count = 0;
        private bool paused = false;

        private const int  MAX_STOP_COUNT = 10000;

        public ActionBaseRoulante(String description, PointOriente pt)
            : base(description)
        {            
            this.destination = pt;
        }

        public override void Execute()
        {
            Informations.printInformations(Priority.MEDIUM, "ActionBaseRoulante - execute called. Desc : "+this.description);
            this.stop_count = 0;
            this.paused = false;
            Robot.Robot.robot.OBSTACLE_MANAGER.ObstacleChangeEvent += this.ObstacleListener;

            Robot.Robot.robot.BASE_ROULANTE.MovingStatusChangeEvent += this.BaseRoulanteListener;
            Robot.Robot.robot.BASE_ROULANTE.GoToOrientedPoint(this.destination);

        }

        public void BaseRoulanteListener(bool isMoving)
        {
            if (!isMoving && !paused)
            {
                Informations.printInformations(Priority.MEDIUM, "ActionBaseRoulante - BaseRoulanteListener - !moving && !paused : success. Desc : " + this.description);
                this.Status = ActionStatus.SUCCESS; 
                Robot.Robot.robot.BASE_ROULANTE.MovingStatusChangeEvent -= this.BaseRoulanteListener;
            }
            else
            {
                Informations.printInformations(Priority.MEDIUM, "ActionBaseRoulante - BaseRoulanteListener - !(!moving && !paused). No change to status. Desc : " + this.description);
            }
        }

        public void ObstacleListener(OBSTACLE_DIRECTION direction, bool isThereAnObstacle)
        {
            if (!Map.MapInformation.isObstacleDetecteurOn())
            {
                Informations.printInformations(Priority.MEDIUM, "ActionBaseRoulante - ObstacleListener - !Map.MapInformation.isObstacleDetecteurOn(), no change on trajectory. Desc : " + this.description);
                return;
            }

            if (direction != Robot.Robot.robot.BASE_ROULANTE.GetDirection())
            {
                if (isThereAnObstacle)
                {
                    this.stop_count++;
                    if (this.stop_count <= MAX_STOP_COUNT)
                    {
                        Informations.printInformations(Priority.MEDIUM, "ActionBaseRoulante - ObstacleListener - obstacle detected : pausing movement. Desc : " + this.description);
                        this.paused = true;
                        Robot.Robot.robot.BASE_ROULANTE.Stop();
                    }
                    else
                    {
                        Informations.printInformations(Priority.MEDIUM, "ActionBaseRoulante - ObstacleListener - obstacle detected BUT MAX STOP COUNT REACHED : NOT STOPPING ASSHOLE. Desc : " + this.description);
                    }
                }
                else
                {
                    Informations.printInformations(Priority.MEDIUM, "ActionBaseRoulante - ObstacleListener - obstacle removed : resuming movement. Desc : " + this.description);
                    this.paused = false;
                    Robot.Robot.robot.BASE_ROULANTE.GoToOrientedPoint(this.destination);
                }
            }
            else
            {
                Informations.printInformations(Priority.MEDIUM, "ActionBaseRoulante - ObstacleListener - obstacle in other direction : ignored. Desc : " + this.description);
            }
        }


        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            return true;
        }

        public override void Feedback(Action a)
        {
            throw new NotImplementedException();
        }
    }
}
