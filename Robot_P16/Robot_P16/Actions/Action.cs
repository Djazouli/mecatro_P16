using System;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{
    /// <summary>
    /// Trois types de statuts pour une action :
    /// - succ�s
    /// - �chec
    /// - ind�termin�e : on ne sait pas encore le r�sultat de l'action
    /// </summary>
    public enum ActionStatus {
        SUCCESS,
        FAILURE,
        UNDETERMINED
    }


    /// <summary>
    /// Delegate de base pour �couter les �v�nements li�s aux actions
    /// </summary>
    /// <param name="action">Action � �couter</param>
    public delegate void ActionListenerDelegate(Action action);

    public abstract class Action
    {

        /// <summary>
        /// Statut de l'action, change au cours du temps. Peut �tre r�initialiser.
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
        /// Action suivante � ex�cuter apr�s succ�s.
        /// </summary>
        public Action ActionSuivante { get; protected set; }


        /// <summary>
        /// Lance l'ex�cution de l'action, sp�cifi�e par les classes filles.
        /// </summary>
        public abstract void execute();

        /// <summary>
        /// Code � lancer apr�s un succ�s avant de fire l'�v�nement de succ�s
        /// </summary>
        protected abstract void postSuccessCode();
        /// <summary>
        /// Code � lancer apr�s un succ�s avant de fire l'�v�nement d'�chec
        /// </summary>
        protected abstract void postFailureCode();
        /// <summary>
        /// Code � lancer apr�s un reset avant de fire l'�v�nement d'�chec
        /// </summary>
        protected abstract void postResetCode();

        protected void OnStatusChange() // Rajouter un check du statut pr�c�dent ?
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
