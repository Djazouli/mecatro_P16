using System;
using Microsoft.SPOT;

namespace Robot_P16.Robot
{
    public enum Priority
    {
        VERY_LOW,
        LOW,
        MEDIUM,
        HIGH,
    }



    public enum OBSTACLE_DIRECTION
    {
        AVANT,
        ARRIERE
    }
    public class Informations
    {
        static public Priority priorityLevel = Priority.VERY_LOW; /// 0=LOW, 1=MEDIUM, 2=HIGH

        public static void printInformations(Priority priority, string message)
        {
            if (priority >= priorityLevel)
            {
                Debug.Print(message);
            }
        }
    }


}
