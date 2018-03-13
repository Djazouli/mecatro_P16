using System;
using Microsoft.SPOT;

namespace Robot_P16.Robot
{
    public enum Priority
    {
        LOW,
        MEDIUM,
        HIGH,
    }
    public class Informations
    {
        static public Priority priorityLevel = Priority.HIGH; /// 0=LOW, 1=MEDIUM, 2=HIGH

        public static void printInformations(Priority priority, string message)
        {
            if (priority >= priorityLevel)
            {
                Debug.Print(message);
            }
        }
    }
}
