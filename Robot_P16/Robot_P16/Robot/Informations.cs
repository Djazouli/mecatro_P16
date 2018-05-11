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
        public static DateTime startTime = DateTime.Now;
        static public Priority priorityLevel = Priority.MEDIUM; /// 0=LOW, 1=MEDIUM, 2=HIGH

        public static void printInformations(Priority priority, string message)
        {
            if (priority >= priorityLevel)
            {
                var endTime = DateTime.Now;
                Double elapsedMillisecs = ((TimeSpan)(endTime - startTime)).Milliseconds + ((TimeSpan)(endTime - startTime)).Seconds * 1000;
                Debug.Print(elapsedMillisecs+"ms : "+message);
            }
        }
    }


}
