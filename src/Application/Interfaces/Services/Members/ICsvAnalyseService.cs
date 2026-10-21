using Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services.Members;

public interface ICsvAnalyseService
{
    /// <summary>
    /// Analyse un fichier CSV d’un étudiant et retourne un résultat avec les cours, la réussite et les erreurs.
    /// </summary>
    /// <param name="file">Le fichier CSV envoyé via formulaire</param>
    /// <param name="ct">Token d’annulation</param>
    /// <returns>SucceededOrNotResponse contenant les données analysées</returns>
    Task<SucceededOrNotResponse> AnalyserCsvAsync(IFormFile file, CancellationToken ct);
}