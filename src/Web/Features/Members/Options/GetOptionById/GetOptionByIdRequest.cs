using Domain.Entities.SPEC;
using Domain.Entities.Students;

namespace Web.Features.Members.Options.GetOptionById;

public class GetOptionByIdRequest
{
    public required Guid Id { get; set; }
    public required Question IdQuestion { get; set; }
    public required string Title { get; set; }
}
