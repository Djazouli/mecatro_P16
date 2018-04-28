using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using Gadgeteer.Networking;
using GT = Gadgeteer;
using Robot_P16.Robot;
using System.IO.Ports;
namespace Robot_P16.Actions
{
    public class ActionRamasseCube : Action
    {
        private string[] codeCouleur;

        public ActionRamasseCube(string[] CodeCouleur)
            : base("Attente de la couleur")
        {
            this.codeCouleur = CodeCouleur;
        }

        public override void execute()
        {
            


        }
    }
}