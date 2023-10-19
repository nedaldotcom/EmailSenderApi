using EmailSenderApi.Models;

namespace EmailSenderApi.Interface;

public interface IEmailSender
{
    public bool SendEmail(Email Email);
    public bool ResetPassword(string EmailAddress);
}