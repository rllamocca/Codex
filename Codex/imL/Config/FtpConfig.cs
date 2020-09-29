using System;

namespace Codex.Config
{
    public class FtpConfig
    {
        public String Uri { set; get; }
        public String FileName { set; get; }
        public Boolean UseBinary { set; get; }
        public Boolean KeepAlive { set; get; }
        public Boolean UsePassive { set; get; }
        public Boolean EnableSsl { set; get; }

        public String UserName { set; get; }
        public String Password { set; get; }
    }
}
