using System;
using System.Collections;
using Microsoft.SPOT;

namespace Robot_P16.Robot.Composants.Servomoteurs
{

    public enum AnglesServos {
        GR_ANGLE_CLAPET_1
    }

    public class DonneesServo
    {
        private static Hashtable ANGLES = new Hashtable();

        public static void LoadAllData() {

        }

        public static void AddAngle(AnglesServos typeAngle, int angle) {
            ANGLES.Add(typeAngle, angle);
        }
        public static int GetAngle(AnglesServos typeAngle) {
            return (int)ANGLES[typeAngle];
        }
    }
}
