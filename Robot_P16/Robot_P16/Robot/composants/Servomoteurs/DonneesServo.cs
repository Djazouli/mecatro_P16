using System;
using System.Collections;
using Microsoft.SPOT;

namespace Robot_P16.Robot.composants.Servomoteurs
{

    public static class DonneesServo
    {
        /* ***************** REGLES DE NOMMAGE A RESPECTER **********
         * public const int [TYPE DE PARAMETRE]_[TYPE DE ROBOT]_[ROLE DU SERVO]_[ROLE DE LA ROTATION]=-1;// = X;
         * Exemple : angle de rotation pour ouvrir le clapet des réservoirs sur le grand robot :
         * public const int ANGLE_GR_CLAPET_RESERVOIR_OUVRIR=-1;// = X;
         * 
         * Si l'angle doit dépendre de la couleur de l'équipe, rajouter _[COULEUR] à la fin du nom de la constante
         * ***************** FIN DES REGLES DE NOMMAGE ****************** */


 	 public const int ANGLE_PR_POUSSOIRJOKER_POUSSER=90*1024/300;// = 90; //en degré, valeur approximative, odg moins d'un tour
 	 public const int ANGLE_PR_POUSSOIRJOKER_RETOUR=0;// = 90;
 	 
 	 public const int ANGLE_PR_ASCENSEURGAUCHE_MONTERUNITE=1300;// = 342.86; //pour monter de 60mm, monter un cube d'une hauteur de 60mm
 	 public const int ANGLE_PR_ASCENSEURGAUCHE_DESCENDREUNITE=1220;// = -342.86;
 	 
 	 public const int ANGLE_PR_ASCENSEURDROIT_MONTERUNITE=885;// = 202.32; //pour monter de 60mm, systeme different de l'autre ascenseur
 	 public const int ANGLE_PR_ASCENSEURDROIT_DESCENDREUNITE=710;// = -202.32;

     public const int ANGLE_PR_ROTATIONGAUCHE_HORAIRE = 165 * 1024 / 300;//stockage d'un cube sur la pile (90 ou -90)
     public const int ANGLE_PR_ROTATIONGAUCHE_ANTIHORAIRE =  70*1024/300;
     public const int ANGLE_PR_ROTATIONGAUHE_COINCER = -1; //15=-1;//utiliser les patins pour saisir un cube de la pile
 	 public const int ANGLE_PR_ROTATIONGAUCHE_DECOINCER= 15;
 	 
 	 public const int ANGLE_PR_ROTATIONDROIT_ANTIHORAIRE=60*1024/300;// = -90;
 	 public const int ANGLE_PR_ROTATIONDROIT_HORAIRE= 150*1024/300 ;
 	 public const int ANGLE_PR_ROTATIONDROIT_COINCER= 90;
 	 public const int ANGLE_PR_ROTATIONDROIT_DECOINCER= 90;
 
 	 public const int ANGLE_PR_DEPLOIEMENTGAUCHE_SORTIR=773;// = 195,58=-1;//déployer le bras gauche d'une longueur d'un cube
 	 public const int ANGLE_PR_DEPLOIEMENTGAUCHE_RENTRER=773;// = -195.58;

     public const int ANGLE_PR_AIGUILLAGE_VENTOUSEDROITE = 819;
     public const int ANGLE_PR_AIGUILLAGE_VENTOUSEGAUCHE = 477;// = 90=-1;//faire passer le flux d'air dans la ventouse gauche
 	 
 	 //////Grand Robot
 	 
 	 public const int ANGLE_GR_CLAPET_RESERVOIR_OUVRIR=-1;// = 0;
 	 
 	 public const int DUREE_GR_CANONBAS_PROJETERBALLE=-1;// = ?=-1;//faire tourner le moteur pendant un temps court pour catapulter une ou plusieurs balle
 	 //ce moteur n'est pas un servo !
 	 public const int DUREE_GR_CANONHAUT_PROJETERBALLE=-1;// = ?;
 	
 	 public const int ANGLE_GR_PLATEAU_CRANHORAIRE=-1;// = 36.00;
 	 public const int ANGLE_GR_PLATEAU_CRANANTIHORAIRE=-1;// = -36.00;
 	 public const int ANGLE_GR_PLATEAU_TAQUET=-1;// = -54.00;
 	 
 	 public const int ANGLE_GR_TRAPPE_OUVRIR=-1;// = ?=-1;//still unknown so far
 	 public const int ANGLE_GR_TRAPPE_FERMER=-1;// = ?;


        public const int ANGLE_PR_ROTATIONDROITE_COINCER=-1;// = -36.00;
        public const int ANGLE_PR_ROTATIONDROITE_DECOINCER=-1;// = -36.00;
        public const int ANGLE_PR_ROTATIONGAUCHE_COINCER=-1;// = -36.00;


    }
}
