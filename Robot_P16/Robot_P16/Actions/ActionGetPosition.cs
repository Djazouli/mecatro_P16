using System;
using Microsoft.SPOT;

using Robot_P16.Robot;
namespace Robot_P16.Actions
{
    /// <summary>
    /// Permet d'envoyer la position du robot à interval régulier
    /// </summary>
    public class ActionGetPosition : Action
    {

        public ActionGetPosition(String description) : base(description) {}

        public override void Execute()
        {
            GestionnairePosition.sendPositionInformation();
            this.Status = ActionStatus.SUCCESS;
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            return true;
        }

        public override void Feedback(Action a)
        {
            throw new NotImplementedException();
        }
    }
}
