using System;
using Microsoft.SPOT;
using System.Threading;
using Gadgeteer;

namespace Robot_P16.Robot.composants.CapteursObstacle
{

    public delegate void ObstacleListenerDelegate(OBSTACLE_DIRECTION direction, bool isThereAnObstacle);

    public class CapteurObstacleManager
    {
        private CapteurObstacle[] capteurs;
        private bool[] lastObstacleFoundForCapteurs;
        public event ObstacleListenerDelegate ObstacleChangeEvent;

        private static int REFRESH_RATE = 500;

        public CapteurObstacleManager(CapteurObstacle[] capteurs)
        {
            this.capteurs = capteurs;
            this.lastObstacleFoundForCapteurs = new bool[this.capteurs.Length];
            for (int i = 0; i < this.capteurs.Length; i++)
            {
                this.lastObstacleFoundForCapteurs[i] = capteurs[i].IsThereAnObstacle();
            }

            this.start();

            /*
            foreach (CapteurObstacle capteur in capteurs)
            {
                capteur.ObstacleChangeEvent += this.OnObstacle;
            }*/
        }

        private void start()
        {
            //new Thread(() => { Thread.Sleep(REFRESH_RATE); checkObstacles(); }).Start();
            Gadgeteer.Timer timer = new Gadgeteer.Timer(REFRESH_RATE);
            timer.Tick += (Gadgeteer.Timer t) => this.checkObstacles();
            timer.Start();
        }

        private void checkObstacles()
        {
            bool[] newObstacles = new bool[this.capteurs.Length];
            for (int i = 0; i < this.capteurs.Length; i++)
            {
                newObstacles[i] = capteurs[i].IsThereAnObstacle();
            }

            bool obstacleDEVANT = getObstacleStatusForDirection(OBSTACLE_DIRECTION.AVANT, newObstacles);
            bool obstacleARRIERE = getObstacleStatusForDirection(OBSTACLE_DIRECTION.ARRIERE, newObstacles);

            if (obstacleDEVANT != getObstacleStatusForDirection(OBSTACLE_DIRECTION.AVANT, this.lastObstacleFoundForCapteurs))
            {
                Informations.printInformations(Priority.MEDIUM, "CapteurObstacleManager : obstacle change (obstacle : " + obstacleDEVANT + ") in direction AVANT");
                if (this.ObstacleChangeEvent != null)
                    this.ObstacleChangeEvent(OBSTACLE_DIRECTION.AVANT, obstacleDEVANT);
            }

            if (obstacleARRIERE != getObstacleStatusForDirection(OBSTACLE_DIRECTION.ARRIERE, this.lastObstacleFoundForCapteurs))
            {
                Informations.printInformations(Priority.MEDIUM, "CapteurObstacleManager : obstacle change (obstacle : " + obstacleARRIERE + ") in direction ARRIERE");
                if (this.ObstacleChangeEvent != null)
                    this.ObstacleChangeEvent(OBSTACLE_DIRECTION.ARRIERE, obstacleARRIERE);
            }

            this.lastObstacleFoundForCapteurs = newObstacles;
        }

        private bool getObstacleStatusForDirection(OBSTACLE_DIRECTION direction, bool[] obstaclesBySensor)
        {
            for (int i = 0; i < this.capteurs.Length; i++)
            {
                if (capteurs[i].direction == direction && obstaclesBySensor[i])
                {
                    return true;
                }
            }
            return false;
        }

        // OUTDATED CODE
        /*
        private void OnObstacle(OBSTACLE_DIRECTION direction, bool isThereAnObstacle)
        {
            // TODO : Add check of the zone
            if (ObstacleChangeEvent != null)
            {
                if( isThereAnObstacle ){
                    Informations.printInformations(Priority.MEDIUM, "CapteurObstacleManager : obstacle found in direction : " + direction);
                    ObstacleChangeEvent(direction, isThereAnObstacle);
                }
                else
                {
                    foreach (CapteurObstacle capteur in capteurs)
                    {
                        if (capteur.direction == direction && capteur.IsThereAnObstacle())
                        {

                            Informations.printInformations(Priority.MEDIUM, "CapteurObstacleManager : obstacle removed BUT CANCLLED in direction : " + direction);
                            return;
                        }
                    }
                    Informations.printInformations(Priority.MEDIUM, "CapteurObstacleManager : obstacle removed in direction : " + direction);
                    ObstacleChangeEvent(direction, isThereAnObstacle);
                }
            }
        }*/
    }

    public abstract class CapteurObstacle
    {
        public event ObstacleListenerDelegate ObstacleChangeEvent;
        public OBSTACLE_DIRECTION direction;

        public CapteurObstacle(OBSTACLE_DIRECTION direction)
        {
            this.direction = direction;
        }

        public abstract bool IsThereAnObstacle();

        protected void OnObstacleChange(bool isThereAnobstacle)
        {
            if (ObstacleChangeEvent != null)
            {
                ObstacleChangeEvent(direction, isThereAnobstacle);
            }
        }

    }
}
