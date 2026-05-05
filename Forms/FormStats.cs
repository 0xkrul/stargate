using System.Data;
using System.Data.SQLite;

namespace appliPandora.Forms
{
    /// <summary>
    /// Volet 7 — Données statistiques.
    /// Cinq requêtes SQL exécutées à la demande, résultats affichés dans un DataGridView.
    /// </summary>
    public partial class FormStats : Form
    {
        public FormStats()
        {
            InitializeComponent();
            this.Load += FormStats_Load;
        }

        private void FormStats_Load(object sender, EventArgs e)
        {
            // Remplir le ComboBox de membres (stats 1)
            // TODO: lier cboMembre depuis MesDatas.DsGlobal["Membre"]

            // Remplir le ComboBox de missions (stats 5)
            // TODO: lier cboMission depuis MesDatas.DsGlobal["Mission"]
        }

        // ─── Stat 1 ───────────────────────────────────────────────────────────
        /// <summary>
        /// Liste des personnes avec qui le membre sélectionné est parti en mission.
        /// </summary>
        private void btnStat1_Click(object sender, EventArgs e)
        {
            if (cboMembre.SelectedValue == null) return;
            string matricule = cboMembre.SelectedValue.ToString()!;

            string sql = @"
                SELECT DISTINCT m.matricule, m.nom, m.prenom,
                       CASE WHEN mil.matriculeMembre IS NOT NULL THEN 'Militaire' ELSE 'Civil' END AS type
                FROM Membre m
                LEFT JOIN Militaire mil ON m.matricule = mil.matriculeMembre
                WHERE m.matricule != @mat
                  AND m.matricule IN (
                      SELECT c2.matriculeMembre
                      FROM Composer c1
                      JOIN Composer c2 ON c1.nomPlanete = c2.nomPlanete AND c1.numeroMission = c2.numeroMission
                      WHERE c1.matriculeMembre = @mat
                  )
                ORDER BY m.nom, m.prenom";

            ExecuterRequete(sql, new SQLiteParameter("@mat", matricule));
        }

        // ─── Stat 2 ───────────────────────────────────────────────────────────
        /// <summary>
        /// Pour les missions avec équipage > 10 : liste des dépenses + budgets.
        /// </summary>
        private void btnStat2_Click(object sender, EventArgs e)
        {
            string sql = @"
                SELECT m.nomPlanete, m.numero,
                       m.budget AS budgetInitial,
                       m.budget - COALESCE(SUM(d.montant), 0) AS budgetActuel,
                       d.dateD, d.motif, d.montant, td.libelle AS typeDepense
                FROM Mission m
                LEFT JOIN Depense d ON m.nomPlanete = d.nomPlanete AND m.numero = d.numeroMission
                LEFT JOIN TypeDepense td ON d.idTypeDepense = td.id
                WHERE (SELECT COUNT(*) FROM Composer c WHERE c.nomPlanete = m.nomPlanete AND c.numeroMission = m.numero) > 10
                GROUP BY m.nomPlanete, m.numero, d.id
                ORDER BY m.nomPlanete, m.numero";

            ExecuterRequete(sql);
        }

        // ─── Stat 3 ───────────────────────────────────────────────────────────
        /// <summary>
        /// Nombre de missions par planète (y compris planètes sans mission).
        /// </summary>
        private void btnStat3_Click(object sender, EventArgs e)
        {
            string sql = @"
                SELECT p.nom AS planete, COUNT(m.numero) AS nbMissions
                FROM Planete p
                LEFT JOIN Mission m ON p.nom = m.nomPlanete
                GROUP BY p.nom
                ORDER BY nbMissions DESC, p.nom";

            ExecuterRequete(sql);
        }

        // ─── Stat 4 ───────────────────────────────────────────────────────────
        /// <summary>
        /// Dépenses les plus élevées de chaque mission + chef de mission.
        /// </summary>
        private void btnStat4_Click(object sender, EventArgs e)
        {
            string sql = @"
                SELECT d.dateD || ' — ' || d.motif || ' — ' || d.montant || ' $ gal.' AS [Dépenses les plus importantes],
                       m.nomPlanete || ' #' || m.numero AS [Mission],
                       mb.nom || ' ' || mb.prenom AS [Chef de mission]
                FROM Depense d
                JOIN Mission m ON d.nomPlanete = m.nomPlanete AND d.numeroMission = m.numero
                JOIN Membre mb ON m.matriculeChef = mb.matricule
                WHERE d.montant = (
                    SELECT MAX(d2.montant)
                    FROM Depense d2
                    WHERE d2.nomPlanete = d.nomPlanete AND d2.numeroMission = d.numeroMission
                )
                ORDER BY m.nomPlanete, m.numero";

            ExecuterRequete(sql);
        }

        // ─── Stat 5 ───────────────────────────────────────────────────────────
        /// <summary>
        /// Informateurs ayant reçu le moins d'argent sur une mission donnée.
        /// </summary>
        private void btnStat5_Click(object sender, EventArgs e)
        {
            if (cboMission.SelectedItem == null) return;

            // La mission est identifiée par nomPlanete + numero
            // TODO: extraire nomPlanete et numero depuis cboMission
            string nomPlanete = ""; // TODO
            int numero = 0;          // TODO

            string sql = @"
                SELECT i.nomCode, e.nom AS especeOrigine, SUM(c.sommeVersee) AS totalRecu
                FROM Contact c
                JOIN Informateur i ON c.nomCodeInformateur = i.nomCode
                JOIN Espece e ON i.idEspeceEnnemi = e.id
                WHERE c.nomPlanete = @planet AND c.numeroMission = @num
                GROUP BY i.nomCode
                HAVING SUM(c.sommeVersee) = (
                    SELECT MIN(sTotal)
                    FROM (
                        SELECT SUM(c2.sommeVersee) AS sTotal
                        FROM Contact c2
                        WHERE c2.nomPlanete = @planet AND c2.numeroMission = @num
                        GROUP BY c2.nomCodeInformateur
                    )
                )
                ORDER BY i.nomCode";

            ExecuterRequete(sql,
                new SQLiteParameter("@planet", nomPlanete),
                new SQLiteParameter("@num",    numero));
        }

        // ─── Exécution générique d'une requête ───────────────────────────────
        private void ExecuterRequete(string sql, params SQLiteParameter[] parametres)
        {
            try
            {
                using SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                cmd.Parameters.AddRange(parametres);
                using SQLiteDataAdapter ada = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                dgvResultats.DataSource = dt;
                lblNbResultats.Text = $"{dt.Rows.Count} résultat(s)";
                Connexion.FermerConnexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la requête :\n{ex.Message}", "Erreur SQL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
