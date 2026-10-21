using Domain.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IMapper = AutoMapper.IMapper;


namespace Web.Features.Members.Questions.GetQuestionById;
public class GetQuestionByIdEndPoint : Endpoint<GetQuestionByIdRequest, QuestionDto>
{
    private readonly IMapper _mapper;
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionByIdEndPoint(IMapper mapper, IQuestionRepository QuestionRepository)
    {
        _mapper = mapper;
        _questionRepository = QuestionRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();

        Get("question/{id}");
        Roles(Domain.Constants.User.Roles.MEMBER);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(GetQuestionByIdRequest request, CancellationToken ct)
    {
        var question = _questionRepository.GetQuestionById(request.Id);
        await SendOkAsync(_mapper.Map<QuestionDto>(question), ct);
    }
}
