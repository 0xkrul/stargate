using System.Data;
using appliPandora.Classes;

namespace appliPandora.Forms
{
    /// <summary>
    /// Volet 6 — Informations sur les planètes (mode déconnecté).
    /// Liste les planètes connues avec température, gravité, présence de DataBaz.
    /// Sélection d'une planète → espèces présentes (% allié/ennemi) + missions effectuées.
    /// </summary>
    public partial class FormPlanetes : Form
    {
        public FormPlanetes()
        {
            InitializeComponent();
            this.Load += FormPlanetes_Load;
        }

        private void FormPlanetes_Load(object sender, EventArgs e)
        {
            AfficherPlanetes();
        }

        private void AfficherPlanetes()
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Planete")) return;
            dgvPlanetes.DataSource = MesDatas.DsGlobal.Tables["Planete"];
            if (dgvPlanetes.Columns.Contains("nom"))         dgvPlanetes.Columns["nom"]!.HeaderText         = "Planète";
            if (dgvPlanetes.Columns.Contains("temperature")) dgvPlanetes.Columns["temperature"]!.HeaderText = "Temp. (°C)";
            if (dgvPlanetes.Columns.Contains("gravite"))     dgvPlanetes.Columns["gravite"]!.HeaderText     = "Gravité (g)";
            if (dgvPlanetes.Columns.Contains("dataBazON"))   dgvPlanetes.Columns["dataBazON"]!.HeaderText   = "DataBaz";
        }

        private void dgvPlanetes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPlanetes.CurrentRow == null) return;

            string? nomPlanete = dgvPlanetes.CurrentRow.Cells["nom"].Value?.ToString();
            if (nomPlanete == null) return;

            AfficherDetailsPlanete(nomPlanete);
            AfficherEspeces(nomPlanete);
            AfficherMissions(nomPlanete);
        }

        private void AfficherDetailsPlanete(string nomPlanete)
        {
            DataRow[] rows = MesDatas.DsGlobal.Tables["Planete"]!
                .Select($"nom = '{nomPlanete}'");
            if (rows.Length == 0) return;
            DataRow p = rows[0];
            lblNomPlanete.Text = nomPlanete;
            lblTemp.Text    = $"Température : {p["temperature"]} °C";
            lblGravite.Text = $"Gravité : {p["gravite"]} g";
            lblDataBaz.Text = $"DataBaz présent : {(Convert.ToInt32(p["dataBazON"]) == 1 ? "✓ Oui" : "✗ Non")}";
        }

        private void AfficherEspeces(string nomPlanete)
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Habiter") ||
                !MesDatas.DsGlobal.Tables.Contains("Espece")) return;

            DataRow[] habRows = MesDatas.DsGlobal.Tables["Habiter"]!
                .Select($"nomPlanete = '{nomPlanete}'");

            DataTable dt = new DataTable();
            dt.Columns.Add("Nom espèce",  typeof(string));
            dt.Columns.Add("Type",         typeof(string));
            dt.Columns.Add("Couleur",      typeof(string));
            dt.Columns.Add("Présence %",   typeof(string));

            foreach (DataRow h in habRows)
            {
                int id = Convert.ToInt32(h["idEspece"]);
                DataRow[] esp = MesDatas.DsGlobal.Tables["Espece"]!.Select($"id = {id}");
                if (esp.Length == 0) continue;

                bool estEn = MesDatas.DsGlobal.Tables.Contains("Ennemi") &&
                    MesDatas.DsGlobal.Tables["Ennemi"]!.Select($"idEspece = {id}").Length > 0;
                bool estAl = MesDatas.DsGlobal.Tables.Contains("Allie") &&
                    MesDatas.DsGlobal.Tables["Allie"]!.Select($"idEspece = {id}").Length > 0;

                dt.Rows.Add(
                    esp[0]["nom"],
                    estEn ? "⚔ Ennemi" : estAl ? "🤝 Allié" : "—",
                    esp[0]["couleur"],
                    $"{h["pourcentage"]} %");
            }
            dgvEspeces.DataSource = dt;
        }

        private void AfficherMissions(string nomPlanete)
        {
            if (!MesDatas.DsGlobal.Tables.Contains("Mission") ||
                !MesDatas.DsGlobal.Tables.Contains("Membre")) return;

            DataRow[] mRows = MesDatas.DsGlobal.Tables["Mission"]!
                .Select($"nomPlanete = '{nomPlanete}'", "numero ASC");

            DataTable dt = new DataTable();
            dt.Columns.Add("N°",      typeof(int));
            dt.Columns.Add("Chef",    typeof(string));
            dt.Columns.Add("Départ",  typeof(string));
            dt.Columns.Add("Retour",  typeof(string));

            foreach (DataRow m in mRows)
            {
                string chef = m["matriculeChef"]?.ToString() ?? "";
                DataRow[] mb = MesDatas.DsGlobal.Tables["Membre"]!
                    .Select($"matricule = '{chef}'");
                string nomChef = mb.Length > 0
                    ? $"{mb[0]["nom"]} {mb[0]["prenom"]}"
                    : chef;
                dt.Rows.Add(m["numero"], nomChef, m["dateDepart"], m["dateRetour"]);
            }
            dgvMissions.DataSource = dt;
        }
    }
}
