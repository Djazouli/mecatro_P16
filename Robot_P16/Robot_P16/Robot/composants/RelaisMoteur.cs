using System;
using Microsoft.SPOT;

using Microsoft.SPOT.Hardware;
using Gadgeteer;

namespace Robot_P16.Robot.composants
{
    public class RelaisMoteur : Composant
    {
        private static RelaisMoteur RELAIS_ACTIF = null;

        private OutputPort Port_relais;

        public RelaisMoteur(int socket, int port) : base(socket)
        {
            Socket sock = Socket.GetSocket(this.socket, true, null, null);
            this.Port_relais = new OutputPort(sock.CpuPins[port],false);
        }

        public void Activate()
        {
            if(RELAIS_ACTIF != null) {
                RELAIS_ACTIF.Desactivate();
            }
            this.Port_relais.Write(true);
            RELAIS_ACTIF = this;
        }

        public void Desactivate()
        {
            this.Port_relais.Write(false);
            RELAIS_ACTIF = null;
        }

    }
}
