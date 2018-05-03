using System;
using Microsoft.SPOT;

using Robot_P16.Map;
using Robot_P16.Robot.composants.BaseRoulante;
using System.Threading;


namespace Robot_P16.Robot.composants.BaseRoulante
{
    public class Mouvement
    {
        public PointOriente destination;
        private Boolean isPaused = false;
        private Boolean adjustToAngle = false;
        private Boolean isDirectionForced = false;
        private OBSTACLE_DIRECTION forcedDirection = OBSTACLE_DIRECTION.AVANT;

        public Mouvement(PointOriente destination)
        {
            this.destination = destination;
        }
        public Mouvement(PointOriente destination, bool adjustToAngle)
        {
            this.adjustToAngle = adjustToAngle;
            this.destination = destination;
        }
        public Mouvement(PointOriente destination, bool adjustToAngle, OBSTACLE_DIRECTION forcedDirection)
        {
            this.adjustToAngle = adjustToAngle;
            this.destination = destination;
            this.forcedDirection = forcedDirection;
        }
        public Mouvement(PointOriente destination,  OBSTACLE_DIRECTION forcedDirection)
        {
            this.destination = destination;
            this.forcedDirection = forcedDirection;
        }

        public void Start()
        {
            this.isPaused = false;
            // Launch command

            Robot.robot.OBSTACLE_MANAGER.ObstacleChangeEvent += this.ObstacleListener;

            if (this.isDirectionForced)
                this.GoToOrientedPoint(this.destination, this.forcedDirection);
            else
                this.GoToOrientedPoint(this.destination);

            
            // waiting for the move to be completed
            if (this.isPaused) return; // MUST BE CHECKED AFTER EACH WaitOne!!!!

            // Movement completed
            Robot.robot.OBSTACLE_MANAGER.ObstacleChangeEvent -= this.ObstacleListener;
        }

        public Boolean GoToOrientedPoint(PointOriente pt, OBSTACLE_DIRECTION forceDir) // forceDir = AVANT or ARRIERE
        {
            Robot.robot.BASE_ROULANTE.MoveCompleted.WaitOne();// waiting for the move to be completed
            if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
            // AdjustToAngle == True must be handled here
            return true;
        }
        public Boolean GoToOrientedPoint(PointOriente pt) // Automatically find direction
        {
            return true;
        }

        public void Pause()
        {
            this.isPaused = true;
            Robot.robot.BASE_ROULANTE.kangaroo.stop(); // This updates position automatically
            Robot.robot.BASE_ROULANTE.MoveCompleted.Set();
        }

        public void ObstacleListener(OBSTACLE_DIRECTION direction, bool isThereAnObstacle)
        {
            if (!Map.MapInformation.isObstacleDetecteurOn())
            {
                Informations.printInformations(Priority.MEDIUM, "Mouvement - ObstacleListener - !Map.MapInformation.isObstacleDetecteurOn(), no change on trajectory. ");
                return;
            }

            if (direction != Robot.robot.BASE_ROULANTE.GetDirection())
            {
                if (isThereAnObstacle)
                {
                   Informations.printInformations(Priority.MEDIUM, "Mouvement - ObstacleListener - obstacle detected : pausing movement. ");
                   this.Pause();
                }
                else
                {
                    Informations.printInformations(Priority.MEDIUM, "Mouvement - ObstacleListener - obstacle removed : resuming movement. ");
                    this.Start();
                }
            }
            else
            {
                Informations.printInformations(Priority.MEDIUM, "Mouvement - ObstacleListener - obstacle in other direction : ignored. ");
            }
        }

    }
}
