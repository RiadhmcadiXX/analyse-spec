using Domain.Entities.Students;

namespace Web.Features.Members.Students.GetAllStudents;

public class GetAllStudentsReponse
{
    public Guid Id { get; set; }
    public string FirstAndLastName { get; set; } = null!;
    public string Cegep { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public char Gender { get; set; }
}