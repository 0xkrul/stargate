using System.Data;
using System.Data.SQLite;
using appliPandora.Classes;

namespace appliPandora.Forms
{
    /// <summary>
    /// Volet 3 — Récapitulatif complet d'une mission sélectionnée.
    /// Mode déconnecté : toutes les données proviennent de MesDatas.DsGlobal.
    /// Permet aussi la saisie de nouvelles dépenses, événements et contacts
    /// tant que la mission n'est pas terminée (mode connecté momentané).
    /// </summary>
    public partial class FormMission : Form
    {
        // Clés primaires de la mission courante
        private readonly string _nomPlanete;
        private readonly int    _numero;
        private DataRow? _missionRow; // référence au DataRow de la mission courante

        public FormMission(string nomPlanete, int numero)
        {
            InitializeComponent();
            UiTheme.Apply(this);
            _nomPlanete = nomPlanete;
            _numero     = numero;
            this.Load  += FormMission_Load;
        }

        private void FormMission_Load(object? sender, EventArgs e)
        {
            ChargerComboBoxes();
            AfficherInfosMission();
            AfficherEquipage();
            AfficherObjectifsCaptures();
            AfficherBudget();
            VerifierStatutMission();
        }

        /// <summary>Remplit les ComboBox de saisie depuis le DataSet.</summary>
        private void ChargerComboBoxes()
        {
            if (MesDatas.DsGlobal.Tables.Contains("TypeDepense"))
            {
                cboDepType.DataSource    = MesDatas.DsGlobal.Tables["TypeDepense"];
                cboDepType.DisplayMember = "libelle";
                cboDepType.ValueMember   = "id";
                cboDepType.SelectedIndex = -1;
            }
            if (MesDatas.DsGlobal.Tables.Contains("Informateur"))
            {
                cboCtcInformateur.DataSource    = MesDatas.DsGlobal.Tables["Informateur"];
                cboCtcInformateur.DisplayMember = "nomCode";
                cboCtcInformateur.ValueMember   = "nomCode";
                cboCtcInformateur.SelectedIndex = -1;
            }
        }

        private void AfficherInfosMission()
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Mission")) return;
            DataRow[] rows = MesDatas.DsGlobal.Tables["Mission"]!
                .Select($"nomPlanete = '{_nomPlanete}' AND numero = {_numero}");
            if (rows.Length == 0) return;

            _missionRow = rows[0];

            // Chef de mission
            string chef = _missionRow["matriculeChef"]?.ToString() ?? "";
            if (MesDatas.DsGlobal.Tables.Contains("Membre"))
            {
                DataRow[] mb = MesDatas.DsGlobal.Tables["Membre"]!
                    .Select($"matricule = '{chef}'");
                if (mb.Length > 0)
                    chef = $"{mb[0]["nom"]} {mb[0]["prenom"]} ({chef})";
            }

            this.Text              = $"Stargate — Mission {_nomPlanete} #{_numero}";
            lblTitreMission.Text   = $"Mission {_nomPlanete} #{_numero}  |  Chef : {chef}";
            lblDateDepart.Text     = $"Départ : {_missionRow["dateDepart"]}";
            lblDateRetour.Text     = $"Retour prévu : {_missionRow["dateRetour"]}";
            txtFeuille.Text        = _missionRow["feuilleDeRoute"]?.ToString() ?? "";
        }

