namespace appliPandora.Forms
{
    /// <summary>
    /// Volet 2 (étape 0) — Authentification administrateur.
    /// Vérifie le login et le mot de passe haché (BCrypt) dans la table Admin.
    /// Renvoie DialogResult.OK si l'authentification réussit.
    /// </summary>
    public partial class FormAuth : Form
    {
        public FormAuth()
        {
            InitializeComponent();
        }

        // ─── Bouton Connexion ─────────────────────────────────────────────────
        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string mdp   = txtMdp.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(mdp))
            {
                MessageBox.Show("Veuillez saisir le login et le mot de passe.", "Champs manquants",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: interroger la table Admin via Connexion.Connec
            //       et vérifier le mot de passe haché avec BCrypt.Net.BCrypt.Verify(mdp, hashBdd)
            // Si OK : this.DialogResult = DialogResult.OK; this.Close();
            // Sinon : afficher un message d'erreur
        }

        // ─── Bouton Annuler ───────────────────────────────────────────────────
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
