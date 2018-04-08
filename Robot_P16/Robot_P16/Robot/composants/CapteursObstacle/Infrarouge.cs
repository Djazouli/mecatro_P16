using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Gadgeteer;

namespace Robot_P16.Robot.composants.CapteursObstacle
{
    public class Infrarouge : CapteurObstacle
    {
        private InputPort capteurIR;
        private static int REFRESH_RATE = 500;
        private bool lastStatus;

        public Infrarouge(int socket, int port, OBSTACLE_DIRECTION direction)
            : base(direction)
        {
            
            Informations.printInformations(Priority.MEDIUM, "New infrarouge on socket " + socket + " & port " + port + "; pin : " + Socket.GetSocket(socket, true, null, null).CpuPins[port] + "; type of pin : " + Socket.GetSocket(socket, true, null, null).CpuPins[port].GetType());
            /*capteurIR = new InterruptPort(Socket.GetSocket(socket, true, null, null).CpuPins[port], false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            //capteurIR.EnableInterrupt();
            capteurIR.OnInterrupt += new NativeEventHandler((uint data1, uint data2, DateTime time) => this.OnObstacleChange());*/

            capteurIR = new InputPort(Socket.GetSocket(socket, true, null, null).CpuPins[port], false, Port.ResistorMode.Disabled);

            lastStatus = IsThereAnObstacle();

            /*Gadgeteer.Timer timer = new Gadgeteer.Timer(REFRESH_RATE);
            timer.Tick += this.detectObstacle;
            timer.Start();*/
        }


        public override bool IsThereAnObstacle()
        {
            return !capteurIR.Read();
        }


        private void detectObstacle(Gadgeteer.Timer timer)
        {
            bool obstacle = IsThereAnObstacle();
            Informations.printInformations(Priority.VERY_LOW, "Obstacle read by IR "+capteurIR.Id+": " + obstacle);
            // TODO : Add threshold
            if (obstacle != lastStatus)
            {
                lastStatus = obstacle;
                this.OnObstacleChange(obstacle);
            }
        }
    }
}
