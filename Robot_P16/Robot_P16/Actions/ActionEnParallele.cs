using System;
using System.Threading;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{
    public class ActionEnParallele : Action
    {
        public readonly Action[] listeActions;
        protected Thread[] threads;


        public ActionEnParallele(Action[] listeActions, String description)
            : base(description)
        {
            this.listeActions = listeActions;
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnParallele : creation nouvelle action en parallele avec description " + description + " et a partir d une liste d actions");
        }

        private int IndexOfAction(Action a)
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnParallele.IndexOfAction : recherche de l index de l action " + a.description + "parmi la liste d actions en parallele");
            for (int i = 0; i < listeActions.Length; i++)
            {
                if (listeActions[i].Equals(a))
                    return i;
            }
            return -1;
        }


        public override void ResetStatus()
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnParallele.ResetStatus : reinitialisation du satut de toutes les actions de la liste d actions en parallele");
            for (int i = 0; i < threads.Length; i++)
            {
                if (threads[i] != null) threads[i].Abort();
            }
            threads = null;
            foreach (Action a in listeActions)
                a.ResetStatus();
            base.ResetStatus();
        }


        public override void Execute()
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnParallele.Execute : execution des actions en parallele");
            threads = new Thread[listeActions.Length];
            for (int i = 0; i < listeActions.Length; i++)
            {
                listeActions[i].StatusChangeEvent -= this.Feedback; // Au cas où on utilise Execute() plusieurs fois
                listeActions[i].StatusChangeEvent += this.Feedback;
                threads[i] = new Thread(() => listeActions[i].Execute());
                threads[i].Start();
            }
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            return true;
        }

        /// <summary>
        /// Dispatch le feedback aux différentes actions en cours
        /// </summary>
        /// <param name="a"></param>
        public override void Feedback(Action a)
        {
            //Debug.Print("Feedback sur parallele 0");    
            if(this.Status != ActionStatus.SUCCESS) { // On bloque les feedbacks si l'action est réalisée avec succès
                Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnParallele : apport du feedback d'une action a toutes les autres");
                //Debug.Print("Feedback sur parallele 1 ");    
                int index = IndexOfAction(a);
                if (index >= 0) // Si l'action fait bien parti de notre liste d'action
                {
                    //Debug.Print("Feedback sur parallele 2 ");    
                    for (int i = 0; i < listeActions.Length; i++)
                    {
                        //Debug.Print("Feedback sur parallele 3 ");   
                        if (i != index)
                            listeActions[i].Feedback(a);
                    }
                }
                if (a.Status == ActionStatus.SUCCESS && this.Status != ActionStatus.SUCCESS) // Redondant mais utile en cas de suppression de la condition englobante
                {
                    Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionDelegate : re);
                    if (TestActionStatus(listeActions, ActionStatus.SUCCESS))
                    {
                        Status = ActionStatus.SUCCESS;
                        //Debug.Print("success de l'action en parallèle !");
                    }
                }
            }
        }

        public override Action Clone()
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionEnParallele : clonage de la liste d actions en parallele");
            Action[] newListeAction = new Action[this.listeActions.Length];
            for (int i = 0; i < this.listeActions.Length; i++)
            {
                newListeAction[i] = (Action)this.listeActions[i].Clone();
            }
            return new ActionEnParallele(newListeAction, description);

        }
    }
}
