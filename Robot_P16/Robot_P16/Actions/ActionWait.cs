using System;
using System.Threading;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{
    public class ActionWait : Action
    {

        public readonly int duration;

        public ActionWait(String description, int duration)
            : base(description)
        {
            this.duration = duration;
        }

        public override void Execute()
        {
            Thread.Sleep(duration);
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
