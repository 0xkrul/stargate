namespace appliPandora.Forms
{
    partial class FormAuth
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitre      = new System.Windows.Forms.Label();
            this.lblLogin      = new System.Windows.Forms.Label();
            this.txtLogin      = new System.Windows.Forms.TextBox();
            this.lblMdp        = new System.Windows.Forms.Label();
            this.txtMdp        = new System.Windows.Forms.TextBox();
            this.btnConnexion  = new System.Windows.Forms.Button();
            this.btnAnnuler    = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitre
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitre.Location = new System.Drawing.Point(20, 15);
            this.lblTitre.Size = new System.Drawing.Size(340, 25);
            this.lblTitre.Text = "🔐 Authentification administrateur";

            // lblLogin
            this.lblLogin.Location = new System.Drawing.Point(20, 60);
            this.lblLogin.Size = new System.Drawing.Size(80, 23);
            this.lblLogin.Text = "Login :";

            // txtLogin
            this.txtLogin.Location = new System.Drawing.Point(110, 57);
            this.txtLogin.Size = new System.Drawing.Size(230, 23);
            this.txtLogin.Name = "txtLogin";

            // lblMdp
            this.lblMdp.Location = new System.Drawing.Point(20, 100);
            this.lblMdp.Size = new System.Drawing.Size(80, 23);
            this.lblMdp.Text = "Mot de passe :";

            // txtMdp
            this.txtMdp.Location = new System.Drawing.Point(110, 97);
            this.txtMdp.Size = new System.Drawing.Size(230, 23);
            this.txtMdp.PasswordChar = '•';
            this.txtMdp.Name = "txtMdp";

            // btnConnexion
            this.btnConnexion.Location = new System.Drawing.Point(110, 140);
            this.btnConnexion.Size = new System.Drawing.Size(100, 35);
            this.btnConnexion.Text = "Se connecter";
            this.btnConnexion.Click += new System.EventHandler(this.btnConnexion_Click);

            // btnAnnuler
            this.btnAnnuler.Location = new System.Drawing.Point(225, 140);
            this.btnAnnuler.Size = new System.Drawing.Size(80, 35);
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);

            // FormAuth
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 200);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblMdp);
            this.Controls.Add(this.txtMdp);
            this.Controls.Add(this.btnConnexion);
            this.Controls.Add(this.btnAnnuler);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAuth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connexion administrateur";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblMdp;
        private System.Windows.Forms.TextBox txtMdp;
        private System.Windows.Forms.Button btnConnexion;
        private System.Windows.Forms.Button btnAnnuler;
    }
}
