using Domain.Common;

namespace Domain.Entities.DataLogs;

public class Reject: AuditableAndSoftDeletableEntity
{
    public string[] Data { get; set; }
    public string[] Exception { get; set; }
}