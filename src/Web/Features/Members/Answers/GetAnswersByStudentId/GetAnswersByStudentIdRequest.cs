using Domain.Entities.SPEC;
using Domain.Entities.Students;

namespace Web.Features.Members.Answers.GetAnswersByStudentId;

public class GetOptionsByIdsRequest
{
    public Guid Id { get; set; }
    public required Student IdStudent { get; set; }
    public Option? IdOption { get; set; }
}
