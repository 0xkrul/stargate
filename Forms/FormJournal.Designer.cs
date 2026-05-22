namespace appliPandora.Forms
{
    partial class FormJournal
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
            this.grpJournal           = new System.Windows.Forms.GroupBox();
            this.lblJournalDate       = new System.Windows.Forms.Label();
            this.txtJournalDate       = new System.Windows.Forms.TextBox();
            this.lblJournalCommentaire = new System.Windows.Forms.Label();
            this.txtJournalCommentaire = new System.Windows.Forms.TextBox();
            this.pnlNavigation        = new System.Windows.Forms.Panel();
            this.btnPremier           = new System.Windows.Forms.Button();
            this.btnPrecedent         = new System.Windows.Forms.Button();
            this.lblPosition          = new System.Windows.Forms.Label();
            this.btnSuivant           = new System.Windows.Forms.Button();
            this.btnDernier           = new System.Windows.Forms.Button();
            this.grpDepenses          = new System.Windows.Forms.GroupBox();
            this.dgvDepenses          = new System.Windows.Forms.DataGridView();
            this.grpContacts          = new System.Windows.Forms.GroupBox();
            this.dgvContacts          = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepenses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).BeginInit();
            this.SuspendLayout();

            // grpJournal
            this.grpJournal.Location = new System.Drawing.Point(10, 10);
            this.grpJournal.Size = new System.Drawing.Size(1100, 200);
            this.grpJournal.Text = "📖 Journal de bord — événement courant";
            this.lblJournalDate.Location = new System.Drawing.Point(10, 28); this.lblJournalDate.Size = new System.Drawing.Size(80, 23); this.lblJournalDate.Text = "Date :";
            this.txtJournalDate.Location = new System.Drawing.Point(100, 25); this.txtJournalDate.Size = new System.Drawing.Size(200, 23); this.txtJournalDate.ReadOnly = true; this.txtJournalDate.Name = "txtJournalDate";
            this.lblJournalCommentaire.Location = new System.Drawing.Point(10, 60); this.lblJournalCommentaire.Size = new System.Drawing.Size(80, 23); this.lblJournalCommentaire.Text = "Événement :";
            this.txtJournalCommentaire.Location = new System.Drawing.Point(100, 57); this.txtJournalCommentaire.Size = new System.Drawing.Size(970, 100); this.txtJournalCommentaire.Multiline = true; this.txtJournalCommentaire.ReadOnly = true; this.txtJournalCommentaire.ScrollBars = System.Windows.Forms.ScrollBars.Vertical; this.txtJournalCommentaire.Name = "txtJournalCommentaire";

            // pnlNavigation
            this.pnlNavigation.Location = new System.Drawing.Point(100, 165);
            this.pnlNavigation.Size = new System.Drawing.Size(600, 30);
            this.btnPremier.Location = new System.Drawing.Point(0, 0); this.btnPremier.Size = new System.Drawing.Size(80, 28); this.btnPremier.Text = "|< Premier"; this.btnPremier.Click += new System.EventHandler(this.btnPremier_Click);
            this.btnPrecedent.Location = new System.Drawing.Point(90, 0); this.btnPrecedent.Size = new System.Drawing.Size(80, 28); this.btnPrecedent.Text = "◀ Précédent"; this.btnPrecedent.Click += new System.EventHandler(this.btnPrecedent_Click);
            this.lblPosition.Location = new System.Drawing.Point(180, 5); this.lblPosition.Size = new System.Drawing.Size(160, 20); this.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; this.lblPosition.Text = "— / —";
            this.btnSuivant.Location = new System.Drawing.Point(350, 0); this.btnSuivant.Size = new System.Drawing.Size(80, 28); this.btnSuivant.Text = "Suivant >"; this.btnSuivant.Click += new System.EventHandler(this.btnSuivant_Click);
            this.btnDernier.Location = new System.Drawing.Point(440, 0); this.btnDernier.Size = new System.Drawing.Size(80, 28); this.btnDernier.Text = "Dernier >|"; this.btnDernier.Click += new System.EventHandler(this.btnDernier_Click);
            this.pnlNavigation.Controls.AddRange(new System.Windows.Forms.Control[] { this.btnPremier, this.btnPrecedent, this.lblPosition, this.btnSuivant, this.btnDernier });

            this.grpJournal.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblJournalDate, this.txtJournalDate, this.lblJournalCommentaire, this.txtJournalCommentaire, this.pnlNavigation });

            // grpDepenses
            this.grpDepenses.Location = new System.Drawing.Point(10, 225);
            this.grpDepenses.Size = new System.Drawing.Size(540, 280);
            this.grpDepenses.Text = "💸 Dépenses effectuées";
            this.dgvDepenses.Location = new System.Drawing.Point(10, 20);
            this.dgvDepenses.Size = new System.Drawing.Size(520, 245);
            this.dgvDepenses.AllowUserToAddRows = false;
            this.dgvDepenses.ReadOnly = true;
            this.grpDepenses.Controls.Add(this.dgvDepenses);

            // grpContacts
            this.grpContacts.Location = new System.Drawing.Point(565, 225);
            this.grpContacts.Size = new System.Drawing.Size(545, 280);
            this.grpContacts.Text = "🛸 Contacts avec informateurs";
            this.dgvContacts.Location = new System.Drawing.Point(10, 20);
            this.dgvContacts.Size = new System.Drawing.Size(525, 245);
            this.dgvContacts.AllowUserToAddRows = false;
            this.dgvContacts.ReadOnly = true;
            this.grpContacts.Controls.Add(this.dgvContacts);

            // FormJournal
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 520);
            this.Controls.Add(this.grpJournal);
            this.Controls.Add(this.grpDepenses);
            this.Controls.Add(this.grpContacts);
            this.Name = "FormJournal";
            this.Text = "Stargate — Journal de bord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepenses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContacts)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpJournal;
        private System.Windows.Forms.Label lblJournalDate;
        private System.Windows.Forms.TextBox txtJournalDate;
        private System.Windows.Forms.Label lblJournalCommentaire;
        private System.Windows.Forms.TextBox txtJournalCommentaire;
        private System.Windows.Forms.Panel pnlNavigation;
        private System.Windows.Forms.Button btnPremier;
        private System.Windows.Forms.Button btnPrecedent;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Button btnSuivant;
        private System.Windows.Forms.Button btnDernier;
        private System.Windows.Forms.GroupBox grpDepenses;
        private System.Windows.Forms.DataGridView dgvDepenses;
        private System.Windows.Forms.GroupBox grpContacts;
        private System.Windows.Forms.DataGridView dgvContacts;
    }
}

