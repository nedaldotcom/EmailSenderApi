using System.Text.Json;
using EmailSenderApi.Interface;
using EmailSenderApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EmailSenderController : ControllerBase
{
    private readonly IEmailSender _emailSender;
    
    public EmailSenderController(IEmailSender emailSender)
    => _emailSender = emailSender;

    [HttpPost]
    public async Task<IActionResult> SendEmail(EmailSenderReq? email)
    {
        var files = new List<IFormFile>();
        if (Request.ContentType != "application/json")
        {
            var forms = await Request.ReadFormAsync();
            files = forms.Files.ToList();
            var content = forms.FirstOrDefault(k => k.Key == "email").Value;
            email = JsonSerializer.Deserialize<EmailSenderReq>(content);
        }
        else if (email is null) throw new Exception("no body available");

        return Ok(_emailSender.SendEmail(email.ToEmail(files)));
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(string emailAddress)
    {
        return Ok(_emailSender.ResetPassword(emailAddress));
    }
}