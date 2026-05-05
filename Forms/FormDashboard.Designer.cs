namespace appliPandora.Forms
{
    partial class FormDashboard
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
            this.dgvMissions = new System.Windows.Forms.DataGridView();
            this.btnNouvelleMission = new System.Windows.Forms.Button();
            this.btnVoirMission = new System.Windows.Forms.Button();
            this.btnGenererPdf = new System.Windows.Forms.Button();
            this.btnRaces = new System.Windows.Forms.Button();
            this.btnPlanetes = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.lblTitre = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissions)).BeginInit();
            this.SuspendLayout();

            // lblTitre
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitre.Location = new System.Drawing.Point(20, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Text = "🌌 Stargate — Tableau de bord des missions";

            // dgvMissions
            this.dgvMissions.AllowUserToAddRows = false;
            this.dgvMissions.ReadOnly = true;
            this.dgvMissions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMissions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvMissions.Location = new System.Drawing.Point(20, 60);
            this.dgvMissions.Name = "dgvMissions";
            this.dgvMissions.Size = new System.Drawing.Size(1150, 560);

            // Boutons (panneau latéral à droite)
            int bx = 1190, by = 60, bw = 160, bh = 40, gap = 15;

            this.btnVoirMission.Location = new System.Drawing.Point(bx, by);
            this.btnVoirMission.Size = new System.Drawing.Size(bw, bh);
            this.btnVoirMission.Text = "Voir la mission";
            this.btnVoirMission.Click += new System.EventHandler(this.btnVoirMission_Click);

            this.btnNouvelleMission.Location = new System.Drawing.Point(bx, by += bh + gap);
            this.btnNouvelleMission.Size = new System.Drawing.Size(bw, bh);
            this.btnNouvelleMission.Text = "Nouvelle mission";
            this.btnNouvelleMission.Click += new System.EventHandler(this.btnNouvelleMission_Click);

            this.btnGenererPdf.Location = new System.Drawing.Point(bx, by += bh + gap);
            this.btnGenererPdf.Size = new System.Drawing.Size(bw, bh);
            this.btnGenererPdf.Text = "Générer PDF";
            this.btnGenererPdf.Click += new System.EventHandler(this.btnGenererPdf_Click);

            this.btnRaces.Location = new System.Drawing.Point(bx, by += bh + gap);
            this.btnRaces.Size = new System.Drawing.Size(bw, bh);
            this.btnRaces.Text = "Races";
            this.btnRaces.Click += new System.EventHandler(this.btnRaces_Click);

            this.btnPlanetes.Location = new System.Drawing.Point(bx, by += bh + gap);
            this.btnPlanetes.Size = new System.Drawing.Size(bw, bh);
            this.btnPlanetes.Text = "Planètes";
            this.btnPlanetes.Click += new System.EventHandler(this.btnPlanetes_Click);

            this.btnStats.Location = new System.Drawing.Point(bx, by += bh + gap);
            this.btnStats.Size = new System.Drawing.Size(bw, bh);
            this.btnStats.Text = "Statistiques";
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);

            // FormDashboard
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 650);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.dgvMissions);
            this.Controls.Add(this.btnVoirMission);
            this.Controls.Add(this.btnNouvelleMission);
            this.Controls.Add(this.btnGenererPdf);
            this.Controls.Add(this.btnRaces);
            this.Controls.Add(this.btnPlanetes);
            this.Controls.Add(this.btnStats);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "FormDashboard";
            this.Text = "Stargate — Tableau de bord";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMissions;
        private System.Windows.Forms.Button btnNouvelleMission;
        private System.Windows.Forms.Button btnVoirMission;
        private System.Windows.Forms.Button btnGenererPdf;
        private System.Windows.Forms.Button btnRaces;
        private System.Windows.Forms.Button btnPlanetes;
        private System.Windows.Forms.Button btnStats;
        private System.Windows.Forms.Label lblTitre;
    }
}
