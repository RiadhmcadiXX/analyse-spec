using Domain.Entities.Identity;

namespace Domain.Repositories;

public interface IProgrammeRepository: IDisposable
{
    List<Programme> GetAllProgramme();
    Task<Programme> GetProgrammeById(Guid id);
    Task<Programme> GetProgrammeByCode(string code);
    Task<Programme> FindByName(string name);
    void AddProgramme(Programme programme);
    void UpdateProgramme(Programme programme);
    void DeleteProgramme(Programme programme);
    Task<List<Programme>> FindByNames(List<string> names);

    

}