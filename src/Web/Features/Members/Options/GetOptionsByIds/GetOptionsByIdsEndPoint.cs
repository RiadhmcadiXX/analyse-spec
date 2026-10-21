using Domain.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag.Annotations;
using Web.Dtos;
using Web.Features.Members.Answers;
using IMapper = AutoMapper.IMapper;


namespace Web.Features.Members.Options.GetOptionsByIds;
public class GetQuestionsByIdsEndPoint : Endpoint<GetOptionsByIdsRequest, List<AnswerDto>>
{
    private readonly IMapper _mapper;
    private readonly IOptionRepository _optionRepository;

    public GetQuestionsByIdsEndPoint(IMapper mapper, IOptionRepository OptionRepository)
    {
        _mapper = mapper;
        _optionRepository = OptionRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();

        Get("options/{ids}");
        Roles(Domain.Constants.User.Roles.MEMBER);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(GetOptionsByIdsRequest request, CancellationToken ct)
    {
        var options = _optionRepository.GetOptionsByIds(request.Ids);
        await SendOkAsync(_mapper.Map<List<AnswerDto>>(options), ct);
    }
}
