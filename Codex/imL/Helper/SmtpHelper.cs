#if (NET35 || NET40 || NET45 || NETSTANDARD2_0)

using Codex.Config;
using Codex.Extension;

using System.Net;
using System.Net.Mail;
using System.Text;

namespace Codex.Helper
{
    public static class SmtpHelper
    {
        public static void To_Send(SmtpConfig _config, MailConfig[] _mails, Encoding _enc = null)
        {
            if (_enc == null)
                _enc = Encoding.UTF8;

            SmtpClient _sc = new SmtpClient
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,

                Host = _config.Host,
                Port = _config.Port,
                EnableSsl = _config.EnableSsl,
                Credentials = new NetworkCredential(_config.UserName, _config.Password)
            };

            foreach (MailConfig _item in _mails)
            {
                using (MailMessage _mm = new MailMessage())
                {
                    _mm.SubjectEncoding = _enc;
                    _mm.BodyEncoding = _enc;
                    _mm.IsBodyHtml = _item.IsBodyHtml;

                    //_mm.Priority = MailPriority.High;

                    _mm.From = new MailAddress(_item.FromAddress, _item.FromDisplayName, _enc);

                    foreach (string _item3 in _item.TO.DefaultOrEmpty())
                        _mm.To.Add(new MailAddress(_item3));

                    foreach (string _item3 in _item.CC.DefaultOrEmpty())
                        _mm.CC.Add(new MailAddress(_item3));

                    foreach (string _item3 in _item.CCO.DefaultOrEmpty())
                        _mm.Bcc.Add(new MailAddress(_item3));

                    foreach (string _item3 in _item.Attachments.DefaultOrEmpty())
                        _mm.Attachments.Add(new Attachment(_item3));

                    _mm.Subject = _item.Subject;
                    _mm.Body = _item.Body;

                    _sc.Send(_mm);
                }
            }

#if (NET35 == false)
            _sc.Dispose();
#endif

        }
    }
}
#endif