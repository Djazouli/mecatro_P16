using System;
using Microsoft.SPOT;

using System.Threading;
using GHI.Glide;
using GHI.Glide.Display;
using GHI.Glide.UI;

namespace Robot_P16.Robot.composants.IHM
{
    internal abstract class CFenetre
    {
        // Attributs ----------------------------------------
        protected AutoResetEvent m_autoReset;
        protected C_IHM m_IHM;

        protected const ushort FENETRE_WIDTH = 320;
        protected const ushort FENETRE_HEIGHT = 240;
        // --------------------------------------------------

        // Propri�t�s ---------------------------------------
        // --------------------------------------------------

        // M�thodes -----------------------------------------
        /// <summary>
        /// G�n�re la fenetre � afficher
        /// </summary>
        /// <param name="numFenetre">Indice de fen�tre � afficher. Par d�fault il vaut 0</param>
        public abstract void Afficher(byte numFenetre = 0);

        /// <summary>
        /// Efface l'interface � l'�cran et vide l'�cran
        /// </summary>
        public void Clear()
        {
            Glide.MainWindow = null;
            Glide.Screen.Clear();
            Glide.Screen.Flush();
        }

        /// <summary>
        /// G�n�re la fenetre
        /// </summary>
        protected abstract void GenererFenetre();
        // --------------------------------------------------
    }
}
