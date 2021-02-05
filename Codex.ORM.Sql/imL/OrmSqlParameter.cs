using System;
using System.Data;
using System.Data.SqlClient;

namespace Codex.ORM.Sql
{
    public class OrmSqlParameter : IOrmParameter
    {
        public string Affect { set; get; }
        public string Name_Function { set; get; }

        public SqlParameter Parameter { get; }

        public OrmSqlParameter(
            string _affect,
            string _function
            )
        {
            this.Affect = _affect;
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
            this.Name_Function = string.Format("@_{0}_", this.Affect);

            SqlParameter _return = new SqlParameter();
            _return.Value = DBNull.Value;

            _return.ParameterName = this.Name_Function;
            if (_value != null)
                _return.Value = _value;
            _return.SqlDbType = _dbtype;
            _return.Direction = _direction;

            this.Parameter = _return;
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
