using System;
using Microsoft.SPOT;

using Robot_P16.Robot.composants.Servomoteurs;
using Robot_P16.Robot.composants;
using System.Threading;

namespace Robot_P16.Actions
{
    public enum VENTOUZES
    {
        VENTOUZE_GAUCHE,
        VENTOUZE_DROITE
    }

    public class ActionVentouze: Action
    {

        public readonly VENTOUZES ventouze;
        public bool activate;

        public ActionVentouze(String description, VENTOUZES ventouze, bool activate)
            : base(description)
        {
            this.ventouze = ventouze;
            this.activate = activate;
        }

        public override void Execute()
        {
            GestionnaireServosPR gestio = new GestionnaireServosPR();
            if (this.ventouze == VENTOUZES.VENTOUZE_DROITE)
            {
                gestio.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
            }
            else
            {
                gestio.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute();
            }

            if(activate) {
                Robot.Robot.robot.PR_RELAIS_VENTOUZES.Activate();
            }
            else {
                Robot.Robot.robot.PR_RELAIS_VENTOUZES.Desactivate();
            }

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
