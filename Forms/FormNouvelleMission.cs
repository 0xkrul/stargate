using System.Data;
using System.Data.SQLite;

namespace appliPandora.Forms
{
    /// <summary>
    /// Volet 2 — Création d'une nouvelle mission (mode connecté, admin uniquement).
    /// 3 étapes :
    ///   Étape 1 : saisie des infos de la mission (INSERT Mission)
    ///   Étape 2 : affectation des membres de l'équipage (INSERT Composer)
    ///   Étape 3 : définition des objectifs de capture (INSERT ObjectifCapture — transaction)
    /// </summary>
    public partial class FormNouvelleMission : Form
    {
        // Clé de la mission créée à l'étape 1, utilisée aux étapes 2 et 3
        private string _nomPlaneteCreee = string.Empty;
        private int    _numeroCreee     = 0;

        public FormNouvelleMission()
        {
            InitializeComponent();
            this.Load += FormNouvelleMission_Load;
        }

        // ─── Chargement ───────────────────────────────────────────────────────
        private void FormNouvelleMission_Load(object sender, EventArgs e)
        {
            ChargerPlanetes();
            ChargerMilitaires();   // pour le ComboBox "chef de mission"
            ChargerMembres();      // pour la liste équipage
            ChargerEspecesEnnemies(); // pour les objectifs de capture
            dtpDateDepart.Value = DateTime.Today;
        }

        private void ChargerPlanetes()
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Planete")) return;
            cboPlanete.DataSource    = MesDatas.DsGlobal.Tables["Planete"];
            cboPlanete.DisplayMember = "nom";
            cboPlanete.ValueMember   = "nom";
            cboPlanete.SelectedIndex = -1;
        }

