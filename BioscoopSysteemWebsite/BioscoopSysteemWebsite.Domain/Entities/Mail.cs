using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BioscoopSysteemWebsite.Domain.Entities {
    public class Mail {
        //Voor het inschrijven en de nieuwsbrief. - FrankM
        [Key]
        public int MailId { get; set; }
        public string MailAdres { get; set; }
        public string Voornaam { get; set; }

        //Geschreven door Robert-Jan Kooijman
        public bool checkMailAdres(string mail) {
            bool b = true;
            if (!mail.Equals("")) {
                if (mail.Equals(MailAdres)) {
                    b = false;
                }
            }
            return b;
        }

        //Geschreven door Robert-Jan Kooijman
        [ExcludeFromCodeCoverage]
        public void SendEmail() {
            try {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "rkooijma1@avans.nl";
                WebMail.Password = "CarpoAnt!th3us";
                WebMail.From = "rkooijma1@avans.nl";

                WebMail.Send(MailAdres, "Nieuwsbrief",
                "Dit is een test nieuwsbrief");
            } catch (Exception) {

            }
        }
        public void SendNewsLetter(string onderwerp, string tekst, Attachment attachment) {
            try {
                System.Net.Mail.MailMessage myMail = new System.Net.Mail.MailMessage();
                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                myMail.From = new System.Net.Mail.MailAddress("rkooijma1@avans.nl");
                myMail.Subject = onderwerp;
                myMail.IsBodyHtml = true;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("rkooijma1@avans.nl", "CarpoAnt!th3us");
                SmtpServer.EnableSsl = true;
                myMail.To.Add(MailAdres);
                myMail.Body = "Beste " + Voornaam + ", <br />" +
                    tekst;
                myMail.Attachments.Add(attachment);
                SmtpServer.Send(myMail);
            } catch (Exception) {
                
            }
        }
    }
}