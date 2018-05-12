using System;
using Microsoft.SPOT;
using System.Collections;
using System.IO.Ports;
using GT = Gadgeteer;
using Microsoft.SPOT.Hardware;
using GHI.Pins;
using Robot_P16.Actions;

using System.Threading;

namespace Robot_P16.Robot.composants.Servomoteurs
{
    public abstract class Ascenseur : AX12
    {
        public Ascenseur(int socket, int id)
            : base(socket, id)
        {

        }

        /*
         * 
         * Actions nécessaires :
         * - Poser ventouze
         * - Recadrage cube : hauteur (1 ?),2,3
         * 
         * 
         * */

        public abstract Action ActionGoToZoneBasse();
        public  abstract Action ActionGoToZoneHaute();
        public abstract Action ActionPoserVentouze();
        public abstract Action ActionHauteurPose_2emeCube();
        public abstract Action ActionHauteurPose_3emeCube();
    }

    public class AscenseurDroite : Ascenseur
    {

        private bool isZoneBasse = true;
        public GestionnaireServosPR gestionnaire = new GestionnaireServosPR();

        public AscenseurDroite(int socket, int id)
            : base(socket, id)
        {

        }

        public override Action ActionGoToZoneBasse()
        {
            return ActionGoToZoneBasse(false);
        }
        public  Action ActionGoToZoneBasse(bool forced)
        {
            if (isZoneBasse && !forced)
                return null;
            isZoneBasse = true;

            return new ActionBuilder("GoToZoneBasse").Add(
                new ActionBuilder("Descendre minimum partie haute").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW,
                    DonneesServo.ANGLE_PR_ASCENSEURDROIT_PARTIE_HAUTE_MINIMUM)
                ).Add(new ActionWait("Wait", 1000)).Add(
                new ActionBuilder("Rotation descendre partie haute ->  basse").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW,
                    DonneesServo.VITESSE_PR_ASCENSEURDROIT_PARTIE_HAUTE_VERS_BASSE, DonneesServo.DELAI_PR_ASCENSEURDROIT_PARTIE_HAUTE_VERS_BASSE)
                ).Add(
                    new ActionBuilder("Set zone basse/haute").BuildActionDelegate(() => isZoneBasse = true)
                ).BuildActionEnSerie();

        }
        public override Action ActionGoToZoneHaute()
        {
            return ActionGoToZoneHaute(false);
        }
        public  Action ActionGoToZoneHaute(bool forced)
        {
            if (!isZoneBasse && !forced)
                return null;
            isZoneBasse = false;

            return new ActionBuilder("GoToZoneHaute").Add(
                new ActionBuilder("Monter maximum partie basse").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW,
                    DonneesServo.ANGLE_PR_ASCENSEURDROIT_PARTIE_BASSE_MAXIMUM)
                ).Add(
                new ActionBuilder("Rotation monter partie basse ->  haute ").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW,
                    DonneesServo.VITESSE_PR_ASCENSEURDROIT_PARTIE_BASSE_VERS_HAUTE, DonneesServo.DELAI_PR_ASCENSEURDROIT_PARTIE_BASSE_VERS_HAUTE)
                ).Add(
                    new ActionBuilder("Set zone basse/haute").BuildActionDelegate(() => isZoneBasse = false)
                ).BuildActionEnSerie();
        }


        public override Action ActionHauteurPose_2emeCube()
        {
            return new ActionBuilder("Hauteur de pose pour le 2eme cube")
                .Add(ActionGoToZoneBasse())
                .Add(
                    new ActionBuilder("Hauteur pose 2eme cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW,
                    DonneesServo.ANGLE_PR_ASCENSEURDROIT_PARTIE_BASSE_HAUTEUR_1_CUBE)
                ).BuildActionEnSerie();
        }
        public override Action ActionHauteurPose_3emeCube()
        {
            return new ActionBuilder("Hauteur de pose pour le 2eme cube")
                .Add(ActionGoToZoneHaute())
                .Add(
                    new ActionBuilder("Hauteur pose 2eme cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW,
                    DonneesServo.ANGLE_PR_ASCENSEURDROIT_PARTIE_HAUTE_HAUTEUR_2_CUBE)
                ).BuildActionEnSerie();
        }

        public override Action ActionPoserVentouze()
        {
            return new ActionBuilder("Aller en bas et Poser ventouse")
                 .Add(ActionGoToZoneBasse())
                 .Add(
                     new ActionBuilder("Poser ventouse").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW,
                     DonneesServo.ANGLE_PR_ASCENSEURDROIT_PARTIE_BASSE_MINIMUM)
                 ).BuildActionEnSerie();
        }

    }



    public class AscenseurGauche : Ascenseur
    {

        private bool isZoneBasse = true;
        public GestionnaireServosPR gestionnaire = new GestionnaireServosPR();

        public AscenseurGauche(int socket, int id)
            : base(socket, id)
        {

        }

        public override Action ActionGoToZoneBasse()
        {
            return ActionGoToZoneBasse(false);

        }
        public Action ActionGoToZoneBasse(bool forced)
        {
            if (isZoneBasse && !forced)
                return null;
            isZoneBasse = true;

            return new ActionBuilder("GoToZoneBasse").Add(
                new ActionBuilder("Descendre minimum partie haute").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW,
                    DonneesServo.ANGLE_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_MINIMUM)
                ).Add(new ActionWait("Wait", 1000)).Add(
                new ActionBuilder("Rotation descendre partie haute ->  basse").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW,
                    DonneesServo.VITESSE_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_VERS_BASSE, DonneesServo.DELAI_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_VERS_BASSE)
                ).Add(
                    new ActionBuilder("Set zone basse/haute").BuildActionDelegate(() => isZoneBasse = true)
                ).BuildActionEnSerie();

        }
        public override Action ActionGoToZoneHaute()
        {
            return ActionGoToZoneHaute(false);
        }
        public Action ActionGoToZoneHaute(bool forced)
        {
            if (!isZoneBasse && !forced)
                return null;
            isZoneBasse = false;

            return new ActionBuilder("GoToZoneHaute").Add(
                new ActionBuilder("Monter maximum partie basse").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW,
                    DonneesServo.ANGLE_PR_ASCENSEURGAUCHE_PARTIE_BASSE_MAXIMUM)
                ).Add(
                new ActionBuilder("Rotation monter partie basse ->  haute ").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW,
                    DonneesServo.VITESSE_PR_ASCENSEURGAUCHE_PARTIE_BASSE_VERS_HAUTE, DonneesServo.DELAI_PR_ASCENSEURGAUCHE_PARTIE_BASSE_VERS_HAUTE)
                ).Add(
                    new ActionBuilder("Set zone basse/haute").BuildActionDelegate(() => isZoneBasse = false)
                ).BuildActionEnSerie();
        }


        public override Action ActionHauteurPose_2emeCube()
        {
            return new ActionBuilder("Hauteur de pose pour le 2eme cube")
                .Add(ActionGoToZoneHaute())
                .Add(
                    new ActionBuilder("Hauteur pose 2eme cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW,
                    DonneesServo.ANGLE_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_HAUTEUR_1_CUBE)
                ).Add(
                    new ActionBuilder("Descendre un peu").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW,
                    DonneesServo.VITESSE_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_VERS_BASSE, 500)
                ).BuildActionEnSerie();
        }
        public override Action ActionHauteurPose_3emeCube()
        {
            return new ActionBuilder("Hauteur de pose pour le 2eme cube")
                .Add(ActionGoToZoneHaute())
                .Add(
                    new ActionBuilder("Hauteur pose 2eme cube").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW,
                    DonneesServo.ANGLE_PR_ASCENSEURGAUCHE_PARTIE_HAUTE_HAUTEUR_2_CUBE)
                ).BuildActionEnSerie();
        }

        public override Action ActionPoserVentouze()
        {
            return new ActionBuilder("Aller en bas et Poser ventouse")
                 .Add(ActionGoToZoneBasse())
                 .Add(
                     new ActionBuilder("Poser ventouse").BuildActionServoAbsolue(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW,
                     DonneesServo.ANGLE_PR_ASCENSEURGAUCHE_PARTIE_BASSE_MINIMUM)
                 ).Add(
                new ActionBuilder("Rotation poss ventouse").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW,
                    DonneesServo.VITESSE_PR_ASCENSEURGAUCHE_APPUYER_VENTOUZE, DonneesServo.DELAI_PR_ASCENSEURGAUCHE_APPUYER_VENTOUZE)
                ).Add(
                new ActionBuilder("Rotation poss ventouse").BuildActionServoRotation(Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW,
                    DonneesServo.VITESSE_PR_ASCENSEURGAUCHE_ENLEVER_VENTOUZE, DonneesServo.DELAI_PR_ASCENSEURGAUCHE_ENLEVER_VENTOUZE)
                ).BuildActionEnSerie();
        }

    }
}
