using System;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot;

namespace Robot_P16.Robot.composants.Servomoteurs
{
    class GestionnaireServosGR
    {
        public  ActionServo GR_PLATEAU_TAQUET =
            new ActionBuilder("ServoGR- ouvrir le taquet").BuildActionServo(Robot.robot.GR_SERVO_PLATEAU, ServoCommandTypes.ABSOLUTE_ROTATION, DonneesServo.ANGLE_GR_PLATEAU_TAQUET);

        //AJOUTER GR_SERVO_PLATEAU dans Robot.robot et ANGLE_GR_PLATEAU_TAQUET dans Donnees Servo

        public  ActionServo GR_PLATEAU_CRANHORAIRE =
           new ActionBuilder("ServoGR- tourner le plateau d'un cran dans le sens horaire").BuildActionServo(Robot.robot.GR_SERVO_PLATEAU, ServoCommandTypes.ABSOLUTE_ROTATION, DonneesServo.ANGLE_GR_PLATEAU_CRANHORAIRE);

        //AJOUTER ANGLE_GR_PLATEAU_CRANHORAIRE dans Donnees Servo

        public  ActionServo GR_PLATEAU_CRANANTIHORAIRE =
            new ActionBuilder("ServoGR- tourner le plateau d'un cran dans le sens antihoraire").BuildActionServo(Robot.robot.GR_SERVO_PLATEAU, ServoCommandTypes.ABSOLUTE_ROTATION, DonneesServo.ANGLE_GR_PLATEAU_CRANANTIHORAIRE);

        //AJOUTER ANGLE_GR_PLATEAU_CRANANTIHORAIRE dans Donnees Servo

        //II- ACTIONS POUR LA TRAPPE

        public  ActionServo GR_TRAPPE_OUVRIR =
    new ActionBuilder("ServoGR- ouvrir la trappe").BuildActionServo(Robot.robot.GR_SERVO_TRAPPE, ServoCommandTypes.ABSOLUTE_ROTATION, DonneesServo.ANGLE_GR_TRAPPE_OUVRIR);

        //AJOUTER GR_SERVO_TRAPPE dans Robot.robot et ANGLE_GR_TRAPPE_OUVRIR dans Donnees Servo

        public  ActionServo GR_TRAPPE_FERMER =
    new ActionBuilder("ServoGR- fermer la trappe").BuildActionServo(Robot.robot.GR_SERVO_TRAPPE, ServoCommandTypes.ABSOLUTE_ROTATION, DonneesServo.ANGLE_GR_TRAPPE_FERMER);

        //AJOUTER ANGLE_GR_TRAPPE_FERMER dans Donnees Servo
    }
}
