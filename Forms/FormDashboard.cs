using System.Data;
using System.Data.SQLite;
using appliPandora.Classes;

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
            try
            {
                string[] tables = new[]
                {
                    "Planete", "Espece", "Ennemi", "Allie", "Habiter",
                    "Membre", "Militaire", "Civil",
                    "Mission", "Composer", "ObjectifCapture", "Capturer",
                    "JournalDeBord", "Depense", "TypeDepense",
                    "Contact", "Informateur", "Negocier"
                };
                foreach (string t in tables)
                {
                    if (MesDatas.DsGlobal.Tables.Contains(t))
                        MesDatas.DsGlobal.Tables.Remove(t);
                    using SQLiteDataAdapter ada = new SQLiteDataAdapter(
                        $"SELECT * FROM {t}", Connexion.Connec);
                    DataTable dt = new DataTable(t);
                    ada.Fill(dt);
                    MesDatas.DsGlobal.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de chargement :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Connexion.FermerConnexion();
            }
        }

        /// <summary>
        /// Affiche les missions dans le DataGridView du tableau de bord.
        /// </summary>
        private void AfficherMissions()
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Mission")) return;
            dgvMissions.DataSource = MesDatas.DsGlobal.Tables["Mission"];
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
            if (dgvMissions.CurrentRow == null) return;
            string? nomPlanete = dgvMissions.CurrentRow.Cells["nomPlanete"].Value?.ToString();
            if (nomPlanete == null) return;
            if (!int.TryParse(dgvMissions.CurrentRow.Cells["numero"].Value?.ToString(), out int numero)) return;

            using FormMission fm = new FormMission(nomPlanete, numero);
            fm.ShowDialog();
            ChargerDonnees();
            AfficherMissions();
        }

        // ─── Bouton : Générer PDF de bilan ────────────────────────────────────
        private void btnGenererPdf_Click(object sender, EventArgs e)
        {
            if (dgvMissions.CurrentRow == null)
            {
                MessageBox.Show("Sélectionnez d'abord une mission.",
                    "PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string? nomPlanete = dgvMissions.CurrentRow.Cells["nomPlanete"].Value?.ToString();
            if (nomPlanete == null) return;
            if (!int.TryParse(dgvMissions.CurrentRow.Cells["numero"].Value?.ToString(), out int numero)) return;

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Filter   = "Fichier PDF|*.pdf",
                FileName = $"Bilan_{nomPlanete}_{numero}.pdf",
                Title    = "Enregistrer le bilan de mission"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try   { PdfGenerator.GenererBilanMission(nomPlanete, numero, sfd.FileName); }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur PDF :\n{ex.Message}",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
