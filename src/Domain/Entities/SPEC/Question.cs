using Domain.Common;

namespace Domain.Entities.SPEC;

public class Question: AuditableAndSoftDeletableEntity, ISanitizable
{
    public string Title { get; set; } = default!;
    public string? DescriptionQuestion { get; set; }
    public Question? Parent { get; set; } = default!;
    
    public void SanitizeForSaving()
    {
        Title = Title.Trim();
        DescriptionQuestion = DescriptionQuestion?.Trim();  
    }
}