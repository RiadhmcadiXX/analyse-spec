using Application.Exceptions.Members;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.SPEC;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories.Questions;

public class QuestionRepository : IQuestionRepository
{
    private readonly AnalyseSpecDbContext _context;

    public QuestionRepository(AnalyseSpecDbContext context)
    {
        _context = context;
    }

    public Question GetQuestionById(Guid id)
    {
        return _context.Questions
            .AsNoTracking()
            .Include(q => q.Parent) 
            .FirstOrDefault(q => q.Id == id)
            ?? throw new Exception("Question not found.");
    }

    public List<Question> GetAllQuestions()
    {
        return _context.Questions.AsNoTracking().ToList();
    }

    public async Task CreateQuestion(Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
    }

    public async Task CreateBulkQuestions(List<Question> questions)
    {
        foreach (var question in questions)
        {
            if(!_context.Questions.Any(q => q.Title == question.Title))
                _context.Questions.Add(question);
        }
        await _context.SaveChangesAsync();
    }

    public List<Question> GetQuestionsByIds(List<Guid> questionsIds)
    {
        return _context.Questions
            .AsNoTracking()
            .Where(question => questionsIds.Contains(question.Id))
            .Include(a => (a as Question).Parent)
            .Include(a => (a as Question).Title)
            .ToList();
    }

    public async Task UpdateQuestions(List<Question> questions)
    {
        _context.Questions.UpdateRange(questions);
        await _context.SaveChangesAsync();
    }    
}