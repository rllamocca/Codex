namespace Codex.Utility.Ftp
{
    public class FormatFtp
    {
        public bool? UseBinary { get; set; }
        public int? Timeout { get; set; }
        public int? ReadWriteTimeout { get; set; }
        public bool? KeepAlive { get; set; }
        public bool? EnableSsl { get; set; }
        public bool? UsePassive { get; set; }


        public string Host { set; get; }
        public string Path { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}
