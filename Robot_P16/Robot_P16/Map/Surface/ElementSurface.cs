using System;
using Microsoft.SPOT;

namespace Robot_P16.Map.Surface
{
    abstract class ElementSurface
    {
        public abstract bool Appartient(PointOriente p);
    }
}
