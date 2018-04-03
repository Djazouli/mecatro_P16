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
        /// Sert simplement � ex�cuter la m�thode associ� au type de rotation demand� dans l'�num
        /// </summary>
        /// <param name="type"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public int ExecuteCommand(ServoCommandTypes type, float angle)
        {
            Informations.printInformations(Priority.LOW, "Robot.composants.Servomoteurs.AX12.ExecuteCommand : ex�cution de la commande");
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
        /// Rotation RELATIVE par rapport � la position actuelle du servo.
        /// (Passe en mode wheel si n�cessaire)
        /// </summary>
        /// <param name="angle">En degr�s</param>
        /// <returns>Dur�e en ms estim�e de la rotation</returns>
        public int RotateOf(float angle)
        {
            // TODO
            string a = angle.ToString();
            Informations.printInformations(Priority.HIGH, "Robot.composants.Servomoteurs.AX12.RotateOf : rotation du servo de " + a + "degr�s");
            return 1;
        }

        /// <summary>
        /// Rotation ABSOLUE : d�fini l'angle de mani�re directe.
        /// </summary>
        /// <param name="angle">En degr�s</param>
        /// <returns>Dur�e en ms estim�e de la rotation</returns>
        public int SetAngle(float angle)
        {
            // TODO
            string a = angle.ToString();
            Informations.printInformations(Priority.HIGH, "Robot.composants.Servomoteurs.AX12.SetAngle : l'angle absolu du robot est d�sormais de " + a + "degr�s");
            return 1;
        }

        public int GetDurationOfRotation(float angle)
        {
            // R�cup�re la vitesse (et le mode de fonctionnement ?) / d�pend de la vitesse et du mode (� rajouter en param dans ce cas)
            // Pour renvoyer le nombre de ms � attendre pour effectuer la rotation
            Informations.printInformations(Priority.LOW, "Robot.composants.Servomoteurs.AX12.GetDurationOfRotation : la dur�e de la rotation a �t� de _ secondes");
            return 0;
        }

        public void Stop()
        {
            Informations.printInformations(Priority.HIGH, "Robot.composants.Servomoteurs.AX12.Stop : arr�t de la rotation en cours");
            // Stop simplement la rotation actuelle, quelqu'elle soit (wheel/Joint)
        }



    }
}
