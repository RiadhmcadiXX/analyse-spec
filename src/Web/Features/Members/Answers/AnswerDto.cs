using Domain.Entities.SPEC;
using Domain.Entities.Students;

namespace Web.Features.Members.Answers;

public class AnswerDto
{
    public Guid Id { get; set; }
    public Option IdOption { get; set; } = default!;
    public Student IdStudent { get; set; } = default!;
}