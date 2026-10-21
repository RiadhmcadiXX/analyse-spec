using Domain.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.Dtos;
using IMapper = AutoMapper.IMapper;

namespace Web.Features.Members.Answers.GetAllAnswers;

public class GetAllAnswersEndPoint : EndpointWithoutRequest<List<AnswerDto>>
{
    private readonly IMapper _mapper;
    private readonly IAnswerRepository _answerRepository;

    public GetAllAnswersEndPoint(IMapper mapper, IAnswerRepository answerRepository)
    {
        _mapper = mapper;
        _answerRepository = answerRepository;
    }

    public override void Configure()
    {
        Get("answers");
        Roles(Domain.Constants.User.Roles.MEMBER);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var answers = _answerRepository.GetAllAnswers();
        await SendOkAsync(_mapper.Map<List<AnswerDto>>(answers), ct);
    }
}