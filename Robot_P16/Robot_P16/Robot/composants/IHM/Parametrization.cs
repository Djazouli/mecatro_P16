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
        }

        public override TypeRobot GetTypeRobot()
        {
            return typeRobot;
        }

        public override ModeOperatoire GetModeOperatoire()
        {
            return modeOperatoire;
        }

        public override CouleurEquipe GetCouleurEquipe()
        {
            return couleurEquipe;
        }
    }
}
