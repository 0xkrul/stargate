namespace appliPandora.Forms
{
    partial class FormRaces
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
            this.grpFiltres        = new System.Windows.Forms.GroupBox();
            this.lblFiltreType     = new System.Windows.Forms.Label();
            this.cboFiltreType     = new System.Windows.Forms.ComboBox();
            this.lblFiltreCouleur  = new System.Windows.Forms.Label();
            this.cboFiltreCouleur  = new System.Windows.Forms.ComboBox();
            this.lblFiltrePlanete  = new System.Windows.Forms.Label();
            this.cboFiltrePlanete  = new System.Windows.Forms.ComboBox();
            this.btnFiltrer        = new System.Windows.Forms.Button();
            this.btnReset          = new System.Windows.Forms.Button();
            this.dgvRaces          = new System.Windows.Forms.DataGridView();
            this.grpDetails        = new System.Windows.Forms.GroupBox();
            this.lblDetNom         = new System.Windows.Forms.Label();
            this.lblDetCouleur     = new System.Windows.Forms.Label();
            this.lblDetType        = new System.Windows.Forms.Label();
            this.lblDetInfo1       = new System.Windows.Forms.Label();
            this.lblDetInfo2       = new System.Windows.Forms.Label();
            this.lblDetInfo3       = new System.Windows.Forms.Label();
            this.dgvPlanetes       = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanetes)).BeginInit();
            this.SuspendLayout();

            // grpFiltres
            this.grpFiltres.Location = new System.Drawing.Point(10, 10);
            this.grpFiltres.Size = new System.Drawing.Size(760, 65);
            this.grpFiltres.Text = "Filtres";
            this.lblFiltreType.Location = new System.Drawing.Point(10, 28); this.lblFiltreType.Size = new System.Drawing.Size(50, 23); this.lblFiltreType.Text = "Type :";
            this.cboFiltreType.Location = new System.Drawing.Point(65, 25); this.cboFiltreType.Size = new System.Drawing.Size(100, 23); this.cboFiltreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lblFiltreCouleur.Location = new System.Drawing.Point(180, 28); this.lblFiltreCouleur.Size = new System.Drawing.Size(60, 23); this.lblFiltreCouleur.Text = "Couleur :";
            this.cboFiltreCouleur.Location = new System.Drawing.Point(245, 25); this.cboFiltreCouleur.Size = new System.Drawing.Size(150, 23); this.cboFiltreCouleur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lblFiltrePlanete.Location = new System.Drawing.Point(410, 28); this.lblFiltrePlanete.Size = new System.Drawing.Size(60, 23); this.lblFiltrePlanete.Text = "Planète :";
            this.cboFiltrePlanete.Location = new System.Drawing.Point(475, 25); this.cboFiltrePlanete.Size = new System.Drawing.Size(150, 23); this.cboFiltrePlanete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.btnFiltrer.Location = new System.Drawing.Point(635, 22); this.btnFiltrer.Size = new System.Drawing.Size(60, 28); this.btnFiltrer.Text = "Filtrer"; this.btnFiltrer.Click += new System.EventHandler(this.btnFiltrer_Click);
            this.btnReset.Location = new System.Drawing.Point(700, 22); this.btnReset.Size = new System.Drawing.Size(50, 28); this.btnReset.Text = "Reset"; this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.grpFiltres.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblFiltreType, this.cboFiltreType, this.lblFiltreCouleur, this.cboFiltreCouleur, this.lblFiltrePlanete, this.cboFiltrePlanete, this.btnFiltrer, this.btnReset });

            // dgvRaces
            this.dgvRaces.Location = new System.Drawing.Point(10, 90);
            this.dgvRaces.Size = new System.Drawing.Size(760, 520);
            this.dgvRaces.AllowUserToAddRows = false;
            this.dgvRaces.ReadOnly = true;
            this.dgvRaces.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRaces.SelectionChanged += new System.EventHandler(this.dgvRaces_SelectionChanged);

            // grpDetails
            this.grpDetails.Location = new System.Drawing.Point(785, 10);
            this.grpDetails.Size = new System.Drawing.Size(370, 600);
            this.grpDetails.Text = "Détails de l'espèce";
            int dy = 20, dh = 25;
            this.lblDetNom.Location     = new System.Drawing.Point(10, dy);     this.lblDetNom.Size = new System.Drawing.Size(350, dh);     this.lblDetNom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.lblDetNom.Text = "—";
            this.lblDetCouleur.Location = new System.Drawing.Point(10, dy += dh + 5); this.lblDetCouleur.Size = new System.Drawing.Size(350, dh); this.lblDetCouleur.Text = "Couleur : —";
            this.lblDetType.Location    = new System.Drawing.Point(10, dy += dh + 5); this.lblDetType.Size = new System.Drawing.Size(350, dh); this.lblDetType.Text = "Type : —";
            this.lblDetInfo1.Location   = new System.Drawing.Point(10, dy += dh + 15); this.lblDetInfo1.Size = new System.Drawing.Size(350, dh); this.lblDetInfo1.Text = "—";
            this.lblDetInfo2.Location   = new System.Drawing.Point(10, dy += dh + 5);  this.lblDetInfo2.Size = new System.Drawing.Size(350, dh); this.lblDetInfo2.Text = "—";
            this.lblDetInfo3.Location   = new System.Drawing.Point(10, dy += dh + 5);  this.lblDetInfo3.Size = new System.Drawing.Size(350, dh); this.lblDetInfo3.Text = "—";
            var lblPlanetes = new System.Windows.Forms.Label() { Location = new System.Drawing.Point(10, dy += dh + 20), Size = new System.Drawing.Size(350, dh), Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold), Text = "Planètes habitées :" };
            this.dgvPlanetes.Location = new System.Drawing.Point(10, dy + dh + 5);
            this.dgvPlanetes.Size = new System.Drawing.Size(350, 280);
            this.dgvPlanetes.AllowUserToAddRows = false;
            this.dgvPlanetes.ReadOnly = true;
            this.grpDetails.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblDetNom, this.lblDetCouleur, this.lblDetType, this.lblDetInfo1, this.lblDetInfo2, this.lblDetInfo3, lblPlanetes, this.dgvPlanetes });

            // FormRaces
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 630);
            this.Controls.Add(this.grpFiltres);
            this.Controls.Add(this.dgvRaces);
            this.Controls.Add(this.grpDetails);
            this.Name = "FormRaces";
            this.Text = "Stargate — Races répertoriées";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanetes)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpFiltres;
        private System.Windows.Forms.Label lblFiltreType;
        private System.Windows.Forms.ComboBox cboFiltreType;
        private System.Windows.Forms.Label lblFiltreCouleur;
        private System.Windows.Forms.ComboBox cboFiltreCouleur;
        private System.Windows.Forms.Label lblFiltrePlanete;
        private System.Windows.Forms.ComboBox cboFiltrePlanete;
        private System.Windows.Forms.Button btnFiltrer;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridView dgvRaces;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblDetNom;
        private System.Windows.Forms.Label lblDetCouleur;
        private System.Windows.Forms.Label lblDetType;
        private System.Windows.Forms.Label lblDetInfo1;
        private System.Windows.Forms.Label lblDetInfo2;
        private System.Windows.Forms.Label lblDetInfo3;
        private System.Windows.Forms.DataGridView dgvPlanetes;
    }
}

