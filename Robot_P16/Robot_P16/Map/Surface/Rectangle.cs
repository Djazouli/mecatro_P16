using System;
using Microsoft.SPOT;

namespace Robot_P16.Map.Surface
{
    class Rectangle : ElementSurface
    {
        private double largeur;
        private double longueur;

        public Rectangle(PointOriente p, double la, double lo)
        {
            origine = p;
            largeur = la;
            longueur = lo;
        }

        public override bool Appartient(PointOriente p)
        {
            double rel_x = p.x - origine.x;
            double rel_y = p.y - origine.y;
            double proj_rel_1 = System.Math.Cos(origine.theta) * rel_x + System.Math.Sin(origine.theta) * rel_y;
            double proj_rel_2 = -System.Math.Sin(origine.theta) * rel_x + System.Math.Cos(origine.theta) * rel_y;

            return (0 <= proj_rel_1 && proj_rel_1 <= longueur) && (-largeur / 2 <= proj_rel_2 && proj_rel_2 <= largeur / 2);
        }


        public override ElementSurface clone()
        {
            return new Rectangle(origine, largeur, longueur);
        }
    }
}
