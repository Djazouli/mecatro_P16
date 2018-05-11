using System;
using System.Collections;
using Robot_P16.Robot;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{
    /// <summary>
    /// Classe utilitaire pour builder plus facilement une action en série
    /// </summary>

    public class ActionEnSerie : Action
    {

        private int indexOfCurrentAction = 0;
        public readonly Action[] listeActions;

        public ActionEnSerie( Action[] listeActions, String description)
            : base(description)
        {
            this.listeActions = listeActions;
        }

        public override void Execute()
        {
            Informations.printInformations(Priority.MEDIUM, "ActionEnSerie - execute called. Desc : " + this.description);
            //this.ResetStatus(); Not necessary for now
            this.indexOfCurrentAction = 0;

            if (listeActions.Length > this.indexOfCurrentAction)
            {
                this.listeActions[this.indexOfCurrentAction].StatusChangeEvent += this.Feedback;
                this.listeActions[this.indexOfCurrentAction].Execute();
            }
            else // No actions, success
            {
                this.Status = ActionStatus.SUCCESS;
            }
        }

        protected override bool PostStatusChangeCheck(ActionStatus oldpreviousStatus)
        {
            return true;
        }

        public override void ResetStatus()
        {
            foreach (Action a in listeActions)
                a.ResetStatus();
            base.ResetStatus();
        }


        /// <summary>
        /// Feedback est apppelé quand une action de la liste d'action change de statut.
        /// </summary>
        /// <param name="a">Action qui a changé de statut</param>
        public override void Feedback(Action a)
        {
            if (Robot.Robot.robot.isStopped) return;
            if (a.Status == ActionStatus.SUCCESS)
            {
                this.listeActions[this.indexOfCurrentAction].StatusChangeEvent -= this.Feedback;
                this.indexOfCurrentAction++;
                if (listeActions.Length > this.indexOfCurrentAction)
                {
                    Informations.printInformations(Priority.MEDIUM, "ActionEnSerie - executing next action. Desc of next : " + this.listeActions[this.indexOfCurrentAction].description);
                    this.listeActions[this.indexOfCurrentAction].StatusChangeEvent += this.Feedback;
                    this.listeActions[this.indexOfCurrentAction].Execute();
                }
                else // No actions, success
                {
                    Informations.printInformations(Priority.LOW, "ActionEnSerie - After last action feedback, status = success. Desc : "+this.description );
                    this.Status = ActionStatus.SUCCESS;
                }
            }
        }

        public override Action Clone()
        {
            Action[] newListeAction = new Action[this.listeActions.Length];
            for (int i = 0; i < this.listeActions.Length; i++)
            {
                newListeAction[i] = (Action)this.listeActions[i].Clone();
            }
            return new ActionEnSerie(newListeAction, description);

        }


    }
}
