using System.Data;
using appliPandora.Classes;

namespace appliPandora.Forms
{
    /// <summary>
    /// Volet 5 â€” Liste des races rÃ©pertoriÃ©es (mode dÃ©connectÃ©).
    /// Filtres disponibles : type (Allie / Ennemi / Tout), couleur, planÃ¨te.
    /// Clic sur une espÃ¨ce â†’ dÃ©tails selon son type.
    /// </summary>
    public partial class FormRaces : Form
    {
        public FormRaces()
        {
            InitializeComponent();
            this.Load += FormRaces_Load;
        }

        private void FormRaces_Load(object? sender, EventArgs e)
        {
            RemplirFiltres();
            AppliquerFiltres();
        }

        // â”€â”€â”€ Remplissage des filtres â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
        private void RemplirFiltres()
        {
            // Filtre "Type"
            cboFiltreType.Items.Clear();
            cboFiltreType.Items.AddRange(new object[] { "Tout", "AlliÃ©", "Ennemi" });
            cboFiltreType.SelectedIndex = 0;

            // Filtre "Couleur"
            cboFiltreCouleur.Items.Clear();
            cboFiltreCouleur.Items.Add("Toutes");
            if (MesDatas.DsGlobal.Tables.Contains("Espece"))
            {
                var couleurs = MesDatas.DsGlobal.Tables["Espece"]!.AsEnumerable()
                    .Select(r => r["couleur"]?.ToString() ?? "")
                    .Where(c => !string.IsNullOrEmpty(c))
                    .Distinct().OrderBy(c => c);
                foreach (string c in couleurs)
                    cboFiltreCouleur.Items.Add(c);
            }
            cboFiltreCouleur.SelectedIndex = 0;

            // Filtre "PlanÃ¨te"
            cboFiltrePlanete.Items.Clear();
            cboFiltrePlanete.Items.Add("Toutes");
            if (MesDatas.DsGlobal.Tables.Contains("Planete"))
            {
                foreach (DataRow r in MesDatas.DsGlobal.Tables["Planete"]!.Rows)
                    cboFiltrePlanete.Items.Add(r["nom"].ToString()!);
            }
            cboFiltrePlanete.SelectedIndex = 0;
        }

        // â”€â”€â”€ Application des filtres â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
        private void AppliquerFiltres()
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Espece")) return;

            string filtreType    = cboFiltreType.SelectedItem?.ToString()    ?? "Tout";
            string filtreCouleur = cboFiltreCouleur.SelectedItem?.ToString() ?? "Toutes";
            string filtrePlanete = cboFiltrePlanete.SelectedItem?.ToString() ?? "Toutes";

            var ennemis = MesDatas.DsGlobal.Tables.Contains("Ennemi")  ? MesDatas.DsGlobal.Tables["Ennemi"]!  : null;
            var allies  = MesDatas.DsGlobal.Tables.Contains("Allie")   ? MesDatas.DsGlobal.Tables["Allie"]!   : null;
            var habiter = MesDatas.DsGlobal.Tables.Contains("Habiter") ? MesDatas.DsGlobal.Tables["Habiter"]! : null;

            DataTable dt = new DataTable();
            dt.Columns.Add("id",      typeof(int));
            dt.Columns.Add("Nom",     typeof(string));
            dt.Columns.Add("Couleur", typeof(string));
            dt.Columns.Add("Type",    typeof(string));

