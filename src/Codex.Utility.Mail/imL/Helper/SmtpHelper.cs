using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Codex.Utility.Mail.Helper
{
    public static class SmtpHelper
    {
        public static void Init_SmtpClient(ref SmtpClient _ref, SmtpConfig _conf)
        {
            _ref.Timeout = _conf.Timeout ?? _ref.Timeout;
            _ref.TargetName = _conf.TargetName ?? _ref.TargetName;
            _ref.Port = _conf.Port ?? _ref.Port;
            _ref.PickupDirectoryLocation = _conf.PickupDirectoryLocation ?? _ref.PickupDirectoryLocation;
            _ref.Host = _conf.Host ?? _ref.Host;
            _ref.EnableSsl = _conf.EnableSsl ?? _ref.EnableSsl;
            _ref.DeliveryMethod = _conf.DeliveryMethod;
#if (NET45 || NETSTANDARD2_0 )
            _ref.DeliveryFormat = _conf.DeliveryFormat;
#endif
            _ref.UseDefaultCredentials = _conf.UseDefaultCredentials ?? _ref.UseDefaultCredentials;

            if (_ref.UseDefaultCredentials == false)
                _ref.Credentials = new NetworkCredential(_conf.UserName, _conf.Password);
        }
        public static void Init_MailMessage(ref MailMessage _ref, MailConfig _conf, Encoding _enc)
        {
#if NET35 == false
            _ref.HeadersEncoding = _enc;
#endif
            _ref.SubjectEncoding = _enc;
            _ref.BodyEncoding = _enc;

            _ref.IsBodyHtml = _conf.IsBodyHtml ?? _ref.IsBodyHtml;
            _ref.Priority = _conf.Priority;
#if (NET45 || NETSTANDARD2_0)
            _ref.BodyTransferEncoding = _conf.BodyTransferEncoding;
#endif
            _ref.DeliveryNotificationOptions = _conf.DeliveryNotificationOptions;

            _ref.From = new MailAddress(_conf.FromAddress, _conf.FromDisplayName, _enc);
            _ref.Sender = _ref.From;

            foreach (string _item2 in _conf.TO.DefaultOrEmpty())
                _ref.To.Add(new MailAddress(_item2));

            foreach (string _item2 in _conf.CC.DefaultOrEmpty())
                _ref.CC.Add(new MailAddress(_item2));

            foreach (string _item2 in _conf.BCC.DefaultOrEmpty())
                _ref.Bcc.Add(new MailAddress(_item2));

            foreach (string _item2 in _conf.PathAttachments.DefaultOrEmpty())
                if (File.Exists(_item2))
                    _ref.Attachments.Add(new Attachment(_item2));

            foreach (StreamAttachment _item2 in _conf.StreamAttachments.DefaultOrEmpty())
                _ref.Attachments.Add(new Attachment(_item2.Content, _item2.Name));

            _ref.Subject = _conf.Subject;
            _ref.Body = _conf.Body;
        }

        public static void To_Send(SmtpConfig _config, MailConfig[] _mails, Encoding _enc = null)
        {
            EncodingUtility.SolutionDefault(ref _enc);

            SmtpClient _sc = new SmtpClient();
            SmtpHelper.Init_SmtpClient(ref _sc, _config);

            foreach (MailConfig _item in _mails)
            {
                MailMessage _mm = new MailMessage();
                SmtpHelper.Init_MailMessage(ref _mm, _item, _enc);
                _sc.Send(_mm);
                _mm.Dispose();
            }

#if (NET35 == false)
            _sc.Dispose();
#endif

        }
    }
}