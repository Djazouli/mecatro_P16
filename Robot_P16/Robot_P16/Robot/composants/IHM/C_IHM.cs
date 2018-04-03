using System;
using Microsoft.SPOT;
using Robot_P16;
using System.Threading; // Nécéssaire pour les threads

namespace Robot_P16.Robot.composants.IHM
{

    public class C_IHM : Parametrization
    {
        // Attribut -------------------------------------------------------------------
        private AutoResetEvent threadPause = new AutoResetEvent(false);

        private MonoFenetre m_Current_monoFenetre;
        private MultiFenetre m_Current_multiFenetre;
        // ----------------------------------------------------------------------------

        // propriété utilisé pour pouvoir récupérer les choix dans les fenetres -------
        internal ModeOperatoire Robot_Mode { private get; set; }
        internal CouleurEquipe Robot_Couleur { private get; set; }
        internal TypeRobot Robot_Type { private get; set; }
        internal string SelectActionName { private get; set; }
        // ----------------------------------------------------------------------------

        // Méthodes -------------------------------------------------------------------
        /// <summary>
        /// Génère une fenêtre à l'écran permettant à l'utilisateur de choisir un mode opératoire
        /// </summary>
        /// <returns></returns>
        public override ModeOperatoire GetModeOperatoire()
        {
            m_Current_monoFenetre = new MonoFenetre(monoFenetre_Type.FEN_CHOIX_MODE, threadPause, this);
            m_Current_monoFenetre.Afficher();

            threadPause.WaitOne();

            m_Current_monoFenetre.Clear();

            return Robot_Mode;
        }

        /// <summary>
        /// Génère une fenêtre à l'écran permettant à l'utilisateur de choisir une couleur d'équipe
        /// </summary>
        /// <returns></returns>
        public override CouleurEquipe GetCouleurEquipe()
        {
            m_Current_monoFenetre = new MonoFenetre(monoFenetre_Type.FEN_CHOIX_COULEUR, threadPause, this);
            m_Current_monoFenetre.Afficher();

            threadPause.WaitOne();

            m_Current_monoFenetre.Clear();

            return Robot_Couleur;
        }

        /// <summary>
        /// Génère une fenêtre à l'écran permettant à l'utilisateur le type du Robot
        /// </summary>
        /// <returns></returns>
        public override TypeRobot GetTypeRobot()
        {
            m_Current_monoFenetre = new MonoFenetre(monoFenetre_Type.FEN_CHOIX_TYPE, threadPause, this);
            m_Current_monoFenetre.Afficher();

            threadPause.WaitOne();

            m_Current_monoFenetre.Clear();

            return Robot_Type;
        }

        /// <summary>
        /// Affiche à l'écran une fenêtre d'erreur
        /// </summary>
        /// <param name="title">Titre du message</param>
        /// <param name="message">Message</param>
        public void AfficherErreur(string title, string message)
        {
            m_Current_monoFenetre = new MonoFenetre(monoFenetre_Type.FEN_ERREUR, threadPause, this);
            m_Current_monoFenetre.SetErrorMessage(title, message);
            m_Current_monoFenetre.Afficher();

            threadPause.WaitOne();

            m_Current_monoFenetre.Clear();
        }

        /// <summary>
        /// Affiche un message d'information à l'ecran
        /// </summary>
        /// <param name="message">Message</param>
        public void AfficherInformation(string message, bool blocThread)
        {
            if (blocThread)
                m_Current_monoFenetre = new MonoFenetre(monoFenetre_Type.FEN_INFORMATION, threadPause, this);
            else
                m_Current_monoFenetre = new MonoFenetre(monoFenetre_Type.FEN_INFORMATION, null, this);

            m_Current_monoFenetre.SetMessageInformation(message);
            m_Current_monoFenetre.Afficher();

            if (blocThread)
            {
                threadPause.WaitOne();
                m_Current_monoFenetre.Clear();
            }
        }

        /// <summary>
        /// Affiche l'écran de compétition
        /// </summary>
        /// <param name="fonctionAjoutScore"></param>
        public void SetModeCompetition()
        {
            m_Current_monoFenetre = new MonoFenetre(monoFenetre_Type.FEN_COMPETITION, threadPause, this);
            m_Current_monoFenetre.defineRobotParam(Robot_Type, Robot_Couleur);
            m_Current_monoFenetre.Afficher();
        }

        /// <summary>
        /// Affiche l'écran pour les test du Robot
        /// </summary>
        /// <param name="fonctionsTest"></param>
        public string getFonctionTest(string[] fonctionsTest)
        {
            m_Current_multiFenetre = new MultiFenetre(multiFenetre_Type.FEN_MODE_TEST, threadPause, this);
            m_Current_multiFenetre.SetListOfAction_for_FenTest(fonctionsTest);
            m_Current_multiFenetre.Afficher();

            threadPause.WaitOne();

            m_Current_multiFenetre.Clear();

            return SelectActionName;
        }

        /// <summary>
        /// Affiche l'écran d'homologation
        /// </summary>
        public void SetModeHomologation()
        {
            // TODO
            //m_Current_monoFenetre = new MonoFenetre(monoFenetre_Type.FEN_HOMOLOGATION, threadPause);
            //m_Current_monoFenetre.Afficher();

            // Pour l'instant cette fenetre n'afiche rien - - -
            m_Current_monoFenetre = new MonoFenetre(monoFenetre_Type.FEN_INFORMATION, threadPause, this);
            m_Current_monoFenetre.SetMessageInformation("Cette fenetre est en construction. Veuillez patienter !");
            m_Current_monoFenetre.Afficher();

            threadPause.WaitOne();

            m_Current_monoFenetre.Clear();
        }

        /// <summary>
        /// Permet de mettre à jour le score afficher à l'écran
        /// A utiliser aprés avoir appeler la fonction C_IHM.SetModeCompetition()
        /// </summary>
        /// <param name="score">Nombre de score</param>
        public void UpdateScoreInView(int score)
        {
            if ((m_Current_monoFenetre != null) && (m_Current_monoFenetre.Type == monoFenetre_Type.FEN_COMPETITION))
            {
                m_Current_monoFenetre.SetScore(score);
            }
        }
        // ----------------------------------------------------------------------------
    }
}
