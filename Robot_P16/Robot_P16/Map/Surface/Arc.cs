using System;
using Microsoft.SPOT;

namespace Robot_P16.Map.Surface
{
    public class Arc : ElementSurface
    {
        
        private double distance;
        private double largeur;
        private double angle;

        public Arc(PointOriente p, double l, double d, double a)
        {
            origine = p;
            largeur = l;
            distance = d;
            angle = a;
        }

        public override bool Appartient(PointOriente p)
        {
            double rel_x = p.x - origine.x;
            double rel_y = p.y - origine.y;

            if (rel_x == 0 && rel_y == 0)
            {
                return distance == 0;
            }
            else
            {
                bool horsCercle1 = (rel_x * rel_x + rel_y * rel_y) >= distance;
                bool dansCercle2 = (rel_x * rel_x + rel_y * rel_y) <= distance + largeur;
                bool dansCadrant = (System.Math.Atan(rel_y / rel_x) >= p.theta && System.Math.Atan(rel_y / rel_x) <= p.theta + angle);
                return horsCercle1 && dansCercle2 && dansCadrant;
            }
        }

        public override ElementSurface clone()
        {
            return new Arc(origine, largeur, distance, angle);
        }
    }
}
