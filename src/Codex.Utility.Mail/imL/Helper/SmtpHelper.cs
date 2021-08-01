using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Codex.Utility.Mail
{
    public static class SmtpHelper
    {
        public static void Init_SmtpClient(ref SmtpClient _ref, FormatSmtp _format)
        {
            _ref.Timeout = _format.Timeout ?? _ref.Timeout;
            _ref.TargetName = _format.TargetName ?? _ref.TargetName;
            _ref.Port = _format.Port ?? _ref.Port;
            _ref.PickupDirectoryLocation = _format.PickupDirectoryLocation ?? _ref.PickupDirectoryLocation;
            _ref.Host = _format.Host ?? _ref.Host;
            _ref.EnableSsl = _format.EnableSsl ?? _ref.EnableSsl;
            _ref.DeliveryMethod = _format.DeliveryMethod;
#if (NET45 || NETSTANDARD2_0 )
            _ref.DeliveryFormat = _format.DeliveryFormat;
#endif
            _ref.UseDefaultCredentials = _format.UseDefaultCredentials ?? _ref.UseDefaultCredentials;

            if (_ref.UseDefaultCredentials == false)
                _ref.Credentials = new NetworkCredential(_format.UserName, _format.Password);
        }
        public static void Init_MailMessage(ref MailMessage _ref, FormatMailMessage _format, Encoding _enc)
        {
#if NET35 == false
            _ref.HeadersEncoding = _enc;
#endif
            _ref.SubjectEncoding = _enc;
            _ref.BodyEncoding = _enc;

            _ref.IsBodyHtml = _format.IsBodyHtml ?? _ref.IsBodyHtml;
            _ref.Priority = _format.Priority;
#if (NET45 || NETSTANDARD2_0)
            _ref.BodyTransferEncoding = _format.BodyTransferEncoding;
#endif
            _ref.DeliveryNotificationOptions = _format.DeliveryNotificationOptions;


            _ref.From = new MailAddress(_format.FromAddress, _format.FromDisplayName, _enc);
            _ref.Sender = _ref.From;

            foreach (string _item2 in _format.TO.DefaultOrEmpty())
                _ref.To.Add(new MailAddress(_item2));

            foreach (string _item2 in _format.CC.DefaultOrEmpty())
                _ref.CC.Add(new MailAddress(_item2));

            foreach (string _item2 in _format.BCC.DefaultOrEmpty())
                _ref.Bcc.Add(new MailAddress(_item2));

            foreach (string _item2 in _format.PathAttachments.DefaultOrEmpty())
                if (File.Exists(_item2))
                    _ref.Attachments.Add(new Attachment(_item2));

            foreach (StreamAttachment _item2 in _format.StreamAttachments.DefaultOrEmpty())
                _ref.Attachments.Add(new Attachment(_item2.Content, _item2.Name));

            _ref.Subject = _format.Subject;
            _ref.Body = _format.Body;

            if (_ref.IsBodyHtml)
            {
                ContentType _mime = new ContentType("text/html");
                AlternateView _aw = AlternateView.CreateAlternateViewFromString(_format.Body, _mime);
                _ref.AlternateViews.Add(_aw);
            }
        }

        public static void Send(FormatSmtp _format, FormatMailMessage[] _formats, Encoding _enc = null)
        {
            EncodingUtility.SolutionDefault(ref _enc);

            SmtpClient _sc = new SmtpClient();
            SmtpHelper.Init_SmtpClient(ref _sc, _format);

            foreach (FormatMailMessage _item in _formats)
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