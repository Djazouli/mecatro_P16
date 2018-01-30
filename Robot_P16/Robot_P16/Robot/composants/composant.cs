using System;
using Microsoft.SPOT;

using Gadgeteer;
using Gadgeteer.Modules.GHIElectronics;

namespace Robot_P16.Robot.Composants
{
    public class Composant
    {
        public readonly int socket;
        public Composant(int socket)
        {
            this.socket = socket;
        }
    }
}
