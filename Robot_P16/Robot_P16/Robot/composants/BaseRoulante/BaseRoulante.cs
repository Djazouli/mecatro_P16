using System;
using Microsoft.SPOT;

using Robot_P16.Map;
using Robot_P16.Robot.Composants.BaseRoulante;
using System.Threading;


namespace Robot_P16.Robot.composants.BaseRoulante
{
    class BaseRoulante
    {
        private PointOriente position;
        public Kangaroo kangaroo;
        public int speedDrive;
        public int speedTurn;
        int PARAMETER_FOR_XY = 100;
        int PARAMETER_FOR_THETA = 100;


        enum TYPESMOVE
        {
            GoTo = 1, AdjustAngle = 2, GotoAndAdjustAngle = 3
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
        }

        private void AvanceAndSleep(int distance)
        {
            kangaroo.allerEn(distance, speedDrive);
            Thread.Sleep(System.Math.Abs(distance / speedDrive * 1000));
        }

        //TODO: to be continued...
        public Boolean GoToOrientedPoint(PointOriente pt)
        {
            PointOriente currentPosition = getPosition();
            
            //Calculer l'angle a tourner(TODO: a optimiser) 
            double deltaX = pt.x - currentPosition.x;
            double deltaY = pt.y - currentPosition.y;
            double result = deltaY / deltaX;
            double radians = System.Math.Atan(result);
            double angle = radians * (180 / System.Math.PI);
            int deltaAngle = doubleToIntForTheta(angle - currentPosition.theta);
            
            //Faire orienter a la destination
            RotateAndSleep(deltaAngle);
            //kangaroo.tourner(deltaAngle, speedTurn);
            //Thread.Sleep(System.Math.Abs(deltaAngle / speedTurn * 1000));

            //Aller tout droite
            int distance = doubleToIntForXY(System.Math.Sqrt(pt.distanceSquared(currentPosition)));
            AvanceAndSleep(distance);
            //kangaroo.allerEn(distance, speedDrive);
            //Thread.Sleep(System.Math.Abs(distance / speedDrive * 1000));

            //Mise a jour la position
            setPosition(pt);
             
            return false;
        }

        [Obsolete]
        public Boolean GoToOrientedPoint(PointOriente pt) // ajuster position X,Y, mais pas theta => mode drive
        {
            PointOriente currentPosition = kangaroo.getPosition();            

            int originTheta = doubleToIntForTheta(currentPosition.theta);
            kangaroo.tourner(-originTheta,speedTurn);
            Thread.Sleep(System.Math.Abs(originTheta / speedTurn * 1000));

            int deltaX = doubleToIntForXY(pt.x - currentPosition.x);
            int deltaY = doubleToIntForXY(pt.y - currentPosition.y);

            kangaroo.allerEn(deltaX,speedDrive);
            Thread.Sleep(System.Math.Abs(deltaX / speedDrive) * 1000);

            kangaroo.tourner(doubleToIntForTheta(90),speedTurn);
            Thread.Sleep(System.Math.Abs(doubleToIntForTheta(90) / speedTurn * 1000));

            kangaroo.allerEn(deltaY, speedDrive);
            Thread.Sleep(System.Math.Abs(deltaY / speedDrive) * 1000);

            kangaroo.tourner(originTheta, speedTurn);
            Thread.Sleep(System.Math.Abs(originTheta / speedTurn * 1000)); 
          
            //TODO:update kangaroo.pointOriente
            PointOriente newPt = pt.translater(0, 0, currentPosition.theta);//theta = 0 -> theta de depart
            kangaroo.setPosition(newPt);  

            return false;
        }

        public Boolean AdjustAngleToPoint(PointOriente pt) // ajuste theta, mais pas X,Y => mode turn
        {
            //Calculer l'angle a tourner
            Double currentAngle = getPosition().theta;//[-180,180]
            Double Angle = pt.theta;//[-180,180]
            int deltaAngle = doubleToIntForTheta((Angle - currentAngle));//[-360,360]
            if (deltaAngle > 180) deltaAngle -= 360;
            else if (deltaAngle < -180) deltaAngle += 360;
            RotateAndSleep(deltaAngle);         
            
            return false;
        }

        public Boolean GoToAndAdjustAngleToPoint(PointOriente pt)
        {
            return false;
        }
        public Boolean GoToLieuCle(LieuCle lieu)
        {
            PointOriente positionDuRobotApresDeplacement = null;
            return lieu.IsAtTheRightPlace(positionDuRobotApresDeplacement);
        }

        public Boolean AdjustAngleToLieuCle(LieuCle lieu)
        {
            PointOriente positionDuRobotApresDeplacement = null;
            return lieu.IsAtTheRightAngle(positionDuRobotApresDeplacement);
        }

        public Boolean GoAndAdjustAngleToLieuCle(LieuCle lieu)
        {
            GoToLieuCle(lieu);
            AdjustAngleToLieuCle(lieu);
            PointOriente positionDuRobotApresDeplacement = null;
            return lieu.IsAtTheRightPlaceAndAngle(positionDuRobotApresDeplacement);
        }

    }
}
