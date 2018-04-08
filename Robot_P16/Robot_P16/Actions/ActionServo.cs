using System;
using Microsoft.SPOT;
using Robot_P16.Robot.composants.Servomoteurs;
using System.Threading;

namespace Robot_P16.Actions
{
    public class ActionServo: Action
    {

        public readonly AX12 servomoteur;
        public readonly float angle;
        public readonly ServoCommandTypes commandType;

        /// <summary>
        /// Action concernant un servomoteur.
        /// Effectue lui-même le wait d'attente de fin de rotation
        /// </summary>
        public ActionServo(String description, AX12 servomoteur, ServoCommandTypes commandType, float angle)
            : base(description)
        {
            this.servomoteur = servomoteur;
            this.angle = angle;
            this.commandType = commandType;
        }

        public override void Execute()
        {
            int delay = servomoteur.ExecuteCommand(commandType, angle);
            Thread.Sleep(delay);
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
