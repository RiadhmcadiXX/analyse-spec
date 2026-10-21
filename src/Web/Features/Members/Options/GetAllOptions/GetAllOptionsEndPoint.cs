using Domain.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.Dtos;
using IMapper = AutoMapper.IMapper;

namespace Web.Features.Members.Options.GetAllOptions;

public class GetAllOptionsEndPoint : EndpointWithoutRequest<List<OptionDto>>
{
    private readonly IMapper _mapper;
    private readonly IOptionRepository _optionRepository;

    public GetAllOptionsEndPoint(IMapper mapper, IOptionRepository optionRepository)
    {
        _mapper = mapper;
        _optionRepository = optionRepository;
    }

    public override void Configure()
    {
        Get("options");
        Roles(Domain.Constants.User.Roles.MEMBER);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var options = _optionRepository.GetAllOptions();
        await SendOkAsync(_mapper.Map<List<OptionDto>>(options), ct);
    }
}