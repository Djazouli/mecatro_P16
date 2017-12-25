using System;
using Microsoft.SPOT;

using Robot_P16.Map;
namespace Robot_P16.Robot
{

    /// <summary>
    /// Delegate de base pour �couter des positions
    /// </summary>
    /// <param name="position">Position du robot</param>
    public delegate void PositionDelegateListener(PointOriente position);

    public class GestionnairePosition
    {

        /// <summary>
        /// Evenement � �couter pour obtenir la position du robot � interval r�gulier
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
