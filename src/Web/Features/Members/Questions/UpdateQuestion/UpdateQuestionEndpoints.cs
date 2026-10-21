using Domain.Common;
using Domain.Entities.SPEC;
using Domain.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.Features.Members.Questions;

using IMapper = AutoMapper.IMapper;

namespace Web.Features.Members.Questions.UpdateQuestion;

public class UpdateQuestionEndpoints : Endpoint<List<UpdateQuestionRequest>, SucceededOrNotResponse>
{
    
    private readonly IMapper _mapper;
    private readonly IQuestionRepository _questionRepository;


    public UpdateQuestionEndpoints(IMapper mapper, IQuestionRepository questionRepository)
    {
        
        _mapper = mapper;
        _questionRepository = questionRepository;
    }

    public override void Configure()
    {
        Put("/questions/update");
        Roles(Domain.Constants.User.Roles.ADMINISTRATOR);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(List<UpdateQuestionRequest> req, CancellationToken ct)
    {

        var updateQuestionDto = _mapper.Map<List<Question>>(req);
        await _questionRepository.UpdateQuestions(updateQuestionDto);
        await SendOkAsync(new SucceededOrNotResponse(true), ct);
    }
    
}