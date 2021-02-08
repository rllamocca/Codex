using System;
using System.Data;
using System.Data.SqlClient;

namespace Codex.ORM.Sql
{
    public class OrmSqlParameter : IOrmParameter
    {
        public string Affect { set; get; }
        public object Value { set; get; }
        public string Name_Function { set; get; }

        public SqlParameter Parameter { get; }

        public OrmSqlParameter(
            string _affect,
            string _function
            )
        {
            this.Affect = _affect;
            this.Value = DBNull.Value;
            this.Name_Function = _function;

            this.Parameter = null;
        }
        public OrmSqlParameter(
            string _affect,
            object _value,
            SqlDbType _dbtype,
            ParameterDirection _direction = ParameterDirection.Input
            )
        {
            this.Affect = _affect;
            this.Value = (_value == null) ? DBNull.Value : _value;
            this.Name_Function = string.Format("@_{0}_", this.Affect);

            this.Parameter = new SqlParameter();
            this.Parameter.ParameterName = this.Name_Function;
            this.Parameter.Value = _value;
            this.Parameter.SqlDbType = _dbtype;
            this.Parameter.Direction = _direction;
        }
        public OrmSqlParameter(
            string _affect,
            object _value,
            SqlDbType _dbtype,
            int _size,
            ParameterDirection _direction = ParameterDirection.Input
            ) : this(_affect, _value, _dbtype, _direction)
        {
            this.Parameter.Size = _size;
        }
        public OrmSqlParameter(
            string _affect,
            object _value,
            SqlDbType _dbtype,
            byte _precision,
            byte _scale,
            ParameterDirection _direction = ParameterDirection.Input
            ) : this(_affect, _value, _dbtype, _direction)
        {
            this.Parameter.Precision = _precision;
            this.Parameter.Scale = _scale;
        }
    }
}
