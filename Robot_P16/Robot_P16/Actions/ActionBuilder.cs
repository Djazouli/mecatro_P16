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
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionBuilder : creation nouvel ActionBuilder avec description" + description);
        }
        public ActionBuilder() : this(null) { Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.Action : creation nouvel ActionBuilder"); }

        public ActionBuilder Add(Action a)
        {
            liste.Add(a);
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.MEDIUM, "Actions.ActionBuilder.Add : ajout d une action a l actionbuilder ");
            return this;
        }

        public ActionEnSerie BuildActionEnSerie()
        {
            Action[] listeActions = new Action[liste.Count];
            int i = 0;
            foreach (Object o in liste)
                listeActions[i++] = (Action)o;
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionBuilder.BuildActionEnSerie : construction d une liste d actions en serie avec la description " + description);
            return new ActionEnSerie(listeActions, description);
        }
        public ActionEnParallele BuildActionEnParallele()
        {
            Action[] listeActions = new Action[liste.Count];
            int i = 0;
            foreach (Object o in liste)
                listeActions[i++] = (Action)o;
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionBuilder.BuildActionEnParallele : construction d une liste d actions en parallele avec la description " + description);
            return new ActionEnParallele(listeActions, description);
        }

        public ActionWait BuildActionWait(int duration)
        {
            String t = duration.ToString();
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionBuilder.BuildActionWait : construction d une action d'attente de temps " + t + " avec la description " + description);
            return new ActionWait(description, duration);
        }

        public ActionRepeated BuildActionUniqueRepeated(Action a, int compteur)
        {
            String t = compteur.ToString();
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionBuilder.BuildActionUniqueRepeated : construction d une repetition d action unique " + t + " fois avec la description " + description);
            return new ActionRepeated(description, a, compteur);
        }

        public ActionRepeated BuildActionEnSerieRepeated(int compteur)
        {
            Robot.Informations.printInformations(Robot_P16.Robot.Priority.LOW, "Actions.ActionBuilder.BuildActionEnSerieRepeated : construction d une repetition d actions en serie");
            return new ActionRepeated(description + " - mère", this.BuildActionEnSerie(), compteur);
        }



        public ActionGetPosition BuildActionGetPosition()
        {

            return new ActionGetPosition(description);
        }

        public ActionServo BuildActionServo(AX12 servomoteur, ServoCommandTypes commandType, float angle)
        {
            return new  ActionServo(description, servomoteur, commandType, angle);
        }

        public ActionDelegate BuildActionDelegate(VoidFunc method) {
            return new ActionDelegate(description, method);
        }

    }
}
