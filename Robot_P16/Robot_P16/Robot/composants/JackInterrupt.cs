using System;
using Microsoft.SPOT;

using Gadgeteer;
using Gadgeteer.Modules;
using Microsoft.SPOT.Hardware;

using System.Threading;

namespace Robot_P16.Robot.composants
{

    public delegate void JackListenerDelegate(bool isJackOn);

    public class JackInterrupt : Composant
    {
        private InterruptPort portJack;
        private int port;
        private bool isJackEventAboutToBeLaunched = false;
        public bool doneListeningToJack = false;

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
            if (doneListeningToJack)
            {
                Informations.printInformations(Priority.MEDIUM, "LaunchJackEvent called, but done listening to Jack.");
                return; // Already called by another interrupt event
            }
            if (isJackEventAboutToBeLaunched)
            {
                Informations.printInformations(Priority.HIGH, "LaunchJackEvent called, but was already called earlier.");
                return; // Already called by another interrupt event
            }
            isJackEventAboutToBeLaunched = true; // Not very Thread safe, but who cares ?
            new Thread(() => {
                Thread.Sleep(500);
                Informations.printInformations(Priority.HIGH, "Jack interrupt event detected : isJackOn : " + this.IsJackOn());
                if (JackChangeEvent != null)
                {
                    JackChangeEvent(this.IsJackOn());
                }
                isJackEventAboutToBeLaunched = false;
            }).Start();
        }

        public bool IsJackOn()
        {
            return !this.portJack.Read();
        }

    }
}
