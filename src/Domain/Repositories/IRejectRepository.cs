using Domain.Entities.DataLogs;

namespace Domain.Repositories;

public interface IRejectRepository
{
    Task CreateReject(Reject reject);
    Task CreateBulkRejects(List<Reject> rejects);
}