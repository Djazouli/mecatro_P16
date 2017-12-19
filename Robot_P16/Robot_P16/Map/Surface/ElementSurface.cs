using System;
using Microsoft.SPOT;

namespace Robot_P16.Map.Surface
{
    abstract class ElementSurface
    {
        protected PointOriente origine; // centre de la surface

        public abstract bool Appartient(PointOriente p);

        /// <summary>
        /// Modifie en place
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="theta"></param>
        public void translater(double x, double y, double theta)
        {
            origine = origine.translater(x, y, theta);
        }
        /// <summary>
        /// Modifie en place
        /// </summary>
        /// <param name="pt"></param>
        public void translater(PointOriente pt)
        {
            origine = origine.translater(pt);
        }

        public abstract ElementSurface clone();

    }
}
