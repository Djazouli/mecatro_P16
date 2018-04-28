using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using Gadgeteer.Networking;
using GT = Gadgeteer;
using Robot_P16.Robot;
using System.IO.Ports;
using Robot_P16.Robot.composants.Servomoteurs;

namespace Robot_P16.Actions
{
    public class ActionRamasseCube : Action
    {
        private string codeCouleur;
        public readonly GestionnaireServosPR gestionnaire;
        private CouleurEquipe couleurEquipe;
        public ActionRamasseCube(string CodeCouleur)
            : base("Ramasse des cubes")
        {
            this.codeCouleur = CodeCouleur;
            this.gestionnaire = new GestionnaireServosPR();
        }



        public int[] getSequence()
        {
            if (couleurEquipe.Equals(CouleurEquipe.VERT)){
                switch(this.codeCouleur){
                    case "J-N-B":
                        return new int[] {3,14,5};
                    case "B-V-O":
                        return new int[] {1,13,5};
                    case "V-O-J":
                        return new int[] {1,13,5};
                    case "O-N-V":
                        return new int[] {1,4,5};
                    case "V-J-B":
                        return new int[] {2,13,5};
                    case "N-J-O":
                        return new int[] {3,14,5};
                    case "B-O-N":
                        return new int[] {4,11,5};
                    case "J-V-N":
                        return new int[] {3,6,13};
                    case "N-B-V":
                        return new int[] {4,12,5};
                    case "O-B-J":
                        return new int[] {2,13,5};
                }
            }
               if (couleurEquipe.Equals(CouleurEquipe.ORANGE)){
                       switch(this.codeCouleur){
                    case "J-N-B":
                        return new int[] {4,2,5};
                    case "B-V-O":
                        return new int[] {1,13,5};
                    case "V-O-J":
                        return new int[] {1,13,5};
                    case "O-N-V":
                        return new int[] {1,2,5};
                    case "V-J-B":
                        return new int[] {4,13,5};
                    case "N-J-O":
                        return new int[] {3,12,5};
                    case "B-O-N":
                        return new int[] {7,11,5};
                    case "J-V-N":
                        return new int[] {3,6,3};
                    case "N-B-V":
                        return new int[] {2,14,5};
                    case "O-B-J":
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
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute(); // on monte de 2 pour etre large
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute(); // on colle bien la ventouse
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    new ActionBaseRoulante("Avancer de 1 cube", null).Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute(); // faire tomber le cube
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute(); // Retour à la position intiale
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    break;
                case 11: 
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute(); // on monte de 2 pour etre large
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute(); // on colle bien la ventouse
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    new ActionBaseRoulante("Avancer de 1 cube", null).Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute(); // faire tomber le cube
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute(); // Retour à la position intiale
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    break;
                case 2:
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    break;
                case 12:
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    break;
                case 3:
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_DEPLOIEMENT_SORTIR.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_DEPLOIEMENT_RENTRER.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    break;
                case 13:
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_DEPLOIEMENT_SORTIR.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_DEPLOIEMENT_RENTRER.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    break;
                case 4:
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute();
                    break;
                case 14:
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute();
                    break;
                case 5:
                    gestionnaire.PR_POUSSOIRJOKER_POUSSER.Execute();
                    gestionnaire.PR_POUSSOIRJOKER_RETOUR.Execute();
                    break;
                case 6:
                    new ActionBaseRoulante("Avancer de 1 cube", null).Execute();
                    break;
                case 7:
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEDROITE.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_MONTER.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_AIGUILLAGE_VENTOUSEGAUCHE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATIONANTIHORAIRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_DROIT_DESCENDRE.Execute();
                    break;
                default:
                    Debug.Print("ALLO CE NEST PAS UN NUMERO VALIDE DE MAKEMOVE");
                    break;
            }
        }


        private override void execute()
        {
            int[] sequence = getSequence();
            for(int i = 0; i<sequence.Length; i++){
                makeMove(sequence[i]);
            }
        }
    }
}