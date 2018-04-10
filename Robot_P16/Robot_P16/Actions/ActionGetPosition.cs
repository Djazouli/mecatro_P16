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

        public ActionGetPosition(String description) : base(description) { Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionGetPosition : cree une nouvelle action de recuperation de la position"); }

        public override void Execute()
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionGetPosition.Execute : execute l action de recuperation d information");
            GestionnairePosition.sendPositionInformation();
            this.Status = ActionStatus.SUCCESS;
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            return true;
        }

        public override void Feedback(Action a)
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionGetPosition.Feedback : feedback de l'action "+ a.description);
            throw new NotImplementedException();
        }
    }
}
