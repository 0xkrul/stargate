namespace appliPandora.Forms
{
    partial class FormStats
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
            this.grpRequetes    = new System.Windows.Forms.GroupBox();
            this.lblMembre      = new System.Windows.Forms.Label();
            this.cboMembre      = new System.Windows.Forms.ComboBox();
            this.btnStat1       = new System.Windows.Forms.Button();
            this.btnStat2       = new System.Windows.Forms.Button();
            this.btnStat3       = new System.Windows.Forms.Button();
            this.btnStat4       = new System.Windows.Forms.Button();
            this.lblMission     = new System.Windows.Forms.Label();
            this.cboMission     = new System.Windows.Forms.ComboBox();
            this.btnStat5       = new System.Windows.Forms.Button();
            this.dgvResultats   = new System.Windows.Forms.DataGridView();
            this.lblNbResultats = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultats)).BeginInit();
            this.SuspendLayout();

            // grpRequetes
            this.grpRequetes.Location = new System.Drawing.Point(10, 10);
            this.grpRequetes.Size = new System.Drawing.Size(1140, 190);
            this.grpRequetes.Text = "Requêtes statistiques";

            int bw = 1090, bh = 28, bx = 15, by = 20, gap = 8;

            // Stat 1
            this.lblMembre.Location = new System.Drawing.Point(bx, by + 4); this.lblMembre.Size = new System.Drawing.Size(120, 23); this.lblMembre.Text = "⭐ Stat 1 — Membre :";
            this.cboMembre.Location = new System.Drawing.Point(bx + 130, by); this.cboMembre.Size = new System.Drawing.Size(250, 23); this.cboMembre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.btnStat1.Location = new System.Drawing.Point(bx + 395, by); this.btnStat1.Size = new System.Drawing.Size(680, bh); this.btnStat1.Text = "Lister les co-équipiers de ce membre"; this.btnStat1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnStat1.Click += new System.EventHandler(this.btnStat1_Click);
            by += bh + gap;

            // Stat 2
            this.btnStat2.Location = new System.Drawing.Point(bx, by); this.btnStat2.Size = new System.Drawing.Size(bw, bh); this.btnStat2.Text = "⭐ Stat 2 — Missions avec équipage > 10 : dépenses + budgets"; this.btnStat2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnStat2.Click += new System.EventHandler(this.btnStat2_Click);
            by += bh + gap;

            // Stat 3
            this.btnStat3.Location = new System.Drawing.Point(bx, by); this.btnStat3.Size = new System.Drawing.Size(bw, bh); this.btnStat3.Text = "⭐ Stat 3 — Nombre de missions par planète (y compris 0)"; this.btnStat3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnStat3.Click += new System.EventHandler(this.btnStat3_Click);
            by += bh + gap;

            // Stat 4
            this.btnStat4.Location = new System.Drawing.Point(bx, by); this.btnStat4.Size = new System.Drawing.Size(bw, bh); this.btnStat4.Text = "⭐ Stat 4 — Dépenses les plus élevées de chaque mission + chef"; this.btnStat4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnStat4.Click += new System.EventHandler(this.btnStat4_Click);
            by += bh + gap;

            // Stat 5
            this.lblMission.Location = new System.Drawing.Point(bx, by + 4); this.lblMission.Size = new System.Drawing.Size(130, 23); this.lblMission.Text = "⭐ Stat 5 — Mission :";
            this.cboMission.Location = new System.Drawing.Point(bx + 140, by); this.cboMission.Size = new System.Drawing.Size(250, 23); this.cboMission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.btnStat5.Location = new System.Drawing.Point(bx + 405, by); this.btnStat5.Size = new System.Drawing.Size(670, bh); this.btnStat5.Text = "Informateurs ayant reçu le moins d'argent"; this.btnStat5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnStat5.Click += new System.EventHandler(this.btnStat5_Click);

            this.grpRequetes.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblMembre, this.cboMembre, this.btnStat1, this.btnStat2, this.btnStat3, this.btnStat4, this.lblMission, this.cboMission, this.btnStat5 });

            // dgvResultats
            this.dgvResultats.Location = new System.Drawing.Point(10, 215);
            this.dgvResultats.Size = new System.Drawing.Size(1140, 420);
            this.dgvResultats.AllowUserToAddRows = false;
            this.dgvResultats.ReadOnly = true;

            // lblNbResultats
            this.lblNbResultats.Location = new System.Drawing.Point(10, 645);
            this.lblNbResultats.Size = new System.Drawing.Size(400, 23);
            this.lblNbResultats.Text = "";

            // FormStats
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 680);
            this.Controls.Add(this.grpRequetes);
            this.Controls.Add(this.dgvResultats);
            this.Controls.Add(this.lblNbResultats);
            this.Name = "FormStats";
            this.Text = "Stargate — Statistiques";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultats)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpRequetes;
        private System.Windows.Forms.Label lblMembre;
        private System.Windows.Forms.ComboBox cboMembre;
        private System.Windows.Forms.Button btnStat1;
        private System.Windows.Forms.Button btnStat2;
        private System.Windows.Forms.Button btnStat3;
        private System.Windows.Forms.Button btnStat4;
        private System.Windows.Forms.Label lblMission;
        private System.Windows.Forms.ComboBox cboMission;
        private System.Windows.Forms.Button btnStat5;
        private System.Windows.Forms.DataGridView dgvResultats;
        private System.Windows.Forms.Label lblNbResultats;
    }
}
