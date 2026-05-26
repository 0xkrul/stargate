using System.Data;
using System.Data.SQLite;
using appliPandora.Classes;

namespace appliPandora.Forms
{
    /// <summary>
    /// Creation d'une nouvelle mission en trois etapes :
    /// mission, equipage, objectifs de capture.
    /// </summary>
    public partial class FormNouvelleMission : Form
    {
        private string _nomPlaneteCreee = string.Empty;
        private int _numeroCreee = 0;
        private string _matriculeChefCree = string.Empty;
        private int _nbMembresRequis = 0;

        public FormNouvelleMission()
        {
            InitializeComponent();
            UiTheme.Apply(this);
            this.Load += FormNouvelleMission_Load;
            dtpDateDepart.ValueChanged += DatesMission_ValueChanged;
            dtpDateRetour.ValueChanged += DatesMission_ValueChanged;
            cboPlanete.SelectedIndexChanged += cboPlanete_SelectedIndexChanged;
        }

        private void FormNouvelleMission_Load(object? sender, EventArgs e)
        {
            dtpDateDepart.Value = DateTime.Today;
            dtpDateRetour.Value = DateTime.Today.AddDays(7);

            ChargerPlanetes();
            ChargerMilitaires();
            ChargerMembres();
            ChargerEspecesEnnemies();
            ActualiserObjectifDataBaz();
        }

        private void DatesMission_ValueChanged(object? sender, EventArgs e)
        {
            if (_numeroCreee != 0)
                return;

            ChargerMilitaires();
            ChargerMembres();
        }

        private void cboPlanete_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_numeroCreee != 0)
                return;

