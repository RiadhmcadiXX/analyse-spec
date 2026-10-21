using Application.Extensions;
using Domain.Common;
using Domain.Extensions;
using Domain.Repositories;
using FastEndpoints;
using Web.Cookies;

namespace Web.Features.Public.Authentication.ResetPassword;

public class ResetPasswordEndpoint : Endpoint<ResetPasswordRequest, SucceededOrNotResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ResetPasswordEndpoint> _logger;

    public ResetPasswordEndpoint(
        IUserRepository userRepository,
        ILogger<ResetPasswordEndpoint> logger)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();

        Post("authentication/reset-password");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ResetPasswordRequest req, CancellationToken ct)
    {   
        var user = _userRepository.FindById(req.UserId);
        
        if (user == null)
        {
            _logger.LogInformation("Could not reset password since no user with user id {id} exists.", req.UserId);
            await SendOkAsync(new SucceededOrNotResponse(false), ct);
            return;
        }
        Console.WriteLine("This is the token on restePassword" + req.Token);
        var resetToken = await  _userRepository.GetResetPasswordTokenForUser(user);
        var identityResult = await _userRepository.UpdateUserPassword(user, req.Password, resetToken);
        if (user.IsFirstConnection == true)
        {
            user.SetFirstConnectionFalse();
            await _userRepository.UpdateUser(user);
        }
        if (!identityResult.Succeeded)
        {
            await SendOkAsync(new SucceededOrNotResponse(false, identityResult.GetErrors()), ct);
            return;
        }

        await SendOkAsync(new SucceededOrNotResponse(true), ct);
    }
}