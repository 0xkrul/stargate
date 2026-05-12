using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;

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

        private static void AjouterSectionEquipage(Document doc, string nomPlanete, int numero)
        {
            doc.Add(new Paragraph("Équipage", PoliceSousTitle));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));

            PdfPTable table = CreerTableauVide("Matricule", "Nom", "Prénom", "Type", "Chef ?");

            if (!MesDatas.DsGlobal.Tables.Contains("Composer") ||
                !MesDatas.DsGlobal.Tables.Contains("Membre"))
            { doc.Add(table); doc.Add(new Paragraph(" ")); return; }

            DataRow? mission = TrouverMission(nomPlanete, numero);
            string chefMat   = mission?["matriculeChef"]?.ToString() ?? "";

            DataRow[] composeurs = MesDatas.DsGlobal.Tables["Composer"]!
                .Select($"nomPlanete = '{nomPlanete}' AND numeroMission = {numero}");

            int i = 0;
            foreach (DataRow c in composeurs)
            {
                string mat = c["matriculeMembre"].ToString()!;
                DataRow[] mb = MesDatas.DsGlobal.Tables["Membre"]!.Select($"matricule = '{mat}'");
                if (mb.Length == 0) continue;

                bool estMil = MesDatas.DsGlobal.Tables.Contains("Militaire") &&
                    MesDatas.DsGlobal.Tables["Militaire"]!
                        .Select($"matriculeMembre = '{mat}'").Length > 0;

                AjouterLigneTableau(table, i++,
                    mat,
                    mb[0]["nom"].ToString()!,
                    mb[0]["prenom"].ToString()!,
                    estMil ? "Militaire" : "Civil",
                    mat == chefMat ? "★ Chef" : "");
            }

            doc.Add(table);
            doc.Add(new Paragraph(" "));
        }

        private static void AjouterSectionCaptures(Document doc, string nomPlanete, int numero)
        {
            doc.Add(new Paragraph("Bilan des captures", PoliceSousTitle));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));

            PdfPTable table = CreerTableauVide("Espèce", "Objectif", "Captures réalisées", "Taux de réussite");

            // Utiliser la table locale BilanCapture si elle est déjà construite
            string nomBilan = $"Bilan_{nomPlanete}_{numero}";
            if (MesDatas.DsGlobal.Tables.Contains(nomBilan))
            {
                int i = 0;
                foreach (DataRow row in MesDatas.DsGlobal.Tables[nomBilan]!.Rows)
                    AjouterLigneTableau(table, i++,
                        row[0].ToString()!, row[1].ToString()!,
                        row[2].ToString()!, row[3].ToString()!);
            }
            else if (MesDatas.DsGlobal.Tables.Contains("ObjectifCapture"))
            {
                DataRow[] objectifs = MesDatas.DsGlobal.Tables["ObjectifCapture"]!
                    .Select($"nomPlanete = '{nomPlanete}' AND numeroMission = {numero}");
                int i = 0;
                foreach (DataRow obj in objectifs)
                {
                    int idEspece = Convert.ToInt32(obj["idEspeceEnnemi"]);
                    int objectif = Convert.ToInt32(obj["objectif"]);
                    int captures = 0;
                    if (MesDatas.DsGlobal.Tables.Contains("Capturer"))
                    {
                        DataRow[] cap = MesDatas.DsGlobal.Tables["Capturer"]!
                            .Select($"nomPlanete='{nomPlanete}' AND numeroMission={numero} AND idEspeceEnnemi={idEspece}");
                        if (cap.Length > 0) captures = Convert.ToInt32(cap[0]["nombre"]);
                    }
                    string nomEspece = idEspece.ToString();
                    if (MesDatas.DsGlobal.Tables.Contains("Espece"))
                    {
                        DataRow[] esp = MesDatas.DsGlobal.Tables["Espece"]!.Select($"id = {idEspece}");
                        if (esp.Length > 0) nomEspece = esp[0]["nom"].ToString()!;
                    }
                    double taux = objectif > 0 ? Math.Round((double)captures / objectif * 100, 1) : 0;
                    AjouterLigneTableau(table, i++,
                        nomEspece, objectif.ToString(), captures.ToString(), $"{taux} %");
                }
            }

            doc.Add(table);
            doc.Add(new Paragraph(" "));
        }

        private static void AjouterSectionDepenses(Document doc, string nomPlanete, int numero)
        {
            doc.Add(new Paragraph("Dépenses effectuées", PoliceSousTitle));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));

            PdfPTable table = CreerTableauVide("Date", "Type", "Motif", "Montant ($ gal.)");

            if (MesDatas.DsGlobal.Tables.Contains("Depense"))
            {
                DataRow[] depenses = MesDatas.DsGlobal.Tables["Depense"]!
                    .Select($"nomPlanete = '{nomPlanete}' AND numeroMission = {numero}", "dateD ASC");

                int i = 0, total = 0;
                foreach (DataRow d in depenses)
                {
                    string typeLib = d["idTypeDepense"].ToString()!;
                    if (MesDatas.DsGlobal.Tables.Contains("TypeDepense"))
                    {
                        DataRow[] td = MesDatas.DsGlobal.Tables["TypeDepense"]!
                            .Select($"id = {d["idTypeDepense"]}");
                        if (td.Length > 0) typeLib = td[0]["libelle"].ToString()!;
                    }
                    int montant = Convert.ToInt32(d["montant"]);
                    total += montant;
                    AjouterLigneTableau(table, i++,
                        d["dateD"].ToString()!, typeLib,
                        d["motif"].ToString()!, $"{montant:N0}");
                }

                // Ligne total
                BaseColor bgTotal = new BaseColor(240, 240, 240);
                PdfPCell[] totalCells =
                {
                    new PdfPCell(new Phrase("", PoliceNormal))        { BackgroundColor = bgTotal },
                    new PdfPCell(new Phrase("", PoliceNormal))        { BackgroundColor = bgTotal },
                    new PdfPCell(new Phrase("TOTAL", PoliceBold))     { BackgroundColor = bgTotal, HorizontalAlignment = Element.ALIGN_RIGHT },
                    new PdfPCell(new Phrase($"{total:N0} $ gal.", PoliceBold)) { BackgroundColor = bgTotal }
                };
                foreach (var tc in totalCells) table.AddCell(tc);
            }

            doc.Add(table);
            doc.Add(new Paragraph(" "));
        }

        private static void AjouterSectionContacts(Document doc, string nomPlanete, int numero)
        {
            doc.Add(new Paragraph("Contacts avec informateurs", PoliceSousTitle));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));

            PdfPTable table = CreerTableauVide("Date", "Nom de code", "Espèce", "Somme versée", "Appréciation");

            if (MesDatas.DsGlobal.Tables.Contains("Contact"))
            {
                DataRow[] contacts = MesDatas.DsGlobal.Tables["Contact"]!
                    .Select($"nomPlanete = '{nomPlanete}' AND numeroMission = {numero}", "dateC ASC");

                int i = 0;
                foreach (DataRow c in contacts)
                {
                    string nomCode = c["nomCodeInformateur"].ToString()!;
                    string nomEsp  = "";
                    if (MesDatas.DsGlobal.Tables.Contains("Informateur"))
                    {
                        DataRow[] inf = MesDatas.DsGlobal.Tables["Informateur"]!
                            .Select($"nomCode = '{nomCode}'");
                        if (inf.Length > 0)
                        {
                            string idEsp = inf[0]["idEspeceEnnemi"].ToString()!;
                            if (MesDatas.DsGlobal.Tables.Contains("Espece"))
                            {
                                DataRow[] esp = MesDatas.DsGlobal.Tables["Espece"]!
                                    .Select($"id = {idEsp}");
                                if (esp.Length > 0) nomEsp = esp[0]["nom"].ToString()!;
                            }
                        }
                    }
                    AjouterLigneTableau(table, i++,
                        c["dateC"].ToString()!, nomCode, nomEsp,
                        $"{c["sommeVersee"]} $ gal.",
                        c["appreciation"]?.ToString() ?? "");
                }
            }

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
