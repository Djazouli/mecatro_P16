using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Gadgeteer;

namespace Robot_P16.Robot.composants
{
    
    public delegate void JackListenerDelegate(bool isJackOn);

    public class Jack
    {
        public event JackListenerDelegate JackChangeEvent;
        private InputPort cableJack;
        private bool lastStatus;
        private static int REFRESH_RATE = 100;
        private Timer timer;
        private int iterations= 0;

        public Jack(int socket, int port)
        {
            
            Informations.printInformations(Priority.MEDIUM, "New Jack on socket " + socket + " & port " + port + "; pin : " + Socket.GetSocket(socket, true, null, null).CpuPins[port] + "; type of pin : " + Socket.GetSocket(socket, true, null, null).CpuPins[port].GetType());
            /*capteurIR = new InterruptPort(Socket.GetSocket(socket, true, null, null).CpuPins[port], false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            //capteurIR.EnableInterrupt();
            capteurIR.OnInterrupt += new NativeEventHandler((uint data1, uint data2, DateTime time) => this.OnObstacleChange());*/

            this.cableJack = new InputPort(Socket.GetSocket(socket, true, null, null).CpuPins[port], false, Port.ResistorMode.Disabled);

            this.timer = new Gadgeteer.Timer(REFRESH_RATE);
            timer.Tick += this.Tick;
        }

        public void StartTimer()
        {
            this.lastStatus = IsJackOn();
            timer.Start();
        }
        public void StopTimer()
        {
            timer.Stop();
        }

        public bool IsJackOn()
        {
            return !cableJack.Read();
        }

        private void Tick(Timer t)
        {
            iterations++;
            bool status = IsJackOn();
            Robot_P16.Robot.Robot.robot.IHM.AfficherInformation("Iteration "+iterations+"\nJack satus :\n" + Robot_P16.Robot.Robot.robot.JACK.IsJackOn(), false);
            if (status != lastStatus) {
                Informations.printInformations(Priority.HIGH, "Jack changed : new status : " + status);
                lastStatus = status; ;
                OnJackChange(lastStatus);
            }
        }

        protected void OnJackChange(bool isJackOn)
        {
            if (JackChangeEvent != null)
            {
                JackChangeEvent(isJackOn);
            }
        }

    }
}
