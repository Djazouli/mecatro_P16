using System;
using Microsoft.SPOT;
using Robot_P16.Map.Surface;

namespace Robot_P16.Map
{
    /// <summary>
    /// TypeObstacle est une enum qui caractérise les différents obstacles.
    /// </summary>
    public enum TypeObstacle
    {
        ROBOT_ADVERSE,
        EMPILEMENT_CUBE,
        ROBOT_ALLIE_GRAND,
        ROBOT_ALLIE_PETIT
    }

    public class Obstacle
    {
        private ElementSurface surfaceInterdite;
        private TypeObstacle typeObstacle;

        public Obstacle(ElementSurface surface, TypeObstacle type)
        {
            surfaceInterdite = surface;
            typeObstacle = type;
        }

    }
}