#if (NETSTANDARD2_0 || NETSTANDARD2_1)

using Codex.Config;
using Codex.Extension;

using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Codex.Helper
{
    public static class SmtpHelper
    {
        public static void To_Send(SmtpConfig _config, MailConfig[] _mails)
        {
            Encoding _encoding = Encoding.UTF8;

            using (SmtpClient _sc = new SmtpClient())
            {
                _sc.UseDefaultCredentials = false;
                _sc.DeliveryMethod = SmtpDeliveryMethod.Network;

                _sc.Host = _config.Host;
                _sc.Port = _config.Port;
                _sc.EnableSsl = _config.EnableSsl;
                _sc.Credentials = new NetworkCredential(_config.UserName, _config.Password);

                foreach (MailConfig _item in _mails)
                {
                    using (MailMessage _mm = new MailMessage())
                    {
                        _mm.SubjectEncoding = _encoding;
                        _mm.BodyEncoding = _encoding;
                        _mm.IsBodyHtml = _item.IsBodyHtml;

                        //_mm.Priority = MailPriority.High;

                        _mm.From = new MailAddress(_item.FromAddress, _item.FromDisplayName, _encoding);

                        foreach (String _item3 in _item.TO.DefaultOrEmpty())
                            _mm.To.Add(new MailAddress(_item3));

                        foreach (String _item3 in _item.CC.DefaultOrEmpty())
                            _mm.CC.Add(new MailAddress(_item3));

                        foreach (String _item3 in _item.CCO.DefaultOrEmpty())
                            _mm.Bcc.Add(new MailAddress(_item3));

                        foreach (String _item3 in _item.Attachments.DefaultOrEmpty())
                            _mm.Attachments.Add(new Attachment(_item3));

                        _mm.Subject = _item.Subject;
                        _mm.Body = _item.Body;

                        _sc.Send(_mm);
                    }
                }
            }
        }
    }
}

#endif