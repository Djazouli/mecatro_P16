using System;
using Microsoft.SPOT;

using Gadgeteer;
using Gadgeteer.Modules;
using Microsoft.SPOT.Hardware;

namespace Robot_P16.Robot.composants
{

    public delegate void JackListenerDelegate(bool isJackOn);

    public class JackInterrupt : Composant
    {
        private InterruptPort portJack;
        private int port;

        public event JackListenerDelegate JackChangeEvent;

        public JackInterrupt(int socket, int port)
            : base(socket)
        {
            this.port = port;
            this.portJack = new InterruptPort(
               Socket.GetSocket(socket, true, null, null).CpuPins[port],
               true,
               Port.ResistorMode.PullUp,
               Port.InterruptMode.InterruptEdgeHigh);

            this.portJack.OnInterrupt += (uint pin, uint state, DateTime time) => launchJackEvent();
        }

        public void launchJackEvent()
        {
            Informations.printInformations(Priority.HIGH, "Jack interrupt event detected");
            if( JackChangeEvent != null ) {
                JackChangeEvent(this.IsJackOn());
            }
        }

        public bool IsJackOn()
        {
            return !this.portJack.Read();
        }

    }
}
