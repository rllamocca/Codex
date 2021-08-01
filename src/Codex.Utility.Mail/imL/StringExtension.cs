using System;
using System.Net.Mail;

namespace Codex.Utility.Mail
{
    public static class StringExtension
    {
        public static bool IsMail(this string _this, bool _throw = false)
        {
            if (_this == null)
                return false;

            try
            {
                new MailAddress(_this);
                return true;
            }
            catch (Exception _ex)
            {
                if (_throw)
                    throw _ex;
            }
            return false;
        }
    }
}
