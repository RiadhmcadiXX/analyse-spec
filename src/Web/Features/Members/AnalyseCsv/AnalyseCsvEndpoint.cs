namespace Web.Features.Members.AnalyseCsv;

using Application.Interfaces.Services.Members;
using Domain.Common;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;


public class AnalyseCsvRequest
{
    public IFormFile File { get; set; }
}

public class AnalyseCsvEndpoint : Endpoint<AnalyseCsvRequest, SucceededOrNotResponse>
{
    private readonly ICsvAnalyseService _csvAnalyseService;

    public AnalyseCsvEndpoint(ICsvAnalyseService csvAnalyseService)
    {
        _csvAnalyseService = csvAnalyseService;
    }

    public override void Configure()
    {
        AllowFileUploads();
        DontCatchExceptions();
        Post("csv-analyse");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme); // ou commente temporairement pour test
    }

    public override async Task HandleAsync(AnalyseCsvRequest req, CancellationToken cancellation)
    {
        if (req.File == null || req.File.Length == 0)
        {
            await SendAsync(new SucceededOrNotResponse(false, new[] {
                new Error("FichierManquant", "Aucun fichier fourni.")
            }), 200,cancellation); // ✅ ici tu passes bien le CancellationToken
            return;
        }

        var result = await _csvAnalyseService.AnalyserCsvAsync(req.File, cancellation);
        await SendAsync(result, 200, cancellation); // ✅ parfait
    }
}
