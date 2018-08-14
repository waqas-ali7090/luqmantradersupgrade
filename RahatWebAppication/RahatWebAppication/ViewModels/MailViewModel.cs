using System.Net.Mail;

namespace RahatWebAppication.ViewModels
{
    public class MailViewModel
    {
        public string From { get; set; }
        public MailAddress To { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
    }
}