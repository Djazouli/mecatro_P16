using System;
using Microsoft.SPOT;

using System.Threading;
using GHI.Glide;
using GHI.Glide.Display;
using GHI.Glide.UI;

namespace Robot_P16.Robot.composants.IHM
{
    /// <summary>
    /// Type de mono fenetre
    /// </summary>
    internal enum monoFenetre_Type
    {
        FEN_CHOIX_COULEUR,
        FEN_CHOIX_MODE,
        FEN_CHOIX_TYPE,
        FEN_ERREUR,
        FEN_COMPETITION,
        FEN_HOMOLOGATION,
        FEN_INFORMATION
    }

    internal class MonoFenetre : CFenetre
    {
        // Attributs -----------------------------------------
        private Window m_window;
        // ---------------------------------------------------

        // Propriétés ----------------------------------------
        public monoFenetre_Type Type { get; private set; }
        // ---------------------------------------------------

        // Constructeur --------------------------------------
        public MonoFenetre(monoFenetre_Type type, AutoResetEvent autoReset, C_IHM ihm)
        {
            Type = type;
            m_autoReset = autoReset;
            m_IHM = ihm;

            GenererFenetre();
        }
        // ---------------------------------------------------

        // Méthodes ------------------------------------------
        /// <summary>
        /// Affiche la fenetre précédemment generer
        /// </summary>
        public override void Afficher(byte numFenetre = 0)
        {
            if (m_window != null)
            {
                GlideTouch.Initialize();
                Glide.MainWindow = m_window;
            }
            else
            {
                Debug.Print("Erreur: la fenetre n'existe pas");
            }
        }

        protected override void GenererFenetre()
        {
            switch (Type)
            {
                case monoFenetre_Type.FEN_CHOIX_COULEUR:
                    Generer_Fen_Choix_Couleur();
                    break;
                case monoFenetre_Type.FEN_CHOIX_MODE:
                    Generer_Fen_Choix_Mode();
                    break;
                case monoFenetre_Type.FEN_CHOIX_TYPE:
                    Generer_Fen_Choix_Type();
                    break;
                case monoFenetre_Type.FEN_COMPETITION:
                    Generer_Fen_Comptition();
                    break;
                case monoFenetre_Type.FEN_ERREUR:
                    Generer_Fen_Erreur();
                    break;
                case monoFenetre_Type.FEN_HOMOLOGATION:
                    Generer_Fen_Homologation();
                    break;
                case monoFenetre_Type.FEN_INFORMATION:
                    Generer_Fen_Information();
                    break;
            }
        }

        #region Méthode pour Generer le type de fenetre
        private void Generer_Fen_Choix_Couleur()
        {
            m_window = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.fenetreChoixCouleur));

            TextBlock couleurVert = (TextBlock)m_window.GetChildByName("couleurVert");
            couleurVert.TapEvent += new OnTap(sender =>
            {
                m_IHM.Robot_Couleur = CouleurEquipe.VERT;
                m_autoReset.Set();
            });

