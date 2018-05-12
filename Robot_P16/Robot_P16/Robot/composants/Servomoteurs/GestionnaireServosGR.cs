using System;
using Microsoft.SPOT;
using Robot_P16.Actions;
using Robot_P16.Robot;

namespace Robot_P16.Robot.composants.Servomoteurs
{
    public class GestionnaireServosGR
    {

        public ActionServoAbsolue GR_ABEILLE_DECLENCHER =
    new ActionBuilder("ServoGR- declencher abeille").BuildActionServoAbsolue(Robot.robot.GR_SERVO_ABEILLE, DonneesServo.ANGLE_GR_ABEILLE_DECLENCHER);
        public ActionServoAbsolue GR_ABEILLE_REPLIER =
    new ActionBuilder("ServoGR- replier abeille").BuildActionServoAbsolue(Robot.robot.GR_SERVO_ABEILLE, DonneesServo.ANGLE_GR_ABEILLE_REPLIER);

        public ActionServoAbsolue GR_TRAPPE_OUVRIR =
    new ActionBuilder("ServoGR- ouvrir la trappe").BuildActionServoAbsolue(Robot.robot.GR_SERVO_TRAPPE,  DonneesServo.ANGLE_GR_TRAPPE_OUVRIR);

        //AJOUTER GR_SERVO_TRAPPE dans Robot.robot et ANGLE_GR_TRAPPE_OUVRIR dans Donnees Servo

        public ActionServoAbsolue GR_TRAPPE_FERMER =
    new ActionBuilder("ServoGR- fermer la trappe").BuildActionServoAbsolue(Robot.robot.GR_SERVO_TRAPPE,  DonneesServo.ANGLE_GR_TRAPPE_FERMER);


        public ActionServoAbsolue GR_PLATEAU_AVANT_ORANGE=
    new ActionBuilder("ServoGR- trou du plateau vers l'avant (sticker orange)").BuildActionServoAbsolue(Robot.robot.GR_SERVO_PLATEAU, DonneesServo.ANGLE_GR_PLATEAU_OUVERTURE_AVANT_ORANGE);

        //AJOUTER GR_SERVO_TRAPPE dans Robot.robot et ANGLE_GR_TRAPPE_OUVRIR dans Donnees Servo

        public ActionServoAbsolue GR_PLATEAU_AVANT_VERT =
    new ActionBuilder("ServoGR- trou du plateau vers l'arriere (sticker vert)").BuildActionServoAbsolue(Robot.robot.GR_SERVO_PLATEAU, DonneesServo.ANGLE_GR_PLATEAU_OUVERTURE_ARRIERE_VERT);

        public ActionServoRotation GR_PLATEAU_RECOLTE = new ActionBuilder("ServoGR - Tourne le plateau en continu").
            BuildActionServoRotation(Robot.robot.GR_SERVO_PLATEAU, 700, 2500); // Rotation continu sens trigo

        public ActionServoAbsolue GR_PLATEAU_SLOT0 = new ActionServoAbsolue("ServoGR - plateau slot 1", Robot.robot.GR_SERVO_PLATEAU, DonneesServo.ANGLE_GR_PLATEAU_SLOT_0); // rien sur la trappe
        public ActionServoAbsolue GR_PLATEAU_SLOT1 = new ActionServoAbsolue("ServoGR - plateau slot 1", Robot.robot.GR_SERVO_PLATEAU, DonneesServo.ANGLE_GR_PLATEAU_SLOT_1);
        public ActionServoAbsolue GR_PLATEAU_SLOT2 = new ActionServoAbsolue("ServoGR - plateau slot 2", Robot.robot.GR_SERVO_PLATEAU, DonneesServo.ANGLE_GR_PLATEAU_SLOT_2);
        public ActionServoAbsolue GR_PLATEAU_SLOT3 = new ActionServoAbsolue("ServoGR - plateau slot 3", Robot.robot.GR_SERVO_PLATEAU, DonneesServo.ANGLE_GR_PLATEAU_SLOT_3);
        public ActionServoAbsolue GR_PLATEAU_SLOT4 = new ActionServoAbsolue("ServoGR - plateau slot 4", Robot.robot.GR_SERVO_PLATEAU, DonneesServo.ANGLE_GR_PLATEAU_SLOT_4);
        public ActionServoAbsolue GR_PLATEAU_SLOT5 = new ActionServoAbsolue("ServoGR - plateau slot 5", Robot.robot.GR_SERVO_PLATEAU, DonneesServo.ANGLE_GR_PLATEAU_SLOT_5);
        public ActionServoAbsolue GR_PLATEAU_SLOT6 = new ActionServoAbsolue("ServoGR - plateau slot 6", Robot.robot.GR_SERVO_PLATEAU, DonneesServo.ANGLE_GR_PLATEAU_SLOT_6);
        public ActionServoAbsolue GR_PLATEAU_SLOT7 = new ActionServoAbsolue("ServoGR - plateau slot 7", Robot.robot.GR_SERVO_PLATEAU, DonneesServo.ANGLE_GR_PLATEAU_SLOT_7);
        public ActionServoAbsolue GR_PLATEAU_SLOT8 = new ActionServoAbsolue("ServoGR - plateau slot 8", Robot.robot.GR_SERVO_PLATEAU, DonneesServo.ANGLE_GR_PLATEAU_SLOT_8);

