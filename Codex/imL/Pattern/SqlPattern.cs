using System.Linq;

namespace Codex.Pattern
{
    public static class SqlPattern
    {
        public static string Insert(string _table, IOrmParameter[] _a, bool _scope_identity = false)
        {
            string _pattern = @"
 INSERT INTO {0}
 ({1})
 VALUES
 ({2});
 {3}
";
            string _0 = null;
            string _1 = null;
            string _2 = null;
            string _3 = null;


            _0 = string.Format("[{0}]", _table);
            string[] _tmp = _a.Select(_s => string.Format("[{0}]", _s.Source)).ToArray();
            _1 = string.Join(",", _tmp);
            _tmp = _a.Select(_s => _s.Affect).ToArray();
            _2 = string.Join(",", _tmp);

            if (_scope_identity)
                _3 = "SELECT SCOPE_IDENTITY();";

            return string.Format(_pattern, _0, _1, _2, _3);
        }
    }
}
