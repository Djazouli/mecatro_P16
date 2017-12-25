using System;
using Microsoft.SPOT;

using Robot_P16.Map;
namespace Robot_P16.Robot
{

    /// <summary>
    /// Delegate de base pour écouter des positions
    /// </summary>
    /// <param name="position">Position du robot</param>
    public delegate void PositionDelegateListener(PointOriente position);

    public class GestionnairePosition
    {

        /// <summary>
        /// Evenement à écouter pour obtenir la position du robot à interval régulier
        /// </summary>
        public static event PositionDelegateListener GetRobotPositionEvent;

        public static void sendPositionInformation()
        {
            if(GetRobotPositionEvent != null) {
                GetRobotPositionEvent(getPosition());
            }
        }

        public static PointOriente getPosition()
        {
            return null;
        }

    }
}
