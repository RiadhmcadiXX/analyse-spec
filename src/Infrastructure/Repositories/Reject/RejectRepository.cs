using Domain.Repositories;
using Persistence;

namespace Infrastructure.Repositories.Reject;

public class RejectRepository:IRejectRepository
{
    private readonly AnalyseSpecDbContext _context;
    
    public RejectRepository(AnalyseSpecDbContext context)
    {
        _context = context;
    }

    public async Task CreateReject(Domain.Entities.DataLogs.Reject reject)
    {
        _context.Rejects.Add(reject);
        await _context.SaveChangesAsync();
    }

    public async Task CreateBulkRejects(List<Domain.Entities.DataLogs.Reject> rejects)
    {
        foreach (var reject in rejects)
        {
            _context.Rejects.Add(reject);
        }
        await _context.SaveChangesAsync();
    }
}