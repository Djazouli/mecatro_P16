using System;
using System.Collections;
using Microsoft.SPOT;

namespace Robot_P16.Robot.composants.Servomoteurs
{

    public static class DonneesServo
    {
        /* ***************** REGLES DE NOMMAGE A RESPECTER **********
         * public const float [TYPE DE PARAMETRE]_[TYPE DE ROBOT]_[ROLE DU SERVO]_[ROLE DE LA ROTATION] = X;
         * Exemple : angle de rotation pour ouvrir le clapet des r�servoirs sur le grand robot :
         * public const float ANGLE_GR_CLAPET_RESERVOIR_OUVRIR = X;
         * 
         * Si l'angle doit d�pendre de la couleur de l'�quipe, rajouter _[COULEUR] � la fin du nom de la constante
         * ***************** FIN DES REGLES DE NOMMAGE ****************** */

        public const float ANGLE_GR_CLAPET_RESERVOIR_OUVRIR = 0;

    }
}
