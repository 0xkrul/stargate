using System.Data;

namespace appliPandora.Forms
{
    /// <summary>
    /// Volet 4 — Visualisation des événements du journal de bord d'une mission.
    /// Navigation (Précédent / Suivant) via liaison de données (BindingSource) uniquement.
    /// Aucun accès à la base de données ici : tout vient de MesDatas.DsGlobal.
    /// </summary>
    public partial class FormJournal : Form
    {
        private readonly string _nomPlanete;
        private readonly int    _numero;

        // BindingSource pour la liaison de données (navigation sans requêtes SQL)
        private readonly BindingSource _bsJournal  = new BindingSource();
        private readonly BindingSource _bsDepenses  = new BindingSource();
        private readonly BindingSource _bsContacts  = new BindingSource();

        public FormJournal(string nomPlanete, int numero)
        {
            InitializeComponent();
            _nomPlanete = nomPlanete;
            _numero     = numero;
            this.Load  += FormJournal_Load;
        }

        private void FormJournal_Load(object? sender, EventArgs e)
        {
            LierDonnees();
        }

        /// <summary>
        /// Filtre les tables du DataSet global et lie les BindingSources aux contrôles.
        /// Aucun appel SQL — mode déconnecté pur.
        /// </summary>
        private void LierDonnees()
        {
            // ── Journal de bord ───────────────────────────────────────────────
            DataTable dtJournal = MesDatas.DsGlobal.Tables["JournalDeBord"]!;
            DataView dvJournal  = new DataView(dtJournal);
            dvJournal.RowFilter  = $"nomPlanete = '{_nomPlanete}' AND numero = {_numero}";
            dvJournal.Sort       = "dateJ ASC";
            _bsJournal.DataSource = dvJournal;

            // Lier les contrôles du journal
            txtJournalDate.DataBindings.Add("Text", _bsJournal, "dateJ");
            txtJournalCommentaire.DataBindings.Add("Text", _bsJournal, "commentaires");

            // ── Dépenses ──────────────────────────────────────────────────────
            DataTable dtDep = MesDatas.DsGlobal.Tables["Depense"]!;
            DataView dvDep  = new DataView(dtDep);
            dvDep.RowFilter  = $"nomPlanete = '{_nomPlanete}' AND numeroMission = {_numero}";
            dvDep.Sort       = "dateD ASC";
            _bsDepenses.DataSource = dvDep;
            dgvDepenses.DataSource = _bsDepenses;

            // ── Contacts ──────────────────────────────────────────────────────
            DataTable dtCtc = MesDatas.DsGlobal.Tables["Contact"]!;
            DataView dvCtc  = new DataView(dtCtc);
            dvCtc.RowFilter  = $"nomPlanete = '{_nomPlanete}' AND numeroMission = {_numero}";
            dvCtc.Sort       = "dateC ASC";
            _bsContacts.DataSource = dvCtc;
            dgvContacts.DataSource = _bsContacts;

            MettreAJourPosition();
        }

        // ─── Navigation (BindingSource — aucun accès SQL) ─────────────────────
        private void btnPremier_Click(object? sender, EventArgs e)
        {
            _bsJournal.MoveFirst();
            MettreAJourPosition();
        }

        private void btnPrecedent_Click(object? sender, EventArgs e)
        {
            _bsJournal.MovePrevious();
            MettreAJourPosition();
        }

        private void btnSuivant_Click(object? sender, EventArgs e)
        {
            _bsJournal.MoveNext();
            MettreAJourPosition();
        }

        private void btnDernier_Click(object? sender, EventArgs e)
        {
            _bsJournal.MoveLast();
            MettreAJourPosition();
        }

        private void MettreAJourPosition()
        {
            lblPosition.Text = _bsJournal.Count > 0
                ? $"Événement {_bsJournal.Position + 1} / {_bsJournal.Count}"
                : "Aucun événement";

            btnPremier.Enabled   = _bsJournal.Position > 0;
            btnPrecedent.Enabled = _bsJournal.Position > 0;
            btnSuivant.Enabled   = _bsJournal.Position < _bsJournal.Count - 1;
            btnDernier.Enabled   = _bsJournal.Position < _bsJournal.Count - 1;
        }
    }
}

