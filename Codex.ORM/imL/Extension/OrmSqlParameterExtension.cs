using System.Data.SqlClient;

using Codex.ORM.Generic;

namespace Codex.ORM.Extension
{
    public static class OrmSqlParameterExtension
    {
        public static SqlParameter Sql(this OrmSqlParameter _item)
        {
            if (_item == null) return null;

            _item.Nombre = _item.Nombre.Trim();
            if (!_item.Nombre.Contains("@"))
                _item.Nombre = "@" + _item.Nombre;
            SqlParameter _pmt = new SqlParameter(_item.Nombre, _item.Tipo, _item.Largo)
            {
                Direction = _item.Direccion,
                Value = _item.Valor,
                Precision = _item.Precision,
                Scale = _item.Escala
            };
            return _pmt;
        }

        public static OrmSqlParameter[] To_Array(this OrmSqlParameter _item)
        {
            if (_item == null) return null;

            OrmSqlParameter[] _return = new OrmSqlParameter[1];
            _return[0] = _item;
            return _return;
        }

        //    public static String MSQLS_Where(this Codex_MSQLSp[] value)
        //    {
        //        String _return = String.Empty;
        //        if (value != null)
        //        {
        //            for (Int32 _i = 0; _i < value.Length; _i++)
        //            {
        //                _return += " ";
        //                if (value[_i].Mientras.Contains("{0}") && value[_i].Mientras.Contains("{1}") && value[_i].Mientras.Contains("{2}"))
        //                {
        //                    _return += String.Format(value[_i].Mientras, value[_i].Nombre, value[_i + 1].Nombre, value[_i + 2].Nombre);
        //                    _i += 2;
        //                    continue;
        //                }
        //                if (value[_i].Mientras.Contains("{0}") && value[_i].Mientras.Contains("{1}"))
        //                {
        //                    _return += String.Format(value[_i].Mientras, value[_i].Nombre, value[_i + 1].Nombre);
        //                    _i++;
        //                    continue;
        //                }
        //                if (value[_i].Mientras.Contains("{0}"))
        //                {
        //                    _return += String.Format(value[_i].Mientras, value[_i].Nombre);
        //                    continue;
        //                }
        //                _return += value[_i].Mientras;
        //            }
        //            _return = _return.Trim();
        //        }
        //        return _return;
        //    }
        //    public static String MSQLS_Where(this Codex_MSQLSp value)
        //    {
        //        String _return = String.Empty;
        //        if (value != null)
        //        {
        //            _return += String.Format(value.Mientras, value.Nombre, value.Nombre, value.Nombre);
        //            _return = _return.Trim();
        //        }
        //        return _return;
        //    }
    }
}
