namespace Codex.Config
{
    public class MailConfig
    {
        public bool IsBodyHtml { set; get; }

        public string FromAddress { set; get; }
        public string FromDisplayName { set; get; }

        public string[] TO { set; get; }
        public string[] CC { set; get; }
        public string[] CCO { set; get; }
        public string[] Attachments { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }
    }
}
