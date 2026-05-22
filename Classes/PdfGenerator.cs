using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Font = iTextSharp.text.Font;

namespace appliPandora.Classes
{
    public static class PdfGenerator
    {
        private static readonly BaseColor CouleurEntete = new BaseColor(30, 60, 114);
        private static readonly BaseColor CouleurLigne1 = new BaseColor(220, 230, 245);
        private static readonly BaseColor CouleurLigne2 = BaseColor.WHITE;
        private static readonly Font PoliceTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.WHITE);
        private static readonly Font PoliceSousTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, CouleurEntete);
        private static readonly Font PoliceNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
        private static readonly Font PoliceBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK);

        public static void GenererBilanMission(string nomPlanete, int numero, string cheminFichier)
        {
            using FileStream flux = new FileStream(cheminFichier, FileMode.Create);
            Document doc = new Document(PageSize.A4, 40, 40, 60, 40);
            PdfWriter.GetInstance(doc, flux);
            doc.Open();

            AjouterEntete(doc, nomPlanete, numero);
            AjouterSectionInfosMission(doc, nomPlanete, numero);
            AjouterSectionEquipage(doc, nomPlanete, numero);
            AjouterSectionCaptures(doc, nomPlanete, numero);
            AjouterSectionNegociations(doc, nomPlanete, numero);
            AjouterSectionDepenses(doc, nomPlanete, numero);
            AjouterSectionContacts(doc, nomPlanete, numero);
            AjouterSectionJournal(doc, nomPlanete, numero);

            doc.Close();

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = cheminFichier,
                UseShellExecute = true
            });
        }

        public static string NomTableBilanCaptures(string nomPlanete, int numero)
        {
            return $"BilanCapture{nomPlanete}-{numero}";
        }

        public static DataTable ObtenirBilanCaptures(string nomPlanete, int numero)
        {
            string nomTable = NomTableBilanCaptures(nomPlanete, numero);
            if (MesDatas.DsGlobal.Tables.Contains(nomTable))
                MesDatas.DsGlobal.Tables.Remove(nomTable);

            DataTable bilan = new DataTable(nomTable);
            bilan.Columns.Add("Nom de l'espece", typeof(string));
            bilan.Columns.Add("Objectif initial", typeof(int));
            bilan.Columns.Add("Nombre de captures realisees", typeof(int));
            bilan.Columns.Add("Taux de reussite (en %)", typeof(string));
            MesDatas.DsGlobal.Tables.Add(bilan);

            DataTable? objectifs = MesDatas.DsGlobal.Tables["ObjectifCapture"];
            if (objectifs == null) return bilan;

            foreach (DataRow objectif in objectifs.Select(
                $"nomPlanete = '{Filtre(nomPlanete)}' AND numeroMission = {numero}"))
            {
                int idEspece = LireEntier(objectif["idEspeceEnnemi"]);
                int objectifInitial = LireEntier(objectif["objectif"]);
                int captures = NombreCaptures(nomPlanete, numero, idEspece);
                double taux = objectifInitial > 0
                    ? Math.Round((double)captures / objectifInitial * 100, 1)
                    : 0;

                bilan.Rows.Add(NomEspece(idEspece), objectifInitial, captures, $"{taux} %");
            }

            return bilan;
        }

        private static void AjouterEntete(Document doc, string nomPlanete, int numero)
        {
            PdfPTable header = new PdfPTable(1) { WidthPercentage = 100 };
            PdfPCell cell = new PdfPCell(new Phrase($"PROJET STARGATE - BILAN DE MISSION\n{nomPlanete} #{numero}", PoliceTitle))
            {
                BackgroundColor = CouleurEntete,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 12
            };
            header.AddCell(cell);
            doc.Add(header);
            doc.Add(new Paragraph(" "));
        }

        private static void AjouterSectionInfosMission(Document doc, string nomPlanete, int numero)
        {
            AjouterTitreSection(doc, "Informations generales");
            DataRow? mission = TrouverMission(nomPlanete, numero);
            if (mission == null)
            {
                doc.Add(new Paragraph("Mission introuvable dans le DataSet local.", PoliceNormal));
                return;
            }

            int budgetInitial = LireEntier(mission["budget"]);
            int budgetRestant = budgetInitial - TotalDepenses(nomPlanete, numero);

            AjouterLigneInfo(doc, "Planete de destination", nomPlanete);
            AjouterLigneInfo(doc, "Numero de mission", numero.ToString());
            AjouterLigneInfo(doc, "Date de depart", mission["dateDepart"].ToString() ?? "");
            AjouterLigneInfo(doc, "Date de retour prevue", mission["dateRetour"].ToString() ?? "");
            AjouterLigneInfo(doc, "Chef de mission", FormatterMembre(mission["matriculeChef"]?.ToString() ?? ""));
            AjouterLigneInfo(doc, "Membres requis", mission["nbMembreRequis"].ToString() ?? "");
            AjouterLigneInfo(doc, "Objectif DataBaz", $"{LireEntier(mission["objectifDatabaz"]):N0} kg");
            AjouterLigneInfo(doc, "Budget initial", $"{budgetInitial:N0} $ gal.");
            AjouterLigneInfo(doc, "Budget restant", $"{budgetRestant:N0} $ gal.");
            AjouterLigneInfo(doc, "Feuille de route", mission["feuilleDeRoute"].ToString() ?? "");
            doc.Add(new Paragraph(" "));
        }

        private static void AjouterSectionEquipage(Document doc, string nomPlanete, int numero)
        {
            AjouterTitreSection(doc, "Equipage");
            PdfPTable table = CreerTableau("Matricule", "Nom", "Prenom", "Type", "Chef ?");
            DataRow? mission = TrouverMission(nomPlanete, numero);
            string chef = mission?["matriculeChef"]?.ToString() ?? "";
            DataTable? composer = MesDatas.DsGlobal.Tables["Composer"];
            DataTable? membres = MesDatas.DsGlobal.Tables["Membre"];

            int i = 0;
            if (composer != null && membres != null)
            {
                foreach (DataRow row in composer.Select(
                    $"nomPlanete = '{Filtre(nomPlanete)}' AND numeroMission = {numero}"))
                {
                    string matricule = row["matriculeMembre"].ToString() ?? "";
                    DataRow? membre = TrouverLigne(membres, $"matricule = '{Filtre(matricule)}'");
                    AjouterLigneTableau(table, i++,
                        matricule,
                        membre?["nom"]?.ToString() ?? "",
                        membre?["prenom"]?.ToString() ?? "",
                        EstMilitaire(matricule) ? "Militaire" : "Civil",
                        matricule == chef ? "Oui" : "");
                }
            }

            AjouterTableOuMessage(doc, table, i, "Aucun membre d'equipage trouve.");
        }

        private static void AjouterSectionCaptures(Document doc, string nomPlanete, int numero)
        {
            AjouterTitreSection(doc, "Bilan des captures");
            PdfPTable table = CreerTableau("Espece", "Objectif", "Captures realisees", "Taux de reussite");

            int i = 0;
            foreach (DataRow row in ObtenirBilanCaptures(nomPlanete, numero).Rows)
            {
                AjouterLigneTableau(table, i++,
                    row[0].ToString() ?? "",
                    row[1].ToString() ?? "",
                    row[2].ToString() ?? "",
                    row[3].ToString() ?? "");
            }

            AjouterTableOuMessage(doc, table, i, "Aucun objectif de capture trouve.");
        }

        private static void AjouterSectionNegociations(Document doc, string nomPlanete, int numero)
        {
            AjouterTitreSection(doc, "Negociation DataBaz");
            PdfPTable table = CreerTableau("Espece alliee", "Quantite negociee (kg)");
            DataTable? negociations = MesDatas.DsGlobal.Tables["Negocier"];

            int i = 0;
            if (negociations != null)
            {
                foreach (DataRow negociation in negociations.Select(
                    $"nomPlanete = '{Filtre(nomPlanete)}' AND numeroMission = {numero}"))
                {
                    AjouterLigneTableau(table, i++,
                        NomEspece(LireEntier(negociation["idEspeceAllie"])),
                        $"{LireEntier(negociation["qteDataBaz"]):N0}");
                }
            }

            AjouterTableOuMessage(doc, table, i, "Aucune negociation DataBaz enregistree.");
        }

        private static void AjouterSectionDepenses(Document doc, string nomPlanete, int numero)
        {
            AjouterTitreSection(doc, "Depenses effectuees");
            PdfPTable table = CreerTableau("Date", "Type", "Motif", "Montant ($ gal.)");
            DataTable? depenses = MesDatas.DsGlobal.Tables["Depense"];

            int i = 0;
            if (depenses != null)
            {
                foreach (DataRow depense in depenses.Select(
                    $"nomPlanete = '{Filtre(nomPlanete)}' AND numeroMission = {numero}", "dateD ASC"))
                {
                    AjouterLigneTableau(table, i++,
                        depense["dateD"].ToString() ?? "",
                        LibelleTypeDepense(depense["idTypeDepense"]),
                        depense["motif"].ToString() ?? "",
                        $"{LireEntier(depense["montant"]):N0}");
                }
            }

            AjouterTableOuMessage(doc, table, i, "Aucune depense enregistree.");
        }

        private static void AjouterSectionContacts(Document doc, string nomPlanete, int numero)
        {
            AjouterTitreSection(doc, "Contacts avec informateurs");
            PdfPTable table = CreerTableau("Date", "Nom de code", "Espece", "Somme versee", "Appreciation");
            DataTable? contacts = MesDatas.DsGlobal.Tables["Contact"];

            int i = 0;
            if (contacts != null)
            {
                foreach (DataRow contact in contacts.Select(
                    $"nomPlanete = '{Filtre(nomPlanete)}' AND numeroMission = {numero}", "dateC ASC"))
                {
                    string nomCode = contact["nomCodeInformateur"].ToString() ?? "";
                    AjouterLigneTableau(table, i++,
                        contact["dateC"].ToString() ?? "",
                        nomCode,
                        EspeceInformateur(nomCode),
                        $"{LireEntier(contact["sommeVersee"]):N0}",
                        contact["appreciation"].ToString() ?? "");
                }
            }

            AjouterTableOuMessage(doc, table, i, "Aucun contact informateur enregistre.");
        }

        private static void AjouterSectionJournal(Document doc, string nomPlanete, int numero)
        {
            AjouterTitreSection(doc, "Chronologie du journal de bord");
            PdfPTable table = CreerTableau("Date", "Commentaires");
            DataTable? journal = MesDatas.DsGlobal.Tables["JournalDeBord"];

            int i = 0;
            if (journal != null)
            {
                foreach (DataRow evenement in journal.Select(
                    $"nomPlanete = '{Filtre(nomPlanete)}' AND numero = {numero}", "dateJ ASC"))
                {
                    AjouterLigneTableau(table, i++,
                        evenement["dateJ"].ToString() ?? "",
                        evenement["commentaires"].ToString() ?? "");
                }
            }

            AjouterTableOuMessage(doc, table, i, "Aucun evenement de journal enregistre.");
        }

        private static void AjouterTitreSection(Document doc, string titre)
        {
            doc.Add(new Paragraph(titre, PoliceSousTitle));
            doc.Add(new LineSeparator());
            doc.Add(new Paragraph(" "));
        }

        private static void AjouterLigneInfo(Document doc, string libelle, string valeur)
        {
            Paragraph p = new Paragraph();
            p.Add(new Chunk($"{libelle} : ", PoliceBold));
            p.Add(new Chunk(valeur, PoliceNormal));
            doc.Add(p);
        }

        private static PdfPTable CreerTableau(params string[] colonnes)
        {
            PdfPTable table = new PdfPTable(colonnes.Length) { WidthPercentage = 100, SpacingBefore = 5 };
            foreach (string col in colonnes)
            {
                table.AddCell(new PdfPCell(new Phrase(col, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.WHITE)))
                {
                    BackgroundColor = CouleurEntete,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5
                });
            }
            return table;
        }

        private static void AjouterLigneTableau(PdfPTable table, int ligneIndex, params string[] valeurs)
        {
            BaseColor bg = ligneIndex % 2 == 0 ? CouleurLigne1 : CouleurLigne2;
            foreach (string val in valeurs)
            {
                table.AddCell(new PdfPCell(new Phrase(val, PoliceNormal))
                {
                    BackgroundColor = bg,
                    Padding = 4
                });
            }
        }

        private static void AjouterTableOuMessage(Document doc, PdfPTable table, int nbLignes, string messageVide)
        {
            if (nbLignes == 0)
                doc.Add(new Paragraph(messageVide, PoliceNormal));
            else
                doc.Add(table);
            doc.Add(new Paragraph(" "));
        }

        private static DataRow? TrouverMission(string nomPlanete, int numero)
        {
            DataTable? dt = MesDatas.DsGlobal.Tables["Mission"];
            if (dt == null) return null;
            return TrouverLigne(dt, $"nomPlanete = '{Filtre(nomPlanete)}' AND numero = {numero}");
        }

        private static DataRow? TrouverLigne(DataTable table, string filtre)
        {
            DataRow[] rows = table.Select(filtre);
            return rows.Length > 0 ? rows[0] : null;
        }

        private static string Filtre(string valeur) => valeur.Replace("'", "''");

        private static int LireEntier(object? valeur)
        {
            if (valeur == null || valeur == DBNull.Value) return 0;
            return Convert.ToInt32(valeur);
        }

        private static bool EstMilitaire(string matricule)
        {
            DataTable? militaires = MesDatas.DsGlobal.Tables["Militaire"];
            return militaires != null &&
                militaires.Select($"matriculeMembre = '{Filtre(matricule)}'").Length > 0;
        }

        private static string FormatterMembre(string matricule)
        {
            DataTable? membres = MesDatas.DsGlobal.Tables["Membre"];
            if (membres == null) return matricule;

            DataRow? membre = TrouverLigne(membres, $"matricule = '{Filtre(matricule)}'");
            return membre == null
                ? matricule
                : $"{membre["nom"]} {membre["prenom"]} ({matricule})";
        }

        private static string NomEspece(int idEspece)
        {
            DataTable? especes = MesDatas.DsGlobal.Tables["Espece"];
            if (especes == null) return idEspece.ToString();

            DataRow? espece = TrouverLigne(especes, $"id = {idEspece}");
            return espece?["nom"]?.ToString() ?? idEspece.ToString();
        }

        private static int NombreCaptures(string nomPlanete, int numero, int idEspece)
        {
            DataTable? captures = MesDatas.DsGlobal.Tables["Capturer"];
            if (captures == null) return 0;

            DataRow? capture = TrouverLigne(captures,
                $"nomPlanete = '{Filtre(nomPlanete)}' AND numeroMission = {numero} AND idEspeceEnnemi = {idEspece}");
            return LireEntier(capture?["nombre"]);
        }

        private static int TotalDepenses(string nomPlanete, int numero)
        {
            DataTable? depenses = MesDatas.DsGlobal.Tables["Depense"];
            if (depenses == null) return 0;

            int total = 0;
            foreach (DataRow depense in depenses.Select(
                $"nomPlanete = '{Filtre(nomPlanete)}' AND numeroMission = {numero}"))
                total += LireEntier(depense["montant"]);
            return total;
        }

        private static string LibelleTypeDepense(object idTypeDepense)
        {
            int id = LireEntier(idTypeDepense);
            DataTable? types = MesDatas.DsGlobal.Tables["TypeDepense"];
            if (types == null) return id.ToString();

            DataRow? type = TrouverLigne(types, $"id = {id}");
            return type?["libelle"]?.ToString() ?? id.ToString();
        }

        private static string EspeceInformateur(string nomCode)
        {
            DataTable? informateurs = MesDatas.DsGlobal.Tables["Informateur"];
            if (informateurs == null) return "";

            DataRow? informateur = TrouverLigne(informateurs, $"nomCode = '{Filtre(nomCode)}'");
            return informateur == null ? "" : NomEspece(LireEntier(informateur["idEspeceEnnemi"]));
        }
    }
}
