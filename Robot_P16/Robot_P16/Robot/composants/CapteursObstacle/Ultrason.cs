using System;
using Microsoft.SPOT;
using GTM = Gadgeteer.Modules;
using System.Threading;

namespace Robot_P16.Robot.composants.CapteursObstacle
{
    public class Ultrason : CapteurObstacle
    {
        private GTM.GHIElectronics.DistanceUS3 capteurUltrason;
        private bool lastStatus = false;

        public int REFRESH_RATE = 100;
        private static double THRESHOLD = 20.0;

        public Ultrason(int socket, OBSTACLE_DIRECTION direction)
            : base(direction)
        {
            capteurUltrason = new GTM.GHIElectronics.DistanceUS3(socket);
            capteurUltrason.DebugPrintEnabled = true;
            //lastStatus = IsThereAnObstacle();
            /*Gadgeteer.Timer timer = new Gadgeteer.Timer(REFRESH_RATE);
            timer.Tick += this.detectObstacle;
            timer.Start();*/

        }

        public void start()
        {
            
        }

        public override bool IsThereAnObstacle()
        {
            return (capteurUltrason.GetDistance() < THRESHOLD);
        }

        public void detectObstacle(Gadgeteer.Timer timer)
        {
            //if (Robot.robot.OBSTACLE_MANAGER.ObstacleChangeEvent == null) return; TODO : UNCOMMENT AFTER DEBUG

            bool obstacle = IsThereAnObstacle();

            Informations.printInformations(Priority.VERY_LOW, "Obstacle IR : "+obstacle+"; lastStatus : " + lastStatus);

            if (obstacle != this.lastStatus)
            {
                this.lastStatus = obstacle;
                this.OnObstacleChange(obstacle);
            }
        }
    }
}
