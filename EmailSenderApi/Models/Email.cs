namespace EmailSenderApi.Models;

public class Email
{
    public EmailParticipant From { get; set; }
    public List<EmailParticipant> To { get; set; }
    public List<EmailParticipant> Cc { get; set; }
    public List<EmailParticipant> Bcc { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool HasAttachments { get; set; }
    private List<EmailAttachment> _attachments;
    public List<EmailAttachment> Attachments
    {
        get => _attachments;
        set
        {
            _attachments = value;
            HasAttachments = true;
        }
    }
}

public abstract record EmailParticipant(string Name, string EmailAddress);

public record EmailAttachment(string Name, Stream File, string ContentType);