using System;
using Microsoft.SPOT;
using Robot_P16.Map;
using Gadgeteer;

namespace Robot_P16.Robot.composants.BaseRoulante
{
    /***
     * Déplacement représente uniquement les déplacements en ligne droite
     * **/
    public class Deplacement
    {
        private static Deplacement instance = new Deplacement();
        
        //private MOVETYPES typeDeDeplacement = MOVETYPES.GoTo;
        private LieuCle destination = null;
        private Timer timer = new Timer(REFRESH_RATE);

        private static int REFRESH_RATE = 50;

        public static Deplacement GetInstance()
        {
            return instance;
        }

        private Deplacement() 
        {
            this.timer.Tick += this.Tick;
        }

        private void Start()
        {
            if(!this.timer.IsRunning)
                this.timer.Start();
        }
        private void Pause()
        {

        }
        private void End()
        {
            this.timer.Stop();
            this.destination = null;
        }

        private void Tick(Timer timer)
        {
            
        } 


    }
}
