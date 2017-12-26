using System;
using Microsoft.SPOT;

namespace Robot_P16.Map.Surface
{
    public class Rectangle : ElementSurface
    {
        private double l_Y;
        private double l_X;

        /// <summary>
        /// Constructeur d'une surface rectangulaire orientée par l'angle du point central.
        /// </summary>
        /// <param name="centre">Centre du rectangle</param>
        /// <param name="deltaX">Taille par rapport à l'axe X : [centre.x +- deltaX]</param>
        /// <param name="deltaY">Taille par rapport à l'axe Y : [centre.y +- deltaY]</param>
        public Rectangle(PointOriente centre, double deltaX, double deltaY)
        {
            origine = centre.Clone();
            l_Y = System.Math.Abs(deltaY);
            l_X = System.Math.Abs(deltaX);
        }

        public override bool Appartient(PointOriente p)
        {
            double rel_x = p.x - origine.x;
            double rel_y = p.y - origine.y;
            double proj_rel_1 = System.Math.Cos(origine.theta) * rel_x + System.Math.Sin(origine.theta) * rel_y;
            double proj_rel_2 = -System.Math.Sin(origine.theta) * rel_x + System.Math.Cos(origine.theta) * rel_y;


            return (-l_X <= proj_rel_1 && proj_rel_1 <= l_X) && (-l_Y <= proj_rel_2 && proj_rel_2 <= l_Y);
        }


        public override ElementSurface clone()
        {
            return new Rectangle(origine, l_Y, l_X);
        }
    }
}
