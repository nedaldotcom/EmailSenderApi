namespace EmailSenderApi.Models;

public class EmailSenderReq
{
    public string Subject { get; set; }
    public string Content { get; set; }
    public EmailParticipant From { get; set; }
    public List<EmailParticipant> To { get; set; }
    public List<EmailParticipant> Cc { get; set; }
    public List<EmailParticipant> Bcc { get; set; }

    public Email ToEmail(List<IFormFile> files)
    {
        var email = new Email
        {
            Bcc = Bcc,
            Subject = Subject,
            Body = Content,
            From = From,
            To = To,
            Cc = Cc,
            Attachments = new List<EmailAttachment>()
        };
        if (files.Any()) files.ForEach(f =>
            email.Attachments.Add(new EmailAttachment(f.FileName, f.OpenReadStream(), f.ContentType)));

        return email;
    }
}