using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public class SendEmailProcessHandler:ProcessHandler
    {
        private readonly string _fileName;
        private readonly string _toEmail;

        public SendEmailProcessHandler(string fileName, string toEmail)
        {
            _fileName = fileName;
            _toEmail = toEmail;
        }
        public override object handle(object o)
        {
            var mailMessage = new MailMessage();

            var zipMemoryStream = o as MemoryStream;
            zipMemoryStream.Position = 0;

            var smtpClient = new SmtpClient("srvm11.trwww.com");
            mailMessage.From = new MailAddress("deneme@kariyersistem.com");
            mailMessage.To.Add(new MailAddress(_toEmail));

            mailMessage.Subject = "Zip dosyası.";

            mailMessage.Body = "<p>Zip dosyası ektedir : ... </p>";

            Attachment attachment = new Attachment(zipMemoryStream,_fileName,MediaTypeNames.Application.Zip);

            mailMessage.Attachments.Add(attachment);

            mailMessage.IsBodyHtml = true;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("deneme@kariyersistem.com", "Password12*");
            smtpClient.Send(mailMessage);

            return base.handle(null); //Zincirin başka halkası olmadığı için 
        }
    }
}
