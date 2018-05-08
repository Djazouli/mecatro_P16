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
        private int speedDrive;
        private int speedTurn;
        public Mouvement(PointOriente destination)
        {
            this.destination = destination;
            this.speedDrive = Robot.robot.BASE_ROULANTE.speedDrive;
            this.speedTurn = Robot.robot.BASE_ROULANTE.speedTurn;
        }
        public Mouvement(PointOriente destination, bool adjustToAngle)
        {
            this.adjustToAngle = adjustToAngle;
            this.destination = destination;
            this.speedDrive = Robot.robot.BASE_ROULANTE.speedDrive;
            this.speedTurn = Robot.robot.BASE_ROULANTE.speedTurn;
        }
        public Mouvement(PointOriente destination, bool adjustToAngle, OBSTACLE_DIRECTION forcedDirection)
        {
            this.adjustToAngle = adjustToAngle;
            this.destination = destination;
            this.isDirectionForced = true;
            this.forcedDirection = forcedDirection;
            this.speedDrive = Robot.robot.BASE_ROULANTE.speedDrive;
            this.speedTurn = Robot.robot.BASE_ROULANTE.speedTurn;
        }
        public Mouvement(PointOriente destination,  OBSTACLE_DIRECTION forcedDirection)
        {
            this.destination = destination;
            this.isDirectionForced = true;
            this.forcedDirection = forcedDirection;
            this.speedDrive = Robot.robot.BASE_ROULANTE.speedDrive;
            this.speedTurn = Robot.robot.BASE_ROULANTE.speedTurn;
        }
        public Mouvement(PointOriente destination, int speedDrive)
        {
            this.destination = destination;
            this.speedDrive = speedDrive;
            this.speedTurn = Robot.robot.BASE_ROULANTE.speedTurn;
        }
        public Mouvement(PointOriente destination, bool adjustToAngle, int speedDrive)
        {
            this.adjustToAngle = adjustToAngle;
            this.destination = destination;
            this.speedDrive =speedDrive;
            this.speedTurn = Robot.robot.BASE_ROULANTE.speedTurn;
        }
        public Mouvement(PointOriente destination, bool adjustToAngle, OBSTACLE_DIRECTION forcedDirection, int speedDrive)
        {
            this.adjustToAngle = adjustToAngle;
            this.destination = destination;
            this.isDirectionForced = true;
            this.forcedDirection = forcedDirection;
            this.speedDrive = speedDrive;
            this.speedTurn = Robot.robot.BASE_ROULANTE.speedTurn;
        }
        public Mouvement(PointOriente destination, OBSTACLE_DIRECTION forcedDirection, int speedDrive)
        {
            this.destination = destination;
            this.isDirectionForced = true;
            this.forcedDirection = forcedDirection;
            this.speedDrive = speedDrive;
            this.speedTurn = Robot.robot.BASE_ROULANTE.speedTurn;
        }

        public Mouvement(PointOriente destination, int speedDrive, int speedTurn)
        {
            this.destination = destination;
            this.speedDrive = speedDrive;
            this.speedTurn = speedTurn;
        }
        public Mouvement(PointOriente destination, bool adjustToAngle, int speedDrive, int speedTurn)
        {
            this.adjustToAngle = adjustToAngle;
            this.destination = destination;
            this.speedDrive = speedDrive;
            this.speedTurn = speedTurn;
        }
        public Mouvement(PointOriente destination, bool adjustToAngle, OBSTACLE_DIRECTION forcedDirection, int speedDrive, int speedTurn)
        {
            this.adjustToAngle = adjustToAngle;
            this.destination = destination;
            this.isDirectionForced = true;
            this.forcedDirection = forcedDirection;
            this.speedDrive = speedDrive;
            this.speedTurn = speedTurn;
        }
        public Mouvement(PointOriente destination, OBSTACLE_DIRECTION forcedDirection, int speedDrive, int speedTurn)
        {
            this.destination = destination;
            this.isDirectionForced = true;
            this.forcedDirection = forcedDirection;
            this.speedDrive = speedDrive;
            this.speedTurn = speedTurn;
        }

        public Mouvement setSpeedDrive(int speedDrive)
        {
            this.speedDrive = speedDrive;
            return this;
        }

        public Mouvement setSpeedTurn(int speedTurn)
        {
            this.speedTurn = speedTurn;
            return this;
        }

        public void Start()
        {
            // Launch command
            Informations.printInformations(Priority.HIGH, "Mouvement - Start() called");
            if (Robot.robot.OBSTACLE_MANAGER != null)
                Robot.robot.OBSTACLE_MANAGER.ObstacleChangeEvent += this.ObstacleListener;
            Informations.printInformations(Priority.HIGH, "Started at" + GetPosition().x.ToString() + "," + GetPosition().y.ToString()+","+GetPosition().theta.ToString());

            bool ok;
            if (this.isDirectionForced)
                ok = this.GoToOrientedPoint(this.destination, this.forcedDirection);
            else
                ok = this.GoToOrientedPoint(this.destination);

            if (ok)
            {
                this.isPaused = false;
                if (Robot.robot.OBSTACLE_MANAGER != null)
                    Robot.robot.OBSTACLE_MANAGER.ObstacleChangeEvent -= this.ObstacleListener;
                Robot.robot.BASE_ROULANTE.LaunchMovingInstructionCompletedEvent();
            }
            else
            {
                while (isPaused)
                {
                    Thread.Sleep(500);
                }
                Start();
            }
        }
        public int getSpeedDrive()
        {
            return Robot.robot.BASE_ROULANTE.speedDrive;
        }

        public int getSpeedTurn()
        {
            return Robot.robot.BASE_ROULANTE.speedTurn;
        }
        public void Rotate(double angle)
        {
            if (Robot.robot.TypeRobot == TypeRobot.GRAND_ROBOT)
            {
                angle = -angle;
            }
            Robot.robot.BASE_ROULANTE.kangaroo.rotate(angle, getSpeedTurn());
        }

        public void Avance(int distance)
        {
            if (Robot.robot.TypeRobot == TypeRobot.PETIT_ROBOT)
            {
                distance = -distance;
            }
            Robot.robot.BASE_ROULANTE.kangaroo.drive(distance, getSpeedDrive());
        }

        public Boolean GoToOrientedPoint(PointOriente pt, OBSTACLE_DIRECTION forceDir) // forceDir = AVANT or ARRIERE
        {
            isPaused = Robot.robot.OBSTACLE_MANAGER.getObstacleStatusForDirection(forceDir);
            if (isPaused) return false;
            Informations.printInformations(Priority.HIGH, "Going (dir forced) to " + pt.x.ToString() + "," + pt.y.ToString() + "\r\n");
            Robot.robot.BASE_ROULANTE.direction = forceDir;
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
                Informations.printInformations(Priority.HIGH, "Going en avant");
                if (this.GetPosition().theta%360 - deltaTheta%360 > 180) // That means we have to turn atrigo
                {
                    Rotate(convertTo180(360 - this.GetPosition().theta%360 - deltaTheta%360));
                   // waiting for the move to be completed
                    if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
                }
                else
                {
                    Rotate(convertTo180(-this.GetPosition().theta%360 + deltaTheta%360));
                   // waiting for the move to be completed
                    if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
                }
                deltaX = pt.x - this.GetPosition().x;
                deltaY = pt.y - this.GetPosition().y;
                Avance((int)System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
                //Robot.robot.BASE_ROULANTE.MoveCompleted.WaitOne();// waiting for the move to be completed
                if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
                //position = new PointOriente(pt.x, pt.y, deltaTheta);
                angle = deltaTheta;
            }
            if (forceDir == OBSTACLE_DIRECTION.ARRIERE)
            {
                Informations.printInformations(Priority.HIGH, "Going en arriere");
                if (this.GetPosition().theta%360 - deltaTheta > 180)
                {//turn antitrigo
                    Rotate(convertTo180(-convertTo360(this.GetPosition().theta) + 180 - convertTo360(deltaTheta)));
                    //Robot.robot.BASE_ROULANTE.MoveCompleted.WaitOne();// waiting for the move to be completed
                    if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
                }
                else
                {
                    Rotate(convertTo180(180 - convertTo360(this.GetPosition().theta)+convertTo360(deltaTheta)));
                    //Robot.robot.BASE_ROULANTE.MoveCompleted.WaitOne();// waiting for the move to be completed
                    if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
                }
                deltaX = pt.x - this.GetPosition().x;
                deltaY = pt.y - this.GetPosition().y;
                Avance(-(int)System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
                //position = new PointOriente(pt.x, pt.y, 180+deltaTheta%360); // check the angle
                //Robot.robot.BASE_ROULANTE.MoveCompleted.WaitOne();// waiting for the move to be completed
                if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
            }
            if (adjustToAngle)
            {
                Informations.printInformations(Priority.LOW, "Starting to adjust angle");
                Rotate((convertTo180(-GetPosition().theta + pt.theta)));
                //Robot.robot.BASE_ROULANTE.MoveCompleted.WaitOne();// waiting for the move to be completed
                if (this.isPaused) return false; // MUST BE CHECKED AFTER EACH WaitOne!!!!
            }
            Informations.printInformations(Priority.HIGH, "DOne with gotoForced dir.");
            return true;
        }
        public Boolean GoToOrientedPoint(PointOriente pt) // Automatically find direction
        {
            double angle;
            double deltaX, deltaY, deltaTheta;
            deltaX = pt.x - this.GetPosition().x;
            deltaY = pt.y - this.GetPosition().y;

            Informations.printInformations(Priority.HIGH, "Going to " + pt.x.ToString() + "," + pt.y.ToString() + "\r\n");
            if (deltaX == 0)
            {
                if (deltaY > 0)
                {
                    deltaTheta = 90;
                }
                else deltaTheta = 270;
            }
            else{
                deltaTheta = convertTo360(180 / System.Math.PI * System.Math.Atan2(deltaY , deltaX));
            }
            angle = (convertTo360(this.GetPosition().theta) -  convertTo360(deltaTheta)+90);
            Informations.printInformations(Priority.HIGH, "Angle before modulo : " + angle.ToString());
            angle = convertTo360(angle);
            Informations.printInformations(Priority.LOW, "J'ai deltaTheta = " + deltaTheta.ToString());
            Informations.printInformations(Priority.LOW, "Donc angle = " + angle.ToString());
            if (angle >180)
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
            //Robot.robot.OBSTACLE_MANAGER.TimerRefresh.Stop();
            Robot.robot.IHM.AfficherInformation("PAUSED", false);
            Robot.robot.BASE_ROULANTE.kangaroo.stop();
        }

        public void ObstacleListener(OBSTACLE_DIRECTION direction, bool isThereAnObstacle)
        {
            if (!Map.MapInformation.isObstacleDetecteurOn())
            {
                Informations.printInformations(Priority.MEDIUM, "Mouvement - ObstacleListener - !Map.MapInformation.isObstacleDetecteurOn(), no change on trajectory. ");
                return;
            }

            if (direction == Robot.robot.BASE_ROULANTE.GetDirection())
            {
                if (isThereAnObstacle /*&& Robot.robot.BASE_ROULANTE.kangaroo.currentMode == "D"*/)
                {
                   Informations.printInformations(Priority.MEDIUM, "Mouvement - ObstacleListener - obstacle detected : pausing movement. ");

                   this.Pause();
                }
                if(!isThereAnObstacle)
                {
                    Informations.printInformations(Priority.MEDIUM, "Mouvement - ObstacleListener - obstacle removed : resuming movement. ");
                    Robot.robot.IHM.AfficherInformation("UNPAUSED", false);
                    //new Thread(() => this.Start()).Start();
                    isPaused = false;
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
            angle = convertTo360(angle); ; //angle in [0,360]
            if (angle <= 180)
            {
                return angle;
            }
            else
            {   
             
                return (angle - 360);
            }

        }

        public double convertTo360(double angle)
        {
            if (angle >= 360) return (angle - 360);
            if (angle >= 0) return angle;
            if (angle <= -360) return (720 + angle);
            else return (360 + angle);
        }
    }
}
