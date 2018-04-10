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
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionBaseRoulante : creation nouvelle action avec description" + description + " et un point objectif");
        }

        public override void Execute()
        {
            Robot.Robot.robot.BASE_ROULANTE.GoToOrientedPoint(this.destination);
            this.Status = ActionStatus.SUCCESS;
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.MEDIUM, "Actions.Action.ActionBaseRoulante.Execute : execution de l action aller vers le point oriente");
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.Action.ActionBasePoulante.PostStatusChangeCheck : validation du changement de statut");
            return true;
        }

        public override void Feedback(Action a)
        {
            throw new NotImplementedException();
        }
    }
}
