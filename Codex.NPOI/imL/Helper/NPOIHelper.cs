#if NET35
using Codex.Struct;
#endif
#if (NET35 || NET40)
using Codex.Contract;
#endif

using Codex.NPOI.Extension;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using System;
using System.Data;
using System.IO;
using System.Linq;

namespace Codex.NPOI.Helper
{
    public static class NPOIHelper
    {
        public static DataSet To_DataSet(string _path,
            bool _columnnames = true,
            bool _xls = true,
            Tuple<int, int>[] _datetime = null,
            IProgress<int> _dt_progress = null,
            IProgress<int> _progress = null)
        {
            IWorkbook _wb;
            using (FileStream _s = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                if (_xls)
                    _wb = new HSSFWorkbook(_s);
                else
                    _wb = new XSSFWorkbook(_s);
            }

#if NETSTANDARD2_0
            if (_datetime == null)
                _datetime = Array.Empty<Tuple<int, int>>();
#else
            if (_datetime == null)
                _datetime = new Tuple<int, int>[0];
#endif

            DataSet _return = new DataSet("NPOI");

            for (int _i = 0; _i < _wb.NumberOfSheets; _i++)
            {
                ISheet _sheet = _wb.GetSheetAt(_i);

                DataTable _dt = new DataTable(_sheet.SheetName);

                int _init = 0;
                IRow _row = _sheet.GetRow(_init);
                DataColumn _dc = null;

                foreach (ICell _item in _row)
                {
                    Type _type = _datetime.Contains(new Tuple<int, int>(_i, _init)) ? typeof(DateTime) : typeof(object);
                    if (_columnnames)
                        _dc = new DataColumn(_item.StringCellValue, _type);
                    else
                        _dc = new DataColumn("Column_" + Convert.ToString(_init), _type);

                    _dc.DefaultValue = DBNull.Value;
                    _dt.Columns.Add(_dc);

                    _init++;
                }

                if (_columnnames)
                    _init = 1;
                else
                    _init = 0;

                for (int _j = _init; _j < _sheet.PhysicalNumberOfRows; _j++)
                {
                    _row = _sheet.GetRow(_j);
                    DataRow _new = _dt.NewRow();
                    _new.ItemArray = _dt.Columns
                        .Cast<DataColumn>()
                        .Select(_s => _row.GetCell(_s.Ordinal).DBCellValue(_datetime.Contains(new Tuple<int, int>(_i, _s.Ordinal))))
                        .ToArray();
                    _dt.Rows.Add(_new);

                    _progress?.Report(0);
                }

                _return.Tables.Add(_dt);
                _dt_progress?.Report(0);
            }

            return _return;
        }
    }
}
