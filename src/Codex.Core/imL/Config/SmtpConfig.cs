#if (NET35 || NET40 || NET45 || NETSTANDARD2_0)

using System.Net.Mail;

namespace Codex.Config
{
    public class SmtpConfig
    {
        public int Timeout { set; get; }
        public string TargetName { set; get; }
        public int Port { set; get; }
        public string PickupDirectoryLocation { set; get; }
        public string Host { set; get; }
        public bool EnableSsl { set; get; }
        public SmtpDeliveryMethod DeliveryMethod { set; get; }
#if NET45 || NETSTANDARD2_0
        public SmtpDeliveryFormat DeliveryFormat { set; get; }
#endif
        public bool UseDefaultCredentials { set; get; }

        public string UserName { set; get; }
        public string Password { set; get; }
    }
}

#endif