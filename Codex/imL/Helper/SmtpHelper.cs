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
        public static void To_Mail(SmtpConfig _smtp, MailConfig[] _mails)
        {
            Encoding _ENCODING = Encoding.UTF8;

            using (SmtpClient _sc = new SmtpClient())
            {
                _sc.UseDefaultCredentials = false;
                _sc.DeliveryMethod = SmtpDeliveryMethod.Network;

                _sc.Host = _smtp.Host;
                _sc.Port = _smtp.Port;
                _sc.EnableSsl = _smtp.EnableSsl;
                _sc.Credentials = new NetworkCredential(_smtp.UserName, _smtp.Password);

                foreach (MailConfig _item in _mails)
                {
                    using (MailMessage _mm = new MailMessage())
                    {
                        _mm.SubjectEncoding = _ENCODING;
                        _mm.BodyEncoding = _ENCODING;
                        _mm.IsBodyHtml = _item.IsBodyHtml;

                        //_mm.Priority = MailPriority.High;

                        _mm.From = new MailAddress(_item.FromAddress, _item.FromDisplayName, _ENCODING);

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