        private void ChargerMilitaires()
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Membre") ||
                !MesDatas.DsGlobal.Tables.Contains("Militaire")) return;

            DataTable dt = new DataTable();
            dt.Columns.Add("matricule", typeof(string));
            dt.Columns.Add("affichage", typeof(string));

            foreach (DataRow mil in MesDatas.DsGlobal.Tables["Militaire"]!.Rows)
            {
                DataRow[] mb = MesDatas.DsGlobal.Tables["Membre"]!
                    .Select($"matricule = '{mil["matriculeMembre"]}'" );
                if (mb.Length > 0)
                    dt.Rows.Add(
                        mb[0]["matricule"],
                        $"{mb[0]["nom"]} {mb[0]["prenom"]} ({mil["grade"]}) — {mb[0]["matricule"]}");
            }

            cboChef.DataSource    = dt;
            cboChef.DisplayMember = "affichage";
            cboChef.ValueMember   = "matricule";
            cboChef.SelectedIndex = -1;
        }

        private void ChargerMembres()
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Membre")) return;
            lstMembresDisponibles.Items.Clear();
            var militaires = MesDatas.DsGlobal.Tables["Militaire"]!;

            foreach (DataRow row in MesDatas.DsGlobal.Tables["Membre"]!.Rows)
            {
                bool estMil = militaires
                    .Select($"matriculeMembre = '{row["matricule"]}'").Length > 0;
                string type = estMil ? "Mil." : "Civ.";
                lstMembresDisponibles.Items.Add(new MembreItem(
                    row["matricule"].ToString()!,
                    $"[{type}] {row["nom"]} {row["prenom"]} — {row["matricule"]}"));
            }
        }

        // Objet de liste pour afficher les membres avec leur matricule
        private sealed class MembreItem
        {
            public string Matricule { get; }
            private string Affichage { get; }
            public MembreItem(string matricule, string affichage)
            {
                Matricule = matricule;
                Affichage = affichage;
            }
            public override string ToString() => Affichage;
        }

        private void ChargerEspecesEnnemies()
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Espece") ||
                !MesDatas.DsGlobal.Tables.Contains("Ennemi")) return;

            DataTable dt = new DataTable();
            dt.Columns.Add("idEspece", typeof(int));
            dt.Columns.Add("Espèce",   typeof(string));
            dt.Columns.Add("Couleur",   typeof(string));
            dt.Columns.Add("Objectif",  typeof(int));

            foreach (DataRow en in MesDatas.DsGlobal.Tables["Ennemi"]!.Rows)
            {
                DataRow[] esp = MesDatas.DsGlobal.Tables["Espece"]!
                    .Select($"id = {en["idEspece"]}");
                if (esp.Length > 0)
                    dt.Rows.Add(en["idEspece"], esp[0]["nom"], esp[0]["couleur"], 0);
            }

            dgvObjectifs.DataSource = dt;
            dgvObjectifs.Columns["idEspece"]!.Visible  = false;
            dgvObjectifs.Columns["Espèce"]!.ReadOnly   = true;
            dgvObjectifs.Columns["Couleur"]!.ReadOnly   = true;
            dgvObjectifs.Columns["Objectif"]!.ReadOnly  = false;
        }

        // ─── Étape 1 : Créer la mission ───────────────────────────────────────
        private void btnCreerMission_Click(object sender, EventArgs e)
        {
            if (cboPlanete.SelectedItem == null || cboChef.SelectedItem == null)
            {
                MessageBox.Show("Planète et chef de mission obligatoires.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dtpDateRetour.Value.Date <= dtpDateDepart.Value.Date)
            {
                MessageBox.Show("La date de retour doit être postérieure à la date de départ.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nomPlanete    = cboPlanete.SelectedValue!.ToString()!;
            string matriculeChef = cboChef.SelectedValue!.ToString()!;

            try
            {
                // Prochain numéro de mission pour cette planète
                using SQLiteCommand cmdMax = new SQLiteCommand(
                    "SELECT COALESCE(MAX(numero),0)+1 FROM Mission WHERE nomPlanete=@p",
                    Connexion.Connec);
                cmdMax.Parameters.AddWithValue("@p", nomPlanete);
                int nouveauNum = Convert.ToInt32(cmdMax.ExecuteScalar());

                string sql = @"
                    INSERT INTO Mission
                        (nomPlanete, numero, nbMembreRequis, dateDepart, dateRetour,
                         matriculeChef, feuilleDeRoute, objectifDatabaz, budget)
                    VALUES (@p,@n,@nb,@dep,@ret,@chef,@fdr,@db,@bud)";

                using SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                cmd.Parameters.AddWithValue("@p",    nomPlanete);
                cmd.Parameters.AddWithValue("@n",    nouveauNum);
                cmd.Parameters.AddWithValue("@nb",   (int)nudNbMembres.Value);
                cmd.Parameters.AddWithValue("@dep",  dtpDateDepart.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ret",  dtpDateRetour.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@chef", matriculeChef);
                cmd.Parameters.AddWithValue("@fdr",  txtFeuille.Text.Trim());
                cmd.Parameters.AddWithValue("@db",   (int)nudObjectifDB.Value);
                cmd.Parameters.AddWithValue("@bud",  (int)nudBudget.Value);
                cmd.ExecuteNonQuery();

                _nomPlaneteCreee = nomPlanete;
                _numeroCreee     = nouveauNum;

                MessageBox.Show(
                    $"Mission {nomPlanete} #{nouveauNum} créée !\nPassez à l'onglet Équipage.",
                    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tabEquipage.Enabled    = true;
                tabControl.SelectedTab = tabEquipage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur SQL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Connexion.FermerConnexion(); }
        }

        // ─── Étape 2 : Affecter les membres ──────────────────────────────────
        private void btnAjouterMembre_Click(object sender, EventArgs e)
        {
            var selected = lstMembresDisponibles.SelectedItems
                .Cast<MembreItem>().ToList();
            foreach (MembreItem item in selected)
            {
                lstMembresChoisis.Items.Add(item);
                lstMembresDisponibles.Items.Remove(item);
            }
        }

        private void btnRetirerMembre_Click(object sender, EventArgs e)
        {
            var selected = lstMembresChoisis.SelectedItems
                .Cast<MembreItem>().ToList();
            foreach (MembreItem item in selected)
            {
                lstMembresDisponibles.Items.Add(item);
                lstMembresChoisis.Items.Remove(item);
            }
        }

        private void btnValiderEquipage_Click(object sender, EventArgs e)
        {
            if (lstMembresChoisis.Items.Count == 0)
            {
                MessageBox.Show("Ajoutez au moins un membre à l'équipage.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                foreach (MembreItem item in lstMembresChoisis.Items)
                {
                    using SQLiteCommand cmd = new SQLiteCommand(
                        "INSERT OR IGNORE INTO Composer (nomPlanete,numeroMission,matriculeMembre) VALUES(@p,@n,@m)",
                        Connexion.Connec);
                    cmd.Parameters.AddWithValue("@p", _nomPlaneteCreee);
                    cmd.Parameters.AddWithValue("@n", _numeroCreee);
                    cmd.Parameters.AddWithValue("@m", item.Matricule);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Équipage enregistré ! Définissez maintenant les objectifs de capture.",
                    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabObjectifs.Enabled    = true;
                tabControl.SelectedTab  = tabObjectifs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur SQL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Connexion.FermerConnexion(); }
        }

        // ─── Étape 3 : Objectifs de capture (transaction) ────────────────────
        private void btnValiderObjectifs_Click(object sender, EventArgs e)
        {
            SQLiteTransaction? transaction = null;
            try
            {
                transaction = Connexion.Connec.BeginTransaction();

                foreach (DataGridViewRow row in dgvObjectifs.Rows)
                {
                    int idEspece = Convert.ToInt32(row.Cells["idEspece"].Value);
                    int objectif = Convert.ToInt32(row.Cells["Objectif"].Value);
                    if (objectif <= 0) continue;

                    using SQLiteCommand cmd = new SQLiteCommand(
                        "INSERT INTO ObjectifCapture (nomPlanete,numeroMission,idEspeceEnnemi,objectif) VALUES(@p,@n,@id,@obj)",
                        Connexion.Connec, transaction);
                    cmd.Parameters.AddWithValue("@p",   _nomPlaneteCreee);
                    cmd.Parameters.AddWithValue("@n",   _numeroCreee);
                    cmd.Parameters.AddWithValue("@id",  idEspece);
                    cmd.Parameters.AddWithValue("@obj", objectif);
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                MessageBox.Show("Objectifs de capture enregistrés.\nMission entièrement créée !",
                    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show(
                    $"Erreur — aucun objectif enregistré (transaction annulée) :\n{ex.Message}",
                    "Erreur de transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Connexion.FermerConnexion(); }
        }
    }
}
