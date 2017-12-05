using System;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{
    /// <summary>
    /// Trois types de statuts pour une action :
    /// - succès
    /// - échec
    /// - indéterminée : on ne sait pas encore le résultat de l'action
    /// </summary>
    public enum ActionStatus {
        SUCCESS,
        FAILURE,
        UNDETERMINED
    }

    /// <summary>
    /// Sert à définir les différents types d'actions.
    /// Pratique pour réagir au feedback d'une action
    /// </summary>
    public enum ActionType
    {
        QUELCONQUE
    }


    /// <summary>
    /// Delegate de base pour écouter les événements liés aux actions
    /// </summary>
    /// <param name="action">Action à écouter</param>
    public delegate void ActionListenerDelegate(Action action);

    public abstract class Action
    {

        /// <summary>
        /// Statut de l'action, change au cours du temps. Peut être réinitialiser.
        /// </summary>
        private ActionStatus status = ActionStatus.UNDETERMINED;
        public ActionStatus Status
        {
            get { return status; }
            protected set
            {
                ActionStatus oldStatus = status;
                status = value;
                OnStatusChange(oldStatus);
            }
        }
        private ActionType type = ActionType.QUELCONQUE;
        public ActionType Type
        {
            get { return type; }
            set
            {
                type = value;
            }
        }

        public readonly String description;

        public event ActionListenerDelegate StatusChangeEvent;

        public Action(String description) { this.description = description; }


        /// <summary>
        /// Lance l'exécution de l'action, spécifiée par les classes filles.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Code à lancer après changement d'état.
        /// Si renvoit false, annule le triggering de l'événement StatusChangeEvent
        /// </summary>
        protected abstract bool PostStatusChangeCheck(ActionStatus previousStatus);


        /// <summary>
        /// Feedback réagit lorsqu'une action renvoit son feedback
        /// </summary>
        /// <param name="a">L'action qui renvoie son feedback</param>
        public abstract void Feedback(Action a);


        protected void OnStatusChange(ActionStatus previousStatus) // Rajouter un check du statut précédent ?
        {
            if(PostStatusChangeCheck(previousStatus))
                if(StatusChangeEvent != null)
                    StatusChangeEvent(this); 
            
        }

        /// <summary>
        /// Permet de reset le statut de l'action depuis l'extérieur
        /// ResetStatus est public, contrairement au setter de status, protected
        /// </summary>
        public virtual void ResetStatus()
        {
            Status = ActionStatus.UNDETERMINED;
        }

        protected static bool TestActionStatus(Action[] actions, ActionStatus status) {
            if (actions == null || actions.Length == 0) return false;
             foreach(Action a in actions) {
                 if (a.Status != status)
                     return false;
             }
             return true;
        }

        public abstract Action Clone();

    }
}
