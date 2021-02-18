using Codex.Extension;
using Codex.Struct;

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
        public static void To_Excel(out Stream _out, DataSet _ds, bool _columnnames = true, bool _xls = true)
        {
            IWorkbook _wb;
            if (_xls) _wb = new HSSFWorkbook();
            else _wb = new XSSFWorkbook();

            IFont _fbasic = _wb.CreateFont();
            _fbasic.FontHeightInPoints = 10;
            _fbasic.FontName = "Consolas";

            IFont _fbold = _wb.CreateFont();
            _fbold.FontHeightInPoints = 12;
            _fbold.FontName = "Consolas";
            _fbold.IsBold = true;

            ICellStyle _sbasic = _wb.CreateCellStyle();
            _sbasic.Alignment = HorizontalAlignment.Left;
            _sbasic.VerticalAlignment = VerticalAlignment.Center;
            _sbasic.SetFont(_fbasic);

            ICellStyle _sbasic_number = _wb.CreateCellStyle();
            _sbasic_number.Alignment = HorizontalAlignment.Right;
            _sbasic_number.VerticalAlignment = VerticalAlignment.Center;
            _sbasic_number.SetFont(_fbasic);

            ICellStyle _sbold = _wb.CreateCellStyle();
            _sbold.Alignment = HorizontalAlignment.Center;
            _sbold.VerticalAlignment = VerticalAlignment.Center;
            _sbold.SetFont(_fbold);

            ICellStyle _stime = _wb.CreateCellStyle();
            _stime.Alignment = HorizontalAlignment.Right;
            _stime.VerticalAlignment = VerticalAlignment.Center;
            _stime.DataFormat = _wb.CreateDataFormat().GetFormat("H:mm:ss");
            _stime.SetFont(_fbasic);

            ICellStyle _stime_acum = _wb.CreateCellStyle();
            _stime_acum.Alignment = HorizontalAlignment.Right;
            _stime_acum.VerticalAlignment = VerticalAlignment.Center;
            _stime_acum.DataFormat = _wb.CreateDataFormat().GetFormat("[H]:mm:ss");
            _stime_acum.SetFont(_fbasic);

            ICellStyle _sdate = _wb.CreateCellStyle();
            _sdate.Alignment = HorizontalAlignment.Right;
            _sdate.VerticalAlignment = VerticalAlignment.Center;
            _sdate.DataFormat = _wb.CreateDataFormat().GetFormat("yyyyMMdd");
            _sdate.SetFont(_fbasic);

            ICellStyle _sdatetime = _wb.CreateCellStyle();
            _sdatetime.Alignment = HorizontalAlignment.Right;
            _sdatetime.VerticalAlignment = VerticalAlignment.Center;
            _sdatetime.DataFormat = _wb.CreateDataFormat().GetFormat("yyyyMMdd H:mm:ss");
            _sdatetime.SetFont(_fbasic);

            foreach (DataTable _item in _ds.Tables)
            {
                int _row = 0;
                int _col = 0;

                ISheet _wbs = _wb.CreateSheet(_item.TableName);
                _wbs.DefaultColumnWidth = 17;
                IRow _wbsr;

                if (_columnnames)
                {
                    _wbsr = _wbs.CreateRow(_row);
                    foreach (DataColumn _dc in _item.Columns)
                    {
                        ICell _wbsrc = _wbsr.CreateCell(_col);
                        _wbsrc.CellStyle = _sbold;
                        _wbsrc.SetCellType(CellType.String);

                        if (_dc.Caption == null)
                            _wbsrc.SetCellValue(_dc.ColumnName);
                        else
                            _wbsrc.SetCellValue(_dc.Caption);

                        _col++;
                    }
                    _row++;
                }

                foreach (DataRow _dr in _item.Rows)
                {
                    _col = 0;
                    _wbsr = _wbs.CreateRow(_row);
                    foreach (DataColumn _dc in _item.Columns)
                    {
                        ICell _wbsrc = _wbsr.CreateCell(_col);
                        object _obj = _dr[_dc.ColumnName];
                        if (_obj != DBNull.Value && _obj != null)
                        {
                            switch (Convert.ToString(_dc.DataType))
                            {
                                case "System.Int16":
                                case "System.Int32":
                                case "System.Int64":
                                case "System.UInt16":
                                case "System.UInt32":
                                case "System.UInt64":
                                case "System.Decimal":
                                    _wbsrc.CellStyle = _sbasic_number;
                                    _wbsrc.SetCellType(CellType.Numeric);
                                    _wbsrc.SetCellValue(Convert.ToDouble(_obj));
                                    break;
                                case "System.DateTime":
                                    DateTime _tmp = Convert.ToDateTime(_obj);

                                    if (_tmp.Excel18991231())
                                    {
                                        _wbsrc.CellStyle = _stime;
                                        _tmp = _tmp.ToExcelTime();
                                        _wbsrc.SetCellValue(_tmp.ToOADate());
                                    }
                                    else
                                    {
                                        if (_tmp.Year == 1900)
                                            _wbsrc.CellStyle = _stime_acum;
                                        else if (_tmp.IsDateTime())
                                            _wbsrc.CellStyle = _sdatetime;
                                        else
                                            _wbsrc.CellStyle = _sdate;

                                        _wbsrc.SetCellValue(_tmp);
                                    }
                                    break;
                                default:
                                    _wbsrc.CellStyle = _sbasic;
                                    _wbsrc.SetCellType(CellType.String);
                                    _wbsrc.SetCellValue(Convert.ToString(_obj));
                                    break;
                            }
                        }
                        _col++;
                    }
                    _row++;
                }

                //_col = 0;
                //foreach (DataColumn _dc in _item.Columns)
                //{
                //    _wbs.AutoSizeColumn(_col);
                //    _col++;
                //}
            }

            _out = new MemoryStream();
            _wb.Write(_out);
        }
        public static void To_Excel(out Stream _out, DataSet _ds, SArray3<string, string, string>[] _formats, bool _columnnames = true, bool _xls = true)
        {
            IWorkbook _wb;
            if (_xls) _wb = new HSSFWorkbook();
            else _wb = new XSSFWorkbook();

            IFont _fbasic = _wb.CreateFont();
            _fbasic.FontHeightInPoints = 10;
            _fbasic.FontName = "Consolas";

            IFont _fbold = _wb.CreateFont();
            _fbold.FontHeightInPoints = 12;
            _fbold.FontName = "Consolas";
            _fbold.IsBold = true;

            ICellStyle _sbasic = _wb.CreateCellStyle();
            _sbasic.Alignment = HorizontalAlignment.Left;
            _sbasic.VerticalAlignment = VerticalAlignment.Center;
            _sbasic.SetFont(_fbasic);

            ICellStyle _sbold = _wb.CreateCellStyle();
            _sbold.Alignment = HorizontalAlignment.Center;
            _sbold.VerticalAlignment = VerticalAlignment.Center;
            _sbold.SetFont(_fbold);

            foreach (DataTable _item in _ds.Tables)
            {
                int _row = 0;
                int _col = 0;

                ISheet _wbs = _wb.CreateSheet(_item.TableName);
                _wbs.DefaultColumnWidth = 17;
                IRow _wbsr;

                if (_columnnames)
                {
                    _wbsr = _wbs.CreateRow(_row);
                    foreach (DataColumn _dc in _item.Columns)
                    {
                        ICell _wbsrc = _wbsr.CreateCell(_col);
                        _wbsrc.CellStyle = _sbold;
                        _wbsrc.SetCellType(CellType.String);

                        if (_dc.Caption == null)
                            _wbsrc.SetCellValue(_dc.ColumnName);
                        else
                            _wbsrc.SetCellValue(_dc.Caption);

                        _col++;
                    }
                    _row++;
                }

                _col = 0;
                ICellStyle[] _t_styles = new ICellStyle[_item.Columns.Count];
                foreach (DataColumn _dc in _item.Columns)
                {
                    SArray3<string, string, string> _f = _formats.Where(_w => _w.A == _item.TableName && _w.B == _dc.ColumnName).FirstOrDefault();

                    if (_f.Equals(default(SArray3<string, string, string>)))
                        _t_styles[_col] = _sbasic;
                    else
                    {
                        _t_styles[_col] = _wb.CreateCellStyle();
                        _t_styles[_col].Alignment = HorizontalAlignment.Left;
                        _t_styles[_col].VerticalAlignment = VerticalAlignment.Center;
                        _t_styles[_col].DataFormat = _wb.CreateDataFormat().GetFormat(_f.Value);
                        _t_styles[_col].SetFont(_fbasic);
                    }

                    _col++;
                }

                foreach (DataRow _dr in _item.Rows)
                {
                    _col = 0;
                    _wbsr = _wbs.CreateRow(_row);
                    foreach (DataColumn _dc in _item.Columns)
                    {
                        ICell _wbsrc = _wbsr.CreateCell(_col);
                        _wbsrc.CellStyle = _t_styles[_col];
                        object _obj = _dr[_dc.ColumnName];
                        if (_obj != DBNull.Value && _obj != null)
                        {
                            switch (Convert.ToString(_dc.DataType))
                            {
                                case "System.Int16":
                                case "System.Int32":
                                case "System.Int64":
                                case "System.UInt16":
                                case "System.UInt32":
                                case "System.UInt64":
                                case "System.Decimal":
                                    _wbsrc.SetCellType(CellType.Numeric);
                                    _wbsrc.SetCellValue(Convert.ToDouble(_obj));
                                    break;
                                case "System.DateTime":
                                    DateTime _tmp = Convert.ToDateTime(_obj);

                                    if (_tmp.Excel18991231())
                                    {
                                        _tmp = _tmp.ToExcelTime();
                                        _wbsrc.SetCellValue(_tmp.ToOADate());
                                    }
                                    else
                                        _wbsrc.SetCellValue(_tmp);
                                    break;
                                default:
                                    _wbsrc.SetCellType(CellType.String);
                                    _wbsrc.SetCellValue(Convert.ToString(_obj));
                                    break;
                            }
                        }
                        _col++;
                    }
                    _row++;
                }
            }

            _out = new MemoryStream();
            _wb.Write(_out);
        }

        public static DataSet To_DataSet(string _path, bool _xls = true, bool _columnnames = true, SArray2<int, int>[] _datetime = null)
        {
            IWorkbook _wb;
            using (FileStream _s = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                if (_xls)
                    _wb = new HSSFWorkbook(_s);
                else
                    _wb = new XSSFWorkbook(_s);
            }
            if (_datetime == null)
                _datetime = new SArray2<int, int>[0];

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
                    Type _type = _datetime.Contains(new SArray2<int, int>(_i, _ordinal)) ? typeof(DateTime) : typeof(object);
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
                        .Select(_s => DBCellValue(_row.GetCell(_s.Ordinal), _datetime.Contains(new SArray2<int, int>(_i, _s.Ordinal))))
                        .ToArray();
                    _table.Rows.Add(_newrow);
                }

                _return.Tables.Add(_table);
            }

            return _return;
        }

        public static object CellValue(ICell _cell, bool _datetime = false)
        {
            object _return = null;

            switch (_cell.CellType)
            {
                case CellType.Unknown:
                    _return = Convert.ToString(_cell);
                    break;
                case CellType.Numeric:
                    if (_datetime)
                        _return = _cell.DateCellValue;
                    else
                        _return = _cell.NumericCellValue;
                    break;
                case CellType.String:
                    _return = _cell.StringCellValue;
                    break;
                case CellType.Formula:
                    break;
                case CellType.Blank:
                    break;
                case CellType.Boolean:
                    _return = _cell.BooleanCellValue;
                    break;
                case CellType.Error:
                    break;
                default:
                    break;
            }

            return _return;
        }
        public static object DBCellValue(ICell _cell, bool _datetime = false)
        {
            object _return = DBNull.Value;

            switch (_cell.CellType)
            {
                case CellType.Unknown:
                    _return = Convert.ToString(_cell);
                    break;
                case CellType.Numeric:
                    if (_datetime)
                        _return = _cell.DateCellValue;
                    else
                        _return = _cell.NumericCellValue;
                    break;
                case CellType.String:
                    _return = _cell.StringCellValue;
                    break;
                case CellType.Formula:
                    break;
                case CellType.Blank:
                    break;
                case CellType.Boolean:
                    _return = _cell.BooleanCellValue;
                    break;
                case CellType.Error:
                    break;
                default:
                    break;
            }

            return _return;
        }
    }
}
