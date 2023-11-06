using EticaretAPI.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.MailService
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
           await SendMailAsync(new[] {to},subject,body,isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in  tos) 
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"], "LearningEticaret",System.Text.Encoding.UTF8);

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtpClient.Port = 587;
            smtpClient.EnableSsl=true;
            smtpClient.Host = _configuration["Mail:Host"];
            await smtpClient.SendMailAsync(mail);


           
        }

        public async Task SendPasswordResetMailAsync(string to,string userId,string resetToken)
        {
            StringBuilder mail = new StringBuilder();
            mail.AppendLine("Merhaba,<br>");
            mail.AppendLine("Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br>");
            mail.AppendLine($"<strong><a target=\"_blank\" href=\"{_configuration["AngularClientUrl"]}/update-password/{userId}/{resetToken}\">Yeni şifre oluşturabilmek için tıklayınız</a></strong><br><br>");
            mail.AppendLine("<span>Eğer bu talebi siz gerçekleştirmediyseniz, mail bilgilerinizi korumaya açınız.</span><br>");
            mail.AppendLine("Saygılarımızla<br><br>ElearningEticaret");

            await SendMailAsync(to, "Şifre yenileme talebi", mail.ToString());

        }
        public async Task SendCompleteOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName, string userSurname)   
        {
            string mail = $"Sayın {userName}{userSurname}<br>" +
                $"{orderDate} tarihinde vermiş olduğunuz {orderCode} kodlu siparişiniz kargoya verilmiştir.<br>Elearning İyi Günler diler. ";
            await SendMailAsync(to, $"{orderCode} Sipariş numaralı Siparişiniz Tamamlandı", mail);

        }
    }
}
