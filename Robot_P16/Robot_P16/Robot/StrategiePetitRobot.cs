using System;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot.composants.Servomoteurs;
using Robot_P16.Robot.composants.BaseRoulante;

namespace Robot_P16.Robot
{
    public class StrategiePetitRobot {


        public void loadActionsPetitRobot()
        {
            
            GestionnaireServosPR gestionnaireServo = null;

            Action PR_MOUVEMENT_4 =   new ActionBuilder("empiler le cube de droite hauteur 1").Add(
                //mettre la position de base à bras sorti
                                gestionnaireServo.PR_AIGUILLAGE_VENTOUSEDROITE).Add(
                //action qui lance la pompe 
                                gestionnaireServo.PR_BRAS_DROIT_MONTER).Add(
                                gestionnaireServo.PR_BRAS_DROIT_ROTATIONANTIHORAIRE).Add(
                //action qui arrête la pompe
                                gestionnaireServo.PR_BRAS_DROIT_ROTATIONHORAIRE).Add(
                                gestionnaireServo.PR_BRAS_DROIT_DESCENDRE
                      ).BuildActionEnSerie();

            Action PR_MOUVEMENT_2_BIS = new ActionBuilder("empiler cube de gauche hauteur 2").Add(
                                gestionnaireServo.PR_AIGUILLAGE_VENTOUSEGAUCHE).Add(
                                 //action qui lance la pompe 
                                gestionnaireServo.PR_BRAS_GAUCHE_MONTER).Add(
                                gestionnaireServo.PR_BRAS_GAUCHE_MONTER).Add(
                                gestionnaireServo.PR_BRAS_GAUCHE_ROTATIONHORAIRE).Add(
                                   //action qui arrête la pompe
                                gestionnaireServo.PR_BRAS_DROIT_ROTATIONANTIHORAIRE).Add(
                                gestionnaireServo.PR_BRAS_DROIT_DESCENDRE).Add(
                                gestionnaireServo.PR_BRAS_DROIT_DESCENDRE
                      ).BuildActionEnSerie();

            Action MOTHER_ACTION = new ActionBuilder("Action mère Test1").Add(
                    new ActionBuilder("Action en série pipot").Add(
                        new Actions.ActionsIHM.ActionBouton(Robot.robot.TR1_BOUTON_1)
                    ).BuildActionEnSerie()
               )
               .Add(
                   new ActionBuilder("Allumer LED").BuildActionDelegate(Robot.robot.TR1_BOUTON_1.TurnLedOn)
               ).Add(
                   new ActionBuilder("Wait a bit...").BuildActionWait(2000)
               )
               .Add(
                   new ActionBuilder("Eteindre LED").BuildActionDelegate(Robot.robot.TR1_BOUTON_1.TurnLedOff)
              )
                /*   .Add(
                        new ActionBuilder("allerAuCodeCouleur").
                     ) */
           .Add(new ActionBuilder("Creer la première pile de cube").Add( // cas équipe orange combinaison J-N-B
                     PR_MOUVEMENT_4
                      ).Add(
                      PR_MOUVEMENT_2_BIS
                      ).Add(
                       new ActionBuilder("Mouvement 5").Add(
                                gestionnaireServo. PR_POUSSOIRJOKER_POUSSER).Add(
                                gestionnaireServo. PR_POUSSOIRJOKER_RETOUR                     
                       ).BuildActionEnSerie()
                       ).BuildActionEnSerie()
                      

                    /*  .Add( new ActionBuilder ("Poser la pile 1")).Add(
                                
                      
                      )*/

                        ).BuildActionEnSerie();
                                   
              

               
        }
    }
}
