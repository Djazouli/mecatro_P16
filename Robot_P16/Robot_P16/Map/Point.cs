using System;
using Microsoft.SPOT;

namespace Robot_P16.Map
{
    class PointOriente
    {
        public double x;
        public double y;
        public double theta;

        public PointOriente(double x, double y, double theta)
        {
            this.x = x;
            this.y = y;
            this.theta = theta;
        }
    }
}