        public ActionEnSerie GR_PLATEAU_PETIT_RETOUR_ARRIERE = new ActionBuilder("ServoGR - plateau petit retour en arriere")
            .Add(new ActionWait("Wait a bit...", 400))
            /*.Add(new ActionServoRotation("Plateau servo petit retour arriere", Robot.robot.GR_SERVO_PLATEAU, 250, 200))
            .Add(new ActionServoRotation("Plateau servo petit retour arriere", Robot.robot.GR_SERVO_PLATEAU, 1250, 200))*/
            //.Add(new ActionWait("Wait a bit...", 300))
            .BuildActionEnSerie();

        public Action GR_PLATEAU_LIBERATION_TUBE_UNICOULEUR()
        {
            return new ActionBuilder("ServoGR - Liberation balles cote vert (arriere)")
            .Add(new ActionBuilder("Demarrer lanceur").BuildActionLanceurBalle(/*0.618*/ 0.642 ))
            .Add(GR_TRAPPE_OUVRIR)
            .Add(new ActionWait("Wait a bit...", 50))
            .Add(GR_PLATEAU_SLOT1)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_PLATEAU_SLOT2)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_PLATEAU_SLOT3)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_PLATEAU_SLOT4)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_PLATEAU_SLOT5)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_PLATEAU_SLOT6)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_PLATEAU_SLOT7)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_PLATEAU_SLOT8)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(new ActionWait("Wait a bit...", 1000))
            .Add(new ActionBuilder("Stopper lanceur").BuildActionLanceurBalleStop())
            .BuildActionEnSerie();
        }

        public Action GR_PLATEAU_LIBERATION_BALLES_COULEUR_OPPOSEE()
        {
            return new ActionBuilder("ServoGR - Liberation balles cote vert (arriere)")
            .Add(GR_TRAPPE_FERMER)
            .Add(GR_PLATEAU_SLOT0)
            .Add(new ActionBuilder("Demarrer lanceur").BuildActionLanceurBalle(0.40))
            .Add(GR_PLATEAU_SLOT2)
            .Add(new ActionWait("Wait a bit...", 100))
            .Add(GR_TRAPPE_OUVRIR)
            .Add(new ActionBuilder("Ajouter score").BuildActionAddScore(10))
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_TRAPPE_FERMER)
            .Add(GR_PLATEAU_SLOT4)
            .Add(new ActionWait("Wait a bit...", 100))
            .Add(GR_TRAPPE_OUVRIR)
            .Add(new ActionBuilder("Ajouter score").BuildActionAddScore(10))
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_TRAPPE_FERMER)
            .Add(GR_PLATEAU_SLOT6)
            .Add(new ActionWait("Wait a bit...", 100))
            .Add(GR_TRAPPE_OUVRIR)
            .Add(new ActionBuilder("Ajouter score").BuildActionAddScore(10))
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_TRAPPE_FERMER)
            .Add(GR_PLATEAU_SLOT8)
            .Add(new ActionWait("Wait a bit...", 100))
            .Add(GR_TRAPPE_OUVRIR)
            .Add(new ActionBuilder("Ajouter score").BuildActionAddScore(10))
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(new ActionWait("Wait a bit", 1500))
            .Add(GR_TRAPPE_FERMER)
            .Add(new ActionBuilder("Stopper lanceur").BuildActionLanceurBalleStop())
            .BuildActionEnSerie();
        }


        public Action GR_PLATEAU_LIBERATION_BALLES_TUBE_MIXTE_NOTRE_COULEUR()
        {
            return new ActionBuilder("ServoGR - Envoi balles vertes pour recuperateur mixte")
            .Add(new ActionBuilder("Lanceur fort").BuildActionLanceurBalle(0.748))
            .Add(GR_TRAPPE_FERMER)
            .Add(GR_PLATEAU_SLOT0)
            .Add(new ActionWait("Wait a bit", 300))
            .Add(GR_PLATEAU_SLOT1)
            .Add(new ActionWait("Wait a bit...", 200))
            .Add(GR_TRAPPE_OUVRIR)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_TRAPPE_FERMER)
            .Add(GR_PLATEAU_SLOT3)
            .Add(new ActionWait("Wait a bit...", 200))
            .Add(GR_TRAPPE_OUVRIR)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_TRAPPE_FERMER)
            .Add(GR_PLATEAU_SLOT5)
            .Add(new ActionWait("Wait a bit...", 200))
            .Add(GR_TRAPPE_OUVRIR)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(GR_TRAPPE_FERMER)
            .Add(GR_PLATEAU_SLOT7)
            .Add(new ActionWait("Wait a bit...", 200))
            .Add(GR_TRAPPE_OUVRIR)
            .Add(GR_PLATEAU_PETIT_RETOUR_ARRIERE)
            .Add(new ActionWait("Wait a bit...", 1500))
            .Add(new ActionBuilder("Stopper lanceur").BuildActionLanceurBalleStop())
            .BuildActionEnSerie();
        }
    }
}
