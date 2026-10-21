using Domain.Entities.SPEC;

namespace Web.Features.Members.Questions;

public class QuestionDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    
    public string? Label { get; set; }
    public Question? Parent { get; set; } = default!;
}
