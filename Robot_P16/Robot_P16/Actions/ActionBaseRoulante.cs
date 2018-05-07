using System;
using Microsoft.SPOT;
using Robot_P16.Map;
using Robot_P16.Robot;
using Robot_P16.Robot.composants.BaseRoulante;
namespace Robot_P16.Actions
{
    public class ActionBaseRoulante : Action
    {

        private Mouvement mouvement;

        public ActionBaseRoulante(String description, Mouvement mouvement)
            : base(description)
        {
            this.mouvement = mouvement;
        }

        public override void Execute()
        {
            Informations.printInformations(Priority.HIGH, "ActionBaseRoulante - execute called. Desc : "+this.description);

            Robot.Robot.robot.BASE_ROULANTE.InstructionCompletedEvent += () =>
            {

                Informations.printInformations(Priority.HIGH, "ActionBaseRoulante : completed, status = success");
                this.Status = ActionStatus.SUCCESS;
            };
            this.mouvement.Start();
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