        private void AfficherEquipage()
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Composer") ||
                !MesDatas.DsGlobal.Tables.Contains("Membre")) return;

            string chefMat = _missionRow?["matriculeChef"]?.ToString() ?? "";
            DataRow[] composeurs = MesDatas.DsGlobal.Tables["Composer"]!
                .Select($"nomPlanete = '{_nomPlanete}' AND numeroMission = {_numero}");

            DataTable dt = new DataTable();
            dt.Columns.Add("Matricule", typeof(string));
            dt.Columns.Add("Nom",       typeof(string));
            dt.Columns.Add("Prénom",    typeof(string));
            dt.Columns.Add("Type",      typeof(string));
            dt.Columns.Add("Chef ?",    typeof(string));

            foreach (DataRow c in composeurs)
            {
                string mat = c["matriculeMembre"].ToString()!;
                DataRow[] mb = MesDatas.DsGlobal.Tables["Membre"]!
                    .Select($"matricule = '{mat}'");
                if (mb.Length == 0) continue;

                bool estMil = MesDatas.DsGlobal.Tables.Contains("Militaire") &&
                    MesDatas.DsGlobal.Tables["Militaire"]!
                        .Select($"matriculeMembre = '{mat}'").Length > 0;

                dt.Rows.Add(mat, mb[0]["nom"], mb[0]["prenom"],
                    estMil ? "Militaire" : "Civil",
                    mat == chefMat ? "★ Chef" : "");
            }
            dgvEquipage.DataSource = dt;
        }

        private void AfficherObjectifsCaptures()
        {
            dgvCaptures.DataSource = PdfGenerator.ObtenirBilanCaptures(_nomPlanete, _numero);
        }

        private void AfficherBudget()
        {
            if (_missionRow == null) return;
            int budgetInitial = Convert.ToInt32(_missionRow["budget"]);
            int consomme = 0;

            if (MesDatas.DsGlobal.Tables.Contains("Depense"))
            {
                foreach (DataRow d in MesDatas.DsGlobal.Tables["Depense"]!
                    .Select($"nomPlanete='{_nomPlanete}' AND numeroMission={_numero}"))
                    consomme += Convert.ToInt32(d["montant"]);
            }

            lblBudgetInitial.Text  = $"Budget initial : {budgetInitial:N0} $ gal.";
            lblBudgetConsomme.Text = $"Budget consommé : {consomme:N0} $ gal.";
            int restant = budgetInitial - consomme;
            lblBudgetRestant.Text      = $"Budget restant : {restant:N0} $ gal.";
            lblBudgetRestant.ForeColor = restant < 0
                ? System.Drawing.Color.Red
                : System.Drawing.Color.DarkGreen;
        }

        private void VerifierStatutMission()
        {
            if (_missionRow == null) return;
            if (DateTime.TryParse(_missionRow["dateRetour"]?.ToString(), out DateTime dateRetour)
                && dateRetour.Date < DateTime.Today)
            {
                tabSaisie.Enabled = false;
                tabSaisie.Text    = "Ajouter des données (mission terminée)";
            }
        }

        // ─── Bouton : Accès au journal ────────────────────────────────────────
        private void btnJournal_Click(object? sender, EventArgs e)
        {
            using FormJournal fj = new FormJournal(_nomPlanete, _numero);
            fj.ShowDialog();
        }

        // ─── Ajout d'une dépense (mode connecté momentané) ───────────────────
        private void btnAjouterDepense_Click(object? sender, EventArgs e)
        {
            if (cboDepType.SelectedValue == null || string.IsNullOrWhiteSpace(txtDepMotif.Text))
            {
                MessageBox.Show("Type et motif de dépense obligatoires.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudDepMontant.Value <= 0)
            {
                MessageBox.Show("Le montant doit être supérieur à 0.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_missionRow != null)
            {
                int budgetInitial = Convert.ToInt32(_missionRow["budget"]);
                int consomme = 0;

                if (MesDatas.DsGlobal.Tables.Contains("Depense"))
                {
                    string safePlanete = _nomPlanete.Replace("'", "''");
                    foreach (DataRow d in MesDatas.DsGlobal.Tables["Depense"]!
                        .Select($"nomPlanete='{safePlanete}' AND numeroMission={_numero}"))
                    {
                        consomme += Convert.ToInt32(d["montant"]);
                    }
                }

                int nouveauTotal = consomme + (int)nudDepMontant.Value;
                if (nouveauTotal > budgetInitial)
                {
                    DialogResult choix = MessageBox.Show(
                        $"Cette depense fera depasser le budget ({nouveauTotal:N0} / {budgetInitial:N0}). Continuer ?",
                        "Budget depasse",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (choix != DialogResult.Yes)
                        return;
                }
            }

            try
            {
                using SQLiteCommand cmdMax = new SQLiteCommand(
                    "SELECT COALESCE(MAX(id),0)+1 FROM Depense WHERE nomPlanete=@p AND numeroMission=@n",
                    Connexion.Connec);
                cmdMax.Parameters.AddWithValue("@p", _nomPlanete);
                cmdMax.Parameters.AddWithValue("@n", _numero);
                int nextId = Convert.ToInt32(cmdMax.ExecuteScalar());

                using SQLiteCommand cmd = new SQLiteCommand(
                    "INSERT INTO Depense(nomPlanete,numeroMission,id,dateD,montant,motif,idTypeDepense) VALUES(@p,@n,@id,@d,@m,@mo,@t)",
                    Connexion.Connec);
                cmd.Parameters.AddWithValue("@p",  _nomPlanete);
                cmd.Parameters.AddWithValue("@n",  _numero);
                cmd.Parameters.AddWithValue("@id", nextId);
                cmd.Parameters.AddWithValue("@d",  dtpDepDate.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@m",  (int)nudDepMontant.Value);
                cmd.Parameters.AddWithValue("@mo", txtDepMotif.Text.Trim());
                cmd.Parameters.AddWithValue("@t",  cboDepType.SelectedValue!);
                cmd.ExecuteNonQuery();

                RafraichirTable("Depense");
                AfficherBudget();
                txtDepMotif.Clear();
                nudDepMontant.Value = 0;
                MessageBox.Show("Dépense ajoutée.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur SQL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Connexion.FermerConnexion(); }
        }

        // ─── Ajout d'un événement journal ────────────────────────────────────
        private void btnAjouterEvenement_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEvtCommentaire.Text))
            {
                MessageBox.Show("Le commentaire ne peut pas être vide.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using SQLiteCommand cmd = new SQLiteCommand(
                    "INSERT INTO JournalDeBord(nomPlanete,numero,dateJ,commentaires) VALUES(@p,@n,@d,@c)",
                    Connexion.Connec);
                cmd.Parameters.AddWithValue("@p", _nomPlanete);
                cmd.Parameters.AddWithValue("@n", _numero);
                cmd.Parameters.AddWithValue("@d", dtpEvtDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@c", txtEvtCommentaire.Text.Trim());
                cmd.ExecuteNonQuery();

                RafraichirTable("JournalDeBord");
                txtEvtCommentaire.Clear();
                MessageBox.Show("Événement ajouté au journal.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur SQL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Connexion.FermerConnexion(); }
        }

        // ─── Ajout d'un contact informateur ──────────────────────────────────
        private void btnAjouterContact_Click(object? sender, EventArgs e)
        {
            if (cboCtcInformateur.SelectedValue == null)
            {
                MessageBox.Show("Sélectionnez un informateur.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudCtcSomme.Value <= 0)
            {
                MessageBox.Show("La somme versée doit être supérieure à 0.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using SQLiteCommand cmd = new SQLiteCommand(
                    "INSERT INTO Contact(nomPlanete,numeroMission,dateC,sommeVersee,appreciation,nomCodeInformateur) VALUES(@p,@n,@d,@s,@a,@i)",
                    Connexion.Connec);
                cmd.Parameters.AddWithValue("@p", _nomPlanete);
                cmd.Parameters.AddWithValue("@n", _numero);
                cmd.Parameters.AddWithValue("@d", dtpCtcDate.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@s", (int)nudCtcSomme.Value);
                cmd.Parameters.AddWithValue("@a", txtCtcAppreciation.Text.Trim());
                cmd.Parameters.AddWithValue("@i", cboCtcInformateur.SelectedValue!.ToString());
                cmd.ExecuteNonQuery();

                RafraichirTable("Contact");
                nudCtcSomme.Value = 0;
                txtCtcAppreciation.Clear();
                MessageBox.Show("Contact enregistré.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur SQL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Connexion.FermerConnexion(); }
        }

        /// <summary>Recharge une table du DataSet depuis la base (mode connecté momentané).</summary>
        private void RafraichirTable(string nomTable)
        {
            if (MesDatas.DsGlobal.Tables.Contains(nomTable))
                MesDatas.DsGlobal.Tables.Remove(nomTable);
            using SQLiteDataAdapter ada = new SQLiteDataAdapter(
                $"SELECT * FROM {nomTable}", Connexion.Connec);
            DataTable dt = new DataTable(nomTable);
            ada.Fill(dt);
            MesDatas.DsGlobal.Tables.Add(dt);
        }
    }
}


