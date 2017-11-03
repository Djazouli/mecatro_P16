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
            protected set { 
                status = value; 
                OnStatusChange();
            }
        }


        public event ActionListenerDelegate SuccessEvent;
        public event ActionListenerDelegate FailureEvent;
        public event ActionListenerDelegate ResetEvent;

        /// <summary>
        /// Action suivante à exécuter après succès.
        /// </summary>
        public Action ActionSuivante { get; protected set; }


        /// <summary>
        /// Lance l'exécution de l'action, spécifiée par les classes filles.
        /// </summary>
        public abstract void execute();

        /// <summary>
        /// Code à lancer après un succès avant de fire l'événement de succès
        /// </summary>
        protected abstract void postSuccessCode();
        /// <summary>
        /// Code à lancer après un succès avant de fire l'événement d'échec
        /// </summary>
        protected abstract void postFailureCode();
        /// <summary>
        /// Code à lancer après un reset avant de fire l'événement d'échec
        /// </summary>
        protected abstract void postResetCode();

        protected void OnStatusChange() // Rajouter un check du statut précédent ?
        {
            switch (Status)
            {
                case ActionStatus.SUCCESS:
                    postSuccessCode();
                    if (SuccessEvent != null)
                        SuccessEvent(this);
                    if (ActionSuivante != null)
                        ActionSuivante.execute();
                    break;

                case ActionStatus.FAILURE:
                    postFailureCode();
                    if (FailureEvent != null)
                        FailureEvent(this);
                    break;

                case ActionStatus.UNDETERMINED:
                    postResetCode();
                    if (ResetEvent != null)
                        ResetEvent(this);
                    break;
            }
        }


        protected void ResetStatus()
        {
            Status = ActionStatus.UNDETERMINED;
            postResetCode();
            OnStatusChange();
        }

    }
}
