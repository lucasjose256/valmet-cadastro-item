using System.Net;
using System.Net.Mail;

namespace valmet_cadastro_item.Repositories
{
    public class SmptRepository : ISmtpRepository
    {
        private readonly IConfiguration _configuration;

        public SmptRepository(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public bool SendEmail(string email, string message, string subject)
        {
            try {
                string host = _configuration.GetValue<string>("SMTP:Host");
                string name = _configuration.GetValue<string>("SMTP:Name");
                string username = _configuration.GetValue<string>("SMTP:UserName");
                string password = _configuration.GetValue<string>("SMTP:Senha");
                int port = _configuration.GetValue<int>("SMTP:Port");

                MailMessage mail = new MailMessage()
                {
                    From=new MailAddress(username, name),
                };

                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body=message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, port))
                {


                    smtp.Credentials = new NetworkCredential(username, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return true;
                }


            }
            catch(Exception ex) { 

                return false;
            }
        }
    }
}
