using BioEasy.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BioEasy.Shared
{
    public class Mail
    {
        private readonly DatabaseContext _context;
        public Mail(DatabaseContext context)
        {
            _context = context;
        }

        public async Task SendMail(string email, string subject, string message)
        {
            try
            {
                var config = _context.Configuracoes.First();

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(config.Email, "BioEasy Pro")
                };

                mail.To.Add(new MailAddress(email));

                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (SmtpClient smtp = new SmtpClient(config.SMTP, Convert.ToInt32(config.Porta)))
                {
                    smtp.Credentials = new NetworkCredential(config.Email, config.Senha);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception){}
        }
    }
}