            TextBlock couleurOrange = (TextBlock)m_window.GetChildByName("couleurOrange");
            couleurOrange.TapEvent += new OnTap(sender =>
            {
                m_IHM.Robot_Couleur = CouleurEquipe.ORANGE;
                m_autoReset.Set();
            });
        }

        private void Generer_Fen_Choix_Mode()
        {
            m_window = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.fenetreChoixMode));

            Image logoTest = (Image)m_window.GetChildByName("logoTest");
            logoTest.Bitmap = new Bitmap(Resources.GetBytes(Resources.BinaryResources.logoTest), Bitmap.BitmapImageType.Gif);
            Image logoHomologation = (Image)m_window.GetChildByName("logoHomologation");
            logoHomologation.Bitmap = new Bitmap(Resources.GetBytes(Resources.BinaryResources.logoHomologation), Bitmap.BitmapImageType.Gif);
            Image logoCompetition = (Image)m_window.GetChildByName("logoCompetition");
            logoCompetition.Bitmap = new Bitmap(Resources.GetBytes(Resources.BinaryResources.logoCompetition), Bitmap.BitmapImageType.Gif);

            Button boutonTest = (Button)m_window.GetChildByName("boutonTest");
            boutonTest.TapEvent += new OnTap(sender =>
            {
                m_IHM.Robot_Mode = ModeOperatoire.TEST1;
                m_autoReset.Set();
            });

            Button boutonCompetition = (Button)m_window.GetChildByName("boutonCompetition");
            boutonCompetition.TapEvent += new OnTap(sender =>
            {
                m_IHM.Robot_Mode = ModeOperatoire.COMPETITION;
                m_autoReset.Set();
            });

            Button boutonHomologation = (Button)m_window.GetChildByName("boutonHomologation");
            boutonHomologation.TapEvent += new OnTap(sender =>
            {
                m_IHM.Robot_Mode = ModeOperatoire.HOMOLOGATION;
                m_autoReset.Set();
            });
        }

        private void Generer_Fen_Choix_Type()
        {
            m_window = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.fenetreChoixType));

            Button robotPrincipal = (Button)m_window.GetChildByName("principal");
            robotPrincipal.TapEvent += new OnTap(sender =>
            {
                m_IHM.Robot_Type = TypeRobot.GRAND_ROBOT;
                m_autoReset.Set();
            });

            Button robotSecondaire = (Button)m_window.GetChildByName("secondaire");
            robotSecondaire.TapEvent += new OnTap(sender =>
            {
                m_IHM.Robot_Type = TypeRobot.PETIT_ROBOT;
                m_autoReset.Set();
            });
        }

        private void Generer_Fen_Comptition()
        {
            m_window = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.fenetreModeCompetition));
        }

        private void Generer_Fen_Homologation()
        {
            // TODO
        }

        private void Generer_Fen_Erreur()
        {
            m_window = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.fenetreErreur));

            Button boutonOK = (Button)m_window.GetChildByName("boutonOK");
            boutonOK.TapEvent += new OnTap(sender =>
            {
                m_autoReset.Set();
            });
        }

        private void Generer_Fen_Information()
        {
            m_window = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.fenetreInformation));

            if (m_autoReset != null)
            {
                Button boutonOK = (Button)m_window.GetChildByName("boutonOK");
                boutonOK.TapEvent += new OnTap(sender =>
                {
                    m_autoReset.Set();
                });
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">Titre du message d'erreur</param>
        /// <param name="messageError">Message d'erreur</param>
        internal void SetErrorMessage(string title, string messageError)
        {
            if (Type == monoFenetre_Type.FEN_ERREUR)
            {
                TextBlock titre = (TextBlock)m_window.GetChildByName("titreMessage");
                titre.Text = "Erreur : " + title;

                TextBlock message = (TextBlock)m_window.GetChildByName("messageErreur");
                message.Text = messageError;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">Message d'information</param>
        internal void SetMessageInformation(string message)
        {
            if (Type == monoFenetre_Type.FEN_INFORMATION)
            {
                TextBlock messageView = (TextBlock)m_window.GetChildByName("messageInformation");
                messageView.Text = message;
            }
        }

        /// <summary>
        /// Configure la fenetre de competition avec les parametre du robot
        /// </summary>
        /// <param name="type">Type de Robot</param>
        /// <param name="couleur">Couleur de l'equipe</param>
        internal void defineRobotParam(TypeRobot type, CouleurEquipe couleur)
        {
            TextBlock indicatifRobot;

            indicatifRobot = (TextBlock)m_window.GetChildByName("indicatifRobot");

            switch (type)
            {
                case TypeRobot.GRAND_ROBOT:
                    indicatifRobot.Text = "GRAND_ROBOT";
                    break;
                case TypeRobot.PETIT_ROBOT:
                    indicatifRobot.Text = "PETIT_ROBOT";
                    break;
                default:
                    indicatifRobot.Text = "Robot type inconnu";
                    break;
            }

            switch (couleur)
            {
                case CouleurEquipe.VERT:
                    indicatifRobot.BackColor = Colors.Green;
                    break;
                case CouleurEquipe.ORANGE:
                    indicatifRobot.BackColor = Colors.Orange;
                    break;
                default:
                    indicatifRobot.BackColor = Colors.Blue;
                    break;
            }
        }

        /// <summary>
        /// Met à jour le score affiché
        /// </summary>
        /// <param name="score">nouveau score</param>
        internal void SetScore(int score)
        {
            if (Type == monoFenetre_Type.FEN_COMPETITION)
            {
                TextBlock scoreView = (TextBlock)m_window.GetChildByName("scoreText");

                m_window.Invalidate();
                scoreView.Text = score.ToString();
            }
        }
        // ---------------------------------------------------
    }
}
