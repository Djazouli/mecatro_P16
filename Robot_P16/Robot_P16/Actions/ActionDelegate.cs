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
        }

        public override void Execute()
        {
            this.method();
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
