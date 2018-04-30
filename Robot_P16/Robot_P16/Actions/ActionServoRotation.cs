using System;
using Microsoft.SPOT;
using Robot_P16.Robot.composants.Servomoteurs;
using System.Threading;

namespace Robot_P16.Actions
{
    public class ActionServoRotation: Action
    {

        public readonly AX12 servomoteur;
        public readonly int duration;
        public readonly speed direction;

        /// <summary>
        /// Action concernant un servomoteur.
        /// Effectue lui-même le wait d'attente de fin de rotation
        /// </summary>
        public ActionServoRotation(String description, AX12 servomoteur, speed direction, int duration)
            : base(description)
        {
            this.servomoteur = servomoteur;
            this.duration = duration;
            this.direction = direction;
        }

        public override void Execute()
        {
            servomoteur.Rotate(direction, duration);
            //Thread.Sleep(delay);
            this.Status = ActionStatus.SUCCESS;
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            return true;
        }

        public override void Feedback(Action a)
        {

        }
    }
}
