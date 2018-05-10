using System;
using Microsoft.SPOT;

using Robot_P16.Map;
using Robot_P16.Robot.composants.BaseRoulante;
using System.Threading;


namespace Robot_P16.Robot.composants.BaseRoulante
{
    public enum MOVETYPES
    {
        GoTo = 1, AdjustAngle = 2, GoToAndAdjustAngle = 3
    };


    public delegate void BaseRoulanteInstructionCompleted();

    public class BaseRoulante : Composant
    {
        public Kangaroo kangaroo;

        private Mouvement CURRENT_MOUVEMENT = null;

        public event BaseRoulanteInstructionCompleted InstructionCompletedEvent;


        public OBSTACLE_DIRECTION direction = OBSTACLE_DIRECTION.AVANT;

        public int speedDrive = 150;// avance 10 cm par seconde
        public int speedTurn = 150; //tourne 30 degrees par seconde

        public int REFRESH_RATE_KANGAROO = 200;


        int PARAMETER_FOR_XY = 1;//l'unite de la dist. = millimetre, on n'accepte QUE l'entier
        int PARAMETER_FOR_THETA = 100;//l'unite de l'angle = millidegree, on accepte l'entree de la forme X.XX degrees

        public BaseRoulante(int socket)
            : base(socket)
        {
            this.kangaroo = new Kangaroo(socket);

            /*Gadgeteer.Timer timer = new Gadgeteer.Timer(REFRESH_RATE_EVENT);
            timer.Tick += (Gadgeteer.Timer t) => kangaroo.CheckMovingStatus();
            timer.Start();*/

        }

        public void setPosition(double x, double y, double theta)
        {
            this.kangaroo.setPosition(x, y, theta);
        }


        public PointOriente GetPosition()
        {
            return this.kangaroo.GetStaticPosition();
        }


        public OBSTACLE_DIRECTION GetDirection()
        {
            return this.direction;
        }

        public void LaunchMovingInstructionCompletedEvent()
        {
            Informations.printInformations(Priority.MEDIUM, "BaseRoulante : LaunchMovingInstructionCompletedEvent called");
            if (this.InstructionCompletedEvent != null ){
                Informations.printInformations(Priority.MEDIUM, "BaseRoulante - LaunchMovingInstructionCompletedEvent : listeners found !!!");
                this.InstructionCompletedEvent();
            }
            else
            {
                Informations.printInformations(Priority.MEDIUM, "BaseRoulante - LaunchMovingInstructionCompletedEvent : no listeners found");
            }
        }
    }
}
