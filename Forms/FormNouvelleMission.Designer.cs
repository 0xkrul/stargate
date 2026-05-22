namespace appliPandora.Forms
{
    partial class FormNouvelleMission
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
            this.tabControl        = new System.Windows.Forms.TabControl();
            this.tabMission        = new System.Windows.Forms.TabPage();
            this.tabEquipage       = new System.Windows.Forms.TabPage();
            this.tabObjectifs      = new System.Windows.Forms.TabPage();

            // ── Onglet 1 : Mission ────────────────────────────────────────────
            this.lblPlanete        = new System.Windows.Forms.Label();
            this.cboPlanete        = new System.Windows.Forms.ComboBox();
            this.lblChef           = new System.Windows.Forms.Label();
            this.cboChef           = new System.Windows.Forms.ComboBox();
            this.lblDateDepart     = new System.Windows.Forms.Label();
            this.dtpDateDepart     = new System.Windows.Forms.DateTimePicker();
            this.lblDateRetour     = new System.Windows.Forms.Label();
            this.dtpDateRetour     = new System.Windows.Forms.DateTimePicker();
            this.lblNbMembres      = new System.Windows.Forms.Label();
            this.nudNbMembres      = new System.Windows.Forms.NumericUpDown();
            this.lblBudget         = new System.Windows.Forms.Label();
            this.nudBudget         = new System.Windows.Forms.NumericUpDown();
            this.lblFeuille        = new System.Windows.Forms.Label();
            this.txtFeuille        = new System.Windows.Forms.TextBox();
            this.lblObjectifDB     = new System.Windows.Forms.Label();
            this.nudObjectifDB     = new System.Windows.Forms.NumericUpDown();
            this.btnCreerMission   = new System.Windows.Forms.Button();

            // ── Onglet 2 : Équipage ───────────────────────────────────────────
            this.lstMembresDisponibles = new System.Windows.Forms.ListBox();
            this.lstMembresChoisis     = new System.Windows.Forms.ListBox();
            this.btnAjouterMembre      = new System.Windows.Forms.Button();
            this.btnRetirerMembre      = new System.Windows.Forms.Button();
            this.btnValiderEquipage    = new System.Windows.Forms.Button();

            // ── Onglet 3 : Objectifs captures ────────────────────────────────
            this.dgvObjectifs         = new System.Windows.Forms.DataGridView();
            this.btnValiderObjectifs  = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.nudNbMembres)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBudget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjectifDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectifs)).BeginInit();
            this.SuspendLayout();

            // tabControl
            this.tabControl.Location = new System.Drawing.Point(10, 10);
            this.tabControl.Size = new System.Drawing.Size(960, 580);
            this.tabControl.Controls.Add(this.tabMission);
            this.tabControl.Controls.Add(this.tabEquipage);
            this.tabControl.Controls.Add(this.tabObjectifs);
            this.tabEquipage.Enabled = false;
            this.tabObjectifs.Enabled = false;

            // tabMission
            this.tabMission.Text = "1 — Mission";
            int ly = 15, lx = 20, vx = 200, lw = 160, vw = 300, lh = 23, gap = 10;
            this.lblPlanete.Location = new System.Drawing.Point(lx, ly); this.lblPlanete.Size = new System.Drawing.Size(lw, lh); this.lblPlanete.Text = "Planète de destination :";
            this.cboPlanete.Location = new System.Drawing.Point(vx, ly); this.cboPlanete.Size = new System.Drawing.Size(vw, lh); this.cboPlanete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ly += lh + gap;
            this.lblChef.Location = new System.Drawing.Point(lx, ly); this.lblChef.Size = new System.Drawing.Size(lw, lh); this.lblChef.Text = "Chef de mission :";
            this.cboChef.Location = new System.Drawing.Point(vx, ly); this.cboChef.Size = new System.Drawing.Size(vw, lh); this.cboChef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ly += lh + gap;
            this.lblDateDepart.Location = new System.Drawing.Point(lx, ly); this.lblDateDepart.Size = new System.Drawing.Size(lw, lh); this.lblDateDepart.Text = "Date de départ :";
            this.dtpDateDepart.Location = new System.Drawing.Point(vx, ly); this.dtpDateDepart.Size = new System.Drawing.Size(vw, lh);
            ly += lh + gap;
            this.lblDateRetour.Location = new System.Drawing.Point(lx, ly); this.lblDateRetour.Size = new System.Drawing.Size(lw, lh); this.lblDateRetour.Text = "Date de retour prévue :";
            this.dtpDateRetour.Location = new System.Drawing.Point(vx, ly); this.dtpDateRetour.Size = new System.Drawing.Size(vw, lh);
            ly += lh + gap;
            this.lblNbMembres.Location = new System.Drawing.Point(lx, ly); this.lblNbMembres.Size = new System.Drawing.Size(lw, lh); this.lblNbMembres.Text = "Nb membres requis :";
            this.nudNbMembres.Location = new System.Drawing.Point(vx, ly); this.nudNbMembres.Size = new System.Drawing.Size(100, lh); this.nudNbMembres.Minimum = 1; this.nudNbMembres.Maximum = 50;
            ly += lh + gap;
            this.lblBudget.Location = new System.Drawing.Point(lx, ly); this.lblBudget.Size = new System.Drawing.Size(lw, lh); this.lblBudget.Text = "Budget alloué ($ gal.) :";
            this.nudBudget.Location = new System.Drawing.Point(vx, ly); this.nudBudget.Size = new System.Drawing.Size(150, lh); this.nudBudget.Maximum = 9999999; this.nudBudget.ThousandsSeparator = true;
            ly += lh + gap;
            this.lblObjectifDB.Location = new System.Drawing.Point(lx, ly); this.lblObjectifDB.Size = new System.Drawing.Size(lw, lh); this.lblObjectifDB.Text = "Objectif DataBaz (kg) :";
            this.nudObjectifDB.Location = new System.Drawing.Point(vx, ly); this.nudObjectifDB.Size = new System.Drawing.Size(150, lh); this.nudObjectifDB.Maximum = 99999;
            ly += lh + gap;
            this.lblFeuille.Location = new System.Drawing.Point(lx, ly); this.lblFeuille.Size = new System.Drawing.Size(lw, lh); this.lblFeuille.Text = "Feuille de route :";
            this.txtFeuille.Location = new System.Drawing.Point(vx, ly); this.txtFeuille.Size = new System.Drawing.Size(vw, 80); this.txtFeuille.Multiline = true;
            ly += 80 + gap;
            this.btnCreerMission.Location = new System.Drawing.Point(vx, ly); this.btnCreerMission.Size = new System.Drawing.Size(160, 35); this.btnCreerMission.Text = "Créer la mission →"; this.btnCreerMission.Click += new System.EventHandler(this.btnCreerMission_Click);
            this.tabMission.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblPlanete, this.cboPlanete, this.lblChef, this.cboChef, this.lblDateDepart, this.dtpDateDepart, this.lblDateRetour, this.dtpDateRetour, this.lblNbMembres, this.nudNbMembres, this.lblBudget, this.nudBudget, this.lblObjectifDB, this.nudObjectifDB, this.lblFeuille, this.txtFeuille, this.btnCreerMission });

            // tabEquipage
            this.tabEquipage.Text = "2 — Équipage";
            this.lstMembresDisponibles.Location = new System.Drawing.Point(20, 20); this.lstMembresDisponibles.Size = new System.Drawing.Size(360, 440); this.lstMembresDisponibles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.btnAjouterMembre.Location = new System.Drawing.Point(395, 180); this.btnAjouterMembre.Size = new System.Drawing.Size(80, 35); this.btnAjouterMembre.Text = "Ajouter →"; this.btnAjouterMembre.Click += new System.EventHandler(this.btnAjouterMembre_Click);
            this.btnRetirerMembre.Location = new System.Drawing.Point(395, 230); this.btnRetirerMembre.Size = new System.Drawing.Size(80, 35); this.btnRetirerMembre.Text = "← Retirer"; this.btnRetirerMembre.Click += new System.EventHandler(this.btnRetirerMembre_Click);
            this.lstMembresChoisis.Location = new System.Drawing.Point(490, 20); this.lstMembresChoisis.Size = new System.Drawing.Size(360, 440); this.lstMembresChoisis.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.btnValiderEquipage.Location = new System.Drawing.Point(490, 470); this.btnValiderEquipage.Size = new System.Drawing.Size(160, 35); this.btnValiderEquipage.Text = "Valider l'équipage →"; this.btnValiderEquipage.Click += new System.EventHandler(this.btnValiderEquipage_Click);
            this.tabEquipage.Controls.AddRange(new System.Windows.Forms.Control[] { this.lstMembresDisponibles, this.btnAjouterMembre, this.btnRetirerMembre, this.lstMembresChoisis, this.btnValiderEquipage });

            // tabObjectifs
            this.tabObjectifs.Text = "3 — Objectifs captures";
            this.dgvObjectifs.Location = new System.Drawing.Point(20, 20); this.dgvObjectifs.Size = new System.Drawing.Size(900, 460);
            this.btnValiderObjectifs.Location = new System.Drawing.Point(750, 490); this.btnValiderObjectifs.Size = new System.Drawing.Size(170, 35); this.btnValiderObjectifs.Text = "Valider (transaction)"; this.btnValiderObjectifs.Click += new System.EventHandler(this.btnValiderObjectifs_Click);
            this.tabObjectifs.Controls.AddRange(new System.Windows.Forms.Control[] { this.dgvObjectifs, this.btnValiderObjectifs });

            // FormNouvelleMission
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 620);
            this.Controls.Add(this.tabControl);
            this.Name = "FormNouvelleMission";
            this.Text = "Stargate — Nouvelle mission";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.nudNbMembres)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBudget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjectifDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectifs)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMission;
        private System.Windows.Forms.TabPage tabEquipage;
        private System.Windows.Forms.TabPage tabObjectifs;
        private System.Windows.Forms.Label lblPlanete;
        private System.Windows.Forms.ComboBox cboPlanete;
        private System.Windows.Forms.Label lblChef;
        private System.Windows.Forms.ComboBox cboChef;
        private System.Windows.Forms.Label lblDateDepart;
        private System.Windows.Forms.DateTimePicker dtpDateDepart;
        private System.Windows.Forms.Label lblDateRetour;
        private System.Windows.Forms.DateTimePicker dtpDateRetour;
        private System.Windows.Forms.Label lblNbMembres;
        private System.Windows.Forms.NumericUpDown nudNbMembres;
        private System.Windows.Forms.Label lblBudget;
        private System.Windows.Forms.NumericUpDown nudBudget;
        private System.Windows.Forms.Label lblFeuille;
        private System.Windows.Forms.TextBox txtFeuille;
        private System.Windows.Forms.Label lblObjectifDB;
        private System.Windows.Forms.NumericUpDown nudObjectifDB;
        private System.Windows.Forms.Button btnCreerMission;
        private System.Windows.Forms.ListBox lstMembresDisponibles;
        private System.Windows.Forms.ListBox lstMembresChoisis;
        private System.Windows.Forms.Button btnAjouterMembre;
        private System.Windows.Forms.Button btnRetirerMembre;
        private System.Windows.Forms.Button btnValiderEquipage;
        private System.Windows.Forms.DataGridView dgvObjectifs;
        private System.Windows.Forms.Button btnValiderObjectifs;
    }
}

