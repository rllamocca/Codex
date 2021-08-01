#if (NET35 || NET40)
using Codex.Contract;
#endif

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Codex.Utility
{
    public static class DataTableExtension
    {
        public static void To_Plain(this DataTable _this,
            string _path,
            char _separator = '\0',
            bool _columnnames = true,
            Encoding _enc = null,
            IProgress<int> _progress = null)
        {
            _this.To_Plain(out Stream _out, _separator, _columnnames, _enc, _progress);
            _out.FileCreate(_path);
            _out.Dispose();
        }

        public static void To_Plain(this DataTable _this,
            out Stream _out,
            char _separator = '\0',
            bool _columnnames = true,
            Encoding _enc = null,
            IProgress<int> _progress = null)
        {
            EncodingUtility.SolutionDefault(ref _enc);

            string _sep = Convert.ToString(_separator);

            _out = new MemoryStream();
            StreamWriter _sw = new StreamWriter(_out, _enc);
            bool _fw = false;

            if (_columnnames)
            {
                List<string> _line = new List<string>();
                foreach (DataColumn _item in _this.Columns)
                    _line.Add(_item.Caption ?? _item.ColumnName);
#if (NET35)
                _sw.Write(string.Join(_sep, _line.ToArray()));
#else
                _sw.Write(string.Join(_sep, _line));
#endif
                _fw = true;
            }

            foreach (DataRow _item in _this.Rows)
            {
                if (_fw)
                    _sw.WriteLine();
                _sw.Write(string.Join(_sep, _item.ItemArray.DBToString()));

                if (_fw == false)
                    _fw = true;

                _progress?.Report(0);
            }

            _sw.Flush();
        }
    }
}
