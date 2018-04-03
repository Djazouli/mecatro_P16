using System;
using Microsoft.SPOT;

namespace Robot_P16.Robot.composants.Servomoteurs
{

    public enum ServoMoteurTypes
    {
        PR_CLAPET1,
        GR_CLAPET1
    }

    public enum ServoCommandTypes
    {
        ABSOLUTE_ROTATION,
        RELATIVE_ROTATION
    }

    public class AX12
    {

        /// <summary>
        /// Sert simplement à exécuter la méthode associé au type de rotation demandé dans l'énum
        /// </summary>
        /// <param name="type"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public int ExecuteCommand(ServoCommandTypes type, float angle)
        {
            Informations.printInformations(Priority.LOW, "Robot.composants.Servomoteurs.AX12.ExecuteCommand : exécution de la commande");
            switch (type)
            {
                case ServoCommandTypes.ABSOLUTE_ROTATION:
                    return SetAngle(angle);
                case ServoCommandTypes.RELATIVE_ROTATION:
                    return RotateOf(angle);
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Rotation RELATIVE par rapport à la position actuelle du servo.
        /// (Passe en mode wheel si nécessaire)
        /// </summary>
        /// <param name="angle">En degrès</param>
        /// <returns>Durée en ms estimée de la rotation</returns>
        public int RotateOf(float angle)
        {
            // TODO
            string a = angle.ToString();
            Informations.printInformations(Priority.HIGH, "Robot.composants.Servomoteurs.AX12.RotateOf : rotation du servo de " + a + "degrés");
            return 1;
        }

        /// <summary>
        /// Rotation ABSOLUE : défini l'angle de manière directe.
        /// </summary>
        /// <param name="angle">En degrès</param>
        /// <returns>Durée en ms estimée de la rotation</returns>
        public int SetAngle(float angle)
        {
            // TODO
            string a = angle.ToString();
            Informations.printInformations(Priority.HIGH, "Robot.composants.Servomoteurs.AX12.SetAngle : l'angle absolu du robot est désormais de " + a + "degrés");
            return 1;
        }

        public int GetDurationOfRotation(float angle)
        {
            // Récupère la vitesse (et le mode de fonctionnement ?) / dépend de la vitesse et du mode (à rajouter en param dans ce cas)
            // Pour renvoyer le nombre de ms à attendre pour effectuer la rotation
            Informations.printInformations(Priority.LOW, "Robot.composants.Servomoteurs.AX12.GetDurationOfRotation : la durée de la rotation a été de _ secondes");
            return 0;
        }

        public void Stop()
        {
            Informations.printInformations(Priority.HIGH, "Robot.composants.Servomoteurs.AX12.Stop : arrêt de la rotation en cours");
            // Stop simplement la rotation actuelle, quelqu'elle soit (wheel/Joint)
        }



    }
}
