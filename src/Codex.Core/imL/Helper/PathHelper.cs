using System;
using System.IO;

namespace Codex
{
    public static class PathHelper
    {
        public static string Combine(params string[] _paths)
        {
#if (NET35)
            if (_paths == null)
                new ArgumentNullException(nameof(_paths));

            if (_paths.Length > 0)
            {
                string _return = Path.Combine("", _paths[0]);
                for (int _i = 1; _i < _paths.Length; _i++)
                    _return = Path.Combine(_return, _paths[_i]);

                return  _return;
            }
            throw new ArgumentNullException(nameof(_paths));
#else
            return Path.Combine(_paths);
#endif
        }
    }
}
