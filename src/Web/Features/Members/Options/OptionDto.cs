using Domain.Entities.SPEC;

namespace Web.Features.Members.Options;

public class OptionDto
{
    public Guid Id { get; set; }
    public Question IdQuestion { get; set; } = default!;
    public string Title { get; set; } = default!;
}