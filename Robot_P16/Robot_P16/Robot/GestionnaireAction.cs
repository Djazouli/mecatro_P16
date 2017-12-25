using System;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Actions;

namespace Robot_P16.Robot
{
    public class GestionnaireAction
    {

        private static Hashtable ACTION_PER_TYPE = new Hashtable();

        private static readonly int PERIOD_SEND_POSITION = 1000; // En ms
        private static readonly Action ACTION_SEND_POSITION =
            new ActionBuilder("Action send Position m�re").Add(
                    new ActionBuilder("Action send position - Action get position").BuildActionGetPosition()
                )
                .Add(
                    new ActionBuilder("Action send position - P�riode du signal").BuildActionWait(PERIOD_SEND_POSITION)
                ).BuildActionEnSerieRepeated(-1); // Envois infinis

        public static void loadActions()
        {
            ACTION_PER_TYPE.Clear();
            loadActionHomologation();
        }

        public static void startActions(ModeOperatoire mode)
        {
            if (ACTION_PER_TYPE.Contains(mode))
            {
                Debug.Print("Lancement de l'action m�re avec le mode " + mode.ToString());
                ((Action)ACTION_PER_TYPE[mode]).Execute();
            }
            else
            {
                Debug.Print("Impossible de lancer l'action m�re (introuvable) pour le mode " + mode.ToString());
            }
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
