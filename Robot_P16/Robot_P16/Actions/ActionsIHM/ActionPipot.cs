using System;
using Microsoft.SPOT;

namespace Robot_P16.Actions.ActionsIHM
{
    class ActionPipot : Action
    {

        public int compteur;
        public readonly int compteur_initial;

        public ActionPipot(int compteur) 
            : base("Action pipot") {
                this.compteur = compteur;
                this.compteur_initial = compteur;
        }
 

        public override void Execute()
        {
            Debug.Print("ActionPipot executed ! Compteur : "+compteur);
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            if (Status == ActionStatus.UNDETERMINED)
                compteur = compteur_initial;
            return true;
        }

        public override void Feedback(Action a)
        {
            if (a.Status == ActionStatus.SUCCESS) { 
                compteur--;
                Debug.Print("Compteur : " + compteur);
                if (compteur <= 0) this.Status = ActionStatus.SUCCESS;
                else a.ResetStatus();
            }
        }
    }
}
