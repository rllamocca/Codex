using System;
using System.Data;
using System.Data.SqlClient;

namespace Codex.ORM.Sql
{
    public static class FSqlParameter
    {
        public static SqlParameter Factory(
            string _name,
            object _value,
            SqlDbType _dbtype,
            ParameterDirection _direction = ParameterDirection.Input
            )
        {
            SqlParameter _return = new SqlParameter();
            _return.Value = DBNull.Value;

            _return.ParameterName = _name;
            if (_value != null)
                _return.Value = _value;
            _return.SqlDbType = _dbtype;
            _return.Direction = _direction;

            return _return;
        }

        public static SqlParameter Factory(
            string _name,
            object _value,
            SqlDbType _dbtype,
            int _size,
            ParameterDirection _direction = ParameterDirection.Input
            )
        {
            SqlParameter _return = FSqlParameter.Factory(_name, _value, _dbtype, _direction);
            _return.Size = _size;

            return _return;
        }

        public static SqlParameter Factory(
            string _name,
            object _value,
            SqlDbType _dbtype,
            byte _precision,
            byte _scale,
            ParameterDirection _direction = ParameterDirection.Input
            )
        {
            SqlParameter _return = FSqlParameter.Factory(_name, _value, _dbtype, _direction);
            _return.Precision = _precision;
            _return.Scale = _scale;

            return _return;
        }
    }
}
