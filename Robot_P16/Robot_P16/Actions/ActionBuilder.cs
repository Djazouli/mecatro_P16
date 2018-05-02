using System;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Robot.composants.Servomoteurs;

namespace Robot_P16.Actions
{
    public class ActionBuilder
    {
        private ArrayList liste = new ArrayList();
        public String description;

        public ActionBuilder(String description)
        {
            this.description = description;
        }
        public ActionBuilder() : this(null) { }

        public ActionBuilder Add(Action a)
        {
            liste.Add(a);
            return this;
        }

        public ActionEnSerie BuildActionEnSerie()
        {
            Action[] listeActions = new Action[liste.Count];
            int i = 0;
            foreach (Object o in liste)
                listeActions[i++] = (Action)o;
            return new ActionEnSerie(listeActions, description);
        }
        public ActionEnParallele BuildActionEnParallele()
        {
            Action[] listeActions = new Action[liste.Count];
            int i = 0;
            foreach (Object o in liste)
                listeActions[i++] = (Action)o;
            return new ActionEnParallele(listeActions, description);
        }

        public ActionWait BuildActionWait(int duration)
        {
            return new ActionWait(description, duration);
        }

        public ActionRepeated BuildActionUniqueRepeated(Action a, int compteur)
        {
            return new ActionRepeated(description, a, compteur);
        }

        public ActionRepeated BuildActionEnSerieRepeated(int compteur)
        {
            return new ActionRepeated(description + " - serieRepeated "+compteur, this.BuildActionEnSerie(), compteur);
        }


        public ActionGetPosition BuildActionGetPosition()
        {
            return new ActionGetPosition(description);
        }

        public ActionServo BuildActionServo(AX12 servomoteur, ServoCommandTypes commandType, float angle)
        {
            return new  ActionServo(description, servomoteur, commandType, angle);
        }

        public ActionServoAbsolue BuildActionServoAbsolue(AX12 servomoteur, int angle)
        {
            return new ActionServoAbsolue(description, servomoteur, angle);
        }

        public ActionServoRotation BuildActionServoRotation(AX12 servomoteur, speed direction, int duration)
        {
            return new ActionServoRotation(description, servomoteur, direction, duration);
        }
        public ActionVentouze BuildActionVentouze(VENTOUZES ventouze, bool activate)
        {
            return new ActionVentouze(description, ventouze, activate);
        }

        public ActionDelegate BuildActionDelegate(VoidFunc method) {
            return new ActionDelegate(description, method);
        }
    }
}
