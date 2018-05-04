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
            this.isDirectionForced = true;
            this.forcedDirection = forcedDirection;
        }
        public Mouvement(PointOriente destination,  OBSTACLE_DIRECTION forcedDirection)
        {
            this.destination = destination;
            this.isDirectionForced = true;
            this.forcedDirection = forcedDirection;
        }

        public void Start()
        {
            this.isPaused = false;
            // Launch command
            Informations.printInformations(Priority.MEDIUM, "Mouvement - Start() called");
            Robot.robot.OBSTACLE_MANAGER.ObstacleChangeEvent += this.ObstacleListener;
            Informations.printInformations(Priority.MEDIUM, "Started at" + GetPosition().x.ToString() + "," + GetPosition().y.ToString()+","+GetPosition().theta.ToString());

            if (this.isDirectionForced)
                this.GoToOrientedPoint(this.destination, this.forcedDirection);
            else
                this.GoToOrientedPoint(this.destination);

            
            // waiting for the move to be completed
            if (this.isPaused) return; // MUST BE CHECKED AFTER EACH WaitOne!!!!
            Informations.printInformations(Priority.MEDIUM, "Mouvement completed, launching completed event");
            // Movement completed
            Robot.robot.OBSTACLE_MANAGER.ObstacleChangeEvent -= this.ObstacleListener;
            Robot.robot.BASE_ROULANTE.LaunchMovingInstructionCompletedEvent();
        }
        public int getSpeedDrive()
        {
            return Robot.robot.BASE_ROULANTE.speedDrive;
        }

        public int getSpeedTurn()
        {
            return Robot.robot.BASE_ROULANTE.speedTurn;
        }
        public void Rotate(int angle)
        {
            Robot.robot.BASE_ROULANTE.kangaroo.rotate(angle, getSpeedTurn());
        }

        public void Avance(int distance)
        {
            Robot.robot.BASE_ROULANTE.kangaroo.drive(distance, getSpeedDrive());
        }

        public Boolean GoToOrientedPoint(PointOriente pt, OBSTACLE_DIRECTION forceDir) // forceDir = AVANT or ARRIERE
        {

            // AdjustToAngle == True must be handled here
            double angle=0;
            double deltaX, deltaY, deltaTheta, alpha;
            deltaX = pt.x - this.GetPosition().x;
            deltaY = pt.y - this.GetPosition().y;
            deltaTheta = 180 / System.Math.PI * System.Math.Atan2(deltaY, deltaX);
            if (deltaY == 0)
            {
                if (deltaX > 0)
                {
                    deltaTheta = 0;
                }
                else
                {
                    deltaTheta = 180;
                }
            }
            if (deltaTheta < 0)
            {
                deltaTheta = 360 + deltaTheta;
            }
            // We now have the good theta (between 0 and 360)
            if (forceDir == OBSTACLE_DIRECTION.AVANT)
            {
                Debug.Print("Going en avant");
                if (this.GetPosition().theta%360 - deltaTheta%360 > 180) // That means we have to turn atrigo
                {
                    Rotate((int)(360 - this.GetPosition().theta%360 - deltaTheta%360));
                   // waiting for the move to be completed
                    if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
                }
                else
                {
                    Rotate((int)(-this.GetPosition().theta%360 + deltaTheta%360));
                   // waiting for the move to be completed
                    if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
                }
                Avance((int)System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
                //Robot.robot.BASE_ROULANTE.MoveCompleted.WaitOne();// waiting for the move to be completed
                if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
                //position = new PointOriente(pt.x, pt.y, deltaTheta);
                angle = deltaTheta;
            }
            if (forceDir == OBSTACLE_DIRECTION.ARRIERE)
            {
                Debug.Print("Going en arriere");
                if (this.GetPosition().theta%360 - deltaTheta > 180)
                {//turn antitrigo
                    Rotate((int)convertTo180(-this.GetPosition().theta%360 + 180 - deltaTheta%360));
                    //Robot.robot.BASE_ROULANTE.MoveCompleted.WaitOne();// waiting for the move to be completed
                    if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
                }
                else
                {
                    Rotate((int)convertTo180(180 - this.GetPosition().theta%360 + deltaTheta%360));
                    //Robot.robot.BASE_ROULANTE.MoveCompleted.WaitOne();// waiting for the move to be completed
                    if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
                }
                Avance(-(int)System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
                //position = new PointOriente(pt.x, pt.y, 180+deltaTheta%360); // check the angle
                //Robot.robot.BASE_ROULANTE.MoveCompleted.WaitOne();// waiting for the move to be completed
                if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
                angle = (180 + deltaTheta) % 360;
            }
            if (adjustToAngle)
            {
                Informations.printInformations(Priority.LOW, "Starting to adjust angle");
                Rotate((int)(convertTo180(-angle + pt.theta)));
                //Robot.robot.BASE_ROULANTE.MoveCompleted.WaitOne();// waiting for the move to be completed
                if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
            }
            return true;
        }
        public Boolean GoToOrientedPoint(PointOriente pt) // Automatically find direction
        {
            double angle;
            double deltaX, deltaY, deltaTheta;
            deltaX = pt.x - this.GetPosition().x;
            deltaY = pt.y - this.GetPosition().y;

            Debug.Print("Going to " + pt.x.ToString() + "," + pt.y.ToString() + "\r\n");
            if (deltaX > 0)
            {
                deltaTheta = 180 / System.Math.PI * System.Math.Atan(deltaY / deltaX);
                if (deltaTheta < 0)
                {
                    deltaTheta = 360 + deltaTheta;
                }
            }
            else if (deltaX < 0)
            {
                deltaTheta = 180 + 180 / System.Math.PI * System.Math.Atan(deltaY / deltaX);
            }
            else
            {
                if (deltaY > 0)
                {
                    deltaTheta = 90;
                }
                else deltaTheta = 270;
            }
            deltaTheta = convertTo180(deltaTheta);
            angle = this.GetPosition().theta - (90+ deltaTheta);
            angle = convertTo180(angle);
            Informations.printInformations(Priority.LOW, "J'ai deltaTheta = " + deltaTheta.ToString());
            Informations.printInformations(Priority.LOW, "Donc angle = " + angle.ToString());
            if (angle > 0)
            {
                return this.GoToOrientedPoint(pt, OBSTACLE_DIRECTION.ARRIERE);
            }
            else
            {
                return this.GoToOrientedPoint(pt, OBSTACLE_DIRECTION.AVANT);
            }
        }

        public void Pause()
        {
            this.isPaused = true;
            Robot.robot.BASE_ROULANTE.kangaroo.stop();
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
        public PointOriente GetPosition()
        {
            return Robot.robot.BASE_ROULANTE.GetPosition();
        }
        public double convertTo180(double angle)
        {
            if (System.Math.Abs(angle) <= 180)
            {
                return angle;
            }
            else if (angle > 180)
            {   
             
                return (angle - 360);
            }
            else if (angle<-180){
                return (angle + 360);
            }
            return (angle);

        }

        public double convertTo360(double angle)
        {
            return angle % 360;
        }
    }
}
