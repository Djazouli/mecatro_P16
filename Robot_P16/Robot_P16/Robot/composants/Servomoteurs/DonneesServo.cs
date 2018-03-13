using System;
using System.Collections;
using Microsoft.SPOT;

namespace Robot_P16.Robot.composants.Servomoteurs
{

    public static class DonneesServo
    {
        /* ***************** REGLES DE NOMMAGE A RESPECTER **********
         * public const float [TYPE DE PARAMETRE]_[TYPE DE ROBOT]_[ROLE DU SERVO]_[ROLE DE LA ROTATION] = X;
         * Exemple : angle de rotation pour ouvrir le clapet des réservoirs sur le grand robot :
         * public const float ANGLE_GR_CLAPET_RESERVOIR_OUVRIR = X;
         * 
         * Si l'angle doit dépendre de la couleur de l'équipe, rajouter _[COULEUR] à la fin du nom de la constante
         * ***************** FIN DES REGLES DE NOMMAGE ****************** */

        public const float ANGLE_GR_CLAPET_RESERVOIR_OUVRIR = 0;
         //////Petit Robot

 	 public const float ANGLE_PR_POUSSOIRJOKER_POUSSER = 90; //en degré, valeur approximative, odg moins d'un tour
 	 public const float ANGLE_PR_POUSSOIRJOKER_RETOUR = 90;
 	 
 	 public const float ANGLE_PR_ASCENSEURGAUCHE_MONTERUNITE = 342.86; //pour monter de 60mm, monter un cube d'une hauteur de 60mm
 	 public const float ANGLE_PR_ASCENSEURGAUCHE_DESCENDREUNITE = -342.86;
 	 
 	 public const float ANGLE_PR_ASCENSEURDROIT_MONTERUNITE = 202.32; //pour monter de 60mm, systeme different de l'autre ascenseur
 	 public const float ANGLE_PR_ASCENSEURDROIT_DESCENDREUNITE = -202.32;
 	 
 	 public const float ANGLE_PR_ROTATIONGAUCHE_HORAIRE = 90;//stockage d'un cube sur la pile (90 ou -90)
 	 public const float ANGLE_PR_ROTATIONGAUCHE_ANTIHORAIRE = -90;
 	 public const float ANGLE_PR_ROTATIONGAUHE_COINCER= 15;//utiliser les patins pour saisir un cube de la pile
 	 public const float ANGLE_PR_ROTATIONGAUCHE_DECOINCER= 15;
 	 
 	 public const float ANGLE_PR_ROTATIONDROIT_ANTIHORAIRE = -90;
 	 public const float ANGLE_PR_ROTATIONDROIT_HORAIRE= 90;
 	 public const float ANGLE_PR_ROTATIONDROIT_COINCER= 90;
 	 public const float ANGLE_PR_ROTATIONDROIT_DECOINCER= 90;
 
 	 public const float ANGLE_PR_DEPLOIEMENTGAUCHE_SORTIR = 195,58;//déployer le bras gauche d'une longueur d'un cube
 	 public const float ANGLE_PR_DEPLOIEMENTGAUCHE_RENTRER = -195.58;
 	 
 	 public const float ANGLE_PR_AIGUILLAGE_VENTOUSEDROITE = 90;//faire passer le flux d'air dans la ventouse droite
 	 public const float ANGLE_PR_AIGUILLAGE_VENTOUSEGAUCHE = 90;//faire passer le flux d'air dans la ventouse gauche
 	 
 	 //////Grand Robot
 	 
 	 public const float ANGLE_GR_CLAPET_RESERVOIR_OUVRIR = 0;
 	 
 	 public const float DUREE_GR_CANONBAS_PROJETERBALLE = ?;//faire tourner le moteur pendant un temps court pour catapulter une ou plusieurs balle
 	 //ce moteur n'est pas un servo !
 	 public const float DUREE_GR_CANONHAUT_PROJETERBALLE = ?;
 	 
 	 public const float ANGLE_GR_PLATEAU_CRANHORAIRE = 36.00;
 	 public const float ANGLE_GR_PLATEAU_CRANANTIHORAIRE = -36.00;
 	 public const float ANGLE_GR_PLATEAU_TAQUET = -54.00;
 	 
 	 public const float ANGLE_GR_TRAPPE_OUVRIR = ?;//still unknown so far
 	 public const float ANGLE_GR_TRAPPE_FERMER = ?;


    }
}
