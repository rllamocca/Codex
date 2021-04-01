using NPOI.SS.UserModel;

using System;

namespace Codex.NPOI.Extension
{
    public static class NPOIExtension
    {
        public static object DBCellValue(this ICell _this, bool _datetime = false, bool _dbnull = true)
        {
            object _return;
            if (_dbnull)
                _return = DBNull.Value;
            else
                _return = null;

            if (_this == null)
                return _return;

            switch (_this.CellType)
            {
                case CellType.Unknown:
                    _return = Convert.ToString(_this);
                    break;
                case CellType.Numeric:
                    if (_datetime)
                        _return = _this.DateCellValue;
                    else
                        _return = _this.NumericCellValue;
                    break;
                case CellType.String:
                    _return = _this.StringCellValue;
                    break;
                case CellType.Formula:
                    break;
                case CellType.Blank:
                    break;
                case CellType.Boolean:
                    _return = _this.BooleanCellValue;
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
