using System;
using Microsoft.SPOT;

using Robot_P16.Map;
using Robot_P16.Robot.composants.BaseRoulante;
using System.Threading;


namespace Robot_P16.Robot.composants.BaseRoulante
{
    public enum MOVETYPES
    {
        GoTo = 1, AdjustAngle = 2, GoToAndAdjustAngle = 3
    };


    public delegate void BaseRoulanteInstructionCompleted();

    public class BaseRoulante : Composant
    {
        public Kangaroo kangaroo;

        private Mouvement CURRENT_MOUVEMENT = null;

        public event BaseRoulanteInstructionCompleted InstructionCompletedEvent;

        public AutoResetEvent MoveCompleted = new AutoResetEvent(false);
        private bool lastMovingStatus = false;

        public OBSTACLE_DIRECTION direction = OBSTACLE_DIRECTION.AVANT;

        public int speedDrive = 25;// avance 10 cm par seconde
        public int speedTurn = 70; //tourne 30 degrees par seconde

        private const int REFRESH_RATE_EVENT = 200;


        int PARAMETER_FOR_XY = 1;//l'unite de la dist. = millimetre, on n'accepte QUE l'entier
        int PARAMETER_FOR_THETA = 100;//l'unite de l'angle = millidegree, on accepte l'entree de la forme X.XX degrees

        public BaseRoulante(int socket)
            : base(socket)
        {
            this.kangaroo = new Kangaroo(socket);

            new Thread(() => {
                while (true)
                {
                    Thread.Sleep(REFRESH_RATE_EVENT);
                    checkIsMoving();
                }
            }).Start();

        }

        public void checkIsMoving()
        {
            bool currentlyMoving = this.kangaroo.isCurrentlyMoving();
            if (currentlyMoving != this.lastMovingStatus)
            {
                this.lastMovingStatus = currentlyMoving;
                if (currentlyMoving == false)
                {
                    MoveCompleted.Set();
                }
            }
        }

        private int doubleToIntForXY(double X)
        {
            X = X * PARAMETER_FOR_XY;
            return (int)X;
        }

        private int doubleToIntForTheta(double theta)
        {
            theta = theta * PARAMETER_FOR_THETA;
            return (int)theta;
        }

        private void RotateAndSleep(int angle)
        {
            kangaroo.tourner(angle, speedTurn);
            Thread.Sleep(System.Math.Abs(angle / speedTurn * 1000));
            Thread.Sleep(1000);
        }

        private void AvanceAndSleep(int distance)
        {
            kangaroo.allerEn(distance, speedDrive);
            Thread.Sleep(System.Math.Abs(distance / speedDrive * 1000));
            Thread.Sleep(1000);
        }

        public PointOriente GetPosition()
        {
            return this.kangaroo.getPosition();
        }

