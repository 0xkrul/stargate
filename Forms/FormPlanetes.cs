using System.Data;

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
            // TODO: lier dgvPlanetes à MesDatas.DsGlobal["Planete"]
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
            // TODO: lire depuis DataSet, remplir lblTemp, lblGravite, lblDataBaz
        }

        private void AfficherEspeces(string nomPlanete)
        {
            // TODO: filtrer Habiter JOIN Espece JOIN (Ennemi ou Allie) sur nomPlanete
            //       lier à dgvEspeces — colonnes : Nom espèce | Type | % présence
        }

        private void AfficherMissions(string nomPlanete)
        {
            // TODO: filtrer Mission sur nomPlanete
            //       lier à dgvMissions — colonnes : N°, Chef, Départ, Retour
        }
    }
}
