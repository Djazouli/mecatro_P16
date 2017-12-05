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


    class ElementSurface
    {
        class Rectangle
        {
            private PointOriente origine; // origine du rectangle
            private double largeur;
            private double longueur;

            public Rectangle(PointOriente p, double la, double lo)
            {
                origine = p;
                largeur = la;
                longueur = lo;
            }

            public bool Appartient(PointOriente p)
            {
                rel_x = p.x - origine.x;
                rel_y = p.y - origine.y;
                proj_rel_1 = Math.Cos(origine.theta) * rel_x + Math.Sin(origine.theta) * rel_y;
                proj_rel_2 = -Math.Sin(origine.theta) * rel_x + Math.Cos(origine.theta) * rel_y;
                if ((0 <= proj_rel_1 && proj_rel_1 <= longueur) && (-largeur/2 <= proj_rel_2 && proj_rel_2 <= largeur/2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }
    }



}
