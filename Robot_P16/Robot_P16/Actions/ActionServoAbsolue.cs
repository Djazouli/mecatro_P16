using System;
using Microsoft.SPOT;
using Robot_P16.Robot.composants.Servomoteurs;
using System.Threading;
using Robot_P16.Robot;

namespace Robot_P16.Actions
{
    public class ActionServoAbsolue: Action
    {

        public readonly AX12 servomoteur;
        public readonly int angle;

        /// <summary>
        /// Action concernant un servomoteur.
        /// Effectue lui-m�me le wait d'attente de fin de rotation
        /// </summary>
        public ActionServoAbsolue(String description, AX12 servomoteur, int angle)
            : base(description)
        {
            this.servomoteur = servomoteur;
            this.angle = angle;
        }

        public override void Execute()
        {
            Informations.printInformations(Priority.MEDIUM, "Executing action servo absolue; angle : " + angle + "; description : " + description);
            int delay = servomoteur.SetAngle(angle);
            Thread.Sleep(delay+500);
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
