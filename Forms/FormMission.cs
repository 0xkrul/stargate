using System.Data;
using System.Data.SQLite;

namespace appliPandora.Forms
{
    /// <summary>
    /// Volet 3 — Récapitulatif complet d'une mission sélectionnée.
    /// Mode déconnecté : toutes les données proviennent de MesDatas.DsGlobal.
    /// Permet aussi la saisie de nouvelles dépenses, événements et contacts
    /// tant que la mission n'est pas terminée (mode connecté momentané).
    /// </summary>
    public partial class FormMission : Form
    {
        // Clés primaires de la mission courante
        private readonly string _nomPlanete;
        private readonly int    _numero;

        public FormMission(string nomPlanete, int numero)
        {
            InitializeComponent();
            _nomPlanete = nomPlanete;
            _numero     = numero;
            this.Load  += FormMission_Load;
        }

        // ─── Chargement ───────────────────────────────────────────────────────
        private void FormMission_Load(object sender, EventArgs e)
        {
            AfficherInfosMission();
            AfficherEquipage();
            AfficherObjectifsCaptures();
            AfficherBudget();
            VerifierStatutMission(); // désactive les saisies si mission terminée
        }

        private void AfficherInfosMission()
        {
            // TODO: lire depuis MesDatas.DsGlobal["Mission"] avec filtre sur _nomPlanete / _numero
            // Remplir lblPlanete, lblNumero, lblDateDepart, lblDateRetour, txtFeuille, etc.
        }

        private void AfficherEquipage()
        {
            // TODO: jointure Composer + Membre + Militaire/Civil dans le DataSet
            // Afficher dans dgvEquipage avec colonne "Type" (Militaire / Civil)
            // Mettre en évidence le chef de mission
        }

        private void AfficherObjectifsCaptures()
        {
            // TODO: lire ObjectifCapture + Capturer pour cette mission
            // Construire la table locale BilanCapture_<nomPlanete>_<numero> dans DsGlobal
            // Structure : Espèce | Objectif | Capturés | Taux %
            // Afficher dans dgvCaptures
        }

        private void AfficherBudget()
        {
            // TODO: budget initial depuis Mission
            //       budget consommé = SUM(Depense.montant) pour cette mission
            //       budget restant = initial - consommé
        }

        private void VerifierStatutMission()
        {
            // TODO: si dateRetour < DateTime.Today, masquer/désactiver le groupe de saisie
        }

        // ─── Bouton : Accès au journal ────────────────────────────────────────
        private void btnJournal_Click(object sender, EventArgs e)
        {
            using FormJournal fj = new FormJournal(_nomPlanete, _numero);
            fj.ShowDialog();
        }

        // ─── Ajout d'une dépense (mode connecté momentané) ───────────────────
        private void btnAjouterDepense_Click(object sender, EventArgs e)
        {
            // TODO: valider les champs (date, montant, motif, type)
            // INSERT INTO Depense(...)
            // Rafraîchir la table Depense du DataSet et AfficherBudget()
        }

        // ─── Ajout d'un événement journal ────────────────────────────────────
        private void btnAjouterEvenement_Click(object sender, EventArgs e)
        {
            // TODO: valider (date, commentaire)
            // INSERT INTO JournalDeBord(...)
            // Rafraîchir la table JournalDeBord du DataSet
        }

        // ─── Ajout d'un contact informateur ──────────────────────────────────
        private void btnAjouterContact_Click(object sender, EventArgs e)
        {
            // TODO: valider (date, informateur sélectionné, somme, appréciation)
            // INSERT INTO Contact(...)
            // Rafraîchir la table Contact du DataSet
        }
    }
}
