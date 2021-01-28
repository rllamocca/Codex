using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Codex.ORM.Generic;

namespace Codex.ORM.Extension
{
    public static class OrmParameterExtension
    {
        public static OrmSqlParameter[] CopyToMSQLSp(this OrmParameter[] _array)
        {
            if (_array == null) return null;

            OrmSqlParameter[] _return = new OrmSqlParameter[_array.Length];
            for (int _i = 0; _i < _array.Length; _i++)
                _return[_i] = (OrmSqlParameter)_array[_i];
            return _return;
        }
        public static SqlParameter[] Sql(this OrmParameter[] _array)
        {
            if (_array == null) return null;

            List<SqlParameter> _return = new List<SqlParameter>();
            foreach (OrmSqlParameter _a in _array)
                _return.Add(_a.Sql());
            return _return.ToArray();
        }

        public static UInt16 Retornos(this OrmParameter[] _array)
        {
            if (_array == null) return 0;

            UInt16 _return = 0;
            foreach (OrmParameter _pi in _array)
            {
                switch (_pi.Direccion)
                {
                    case ParameterDirection.Output:
                    case ParameterDirection.InputOutput:
                    case ParameterDirection.ReturnValue:
                        _return++;
                        break;
                    default:
                        break;
                }
            }
            return _return;
        }
        public static Boolean Retorno(this OrmParameter[] _array)
        {
            if (_array == null) return false;

            foreach (OrmParameter _pi in _array)
            {
                switch (_pi.Direccion)
                {
                    case ParameterDirection.ReturnValue:
                        return true;
                    default:
                        break;
                }
            }
            return false;
        }
    }
}
