using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace appliPandora.Classes
{
    /// <summary>
    /// Génère les PDF de bilan de mission à partir des données du DataSet local.
    /// Aucune connexion à la base de données n'est requise ici.
    /// </summary>
    public static class PdfGenerator
    {
        // ─── Couleurs & polices ───────────────────────────────────────────────
        private static readonly BaseColor CouleurEntete  = new BaseColor(30, 60, 114);   // bleu nuit
        private static readonly BaseColor CouleurLigne1  = new BaseColor(220, 230, 245); // bleu clair
        private static readonly BaseColor CouleurLigne2  = BaseColor.WHITE;
        private static readonly Font      PoliceTitle    = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.WHITE);
        private static readonly Font      PoliceSousTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, CouleurEntete);
        private static readonly Font      PoliceNormal   = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
        private static readonly Font      PoliceBold     = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK);

        /// <summary>
        /// Génère le PDF de compte-rendu d'une mission et l'ouvre après création.
        /// </summary>
        /// <param name="nomPlanete">Nom de la planète de destination</param>
        /// <param name="numero">Numéro de la mission</param>
        /// <param name="cheminFichier">Chemin du fichier PDF à créer</param>
        public static void GenererBilanMission(string nomPlanete, int numero, string cheminFichier)
        {
            Document doc = new Document(PageSize.A4, 40, 40, 60, 40);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(cheminFichier, FileMode.Create));
            doc.Open();

            // ── En-tête ───────────────────────────────────────────────────────
            AjouterEntete(doc, nomPlanete, numero);

            // ── Informations générales de la mission ──────────────────────────
            AjouterSectionInfosMission(doc, nomPlanete, numero);

            // ── Équipage ──────────────────────────────────────────────────────
            AjouterSectionEquipage(doc, nomPlanete, numero);

            // ── Bilan des captures ────────────────────────────────────────────
            AjouterSectionCaptures(doc, nomPlanete, numero);

            // ── Dépenses effectuées ───────────────────────────────────────────
            AjouterSectionDepenses(doc, nomPlanete, numero);

            // ── Contacts avec informateurs ────────────────────────────────────
            AjouterSectionContacts(doc, nomPlanete, numero);

            doc.Close();

            // Ouvrir le fichier PDF avec l'application par défaut du système
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = cheminFichier,
                UseShellExecute = true
            });
        }

        // ─── En-tête avec titre coloré ────────────────────────────────────────
        private static void AjouterEntete(Document doc, string nomPlanete, int numero)
        {
            PdfPTable header = new PdfPTable(1) { WidthPercentage = 100 };
            PdfPCell cell = new PdfPCell(new Phrase($"PROJET STARGATE — BILAN DE MISSION\n{nomPlanete} #{numero}", PoliceTitle))
            {
                BackgroundColor = CouleurEntete,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 12
            };
            header.AddCell(cell);
            doc.Add(header);
            doc.Add(new Paragraph(" "));
        }

        // ─── Section : infos générales ────────────────────────────────────────
        private static void AjouterSectionInfosMission(Document doc, string nomPlanete, int numero)
        {
            doc.Add(new Paragraph("Informations générales", PoliceSousTitle));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));

            // TODO: lire depuis MesDatas.DsGlobal["Mission"] + Membre (chef)
            // Ajouter : Planète, Numéro, Dates, Chef, Budget initial, Budget restant,
            //           Nb membres, Feuille de route, Objectif DataBaz
            DataRow? mission = TrouverMission(nomPlanete, numero);
            if (mission != null)
            {
                AjouterLigneInfo(doc, "Planète de destination", nomPlanete);
                AjouterLigneInfo(doc, "Numéro de mission", numero.ToString());
                AjouterLigneInfo(doc, "Date de départ", mission["dateDepart"].ToString()!);
                AjouterLigneInfo(doc, "Date de retour prévue", mission["dateRetour"].ToString()!);
                AjouterLigneInfo(doc, "Budget initial", $"{mission["budget"]} $ gal.");
                AjouterLigneInfo(doc, "Feuille de route", mission["feuilleDeRoute"].ToString()!);
            }
            doc.Add(new Paragraph(" "));
        }

        // ─── Section : équipage ───────────────────────────────────────────────
        private static void AjouterSectionEquipage(Document doc, string nomPlanete, int numero)
        {
            doc.Add(new Paragraph("Équipage", PoliceSousTitle));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));

            // TODO: construire un tableau iTextSharp avec les colonnes :
            //       Matricule | Nom | Prénom | Type (Militaire/Civil) | Chef ?
            PdfPTable table = CreerTableauVide("Matricule", "Nom", "Prénom", "Type", "Chef ?");

            // TODO: parcourir Composer JOIN Membre JOIN Militaire/Civil filtrés sur la mission
            // Exemple de ligne :
            // AjouterLigneTableau(table, "MCD-413", "ALGLAVE", "—", "Militaire", "✓", i);

            doc.Add(table);
            doc.Add(new Paragraph(" "));
        }

        // ─── Section : captures ───────────────────────────────────────────────
        private static void AjouterSectionCaptures(Document doc, string nomPlanete, int numero)
        {
            doc.Add(new Paragraph("Bilan des captures", PoliceSousTitle));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));

            PdfPTable table = CreerTableauVide("Espèce", "Objectif", "Captures réalisées", "Taux de réussite");

            // TODO: lire la table locale BilanCapture_<nomPlanete>_<numero> du DataSet
            //       ou recalculer depuis ObjectifCapture + Capturer

            doc.Add(table);
            doc.Add(new Paragraph(" "));
        }

        // ─── Section : dépenses ───────────────────────────────────────────────
        private static void AjouterSectionDepenses(Document doc, string nomPlanete, int numero)
        {
            doc.Add(new Paragraph("Dépenses effectuées", PoliceSousTitle));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));

            PdfPTable table = CreerTableauVide("Date", "Type", "Motif", "Montant ($ gal.)");

            // TODO: parcourir MesDatas.DsGlobal["Depense"] filtrée sur la mission

            doc.Add(table);
            doc.Add(new Paragraph(" "));
        }

        // ─── Section : contacts ───────────────────────────────────────────────
        private static void AjouterSectionContacts(Document doc, string nomPlanete, int numero)
        {
            doc.Add(new Paragraph("Contacts avec informateurs", PoliceSousTitle));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));

            PdfPTable table = CreerTableauVide("Date", "Nom de code", "Espèce", "Somme versée", "Appréciation");

            // TODO: parcourir MesDatas.DsGlobal["Contact"] JOIN Informateur, filtré sur la mission

            doc.Add(table);
        }

        // ─── Helpers ──────────────────────────────────────────────────────────
        private static DataRow? TrouverMission(string nomPlanete, int numero)
        {
            DataTable? dt = MesDatas.DsGlobal.Tables["Mission"];
            if (dt == null) return null;
            DataRow[] rows = dt.Select($"nomPlanete = '{nomPlanete}' AND numero = {numero}");
            return rows.Length > 0 ? rows[0] : null;
        }

        private static void AjouterLigneInfo(Document doc, string libelle, string valeur)
        {
            Paragraph p = new Paragraph();
            p.Add(new Chunk($"{libelle} : ", PoliceBold));
            p.Add(new Chunk(valeur, PoliceNormal));
            doc.Add(p);
        }

        private static PdfPTable CreerTableauVide(params string[] colonnes)
        {
            PdfPTable table = new PdfPTable(colonnes.Length) { WidthPercentage = 100, SpacingBefore = 5 };
            foreach (string col in colonnes)
            {
                PdfPCell cell = new PdfPCell(new Phrase(col, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.WHITE)))
                {
                    BackgroundColor = CouleurEntete,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5
                };
                table.AddCell(cell);
            }
            return table;
        }

        private static void AjouterLigneTableau(PdfPTable table, int ligneIndex, params string[] valeurs)
        {
            BaseColor bg = ligneIndex % 2 == 0 ? CouleurLigne1 : CouleurLigne2;
            foreach (string val in valeurs)
            {
                PdfPCell cell = new PdfPCell(new Phrase(val, PoliceNormal))
                {
                    BackgroundColor = bg,
                    Padding = 4
                };
                table.AddCell(cell);
            }
        }
    }
}
