using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Codex.Utility.Mail.Helper
{
    public static class SmtpHelper
    {
        public static void To_Send(SmtpConfig _config, MailConfig[] _mails, Encoding _enc = null)
        {
            if (_enc == null)
                _enc = Encoding.Unicode;

            SmtpClient _sc = new SmtpClient
            {
                Host = _config.Host,
                Port = _config.Port,
                Timeout = _config.Timeout.Value,
                EnableSsl = _config.EnableSsl,

                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config.UserName, _config.Password)
            };

            foreach (MailConfig _item in _mails)
            {
                using (MailMessage _mm = new MailMessage())
                {
#if NET35 == false
                    _mm.HeadersEncoding = _enc;
#endif
                    _mm.SubjectEncoding = _enc;
                    _mm.BodyEncoding = _enc;

                    _mm.IsBodyHtml = _item.IsBodyHtml;
                    _mm.Priority = _item.Priority;
                    //_mm.BodyTransferEncoding = _item.BodyTransferEncoding;
                    _mm.DeliveryNotificationOptions = _item.DeliveryNotificationOptions;

                    _mm.From = new MailAddress(_item.FromAddress, _item.FromDisplayName, _enc);

                    foreach (string _item2 in _item.TO.DefaultOrEmpty())
                        _mm.To.Add(new MailAddress(_item2));

                    foreach (string _item2 in _item.CC.DefaultOrEmpty())
                        _mm.CC.Add(new MailAddress(_item2));

                    foreach (string _item2 in _item.CCO.DefaultOrEmpty())
                        _mm.Bcc.Add(new MailAddress(_item2));

                    foreach (string _item2 in _item.Attachments.DefaultOrEmpty())
                        if (File.Exists(_item2))
                            _mm.Attachments.Add(new Attachment(_item2));

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