using System;
using System.Text.RegularExpressions;

namespace Codex.Helper
{
    public static class CheckHelper
    {
        public static bool Number(object _a)
        {
            if (_a == null)
                return false;
            try
            {
                string _b = _a.ToString().Trim();
                if (_b.Length == 0)
                    return false;
                return Regex.IsMatch(_b, @"^[-+]?[0-9]*\.?[0-9]+$");
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}