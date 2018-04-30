using System;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot;

namespace Robot_P16.Robot.composants.Servomoteurs
{
    class GestionnaireServosGR
    {
        public ActionServoAbsolue GR_PLATEAU_TAQUET =
            new ActionBuilder("ServoGR- ouvrir le taquet").BuildActionServoAbsolue(Robot.robot.GR_SERVO_PLATEAU,  DonneesServo.ANGLE_GR_PLATEAU_TAQUET);

        //AJOUTER GR_SERVO_PLATEAU dans Robot.robot et ANGLE_GR_PLATEAU_TAQUET dans Donnees Servo

        public ActionServoAbsolue GR_PLATEAU_CRANHORAIRE =
           new ActionBuilder("ServoGR- tourner le plateau d'un cran dans le sens horaire").BuildActionServoAbsolue(Robot.robot.GR_SERVO_PLATEAU,  DonneesServo.ANGLE_GR_PLATEAU_CRANHORAIRE);

        //AJOUTER ANGLE_GR_PLATEAU_CRANHORAIRE dans Donnees Servo

        public ActionServoAbsolue GR_PLATEAU_CRANANTIHORAIRE =
            new ActionBuilder("ServoGR- tourner le plateau d'un cran dans le sens antihoraire").BuildActionServoAbsolue(Robot.robot.GR_SERVO_PLATEAU,  DonneesServo.ANGLE_GR_PLATEAU_CRANANTIHORAIRE);

        //AJOUTER ANGLE_GR_PLATEAU_CRANANTIHORAIRE dans Donnees Servo

        //II- ACTIONS POUR LA TRAPPE

        public ActionServoAbsolue GR_TRAPPE_OUVRIR =
    new ActionBuilder("ServoGR- ouvrir la trappe").BuildActionServoAbsolue(Robot.robot.GR_SERVO_TRAPPE,  DonneesServo.ANGLE_GR_TRAPPE_OUVRIR);

        //AJOUTER GR_SERVO_TRAPPE dans Robot.robot et ANGLE_GR_TRAPPE_OUVRIR dans Donnees Servo

        public ActionServoAbsolue GR_TRAPPE_FERMER =
    new ActionBuilder("ServoGR- fermer la trappe").BuildActionServoAbsolue(Robot.robot.GR_SERVO_TRAPPE,  DonneesServo.ANGLE_GR_TRAPPE_FERMER);

        //AJOUTER ANGLE_GR_TRAPPE_FERMER dans Donnees Servo
    }
}
