using System;
using Microsoft.SPOT;
using GTM = Gadgeteer.Modules;
using System.Threading;

namespace Robot_P16.Robot.composants.CapteursObstacle
{
    public class Ultrason : CapteurObstacle
    {
        private GTM.GHIElectronics.DistanceUS3 capteurUltrason;
        private bool lastStatus;

        private static int REFRESH_RATE = 500;
        private static double THRESHOLD = 20.0;

        public Ultrason(int socket, OBSTACLE_DIRECTION direction)
            : base(direction)
        {
            capteurUltrason = new GTM.GHIElectronics.DistanceUS3(socket);
            lastStatus = IsThereAnObstacle();
            /*Gadgeteer.Timer timer = new Gadgeteer.Timer(REFRESH_RATE);
            timer.Tick += this.detectObstacle;
            timer.Start();*/
        }

        public override bool IsThereAnObstacle()
        {
            return (capteurUltrason.GetDistance() < THRESHOLD);
        }

        private void detectObstacle(Gadgeteer.Timer timer)
        {
            double distance = capteurUltrason.GetDistance();
            Informations.printInformations(Priority.VERY_LOW, "Distance read : " + distance);

            bool obstacle = IsThereAnObstacle();

            Informations.printInformations(Priority.VERY_LOW, "Obstacle IR : "+obstacle+"; lastStatus : " + lastStatus);

            if (obstacle != this.lastStatus)
            {
                this.lastStatus = obstacle;
                this.OnObstacleChange(obstacle);
            }
            // TODO : Add threshold
        }
    }
}
