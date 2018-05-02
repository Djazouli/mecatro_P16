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
            this.stop_count = 0;
            this.paused = false;
            Robot.Robot.robot.OBSTACLE_MANAGER.ObstacleChangeEvent += this.ObstacleListener;

            Robot.Robot.robot.BASE_ROULANTE.GoToOrientedPoint(this.destination);

            Robot.Robot.robot.BASE_ROULANTE.MovingStatusChangeEvent += this.BaseRoulanteListener;
        }

        public void BaseRoulanteListener(bool isMoving)
        {
            if (!isMoving && !paused)
            {
                this.Status = ActionStatus.SUCCESS; 
                Robot.Robot.robot.BASE_ROULANTE.MovingStatusChangeEvent -= this.BaseRoulanteListener;

            }
        }

        public void ObstacleListener(OBSTACLE_DIRECTION direction, bool isThereAnObstacle)
        {
            if (!Map.MapInformation.isObstacleDetecteurOn()) return;

            if (direction == Robot.Robot.robot.BASE_ROULANTE.GetDirection())
            {
                if (isThereAnObstacle)
                {
                    this.stop_count++;
                    if (this.stop_count <= MAX_STOP_COUNT)
                    {
                        this.paused = true;
                        Robot.Robot.robot.BASE_ROULANTE.Stop();
                    }
                }
                else
                {
                    this.paused = false;
                    Robot.Robot.robot.BASE_ROULANTE.GoToOrientedPoint(this.destination);
                }
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
