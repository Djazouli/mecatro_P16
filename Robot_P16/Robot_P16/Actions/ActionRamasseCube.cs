using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using Gadgeteer.Networking;
using GT = Gadgeteer;
using Robot_P16.Robot;
using System.IO.Ports;
using Robot_P16.Robot.composants.Servomoteurs;
using System.Threading;

namespace Robot_P16.Actions
{
    public class ActionRamasseCube : Action
    {
        private string codeCouleur;
        private GestionnaireServosPR gestionnaire;
        private CouleurEquipe couleurEquipe;

        public ActionRamasseCube(string CodeCouleur)
            : base("Ramasse des cubes")
        {
            this.codeCouleur = CodeCouleur;
            this.gestionnaire = new GestionnaireServosPR();
            this.couleurEquipe = Robot.Robot.robot.Couleur;
        }



        public int[] getSequence()
        {
            if (couleurEquipe== CouleurEquipe.VERT){
                Debug.Print("Vert" + codeCouleur);
                switch(this.codeCouleur){
                    case "J-N-B":
                    case "B-N-J":
                        return new int[] {3,14,5};
                    case "B-V-O":
                    case "O-V-B":
                        return new int[] {1,13,5};
                    case "V-O-J":
                    case "J-O-V":
                        return new int[] {1,13,5};
                    case "O-N-V":
                    case "V-N-O":
                        return new int[] {1,14,5};
                    case "V-J-B":
                    case "B-J-V":
                        return new int[] {2,13,5};
                    case "N-J-O":
                    case "O-J-N":
                        return new int[] {3,14,5};
                    case "B-O-N":
                    case "N-O-B":
                        return new int[] {4,11,5};
                    case "J-V-N":
                    case "N-V-J":
                        return new int[] {3,6,13};
                    case "N-B-V":
                    case "V-B-N":
                        return new int[] {4,12,5};
                    case "O-B-J":
                    case "J-B-O":
                        return new int[] {2,13,5};
                }
            }
               if (couleurEquipe==CouleurEquipe.ORANGE){
                   Debug.Print("Orange" + codeCouleur);
                       switch(this.codeCouleur){
                    case "J-N-B":
                    case "B-N-J":
                        return new int[] {4,12,5};
                    case "B-V-O":
                    case "O-V-B":
                        return new int[] {1,13,5};
                    case "V-O-J":
                    case "J-O-V":
                        return new int[] {1,13,5};
                    case "O-N-V":
                    case "V-N-O":
                        return new int[] {1,12,5}; // 1,2,5 ??
                    case "V-J-B":
                    case "B-J-V":
                        return new int[] {4,13,5};
                    case "N-J-O":
                    case "O-J-N":
                        return new int[] {3,12,5};
                    case "B-O-N":
                    case "N-O-B":
                        return new int[] {7,11,5};
                    case "J-V-N":
                    case "N-V-J":
                        return new int[] {3,6,3};
                    case "N-B-V":
                    case "V-B-N":
                        return new int[] {2,14,5};
                    case "O-B-J":
                    case "J-B-O":
                        return new int[] {4,13,5};
                    default:
                        return new int[] {4,2,5}; // C'est juste histoire de faire une pile
                    }
                 }
               else
               {
                   Debug.Print("Code couleur non valide");
                   return new int[] { 4, 2, 5 }; // histoire de faire une pile
               }
                    
               }
            
        

        private void makeMove(int numMove)
        {
            switch (numMove)
            {
                case 1: 
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Active ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true).Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1.Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30*100).Execute();
                    new ActionBuilder("Desactive ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false).Execute();
                    Thread.Sleep(200);
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    break;
                case 11: 
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Active ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true).Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1.Execute();
                    gestionnaire.PR_BRAS_DROIT_GOTO1000.Execute();
                    gestionnaire.PR_BRAS_DROIT_ZONE_BAS_VERS_HAUT.Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_3.Execute();
                    new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-58, 30 * 100).Execute();
                    new ActionBuilder("Desactive ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false).Execute();
                    Thread.Sleep(200);
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    gestionnaire.PR_BRAS_DROIT_GOTO0.Execute();
                    gestionnaire.PR_BRAS_DROIT_ZONE_HAUT_VERS_BAS.Execute();
                    break;
                case 2: // Supposons que le bras gauche est initialement au dessus du premier cube, non deployé (sinon on le pousse en arrivant)
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE.Execute();
                    new ActionBuilder("Active ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true).Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();// C'est en position relative j'espere que ca va
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Desactive ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false).Execute();
                    Thread.Sleep(300);
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU.Execute();
                    break;
                case 12:
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE.Execute();
                    new ActionBuilder("Active ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true).Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    //gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    //Thread.Sleep(200);
                    new ActionBuilder("Desactive ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false).Execute();
                    Thread.Sleep(200);
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    break;
                case 3:
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Active ventouse Gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true).Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Desactive Ventouse Gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false).Execute();
                    Thread.Sleep(300);
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU.Execute();
                    break;
                case 13:
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_CUBE_CENTRAL.Execute();
                    new ActionBuilder("Active ventouse Gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true).Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Desactive Ventouse Gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false).Execute();
                    Thread.Sleep(300);
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU.Execute();
                    break;
                case 4:
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    new ActionBuilder("Activer ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true).Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1.Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Desactiver ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false).Execute();
                    Thread.Sleep(200);
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    break;
                case 14:
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    new ActionBuilder("Activer ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true).Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1.Execute();
                    gestionnaire.PR_BRAS_DROIT_GOTO1000.Execute();
                    gestionnaire.PR_BRAS_DROIT_ZONE_BAS_VERS_HAUT.Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_3.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Desactiver ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false).Execute();
                    Thread.Sleep(300);
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    gestionnaire.PR_BRAS_DROIT_GOTO0.Execute();
                    gestionnaire.PR_BRAS_DROIT_ZONE_HAUT_VERS_BAS.Execute();
                    Thread.Sleep(200);

                    break;
                case 5:
                    gestionnaire.PR_POUSSOIRJOKER_POUSSER.Execute();
                    gestionnaire.PR_POUSSOIRJOKER_RETOUR.Execute();
                    break;
                case 6://peut etre qu'on devrait bloquer les cubes avant d'avancer ?
                    new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-58, 30 * 100).Execute();
                    break;
                case 7:
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE.Execute();
                    new ActionBuilder("Activer ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true).Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Activer ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false).Execute();
                    Thread.Sleep(300);
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU.Execute();
                    break;
                default:
                    Debug.Print("ALLO CE NEST PAS UN NUMERO VALIDE DE MAKEMOVE");
                    break;
            }
        }


        public override void Execute()
        {
            int[] sequence = getSequence();
            for(int i = 0; i<sequence.Length; i++){
                Debug.Print("Making move" + sequence[i].ToString());
                makeMove(sequence[i]);
            }
        }

        public override void Feedback(Action a)
        {
            throw new NotImplementedException();
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            throw new NotImplementedException();
        }
    }
}