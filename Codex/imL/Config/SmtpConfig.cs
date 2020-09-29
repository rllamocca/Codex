using System;

namespace Codex.Config
{
    public class SmtpConfig
    {
        public String Host { set; get; }
        public Int32 Port { set; get; }
        public Boolean EnableSsl { set; get; }

        public String UserName { set; get; }
        public String Password { set; get; }
    }
}
