using System;
using System.Data;
using System.Data.SqlClient;

using Codex.ORM.Enum;

namespace Codex.ORM.Generic
{
    public class OrmSqlParameter : OrmParameter
    {
        public SqlDbType Tipo { set; get; } = SqlDbType.NVarChar;
        public OrmSqlParameter(ECheck _c = ECheck.None,
            ParameterDirection _d = ParameterDirection.Input,
            SqlDbType _t = SqlDbType.NVarChar,
            String _n = "",
            Object _v = null,
            Int32 _l = 32,
            Byte _p = 0,
            Byte _e = 0)
            : base(_c, _d, _n, _v, _l, _p, _e)
        {
            this.Tipo = _t;
        }

        public OrmSqlParameter(SqlParameter _p)
        {
            base.Direccion = _p.Direction;
            this.Tipo = _p.SqlDbType;
            base.Nombre = _p.ParameterName;
            base.Valor = _p.Value;
            base.Largo = _p.Size;
            base.Escala = _p.Scale;
        }

        public new void Dispose()
        {
            this.Tipo = SqlDbType.NVarChar;
            base.Dispose();
        }
    }
}