            ActualiserObjectifDataBaz();
            ChargerEspecesEnnemies();
        }

        private void ChargerPlanetes()
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Planete")) return;

            cboPlanete.DataSource = MesDatas.DsGlobal.Tables["Planete"];
            cboPlanete.DisplayMember = "nom";
            cboPlanete.ValueMember = "nom";
            cboPlanete.SelectedIndex = -1;
        }

        private void ChargerMilitaires()
        {
            DataTable dt = MesDatas.ObtenirChefsDisponibles(
                dtpDateDepart.Value,
                dtpDateRetour.Value);

            cboChef.DataSource = dt;
            cboChef.DisplayMember = "affichage";
            cboChef.ValueMember = "matricule";
            cboChef.SelectedIndex = -1;
        }

        private void ChargerMembres()
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Membre") ||
                !MesDatas.DsGlobal.Tables.Contains("Militaire")) return;

            lstMembresDisponibles.Items.Clear();
            lstMembresChoisis.Items.Clear();
            DataTable militaires = MesDatas.DsGlobal.Tables["Militaire"]!;
            string ignorePlanete = _numeroCreee == 0 ? string.Empty : _nomPlaneteCreee;
            int ignoreNumero = _numeroCreee == 0 ? -1 : _numeroCreee;

            foreach (DataRow row in MesDatas.DsGlobal.Tables["Membre"]!.Rows)
            {
                string matricule = row["matricule"].ToString()!;
                if (!MesDatas.EstDisponible(
                        matricule,
                        dtpDateDepart.Value,
                        dtpDateRetour.Value,
                        out _,
                        ignorePlanete,
                        ignoreNumero))
                    continue;

                string safeMatricule = matricule.Replace("'", "''");
                bool estMil = militaires.Select($"matriculeMembre = '{safeMatricule}'").Length > 0;
                string type = estMil ? "Mil." : "Civ.";
                lstMembresDisponibles.Items.Add(new MembreItem(
                    matricule,
                    $"[{type}] {row["nom"]} {row["prenom"]} - {matricule}"));
            }
        }

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
            DataTable dt = new DataTable();
            dt.Columns.Add("idEspece", typeof(int));
            dt.Columns.Add("Espece", typeof(string));
            dt.Columns.Add("Couleur", typeof(string));
            dt.Columns.Add("Objectif", typeof(int));

            if (!MesDatas.DsGlobal.Tables.Contains("Espece") ||
                !MesDatas.DsGlobal.Tables.Contains("Ennemi") ||
                !MesDatas.DsGlobal.Tables.Contains("Habiter") ||
                cboPlanete.SelectedValue == null)
            {
                AppliquerSourceObjectifs(dt);
                return;
            }

            string nomPlanete = cboPlanete.SelectedValue.ToString()!;
            string safePlanete = nomPlanete.Replace("'", "''");
            foreach (DataRow habitation in MesDatas.DsGlobal.Tables["Habiter"]!
                .Select($"nomPlanete = '{safePlanete}'"))
            {
                DataRow[] ennemis = MesDatas.DsGlobal.Tables["Ennemi"]!
                    .Select($"idEspece = {habitation["idEspece"]}");
                if (ennemis.Length == 0)
                    continue;

                DataRow[] especes = MesDatas.DsGlobal.Tables["Espece"]!
                    .Select($"id = {habitation["idEspece"]}");
                if (especes.Length == 0)
                    continue;

                dt.Rows.Add(habitation["idEspece"], especes[0]["nom"], especes[0]["couleur"], 0);
            }

            AppliquerSourceObjectifs(dt);
        }

        private void AppliquerSourceObjectifs(DataTable dt)
        {
            dgvObjectifs.DataSource = dt;
            dgvObjectifs.AllowUserToAddRows = false;
            dgvObjectifs.Columns["idEspece"]!.Visible = false;
            dgvObjectifs.Columns["Espece"]!.ReadOnly = true;
            dgvObjectifs.Columns["Couleur"]!.ReadOnly = true;
            dgvObjectifs.Columns["Objectif"]!.ReadOnly = false;
        }

        private void ActualiserObjectifDataBaz()
        {
            bool dataBazDisponible = true;

            if (MesDatas.DsGlobal.Tables.Contains("Planete") &&
                cboPlanete.SelectedValue != null)
            {
                string nomPlanete = cboPlanete.SelectedValue.ToString()!.Replace("'", "''");
                DataRow[] rows = MesDatas.DsGlobal.Tables["Planete"]!
                    .Select($"nom = '{nomPlanete}'");

                if (rows.Length > 0 &&
                    rows[0].Table.Columns.Contains("dataBazON") &&
                    Convert.ToInt32(rows[0]["dataBazON"]) == 0)
                {
                    dataBazDisponible = false;
                }
            }

            nudObjectifDB.Enabled = dataBazDisponible;
            if (!dataBazDisponible)
                nudObjectifDB.Value = 0;
        }

        private void btnCreerMission_Click(object? sender, EventArgs e)
        {
            if (cboPlanete.SelectedItem == null || cboChef.SelectedItem == null)
            {
                MessageBox.Show("Planete et chef de mission obligatoires.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDateRetour.Value.Date <= dtpDateDepart.Value.Date)
            {
                MessageBox.Show("La date de retour doit etre posterieure a la date de depart.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudBudget.Value <= 0)
            {
                MessageBox.Show("Le budget alloue doit etre superieur a 0.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFeuille.Text))
            {
                MessageBox.Show("La feuille de route est obligatoire.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ActualiserObjectifDataBaz();
            if (!nudObjectifDB.Enabled && nudObjectifDB.Value > 0)
            {
                MessageBox.Show("Cette planete ne possede pas de DataBaz.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nomPlanete = cboPlanete.SelectedValue!.ToString()!;
            string matriculeChef = cboChef.SelectedValue!.ToString()!;

            if (!MesDatas.EstDisponible(
                    matriculeChef,
                    dtpDateDepart.Value,
                    dtpDateRetour.Value,
                    out string missionInfo))
            {
                MessageBox.Show($"Ce chef est deja affecte a la mission {missionInfo}.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
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
                cmd.Parameters.AddWithValue("@p", nomPlanete);
                cmd.Parameters.AddWithValue("@n", nouveauNum);
                cmd.Parameters.AddWithValue("@nb", (int)nudNbMembres.Value);
                cmd.Parameters.AddWithValue("@dep", dtpDateDepart.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ret", dtpDateRetour.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@chef", matriculeChef);
                cmd.Parameters.AddWithValue("@fdr", txtFeuille.Text.Trim());
                cmd.Parameters.AddWithValue("@db", (int)nudObjectifDB.Value);
                cmd.Parameters.AddWithValue("@bud", (int)nudBudget.Value);
                cmd.ExecuteNonQuery();

                _nomPlaneteCreee = nomPlanete;
                _numeroCreee = nouveauNum;
                _matriculeChefCree = matriculeChef;
                _nbMembresRequis = (int)nudNbMembres.Value;

                MesDatas.RechargerTable("Mission");
                ChargerMembres();
                ChargerEspecesEnnemies();

                MessageBox.Show(
                    $"Mission {nomPlanete} #{nouveauNum} creee !\nPassez a l'onglet Equipage.",
                    "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tabEquipage.Enabled = true;
                tabControl.SelectedTab = tabEquipage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur SQL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Connexion.FermerConnexion(); }
        }

        private void btnAjouterMembre_Click(object? sender, EventArgs e)
        {
            var selected = lstMembresDisponibles.SelectedItems
                .Cast<MembreItem>().ToList();

            foreach (MembreItem item in selected)
            {
                lstMembresChoisis.Items.Add(item);
                lstMembresDisponibles.Items.Remove(item);
            }
        }

        private void btnRetirerMembre_Click(object? sender, EventArgs e)
        {
            var selected = lstMembresChoisis.SelectedItems
                .Cast<MembreItem>().ToList();

            foreach (MembreItem item in selected)
            {
                lstMembresDisponibles.Items.Add(item);
                lstMembresChoisis.Items.Remove(item);
            }
        }

        private void btnValiderEquipage_Click(object? sender, EventArgs e)
        {
            if (lstMembresChoisis.Items.Count != _nbMembresRequis)
            {
                MessageBox.Show($"L'equipage doit contenir exactement {_nbMembresRequis} membre(s).",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!lstMembresChoisis.Items.Cast<MembreItem>().Any(m => m.Matricule == _matriculeChefCree))
            {
                MessageBox.Show("Le chef de mission doit obligatoirement faire partie de l'equipage.",
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

                MesDatas.RechargerTable("Composer");
                MessageBox.Show("Equipage enregistre ! Definissez maintenant les objectifs de capture.",
                    "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabObjectifs.Enabled = true;
                tabControl.SelectedTab = tabObjectifs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur SQL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Connexion.FermerConnexion(); }
        }

        private void btnValiderObjectifs_Click(object? sender, EventArgs e)
        {
            SQLiteTransaction? transaction = null;
            try
            {
                transaction = Connexion.Connec.BeginTransaction();
                int nbObjectifs = 0;

                foreach (DataGridViewRow row in dgvObjectifs.Rows)
                {
                    if (row.IsNewRow) continue;

                    int idEspece = Convert.ToInt32(row.Cells["idEspece"].Value);
                    int objectif = Convert.ToInt32(row.Cells["Objectif"].Value);
                    if (objectif <= 0) continue;

                    nbObjectifs++;
                    using SQLiteCommand cmd = new SQLiteCommand(
                        "INSERT INTO ObjectifCapture (nomPlanete,numeroMission,idEspeceEnnemi,objectif) VALUES(@p,@n,@id,@obj)",
                        Connexion.Connec, transaction);
                    cmd.Parameters.AddWithValue("@p", _nomPlaneteCreee);
                    cmd.Parameters.AddWithValue("@n", _numeroCreee);
                    cmd.Parameters.AddWithValue("@id", idEspece);
                    cmd.Parameters.AddWithValue("@obj", objectif);
                    cmd.ExecuteNonQuery();
                }

                if (nbObjectifs == 0)
                    throw new InvalidOperationException("Au moins un objectif de capture positif est requis.");

                transaction.Commit();
                MesDatas.RechargerTable("ObjectifCapture");
                MessageBox.Show("Objectifs de capture enregistres.\nMission entierement creee !",
                    "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show(
                    $"Erreur - aucun objectif enregistre (transaction annulee) :\n{ex.Message}",
                    "Erreur de transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Connexion.FermerConnexion(); }
        }
    }
}
