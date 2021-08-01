#if (NET35 || NET40)
using Codex.Contract;
#endif

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Codex.Utility.NPOI
{
    public static class NPOIHelper
    {
        private static IWorkbook OpenRead(string _path, bool _xls = true)
        {
            using (FileStream _s = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                if (_xls)
                    return new HSSFWorkbook(_s);
                else
                    return new XSSFWorkbook(_s);
            }
        }

        public static DataTable To_DataTable(string _path,
            int _isheet = 0,
            bool _columnnames = true,
            bool _xls = true,
            IProgress<int> _progress = null)
        {
            IWorkbook _wb = OpenRead(_path, _xls);
            ISheet _sheet = _wb.GetSheetAt(_isheet);
            Type _type = typeof(object);
            int _init = 0;
            IRow _row = _sheet.GetRow(_init);

            DataTable _return = new DataTable(_sheet.SheetName);

            foreach (ICell _item in _row)
            {
                DataColumn _dc = null;
                if (_columnnames)
                    _dc = new DataColumn(_item.StringCellValue, _type);
                else
                    _dc = new DataColumn("Column_" + Convert.ToString(_init), _type);

                _dc.DefaultValue = DBNull.Value;
                _return.Columns.Add(_dc);

                _init++;
            }

            if (_columnnames)
                _init = 1;
            else
                _init = 0;

            for (int _j = _init; _j < _sheet.PhysicalNumberOfRows; _j++)
            {
                _row = _sheet.GetRow(_j);
                DataRow _new = _return.NewRow();
                _new.ItemArray = _return.Columns
                    .Cast<DataColumn>()
                    .Select(_s => _row.GetCell(_s.Ordinal).DBCellValue())
                    .ToArray();
                _return.Rows.Add(_new);

                _progress?.Report(0);
            }

            return _return;
        }

        public static DataSet To_DataSet(string _path,
            bool _columnnames = true,
            bool _xls = true,
            IProgress<int> _dt_progress = null,
            IProgress<int> _progress = null)
        {
            IWorkbook _wb = OpenRead(_path, _xls);
            Type _type = typeof(object);

            DataSet _return = new DataSet("NPOI");

            for (int _i = 0; _i < _wb.NumberOfSheets; _i++)
            {
                ISheet _sheet = _wb.GetSheetAt(_i);
                DataTable _dt = new DataTable(_sheet.SheetName);
                int _init = 0;
                IRow _row = _sheet.GetRow(_init);

                foreach (ICell _item in _row)
                {
                    DataColumn _dc = null;
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
                        .Select(_s => _row.GetCell(_s.Ordinal).DBCellValue())
                        .ToArray();
                    _dt.Rows.Add(_new);

                    _progress?.Report(0);
                }

                _return.Tables.Add(_dt);
                _dt_progress?.Report(0);
            }

            return _return;
        }

        public static T[] To_Generic<T>(string _path,
            int _isheet = 0,
            bool _columnnames = true,
            bool _xls = true,
            IProgress<int> _progress = null)
        {
            IWorkbook _wb = OpenRead(_path, _xls);
            ISheet _sheet = _wb.GetSheetAt(_isheet);
            Settler<T> _set = new Settler<T>();
            int _init = 0;

            if (_columnnames)
                _init++;

            List<T> _return = new List<T>();

            for (int _i = _init; _i < _sheet.PhysicalNumberOfRows; _i++)
            {
                IRow _row = _sheet.GetRow(_i);
                object[] _tmp = new object[_row.LastCellNum];

                for (int _j = 0; _j < _row.LastCellNum; _j++)
                    _tmp[_j] = _row.GetCell(_j).DBCellValue(false);

                _return.Add(_set.Instance(_tmp));

                _progress?.Report(0);
            }

            return _return.ToArray();
        }
    }
}
