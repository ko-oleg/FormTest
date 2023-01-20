using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
namespace SocialApp.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
 
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "olegatorkogay@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
             
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("olegatorkogay@gmail.com", "hapzezuapxshmbzn");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
                
                // client.ServerCertificateValidationCallback =
                //     (sender, certificate, certChainType, errors) => true;
                // client.AuthenticationMechanisms.Remove("XOAUTH2");

                // connection
                // await client.ConnectAsync("smtp.host", 587, true);
                // await client.AuthenticateAsync("UserName", "Password");//error occurs here
                // await client.SendAsync(emailMessage);
                // await client.DisconnectAsync(true);
            }
        }
    }
}