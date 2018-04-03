using System;
using Microsoft.SPOT;

using Robot_P16.Map;
using Robot_P16.Robot.composants.BaseRoulante;
using System.Threading;


namespace Robot_P16.Robot.composants.BaseRoulante
{
    public class BaseRoulante : Composant
    {
        private PointOriente position;
        public Kangaroo kangaroo;

        public int speedDrive = 100;// avance 10 cm par seconde
        public int speedTurn = 3000; //tourne 30 degrees par seconde

        int PARAMETER_FOR_XY = 1;//l'unite de la dist. = millimetre, on n'accepte QUE l'entier
        int PARAMETER_FOR_THETA = 100;//l'unite de l'angle = millidegree, on accepte l'entree de la forme X.XX degrees

        public BaseRoulante(int socket)
            : base(socket)
        {
            this.kangaroo = new Kangaroo(socket);
            this.position = new PointOriente(0, 0, 0);
        }

        public enum MOVETYPES
        {
            GoTo = 1, AdjustAngle = 2, GoToAndAdjustAngle = 3
        };

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

        public Boolean GoToOrientedPoint(PointOriente pt)
        {           
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
            RotateAndSleep(deltaAngle);
        
            //Aller tout droite
            int distance = doubleToIntForXY(System.Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
            AvanceAndSleep(distance*sens);
       
            //Mise a jour la position
            position = new PointOriente(pt.x,pt.y,angle);
             
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
    }
}
