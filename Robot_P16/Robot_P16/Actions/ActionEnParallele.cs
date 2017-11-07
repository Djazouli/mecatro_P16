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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispatch le feedback aux différentes actions en cours
        /// </summary>
        /// <param name="a"></param>
        public override void Feedback(Action a)
        {
            int index = IndexOfAction(a);
            if (index > 0) // Si l'action fait bien parti de notre liste d'action
            {
                for (int i = 0; i < listeActions.Length; i++)
                {
                    if (i != index)
                        listeActions[i].Feedback(a);
                }
            }
        }
    }
}
