using System.Data;

namespace appliPandora.Forms
{
    /// <summary>
    /// Volet 5 — Liste des races répertoriées (mode déconnecté).
    /// Filtres disponibles : type (Allie / Ennemi / Tout), couleur, planète.
    /// Clic sur une espèce → détails selon son type.
    /// </summary>
    public partial class FormRaces : Form
    {
        public FormRaces()
        {
            InitializeComponent();
            this.Load += FormRaces_Load;
        }

        private void FormRaces_Load(object sender, EventArgs e)
        {
            RemplirFiltres();
            AppliquerFiltres();
        }

        // ─── Remplissage des filtres ──────────────────────────────────────────
        private void RemplirFiltres()
        {
            // Filtre "Type"
            cboFiltreType.Items.Clear();
            cboFiltreType.Items.AddRange(new object[] { "Tout", "Allié", "Ennemi" });
            cboFiltreType.SelectedIndex = 0;

            // Filtre "Couleur" : liste des couleurs distinctes depuis DataSet
            // TODO: remplir cboFiltreCouleur depuis MesDatas.DsGlobal["Espece"]

            // Filtre "Planète"
            // TODO: remplir cboFiltrePlanete depuis MesDatas.DsGlobal["Planete"]
        }

        // ─── Application des filtres ──────────────────────────────────────────
        private void AppliquerFiltres()
        {
            // TODO: construire un DataView sur Espece JOIN Ennemi/Allie selon les filtres
            //       et lier à dgvRaces
        }

        private void btnFiltrer_Click(object sender, EventArgs e)
        {
            AppliquerFiltres();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cboFiltreType.SelectedIndex = 0;
            cboFiltreCouleur.SelectedIndex = -1;
            cboFiltrePlanete.SelectedIndex = -1;
            AppliquerFiltres();
        }

        // ─── Sélection d'une race → affichage des détails ─────────────────────
        private void dgvRaces_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRaces.CurrentRow == null) return;

            // Récupérer l'id de l'espèce sélectionnée
            // TODO: afficher dans le panneau de droite (grpDetails) les champs
            //       appropriés selon qu'elle est Ennemi ou Allie :
            //       Ennemi  → typeArme, degreAgressivite
            //       Allie   → datePremierContact, degreBienveillance, instrumentMusique
            //       Commun  → couleur, planètes habitées + pourcentage
        }
    }
}
