using Domain.Entities.SPEC;
using Domain.Entities.Students;

namespace Web.Features.Members.Options.GetOptionsByIds;

public class GetOptionsByIdsRequest
{
    public required List<Guid> Ids { get; set; }
    public required Question IdQuestion { get; set; }
    public required string Title { get; set; }
}
