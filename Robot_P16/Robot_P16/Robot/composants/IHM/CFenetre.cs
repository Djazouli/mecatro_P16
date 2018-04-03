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

        // Propriétés ---------------------------------------
        // --------------------------------------------------

        // Méthodes -----------------------------------------
        /// <summary>
        /// Génère la fenetre à afficher
        /// </summary>
        /// <param name="numFenetre">Indice de fenêtre à afficher. Par défault il vaut 0</param>
        public abstract void Afficher(byte numFenetre = 0);

        /// <summary>
        /// Efface l'interface à l'écran et vide l'écran
        /// </summary>
        public void Clear()
        {
            Glide.MainWindow = null;
            Glide.Screen.Clear();
            Glide.Screen.Flush();
        }

        /// <summary>
        /// Génère la fenetre
        /// </summary>
        protected abstract void GenererFenetre();
        // --------------------------------------------------
    }
}
