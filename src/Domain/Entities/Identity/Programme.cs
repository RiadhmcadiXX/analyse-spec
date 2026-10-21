using Microsoft.AspNetCore.Identity;
using Domain.Common;
namespace Domain.Entities.Identity;

public class Programme: AuditableAndSoftDeletableEntity, ISanitizable

{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;

    public Programme()
    {
        
    }

    public Programme(string name,string code)
    {
        Name = name;
        Code = code;
    }
    public List<User>? UserProgrammes { get; set; }
    
    public void SanitizeForSaving()
    {
        Name = Name.Trim();
        Code = Code.Trim();
    }
}