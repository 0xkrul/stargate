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
            // TODO: remplir cboPlanete depuis MesDatas.DsGlobal["Planete"]
        }

        private void ChargerMilitaires()
        {
            // TODO: remplir cboChef depuis Membre JOIN Militaire (DataSet)
        }

        private void ChargerMembres()
        {
            // TODO: remplir lstMembresDisponibles depuis DataSet
        }

        private void ChargerEspecesEnnemies()
        {
            // TODO: remplir dgvObjectifs depuis Espece JOIN Ennemi (DataSet)
        }

        // ─── Étape 1 : Créer la mission ───────────────────────────────────────
        private void btnCreerMission_Click(object sender, EventArgs e)
        {
            // Validation des champs obligatoires
            if (cboPlanete.SelectedItem == null || cboChef.SelectedItem == null)
            {
                MessageBox.Show("Planète et chef de mission obligatoires.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: calculer le prochain numéro de mission pour la planète choisie
            // TODO: INSERT INTO Mission VALUES (...)
            // En cas de succès, activer tabEquipage
        }

        // ─── Étape 2 : Affecter les membres ──────────────────────────────────
        private void btnAjouterMembre_Click(object sender, EventArgs e)
        {
            // TODO: déplacer le membre sélectionné de lstMembresDisponibles vers lstMembresChoisis
        }

        private void btnRetirerMembre_Click(object sender, EventArgs e)
        {
            // TODO: retirer le membre de lstMembresChoisis et le remettre dans lstMembresDisponibles
        }

        private void btnValiderEquipage_Click(object sender, EventArgs e)
        {
            // TODO: INSERT INTO Composer pour chaque membre dans lstMembresChoisis
            // Activer tabObjectifs
        }

        // ─── Étape 3 : Objectifs de capture (transaction) ────────────────────
        private void btnValiderObjectifs_Click(object sender, EventArgs e)
        {
            // TODO: lire les quantités saisies dans dgvObjectifs
            // Ouvrir une transaction SQLite
            // Pour chaque ligne : INSERT INTO ObjectifCapture(...)
            // Si erreur : ROLLBACK et message
            // Sinon : COMMIT et fermer le formulaire
        }
    }
}
