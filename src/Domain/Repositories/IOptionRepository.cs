using Domain.Entities.SPEC;

namespace Domain.Repositories;

public interface IOptionRepository
{
    List<Option> GetOptionsByIds(List<Guid> ids);
    Option GetOptionById(Guid id);
    List<Option> GetAllOptions();
    Task CreateOption(Option option);
    Task CreateBulkOptions(List<Option> options);
}

