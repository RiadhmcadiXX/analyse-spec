using Domain.Entities.Identity;
using Domain.Repositories;
using Persistence;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories.Programmes;

public class ProgrammeRepository : IProgrammeRepository
{
    private readonly AnalyseSpecDbContext _context;

    public ProgrammeRepository(AnalyseSpecDbContext context)
    {
        _context = context;
    }

    public List<Programme> GetAllProgramme()
    {
        return _context.Programmes.ToList();
    }
    

    public async Task<Programme> GetProgrammeById(Guid id)
    {
        return await _context.Programmes.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Programme> GetProgrammeByCode(string code)
    {
        return await _context.Programmes.FirstOrDefaultAsync(p => p.Code == code);
    }

    public async Task<Programme> FindByName(string name)
    {
        return await _context.Programmes.FirstOrDefaultAsync(p => p.Name == name);
    }
    
    public async Task<List<Programme>> FindByNames(List<string> names)
    {
        return await _context.Programmes
            .Where(p => names.Contains(p.Name))
            .ToListAsync();
    }


    public void AddProgramme(Programme programme)
    {
        _context.Programmes.Add(programme);
    }

    public void UpdateProgramme(Programme programme)
    {
        _context.Programmes.Update(programme);
    }

    public void DeleteProgramme(Programme programme)
    {
        _context.Programmes.Remove(programme);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}