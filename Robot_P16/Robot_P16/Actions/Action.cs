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
            protected set
            {
                ActionStatus oldStatus = status;
                status = value;
                OnStatusChange(oldStatus);
            }
        }

        public readonly String description;

        public event ActionListenerDelegate StatusChangeEvent;

        /// <summary>
        /// Action suivante � ex�cuter apr�s succ�s.
        /// </summary>
        public Action ActionSuivante { get; protected set; }


        public Action(String description) { this.description = description; }


        /// <summary>
        /// Lance l'ex�cution de l'action, sp�cifi�e par les classes filles.
        /// </summary>
        public abstract void execute();

        /// <summary>
        /// Code � lancer apr�s changement d'�tat.
        /// Si renvoit false, annule le triggering de l'�v�nement StatusChangeEvent
        /// </summary>
        protected abstract bool postStatusChangeCheck(ActionStatus previousStatus);

        protected void OnStatusChange(ActionStatus previousStatus) // Rajouter un check du statut pr�c�dent ?
        {
            if(postStatusChangeCheck(previousStatus))
                if(StatusChangeEvent != null)
                    StatusChangeEvent(this);
            
        }

        /// <summary>
        /// Permet de reset le statut de l'action depuis l'ext�rieur
        /// ResetStatus est public, contrairement au setter de status, protected
        /// </summary>
        public void ResetStatus()
        {
            Status = ActionStatus.UNDETERMINED;
        }

    }
}
