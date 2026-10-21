using Application.Interfaces.Services.SPEC;
using Application.Services.SPEC.Dtos;
using Domain.Common;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IMapper = AutoMapper.IMapper;

namespace Web.Features.Admins.SPEC;

public class CreateSpecEndpoint: Endpoint<CreateSpecRequest, SucceededOrNotResponse>
{
    private readonly IMapper _mapper;
    private readonly ISpecCreationService _specCreationService;

    public CreateSpecEndpoint(IMapper mapper, ISpecCreationService specCreationService)
    {
        _mapper = mapper;
        _specCreationService = specCreationService;
    }
    public override void Configure()
    {
        AllowFileUploads();
        DontCatchExceptions();

        Post("spec");
        Roles(Domain.Constants.User.Roles.ADMINISTRATOR);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(CreateSpecRequest req, CancellationToken ct)
    {
        var specInformationDto = _mapper.Map<SpecRegistrationDto>(req);
        await _specCreationService.CreateSpec(specInformationDto);
        await SendOkAsync(new SucceededOrNotResponse(true), cancellation: ct);
    }
}