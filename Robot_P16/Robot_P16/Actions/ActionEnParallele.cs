using System;
using System.Threading;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{
    class ActionEnParallele : Action
    {
        public readonly Action[] listeActions;
        protected Thread[] threads;


        public ActionEnParallele(Action[] listeActions, String description)
            : base(description)
        {
            this.listeActions = listeActions;
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


        public override void ResetStatus()
        {
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
            Action[] newListeAction = new Action[this.listeActions.Length];
            for (int i = 0; i < this.listeActions.Length; i++)
            {
                newListeAction[i] = (Action)this.listeActions[i].Clone();
            }
            return new ActionEnParallele(newListeAction, description);

        }
    }
}
