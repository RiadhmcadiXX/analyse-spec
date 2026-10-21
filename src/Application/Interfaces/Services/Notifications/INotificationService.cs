using Domain.Common;
using Domain.Entities.Identity;

namespace Application.Interfaces.Services.Notifications;

public interface INotificationService
{
    Task<SucceededOrNotResponse> SendForgotPasswordNotification(User user, string link);
    Task<SucceededOrNotResponse> SendInitialPassword(User user, string password);
    Task<SucceededOrNotResponse> SendTwoFactorAuthenticationCodeNotification(User user, string code);
}