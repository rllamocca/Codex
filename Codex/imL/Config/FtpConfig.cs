namespace Codex.Config
{
    public class FtpConfig
    {
        public string Uri { set; get; }
        public string FileName { set; get; }
        public bool UseBinary { set; get; }
        public bool KeepAlive { set; get; }
        public bool UsePassive { set; get; }
        public bool EnableSsl { set; get; }

        public string UserName { set; get; }
        public string Password { set; get; }
    }
}
