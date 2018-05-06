using System;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot;

namespace Robot_P16.Robot.composants.Servomoteurs
{
    class GestionnaireServosPR
    {
        public ActionServoRotation PR_BRAS_GAUCHE_POSITION_BASE =
            new ActionBuilder("ServoPR - mettre bras gauche en position de base").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.forward, 500);

        public ActionServoRotation PR_BRAS_GAUCHE_HAUTEUR_1 =
            new ActionBuilder("bras gauche au niveau du cube du bas en partant de la position de base").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, 300);

        public ActionServoAbsolue PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1 =
            new ActionBuilder("bras droit au niveau du cube du bas avec ventouse appuyee").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, DonneesServo.ANGLE_PR_ASCENSEURDROIT_PARTIE_BASSE_MINIMUM);

        public ActionServoAbsolue PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2 =
            new ActionBuilder("Bras droit va a la hauteur pour poser un cube sur un cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, DonneesServo.ANGLE_PR_ASCENSEURDROIT_PARTIE_BASSE_HAUTEUR_1_CUBE);

        public ActionServoAbsolue PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_3 =
            new ActionBuilder("Bras droit va a la hauteur pour poser un cube sur deux cubes").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, DonneesServo.ANGLE_PR_ASCENSEURDROIT_PARTIE_HAUTE_HAUTEUR_2_CUBE);
        public ActionServoAbsolue PR_BRAS_DROIT_GOTO0 =
            new ActionBuilder("En zone haut, aller en 0 avant de descendre").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, DonneesServo.ANGLE_PR_ASCENSEURDROIT_PARTIE_HAUTE_MINIMUM);
        public ActionServoRotation PR_BRAS_DROIT_ZONE_HAUT_VERS_BAS =
            new ActionBuilder("Partant de la zone haute en 0, on passe dans la zone du bas").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, DonneesServo.VITESSE_PR_ASCENSEURDROIT_PARTIE_HAUTE_VERS_BASSE, DonneesServo.DELAI_PR_ASCENSEURDROIT_PARTIE_HAUTE_VERS_BASSE);
        public ActionServoAbsolue PR_BRAS_DROIT_GOTO1000 =
            new ActionBuilder("On va en 1000 de la zone du bas").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, 1000);
        public ActionServoRotation PR_BRAS_DROIT_ZONE_BAS_VERS_HAUT =
            new ActionBuilder("Partant de la zone basse en 1000, on passe dans la zone du haut").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, DonneesServo.VITESSE_PR_ASCENSEURDROIT_PARTIE_BASSE_VERS_HAUTE, DonneesServo.DELAI_PR_ASCENSEURDROIT_PARTIE_BASSE_VERS_HAUTE);
        /*public ActionServoRotation PR_BRAS_DROIT_MONTER =
            new ActionBuilder("ServoPR- monter le bras droit").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.ANGLE_PR_ASCENSEURDROIT_MONTERUNITE );
        //AJOUTER PR_SERVO_ASCENSEUR_BRAS_DROIT dans Robot.robot et ANGLE_PR_ASCENSEURDROIT_MONTERUNITE dans Donnees Servo

         public ActionServoRotation PR_BRAS_DROIT_MONTER_2 =
            new ActionBuilder("ServoPR- monter le bras droit").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.ANGLE_PR_ASCENSEURDROIT_MONTERUNITE );
        */
        public ActionServoRotation PR_BRAS_GAUCHE_MONTER =
            new ActionBuilder("ServoPR- monter le bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE,Robot_P16.Robot.composants.Servomoteurs.speed.forward ,DonneesServo.TEMPS_PR_ASCENSEURGAUCHE_MONTERUNITE );
        public ActionServoRotation PR_BRAS_GAUCHE_MONTER_2 =
            new ActionBuilder("ServoPR- monter le bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.forward, DonneesServo.TEMPS_PR_ASCENSEURGAUCHE_MONTERUNITE);

        //AJOUTER PR_SERVO_ASCENSEUR_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_ASCENSEURGAUCHE_MONTERUNITE dans Donnees Servo
        
        /*public ActionServoRotation PR_BRAS_DROIT_DESCENDRE =
            new ActionBuilder("ServoPR- descendre le bras droit").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, Robot_P16.Robot.composants.Servomoteurs.speed.forward, DonneesServo.ANGLE_PR_ASCENSEURDROIT_DESCENDREUNITE);

        public ActionServoRotation PR_BRAS_DROIT_DESCENDRE_2 =
            new ActionBuilder("ServoPR- descendre le bras droit").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, Robot_P16.Robot.composants.Servomoteurs.speed.forward, DonneesServo.ANGLE_PR_ASCENSEURDROIT_DESCENDREUNITE);
       */

        public ActionServoRotation PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE=
           new  ActionBuilder("ServoPR- descendre le bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.TEMPS_PR_DESCENDREPOSERVENTOUSE);
       
        public ActionServoRotation PR_BRAS_GAUCHE_DESCENDRE =
            new ActionBuilder("ServoPR- descendre le bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.TEMPS_PR_ASCENSEURGAUCHE_DESCENDREUNITE);

        //AJOUTER ANGLE_PR_ASCENSEURGAUCHE_DESCENDREUNITE dans Donnees Servo
        public ActionServoRotation PR_BRAS_GAUCHE_DESCENDRE_2 =
            new ActionBuilder("ServoPR- descendre le bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.TEMPS_PR_ASCENSEURGAUCHE_DESCENDREUNITE);

       public ActionServoAbsolue PR_BRAS_DROIT_ROTATION_MILIEU =
            new ActionBuilder("ServoPR- rotation horaire bras droit").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,  DonneesServo.ANGLE_PR_ROTATIONDROIT_MILIEU);

        //AJOUTER PR_SERVO_ROTATION_BRAS_DROIT dans Robot.robot et ANGLE_PR_ROTATIONDROIT_HORAIRE dans Donnees Servo


        public ActionServoAbsolue PR_BRAS_DROIT_ROTATION_INTERIEUR =
            new ActionBuilder("ServoPR- rotation antihoraire bras droit").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,  DonneesServo.ANGLE_PR_ROTATIONDROIT_INTERIEUR);

        //AJOUTER ANGLE_PR_ROTATIONDROIT_ANTIHORAIRE dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_ROTATION_MILIEU =
            new ActionBuilder("ServoPR- rotation horaire bras gauche").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ROTATIONGAUCHE_INTERIEUR);

        //AJOUTER PR_SERVO_ROTATION_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_ROTATIONGAUCHE_HORAIRE dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_ROTATION_INTERIEUR =
            new ActionBuilder("ServoPR- rotation antihoraire bras gauche").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ROTATIONGAUCHE_MILIEU);

        //AJOUTER PR_SERVO_ROTATION_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_ROTATIONGAUCHE_HORAIRE dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_DROIT_ROTATION_COINCER =
            new ActionBuilder("ServoPR- rotation bras droit pour coincer cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT, DonneesServo.ANGLE_PR_ROTATIONDROIT_COINCER);

        //AJOUTER ANGLE_PR_ROTATIONDROITE_COINCER dans Donnees Servo

        //public ActionServoAbsolue PR_BRAS_DROIT_ROTATION_DECOINCER =
        //    new ActionBuilder("ServoPR- rotation bras droit pour decoincer cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,  DonneesServo.ANGLE_PR_ROTATIONDROITE_DECOINCER);

        //AJOUTER ANGLE_PR_ROTATIONDROITE_DECOINCER dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_ROTATION_COINCER =
        new ActionBuilder("ServoPR- rotation bras gauche pour coincer cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ROTATIONGAUCHE_COINCER);


        public ActionServoAbsolue PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL =
        new ActionBuilder("ServoPR- deploiement du bras gauche pour attraper le cube central").BuildActionServoAbsolue(Robot.robot.PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE, DonneesServo.ANGLE_PR_DEPLOIEMENTGAUCHE_DEPLOIEMENT_CUBE_CENTRAL);

        public ActionServoAbsolue PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE =
            new ActionBuilder("ServoPR - deploiement bras gauche pour attraper le cube devant lui").BuildActionServoAbsolue(Robot.robot.PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE, DonneesServo.ANGLE_PR_DEPLOIEMENTGAUCHE_DEPLOIEMENT_CUBE_GAUCHE);
        //AJOUTER PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_DEPLOIEMENTGAUCHE_SORTIR dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER =
        new ActionBuilder("ServoPR- rentrer le bras gauche deploye").BuildActionServoAbsolue(Robot.robot.PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE, DonneesServo.ANGLE_PR_DEPLOIEMENTGAUCHE_DEPLOIEMENT_MIN);

        //AJOUTER ANGLE_PR_DEPLOIEMENTGAUCHE_RENTRER dans Donnees Servo


        //II- ACTIONS POUR SORTIR LES CUBES JOKER

        public ActionServoAbsolue PR_POUSSOIRJOKER_POUSSER =
        new ActionBuilder("ServoPR- pousser un cube joker").BuildActionServoAbsolue(Robot.robot.PR_SERVO_POUSSOIRJOKER,  DonneesServo.ANGLE_PR_POUSSOIRJOKER_POUSSER);

        //AJOUTER PR_SERVO_POUSSOIRJOKER dans Robot.robot et ANGLE_PR_POUSSOIRJOKER_POUSSER dans Donnees Servo

        public ActionServoAbsolue PR_POUSSOIRJOKER_RETOUR =
        new ActionBuilder("ServoPR- pousser un cube joker").BuildActionServoAbsolue(Robot.robot.PR_SERVO_POUSSOIRJOKER,  DonneesServo.ANGLE_PR_POUSSOIRJOKER_RETOUR);

        //AJOUTER ANGLE_PR_POUSSOIRJOKER_RETOUR dans Donnees Servo

        //III- ACTIONS POUR AIGUILLER LA POMPE

        public ActionServoAbsolue PR_AIGUILLAGE_VENTOUSEGAUCHE =
            new ActionBuilder("ServoPR- aiguiller la pompe sur la ventouse gauche").BuildActionServoAbsolue(Robot.robot.PR_SERVO_AIGUILLAGE,  DonneesServo.ANGLE_PR_AIGUILLAGE_VENTOUSEGAUCHE);

        //AJOUTER PR_SERVO_AIGUILLAGE dans Robot.robot et ANGLE_PR_AIGUILLAGE_VENTOUSEGAUCHE dans Donnees Servo

        public ActionServoAbsolue PR_AIGUILLAGE_VENTOUSEDROITE =
            new ActionBuilder("ServoPR- aiguiller la pompe sur la ventouse droite").BuildActionServoAbsolue(Robot.robot.PR_SERVO_AIGUILLAGE,  DonneesServo.ANGLE_PR_AIGUILLAGE_VENTOUSEDROITE);

        //AJOUTER ANGLE_PR_AIGUILLAGE_VENTOUSEDROITE dans Donnees Servo

    }
}
