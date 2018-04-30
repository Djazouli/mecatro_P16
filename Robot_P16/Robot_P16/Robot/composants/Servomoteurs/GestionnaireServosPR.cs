using System;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot;

namespace Robot_P16.Robot.composants.Servomoteurs
{
    class GestionnaireServosPR
    {

        public ActionServoAbsolue PR_BRAS_DROIT_MONTER =
            new ActionBuilder("ServoPR- monter le bras droit").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT,  DonneesServo.ANGLE_PR_ASCENSEURDROIT_MONTERUNITE);
        //AJOUTER PR_SERVO_ASCENSEUR_BRAS_DROIT dans Robot.robot et ANGLE_PR_ASCENSEURDROIT_MONTERUNITE dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_MONTER =
            new ActionBuilder("ServoPR- monter le bras gauche").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ASCENSEURGAUCHE_MONTERUNITE);

        //AJOUTER PR_SERVO_ASCENSEUR_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_ASCENSEURGAUCHE_MONTERUNITE dans Donnees Servo


        public ActionServoAbsolue PR_BRAS_DROIT_DESCENDRE =
            new ActionBuilder("ServoPR- descendre le bras droit").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT,  DonneesServo.ANGLE_PR_ASCENSEURDROIT_DESCENDREUNITE);

        //AJOUTER ANGLE_PR_ASCENSEURDROIT_DESCENDREUNITE dans Donnees Servo



        public ActionServoAbsolue PR_BRAS_GAUCHE_DESCENDRE =
            new ActionBuilder("ServoPR- descendre le bras gauche").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ASCENSEURGAUCHE_DESCENDREUNITE);

        //AJOUTER ANGLE_PR_ASCENSEURGAUCHE_DESCENDREUNITE dans Donnees Servo



        public ActionServoAbsolue PR_BRAS_DROIT_ROTATIONHORAIRE =
            new ActionBuilder("ServoPR- rotation horaire bras droit").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,  DonneesServo.ANGLE_PR_ROTATIONDROIT_HORAIRE);

        //AJOUTER PR_SERVO_ROTATION_BRAS_DROIT dans Robot.robot et ANGLE_PR_ROTATIONDROIT_HORAIRE dans Donnees Servo


        public ActionServoAbsolue PR_BRAS_DROIT_ROTATIONANTIHORAIRE =
            new ActionBuilder("ServoPR- rotation antihoraire bras droit").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,  DonneesServo.ANGLE_PR_ROTATIONDROIT_ANTIHORAIRE);

        //AJOUTER ANGLE_PR_ROTATIONDROIT_ANTIHORAIRE dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_ROTATIONHORAIRE =
            new ActionBuilder("ServoPR- rotation horaire bras gauche").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ROTATIONGAUCHE_HORAIRE);

        //AJOUTER PR_SERVO_ROTATION_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_ROTATIONGAUCHE_HORAIRE dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_ROTATIONANTIHORAIRE =
            new ActionBuilder("ServoPR- rotation antihoraire bras gauche").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ROTATIONGAUCHE_HORAIRE);

        //AJOUTER PR_SERVO_ROTATION_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_ROTATIONGAUCHE_HORAIRE dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_DROIT_ROTATION_COINCER =
            new ActionBuilder("ServoPR- rotation bras droit pour coincer cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,  DonneesServo.ANGLE_PR_ROTATIONDROITE_COINCER);

        //AJOUTER ANGLE_PR_ROTATIONDROITE_COINCER dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_DROIT_ROTATION_DECOINCER =
            new ActionBuilder("ServoPR- rotation bras droit pour decoincer cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_DROIT,  DonneesServo.ANGLE_PR_ROTATIONDROITE_DECOINCER);

        //AJOUTER ANGLE_PR_ROTATIONDROITE_DECOINCER dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_ROTATION_COINCER =
        new ActionBuilder("ServoPR- rotation bras gauche pour coincer cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ROTATIONGAUCHE_COINCER);

        //AJOUTER ANGLE_PR_ROTATIONGAUCHE_COINCER dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_GAUCHE_ROTATION_DECOINCER =
        new ActionBuilder("ServoPR- rotation bras gauche pour decoincer cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ROTATION_BRAS_GAUCHE,  DonneesServo.ANGLE_PR_ROTATIONGAUCHE_DECOINCER);

        //AJOUTER ANGLE_PR_ROTATIONGAUCHE_DECOINCER dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_DROIT_DEPLOIEMENT_SORTIR =
        new ActionBuilder("ServoPR- deploiement du bras gauche").BuildActionServoAbsolue(Robot.robot.PR_SERVO_DEPLOIEMENT_BRAS_DROIT,  DonneesServo.ANGLE_PR_DEPLOIEMENTGAUCHE_SORTIR);

        //AJOUTER PR_SERVO_DEPLOIEMENT_BRAS_GAUCHE dans Robot.robot et ANGLE_PR_DEPLOIEMENTGAUCHE_SORTIR dans Donnees Servo

        public ActionServoAbsolue PR_BRAS_DROIT_DEPLOIEMENT_RENTRER =
        new ActionBuilder("ServoPR- rentrer le bras gauche deploye").BuildActionServoAbsolue(Robot.robot.PR_SERVO_DEPLOIEMENT_BRAS_DROIT,  DonneesServo.ANGLE_PR_DEPLOIEMENTGAUCHE_RENTRER);

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
