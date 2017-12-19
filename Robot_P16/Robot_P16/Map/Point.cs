using System;
using Microsoft.SPOT;

namespace Robot_P16.Map
{
    class PointOriente
    {
        public readonly double x;
        public readonly double y;
        public readonly double theta;

        public PointOriente(double x, double y, double theta)
        {
            this.x = x;
            this.y = y;
            this.theta = theta;
        }
        public PointOriente(double x, double y) : this(x,y,0) {}

        public PointOriente translater(double x, double y, double theta)
        {
            return new PointOriente(this.x + x, this.y + y, this.theta + theta);
        }

        public PointOriente translater(PointOriente pt)
        {
            return translater(pt.x, pt.y, pt.theta);
        }

        public double distanceSquared(PointOriente pt)
        {
            return (pt.x - this.x) * (pt.x - this.x) + (pt.y - this.y) * (pt.y - this.y);
        }
    }
}
