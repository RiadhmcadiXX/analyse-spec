using Domain.Entities.SPEC;
using Domain.Entities.Students;

namespace Web.Features.Members.Questions.GetQuestionById;

public class GetQuestionByIdRequest
{
    public required Guid Id { get; set; }
    public required Question? Parent { get; set; }
    public required string Title { get; set; }
}
