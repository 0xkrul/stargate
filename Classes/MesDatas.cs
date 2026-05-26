using System.Data;
using System.Data.SQLite;
using System.Text;

namespace appliPandora
{
    public class MesDatas
    {
        private static DataSet dsGlobal = new DataSet();

        public static DataSet DsGlobal { get { return MesDatas.dsGlobal; } }

        public static void RechargerTable(string nomTable)
        {
            if (string.IsNullOrWhiteSpace(nomTable))
                throw new ArgumentException("Le nom de table est obligatoire.", nameof(nomTable));

            if (!nomTable.All(c => char.IsLetterOrDigit(c) || c == '_'))
                throw new ArgumentException("Nom de table invalide.", nameof(nomTable));

            if (DsGlobal.Tables.Contains(nomTable))
                DsGlobal.Tables.Remove(nomTable);

            using SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                $"SELECT * FROM \"{nomTable}\"",
                Connexion.Connec);
            DataTable table = new DataTable(nomTable);
            adapter.Fill(table);
            DsGlobal.Tables.Add(table);
        }

        public static DataTable ExecuterRequete(string sql, params SQLiteParameter[] parameters)
        {
            using SQLiteCommand command = new SQLiteCommand(sql, Connexion.Connec);
            if (parameters.Length > 0)
                command.Parameters.AddRange(parameters);

            using SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable result = new DataTable();
            adapter.Fill(result);
            return result;
        }

        public static string EchapperFiltreLike(string value)
        {
            StringBuilder escaped = new StringBuilder(value.Length);
            foreach (char c in value)
            {
                if (c is '*' or '%' or '[' or ']')
                    escaped.Append('[').Append(c).Append(']');
                else if (c == '\'')
                    escaped.Append("''");
                else
                    escaped.Append(c);
            }

            return escaped.ToString();
        }

        public static bool EstDisponible(
            string matricule,
            DateTime newStart,
            DateTime newEnd,
            out string missionInfo,
            string ignorePlanete = "",
            int ignoreNumero = -1)
        {
            missionInfo = string.Empty;
            if (!DsGlobal.Tables.Contains("Mission"))
                return true;

            DataTable missions = DsGlobal.Tables["Mission"]!;
            DataTable? composer = DsGlobal.Tables.Contains("Composer")
                ? DsGlobal.Tables["Composer"]
                : null;

            foreach (DataRow mission in missions.Rows)
            {
                string planete = mission["nomPlanete"]?.ToString() ?? string.Empty;
                int numero = Convert.ToInt32(mission["numero"]);

                if (planete == ignorePlanete && numero == ignoreNumero)
                    continue;

                if (!DateTime.TryParse(mission["dateDepart"]?.ToString(), out DateTime start) ||
                    !DateTime.TryParse(mission["dateRetour"]?.ToString(), out DateTime end))
                    continue;

                bool datesOverlap = newStart.Date <= end.Date && start.Date <= newEnd.Date;
                if (!datesOverlap)
                    continue;

                bool isChef = string.Equals(
                    mission["matriculeChef"]?.ToString(),
                    matricule,
                    StringComparison.OrdinalIgnoreCase);

                bool isCrew = false;
                if (composer != null)
                {
                    string safePlanete = planete.Replace("'", "''");
                    string safeMatricule = matricule.Replace("'", "''");
                    isCrew = composer.Select(
                        $"nomPlanete = '{safePlanete}' AND numeroMission = {numero} AND matriculeMembre = '{safeMatricule}'")
                        .Length > 0;
                }

                if (isChef || isCrew)
                {
                    missionInfo = $"{planete} #{numero} ({start:yyyy-MM-dd} -> {end:yyyy-MM-dd})";
                    return false;
                }
            }

            return true;
        }

        public static DataTable ObtenirChefsDisponibles(
            DateTime start,
            DateTime end,
            string ignorePlanete = "",
            int ignoreNumero = -1)
        {
            DataTable result = new DataTable();
            result.Columns.Add("matricule", typeof(string));
            result.Columns.Add("affichage", typeof(string));

            if (!DsGlobal.Tables.Contains("Membre") || !DsGlobal.Tables.Contains("Militaire"))
                return result;

            foreach (DataRow militaire in DsGlobal.Tables["Militaire"]!.Rows)
            {
                string matricule = militaire["matriculeMembre"]?.ToString() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(matricule))
                    continue;

                if (!EstDisponible(matricule, start, end, out _, ignorePlanete, ignoreNumero))
                    continue;

                string safeMatricule = matricule.Replace("'", "''");
                DataRow[] membres = DsGlobal.Tables["Membre"]!.Select($"matricule = '{safeMatricule}'");
                if (membres.Length == 0)
                    continue;

                result.Rows.Add(
                    matricule,
                    $"{membres[0]["nom"]} {membres[0]["prenom"]} ({militaire["grade"]}) - {matricule}");
            }

            return result;
        }
    }
}
