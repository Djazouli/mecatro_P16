using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using Gadgeteer.Networking;
using GT = Gadgeteer;
using Robot_P16.Robot;
using System.IO.Ports;
using Robot_P16.Robot.composants.Servomoteurs;
using System.Threading;

namespace Robot_P16.Actions
{


    public class ActionReleaseCube : Action
    {
        private GestionnaireServosPR gestionnaire;
        public ActionReleaseCube() : base("Release Cube")
        {
            this.gestionnaire = new GestionnaireServosPR();
        }
        public override void Execute()
{
    Action action = new ActionBuilder("build").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
        ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU).BuildActionEnSerie();
    action.StatusChangeEvent += (x) => this.Status = ActionStatus.SUCCESS;
    action.Execute();
}
        public override void Feedback(Action a)
        {
            return;
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            return true;
        }
    }


}