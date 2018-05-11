using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using Gadgeteer.Networking;
using GT = Gadgeteer;
using Robot_P16.Robot;
using System.IO.Ports;
using Robot_P16.Robot.composants.Servomoteurs;
using System.Threading;

namespace Robot_P16.Actions
{
    public class ActionRamasseCube : Action
    {
        private GestionnaireServosPR gestionnaire;
        private CouleurEquipe couleurEquipe;

        public ActionRamasseCube()
            : base("Ramasse des cubes")
        {
            this.gestionnaire = new GestionnaireServosPR();
            this.couleurEquipe = Robot.Robot.robot.Couleur;
        }



        public Action getAction()
        {
            Action action = null;
            string codeCouleur = Robot.Robot.robot.codeCouleur;
            /*Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()*/

            if (couleurEquipe == CouleurEquipe.VERT)
            {
                Debug.Print("Vert" + codeCouleur);
                switch (codeCouleur)
                {
                    case "J-N-B"://Cas de base du bras gauche : hauteur 1 cube bien aligne sur le cube gauche
                    case "B-N-J":
                       action = new ActionBuilder("J-N-B Vert").Add(
                           gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                           ).Add(
                           new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                           ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                           ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                           ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                           ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                           ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                           ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                           ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                           ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                           ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                           ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                           ).Add(new ActionBuilder("wait").BuildActionWait(200)
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                           ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                           ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                           /*).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                           ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                           ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER*/
                           ).BuildActionEnSerie();

                       return action;
                    //return new int[] {3,14,5}; //ok
                    case "B-V-O":
                    case "O-V-B":
                        action = new ActionBuilder("Action B-V-O Vert").Add(
                            new ActionBuilder("Tourner bras droit et avancer bras gauche paralle").Add(
                                gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR).Add(
                                gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL)
                                .BuildActionEnSerie()
                            ).Add(
                            new ActionBuilder("Ventouse droit").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(
                            new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(
                            new ActionBuilder("desactiver ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(
                            new ActionBuilder("Wait ventouze").BuildActionWait(200)
                            ).Add(
                            new ActionBuilder("Rotationner droit /ventouse + descendre gauche").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU).Add(
                                    new ActionBuilder("Ventouse + descendregauche").Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()).BuildActionEnSerie()
                                    ).BuildActionEnSerie()
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                           gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                    
                    case "V-O-J":
                    case "J-O-V":
                           action = new ActionBuilder("Action B-V-O Vert").Add(
                            new ActionBuilder("Tourner bras droit et avancer bras gauche paralle").Add(
                                gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR).Add(
                                gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL)
                                .BuildActionEnSerie()
                            ).Add(
                            new ActionBuilder("Ventouse droit").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(
                            new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(
                            new ActionBuilder("desactiver ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(
                            new ActionBuilder("Wait ventouze").BuildActionWait(200)
                            ).Add(
                            new ActionBuilder("Rotationner droit /ventouse + descendre gauche").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU).Add(
                                    new ActionBuilder("Ventouse + descendregauche").Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()).BuildActionEnSerie()
                                    ).BuildActionEnSerie()
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                           gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {1,13,5};//ok
                    case "O-N-V":
                    case "V-N-O":
                        action = new ActionBuilder("O-N-V Vert").Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(
                            new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(
                            new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(
                            new ActionBuilder("descative ventouze gauche / Activer ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(
                            new ActionBuilder("Rotate bras gauche/ attrape cube droit"
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                                ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                                ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                                ).BuildActionEnSerie()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_3emeCube() //Monter zone avant?
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Desactive Ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE,false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {1,14,5}; // 1 and 14 ok, should be ok
                    case "V-J-B":
                    case "B-J-V":
                        action = new ActionBuilder("Action V-J-B Vert").Add(
                            new ActionBuilder("Activer ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(        
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(
                            new ActionBuilder("Desactiver ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(300)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(
                            new ActionBuilder("Activer ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze())
                            .Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive venotuze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                            ).BuildActionEnSerie();
                        return action;
                        //return new int[] {2,13,5}; // si le cube 2 n'est pas bien pose, il empeche de prendre le cube 3, faut il le recaller ?
                    case "N-J-O":
                    case "O-J-N":
                        action = new ActionBuilder("N-J-O vert").Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(
                            new ActionBuilder("Activer ventouse Gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER
                            ).Add(
                            new ActionBuilder("Desactiver ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                            new ActionBuilder("Wait the ventouZe").BuildActionWait(200)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU //Faudra penser à recaller le cube quand on aura la position absolue
                            ).Add(
                            new ActionBuilder("Activer ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(
                            new ActionBuilder("Desactiver ventouzedroite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(
                            new ActionBuilder("Wait the ventouZe").BuildActionWait(200)
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU //Puis revenir en position basse pour le  truc suivant
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                            ).BuildActionEnSerie();
                        return action;
                            
                        //return new int[] {3,14,5}; // Idee de genie d'Antonin, tourner le cube (car celui poser sur le cube du milieu bloque
                    case "B-O-N":
                    case "N-O-B":
                        action = new ActionBuilder("BON Vert").Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR //c'est important de le faire avant car ca bloquera sinon
                                   ).Add(new ActionBuilder("Ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                                   ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                                   ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                   ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                                   ).Add(new ActionBuilder("wait").BuildActionWait(200)
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                                   ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                                   ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_3emeCube()
                                   ).Add(new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                                   ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                                   ).Add(new ActionBuilder("wait").BuildActionWait(200)
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                                   ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                                   ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                                   ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;

                    case "J-V-N":
                    case "N-V-J":
                        action = new ActionBuilder("J-V-N Vert").Add(
                            new ActionBuilder("Deploiyer et tourner"
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                ).BuildActionEnSerie()
                            ).Add(new ActionBuilder("Ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(new ActionBuilder("Avancer et ramener bras"
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                                ).Add(new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                                ).BuildActionEnSerie()
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL // puis go to hauteur et recalle le cube
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {3,6,13};
                    case "N-B-V":
                    case "V-B-N":
                        action = new ActionBuilder("N-B-V Vert").Add(
                            new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE,false)
                            ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                            //return new int[] {4,12,5};
                    case "O-B-J":
                    case "J-B-O":
                        action = new ActionBuilder("Action V-J-B Vert").Add(
                            new ActionBuilder("Activer ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(        
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(
                            new ActionBuilder("Desactiver ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(300)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(
                            new ActionBuilder("Activer ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze())
                            .Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive venotuze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                            ).BuildActionEnSerie();
                        return action;
                    default:
                        action = new ActionBuilder("O-B-J Orange"
                            ).Add(new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE,false)
                            ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                }
            }
               if (couleurEquipe==CouleurEquipe.ORANGE){
                   Debug.Print("Orange" + codeCouleur);
                       switch(codeCouleur){
                    case "J-N-B":
                    case "B-N-J":
                               action = new ActionBuilder("N-B-V Vert").Add(
                            new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE,false)
                            ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {4,12,5};
                    case "B-V-O":
                    case "O-V-B":
                               action = new ActionBuilder("Action B-V-O Orange").Add(
                            new ActionBuilder("Tourner bras droit et avancer bras gauche paralle").Add(
                                gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR).Add(
                                gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL)
                                .BuildActionEnSerie()
                            ).Add(
                            new ActionBuilder("Ventouse droit").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(
                            new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(
                            new ActionBuilder("desactiver ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(
                            new ActionBuilder("Wait ventouze").BuildActionWait(200)
                            ).Add(
                            new ActionBuilder("Rotationner droit /ventouse + descendre gauche").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU).Add(
                                    new ActionBuilder("Ventouse + descendregauche").Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()).BuildActionEnSerie()
                                    ).BuildActionEnSerie()
                            ).Add(
                            Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                           gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                           ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                               
                        //return new int[] {1,13,5};
                    case "V-O-J":
                    case "J-O-V":
                        action = new ActionBuilder("Action V-O-J Orange").Add(
                     new ActionBuilder("Tourner bras droit et avancer bras gauche paralle").Add(
                         gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR).Add(
                         gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL)
                         .BuildActionEnSerie()
                     ).Add(
                     new ActionBuilder("Activer ventouse bras droit et le baisser, et tourner gauche").Add(
                         new ActionBuilder("activer ventouse et baisser droit").Add(
                             new ActionBuilder("Ventouse droit").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()).BuildActionEnSerie()
                         ).Add(
                         gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                         ).BuildActionEnSerie()
                     ).Add(
                     Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                     ).Add(
                     new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                     ).Add(
                     new ActionBuilder("desactiver ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                     ).Add(
                     new ActionBuilder("Wait ventouze").BuildActionWait(200)
                     ).Add(
                     new ActionBuilder("Rotationner droit /ventouse + descendre gauche").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU).Add(
                             new ActionBuilder("Ventouse + descendregauche").Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()).BuildActionEnSerie()
                             ).BuildActionEnSerie()
                     ).Add(
                     Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                     ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                     ).Add(
                     new ActionBuilder("Desactive ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                     ).Add(
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                     ).Add(
                     gestionnaire.PR_POUSSOIRJOKER_POUSSER
                     ).Add(
                     gestionnaire.PR_POUSSOIRJOKER_RETOUR
                     ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                     ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                     ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {1,13,5};
                    case "O-N-V":
                    case "V-N-O":
                        action = new ActionBuilder("ONV Orange").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(new ActionBuilder("ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {1,12,5}; // 1,2,5 ??
                    case "V-J-B":
                    case "B-J-V":
                        action = new ActionBuilder("V-J-B Orange").Add(
                            new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE,false)
                            ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {4,13,5};
                    case "N-J-O":
                    case "O-J-N":
                               action = new ActionBuilder("NJO Orange"
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                               return action;
                        //return new int[] {3,12,5};
                    case "B-O-N":
                    case "N-O-B":
                               action = new ActionBuilder("BON Orange"
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR //c'est important de le faire avant car ca bloquera sinon
                                   ).Add(new ActionBuilder("Ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                                   ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                                   ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                   ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER
                                   ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                                   ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_3emeCube()
                                   ).Add(new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                                   ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                                   ).Add(new ActionBuilder("wait").BuildActionWait(200)
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                                   ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                                   ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                                   ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                               return action;

                        //return new int[] {7,11,5}; // Le cube tombe lors du replacage au milieu (changer la speed du deploiement interieur ?
                    case "J-V-N":
                    case "N-V-J":
                               action = new ActionBuilder("J-V-N Orange").Add(
                            new ActionBuilder("Deploiyer et tourner"
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                ).BuildActionEnSerie()
                            ).Add(new ActionBuilder("Ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(new ActionBuilder("Avancer et ramener bras"
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                                ).Add(new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                                ).BuildActionEnSerie()
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL // puis go to hauteur et recalle le cube
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                               return action;
                        //return new int[] {3,6,3};
                    case "N-B-V":
                    case "V-B-N":
                        action = new ActionBuilder("N-B-V  Orange"
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("attente").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(new ActionBuilder("attente").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                            //return new int[] {2,14,5};
                    case "O-B-J":
                    case "J-B-O":
                        action = new ActionBuilder("O-B-J Orange"
                            ).Add(new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE,false)
                            ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {4,13,5};
                    default:
                        action = new ActionBuilder("O-B-J Orange"
                            ).Add(new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE,false)
                            ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                    }
                 }
               else
               {
                   Debug.Print("Code couleur non valide");
                   action = new ActionBuilder("O-B-J Orange"
                            ).Add(new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_DROIT_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionPoserVentouze()
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_3emeCube()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(Robot.Robot.robot.PR_SERVO_ASCENSEUR_BRAS_GAUCHE_NEW.ActionHauteurPose_2emeCube()
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                   return action;
                   //return new int[] { 4, 2, 5 }; // histoire de faire une pile
               }
                return action;    
               }
            
       

        public override void Execute()
        {
            Action a  = getAction();
            a.StatusChangeEvent += (x) =>
            {
                if (x.Status == ActionStatus.SUCCESS)
                {
                    Informations.printInformations(Priority.HIGH, "Action Ramasse cube sucessful.");
                    Status = ActionStatus.SUCCESS;
                }
            };
            a.Execute();

            
            
            
            
            /*int[] sequence = getSequence();
            for(int i = 0; i<sequence.Length; i++){
                Debug.Print("Making move" + sequence[i].ToString());
                makeMove(sequence[i]);
            }*/
        }

        public override void Feedback(Action a)
        {
            return;
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            return true;
        }
    }
}