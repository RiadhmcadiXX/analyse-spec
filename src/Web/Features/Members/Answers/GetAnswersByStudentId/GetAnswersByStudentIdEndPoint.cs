using Domain.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IMapper = AutoMapper.IMapper;


namespace Web.Features.Members.Answers.GetAnswersByStudentId;
public class GetQuestionsByIdsEndPoint : Endpoint<GetOptionsByIdsRequest, List<AnswerDto>>
{
    private readonly IMapper _mapper;
    private readonly IAnswerRepository _answerRepository;

    public GetQuestionsByIdsEndPoint(IMapper mapper, IAnswerRepository answerRepository)
    {
        _mapper = mapper;
        _answerRepository = answerRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();

        Get("answers/{id}");
        Roles(Domain.Constants.User.Roles.MEMBER);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(GetOptionsByIdsRequest request, CancellationToken ct)
    {
        var answers = _answerRepository.GetAnswersByStudentId(request.Id);
        await SendOkAsync(_mapper.Map<List<AnswerDto>>(answers), ct);
    }
}
