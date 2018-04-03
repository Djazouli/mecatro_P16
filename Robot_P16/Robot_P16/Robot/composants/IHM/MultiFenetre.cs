using System;
using Microsoft.SPOT;

using System.Threading;
using GHI.Glide;
using GHI.Glide.Display;
using GHI.Glide.UI;


namespace Robot_P16.Robot.composants.IHM
{
    /// <summary>
    /// Type de multi fenetre
    /// </summary>
    internal enum multiFenetre_Type
    {
        FEN_MODE_TEST,
        UNDEFINED
    }

    class MultiFenetre : CFenetre
    {
        // Attributs -----------------------------------------
        private Window[] m_window;

        private Button m_boutonRetour,
            m_boutonSuivant,
            m_boutonValider;
        private Image m_boutonHome;
        private byte m_currentIndice;
        private string[] m_listOfAction;
        private string m_currentFonctionName = "";
        // ---------------------------------------------------

        // Propriétés ----------------------------------------
        public multiFenetre_Type Type { get; private set; }
        // ---------------------------------------------------

        // Constructeur --------------------------------------
        public MultiFenetre(multiFenetre_Type type, AutoResetEvent autoReset, C_IHM ihm)
        {
            Type = type;
            m_autoReset = autoReset;

            m_IHM = ihm;
        }
        // ---------------------------------------------------

        // Méthodes ------------------------------------------
        public override void Afficher(byte numFenetre = 0)
        {
            if (m_window.Length > numFenetre && m_window[numFenetre] != null)
            {
                m_currentIndice = numFenetre;

                InitBoutons();

                GlideTouch.Initialize();
                Glide.MainWindow = m_window[numFenetre];
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
                case multiFenetre_Type.FEN_MODE_TEST:
                    Genetre_Fen_Mode_Test();
                    break;
                default:
                    break;
            }
        }

        #region METHODE POUR LES TYPES DE MULTI FENETRE
        private void Genetre_Fen_Mode_Test()
        {
            string[] fenetreXML = GenererFenetreXMLForTest();

            m_window = new Window[fenetreXML.Length];

            for (int i = 0; i < fenetreXML.Length; i++)
            {
                m_window[i] = GlideLoader.LoadWindow(fenetreXML[i]);
            }

            InitRadio();
        }
        #endregion

        #region METHODE POUR GENERER LA FENETRE DE TEST
        private string[] GenererFenetreXMLForTest()
        {
            byte nbFenetre = CalculerNombreFenetre();

            string[] fenetreXML = new string[nbFenetre];

            for (int i = 0, j = 0; i < nbFenetre; i++)
            {
                fenetreXML[i] = "<Glide Version=\"1.0.7\">\n";
                fenetreXML[i] += "<Window Name=\"fenetre" + i.ToString() + "\" ";
                fenetreXML[i] += "Width=\"" + FENETRE_WIDTH.ToString() + "\" ";
                fenetreXML[i] += "Height=\"" + FENETRE_HEIGHT.ToString() + "\" ";
                fenetreXML[i] += "BackColor=\"FFFFFF\">\n";

                for (int k = 0; k < 4; k++)
                {
                    if (j >= m_listOfAction.Length)
                        break;

                    fenetreXML[i] += "<RadioButton Name=\"radio" + j.ToString() + "\" ";
                    fenetreXML[i] += "X=\"20\" ";
                    fenetreXML[i] += "Y=\"" + (20 + (40 * k)).ToString() + "\" ";
                    fenetreXML[i] += "Width=\"32\" ";
                    fenetreXML[i] += "Height=\"32\" ";
                    fenetreXML[i] += "Alpha=\"255\" ";
                    fenetreXML[i] += "Value=\"\" Checked=\"False\" ";
                    fenetreXML[i] += "GroupName=\"radioButtonGroup\" ShowBackground=\"True\" Color=\"d4d4d4\" ";
                    fenetreXML[i] += "OutlineColor=\"b8b8b8\" SelectedColor=\"358bf6\" SelectedOutlineColor=\"002dff\"/>\n";

                    fenetreXML[i] += "<TextBlock Name=\"instance" + j + "\" ";
                    fenetreXML[i] += "X=\"80\" ";
                    fenetreXML[i] += "Y=\"" + (25 + (40 * k)).ToString() + "\" ";
                    fenetreXML[i] += "Width=\"200\" Height=\"32\" Alpha=\"255\" ";
                    fenetreXML[i] += "Text=\"" + m_listOfAction[j] + "\" ";
                    fenetreXML[i] += "TextAlign=\"Left\" TextVerticalAlign=\"Top\" Font=\"4\" FontColor=\"0\" BackColor=\"000000\" ShowBackColor=\"False\"/>\n";

                    j++;
                }

                fenetreXML[i] += "</Window>\n</Glide>";
            }

            return fenetreXML;
        }

