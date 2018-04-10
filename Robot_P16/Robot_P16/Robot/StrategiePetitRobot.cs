using System;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot.composants.Servomoteurs;
using Robot_P16.Robot.composants.BaseRoulante;

namespace Robot_P16.Robot
{
    public class StrategiePetitRobot {


        public static Action loadActionsPetitRobot()
        {
            
            GestionnaireServosPR gestionnaireServo = new GestionnaireServosPR();

            //mettre la position de base à bras sorti
            // ventouse aiguillee sur le bras droit en position de base

            Action PR_MOUVEMENT_1 = new ActionBuilder("empiler le cube de base sur le cube du milieu").Add(
                gestionnaireServo.PR_BRAS_DROIT_ROTATIONANTIHORAIRE).Add(
                //lancer la pompe
                gestionnaireServo.PR_BRAS_DROIT_MONTER).Add(
                // robot avance d'un cran
                // eteindre la pompe
                gestionnaireServo.PR_BRAS_DROIT_ROTATIONHORAIRE).Add(
                gestionnaireServo.PR_BRAS_DROIT_DESCENDRE
                ).BuildActionEnSerie();

            Action PR_MOUVEMENT_1_BIS = new ActionBuilder("empiler le cube de base sur les cubes du milieu hauteur 2").Add(
             gestionnaireServo.PR_BRAS_DROIT_ROTATIONANTIHORAIRE).Add(
                //lancer la pompe
             gestionnaireServo.PR_BRAS_DROIT_MONTER).Add(
             gestionnaireServo.PR_BRAS_DROIT_MONTER).Add(
                // robot avance d'un cran
                // eteindre la pompe
             gestionnaireServo.PR_BRAS_DROIT_ROTATIONHORAIRE).Add(
             gestionnaireServo.PR_BRAS_DROIT_DESCENDRE).Add(
             gestionnaireServo.PR_BRAS_DROIT_DESCENDRE
             ).BuildActionEnSerie();

            Action PR_MOUVEMENT_2 = new ActionBuilder("empiler cube de gauche hauteur 1").Add(
                               gestionnaireServo.PR_AIGUILLAGE_VENTOUSEGAUCHE).Add(
                //action qui lance la pompe 
                               gestionnaireServo.PR_BRAS_GAUCHE_MONTER).Add(
                               gestionnaireServo.PR_BRAS_GAUCHE_ROTATIONHORAIRE).Add(
                //action qui arrête la pompe
                               gestionnaireServo.PR_BRAS_DROIT_ROTATIONANTIHORAIRE).Add(
                               gestionnaireServo.PR_BRAS_DROIT_DESCENDRE).Add(
                               gestionnaireServo.PR_AIGUILLAGE_VENTOUSEDROITE
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
                               gestionnaireServo.PR_BRAS_DROIT_DESCENDRE).Add(
                               gestionnaireServo.PR_AIGUILLAGE_VENTOUSEDROITE
                     ).BuildActionEnSerie();

            Action PR_MOUVEMENT_3 = new ActionBuilder("placer le cube du milieu sur la pile").Add(
                gestionnaireServo.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_ROTATIONHORAIRE).Add(
                gestionnaireServo.PR_AIGUILLAGE_VENTOUSEGAUCHE).Add(
                //lancer la pompe
                gestionnaireServo.PR_BRAS_GAUCHE_MONTER).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER).Add(
                //arreter la pompe
                gestionnaireServo.PR_BRAS_GAUCHE_ROTATIONANTIHORAIRE).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_DESCENDRE).Add(
                gestionnaireServo.PR_AIGUILLAGE_VENTOUSEDROITE
                ).BuildActionEnSerie();

            Action PR_MOUVEMENT_3_BIS = new ActionBuilder("placer le cube du milieu sur la pile hauteur 2").Add(
                gestionnaireServo.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_ROTATIONHORAIRE).Add(
                gestionnaireServo.PR_AIGUILLAGE_VENTOUSEGAUCHE).Add(
                //lancer la pompe
                gestionnaireServo.PR_BRAS_GAUCHE_MONTER).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_MONTER).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER).Add(
                //arreter la pompe
                gestionnaireServo.PR_BRAS_GAUCHE_ROTATIONANTIHORAIRE).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_DESCENDRE).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_DESCENDRE).Add(
                gestionnaireServo.PR_AIGUILLAGE_VENTOUSEDROITE
                ).BuildActionEnSerie();

            Action PR_MOUVEMENT_4 =   new ActionBuilder("empiler le cube de droite hauteur 1").Add(
                //action qui lance la pompe 
                                gestionnaireServo.PR_BRAS_DROIT_MONTER).Add(
                                gestionnaireServo.PR_BRAS_DROIT_ROTATIONANTIHORAIRE).Add(
                //action qui arrête la pompe
                                gestionnaireServo.PR_BRAS_DROIT_ROTATIONHORAIRE).Add(
                                gestionnaireServo.PR_BRAS_DROIT_DESCENDRE
                      ).BuildActionEnSerie();

            Action PR_MOUVEMENT_4_BIS = new ActionBuilder("empiler le cube de droite hauteur 2").Add(
                //action qui lance la pompe 
                               gestionnaireServo.PR_BRAS_DROIT_MONTER).Add(
                               gestionnaireServo.PR_BRAS_DROIT_MONTER).Add(
                               gestionnaireServo.PR_BRAS_DROIT_ROTATIONANTIHORAIRE).Add(
                //action qui arrête la pompe
                               gestionnaireServo.PR_BRAS_DROIT_ROTATIONHORAIRE).Add(
                               gestionnaireServo.PR_BRAS_DROIT_DESCENDRE).Add(
                               gestionnaireServo.PR_BRAS_DROIT_DESCENDRE
                     ).BuildActionEnSerie();


            Action PR_MOUVEMENT_5 = new ActionBuilder("mettre un cube joker").Add(
                                gestionnaireServo.PR_POUSSOIRJOKER_POUSSER).Add(
                                gestionnaireServo.PR_POUSSOIRJOKER_RETOUR
                       ).BuildActionEnSerie();

            //Action PR_MOUVEMENT_6 = new ActionBuilder("pousser la pile de cubes d'un cran + cube de droite + cube de gauche")

            Action PR_MOUVEMENT_7 = new ActionBuilder("poser le cube de gauche sur le cube du milieu").Add(
                gestionnaireServo.PR_AIGUILLAGE_VENTOUSEGAUCHE).Add(
                //actionner la pompe
                gestionnaireServo.PR_BRAS_GAUCHE_MONTER).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_ROTATIONHORAIRE).Add(
                //eteindre la pompe
                gestionnaireServo.PR_BRAS_GAUCHE_ROTATIONANTIHORAIRE).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER).Add(
                gestionnaireServo.PR_BRAS_GAUCHE_DESCENDRE).Add(
                gestionnaireServo.PR_AIGUILLAGE_VENTOUSEDROITE
                ).BuildActionEnSerie();

            Action MOTHER_ACTION = new ActionBuilder("Action mère Test1")
           .Add(new ActionBuilder("Creer la première pile de cube").Add( // cas équipe orange combinaison J-N-B
                     PR_MOUVEMENT_4
                      ).Add(
                      PR_MOUVEMENT_2_BIS
                      ).Add(
                      PR_MOUVEMENT_5
                       ).BuildActionEnSerie()
                      

                    /*  .Add( new ActionBuilder ("Poser la pile 1")).Add(
                                
                      
                      )*/

                        ).BuildActionEnSerie();

            return MOTHER_ACTION;

               
        }
    }
}
