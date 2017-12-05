using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace Robot_P16.Actions.ActionsIHM
{
    public class ActionBouton : Action
    {
        public readonly Button bouton;
        public ActionBouton(Button bouton)
            : base("Action de bouton")
        {
            this.bouton = bouton;
        }



        public override void Execute()
        {
            bouton.ButtonPressed += (a,b) => Status = ActionStatus.SUCCESS;
            //bouton.ButtonReleased += (a, b) => Status = ActionStatus.UNDETERMINED;
        }


        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            switch (Status)
            {
                case ActionStatus.SUCCESS:
                    Debug.Print("ActionBouton successful !");
                    bouton.TurnLedOn();
                    break;
                case ActionStatus.UNDETERMINED:
                     Debug.Print("ActionBouton reset !");
                    bouton.TurnLedOff();
                    break;
                
            }
            return true;
        }

        public override void Feedback(Action a) {}
    }
}
