using Web.Features.Members.Questions;

namespace Web.Features.Members.Questions.UpdateQuestion;

public class UpdateQuestionRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Label { get; set; }
}