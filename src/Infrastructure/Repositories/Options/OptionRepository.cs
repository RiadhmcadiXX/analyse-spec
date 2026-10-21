using Application.Exceptions.Members;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.SPEC;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories.Options;

public class OptionRepository : IOptionRepository
{
    private readonly AnalyseSpecDbContext _context;

    public OptionRepository(AnalyseSpecDbContext context)
    {
        _context = context;
    }

    public List<Option> GetOptionsByIds(List<Guid> optionsIds)
    {
        return _context.Options
            .AsNoTracking()
            .Where(option => optionsIds.Contains(option.Id))
            .Include(a => (a as Option).IdQuestion)
            .Include(a => (a as Option).Title)
            .ToList();
    }

    public Option GetOptionById(Guid id)
    {
        return _context.Options
            .AsNoTracking()
            .Include(a => (a as Option).IdQuestion)
            .FirstOrDefault(a => a.Id == id) ?? throw new Exception("Option not found.");
    }

    public List<Option> GetAllOptions()
    {
        return _context.Options
            .AsNoTracking()
            .Include(a => a.IdQuestion)
            .ToList();
    }

    public async Task CreateOption(Option option)
    {
        _context.Options.Add(option);
        await _context.SaveChangesAsync();
    }

    public async Task CreateBulkOptions(List<Option> options)
    {
        foreach (var option in options)
        {
            if (!_context.Options.Any(o => o.Title == option.Title && o.IdQuestion.Title == option.IdQuestion.Title))
                _context.Options.Add(option);
        }
        await _context.SaveChangesAsync();
    }
}