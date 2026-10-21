using Domain.Entities.Books;
using Domain.Entities.SPEC;
using Domain.Repositories;
using FastEndpoints;
using Infrastructure.Repositories.Books;
using Infrastructure.Repositories.Members;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.Dtos;
using Web.Features.Members.Books.DeleteBook;
using Web.Features.Members.Books.GetBook;
using IMapper = AutoMapper.IMapper;


namespace Web.Features.Members.Questions.GetQuestionsByIds;
public class GetQuestionsByIdsEndPoint : Endpoint<GetQuestionsByIdsRequest, List<QuestionDto>>
{
    private readonly IMapper _mapper;
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionsByIdsEndPoint(IMapper mapper, IQuestionRepository QuestionRepository)
    {
        _mapper = mapper;
        _questionRepository = QuestionRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();

        Get("questions/{id}");
        Roles(Domain.Constants.User.Roles.MEMBER);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(GetQuestionsByIdsRequest request, CancellationToken ct)
    {
        var questions = _questionRepository.GetQuestionsByIds(request.Ids);
        await SendOkAsync(_mapper.Map<List<QuestionDto>>(questions), ct);
    }
}
