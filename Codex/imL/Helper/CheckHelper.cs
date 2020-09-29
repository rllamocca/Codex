#if (NETSTANDARD2_0 || NETSTANDARD2_1)

using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Codex.Helper
{
    public static class CheckHelper
    {
        public static Boolean Mail(String _a)
        {
            try
            {
                MailAddress _ma = new MailAddress(_a);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean Number(Object _a)
        {
            try
            {
                if (_a == null) return false;
                String _b = _a.ToString().Trim();
                if (_b.Length == 0) return false;
                return Regex.IsMatch(_b, @"^[-+]?[0-9]*\.?[0-9]+$");
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

#endif
