using Domain.Entities.Identity;
using Domain.Entities.Students;
using Microsoft.AspNetCore.Identity;

namespace Domain.Repositories;

public interface IStudentRepository
{
    List<Student> GetAllStudents();
    Student FindStudentById(Guid id);
    Task CreateStudent(Student student);
    Task CreateBulkStudents(List<Student> students);
}