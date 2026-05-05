namespace appliPandora.Forms
{
    partial class FormMission
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
            this.tabControl         = new System.Windows.Forms.TabControl();
            this.tabResume          = new System.Windows.Forms.TabPage();
            this.tabSaisie          = new System.Windows.Forms.TabPage();

            // Résumé
            this.lblTitreMission    = new System.Windows.Forms.Label();
            this.grpInfos           = new System.Windows.Forms.GroupBox();
            this.lblDateDepart      = new System.Windows.Forms.Label();
            this.lblDateRetour      = new System.Windows.Forms.Label();
            this.lblBudgetInitial   = new System.Windows.Forms.Label();
            this.lblBudgetConsomme  = new System.Windows.Forms.Label();
            this.lblBudgetRestant   = new System.Windows.Forms.Label();
            this.txtFeuille         = new System.Windows.Forms.TextBox();
            this.grpEquipage        = new System.Windows.Forms.GroupBox();
            this.dgvEquipage        = new System.Windows.Forms.DataGridView();
            this.grpCaptures        = new System.Windows.Forms.GroupBox();
            this.dgvCaptures        = new System.Windows.Forms.DataGridView();
            this.btnJournal         = new System.Windows.Forms.Button();

            // Saisie
            this.grpDepense         = new System.Windows.Forms.GroupBox();
            this.dtpDepDate         = new System.Windows.Forms.DateTimePicker();
            this.txtDepMotif        = new System.Windows.Forms.TextBox();
            this.nudDepMontant      = new System.Windows.Forms.NumericUpDown();
            this.cboDepType         = new System.Windows.Forms.ComboBox();
            this.btnAjouterDepense  = new System.Windows.Forms.Button();
            this.grpEvenement       = new System.Windows.Forms.GroupBox();
            this.dtpEvtDate         = new System.Windows.Forms.DateTimePicker();
            this.txtEvtCommentaire  = new System.Windows.Forms.TextBox();
            this.btnAjouterEvenement = new System.Windows.Forms.Button();
            this.grpContact         = new System.Windows.Forms.GroupBox();
            this.dtpCtcDate         = new System.Windows.Forms.DateTimePicker();
            this.cboCtcInformateur  = new System.Windows.Forms.ComboBox();
            this.nudCtcSomme        = new System.Windows.Forms.NumericUpDown();
            this.txtCtcAppreciation = new System.Windows.Forms.TextBox();
            this.btnAjouterContact  = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaptures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDepMontant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCtcSomme)).BeginInit();
            this.SuspendLayout();

            // tabControl
            this.tabControl.Location = new System.Drawing.Point(10, 10);
            this.tabControl.Size = new System.Drawing.Size(1150, 620);
            this.tabControl.Controls.Add(this.tabResume);
            this.tabControl.Controls.Add(this.tabSaisie);

            // ── tabResume ─────────────────────────────────────────────────────
            this.tabResume.Text = "Résumé de la mission";

            this.lblTitreMission.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitreMission.Location = new System.Drawing.Point(10, 10);
            this.lblTitreMission.Size = new System.Drawing.Size(700, 28);
            this.lblTitreMission.Text = "Mission — chargement…";

            // grpInfos
            this.grpInfos.Location = new System.Drawing.Point(10, 45);
            this.grpInfos.Size = new System.Drawing.Size(440, 220);
            this.grpInfos.Text = "Informations générales";
            this.lblDateDepart.Location     = new System.Drawing.Point(10, 25); this.lblDateDepart.Size = new System.Drawing.Size(420, 23); this.lblDateDepart.Text = "Départ : —";
            this.lblDateRetour.Location     = new System.Drawing.Point(10, 55); this.lblDateRetour.Size = new System.Drawing.Size(420, 23); this.lblDateRetour.Text = "Retour prévu : —";
            this.lblBudgetInitial.Location  = new System.Drawing.Point(10, 85); this.lblBudgetInitial.Size = new System.Drawing.Size(420, 23); this.lblBudgetInitial.Text = "Budget initial : —";
            this.lblBudgetConsomme.Location = new System.Drawing.Point(10, 115); this.lblBudgetConsomme.Size = new System.Drawing.Size(420, 23); this.lblBudgetConsomme.Text = "Budget consommé : —";
            this.lblBudgetRestant.Location  = new System.Drawing.Point(10, 145); this.lblBudgetRestant.Size = new System.Drawing.Size(420, 23); this.lblBudgetRestant.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.lblBudgetRestant.Text = "Budget restant : —";
            this.txtFeuille.Location = new System.Drawing.Point(10, 170); this.txtFeuille.Size = new System.Drawing.Size(420, 40); this.txtFeuille.Multiline = true; this.txtFeuille.ReadOnly = true; this.txtFeuille.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grpInfos.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblDateDepart, this.lblDateRetour, this.lblBudgetInitial, this.lblBudgetConsomme, this.lblBudgetRestant, this.txtFeuille });

            // grpEquipage
            this.grpEquipage.Location = new System.Drawing.Point(465, 45);
            this.grpEquipage.Size = new System.Drawing.Size(650, 220);
            this.grpEquipage.Text = "Équipage";
            this.dgvEquipage.Location = new System.Drawing.Point(10, 20);
            this.dgvEquipage.Size = new System.Drawing.Size(630, 185);
            this.dgvEquipage.AllowUserToAddRows = false;
            this.dgvEquipage.ReadOnly = true;
            this.grpEquipage.Controls.Add(this.dgvEquipage);

            // grpCaptures
            this.grpCaptures.Location = new System.Drawing.Point(10, 280);
            this.grpCaptures.Size = new System.Drawing.Size(1105, 250);
            this.grpCaptures.Text = "Bilan des captures";
            this.dgvCaptures.Location = new System.Drawing.Point(10, 20);
            this.dgvCaptures.Size = new System.Drawing.Size(1080, 210);
            this.dgvCaptures.AllowUserToAddRows = false;
            this.dgvCaptures.ReadOnly = true;
            this.grpCaptures.Controls.Add(this.dgvCaptures);

            this.btnJournal.Location = new System.Drawing.Point(10, 545);
            this.btnJournal.Size = new System.Drawing.Size(180, 40);
            this.btnJournal.Text = "📖 Accès au journal";
            this.btnJournal.Click += new System.EventHandler(this.btnJournal_Click);

            this.tabResume.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblTitreMission, this.grpInfos, this.grpEquipage, this.grpCaptures, this.btnJournal });

            // ── tabSaisie ─────────────────────────────────────────────────────
            this.tabSaisie.Text = "Ajouter des données";

            // grpDepense
            this.grpDepense.Location = new System.Drawing.Point(10, 10); this.grpDepense.Size = new System.Drawing.Size(500, 180); this.grpDepense.Text = "Nouvelle dépense";
            this.dtpDepDate.Location = new System.Drawing.Point(150, 25); this.dtpDepDate.Size = new System.Drawing.Size(200, 23);
            this.cboDepType.Location = new System.Drawing.Point(150, 55); this.cboDepType.Size = new System.Drawing.Size(200, 23); this.cboDepType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtDepMotif.Location = new System.Drawing.Point(150, 85); this.txtDepMotif.Size = new System.Drawing.Size(300, 23);
            this.nudDepMontant.Location = new System.Drawing.Point(150, 115); this.nudDepMontant.Size = new System.Drawing.Size(150, 23); this.nudDepMontant.Maximum = 9999999; this.nudDepMontant.ThousandsSeparator = true;
            this.btnAjouterDepense.Location = new System.Drawing.Point(350, 135); this.btnAjouterDepense.Size = new System.Drawing.Size(130, 30); this.btnAjouterDepense.Text = "Ajouter la dépense"; this.btnAjouterDepense.Click += new System.EventHandler(this.btnAjouterDepense_Click);
            var lblD1 = new System.Windows.Forms.Label() { Location = new System.Drawing.Point(10, 28), Size = new System.Drawing.Size(130, 23), Text = "Date :" };
            var lblD2 = new System.Windows.Forms.Label() { Location = new System.Drawing.Point(10, 58), Size = new System.Drawing.Size(130, 23), Text = "Type :" };
            var lblD3 = new System.Windows.Forms.Label() { Location = new System.Drawing.Point(10, 88), Size = new System.Drawing.Size(130, 23), Text = "Motif :" };
            var lblD4 = new System.Windows.Forms.Label() { Location = new System.Drawing.Point(10, 118), Size = new System.Drawing.Size(130, 23), Text = "Montant ($ gal.) :" };
            this.grpDepense.Controls.AddRange(new System.Windows.Forms.Control[] { lblD1, lblD2, lblD3, lblD4, this.dtpDepDate, this.cboDepType, this.txtDepMotif, this.nudDepMontant, this.btnAjouterDepense });

            // grpEvenement
            this.grpEvenement.Location = new System.Drawing.Point(10, 210); this.grpEvenement.Size = new System.Drawing.Size(500, 130); this.grpEvenement.Text = "Nouvel événement (journal de bord)";
            this.dtpEvtDate.Location = new System.Drawing.Point(150, 25); this.dtpEvtDate.Size = new System.Drawing.Size(200, 23);
            this.txtEvtCommentaire.Location = new System.Drawing.Point(150, 55); this.txtEvtCommentaire.Size = new System.Drawing.Size(300, 45); this.txtEvtCommentaire.Multiline = true;
            this.btnAjouterEvenement.Location = new System.Drawing.Point(350, 100); this.btnAjouterEvenement.Size = new System.Drawing.Size(130, 25); this.btnAjouterEvenement.Text = "Ajouter l'événement"; this.btnAjouterEvenement.Click += new System.EventHandler(this.btnAjouterEvenement_Click);
            var lblE1 = new System.Windows.Forms.Label() { Location = new System.Drawing.Point(10, 28), Size = new System.Drawing.Size(130, 23), Text = "Date :" };
            var lblE2 = new System.Windows.Forms.Label() { Location = new System.Drawing.Point(10, 58), Size = new System.Drawing.Size(130, 23), Text = "Commentaire :" };
            this.grpEvenement.Controls.AddRange(new System.Windows.Forms.Control[] { lblE1, lblE2, this.dtpEvtDate, this.txtEvtCommentaire, this.btnAjouterEvenement });

            // grpContact
            this.grpContact.Location = new System.Drawing.Point(530, 10); this.grpContact.Size = new System.Drawing.Size(560, 220); this.grpContact.Text = "Nouveau contact informateur";
            this.dtpCtcDate.Location = new System.Drawing.Point(160, 25); this.dtpCtcDate.Size = new System.Drawing.Size(200, 23);
            this.cboCtcInformateur.Location = new System.Drawing.Point(160, 55); this.cboCtcInformateur.Size = new System.Drawing.Size(280, 23); this.cboCtcInformateur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nudCtcSomme.Location = new System.Drawing.Point(160, 85); this.nudCtcSomme.Size = new System.Drawing.Size(150, 23); this.nudCtcSomme.Maximum = 9999999;
            this.txtCtcAppreciation.Location = new System.Drawing.Point(160, 115); this.txtCtcAppreciation.Size = new System.Drawing.Size(350, 60); this.txtCtcAppreciation.Multiline = true;
            this.btnAjouterContact.Location = new System.Drawing.Point(390, 183); this.btnAjouterContact.Size = new System.Drawing.Size(130, 28); this.btnAjouterContact.Text = "Ajouter le contact"; this.btnAjouterContact.Click += new System.EventHandler(this.btnAjouterContact_Click);
            var lblC1 = new System.Windows.Forms.Label() { Location = new System.Drawing.Point(10, 28), Size = new System.Drawing.Size(140, 23), Text = "Date du contact :" };
            var lblC2 = new System.Windows.Forms.Label() { Location = new System.Drawing.Point(10, 58), Size = new System.Drawing.Size(140, 23), Text = "Informateur :" };
            var lblC3 = new System.Windows.Forms.Label() { Location = new System.Drawing.Point(10, 88), Size = new System.Drawing.Size(140, 23), Text = "Somme versée ($ gal.) :" };
            var lblC4 = new System.Windows.Forms.Label() { Location = new System.Drawing.Point(10, 118), Size = new System.Drawing.Size(140, 23), Text = "Appréciation :" };
            this.grpContact.Controls.AddRange(new System.Windows.Forms.Control[] { lblC1, lblC2, lblC3, lblC4, this.dtpCtcDate, this.cboCtcInformateur, this.nudCtcSomme, this.txtCtcAppreciation, this.btnAjouterContact });

            this.tabSaisie.Controls.AddRange(new System.Windows.Forms.Control[] { this.grpDepense, this.grpEvenement, this.grpContact });

            // FormMission
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 660);
            this.Controls.Add(this.tabControl);
            this.Name = "FormMission";
            this.Text = "Stargate — Détail de la mission";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaptures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDepMontant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCtcSomme)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabResume;
        private System.Windows.Forms.TabPage tabSaisie;
        private System.Windows.Forms.Label lblTitreMission;
        private System.Windows.Forms.GroupBox grpInfos;
        private System.Windows.Forms.Label lblDateDepart;
        private System.Windows.Forms.Label lblDateRetour;
        private System.Windows.Forms.Label lblBudgetInitial;
        private System.Windows.Forms.Label lblBudgetConsomme;
        private System.Windows.Forms.Label lblBudgetRestant;
        private System.Windows.Forms.TextBox txtFeuille;
        private System.Windows.Forms.GroupBox grpEquipage;
        private System.Windows.Forms.DataGridView dgvEquipage;
        private System.Windows.Forms.GroupBox grpCaptures;
        private System.Windows.Forms.DataGridView dgvCaptures;
        private System.Windows.Forms.Button btnJournal;
        private System.Windows.Forms.GroupBox grpDepense;
        private System.Windows.Forms.DateTimePicker dtpDepDate;
        private System.Windows.Forms.TextBox txtDepMotif;
        private System.Windows.Forms.NumericUpDown nudDepMontant;
        private System.Windows.Forms.ComboBox cboDepType;
        private System.Windows.Forms.Button btnAjouterDepense;
        private System.Windows.Forms.GroupBox grpEvenement;
        private System.Windows.Forms.DateTimePicker dtpEvtDate;
        private System.Windows.Forms.TextBox txtEvtCommentaire;
        private System.Windows.Forms.Button btnAjouterEvenement;
        private System.Windows.Forms.GroupBox grpContact;
        private System.Windows.Forms.DateTimePicker dtpCtcDate;
        private System.Windows.Forms.ComboBox cboCtcInformateur;
        private System.Windows.Forms.NumericUpDown nudCtcSomme;
        private System.Windows.Forms.TextBox txtCtcAppreciation;
        private System.Windows.Forms.Button btnAjouterContact;
    }
}
