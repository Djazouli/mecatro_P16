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
        public readonly int duration =  -1;

        /// <summary>
        /// Action concernant un servomoteur.
        /// Effectue lui-même le wait d'attente de fin de rotation
        /// </summary>
        public ActionServoAbsolue(String description, AX12 servomoteur, int angle)
            : base(description)
        {
            this.servomoteur = servomoteur;
            this.angle = angle;
        }
        public ActionServoAbsolue(String description, AX12 servomoteur, int angle, int duration)
            : base(description)
        {
            this.servomoteur = servomoteur;
            this.angle = angle;
            this.duration = duration;
        }

        public void Execute(int delay)
        {
            Informations.printInformations(Priority.MEDIUM, "Executing action servo absolue; angle : " + angle + "; description : " + description);

            /*if (delay >= 0)
                delay = servomoteur.SetAngle(angle, duration);
            else
                delay = servomoteur.SetAngle(angle);*/
            servomoteur.SetAngle(angle);
            this.Status = ActionStatus.SUCCESS;
        }

        public override void Execute()
        {
            Execute(duration);
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
