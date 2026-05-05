namespace appliPandora.Forms
{
    partial class FormPlanetes
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
            this.dgvPlanetes   = new System.Windows.Forms.DataGridView();
            this.grpDetails    = new System.Windows.Forms.GroupBox();
            this.lblNomPlanete = new System.Windows.Forms.Label();
            this.lblTemp       = new System.Windows.Forms.Label();
            this.lblGravite    = new System.Windows.Forms.Label();
            this.lblDataBaz    = new System.Windows.Forms.Label();
            this.grpEspeces    = new System.Windows.Forms.GroupBox();
            this.dgvEspeces    = new System.Windows.Forms.DataGridView();
            this.grpMissions   = new System.Windows.Forms.GroupBox();
            this.dgvMissions   = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanetes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspeces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissions)).BeginInit();
            this.SuspendLayout();

            // dgvPlanetes (liste gauche)
            this.dgvPlanetes.Location = new System.Drawing.Point(10, 10);
            this.dgvPlanetes.Size = new System.Drawing.Size(320, 620);
            this.dgvPlanetes.AllowUserToAddRows = false;
            this.dgvPlanetes.ReadOnly = true;
            this.dgvPlanetes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlanetes.SelectionChanged += new System.EventHandler(this.dgvPlanetes_SelectionChanged);

            // grpDetails
            this.grpDetails.Location = new System.Drawing.Point(345, 10);
            this.grpDetails.Size = new System.Drawing.Size(800, 130);
            this.grpDetails.Text = "Informations sur la planète";
            this.lblNomPlanete.Location = new System.Drawing.Point(10, 25); this.lblNomPlanete.Size = new System.Drawing.Size(780, 25); this.lblNomPlanete.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.lblNomPlanete.Text = "—";
            this.lblTemp.Location    = new System.Drawing.Point(10, 58); this.lblTemp.Size = new System.Drawing.Size(380, 23); this.lblTemp.Text = "Température : —";
            this.lblGravite.Location = new System.Drawing.Point(10, 83); this.lblGravite.Size = new System.Drawing.Size(380, 23); this.lblGravite.Text = "Gravité : —";
            this.lblDataBaz.Location = new System.Drawing.Point(400, 58); this.lblDataBaz.Size = new System.Drawing.Size(380, 23); this.lblDataBaz.Text = "DataBaz présent : —";
            this.grpDetails.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblNomPlanete, this.lblTemp, this.lblGravite, this.lblDataBaz });

            // grpEspeces
            this.grpEspeces.Location = new System.Drawing.Point(345, 155);
            this.grpEspeces.Size = new System.Drawing.Size(800, 240);
            this.grpEspeces.Text = "Espèces présentes sur cette planète";
            this.dgvEspeces.Location = new System.Drawing.Point(10, 22);
            this.dgvEspeces.Size = new System.Drawing.Size(780, 205);
            this.dgvEspeces.AllowUserToAddRows = false;
            this.dgvEspeces.ReadOnly = true;
            this.grpEspeces.Controls.Add(this.dgvEspeces);

            // grpMissions
            this.grpMissions.Location = new System.Drawing.Point(345, 410);
            this.grpMissions.Size = new System.Drawing.Size(800, 220);
            this.grpMissions.Text = "Missions effectuées sur cette planète";
            this.dgvMissions.Location = new System.Drawing.Point(10, 22);
            this.dgvMissions.Size = new System.Drawing.Size(780, 185);
            this.dgvMissions.AllowUserToAddRows = false;
            this.dgvMissions.ReadOnly = true;
            this.grpMissions.Controls.Add(this.dgvMissions);

            // FormPlanetes
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 645);
            this.Controls.Add(this.dgvPlanetes);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.grpEspeces);
            this.Controls.Add(this.grpMissions);
            this.Name = "FormPlanetes";
            this.Text = "Stargate — Planètes de la galaxie";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanetes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspeces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissions)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPlanetes;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblNomPlanete;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Label lblGravite;
        private System.Windows.Forms.Label lblDataBaz;
        private System.Windows.Forms.GroupBox grpEspeces;
        private System.Windows.Forms.DataGridView dgvEspeces;
        private System.Windows.Forms.GroupBox grpMissions;
        private System.Windows.Forms.DataGridView dgvMissions;
    }
}
