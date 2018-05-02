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

        private const int  MAX_STOP_COUNT = 4;

        public ActionBaseRoulante(String description, PointOriente pt)
            : base(description)
        {            
            this.destination = pt;
        }

        public override void Execute()
        {
            this.stop_count = 0;
            Robot.Robot.robot.OBSTACLE_MANAGER.ObstacleChangeEvent += this.ObstacleListener;

            Robot.Robot.robot.BASE_ROULANTE.GoToOrientedPoint(this.destination);
            this.Status = ActionStatus.SUCCESS;
        }

        public void ObstacleListener(OBSTACLE_DIRECTION direction, bool isThereAnObstacle)
        {
            if (direction == Robot.Robot.robot.BASE_ROULANTE.GetDirection())
            {
                if (isThereAnObstacle)
                {
                    this.stop_count++;
                    if (this.stop_count <= MAX_STOP_COUNT)
                    {
                        Robot.Robot.robot.BASE_ROULANTE.Stop();
                    }
                }
                else
                {
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
