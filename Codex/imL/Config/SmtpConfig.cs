namespace Codex.Config
{
    public class SmtpConfig
    {
        public string Host { set; get; }
        public int Port { set; get; }
        public int Timeout { set; get; }
        public bool EnableSsl { set; get; }

        public string UserName { set; get; }
        public string Password { set; get; }
    }
}
