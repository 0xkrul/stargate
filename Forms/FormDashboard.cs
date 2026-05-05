using System.Data;
using appliPandora.Forms;

namespace appliPandora.Forms
{
    /// <summary>
    /// Volet 1 — Tableau de bord des missions (passées, en cours, à venir).
    /// Charge les données en mode connecté au démarrage, puis bascule en mode déconnecté.
    /// </summary>
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
            this.Load += FormDashboard_Load;
        }

        // ─── Chargement initial ────────────────────────────────────────────────
        private void FormDashboard_Load(object sender, EventArgs e)
        {
            ChargerDonnees();
            AfficherMissions();
        }

        /// <summary>
        /// Remplit le DataSet global avec toutes les tables utiles (mode connecté),
        /// puis ferme la connexion (mode déconnecté).
        /// </summary>
        private void ChargerDonnees()
        {
            // TODO: remplir MesDatas.DsGlobal via SQLiteDataAdapter pour chaque table :
            //       Mission, Membre, Militaire, Civil, Composer, Espece, Ennemi, Allie,
            //       Planete, Habiter, ObjectifCapture, Capturer, JournalDeBord,
            //       Depense, TypeDepense, Contact, Informateur, Negocier
            // Puis appeler Connexion.FermerConnexion()
        }

        /// <summary>
        /// Affiche les missions dans le DataGridView du tableau de bord.
        /// </summary>
        private void AfficherMissions()
        {
            // TODO: lier dgvMissions à la table "Mission" du DataSet
        }

        // ─── Bouton : Nouvelle mission (admin requis) ──────────────────────────
        private void btnNouvelleMission_Click(object sender, EventArgs e)
        {
            // Ouvrir FormAuth en dialog. Si authentifié, ouvrir FormNouvelleMission.
            using FormAuth auth = new FormAuth();
            if (auth.ShowDialog() == DialogResult.OK)
            {
                using FormNouvelleMission fNM = new FormNouvelleMission();
                fNM.ShowDialog();
                // Recharger les données après création
                ChargerDonnees();
                AfficherMissions();
            }
        }

        // ─── Bouton : Détail mission sélectionnée ──────────────────────────────
        private void btnVoirMission_Click(object sender, EventArgs e)
        {
            // TODO: récupérer la mission sélectionnée dans dgvMissions
            //       et ouvrir FormMission en lui passant les clés (nomPlanete, numero)
        }

        // ─── Bouton : Générer PDF de bilan ────────────────────────────────────
        private void btnGenererPdf_Click(object sender, EventArgs e)
        {
            // TODO: appeler PdfGenerator.GenererBilanMission(nomPlanete, numero)
        }

        // ─── Bouton : Races répertoriées ──────────────────────────────────────
        private void btnRaces_Click(object sender, EventArgs e)
        {
            new FormRaces().Show();
        }

        // ─── Bouton : Planètes ────────────────────────────────────────────────
        private void btnPlanetes_Click(object sender, EventArgs e)
        {
            new FormPlanetes().Show();
        }

        // ─── Bouton : Statistiques ────────────────────────────────────────────
        private void btnStats_Click(object sender, EventArgs e)
        {
            new FormStats().Show();
        }
    }
}
