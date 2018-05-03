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


    public delegate void BaseRoulanteMovingStatusChange(bool isMoving);

    public class BaseRoulante : Composant
    {
        private PointOriente position;
        public Kangaroo kangaroo;
        public event BaseRoulanteMovingStatusChange MovingStatusChangeEvent;

        public OBSTACLE_DIRECTION direction;

        public int speedDrive = 25;// avance 10 cm par seconde
        public int speedTurn = 70; //tourne 30 degrees par seconde


        int PARAMETER_FOR_XY = 1;//l'unite de la dist. = millimetre, on n'accepte QUE l'entier
        int PARAMETER_FOR_THETA = 100;//l'unite de l'angle = millidegree, on accepte l'entree de la forme X.XX degrees

        public BaseRoulante(int socket)
            : base(socket)
        {
            this.kangaroo = new Kangaroo(socket);
            this.position = new PointOriente(0, 0, 0);
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

        public PointOriente getPosition()
        {
            return position;
        }

        public void setPosition(PointOriente pt)
        {
            position = pt;
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

        public Boolean Stop()
        {
            Informations.printInformations(Priority.MEDIUM, "BaseRoulante - Stopping Movement");
            kangaroo.powerdown(mode.drive);
            LaunchMovingStatusChangeEvent(false);
            return true; // TODO
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
            deltaX = pt.x - position.x;
            deltaY = pt.y - position.y;
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
                angle = position.theta - deltaTheta;
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
            deltaX = pt.x - position.x;
            deltaY = pt.y - position.y;
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
                if (position.theta - deltaTheta > 180) // That means we have to turn atrigo
                {
                    RotateAndSleep((int)(360 - position.theta + deltaTheta));
                }
                else
                {
                    RotateAndSleep(-(int)(-position.theta + deltaTheta));
                }
                AvanceAndSleep((int)System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
            }
            if (forceDir == OBSTACLE_DIRECTION.ARRIERE)
            {
                Debug.Print("Arriere");
                if(position.theta - deltaTheta > 180){//turn antitrigo
                       RotateAndSleep(-(int)(position.theta - 180 - deltaTheta));
                }
                else{
                        RotateAndSleep((int) (180-position.theta+deltaTheta));
                    }
            AvanceAndSleep(-(int)System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
            }
            position = new PointOriente(pt.x, pt.y, deltaTheta);
            //Thread.Sleep(20000);
            LaunchMovingStatusChangeEvent(false);
            return false;
        }

        public Boolean AdjustAngleToPoint(PointOriente pt) // ajuste theta, mais pas X,Y => mode turn
        {
            //Calculer l'angle a tourner
            Double currentAngle = position.theta;//[-180,180]
            Double Angle = pt.theta;//[-180,180]
            int deltaAngle = doubleToIntForTheta((Angle - currentAngle));//[-360,360]
            
            //Amerliorer l'angle a tourner
            if (deltaAngle > 180) deltaAngle -= 360; //[180,360]->[-180,0]
            else if (deltaAngle < -180) deltaAngle += 360;//[-360,-180]->[-180,0]
            
            //Faire la rotation
            RotateAndSleep(deltaAngle); 
 
            //Mise a jour la position
            position = new PointOriente(position.x, position.y, pt.theta);
            
            return false;
        }

        public Boolean GoToAndAdjustAngleToPoint(PointOriente pt)
        {
            GoToOrientedPoint(pt); 
            AdjustAngleToPoint(pt);
            return false;
        }

        public Boolean Move(MOVETYPES type, PointOriente pt){
            switch (type)
            {
                case MOVETYPES.GoTo:
                    GoToOrientedPoint(pt);
                    break;
                case MOVETYPES.AdjustAngle:
                    AdjustAngleToPoint(pt);
                    break;
                case MOVETYPES.GoToAndAdjustAngle:
                    GoToAndAdjustAngleToPoint(pt);
                    break;
            }
            return false;
        }
        
        public Boolean GoToLieuCle(LieuCle lieu)
        {
            GoToOrientedPoint(lieu.pointDeReference);
            PointOriente positionDuRobotApresDeplacement = position;
            return lieu.IsAtTheRightPlace(positionDuRobotApresDeplacement);
        }

        public Boolean AdjustAngleToLieuCle(LieuCle lieu)
        {
            AdjustAngleToPoint(lieu.pointDeReference);
            PointOriente positionDuRobotApresDeplacement = position;
            return lieu.IsAtTheRightAngle(positionDuRobotApresDeplacement);
        }

        public Boolean GoAndAdjustAngleToLieuCle(LieuCle lieu)
        {
            GoToLieuCle(lieu);
            AdjustAngleToLieuCle(lieu);
            PointOriente positionDuRobotApresDeplacement = position;
            return lieu.IsAtTheRightPlaceAndAngle(positionDuRobotApresDeplacement);
        }

        public OBSTACLE_DIRECTION GetDirection()
        {
            return this.direction;
        }

        private void LaunchMovingStatusChangeEvent(bool isMoving)
        {
            if (this.MovingStatusChangeEvent != null ){
                this.MovingStatusChangeEvent(isMoving);
            }
        }
    }
}
