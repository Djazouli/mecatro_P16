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


        public const int ANGLE_PR_ASCENSEURDROIT_PARTIE_BASSE_MINIMUM = 78; // PRET A RAMASSER VENTOUSE 
        public const int ANGLE_PR_ASCENSEURDROIT_PARTIE_BASSE_HAUTEUR_1_CUBE = 853;//Depose le 2eme cube
        public const int ANGLE_PR_ASCENSEURDROIT_PARTIE_BASSE_MAXIMUM = 1023;
        public const int ANGLE_PR_ASCENSEURDROIT_PARTIE_HAUTE_MINIMUM = 0;
        public const int ANGLE_PR_ASCENSEURDROIT_PARTIE_HAUTE_HAUTEUR_2_CUBE =325; // Depose le 3eme cube (2eme zone)
        public const int ANGLE_PR_ASCENSEURDROIT_PARTIE_HAUTE_MAXIMUM = 400;
        public const int ANGLE_PR_ASCENSEURDROIT_COINCER = 494;


        public const int DELAI_PR_ASCENSEURDROIT_PARTIE_BASSE_VERS_HAUTE = 900; // TODO
        public const int VITESSE_PR_ASCENSEURDROIT_PARTIE_BASSE_VERS_HAUTE = 600;

        public const int DELAI_PR_ASCENSEURDROIT_PARTIE_HAUTE_VERS_BASSE = 600;
        public const int VITESSE_PR_ASCENSEURDROIT_PARTIE_HAUTE_VERS_BASSE = 1600;

 	 public const int ANGLE_PR_POUSSOIRJOKER_POUSSER=160*1024/300;// = 90; //en degré, valeur approximative, odg moins d'un tour
 	 public const int ANGLE_PR_POUSSOIRJOKER_RETOUR=0;// = 90;

        /* ******************** ASCENSEUR GAUCHE ***************** */
 	 
 	 public const int TEMPS_PR_ASCENSEURGAUCHE_MONTERUNITE=1900;// = 342.86; //pour monter de 60mm, monter un cube d'une hauteur de 60mm
 	 public const int TEMPS_PR_ASCENSEURGAUCHE_DESCENDREUNITE=1400;// = -342.86;
     public const int TEMPS_PR_DESCENDREPOSERVENTOUSE = 500;

     public const int DELAI_PR_ASCENSEURGAUCHE_PARTIE_BASSE_VERS_HAUTE = 1500; // TODO
     public const int VITESSE_PR_ASCENSEURGAUCHE_PARTIE_BASSE_VERS_HAUTE = 2000;

     public const int DELAI_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_VERS_BASSE = 1500;
     public const int VITESSE_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_VERS_BASSE = 1000;

     public const int DELAI_PR_ASCENSEURGAUCHE_APPUYER_VENTOUZE = 300;
     public const int VITESSE_PR_ASCENSEURGAUCHE_APPUYER_VENTOUZE = 700;

     public const int DELAI_PR_ASCENSEURGAUCHE_ENLEVER_VENTOUZE = 1500;
     public const int VITESSE_PR_ASCENSEURGAUCHE_ENLEVER_VENTOUZE = 1700;

     public const int ANGLE_PR_ASCENSEURGAUCHE_PARTIE_BASSE_MINIMUM = 1023; // PRET A RAMASSER VENTOUSE, doit aller un peu vers le bas
     public const int ANGLE_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_HAUTEUR_1_CUBE = 1023;//TODO
     public const int ANGLE_PR_ASCENSEURGAUCHE_PARTIE_BASSE_MAXIMUM = 0;
     public const int ANGLE_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_MINIMUM = 1023;
     public const int ANGLE_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_HAUTEUR_2_CUBE = 0;
     public const int ANGLE_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_MAXIMUM = 0;
     public const int ANGLE_PR_ASCENSEURGAUCHE_COINCER = 494; // TODO





     public const int ANGLE_PR_ROTATIONGAUCHE_INTERIEUR = 170 * 1024 / 300;//stockage d'un cube sur la pile (90 ou -90)
     public const int ANGLE_PR_ROTATIONGAUCHE_MILIEU =  80*1024/300;
     public const int ANGLE_PR_ROTATIONGAUCHE_CUBE_CENTRAL = 170 * 1024 / 300;
     public const int ANGLE_PR_ROTATIONGAUCHE_CUBE_GAUCHE = 80 * 1024 / 300;
     public const int ANGLE_PR_ROTATIONGAUCHE_COINCER = 552; //15=-1;//utiliser les patins pour saisir un cube de la pile

     public const int ANGLE_PR_ROTATIONDROIT_INTERIEUR = 200;// a peu pres 60*1024/300
 	 public const int ANGLE_PR_ROTATIONDROIT_MILIEU= 150*1024/300 ;
 	 public const int ANGLE_PR_ROTATIONDROIT_COINCER= 450;
 
 	 /*public const int TEMPS_PR_DEPLOIEMENTGAUCHE_SORTIR=780;// = 195,58=-1;//déployer le bras gauche d'une longueur d'un cube
 	 public const int TEMPS_PR_DEPLOIEMENTGAUCHE_RENTRER=760;// = -195.58;*/

     public const int ANGLE_PR_DEPLOIEMENTGAUCHE_DEPLOIEMENT_MAX=0;
     public const int ANGLE_PR_DEPLOIEMENTGAUCHE_DEPLOIEMENT_MIN = 800;
     public const int ANGLE_PR_DEPLOIEMENTGAUCHE_DEPLOIEMENT_CUBE_CENTRAL = 0;
     public const int ANGLE_PR_DEPLOIEMENTGAUCHE_DEPLOIEMENT_CUBE_GAUCHE = 717;

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
 	 
 	 public const int ANGLE_GR_TRAPPE_OUVRIR=495;
 	 public const int ANGLE_GR_TRAPPE_FERMER=580; // 580 - 600

     public const int ANGLE_GR_PLATEAU_OUVERTURE_AVANT_ORANGE = 204;
     public const int ANGLE_GR_PLATEAU_OUVERTURE_ARRIERE_VERT = 810;

     public const int ANGLE_GR_PLATEAU_SLOT_0 = 0;
     public const int ANGLE_GR_PLATEAU_SLOT_1 = 83; //80
     public const int ANGLE_GR_PLATEAU_SLOT_2 = 205; //204
     public const int ANGLE_GR_PLATEAU_SLOT_3 = 325; //323
     public const int ANGLE_GR_PLATEAU_SLOT_4 = 445; //440
     public const int ANGLE_GR_PLATEAU_SLOT_5 = 569; //573
     public const int ANGLE_GR_PLATEAU_SLOT_6 = 692; //697
     public const int ANGLE_GR_PLATEAU_SLOT_7 = 816; // 820
     public const int ANGLE_GR_PLATEAU_SLOT_8 = 950; //939
        // Si recole cote orange (avant) : commencer par slot 8 => slot le plus proche
        // Si recolt cote vert (arriere) : commencer par slot 0 => slot le plus proche


     public const int ANGLE_GR_ABEILLE_DECLENCHER = 425; // 432 avant
     public const int ANGLE_GR_ABEILLE_REPLIER = 210;

    }
}