        private byte CalculerNombreFenetre()
        {
            int nbFenetre = 0;
            int nbActions = m_listOfAction.Length;

            do
            {
                nbActions -= 4;
                nbFenetre++;
            } while (nbActions > 0);

            return (byte)nbFenetre;
        }

        private void InitRadio()
        {
            RadioButton[] radioList = new RadioButton[m_listOfAction.Length];

            for (int i = 0, j = 0; i < m_window.Length; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    if (j >= m_listOfAction.Length)
                        break;

                    string name = "radio" + j.ToString();
                    radioList[j] = (RadioButton)m_window[i].GetChildByName(name);
                    radioList[j].TapEvent += OnRadioTap;

                    j++;
                }
            }
        }

        private void OnRadioTap(object radio)
        {
            RadioButton maRadio = (RadioButton)radio;

            int indiceRadio = int.Parse(maRadio.Name.Substring(5));

            m_currentFonctionName = m_listOfAction[indiceRadio];

            m_window[m_currentIndice].Invalidate();
            ActiverBoutonValider();

            m_window[m_currentIndice].Invalidate();
            ActiverBoutonValider();
        }
        #endregion

        #region METHODE DE GENERATION DES BOUTONS SUIVANT, RETOUR, VALIDER, HOME
        private void InitBoutons()
        {
            m_boutonRetour = new Button("b_retour", 255, 0, 208, 80, 32);
            m_boutonRetour.Text = "Retour";
            m_boutonRetour.TapEvent += new OnTap(sender =>
            {
                m_currentFonctionName = "";
                Afficher((byte)(m_currentIndice - 1));
            });

            m_boutonSuivant = new Button("b_suivant", 255, 155, 208, 80, 32);
            m_boutonSuivant.Text = "Suivant";
            m_boutonSuivant.TapEvent += new OnTap(sender =>
            {
                m_currentFonctionName = "";
                Afficher((byte)(m_currentIndice + 1));
            });

            m_boutonValider = new Button("b_valider", 255, 240, 208, 80, 32);
            m_boutonValider.Text = "Valider";
            m_boutonValider.TapEvent += new OnTap(sender =>
            {
                if (m_currentFonctionName != "")
                {
                    m_IHM.SelectActionName = m_currentFonctionName;
                    m_autoReset.Set();
                }
            });

            m_boutonHome = new Image("b_home", 255, 100, 208, 34, 32);
            m_boutonHome.Bitmap = new Bitmap(Resources.GetBytes(Resources.BinaryResources.exit), Bitmap.BitmapImageType.Gif);
            m_boutonHome.TapEvent += new OnTap(sender =>
            {
                m_IHM.SelectActionName = "no-selection";
                m_autoReset.Set();
            });

            DesactiverBoutonValider();

            if (m_window.Length < 2)
            {
                DesactiverBoutonRetour();
                DesactiverBoutonSuivant();
            }

            else
            {
                if (m_currentIndice == 0)
                {
                    DesactiverBoutonRetour();
                }

                else if (m_currentIndice == m_window.Length - 1)
                {
                    DesactiverBoutonSuivant();
                }
            }

            m_window[m_currentIndice].AddChild(m_boutonRetour);
            m_window[m_currentIndice].AddChild(m_boutonSuivant);
            m_window[m_currentIndice].AddChild(m_boutonValider);
            m_window[m_currentIndice].AddChild(m_boutonHome);
        }

        private void DesactiverBoutonRetour()
        {
            m_boutonRetour.Alpha = 128;
            m_boutonRetour.Interactive = false;
            m_boutonRetour.FontColor = Colors.Gray;
        }

        private void DesactiverBoutonSuivant()
        {
            m_boutonSuivant.Alpha = 128;
            m_boutonSuivant.Interactive = false;
            m_boutonSuivant.FontColor = Colors.Gray;
        }

        private void DesactiverBoutonValider()
        {
            m_boutonValider.Alpha = 128;
            m_boutonValider.Interactive = false;
            m_boutonValider.FontColor = Colors.Gray;
        }

        private void ActiverBoutonValider()
        {
            m_boutonValider.Alpha = 255;
            m_boutonValider.Interactive = true;
            m_boutonValider.FontColor = Colors.Black;
        }
        #endregion

        public void SetListOfAction_for_FenTest(string[] action)
        {
            if (Type == multiFenetre_Type.FEN_MODE_TEST)
            {
                m_listOfAction = action;

                GenererFenetre();
            }
        }
        // ---------------------------------------------------
    }
}
