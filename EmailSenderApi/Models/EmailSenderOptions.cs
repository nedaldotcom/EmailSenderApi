namespace EmailSenderApi.Models;

public class EmailSenderOptions
{
    public string MailServer { get; set; }
    public int MailPort { get; set; }
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
    public string AppPassword { get; set; }
    public string AllowedContentType { get; set; }
}