using System;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{
    public class ActionRepeated : Action
    {
        public readonly Action actionToBeRepeated;
        private int compteur;

        /// <summary>
        /// Action répétée un certain nombre de fois. Compteur = -1 pour répétitions infinies
        /// </summary>
        /// <param name="description"></param>
        /// <param name="actionToBeRepeated"></param>
        /// <param name="compteur">Nombre de répétitions. -1 pour infini</param>
        public ActionRepeated(String description, Action actionToBeRepeated, int compteur)
            : base(description)
        {
            this.actionToBeRepeated = actionToBeRepeated;
            this.compteur = compteur;
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionRepeated : creation ActionRepeated "+ description + " Nombre de repetitions: " + compteur.ToString());
        }

        public override void Execute()
        {
            if (compteur == 0)
            {
                Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionRepeated.Execute : compteur egal à 0, pas d’execution d’action");
                return;
            }
            if(compteur>0) compteur--;
            actionToBeRepeated.Execute();
            actionToBeRepeated.StatusChangeEvent += this.Feedback;
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.MEDIUM, "Actions.ActionRepeated.Execute : execution de l’action, diminution du compteur a "+ compteur.ToString());
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            return true;
        }

        public override void Feedback(Action a)
        {
            if (a.Status == ActionStatus.SUCCESS)
            {
                a.ResetStatus();

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
