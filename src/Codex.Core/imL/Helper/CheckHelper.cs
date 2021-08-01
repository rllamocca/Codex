using System;
using System.Text.RegularExpressions;

using Codex.Utility;

namespace Codex
{
    public static class CheckHelper
    {
        public static bool IsNumber(object _number, bool _throw = false)
        {
            if (_number == null)
                return false;

            try
            {
                string _a = Convert.ToString(_number);

                if (_a.HasValueTrim() == false)
                    return false;

                return Regex.IsMatch(_a, @"^[-+]?[0-9]*\.?[0-9]+$");
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