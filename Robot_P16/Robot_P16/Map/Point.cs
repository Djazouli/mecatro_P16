using System;
using Microsoft.SPOT;

namespace Robot_P16.Map
{
    public class PointOriente
    {
        public readonly double x;
        public readonly double y;
        public readonly double theta;

        public PointOriente(double x, double y, double theta)
        {
            this.x = x;
            this.y = y;
            this.theta = theta;//-180 < theta < 180
        }
        public PointOriente(double x, double y) : this(x,y,0) {}
        

        public PointOriente translater(double x, double y, double theta)
        {
            string abs = x.ToString();
            string ord = y.ToString();
            string angle = theta.ToString();
            Robot.Informations.printInformations(Robot.Priority.LOW, "Map.Point.PointOriente.translater : Le point orienté est translaté de " + abs + " en abscisse, " + ord + " en ordonnée et " + theta + " degrés");
            return new PointOriente(this.x + x, this.y + y, this.theta + theta);
            
        }

        public PointOriente translater(PointOriente pt)
        {
            return translater(pt.x, pt.y, pt.theta);
        }

        public double distanceSquared(PointOriente pt)
        {
            string abs = pt.x.ToString();
            string ord = pt.y.ToString();
            string angle = pt.theta.ToString();
            Robot.Informations.printInformations(Robot.Priority.LOW, "Map.Point.PointOriente.distanceSquared : Calcul de la dustance carée au point orienté (" + abs + "," + ord + "," + theta + ")");
            return (pt.x - this.x) * (pt.x - this.x) + (pt.y - this.y) * (pt.y - this.y);
        }

        public PointOriente Clone()
        {
            string abs = this.x.ToString();
            string ord = this.y.ToString();
            string angle = this.theta.ToString();
            Robot.Informations.printInformations(Robot.Priority.LOW, "Map.Point.PointOriente.Clone : Copie du point orienté (" + abs + "," + ord + "," + theta + ")");
            return new PointOriente(x, y, theta);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            PointOriente pt = (PointOriente)obj;
            Robot.Informations.printInformations(Robot.Priority.LOW, "Map.Point.PointOriente.Equals : Les informations sur le point orienté ont été récupérées");
            return pt.x == this.x && pt.y == this.y && pt.theta == this.theta;
        }
        

        public override int GetHashCode()
        {
            string abs = this.x.ToString();
            string ord = this.y.ToString();
            string angle = this.theta.ToString();
            Robot.Informations.printInformations(Robot.Priority.LOW, "Map.Point.PointOriente.GetHashCode : Récupération du hascode du point orienté (" + abs + "," + ord + "," + theta + ")");
            return (int)x * (int)y;
        }
    }
}
