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
            Debug.Print("ActionBouton executed");
            bouton.ButtonPressed += (a,b) => Status = ActionStatus.SUCCESS;
            //System.Threading.Thread.Sleep(-1);
            //bouton.ButtonReleased += (a, b) => Status = ActionStatus.UNDETERMINED;
        }


        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            switch (Status)
            {
                case ActionStatus.SUCCESS:
                    if( previousStatus == ActionStatus.SUCCESS) {
                        Debug.Print("ActionBouton already successful.");
                        return false;
                    } else {
                        Debug.Print("ActionBouton successful !");
                    }
                    break;
                case ActionStatus.UNDETERMINED:
                     Debug.Print("ActionBouton reset !");
                    break;
                
            }
            return true;
        }

        public override void Feedback(Action a) {}
    }
}
