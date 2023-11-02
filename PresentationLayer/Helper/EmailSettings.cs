using DataAccessLayer.Entities;
using System.Net;
using System.Net.Mail;

namespace PresentationLayer.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.ethereal.email", 587);
            
            client.EnableSsl = true;
            //"jjopzedjumikpxtg" "google"
            client.Credentials = new NetworkCredential("tito.gaylord44@ethereal.email", "tXHv6aCw1yFDUNJnPy");
            
            client.Send("tito.gaylord44@ethereal.email", email.To,email.Title,email.Body);
        }
    }
}
