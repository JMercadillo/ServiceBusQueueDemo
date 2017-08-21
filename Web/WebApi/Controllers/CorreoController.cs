using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class CorreoController : ApiController
    {

        public async Task<bool> EnviarCorreo(string EmailTo, string NameFrom, string Subject, string Body)
        {
            var EmailFrom = ConfigurationManager.AppSettings["Email"].ToString();
            var Password = ConfigurationManager.AppSettings["Pass"].ToString();
            var result = false;

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(EmailTo);
            msg.From = new MailAddress(EmailFrom, NameFrom, System.Text.Encoding.UTF8);
            msg.Subject = Subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = Body;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(EmailFrom, Password);
            client.Port = 587;
            client.Host = "smtp.office365.com";
            client.EnableSsl = true;
            try
            {
                await client.SendMailAsync(msg);
                result = false;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                result = false;
            }

            return result;
        }
    }
}
