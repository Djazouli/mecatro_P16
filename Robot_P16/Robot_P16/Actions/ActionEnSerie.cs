using System;
using System.Collections;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{
    /// <summary>
    /// Classe utilitaire pour builder plus facilement une action en série
    /// </summary>
    

    public class ActionEnSerie : Action
    {

        public readonly Action[] listeActions;

        public ActionEnSerie( Action[] listeActions, String description)
            : base(description)
        {
            this.listeActions = listeActions;
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnSerie : creation nouvelle action en serie avec description " + description + " et a partir d une liste d actions");
        }

        private int IndexOfFirstUnsucessfulAction()
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnSerie.IndexOfFirstUnsucessfulAction : recherche de l index de la premiere action non successfull");
            int i = 0;
            while (i < listeActions.Length && listeActions[i].Status == ActionStatus.SUCCESS) i++;
            return i;
        }

        public Action GetFirstUnsucessfulAction()
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnSerie.GetFirstUnsuccessfulAction : recuperation de la premiere action non successfull");
            int index = IndexOfFirstUnsucessfulAction();
            if (index < listeActions.Length)
                return listeActions[index];
            return null;
        }

        private int IndexOfAction(Action a)
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnSerie.IndexOfAction : recherche de l index de l action " + a.description + "parmi la liste d actions en serie");
            for (int i = 0; i < listeActions.Length; i++)
            {
                if (listeActions[i].Equals(a))
                    return i;
            }
            return -1;
        }

        private Action GetNextAction(Action a)
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnSerie.GetNextAction : recuperation de l action suivant l action " + a.description );
            int index = IndexOfAction(a);
            if (index < 0)
                return null;
            index++;
            if (index >= listeActions.Length)
                return null;
            return listeActions[index];
        }

        public override void Execute()
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnSerie.Execute : execute les actions en serie");
            GetFirstUnsucessfulAction().StatusChangeEvent += this.Feedback;
            GetFirstUnsucessfulAction().Execute();
        }



        protected override bool PostStatusChangeCheck(ActionStatus oldpreviousStatus)
        {
            /*switch (this.Status)
            {
                case ActionStatus.UNDETERMINED:
                    foreach (Action a in listeActions)
                        a.ResetStatus();
                    break;

            }*/
            return true;
        }

        public override void ResetStatus()
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnSerie.ResetStatus : reinitialisation du statut de toutes les actions en serie");
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
            int index = IndexOfAction(a);
            if (index >= 0) // L'action est bien dans la liste d'actions
            {
                switch (a.Status) {
                    case ActionStatus.SUCCESS:
                        Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnSerie.Feedback : on arrete d ecouter l action qui a reussi");
                        a.StatusChangeEvent -= this.Feedback; // On arrête d'écoûter l'action

                        Action actionSuivante = this.GetNextAction(a);
                        Debug.Print("Next action...");
                        if (actionSuivante != null)
                        {
                            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnSerie.Feedback : on ecoute l action suivante et on l execute");
                            Debug.Print("executing...");
                            actionSuivante.StatusChangeEvent += this.Feedback;
                            actionSuivante.Execute();
                        }
                        else
                        {
                            this.Status = ActionStatus.SUCCESS;
                        }
                        

                        break;

                    case ActionStatus.FAILURE:
                        Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnSerie.Feedback : echec de la realisation d une action");
                        this.Status = ActionStatus.FAILURE;
                        break;

                    // Attention, ne pas changer le Status à UNDETERMINED en écoutant un changement UNDERTERMINED : boucle infinie
                }
            }
        }

        public override Action Clone()
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnSerie.Clone : clonage des actions en serie");
            Action[] newListeAction = new Action[this.listeActions.Length];
            for (int i = 0; i < this.listeActions.Length; i++)
            {
                newListeAction[i] = (Action)this.listeActions[i].Clone();
            }
            return new ActionEnSerie(newListeAction, description);

        }


    }
}
