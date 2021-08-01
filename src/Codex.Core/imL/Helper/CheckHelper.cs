using System;
using System.Text.RegularExpressions;

namespace Codex
{
    public static class CheckHelper
    {
        public static bool IsNumber(object _a, bool _throw = false)
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
            catch (Exception _ex)
            {
                if (_throw)
                    throw _ex;
            }

            return false;
        }
    }
}