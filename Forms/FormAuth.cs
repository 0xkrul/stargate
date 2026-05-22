using System.Data.SQLite;
using appliPandora.Classes;

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
        private void btnConnexion_Click(object? sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string mdp   = txtMdp.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(mdp))
            {
                MessageBox.Show("Veuillez saisir le login et le mot de passe.", "Champs manquants",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using SQLiteCommand cmd = new SQLiteCommand(
                    "SELECT mdp FROM Admin WHERE login = @login", Connexion.Connec);
                cmd.Parameters.AddWithValue("@login", login);
                string? hashBdd = cmd.ExecuteScalar()?.ToString();
                Connexion.FermerConnexion();

                if (hashBdd != null && BCrypt.Net.BCrypt.Verify(mdp, hashBdd))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login ou mot de passe incorrect.",
                        "Authentification échouée", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMdp.Clear();
                    txtMdp.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Connexion.FermerConnexion();
            }
        }

        // ─── Bouton Annuler ───────────────────────────────────────────────────
        private void btnAnnuler_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

