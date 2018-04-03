using System;
using Microsoft.SPOT;

namespace Robot_P16.Robot.composants.IHM
{

    public delegate void VoidFunc();

    public abstract class Parametrization
    {
        public VoidFunc startMethod;

        public abstract TypeRobot GetTypeRobot();
        public abstract ModeOperatoire GetModeOperatoire();
        public abstract CouleurEquipe GetCouleurEquipe();
    }

    public class CustomParametrization : Parametrization
    {
        TypeRobot typeRobot;
        ModeOperatoire modeOperatoire;
        CouleurEquipe couleurEquipe;

        public CustomParametrization(TypeRobot typeRobot, ModeOperatoire modeOperatoire, CouleurEquipe couleurEquipe)
        {
            this.typeRobot = typeRobot;
            this.modeOperatoire = modeOperatoire;
            this.couleurEquipe = couleurEquipe;
            Informations.printInformations(Priority.LOW, "Robot.composants.IHM.Parametrization.CustomParametrization : le type de robot, le mode op�ratoire et la couleur de l'�quipe ont �t� param�tr�s");
        }

        public override TypeRobot GetTypeRobot()
        {
            Informations.printInformations(Priority.LOW, "Robot.composants.IHM.Parametrization.TypeRobot : information sur le type de robot r�cup�r�e" );
            return typeRobot;
        }

        public override ModeOperatoire GetModeOperatoire()
        {
            Informations.printInformations(Priority.LOW, "Robot.composants.IHM.Parametrization.ModeOperatoire : information sur le mode op�ratoire r�cup�r�e");
            return modeOperatoire;
        }

        public override CouleurEquipe GetCouleurEquipe()
        {
            Informations.printInformations(Priority.LOW, "Robot.composants.IHM.Parametrization.CouleurEquipe : information sur la couleur de l'�quipe r�cup�r�e");
            return couleurEquipe;
        }
    }
}
