using System;
using System.Collections;
using Microsoft.SPOT;
using Robot_P16.Robot.composants.Servomoteurs;
using Robot_P16.Robot;
using Robot_P16.Map;

namespace Robot_P16.Actions
{
    public class ActionBuilder
    {
        private ArrayList liste = new ArrayList();
        public String description;

        public ActionBuilder(String description)
        {
            this.description = description;
        }
        public ActionBuilder() : this(null) { }

        public ActionBuilder Add(Action a)
        {
            liste.Add(a);
            return this;
        }

        public ActionEnSerie BuildActionEnSerie()
        {
            Action[] listeActions = new Action[liste.Count];
            int i = 0;
            foreach (Object o in liste)
                listeActions[i++] = (Action)o;
            return new ActionEnSerie(listeActions, description);
        }
        public ActionEnParallele BuildActionEnParallele()
        {
            Action[] listeActions = new Action[liste.Count];
            int i = 0;
            foreach (Object o in liste)
                listeActions[i++] = (Action)o;
            return new ActionEnParallele(listeActions, description);
        }

        public ActionWait BuildActionWait(int duration)
        {
            return new ActionWait(description, duration);
        }

        public ActionRepeated BuildActionUniqueRepeated(Action a, int compteur)
        {
            return new ActionRepeated(description, a, compteur);
        }

        public ActionRepeated BuildActionEnSerieRepeated(int compteur)
        {
            return new ActionRepeated(description + " - serieRepeated "+compteur, this.BuildActionEnSerie(), compteur);
        }


        public ActionGetPosition BuildActionGetPosition()
        {
            return new ActionGetPosition(description);
        }

        public ActionServo BuildActionServo(AX12 servomoteur, ServoCommandTypes commandType, float angle)
        {
            return new  ActionServo(description, servomoteur, commandType, angle);
        }

        public ActionServoAbsolue BuildActionServoAbsolue(AX12 servomoteur, int angle)
        {
            return new ActionServoAbsolue(description, servomoteur, angle);
        }
        public ActionServoAbsolue BuildActionServoAbsolue(AX12 servomoteur, int angle, int duration)
        {
            return new ActionServoAbsolue(description, servomoteur, angle, duration);
        }

        public ActionServoRotation BuildActionServoRotation(AX12 servomoteur, speed direction, int duration)
        {
            return new ActionServoRotation(description, servomoteur, direction, duration);
        }
        public ActionServoRotation BuildActionServoRotation(AX12 servomoteur, int vitesse, int duration)
        {
            return new ActionServoRotation(description, servomoteur, vitesse, duration);
        }
        public ActionVentouze BuildActionVentouze(VENTOUZES ventouze, bool activate)
        {
            return new ActionVentouze(description, ventouze, activate);
        }

        public ActionDelegate BuildActionDelegate(VoidFunc method) {
            return new ActionDelegate(description, method);
        }

        public ActionBaseRoulante BuildActionBaseRoulante_GOTO_ONLY(PointOriente pt)
        {
            return new ActionBaseRoulante(this.description, new Robot.composants.BaseRoulante.Mouvement(pt));
        }
        public ActionBaseRoulante BuildActionBaseRoulante_GOTO_ANGLE(PointOriente pt)
        {
            return new ActionBaseRoulante(this.description, new Robot.composants.BaseRoulante.Mouvement(pt,true));
        }
        public ActionBaseRoulante BuildActionBaseRoulante_GOTO_ONLY(PointOriente pt, OBSTACLE_DIRECTION forcedDirection)
        {
            return new ActionBaseRoulante(this.description, new Robot.composants.BaseRoulante.Mouvement(pt, forcedDirection));
        }
        public ActionBaseRoulante BuildActionBaseRoulante_GOTO_ANGLE(PointOriente pt, OBSTACLE_DIRECTION forcedDirection)
        {
            return new ActionBaseRoulante(this.description, new Robot.composants.BaseRoulante.Mouvement(pt, true, forcedDirection));
        }
        public ActionDelegate BuildActionBaseRoulante_DRIVE(int distance, int vitesse) {
            return new ActionDelegate(this.description, () => Robot.Robot.robot.BASE_ROULANTE.kangaroo.drive(distance, vitesse));
        }

        public ActionDelegate BuildActionBaseRoulante_TURN(double angle, int vitesse)
        {
            return new ActionDelegate(this.description, () => Robot.Robot.robot.BASE_ROULANTE.kangaroo.rotate(angle, vitesse));

        }
        public ActionDelegate BuildActionLanceurBalle(double speed)
        {
            return new ActionDelegate(this.description, () => Robot.Robot.robot.GR_LANCEUR_BALLE.launchSpeed(speed));
        }
        public ActionDelegate BuildActionLanceurBalleStop()
        {
            return new ActionDelegate(this.description, () => Robot.Robot.robot.GR_LANCEUR_BALLE.stop());
        }

        public ActionDelegate BuildActionRecallageAxeX(double newY, int timeSleep, int speed, int distance, double angle)
        {
            return new ActionDelegate(this.description, () => Robot.Robot.robot.BASE_ROULANTE.kangaroo.RecallageX(newY, timeSleep, speed, distance, angle));
        }
        public ActionDelegate BuildActionRecallageAxeY(double newX, int timeSleep, int speed, int distance, double angle)
        {
            return new ActionDelegate(this.description, () => Robot.Robot.robot.BASE_ROULANTE.kangaroo.RecallageY(newX, timeSleep, speed, distance,angle));
        }
        /*public ActionBaseRoulante BuildActionBaseRoulante_GOTO_ONLY(TypeDeLieu typeDeLieu)
        {
            return new ActionBaseRoulante(this.description, new Robot.composants.BaseRoulante.Mouvement(pt, true));
        }
        public ActionBaseRoulante BuildActionBaseRoulante_GOTO_ONLY(TypeDeLieu typeDeLieu)
        {
            return new ActionBaseRoulante(this.description, new Robot.composants.BaseRoulante.Mouvement(pt, true));
        }*/

        public ActionDelegate BuildActionSetPositionInitiale(double x, double y, double theta)
        {
            return new ActionDelegate(this.description, () => Robot.Robot.robot.BASE_ROULANTE.kangaroo.setPosition(x,y,theta));
        }

        public ActionDelegate BuildActionSetDetecteurObstacle(bool isOn)
        {
            return new ActionDelegate(this.description, () => Map.MapInformation.isDetecteurOn = isOn);
        }
    }
}
