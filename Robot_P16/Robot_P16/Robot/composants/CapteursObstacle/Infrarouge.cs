using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Gadgeteer;

namespace Robot_P16.Robot.composants.CapteursObstacle
{
    public class Infrarouge : CapteurObstacle
    {
        private InterruptPort capteurIR;
        private static int REFRESH_RATE = 500;
        private bool lastStatus;

        public Infrarouge(int socket, int port, OBSTACLE_DIRECTION direction)
            : base(direction)
        {
            
            Informations.printInformations(Priority.MEDIUM, "New infrarouge on socket " + socket + " & port " + port + "; pin : " + Socket.GetSocket(socket, true, null, null).CpuPins[port] + "; type of pin : " + Socket.GetSocket(socket, true, null, null).CpuPins[port].GetType());
            this.capteurIR = new InterruptPort(
              Socket.GetSocket(socket, true, null, null).CpuPins[port],
              true,
              Port.ResistorMode.PullUp,
              Port.InterruptMode.InterruptEdgeBoth);
            this.capteurIR.OnInterrupt += (uint pin, uint state, DateTime time)  => OnObstacleChange(state == 0);
        }


        public override bool IsThereAnObstacle()
        {
            return !capteurIR.Read();
        }

    }
}
