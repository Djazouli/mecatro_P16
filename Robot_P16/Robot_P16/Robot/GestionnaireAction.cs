using System;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot.composants.Servomoteurs;

using Robot_P16.Map;
namespace Robot_P16.Robot
{
    public class GestionnaireAction
    {

        private static Hashtable ACTION_PER_TYPE = new Hashtable();

        public static void loadActions()
        {
            ACTION_PER_TYPE.Clear();

            StrategieGrosRobot strategieGR = new StrategieGrosRobot();
            setMotherAction(ModeOperatoire.COMPETITION, TypeRobot.GRAND_ROBOT, CouleurEquipe.VERT, strategieGR.GetMotherActionForCouleurVerte());

            StrategiePetitRobot strategiePR = new StrategiePetitRobot();
            setMotherAction(ModeOperatoire.COMPETITION, TypeRobot.PETIT_ROBOT, CouleurEquipe.VERT, strategiePR.GetMotherActionPRCompetitionForCouleurVerte());

            Informations.printInformations(Priority.HIGH, "actions chargees");
        }

        public static void startActions(ModeOperatoire mode, TypeRobot type, CouleurEquipe couleur)
        {
            Informations.printInformations(Priority.HIGH, "Starting actions with mod : " + mode.ToString() + " & type : " + type.ToString());
            if (ACTION_PER_TYPE.Contains(mode))
            {
                if (((Hashtable)ACTION_PER_TYPE[mode]).Contains(type))
                {
                    if (((Hashtable)((Hashtable)ACTION_PER_TYPE[mode])[type]).Contains(couleur))
                    {
                        Informations.printInformations(Priority.HIGH, "Lancement de l'action mère avec le mode " + mode.ToString() + " & type : " + type.ToString() +" & couleur "+couleur.ToString());
                        ((Action)((Hashtable)((Hashtable)ACTION_PER_TYPE[mode])[type])[couleur]).Execute();
                        return;
                    }
                    else
                    {
                        Informations.printInformations(Priority.HIGH, "MODE INTROUVABLE : Impossible de lancer l'action mère (introuvable) pour la couleur : " + couleur.ToString());
                    }
                }
                else
                {
                    Informations.printInformations(Priority.HIGH, "MODE INTROUVABLE : Impossible de lancer l'action mère (introuvable) pour le mode : " + mode.ToString());
                }
            }
            else
            {
                Informations.printInformations(Priority.HIGH, "TYPE INTROUVABLE : Impossible de lancer l'action mère (introuvable) pour le type : " + type.ToString());
            }
        }


        private static void setMotherAction(ModeOperatoire mode, TypeRobot type, CouleurEquipe couleur,  Action a)
        {
            if (!ACTION_PER_TYPE.Contains(mode))
            {
                ACTION_PER_TYPE.Add(mode, new Hashtable());
            }
            
            if (((Hashtable)ACTION_PER_TYPE[mode]).Contains(type))
            {
                ((Hashtable)ACTION_PER_TYPE[mode])[type] = a;
            }
            else
            {
                ((Hashtable)ACTION_PER_TYPE[mode]).Add(type, a);
            }

            if (((Hashtable)((Hashtable)ACTION_PER_TYPE[mode])[type]).Contains(couleur))
            {
                ((Hashtable)((Hashtable)ACTION_PER_TYPE[mode])[type])[couleur] = a;
            }
            else
            {
                ((Hashtable)((Hashtable)ACTION_PER_TYPE[mode])[type]).Add(couleur, a);
            }
            Informations.printInformations(Priority.MEDIUM, "actions mere set pour le type "+type+" & le mode "+mode+" & couleur "+couleur);
        }


    }
}
