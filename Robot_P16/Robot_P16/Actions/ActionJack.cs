using System;
using Microsoft.SPOT;
using Robot_P16.Robot;

namespace Robot_P16.Actions
{
    public class ActionJack : Action
    {
        public ActionJack()
            : base("Action Jack")
        {}


        public override void Execute()
        {
            Informations.printInformations(Priority.HIGH, "JackAction : Executing jack action : adding listener");
            Robot.Robot.robot.JACK.JackChangeEvent += this.JackChange;
        }

        private void JackChange(bool newStatus)
        {
            Informations.printInformations(Priority.HIGH, "JackAction : Jack changed, status = success & stop listening to Jack, new Jack status : "+newStatus);
            //Robot.Robot.robot.JACK.JackChangeEvent -= this.JackChange;
            //Robot.Robot.robot.JACK.StopTimer();
            Robot.Robot.robot.JACK.doneListeningToJack = true;
            Robot.Robot.robot.StartCountdown();
            this.Status = ActionStatus.SUCCESS;
            //DisplayOnIHM();
        }


        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        { return true; }

        public override void Feedback(Action a) { }
    }
}
