using Domain.Common;

namespace Domain.Entities.SPEC;

public class Option: AuditableAndSoftDeletableEntity, ISanitizable
{
    public Question IdQuestion { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? DescriptionOption { get; set; }
    
    public void SanitizeForSaving()
    {
        Title = Title?.Trim();
        DescriptionOption = DescriptionOption?.Trim();
    }
}