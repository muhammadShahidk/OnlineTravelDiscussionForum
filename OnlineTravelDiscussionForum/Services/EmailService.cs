using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using System.Configuration;

namespace OnlineTravelDiscussionForum.Services
{
    public class EmailService : IEMailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }



        public void SendEmail(EmailDto request)
        {
            var smtpSettings = _config.GetSection("SmtpSettings").Get<SmtpSettingsDto>(); 
            
            var email = new MimeMessage();
            
            //mail creation
            email.From.Add(MailboxAddress.Parse(smtpSettings.Username));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

           
            // Send email
            using (var client = new SmtpClient())
            {
                client.Connect(smtpSettings.Host, smtpSettings.Port, false);
                client.Authenticate(smtpSettings.Username, smtpSettings.Password);
                client.Send(email);
                client.Disconnect(true);
            }

        }
    }
}


//using var smtp = new SmtpClient();
//smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
//smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
//smtp.Send(email);
//smtp.Disconnect(true);





//message.From.Add(new MailboxAddress("Your Name", "your-email@gmail.com"));
//message.To.Add(new MailboxAddress("", userEmail));
//message.Subject = "Password Reset";

//var bodyBuilder = new BodyBuilder();
//bodyBuilder.TextBody = $"Click the link to reset your password: {link}";

//message.Body = bodyBuilder.ToMessageBody();