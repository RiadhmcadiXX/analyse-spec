using System.Globalization;
using Application.Helpers;
using Application.Interfaces.Services.Members;
using Domain.Common;
using Domain.Entities.StudentCours;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;

namespace Application.Services.csvAnalyse;

public class CsvAnalyseService : ICsvAnalyseService
{
    public async Task<SucceededOrNotResponse> AnalyserCsvAsync(IFormFile file, CancellationToken ct)
    {
        try
        {
            // ✅ Détection automatique du type de fichier : .csv ou .xlsx
            TextReader reader = file.FileName.EndsWith(".xlsx")
                ? new StringReader(ExcelToCsvConverter.ConvertExcelToCsv(file.OpenReadStream()))
                : new StreamReader(file.OpenReadStream());

            using var parser = new TextFieldParser(reader)
            {
                TextFieldType = FieldType.Delimited
            };
            parser.SetDelimiters(",");
            parser.HasFieldsEnclosedInQuotes = true;

            var coursList = new List<StudentCours>();
            bool firstLine = true;

            while (!parser.EndOfData)
            {
                var parts = parser.ReadFields();
                if (firstLine) { firstLine = false; continue; }

                if (parts.Length < 12)
                {
                    // 🔍 Log ligne incomplète si besoin
                    Array.Resize(ref parts, 12);
                    for (int i = 0; i < parts.Length; i++)
                        if (parts[i] == null) parts[i] = "";
                }

                var cours = new StudentCours
                {
                    AnneeSession = parts[0],
                    CodeCours = parts[1],
                    TitreCours = parts[2],
                    ServiceEnseignement = parts[4],
                    Note = double.TryParse(parts[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var note) ? note : 0,
                    Moyenne = double.TryParse(parts[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var moy) ? moy : 0,
                    Remarque = string.IsNullOrWhiteSpace(parts[7]) ? "Vide" : parts[7],
                    NbUnites = double.TryParse(parts[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var unites) ? unites : 0,
                    Categorie = string.IsNullOrWhiteSpace(parts[9]) ? "Pas de catégorie" : parts[9],
                    Programme = parts[11],
                };

                coursList.Add(cours);
            }
            
            var codesStandards = new[] { "601", "340", "109", "604" };


            // ✅ Regroupement logique par catégorie
            var cours601 = coursList.Where(c => c.CodeCours.StartsWith("601") && c.Note >= 60).ToList();
            var cours340 = coursList.Where(c => c.CodeCours.StartsWith("340") && c.Note >= 60).ToList();
            var cours109 = coursList.Where(c => c.CodeCours.StartsWith("109") && c.Note >= 60).ToList();
            var cours604 = coursList.Where(c => c.CodeCours.StartsWith("604") && c.Note >= 60).ToList();
            
            var coursND = coursList
                .Where(c =>
                    !codesStandards.Any(prefix => c.CodeCours.StartsWith(prefix)) &&
                    c.Note >= 60)
                .ToList();

            var coursNonReussis = coursList.Where(c => c.Note < 60).ToList();

            bool reussi =
                cours601.Count >= 4 &&
                cours340.Count >= 3 &&
                cours109.Count >= 3 &&
                cours604.Count >= 2;

            var message = reussi
                ? " L'étudiant a complété les exigences minimales. ✅"
                : " L'étudiant n’a pas encore complété le programme. ❌";

            return new SucceededOrNotResponse(true, null, new
            {
                reussi,
                message,
                coursNonReussis = coursNonReussis.Select(c => new
                {
                    code = c.CodeCours,
                    titre = c.TitreCours,
                    note = c.Note
                }),
                coursReussisParCategorie = new
                {
                    litterature601 = cours601.Select(c => new { code = c.CodeCours, titre = c.TitreCours, note = c.Note }),
                    philosophie340 = cours340.Select(c => new { code = c.CodeCours, titre = c.TitreCours, note = c.Note }),
                    educationPhysique109 = cours109.Select(c => new { code = c.CodeCours, titre = c.TitreCours, note = c.Note }),
                    anglais604 = cours604.Select(c => new { code = c.CodeCours, titre = c.TitreCours, note = c.Note }),
                    coursND = coursND.Select(c => new { code = c.CodeCours, titre = c.TitreCours, note = c.Note })
                }
            });
        }

        catch (Exception ex)
        {
            return new SucceededOrNotResponse(false, new[]
            {
                new Error("CsvError", ex.Message)
            });
        }
    }
}