using Application.Exceptions.Members;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Books;
using Domain.Entities.Identity;
using Domain.Entities.SPEC;
using Domain.Entities.Students;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Drawing;

namespace Infrastructure.Repositories.Answers;

public class AnswerRepository : IAnswerRepository
{
    private readonly AnalyseSpecDbContext _context;

    public AnswerRepository(AnalyseSpecDbContext context)
    {
        _context = context;
    }

    public List<Answer> GetAnswersByStudentId(Guid id)
    {
        return _context.Answers
            .AsNoTracking()
            .Where(answer => answer.IdStudent.Id.ToString().ToUpper() == id.ToString().ToUpper())
            .Include(a => (a as Answer).IdOption)
            .Include(a => (a as Answer).IdStudent)
            .ToList();
    }

    public List<Answer> GetAllAnswers()
    {
        return _context.Answers
            .AsNoTracking()
            .Include(a => (a as Answer).IdOption)
            .Include(a => (a as Answer).IdStudent)
            .ToList();
    }

    public async Task CreateAnswer(Answer answer)
    {
        _context.Answers.Add(answer);
        await _context.SaveChangesAsync(); 
    }

    public async Task CreateBulkAnswers(List<Answer> answers)
    {
        foreach (var answer in answers)
        {
            if(!_context.Answers.Any(a => a.IdOption == answer.IdOption && a.IdStudent == answer.IdStudent))
                _context.Answers.Add(answer);
        }
        await _context.SaveChangesAsync();
    }
}