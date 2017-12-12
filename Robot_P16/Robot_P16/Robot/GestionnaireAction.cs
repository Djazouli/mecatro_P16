using System;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Actions;

namespace Robot_P16.Robot
{
    class GestionnaireAction
    {

        private static Hashtable ACTION_PER_TYPE = new Hashtable();

        public static void loadActions()
        {
            ACTION_PER_TYPE.Clear();
            loadActionHomologation();
        }

        private static void loadActionHomologation()
        {
            // Blablabla
        }

        private static void setMotherAction(ModeOperatoire mode, Action a)
        {
            if (ACTION_PER_TYPE.Contains(mode))
            {
                ACTION_PER_TYPE[mode] = a;
            }
            else
            {
                ACTION_PER_TYPE.Add(mode, a);
            }
        }


    }
}