        /*public Boolean GoToOrientedPoint(PointOriente pt)
        {
            //LaunchMovingStatusChangeEvent(true);
            //Calculer l'angle a tourner 
            double deltaX = pt.x - position.x;
            double deltaY = pt.y - position.y;
            double result, radians, angle;
            int deltaAngle;
            if (deltaX != 0)
            {
                result = deltaY / deltaX;
                radians = System.Math.Atan(result);
                angle = radians * (180 / System.Math.PI);
            }
            else
            {
                if (deltaY > 0) angle = 90;
                else angle = -90;
            }
            deltaAngle = doubleToIntForTheta(angle - position.theta);
            
            int sens = 1; //avance=1,recule=-1
            
            //Ameliorer l'angle a tourner
            //[-360,-270],[270,360] -> [0,90], [-90,0], sens=avance
            if (System.Math.Abs(deltaAngle) >= 270) deltaAngle -= System.Math.Sign(deltaAngle) * 360;                
            //[-270,-90],[90,270]->[-90,90], sens=recule
            else if (System.Math.Abs(deltaAngle) >= 90) {
                deltaAngle -= System.Math.Sign(deltaAngle) * 180;
                sens = -1;
                angle -= System.Math.Sign(angle) * 180; 
            }
            //Faire orienter a la destination
            Debug.Print("Rotation and sleep" + deltaAngle);
            RotateAndSleep(deltaAngle);
        
            //Aller tout droite
            int distance = doubleToIntForXY(System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
            AvanceAndSleep(distance*sens);
       
            //Mise a jour la position
            position = new PointOriente(pt.x,pt.y,angle);

            Informations.printInformations(Priority.MEDIUM, "Done moving, calling launchMovingStatus");
            LaunchMovingStatusChangeEvent(false);
            return false;
        }*/
        public Boolean GoToOrientedPoint(PointOriente pt)
        {
            double angle;
            double deltaX, deltaY, deltaTheta, alpha;

            deltaX = pt.x - this.GetPosition().x;
            deltaY = pt.y - this.GetPosition().y;
            Debug.Print("Going to " + pt.x.ToString() + "," + pt.y.ToString() + "\r\n");
            if (deltaX > 0)
            {
                deltaTheta = 180/System.Math.PI * System.Math.Atan(deltaY / deltaX);
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
            angle = this.GetPosition().theta - deltaTheta;
                if (angle > 270 || angle < 90)
                {
                    return GoToOrientedPoint(pt, OBSTACLE_DIRECTION.AVANT);
                }
                else
                {
                    return GoToOrientedPoint(pt, OBSTACLE_DIRECTION.ARRIERE);
                }
        }
        public Boolean GoToOrientedPoint(PointOriente pt, OBSTACLE_DIRECTION forceDir) // forceDir = AVANT or ARRIERE
        { //We do not care about the angle in this function
            double angle;
            double deltaX, deltaY, deltaTheta, alpha;
            deltaX = pt.x - this.GetPosition().x;
            deltaY = pt.y - this.GetPosition().y;
            direction = forceDir;
            if (deltaX > 0)
            {
                deltaTheta = System.Math.Atan(deltaY / deltaX);
                if (deltaTheta < 0)
                {
                    deltaTheta = 360 + 180 / System.Math.PI * deltaTheta;
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
            if (forceDir == OBSTACLE_DIRECTION.AVANT) {
                Debug.Print("avant");
                if (this.GetPosition().theta - deltaTheta > 180) // That means we have to turn atrigo
                {
                    RotateAndSleep((int)(360 - this.GetPosition().theta + deltaTheta));
                }
                else
                {
                    RotateAndSleep(-(int)(-this.GetPosition().theta + deltaTheta));
                }
                AvanceAndSleep((int)System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
            }
            if (forceDir == OBSTACLE_DIRECTION.ARRIERE)
            {
                Debug.Print("Arriere");
                if (this.GetPosition().theta - deltaTheta > 180)
                {//turn antitrigo
                    RotateAndSleep(-(int)(this.GetPosition().theta - 180 - deltaTheta));
                }
                else{
                    RotateAndSleep((int)(180 - this.GetPosition().theta + deltaTheta));
                    }
            AvanceAndSleep(-(int)System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
            }
            //position = new PointOriente(pt.x, pt.y, deltaTheta);  NOT MY JOB TO DO THAT !!!!
            Thread.Sleep(20000);
            LaunchMovingStatusChangeEvent(false);
            return false;
        }

        public Boolean AdjustAngleToPoint(PointOriente pt) // ajuste theta, mais pas X,Y => mode turn
        {
            //Calculer l'angle a tourner
            Double currentAngle = this.GetPosition().theta;//[-180,180]
            Double Angle = pt.theta;//[-180,180]
            int deltaAngle = doubleToIntForTheta((Angle - currentAngle));//[-360,360]
            
            //Amerliorer l'angle a tourner
            if (deltaAngle > 180) deltaAngle -= 360; //[180,360]->[-180,0]
            else if (deltaAngle < -180) deltaAngle += 360;//[-360,-180]->[-180,0]
            
            //Faire la rotation
            RotateAndSleep(deltaAngle); 
 
            //Mise a jour la position
            // position = new PointOriente(position.x, position.y, pt.theta); NOT MY JOB TO DO THAT
            
            return false;
        }


        public OBSTACLE_DIRECTION GetDirection()
        {
            return this.direction;
        }

        private void LaunchMovingInstructionCompletedEvent()
        {
            if (this.InstructionCompletedEvent != null ){
                this.InstructionCompletedEvent();
            }
        }
    }
}
