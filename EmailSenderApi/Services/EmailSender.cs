using EmailSenderApi.Helpers;
using EmailSenderApi.Interface;
using EmailSenderApi.Models;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;

namespace EmailSenderApi.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailSenderOptions _options;
    
    public EmailSender(IOptions<EmailSenderOptions> options) => _options = options.Value;

    public bool SendEmail(Email email)
    {
        try
        {
            var message = GetMessage(email);

            var attachment = new MimePart();
            var body = new Multipart(email.HasAttachments ? "mixed" : "plain") { message.Body };
            if (email.HasAttachments)
            {
                if (email.Attachments.Any(file =>
                        !_options.AllowedContentType.Split(',').Select(x => x.Trim()).Contains(file.ContentType)))
                    return false;

                var attachments = email.Attachments.ToList();
                var i = 0;
                attachments.ForEach(a =>
                    {
                        attachment = new MimePart
                        {
                            Content = new MimeContent(a.File),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64,
                            FileName = a.Name,
                            IsAttachment = email.HasAttachments
                        };
                        i++;
                        body.Add(attachment);
                    }
                );
            }

            message.Body = body;

            Send(message);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool ResetPassword(string EmailAddress)
    {
        try
        {
            var newPassword = HashExtension.GenerateDefaultPassword();

            var message = new MimeMessage()
            {
                Body = new TextPart("plain")
                {
                    Text = $"Your new password is {newPassword}"
                },
                Subject = "Reset Password",
                From = { MailboxAddress.Parse(_options.SenderEmail) },
                To = { MailboxAddress.Parse(EmailAddress) }
            };
        
            Send(message);
        
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    private void Send(MimeMessage message)
    {
        using var emailClient = new SmtpClient();

        if (_options == null) return;
        
        if (_options.MailServer == "smtp.gmail.com")
            emailClient.Connect(_options.MailServer, _options.MailPort,
                SecureSocketOptions.StartTls);
        else
            emailClient.Connect(_options.MailServer, _options.MailPort);

        emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
        emailClient.Authenticate(_options.SenderEmail, _options.AppPassword);
        emailClient.Send(message);
        emailClient.Disconnect(true);
    }
    
    private static MimeMessage GetMessage(Email Email)
    {
        var message = new MimeMessage();
        var fromEmail = MailboxAddress.Parse(Email.From.EmailAddress);
        fromEmail.Name = Email.From.Name;
        message.From.Add(fromEmail);

        Email.To.ForEach(x =>
        {
            var t = MailboxAddress.Parse(x.EmailAddress);
            t.Name = x.Name;
            message.To.Add(t);
        });
        Email.Cc?.ForEach(x =>
        {
            var t = MailboxAddress.Parse(x.EmailAddress);
            t.Name = x.Name;
            message.Cc.Add(t);
        });
        Email.Bcc?.ForEach(x =>
        {
            var t = MailboxAddress.Parse(x.EmailAddress);
            t.Name = x.Name;
            message.Bcc.Add(t);
        });
        message.Subject = Email.Subject;


        var body = new TextPart("plain")
        {
            Text = Email.Body
        };
        message.Body = body;
        //message.HtmlBody= true;
        return message;
    }
}