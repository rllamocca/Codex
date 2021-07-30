#if (NET45 || NETSTANDARD2_0)

using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Codex.Utility.Mail.Helper
{
    public static class SmtpHelperAsync
    {
        public async static Task To_Send(SmtpConfig _config, MailConfig[] _mails, Encoding _enc = null)
        {
            if (_enc == null)
                _enc = Encoding.Unicode;

            SmtpClient _sc = new SmtpClient();
            _sc.Timeout = _config.Timeout.Value;
            _sc.TargetName = _config.TargetName.HasValue() ? _config.TargetName : _sc.TargetName;
            _sc.Port = _config.Port;
            _sc.PickupDirectoryLocation = _config.PickupDirectoryLocation;
            _sc.Host = _config.Host;
            _sc.EnableSsl = _config.EnableSsl;
            _sc.DeliveryMethod = _config.DeliveryMethod;
            _sc.DeliveryFormat = _config.DeliveryFormat;
            _sc.UseDefaultCredentials = _config.UseDefaultCredentials;

            if (_sc.UseDefaultCredentials == false)
                _sc.Credentials = new NetworkCredential(_config.UserName, _config.Password);

            foreach (MailConfig _item in _mails)
            {
                using (MailMessage _mm = new MailMessage())
                {
                    _mm.HeadersEncoding = _enc;
                    _mm.SubjectEncoding = _enc;
                    _mm.BodyEncoding = _enc;

                    _mm.IsBodyHtml = _item.IsBodyHtml;
                    _mm.Priority = _item.Priority;
                    _mm.BodyTransferEncoding = _item.BodyTransferEncoding;
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

                    await _sc.SendMailAsync(_mm);
                }
            }

            _sc.Dispose();
        }
    }
}
#endif