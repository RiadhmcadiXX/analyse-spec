using Domain.Common;
using Domain.Helpers;
using Microsoft.AspNetCore.Identity;
using NodaTime;


namespace Domain.Entities.Identity;

public class User : IdentityUser<Guid>, ISoftDeletable
{
    public Instant Created { get; private set; }
    public string? CreatedBy { get; private set; }
    
    public bool IsFirstConnection { get; private set; } = true;

    public Instant? LastModified { get; private set; }
    public string? LastModifiedBy { get; private set; }

    public Instant? Deleted { get; private set; }
    public string? DeletedBy { get; private set; }
    
    public void SetFirstConnectionFalse()
    {
        IsFirstConnection = false;
    }

    public Instant? LastTwoFactorAuthentication { get; private set; }

    public int? PhoneExtension { get; set; }

    public List<UserRole> UserRoles { get; private set; } = [];
    public List<Programme> Programmes { get; set; } = [];
    
    
    public List<string> RoleNames => UserRoles.Select(x => x.Role.Name!).ToList();
    public List<string> ProgrammeNames => Programmes.Select(x => x.Name).ToList();

    public string? GetPhoneNumberWithExtension() =>
        PhoneNumberHelper.AddExtensionToPhoneNumber(PhoneNumber, PhoneExtension);
    public bool HasRole(string role) => RoleNames.Contains(role);
    public bool HasProgramme(string programme) => ProgrammeNames.Contains(programme);
    public void ClearRoles() => UserRoles.Clear();
    public void ClearProgrammes() => Programmes.Clear();
    

    public void AddRole(Role role)
    {
        if (UserRoles.Any(x => x.Role.Id == role.Id))
            return;
        UserRoles.Add(new UserRole { User = this, Role = role });
    }

    public void AddProgramme(Programme programme)
    {
        if (Programmes.Any(x => x.Name == programme.Name))
            return;
        Programmes.Add(new Programme { Name = programme.Name, Code = programme.Code  });
    }

    public bool IsActive() => Deleted == null && DeletedBy == null;
    public void SetPhoneExtension(int? phoneExtension) => PhoneExtension = phoneExtension;
    public void SetDeletedBy(string deletedBy) => DeletedBy = deletedBy;
    public void UpdateCreated(Instant created, string createdBy)
    {
        Created = created;
        CreatedBy = createdBy;
    }

    public void UpdateLastModified(Instant lastModified, string lastModifiedBy)
    {
        LastModified = lastModified;
        LastModifiedBy = lastModifiedBy;
    }

    public void UpdateLastTwoFactorAuthentication()
    {
        LastTwoFactorAuthentication = InstantHelper.GetLocalNow();
    }

    public void ClearLastTwoFactorAuthentication()
    {
        LastTwoFactorAuthentication = null;
    }

    public bool LastTwoFactorAuthenticationWasLessThanGivenNumberOfDaysAgo(int days)
    {
        return LastTwoFactorAuthentication.HasValue &&
               (DateTime.Now - LastTwoFactorAuthentication.Value.ToDateTimeUtc()).TotalDays < days;
    }

    public void Activate(string firstName)
    {
        Restore();
    }

    public void SoftDelete(string? deletedBy = null)
    {
        Deleted = InstantHelper.GetLocalNow();
        DeletedBy = deletedBy;
    }

    public void Restore()
    {
        Deleted = null;
        DeletedBy = null;
    }
}