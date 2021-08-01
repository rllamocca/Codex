#if (NET45 || NETSTANDARD2_0)

using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Codex.Utility.Mail.Helper
{
    public static class SmtpHelperAsync
    {
        public async static Task To_Send(SmtpConfig _config, MailConfig[] _mails, Encoding _enc = null)
        {
            EncodingUtility.SolutionDefault(ref _enc);

            SmtpClient _sc = new SmtpClient();
            SmtpHelper.Init_SmtpClient(ref _sc, _config);

            foreach (MailConfig _item in _mails)
            {
                MailMessage _mm = new MailMessage();
                SmtpHelper.Init_MailMessage(ref _mm, _item, _enc);
                await _sc.SendMailAsync(_mm);
                _mm.Dispose();
            }

            _sc.Dispose();
        }
    }
}
#endif