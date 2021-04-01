#if NET35
using Codex.Struct;
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
        public static DataSet To_DataSet(string _path, bool _xls = true, bool _columnnames = true, Tuple<int, int>[] _datetime = null)
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

                DataTable _table = new DataTable(_sheet.SheetName);

                int _ordinal = 0;
                IRow _row = _sheet.GetRow(_ordinal);
                DataColumn _dc = null;

                foreach (ICell _item in _row)
                {
                    Type _type = _datetime.Contains(new Tuple<int, int>(_i, _ordinal)) ? typeof(DateTime) : typeof(object);
                    if (_columnnames)
                        _dc = new DataColumn(_item.StringCellValue, _type);
                    else
                        _dc = new DataColumn("Column_" + Convert.ToString(_ordinal), _type);

                    _dc.DefaultValue = DBNull.Value;
                    _table.Columns.Add(_dc);

                    _ordinal++;
                }

                if (_columnnames)
                    _ordinal = 1;
                else
                    _ordinal = 0;

                for (int _j = _ordinal; _j < _sheet.PhysicalNumberOfRows; _j++)
                {
                    _row = _sheet.GetRow(_j);
                    DataRow _newrow = _table.NewRow();
                    _newrow.ItemArray = _table.Columns
                        .Cast<DataColumn>()
                        .Select(_s => _row.GetCell(_s.Ordinal).DBCellValue(_datetime.Contains(new Tuple<int, int>(_i, _s.Ordinal))))
                        .ToArray();
                    _table.Rows.Add(_newrow);
                }

                _return.Tables.Add(_table);
            }

            return _return;
        }
    }
}
