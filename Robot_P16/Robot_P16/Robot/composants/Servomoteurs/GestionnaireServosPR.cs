using System;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot;

namespace Robot_P16.Robot.composants.Servomoteurs
{
    class GestionnaireServosPR
    {

        public ActionServoRotation PR_BRAS_DROIT_POSITION_BASE =
            new ActionBuilder("ServoPR - mettre bras droit en position de base").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, Robot_P16.Robot.composants.Servomoteurs.speed.reverse,500 );

        public ActionServoRotation PR_BRAS_GAUCHE_POSITION_BASE =
            new ActionBuilder("ServoPR - mettre bras gauche en position de base").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.forward, 500);

        public ActionServoRotation PR_BRAS_GAUCHE_HAUTEUR_1 =
            new ActionBuilder("bras gauche au niveau du cube du bas en partant de la position de base").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, 300);

        public ActionServoRotation PR_BRAS_DROIT_HAUTEUR_1 =
            new ActionBuilder("bras droit au niveau du cube du bas en partant de la position de base").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, Robot_P16.Robot.composants.Servomoteurs.speed.forward, 270);

        public ActionServoRotation PR_BRAS_DROIT_MONTER =
            new ActionBuilder("ServoPR- monter le bras droit").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.ANGLE_PR_ASCENSEURDROIT_MONTERUNITE );
        //AJOUTER PR_SERVO_ASCENSEUR_BRAS_DROIT dans Robot.robot et ANGLE_PR_ASCENSEURDROIT_MONTERUNITE dans Donnees Servo

         public ActionServoRotation PR_BRAS_DROIT_MONTER_2 =
            new ActionBuilder("ServoPR- monter le bras droit").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.ANGLE_PR_ASCENSEURDROIT_MONTERUNITE );
        
        public ActionServoRotation PR_BRAS_GAUCHE_MONTER =
            new ActionBuilder("ServoPR- monter le bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE,Robot_P16.Robot.composants.Servomoteurs.speed.forward ,DonneesServo.TEMPS_PR_ASCENSEURGAUCHE_MONTERUNITE );
        public ActionServoRotation PR_BRAS_GAUCHE_MONTER_2 =
            new ActionBuilder("ServoPR- monter le bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.forward, DonneesServo.TEMPS_PR_ASCENSEURGAUCHE_MONTERUNITE);

        //AJOUTER PR_SERVO_ASCENSEUR_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_ASCENSEURGAUCHE_MONTERUNITE dans Donnees Servo
        public ActionServoRotation PR_BRAS_DROIT_DESCENDREPOURPOSERVENTOUSE =
           new ActionBuilder("ServoPR- descendre le bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.TEMPS_PR_DESCENDREPOSERVENTOUSE);

        public ActionServoRotation PR_BRAS_DROIT_DESCENDRE =
            new ActionBuilder("ServoPR- descendre le bras droit").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, Robot_P16.Robot.composants.Servomoteurs.speed.forward, DonneesServo.ANGLE_PR_ASCENSEURDROIT_DESCENDREUNITE);

        public ActionServoRotation PR_BRAS_DROIT_DESCENDRE_2 =
            new ActionBuilder("ServoPR- descendre le bras droit").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT, Robot_P16.Robot.composants.Servomoteurs.speed.forward, DonneesServo.ANGLE_PR_ASCENSEURDROIT_DESCENDREUNITE);
       
        public ActionServoRotation PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE=
           new  ActionBuilder("ServoPR- descendre le bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.TEMPS_PR_DESCENDREPOSERVENTOUSE);
       
        public ActionServoRotation PR_BRAS_GAUCHE_DESCENDRE =
            new ActionBuilder("ServoPR- descendre le bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.TEMPS_PR_ASCENSEURGAUCHE_DESCENDREUNITE);

        //AJOUTER ANGLE_PR_ASCENSEURGAUCHE_DESCENDREUNITE dans Donnees Servo
        public ActionServoRotation PR_BRAS_GAUCHE_DESCENDRE_2 =
            new ActionBuilder("ServoPR- descendre le bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.TEMPS_PR_ASCENSEURGAUCHE_DESCENDREUNITE);



        public ActionServoAbsolue PR_BRAS_DROIT_ROTATIONHORAIRE =
            new ActionBuilder("ServoPR- rotation horaire bras droit").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,  DonneesServo.ANGLE_PR_ROTATIONDROIT_MILIEU);

        //AJOUTER PR_SERVO_ROTATION_BRAS_DROIT dans Robot.robot et ANGLE_PR_ROTATIONDROIT_HORAIRE dans Donnees Servo


        public ActionServoAbsolue PR_BRAS_DROIT_ROTATIONANTIHORAIRE =
            new ActionBuilder("ServoPR- rotation antihoraire bras droit").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,  DonneesServo.ANGLE_PR_ROTATIONDROIT_INTERIEUR);

        //AJOUTER ANGLE_PR_ROTATIONDROIT_ANTIHORAIRE dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_ROTATIONHORAIRE =
            new ActionBuilder("ServoPR- rotation horaire bras gauche").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ROTATIONGAUCHE_INTERIEUR);

        //AJOUTER PR_SERVO_ROTATION_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_ROTATIONGAUCHE_HORAIRE dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_ROTATIONANTIHORAIRE =
            new ActionBuilder("ServoPR- rotation antihoraire bras gauche").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ROTATIONGAUCHE_MILIEU);

        //AJOUTER PR_SERVO_ROTATION_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_ROTATIONGAUCHE_HORAIRE dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_DROIT_ROTATION_COINCER =
            new ActionBuilder("ServoPR- rotation bras droit pour coincer cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,  DonneesServo.ANGLE_PR_ROTATIONDROITE_COINCER);

        //AJOUTER ANGLE_PR_ROTATIONDROITE_COINCER dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_DROIT_ROTATION_DECOINCER =
            new ActionBuilder("ServoPR- rotation bras droit pour decoincer cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,  DonneesServo.ANGLE_PR_ROTATIONDROITE_DECOINCER);

        //AJOUTER ANGLE_PR_ROTATIONDROITE_DECOINCER dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_ROTATION_COINCER =
        new ActionBuilder("ServoPR- rotation bras gauche pour coincer cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ROTATIONGAUCHE_COINCER);


        public ActionServoRotation PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR =
        new ActionBuilder("ServoPR- deploiement du bras gauche").BuildActionServoRotation(Robot.robot.PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.forward, DonneesServo.TEMPS_PR_DEPLOIEMENTGAUCHE_SORTIR);

        //AJOUTER PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_DEPLOIEMENTGAUCHE_SORTIR dans Donnees Servo

        public ActionServoRotation PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER =
        new ActionBuilder("ServoPR- rentrer le bras gauche deploye").BuildActionServoRotation(Robot.robot.PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE, Robot_P16.Robot.composants.Servomoteurs.speed.reverse, DonneesServo.TEMPS_PR_DEPLOIEMENTGAUCHE_RENTRER);

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
