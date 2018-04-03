using System;
using Microsoft.SPOT;
using Robot_P16.Map;
using Robot_P16.Robot;
namespace Robot_P16.Actions
{
    public class ActionBaseRoulante : Action
    {

        public readonly PointOriente destination;

        public ActionBaseRoulante(String description, PointOriente pt)
            : base(description)
        {            
            this.destination = pt;
        }

        public override void Execute()
        {
            Robot.Robot.robot.BASE_ROULANTE.GoToOrientedPoint(this.destination);
            this.Status = ActionStatus.SUCCESS;
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
