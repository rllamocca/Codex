/*
ESTO NO DEBERIA ESTAR AQUI

namespace Codex.Helper
{
    public static class StringHelper
    {
        public static String Clean_Sql(String _a)
        {
            if (_a == null) return null;

            while (_a.Contains("[["))
            {
                _a = _a.Replace("[[", "[");
            }
            while (_a.Contains("]]"))
            {
                _a = _a.Replace("]]", "]");
            }
            _a = StringHelper.Clean_TBLFCR(_a);
            return _a.Trim();
        }


        private static String Formato_Rut(String _a)
        {
            if (_a == null) return null;
            _a = _a.Replace(".", "").Replace("-", "");
            if (_a.Length == 0) return null;

            List<Char> _l = new List<Char>();
            List<Char> _lr = _a.Reverse().ToList();
            for (Int32 _n = 0; _n < _lr.Count; ++_n)
            {
                _l.Add(_lr[_n]);
                if (_n == 0)
                    _l.Add('-');
                else if (_n % 3 == 0 && _n != _lr.Count - 1)
                    _l.Add('.');
            }
            _l.Reverse();
            return String.Join("", _l);
        }

        public static Task Write(ref Stream _a, string _path, CancellationToken _token = default)
        {
            return Write(_a, _path, _token);
        }
    }
}
*/