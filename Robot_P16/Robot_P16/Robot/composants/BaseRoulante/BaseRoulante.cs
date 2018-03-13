using System;
using Microsoft.SPOT;

using Robot_P16.Map;

namespace Robot_P16.Robot.composants.BaseRoulante
{
    class BaseRoulante
    {


        public Boolean GoToOrientedPoint(PointOriente pt) // ajuster position X,Y, mais pas theta => mode drive
        {
            return false;
        }

        public Boolean AdjustAngleToPoint(PointOriente pt) // ajuste theta, mais pas X,Y => mode turn
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
