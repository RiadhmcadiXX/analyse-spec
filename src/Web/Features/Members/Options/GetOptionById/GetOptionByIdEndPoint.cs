using Domain.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IMapper = AutoMapper.IMapper;


namespace Web.Features.Members.Options.GetOptionById;
public class GetOptionByIdsEndPoint : Endpoint<GetOptionByIdRequest, OptionDto>
{
    private readonly IMapper _mapper;
    private readonly IOptionRepository _optionRepository;

    public GetOptionByIdsEndPoint(IMapper mapper, IOptionRepository OptionRepository)
    {
        _mapper = mapper;
        _optionRepository = OptionRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();

        Get("option/{id}");
        Roles(Domain.Constants.User.Roles.MEMBER);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(GetOptionByIdRequest request, CancellationToken ct)
    {
        var option = _optionRepository.GetOptionById(request.Id);
        await SendOkAsync(_mapper.Map<OptionDto>(option), ct);
    }
}
