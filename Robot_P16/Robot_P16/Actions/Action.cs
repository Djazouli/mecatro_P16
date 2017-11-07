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
        /// Action suivante à exécuter après succès.
        /// </summary>
        public Action ActionSuivante { get; protected set; }


        public Action(String description) { this.description = description; }


        /// <summary>
        /// Lance l'exécution de l'action, spécifiée par les classes filles.
        /// </summary>
        public abstract void execute();

        /// <summary>
        /// Code à lancer après changement d'état.
        /// Si renvoit false, annule le triggering de l'événement StatusChangeEvent
        /// </summary>
        protected abstract bool postStatusChangeCheck(ActionStatus previousStatus);

        protected void OnStatusChange(ActionStatus previousStatus) // Rajouter un check du statut précédent ?
        {
            if(postStatusChangeCheck(previousStatus))
                if(StatusChangeEvent != null)
                    StatusChangeEvent(this);
            
        }

        /// <summary>
        /// Permet de reset le statut de l'action depuis l'extérieur
        /// ResetStatus est public, contrairement au setter de status, protected
        /// </summary>
        public void ResetStatus()
        {
            Status = ActionStatus.UNDETERMINED;
        }

    }
}
