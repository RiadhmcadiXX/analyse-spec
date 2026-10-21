namespace Application.Services.Notifications.Models;

public class InitialPasswordModel : NotificationModel
{
    public string Password { get; set; }

    public InitialPasswordModel(string destination, string locale, string password) 
        : base(destination, locale)
    {
        Password = password;
    }

    public override string TemplateId()
    {
        if (Locale == "fr")
            return "d-81dc4999e87f4393af5b0a50f2b7d69d"; 
        return "dd-b24317adcf3d4be3a0e3dd4c9fa2702c"; 
    }

    public override object TemplateData()
    {
        return new
        {
            Password = Password
        };
    }
}