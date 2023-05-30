using DataBase;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using Models;
using System.Net.Mail;
using MailKit.Net.Smtp;
using static System.Net.Mime.MediaTypeNames;

namespace FYP
{


    public class SendEmail
    {
       

       
        public string? From { get; set; }
        public string? Subject { get; set; }
        public string? UserName { get; set; }
        string fileData = File.ReadAllText("D:\\FYP\\FYP\\FYP\\EmailTemplate\\template.cshtml");
        public async Task sEmail()
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(UserName, From));
            message.To.Add(new MailboxAddress("Ahmad Fraz", "ahmadfrazahm5@gmail.com"));
            message.Subject = Subject;


            message.Body = new TextPart("html")
            {
                Text = fileData
            };

            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(From, "pfwhidhwzkkmrlus");

                    await client.SendAsync(message);
                    Console.WriteLine(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}