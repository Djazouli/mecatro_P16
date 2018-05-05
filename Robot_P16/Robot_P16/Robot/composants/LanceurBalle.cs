using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace Robot_P16.Robot.composants
{


    public class LanceurBalle : Composant
    {

        private MotorDriverL298 moteur;
       
        public LanceurBalle(int socket)
            : base(socket)
        {
            this.moteur = new MotorDriverL298(socket);
        }

        public void launchSpeed(double speed)
        {
            if (speed >= -1.0f && speed <= 1.0f)
            {
                moteur.SetSpeed(MotorDriverL298.Motor.Motor1, speed);
                Informations.printInformations(Priority.MEDIUM, "LanceurBalle : launched motor 1 with speed "+speed);
            }
            else
            {
                Informations.printInformations(Priority.HIGH, "LanceurBalle : VITESSE INVALIDE !!! Doit etre de module <= 1. here speed = "+speed);
            }
        }

        public void stop()
        {
            moteur.SetSpeed(MotorDriverL298.Motor.Motor1, 0.001);
            moteur.StopAll();
            Informations.printInformations(Priority.MEDIUM, "LanceurBalle : stopped all motors");
        }
    }
}
