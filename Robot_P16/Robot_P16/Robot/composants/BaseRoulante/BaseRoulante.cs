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

        public int speedDrive = 100;// avance 10 cm par seconde
        public int speedTurn = 70; //tourne 30 degrees par seconde

        private static int REFRESH_RATE_EVENT = 500;


        int PARAMETER_FOR_XY = 1;//l'unite de la dist. = millimetre, on n'accepte QUE l'entier
        int PARAMETER_FOR_THETA = 100;//l'unite de l'angle = millidegree, on accepte l'entree de la forme X.XX degrees

        public BaseRoulante(int socket)
            : base(socket)
        {
            this.kangaroo = new Kangaroo(socket);

            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(REFRESH_RATE_EVENT);
                    kangaroo.CheckMovingStatus();
                }
            }).Start();

        }


        public PointOriente GetPosition()
        {
            return this.kangaroo.GetDynamicPosition();
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
