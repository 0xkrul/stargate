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
        private void FormDashboard_Load(object? sender, EventArgs e)
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

            DataTable source = MesDatas.DsGlobal.Tables["Mission"]!;
            DataTable tableau = new DataTable();
            tableau.Columns.Add("nomPlanete", typeof(string));
            tableau.Columns.Add("numero", typeof(int));
            tableau.Columns.Add("Mission", typeof(string));
            tableau.Columns.Add("Statut", typeof(string));
            tableau.Columns.Add("Depart", typeof(string));
            tableau.Columns.Add("Retour prevu", typeof(string));
            tableau.Columns.Add("Chef", typeof(string));
            tableau.Columns.Add("Equipage", typeof(string));
            tableau.Columns.Add("Budget initial", typeof(string));
            tableau.Columns.Add("Budget restant", typeof(string));
            tableau.Columns.Add("Objectif DataBaz", typeof(string));

            foreach (DataRow mission in source.Select("", "dateDepart DESC"))
            {
                string nomPlanete = mission["nomPlanete"].ToString() ?? "";
                int numero = Convert.ToInt32(mission["numero"]);
                int budget = Convert.ToInt32(mission["budget"]);
                int depenses = TotalDepenses(nomPlanete, numero);
                int nbMembres = NombreMembres(nomPlanete, numero);
                int requis = Convert.ToInt32(mission["nbMembreRequis"]);

                tableau.Rows.Add(
                    nomPlanete,
                    numero,
                    $"{nomPlanete} #{numero}",
                    StatutMission(mission),
                    mission["dateDepart"].ToString(),
                    mission["dateRetour"].ToString(),
                    NomMembre(mission["matriculeChef"].ToString() ?? ""),
                    $"{nbMembres}/{requis}",
                    $"{budget:N0} $ gal.",
                    $"{budget - depenses:N0} $ gal.",
                    $"{Convert.ToInt32(mission["objectifDatabaz"]):N0} kg");
            }

            dgvMissions.DataSource = tableau;
            if (dgvMissions.Columns.Contains("nomPlanete"))
                dgvMissions.Columns["nomPlanete"]!.Visible = false;
            if (dgvMissions.Columns.Contains("numero"))
                dgvMissions.Columns["numero"]!.Visible = false;
            dgvMissions.AutoResizeColumns();
        }

        private static string StatutMission(DataRow mission)
        {
            DateTime.TryParse(mission["dateDepart"]?.ToString(), out DateTime depart);
            DateTime.TryParse(mission["dateRetour"]?.ToString(), out DateTime retour);
            DateTime today = DateTime.Today;

            if (depart != default && depart.Date > today) return "A venir";
            if (retour != default && retour.Date < today) return "Passee";
            return "En cours";
        }

        private static int NombreMembres(string nomPlanete, int numero)
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Composer")) return 0;
            return MesDatas.DsGlobal.Tables["Composer"]!
                .Select($"nomPlanete = '{Filtre(nomPlanete)}' AND numeroMission = {numero}")
                .Length;
        }

        private static int TotalDepenses(string nomPlanete, int numero)
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Depense")) return 0;
            int total = 0;
            foreach (DataRow depense in MesDatas.DsGlobal.Tables["Depense"]!
                .Select($"nomPlanete = '{Filtre(nomPlanete)}' AND numeroMission = {numero}"))
                total += Convert.ToInt32(depense["montant"]);
            return total;
        }

        private static string NomMembre(string matricule)
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Membre")) return matricule;
            DataRow[] rows = MesDatas.DsGlobal.Tables["Membre"]!
                .Select($"matricule = '{Filtre(matricule)}'");
            return rows.Length == 0
                ? matricule
                : $"{rows[0]["nom"]} {rows[0]["prenom"]}";
        }

        private static string Filtre(string valeur) => valeur.Replace("'", "''");

        // ─── Bouton : Nouvelle mission (admin requis) ──────────────────────────
        private void btnNouvelleMission_Click(object? sender, EventArgs e)
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
        private void btnVoirMission_Click(object? sender, EventArgs e)
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
        private void btnGenererPdf_Click(object? sender, EventArgs e)
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
        private void btnRaces_Click(object? sender, EventArgs e)
        {
            new FormRaces().Show();
        }

        // ─── Bouton : Planètes ────────────────────────────────────────────────
        private void btnPlanetes_Click(object? sender, EventArgs e)
        {
            new FormPlanetes().Show();
        }

        // ─── Bouton : Statistiques ────────────────────────────────────────────
        private void btnStats_Click(object? sender, EventArgs e)
        {
            new FormStats().Show();
        }
    }
}

