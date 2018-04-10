using System;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{

    public delegate void VoidFunc();

    public class ActionDelegate : Action
    {

        public readonly VoidFunc method;

        public ActionDelegate(String description, VoidFunc method)
            : base(description)
        {
            this.method = method;
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionDelegate : creation nouvelle actiondelegate avec description" + description);
        }

        public override void Execute()
        {
            this.method();
            this.Status = ActionStatus.SUCCESS;
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.MEDIUM, "Actions.Action.ActionDelegate.Execute : execution de l actiondelegate");
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.Action.ActionDelegate.PostStatusChangeCheck : validation du changement de statut");
            return true;
        }

        public override void Feedback(Action a)
        {
            throw new NotImplementedException();
        }
    }
}
