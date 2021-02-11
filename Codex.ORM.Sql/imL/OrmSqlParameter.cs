using System;
using System.Data;
using System.Data.SqlClient;

namespace Codex.ORM.Sql
{
    public class OrmSqlParameter : IOrmParameter
    {
        public string Source { set; get; }
        public object Value { set; get; }
        public string Affect { set; get; }

        public SqlParameter Parameter { get; }

        public OrmSqlParameter(
            string _affect,
            string _function
            )
        {
            this.Source = _affect;
            this.Value = DBNull.Value;
            this.Affect = _function;

            this.Parameter = null;
        }
        public OrmSqlParameter(
            string _source,
            object _value,
            SqlDbType _dbtype,
            ParameterDirection _direction = ParameterDirection.Input
            )
        {
            this.Source = _source;
            this.Value = (_value == null) ? DBNull.Value : _value;
            this.Affect = string.Format("@_{0}_", this.Source);

            this.Parameter = new SqlParameter();
            this.Parameter.ParameterName = this.Affect;
            this.Parameter.Value = this.Value;
            this.Parameter.SqlDbType = _dbtype;
            this.Parameter.Direction = _direction;
        }
        public OrmSqlParameter(
            string _source,
            object _value,
            SqlDbType _dbtype,
            int _size,
            ParameterDirection _direction = ParameterDirection.Input
            ) : this(_source, _value, _dbtype, _direction)
        {
            this.Parameter.Size = _size;
        }
        public OrmSqlParameter(
            string _source,
            object _value,
            SqlDbType _dbtype,
            byte _precision,
            byte _scale,
            ParameterDirection _direction = ParameterDirection.Input
            ) : this(_source, _value, _dbtype, _direction)
        {
            this.Parameter.Precision = _precision;
            this.Parameter.Scale = _scale;
        }
    }
}
