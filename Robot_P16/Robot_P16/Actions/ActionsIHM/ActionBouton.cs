using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace Robot_P16.Actions.ActionsIHM
{
    public class ActionBouton : Action
    {
        public readonly Button bouton;
        public ActionBouton(Action actionSuivante, Button bouton)
            : base(actionSuivante)
        {
            this.bouton = bouton;
        }



        public override void execute()
        {
            bouton.ButtonPressed += (a,b) => Status = ActionStatus.SUCCESS;
            bouton.ButtonReleased += (a, b) => Status = ActionStatus.UNDETERMINED;
        }

        protected override void postSuccessCode()
        {
            Debug.Print("ActionBouton successful !");
            bouton.TurnLedOn();
        }

        protected override void postFailureCode()
        {
            throw new NotImplementedException();
        }

        protected override void postResetCode()
        {
            Debug.Print("ActionBouton reset !");
            bouton.TurnLedOff();
        }
    }
}
