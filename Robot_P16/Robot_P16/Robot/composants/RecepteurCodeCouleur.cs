using System;
using Microsoft.SPOT;
using GTM = Gadgeteer.Modules;
using System.Threading;

namespace Robot_P16.Robot.composants
{
    public class RecepteurCodeCouleur : Composant
    {
        private GTM.GHIElectronics.XBeeAdapter adapteur;

        public RecepteurCodeCouleur(int socket)
            : base(socket)
        {
            this.adapteur = new GTM.GHIElectronics.XBeeAdapter(socket);
        }
    }
}
