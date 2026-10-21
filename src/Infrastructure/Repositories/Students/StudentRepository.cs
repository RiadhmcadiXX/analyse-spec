using Application.Exceptions.Members;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Students;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories.Members;

public class StudentRepository : IStudentRepository
{
    private readonly AnalyseSpecDbContext _context;

    public StudentRepository(AnalyseSpecDbContext context)
    {
        _context = context;
    }

    public List<Student> GetAllStudents()
    {
        return _context.Students.AsNoTracking().ToList();
    }

    public Student FindStudentById(Guid id)
    {
        Student? student = _context.Students.AsNoTracking().FirstOrDefault(s => s.Id == id);
        if(student == null)
            throw new ApplicationException($"Student not found with this id: {id}");
        return student;
    }

    public async Task CreateStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

    public async Task CreateBulkStudents(List<Student> students)
    {
        foreach (var student in students)
        {
            if (!_context.Students.Any(s => s.Identification == student.Identification))
                _context.Students.Add(student);
        }
        await _context.SaveChangesAsync();
    }
}