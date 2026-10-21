using Application.Services.SPEC.Dtos;

namespace Application.Interfaces.Services.SPEC;

public interface ISpecCreationService
{
    Task CreateSpec(SpecRegistrationDto fileData);
}