            foreach (DataRow esp in MesDatas.DsGlobal.Tables["Espece"]!.Rows)
            {
                int    id       = Convert.ToInt32(esp["id"]);
                bool   estEn    = ennemis?.Select($"idEspece = {id}").Length > 0;
                bool   estAl    = allies?.Select($"idEspece = {id}").Length > 0;
                string type     = estEn ? "Ennemi" : estAl ? "AlliÃ©" : "Inconnu";

                if (filtreType == "Ennemi" && !estEn) continue;
                if (filtreType == "AlliÃ©"  && !estAl) continue;
                if (filtreCouleur != "Toutes" && esp["couleur"]?.ToString() != filtreCouleur) continue;
                if (filtrePlanete != "Toutes")
                {
                    bool surPlanete = habiter?.Select(
                        $"idEspece = {id} AND nomPlanete = '{filtrePlanete}'").Length > 0;
                    if (!surPlanete) continue;
                }
                dt.Rows.Add(id, esp["nom"], esp["couleur"], type);
            }

            dgvRaces.DataSource = dt;
            if (dgvRaces.Columns.Contains("id"))
                dgvRaces.Columns["id"]!.Visible = false;
        }

        private void btnFiltrer_Click(object? sender, EventArgs e)
        {
            AppliquerFiltres();
        }

        private void btnReset_Click(object? sender, EventArgs e)
        {
            cboFiltreType.SelectedIndex = 0;
            cboFiltreCouleur.SelectedIndex = -1;
            cboFiltrePlanete.SelectedIndex = -1;
            AppliquerFiltres();
        }

        // â”€â”€â”€ SÃ©lection d'une race â†’ affichage des dÃ©tails â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
        private void dgvRaces_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvRaces.CurrentRow == null) return;
            if (!dgvRaces.Columns.Contains("id")) return;
            if (!int.TryParse(dgvRaces.CurrentRow.Cells["id"].Value?.ToString(), out int id)) return;

            // Infos communes
            if (MesDatas.DsGlobal.Tables.Contains("Espece"))
            {
                DataRow[] esp = MesDatas.DsGlobal.Tables["Espece"]!.Select($"id = {id}");
                if (esp.Length > 0)
                {
                    lblDetNom.Text     = esp[0]["nom"].ToString()!;
                    lblDetCouleur.Text = $"Couleur : {esp[0]["couleur"]}";
                }
            }

            // Infos spÃ©cifiques au type
            bool estEnnemi = MesDatas.DsGlobal.Tables.Contains("Ennemi") &&
                MesDatas.DsGlobal.Tables["Ennemi"]!.Select($"idEspece = {id}").Length > 0;

            if (estEnnemi)
            {
                lblDetType.Text = "Type : Ennemi";
                DataRow[] en = MesDatas.DsGlobal.Tables["Ennemi"]!.Select($"idEspece = {id}");
                lblDetInfo1.Text = $"Type d'arme : {en[0]["typeArme"]}";
                lblDetInfo2.Text = $"AgressivitÃ© : {en[0]["degreAgressivite"]}";
                lblDetInfo3.Text = "";
            }
            else if (MesDatas.DsGlobal.Tables.Contains("Allie"))
            {
                DataRow[] al = MesDatas.DsGlobal.Tables["Allie"]!.Select($"idEspece = {id}");
                if (al.Length > 0)
                {
                    lblDetType.Text  = "Type : Allie";
                    lblDetInfo1.Text = $"Premier contact : {al[0]["datePremierContact"]}";
                    lblDetInfo2.Text = $"Bienveillance : {al[0]["degreBienveillance"]}";
                    lblDetInfo3.Text = $"Instrument : {al[0]["instrumentMusique"]}";
                }
            }

            // PlanÃ¨tes habitÃ©es
            if (MesDatas.DsGlobal.Tables.Contains("Habiter"))
            {
                DataRow[] hab = MesDatas.DsGlobal.Tables["Habiter"]!.Select($"idEspece = {id}");
                DataTable dtP = new DataTable();
                dtP.Columns.Add("PlanÃ¨te",    typeof(string));
                dtP.Columns.Add("PrÃ©sence %", typeof(string));
                foreach (DataRow h in hab)
                    dtP.Rows.Add(h["nomPlanete"], $"{h["pourcentage"]} %");
                dgvPlanetes.DataSource = dtP;
            }
        }
    }
}

