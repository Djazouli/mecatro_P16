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
                           ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                           ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                           ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                           ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                           ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                           ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                           ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                           ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                           ).Add(new ActionBuilder("wait").BuildActionWait(200)
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                           ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                           ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                           ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                           ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
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
                            new ActionBuilder("Activer ventouse bras droit et le baisser, et tourner gauche").Add(
                                new ActionBuilder("activer ventouse et baisser droit").Add(
                                    new ActionBuilder("Ventouse droit").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1).BuildActionEnSerie()
                                ).Add(
                                gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                ).BuildActionEnSerie()
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                            ).Add(
                            new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(
                            new ActionBuilder("desactiver ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(
                            new ActionBuilder("Wait ventouze").BuildActionWait(200)
                            ).Add(
                            new ActionBuilder("Rotationner droit /ventouse + descendre gauche").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU).Add(
                                    new ActionBuilder("Ventouse + descendregauche").Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE).BuildActionEnSerie()
                                    ).BuildActionEnSerie()
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                           gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {1,13,5};
                //} //a commenter
            //} // a commenter
                    
                    case "V-O-J":
                    case "J-O-V":
                        action = new ActionBuilder("Action V-O-J Vert").Add(
                            new ActionBuilder("Tourner bras droit et avancer bras gauche paralle").Add(
                                gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR).Add(
                                gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL)
                                .BuildActionEnSerie()
                            ).Add(
                            new ActionBuilder("Activer ventouse bras droit et le baisser, et tourner gauche").Add(
                                new ActionBuilder("activer ventouse et baisser droit").Add(
                                    new ActionBuilder("Ventouse droit").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1).BuildActionEnSerie()
                                ).Add(
                                gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                ).BuildActionEnSerie()
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                            ).Add(
                            new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(
                            new ActionBuilder("desactiver ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(
                            new ActionBuilder("Wait ventouze").BuildActionWait(200)
                            ).Add(
                            new ActionBuilder("Rotationner droit /ventouse + descendre gauche").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU).Add(
                                    new ActionBuilder("Ventouse + descendregauche").Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE).BuildActionEnSerie()
                                    ).BuildActionEnSerie()
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                           gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
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
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(
                            new ActionBuilder("descative ventouze gauche / Activer ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(
                            new ActionBuilder("Rotate bras gauche/ attrape cube droit"
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                                ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                                ).BuildActionEnSerie()
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_GOTO1000
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_ZONE_BAS_VERS_HAUT
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_3
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Desactive Ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE,false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(
                            new ActionBuilder("Poussoir et retour intial").Add(gestionnaire.PR_BRAS_DROIT_GOTO0).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER).BuildActionEnSerie()
                            ).Add(
                            new ActionBuilder("retour poussoir et initial").Add(gestionnaire.PR_BRAS_DROIT_ZONE_HAUT_VERS_BAS).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR).BuildActionEnSerie()
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
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
                            gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(
                            new ActionBuilder("Desactiver ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(400)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(
                            new ActionBuilder("Activer ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE)
                            .Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive venotuze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
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
                            gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER
                            ).Add(
                            new ActionBuilder("Desactiver ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                            new ActionBuilder("Wait the ventouZe").BuildActionWait(200)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU //Faudra penser à recaller le cube quand on aura la position absolue
                            ).Add(
                            new ActionBuilder("Activer ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_GOTO1000
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_ZONE_BAS_VERS_HAUT
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_3
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(
                            new ActionBuilder("Desactiver ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                            new ActionBuilder("Wait the ventouZe").BuildActionWait(200)
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU //Puis revenir en position basse pour le  truc suivant
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_GOTO0
                            ).Add(gestionnaire.PR_BRAS_DROIT_ZONE_HAUT_VERS_BAS
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
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
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                   ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                                   ).Add(new ActionBuilder("wait").BuildActionWait(200)
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                                   ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                                   ).Add(gestionnaire.PR_BRAS_DROIT_GOTO1000
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ZONE_BAS_VERS_HAUT
                                   ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_3
                                   ).Add(new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                                   ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                                   ).Add(new ActionBuilder("wait").BuildActionWait(200)
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                                   ).Add(gestionnaire.PR_BRAS_DROIT_GOTO0
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ZONE_HAUT_VERS_BAS
                                   ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                                   ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        //action = null; // Voir avec Juju j'ai mis 7-11-5 en attendant
                        return action;
                        //return new int[] {4,11,5}; // !!!!!!!!!! Ne marche pas
                    case "J-V-N":
                    case "N-V-J":
                        action = new ActionBuilder("J-V-N Vert").Add(
                            new ActionBuilder("Deploiyer et tourner"
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                ).BuildActionEnSerie()
                            ).Add(new ActionBuilder("Ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(new ActionBuilder("Avancer et ramener bras"
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                                ).Add(new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                                ).BuildActionEnSerie()
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL // puis go to hauteur et recalle le cube
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
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
                            ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                            ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE,false)
                            ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
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
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(
                            new ActionBuilder("Desactiver ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(
                            new ActionBuilder("Activer ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive venotuze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        
                        //return new int[] {2,13,5};
                }
            }
               if (couleurEquipe==CouleurEquipe.ORANGE){
                   Debug.Print("Orange" + codeCouleur);
                       switch(codeCouleur){
                    case "J-N-B":
                    case "B-N-J":
                               action = new ActionBuilder("Action J-N-B Vert").Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(
                            new ActionBuilder("Activer ventouse Gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
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
                            gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_GOTO1000
                            ).Add(gestionnaire.PR_BRAS_DROIT_ZONE_BAS_VERS_HAUT
                            
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_3
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(
                            new ActionBuilder("Desactiver ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                            new ActionBuilder("Wait the ventouZe").BuildActionWait(200)
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU //Puis revenir en position basse pour le  truc suivant
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_DROIT_GOTO0
                            ).Add(gestionnaire.PR_BRAS_DROIT_ZONE_HAUT_VERS_BAS
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                       return action;
                        //return new int[] {4,12,5};
                    case "B-V-O":
                    case "O-V-B":
                               action = new ActionBuilder("Action B-V-O Vert").Add(
                            new ActionBuilder("Tourner bras droit et avancer bras gauche paralle").Add(
                                gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR).Add(
                                gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL)
                                .BuildActionEnSerie()
                            ).Add(
                            new ActionBuilder("Activer ventouse bras droit et le baisser, et tourner gauche").Add(
                                new ActionBuilder("activer ventouse et baisser droit").Add(
                                    new ActionBuilder("Ventouse droit").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1).BuildActionEnSerie()
                                ).Add(
                                gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                ).BuildActionEnSerie()
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                            ).Add(
                            new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(
                            new ActionBuilder("desactiver ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(
                            new ActionBuilder("Wait ventouze").BuildActionWait(200)
                            ).Add(
                            new ActionBuilder("Rotationner droit /ventouse + descendre gauche").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU).Add(
                                    new ActionBuilder("Ventouse + descendregauche").Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE).BuildActionEnSerie()
                                    ).BuildActionEnSerie()
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                           gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                               
                        //return new int[] {1,13,5};
                    case "V-O-J":
                    case "J-O-V":
                        action = new ActionBuilder("Action V-O-J Vert").Add(
                     new ActionBuilder("Tourner bras droit et avancer bras gauche paralle").Add(
                         gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR).Add(
                         gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL)
                         .BuildActionEnSerie()
                     ).Add(
                     new ActionBuilder("Activer ventouse bras droit et le baisser, et tourner gauche").Add(
                         new ActionBuilder("activer ventouse et baisser droit").Add(
                             new ActionBuilder("Ventouse droit").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1).BuildActionEnSerie()
                         ).Add(
                         gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                         ).BuildActionEnSerie()
                     ).Add(
                     gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                     ).Add(
                     new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                     ).Add(
                     new ActionBuilder("desactiver ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                     ).Add(
                     new ActionBuilder("Wait ventouze").BuildActionWait(200)
                     ).Add(
                     new ActionBuilder("Rotationner droit /ventouse + descendre gauche").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU).Add(
                             new ActionBuilder("Ventouse + descendregauche").Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE).BuildActionEnSerie()
                             ).BuildActionEnSerie()
                     ).Add(
                     gestionnaire.PR_BRAS_GAUCHE_MONTER
                     ).Add(
                     gestionnaire.PR_BRAS_GAUCHE_MONTER
                     ).Add(
                     gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                     ).Add(
                     new ActionBuilder("Desactive ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                     ).Add(
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                     ).Add(
                     gestionnaire.PR_POUSSOIRJOKER_POUSSER
                     ).Add(
                     gestionnaire.PR_POUSSOIRJOKER_RETOUR
                     ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                     ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                     ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                     ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {1,13,5};
                    case "O-N-V":
                    case "V-N-O":
                        action = new ActionBuilder("ONV Orange").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                            ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                            ).Add(new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(new ActionBuilder("ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
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
                            ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                            ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE,false)
                            ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {4,13,5};
                    case "N-J-O":
                    case "O-J-N":
                               action = new ActionBuilder("NJO Orange").Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                               return action;
                        //return new int[] {3,12,5};
                    case "B-O-N":
                    case "N-O-B":
                               action = new ActionBuilder("BON Orange").Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR //c'est important de le faire avant car ca bloquera sinon
                                   ).Add(new ActionBuilder("Ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                   ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER
                                   ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                                   ).Add(gestionnaire.PR_BRAS_DROIT_GOTO1000
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ZONE_BAS_VERS_HAUT
                                   ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_3
                                   ).Add(new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                                   ).Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                                   ).Add(new ActionBuilder("wait").BuildActionWait(200)
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                                   ).Add(gestionnaire.PR_BRAS_DROIT_GOTO0
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ZONE_HAUT_VERS_BAS
                                   ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                                   ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                                   ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                                   ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                               return action;

                        //return new int[] {7,11,5}; // Le cube tombe lors du replacage au milieu (changer la speed du deploiement interieur ?
                    case "J-V-N":
                    case "N-V-J":
                               action = new ActionBuilder("J-V-N Vert").Add(
                            new ActionBuilder("Deploiyer et tourner"
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                ).BuildActionEnSerie()
                            ).Add(new ActionBuilder("Ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(new ActionBuilder("Avancer et ramener bras"
                                ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                                ).Add(new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                                ).BuildActionEnSerie()
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL // puis go to hauteur et recalle le cube
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouse").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDRE
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                               return action;
                        //return new int[] {3,6,3};
                    case "N-B-V":
                    case "V-B-N":
                        action = new ActionBuilder("N-B-V  Orange").Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("attente").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                            ).Add(gestionnaire.PR_BRAS_DROIT_GOTO1000
                            ).Add(gestionnaire.PR_BRAS_DROIT_ZONE_BAS_VERS_HAUT
                            ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_3
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(new ActionBuilder("attente").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_DROIT_GOTO0
                            ).Add(gestionnaire.PR_BRAS_DROIT_ZONE_HAUT_VERS_BAS
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                            //return new int[] {2,14,5};
                    case "O-B-J":
                    case "J-B-O":
                        action = new ActionBuilder("N-B-V Vert").Add(
                            new ActionBuilder("Ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)
                            ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1
                            ).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("desactive ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE,false)
                            ).Add(new ActionBuilder("wait venouze").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                            ).Add(new ActionBuilder("Ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(new ActionBuilder("ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(new ActionBuilder("wait").BuildActionWait(200)
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                        return action;
                        //return new int[] {4,13,5};
                    default:
                        action = new ActionBuilder("Action B-V-O Vert").Add(
                            new ActionBuilder("Tourner bras droit et avancer bras gauche paralle").Add(
                                gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR).Add(
                                gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL)
                                .BuildActionEnSerie()
                            ).Add(
                            new ActionBuilder("Activer ventouse bras droit et le baisser, et tourner gauche").Add(
                                new ActionBuilder("activer ventouse et baisser droit").Add(
                                    new ActionBuilder("Ventouse droit").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1).BuildActionEnSerie()
                                ).Add(
                                gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                ).BuildActionEnSerie()
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                            ).Add(
                            new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(
                            new ActionBuilder("desactiver ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(
                            new ActionBuilder("Wait ventouze").BuildActionWait(200)
                            ).Add(
                            new ActionBuilder("Rotationner droit /ventouse + descendre gauche").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU).Add(
                                    new ActionBuilder("Ventouse + descendregauche").Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE).BuildActionEnSerie()
                                    ).BuildActionEnSerie()
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                           gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie(); // C'est juste histoire de faire une pile
                        return action;
                    }
                 }
               else
               {
                   Debug.Print("Code couleur non valide");
                   action = new ActionBuilder("Action B-V-O Vert").Add(
                            new ActionBuilder("Tourner bras droit et avancer bras gauche paralle").Add(
                                gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR).Add(
                                gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL)
                                .BuildActionEnSerie()
                            ).Add(
                            new ActionBuilder("Activer ventouse bras droit et le baisser, et tourner gauche").Add(
                                new ActionBuilder("activer ventouse et baisser droit").Add(
                                    new ActionBuilder("Ventouse droit").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true)).Add(gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1).BuildActionEnSerie()
                                ).Add(
                                gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR
                                ).BuildActionEnSerie()
                            ).Add(
                            gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2
                            ).Add(
                            new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30 * 100)
                            ).Add(
                            new ActionBuilder("desactiver ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false)
                            ).Add(
                            new ActionBuilder("Wait ventouze").BuildActionWait(200)
                            ).Add(
                            new ActionBuilder("Rotationner droit /ventouse + descendre gauche").Add(gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU).Add(
                                    new ActionBuilder("Ventouse + descendregauche").Add(new ActionBuilder("ventouze").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true)).Add(gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE).BuildActionEnSerie()
                                    ).BuildActionEnSerie()
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_MONTER
                            ).Add(
                            gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE
                            ).Add(
                            new ActionBuilder("Desactive ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false)
                            ).Add(
                           gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU
                            ).Add(
                            gestionnaire.PR_POUSSOIRJOKER_POUSSER
                            ).Add(gestionnaire.PR_POUSSOIRJOKER_RETOUR
                            ).Add(gestionnaire.PR_BRAS_DROIT_ROTATION_COINCER
                            ).Add(gestionnaire.PR_BRAS_GAUCHE_ROTATION_COINCER
                                ).BuildActionEnSerie();
                   return action;
                   //return new int[] { 4, 2, 5 }; // histoire de faire une pile
               }
                return action;    
               }
            
        

        /*private void makeMove(int numMove)
        {
            switch (numMove)
            {
                case 1: 
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Active ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true).Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1.Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-65, 30*100).Execute();
                    new ActionBuilder("Desactive ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false).Execute();
                    Thread.Sleep(200);
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    break;
                case 11: 
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Active ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true).Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1.Execute();
                    gestionnaire.PR_BRAS_DROIT_GOTO1000.Execute();
                    gestionnaire.PR_BRAS_DROIT_ZONE_BAS_VERS_HAUT.Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_3.Execute();
                    new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-58, 30 * 100).Execute();
                    new ActionBuilder("Desactive ventouse droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false).Execute();
                    Thread.Sleep(200);
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    gestionnaire.PR_BRAS_DROIT_GOTO0.Execute();
                    gestionnaire.PR_BRAS_DROIT_ZONE_HAUT_VERS_BAS.Execute();
                    break;
                case 2: // Supposons que le bras gauche est initialement au dessus du premier cube, non deployé (sinon on le pousse en arrivant)
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE.Execute();
                    new ActionBuilder("Active ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true).Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();// C'est en position relative j'espere que ca va
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Desactive ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false).Execute();
                    Thread.Sleep(300);
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU.Execute();
                    break;
                case 12:
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE.Execute();
                    new ActionBuilder("Active ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true).Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    //gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    //Thread.Sleep(200);
                    new ActionBuilder("Desactive ventouse gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false).Execute();
                    Thread.Sleep(200);
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    break;
                case 3:
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Active ventouse Gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true).Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Desactive Ventouse Gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false).Execute();
                    Thread.Sleep(300);
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU.Execute();
                    break;
                case 13:
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Active ventouse Gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true).Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_RENTRER.Execute();
                    new ActionBuilder("Desactive Ventouse Gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false).Execute();
                    Thread.Sleep(300);
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU.Execute();
                    break;
                case 4:
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    new ActionBuilder("Activer ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true).Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1.Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Desactiver ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false).Execute();
                    Thread.Sleep(200);
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    break;
                case 14:
                    //gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_2.Execute(50);
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    new ActionBuilder("Activer ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, true).Execute();
                    Thread.Sleep(500);
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_RAMASSER_CUBE_1.Execute();
                    gestionnaire.PR_BRAS_DROIT_GOTO1000.Execute(500);
                    gestionnaire.PR_BRAS_DROIT_ZONE_BAS_VERS_HAUT.Execute();
                    gestionnaire.PR_BRAS_DROIT_HAUTEUR_POSER_CUBE_3.Execute();
                    gestionnaire.PR_BRAS_DROIT_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Desactiver ventouze droite").BuildActionVentouze(VENTOUZES.VENTOUZE_DROITE, false).Execute();
                    Thread.Sleep(300);
                    gestionnaire.PR_BRAS_DROIT_ROTATION_MILIEU.Execute();
                    gestionnaire.PR_BRAS_DROIT_GOTO0.Execute();
                    gestionnaire.PR_BRAS_DROIT_ZONE_HAUT_VERS_BAS.Execute();
                    Thread.Sleep(200);

                    break;
                case 5:
                    gestionnaire.PR_POUSSOIRJOKER_POUSSER.Execute();
                    gestionnaire.PR_POUSSOIRJOKER_RETOUR.Execute();
                    break;
                case 6://peut etre qu'on devrait bloquer les cubes avant d'avancer ?
                    new ActionBuilder("Avancer d'un cube").BuildActionBaseRoulante_DRIVE(-58, 30 * 100).Execute();
                    break;
                case 7:
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIMENT_SORTIR_CUBE_GAUCHE.Execute();
                    new ActionBuilder("Activer ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, true).Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDRE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DESCENDREPOURPOSERVENTOUSE.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_MONTER.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_DEPLOIEMENT_SORTIR_CUBE_CENTRAL.Execute();
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_INTERIEUR.Execute();
                    new ActionBuilder("Activer ventouze gauche").BuildActionVentouze(VENTOUZES.VENTOUZE_GAUCHE, false).Execute();
                    Thread.Sleep(300);
                    gestionnaire.PR_BRAS_GAUCHE_ROTATION_MILIEU.Execute();
                    break;
                default:
                    Debug.Print("ALLO CE NEST PAS UN NUMERO VALIDE DE MAKEMOVE");
                    break;
            }
        }
        */

        public override void Execute()
        {
            getAction().Execute();
            
            
            
            
            /*int[] sequence = getSequence();
            for(int i = 0; i<sequence.Length; i++){
                Debug.Print("Making move" + sequence[i].ToString());
                makeMove(sequence[i]);
            }*/
        }

        public override void Feedback(Action a)
        {
            throw new NotImplementedException();
        }

        protected override bool PostStatusChangeCheck(ActionStatus previousStatus)
        {
            throw new NotImplementedException();
        }
    }
}