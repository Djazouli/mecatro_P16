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
            Informations.printInformations(Priority.MEDIUM, "JackAction : Executing jack action : adding listener");
            Robot.Robot.robot.JACK.JackChangeEvent += this.JackChange;
            Robot.Robot.robot.JACK.StartTimer();
            DisplayOnIHM();
        }

        private void JackChange(bool newStatus)
        {
            Informations.printInformations(Priority.MEDIUM, "JackAction : Jack changed, status = success & stop listening to Jack");
            //Robot.Robot.robot.JACK.JackChangeEvent -= this.JackChange;
            //Robot.Robot.robot.JACK.StopTimer();
            //this.Status = ActionStatus.SUCCESS;
            DisplayOnIHM();
        }

        private void DisplayOnIHM()
        {
            //Robot.Robot.robot.IHM.AfficherInformation("Status jack :\n" + Robot.Robot.robot.JACK.IsJackOn(),false);
        }


        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        { return true; }

        public override void Feedback(Action a) { }
    }
}
