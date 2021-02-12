using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;


namespace Codex.NPOI.Helper
{
    class NPOIHelper
    {
        public static void To_xls(out Stream _out, DataSet _a, Boolean _b = true, KeyValuePair<String, String>[] _c = null)
        {
            IWorkbook _wb;
            if (_b) _wb = new HSSFWorkbook();
            else _wb = new XSSFWorkbook();

            IFont _font = _wb.CreateFont();
            _font.FontHeightInPoints = 10;
            _font.FontName = "Consolas";

            IFont _font2 = _wb.CreateFont();
            _font2.FontHeightInPoints = 11;
            _font2.FontName = "Consolas";
            _font2.IsBold = true;

            ICellStyle _style = _wb.CreateCellStyle();
            _style.VerticalAlignment = VerticalAlignment.Center;
            _style.SetFont(_font);

            ICellStyle _style2 = _wb.CreateCellStyle();
            _style2.Alignment = HorizontalAlignment.Center;
            _style2.VerticalAlignment = VerticalAlignment.Center;
            _style2.SetFont(_font2);

            ICellStyle _s_time = _wb.CreateCellStyle();
            _s_time.VerticalAlignment = VerticalAlignment.Center;
            _s_time.DataFormat = _wb.CreateDataFormat().GetFormat("H:mm:ss");
            _s_time.SetFont(_font);

            ICellStyle _s_time_acum = _wb.CreateCellStyle();
            _s_time_acum.VerticalAlignment = VerticalAlignment.Center;
            _s_time_acum.DataFormat = _wb.CreateDataFormat().GetFormat("[H]:mm:ss");
            _s_time_acum.SetFont(_font);

            ICellStyle _s_date = _wb.CreateCellStyle();
            _s_date.VerticalAlignment = VerticalAlignment.Center;
            _s_date.DataFormat = _wb.CreateDataFormat().GetFormat("yyyyMMdd");
            _s_date.SetFont(_font);

            ICellStyle _s_datetime = _wb.CreateCellStyle();
            _s_datetime.VerticalAlignment = VerticalAlignment.Center;
            _s_datetime.DataFormat = _wb.CreateDataFormat().GetFormat("yyyyMMdd H:mm:ss");
            _s_datetime.SetFont(_font);

            if (_c == null)
            {
                foreach (DataTable _item in _a.Tables)
                {
                    Int32 _row = 0;
                    Int32 _col = 0;

                    ISheet _wbs = _wb.CreateSheet(_item.TableName);
                    _wbs.DefaultColumnWidth = 17;
                    IRow _wbsr = _wbs.CreateRow(_row);

                    foreach (DataColumn _dc in _item.Columns)
                    {
                        ICell _wbsrc = _wbsr.CreateCell(_col);
                        _wbsrc.CellStyle = _style2;
                        _wbsrc.SetCellType(CellType.String);

                        if (_dc.Caption == null) _wbsrc.SetCellValue(_dc.ColumnName);
                        else _wbsrc.SetCellValue(_dc.Caption);

                        _col++;
                    }
                    _row++;

                    foreach (DataRow _dr in _item.Rows)
                    {
                        _col = 0;
                        _wbsr = _wbs.CreateRow(_row);
                        foreach (DataColumn _dc in _item.Columns)
                        {
                            ICell _wbsrc = _wbsr.CreateCell(_col);
                            Object _obj = _dr[_dc.ColumnName];
                            if (_obj != DBNull.Value)
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
                                        _wbsrc.CellStyle = _style;
                                        _wbsrc.SetCellType(CellType.Numeric);
                                        _wbsrc.SetCellValue(Convert.ToDouble(_obj));
                                        break;
                                    case "System.DateTime":
                                        DateTime _tmp = Convert.ToDateTime(_obj);

                                        if (_tmp.Year == 1899 && _tmp.Month == 12 && _tmp.Day == 31)
                                        {
                                            TimeSpan _tmp2 = _tmp - new DateTime(1899, 12, 31);
                                            _tmp = new DateTime(_tmp2.Ticks);
                                            _wbsrc.CellStyle = _s_time;
                                            _wbsrc.SetCellValue(_tmp.ToOADate());
                                        }
                                        else
                                        {
                                            if (_tmp.Year == 1900)
                                                _wbsrc.CellStyle = _s_time_acum;
                                            else if (_tmp.Hour > 0 || _tmp.Minute > 0 || _tmp.Second > 0 || _tmp.Millisecond > 0)
                                                _wbsrc.CellStyle = _s_datetime;
                                            else
                                                _wbsrc.CellStyle = _s_date;

                                            _wbsrc.SetCellValue(_tmp);
                                        }
                                        break;
                                    default:
                                        _wbsrc.CellStyle = _style;
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
            }
            else
            {
                foreach (DataTable _item in _a.Tables)
                {
                    Int32 _row = 0;
                    Int32 _col = 0;

                    ISheet _wbs = _wb.CreateSheet(_item.TableName);
                    _wbs.DefaultColumnWidth = 17;
                    IRow _wbsr = _wbs.CreateRow(_row);

                    ICellStyle[] _t_styles = new ICellStyle[_item.Columns.Count];

                    foreach (DataColumn _dc in _item.Columns)
                    {
                        ICell _wbsrc = _wbsr.CreateCell(_col);
                        _wbsrc.CellStyle = _style2;
                        _wbsrc.SetCellType(CellType.String);

                        if (_dc.Caption == null) _wbsrc.SetCellValue(_dc.ColumnName);
                        else _wbsrc.SetCellValue(_dc.Caption);

                        KeyValuePair<String, String> _f = _c.Where(_w => _w.Key == _dc.ColumnName).FirstOrDefault();

                        if (_f.Equals(default(KeyValuePair<String, String>)))
                            _t_styles[_col] = _style;
                        else
                        {
                            _t_styles[_col] = _wb.CreateCellStyle();
                            _t_styles[_col].VerticalAlignment = VerticalAlignment.Center;
                            _t_styles[_col].DataFormat = _wb.CreateDataFormat().GetFormat(_f.Value);
                            _t_styles[_col].SetFont(_font);
                        }

                        _col++;
                    }
                    _row++;

                    foreach (DataRow _dr in _item.Rows)
                    {
                        _col = 0;
                        _wbsr = _wbs.CreateRow(_row);
                        foreach (DataColumn _dc in _item.Columns)
                        {
                            ICell _wbsrc = _wbsr.CreateCell(_col);
                            _wbsrc.CellStyle = _t_styles[_col];
                            Object _obj = _dr[_dc.ColumnName];

                            if (_obj != DBNull.Value)
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

                                        if (_tmp.Year == 1899 && _tmp.Month == 12 && _tmp.Day == 31)
                                        {
                                            TimeSpan _tmp2 = _tmp - new DateTime(1899, 12, 31);
                                            _tmp = new DateTime(_tmp2.Ticks);
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
            }

            _out = new MemoryStream();
            _wb.Write(_out);
        }

        public static DataTable Xls_to_DataTable(String _path, Boolean _b = true)
        {
            IWorkbook _wb;
            using (FileStream _s = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                if (_b)
                    _wb = new HSSFWorkbook(_s);
                else
                    _wb = new HSSFWorkbook(_s);
            }
            ISheet _sheet = _wb.GetSheetAt(0);
            DataTable _return = new DataTable(_sheet.SheetName);

            IRow _row = _sheet.GetRow(0);
            foreach (ICell _cell in _row)
            {
                _return.Columns.Add(_cell.ToString());
            }

            // write the rest
            for (int _i = 1; _i < _sheet.PhysicalNumberOfRows; _i++)
            {
                var sheetRow = _sheet.GetRow(_i);
                var dtRow = _return.NewRow();
                dtRow.ItemArray = _return.Columns
                    .Cast<DataColumn>()
                    .Select(c => sheetRow.GetCell(c.Ordinal, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString())
                    .ToArray();
                _return.Rows.Add(dtRow);
            }

            return _return;
        }
    }
}
