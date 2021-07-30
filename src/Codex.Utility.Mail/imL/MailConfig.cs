using System.Net.Mail;
using System.Net.Mime;

namespace Codex.Utility.Mail
{
    public class MailConfig
    {
        public string Subject { set; get; }
        public MailPriority Priority { set; get; }
        public bool IsBodyHtml { set; get; }
        public TransferEncoding BodyTransferEncoding { set; get; }
        public string Body { set; get; }
        public DeliveryNotificationOptions DeliveryNotificationOptions { set; get; }


        public string FromAddress { set; get; }
        public string FromDisplayName { set; get; }

        public string[] TO { set; get; }
        public string[] CC { set; get; }
        public string[] CCO { set; get; }
        public string[] Attachments { set; get; }
    }
}
