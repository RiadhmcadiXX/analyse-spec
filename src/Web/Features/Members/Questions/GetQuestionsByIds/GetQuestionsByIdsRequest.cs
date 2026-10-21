using Domain.Entities.SPEC;
using Domain.Entities.Students;

namespace Web.Features.Members.Questions.GetQuestionsByIds;

public class GetQuestionsByIdsRequest
{
    public required List<Guid> Ids { get; set; }
    public required Question? Parent { get; set; }
    public required string Title { get; set; }
}
