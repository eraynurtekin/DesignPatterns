using BaseProject.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;

namespace WebApp.Observer.Observer
{
    public class UserObserverSendEmail : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverSendEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser appUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendEmail>>();

            var mailMessage = new MailMessage();

            var smtpClient = new SmtpClient("srvm11.trwww.com");
            mailMessage.From = new MailAddress("deneme@kariyersistem.com");
            mailMessage.To.Add(new MailAddress(appUser.Email));

            mailMessage.Subject = "Sitemize Hoşgeldiniz.";

            mailMessage.Body = "<p>Sitemizin Genel Kuralları : ... </p>";

            mailMessage.IsBodyHtml = true;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("deneme@kariyersistem.com", "Password12*");
            smtpClient.Send(mailMessage);

            logger.LogInformation($"Email was send to user : {appUser.UserName}");
        }
    }
}
