using System;
using Microsoft.SPOT;
using System.Threading;
using Gadgeteer;

namespace Robot_P16.Robot.composants.CapteursObstacle
{

    public delegate void ObstacleListenerDelegate(OBSTACLE_DIRECTION direction, bool isThereAnObstacle);
    public delegate void CapteurObstacleListenerDelegate(int indexCapteur, bool isThereAnObstacle);

    public class CapteurObstacleManager
    {
        private CapteurObstacle[] capteurs;
        private bool[] lastObstacleFoundForCapteurs;
        public event ObstacleListenerDelegate ObstacleChangeEvent;
        private static int REFRESH_RATE = 50;
        public Gadgeteer.Timer TimerRefresh = new Gadgeteer.Timer(REFRESH_RATE);


        public CapteurObstacleManager(CapteurObstacle[] capteurs)
        {
            this.capteurs = capteurs;
            this.lastObstacleFoundForCapteurs = new bool[this.capteurs.Length];
            for (int i = 0; i < this.capteurs.Length; i++)
            {
                this.lastObstacleFoundForCapteurs[i] = capteurs[i].IsThereAnObstacle();
            }
            
            //this.start();

            
            foreach (CapteurObstacle capteur in capteurs)
            {
                capteur.CapteurObstacleEvent += this.OnObstacle;
            }
        }

        /*private void start()
        {
            //new Thread(() => { Thread.Sleep(REFRESH_RATE); checkObstacles(); }).Start();
            TimerRefresh = new Gadgeteer.Timer(REFRESH_RATE);
            TimerRefresh.Tick += (Gadgeteer.Timer t) => this.checkObstacles();
            TimerRefresh.Start();
        }*/
        private int indexForCapteur(CapteurObstacle c)
        {
            for (int i = 0; i < capteurs.Length; i++)
            {
                if (c.Equals(capteurs[i]))
                    return i;
            }
            return -1;
        }

        public bool getObstacleStatusForDirection(OBSTACLE_DIRECTION direction)
        {
            return getObstacleStatusForDirection(direction, this.lastObstacleFoundForCapteurs);
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

        private void OnObstacle(int indexCapteur, bool isThereAnObstacle)
        {
            bool fireEvent = false;
            lock (lastObstacleFoundForCapteurs)
            {
                //int indexCapteur = indexForCapteur(capteur);
                bool lastStatusForDirection = getObstacleStatusForDirection(capteurs[indexCapteur].direction, this.lastObstacleFoundForCapteurs);
                lastObstacleFoundForCapteurs[indexCapteur] = isThereAnObstacle;
                bool currentStatusForDirection = getObstacleStatusForDirection(capteurs[indexCapteur].direction, this.lastObstacleFoundForCapteurs);
                if (lastStatusForDirection != currentStatusForDirection)
                {
                    fireEvent = true;
                }
                 
            }
            if (fireEvent)
            {
                Informations.printInformations(Priority.MEDIUM, "CapteurObstacleManager : obstacle=" + isThereAnObstacle + " in direction : " + capteurs[indexCapteur].direction);
                if (ObstacleChangeEvent != null)
                {
                    ObstacleChangeEvent(capteurs[indexCapteur].direction, isThereAnObstacle);
                }
            }
            
            
        }
        
    }

    public abstract class CapteurObstacle
    {
        public event CapteurObstacleListenerDelegate CapteurObstacleEvent;
        public OBSTACLE_DIRECTION direction;
        public int index;

        public CapteurObstacle(OBSTACLE_DIRECTION direction)
        {
            this.direction = direction;
        }

        public void setIndex(int index)
        {
            this.index = index;
        }

        public abstract bool IsThereAnObstacle();

        protected void OnObstacleChange(bool isThereAnobstacle)
        {
            Informations.printInformations(Priority.LOW, "CapteurObstacle - OnObstacleChange called , OBSTACLE=" + isThereAnobstacle + " in direction=" + this.direction);
            if (CapteurObstacleEvent != null)
            {
                CapteurObstacleEvent(index, isThereAnobstacle);
            }
        }

    }
}
