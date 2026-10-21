using Domain.Common;

namespace Domain.Entities.SPEC;

public class Answer: AuditableAndSoftDeletableEntity
{
    public Option IdOption { get; set; } = default!;
    public Students.Student IdStudent { get; set; } = default!;
}