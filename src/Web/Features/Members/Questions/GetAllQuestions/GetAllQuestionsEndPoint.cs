using Domain.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.Dtos;
using IMapper = AutoMapper.IMapper;

namespace Web.Features.Members.Questions.GetAllQuestions;

public class GetAllQuestionsEndPoint : EndpointWithoutRequest<List<QuestionDto>>
{
    private readonly IMapper _mapper;
    private readonly IQuestionRepository _questionRepository;

    public GetAllQuestionsEndPoint(IMapper mapper, IQuestionRepository questionRepository)
    {
        _mapper = mapper;
        _questionRepository = questionRepository;
    }

    public override void Configure()
    {
        Get("questions");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var questions = _questionRepository.GetAllQuestions();
        await SendOkAsync(_mapper.Map<List<QuestionDto>>(questions), ct);
    }
}