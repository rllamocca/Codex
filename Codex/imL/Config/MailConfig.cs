using System;

namespace Codex.Config
{
    public class MailConfig
    {
        public Boolean IsBodyHtml { set; get; }

        public String FromAddress { set; get; }
        public String FromDisplayName { set; get; }

        public String[] TO { set; get; }
        public String[] CC { set; get; }
        public String[] CCO { set; get; }
        public String[] Attachments { set; get; }
        public String Subject { set; get; }
        public String Body { set; get; }
    }
}
