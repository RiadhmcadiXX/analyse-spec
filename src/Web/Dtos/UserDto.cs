namespace Web.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public int? PhoneExtension { get; set; }
    public List<string> Roles { get; set; } = default!;
    public List<string> Programmes { get; set; } = default!;
    
    public bool IsFirstConnection { get; set; }
}