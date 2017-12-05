using System;
using System.Collections;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{
    /// <summary>
    /// Classe utilitaire pour builder plus facilement une action en série
    /// </summary>
    public class ActionEnSerieBuilder
    {
        public Action actionSuivante;
        public ArrayList liste = new ArrayList();
        public String description;

        public ActionEnSerieBuilder(String description)
        {
            this.description = description;
        }
        public ActionEnSerieBuilder() : this(null) { }

        public ActionEnSerieBuilder Add(Action a)
        {
            liste.Add(a);
            return this;
        }

        public ActionEnSerie Build()
        {
            Action[] listeActions = new Action[liste.Count];
            int i = 0;
            foreach (Object o in liste)
                listeActions[i++] = (Action)o;
            return new ActionEnSerie(listeActions, description);
        }
    }

    public class ActionEnSerie : Action
    {

        public readonly Action[] listeActions;

        public ActionEnSerie( Action[] listeActions, String description)
            : base(description)
        {
            this.listeActions = listeActions;
        }

        private int IndexOfFirstUnsucessfulAction()
        {
            int i = 0;
            while (i < listeActions.Length && listeActions[i].Status == ActionStatus.SUCCESS) i++;
            return i;
        }

        public Action GetFirstUnsucessfulAction()
        {
            int index = IndexOfFirstUnsucessfulAction();
            if (index < listeActions.Length)
                return listeActions[index];
            return null;
        }

        private int IndexOfAction(Action a)
        {
            for (int i = 0; i < listeActions.Length; i++)
            {
                if (listeActions[i].Equals(a))
                    return i;
            }
            return -1;
        }

        private Action GetNextAction(Action a)
        {
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
                        a.StatusChangeEvent -= this.Feedback; // On arrête d'écoûter l'action

                        Action actionSuivante = this.GetNextAction(a);
                        Debug.Print("Next action...");
                        if (actionSuivante != null)
                        {
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
                        this.Status = ActionStatus.FAILURE;
                        break;

                    // Attention, ne pas changer le Status à UNDETERMINED en écoutant un changement UNDERTERMINED : boucle infinie
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
