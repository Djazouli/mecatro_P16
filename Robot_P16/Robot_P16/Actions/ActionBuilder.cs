using System;
using System.Collections;
using Microsoft.SPOT;

namespace Robot_P16.Actions
{
    public class ActionBuilder
    {
        public ArrayList liste = new ArrayList();
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
    }
}
