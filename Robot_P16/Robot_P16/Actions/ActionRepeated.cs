using System;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{
    public class ActionRepeated : Action
    {
        public readonly Action actionToBeRepeated;
        private int compteur;
        private readonly int base_compteur;
        /// <summary>
        /// Action r�p�t�e un certain nombre de fois. Compteur = -1 pour r�p�titions infinies
        /// </summary>
        /// <param name="description"></param>
        /// <param name="actionToBeRepeated"></param>
        /// <param name="compteur">Nombre de r�p�titions. -1 pour infini</param>
        public ActionRepeated(String description, Action actionToBeRepeated, int compteur)
            : base(description)
        {
            this.actionToBeRepeated = actionToBeRepeated;
            this.compteur = compteur;
            this.base_compteur = compteur;
        }

        public override void Execute()
        {
            this.compteur = this.base_compteur;
            if(compteur == 0) return;
            if (compteur > 0) compteur--;
            actionToBeRepeated.StatusChangeEvent += this.Feedback;
            actionToBeRepeated.Execute();
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            return true;
        }

        public override void Feedback(Action a)
        {
            if (a.Status == ActionStatus.SUCCESS)
            {
                //a.ResetStatus();

                if(compteur < 0) // Boucle infinie
                    a.Execute();
                else if (compteur > 0)
                {
                    compteur--;
                    a.Execute();
                }
                else
                {
                    actionToBeRepeated.StatusChangeEvent -= this.Feedback;
                    this.Status = ActionStatus.SUCCESS;
                }
                
            }
        }
    }
}
