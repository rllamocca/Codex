using Codex.Contract;

using MySql.Data.MySqlClient;

using System;
using System.Data;

namespace Codex.MySql.UserModel
{
    public class IMySqlParameter : IParameter
    {
        public string Source { set; get; }
        public object Value { set; get; }
        public string Affect { set; get; }

        public MySqlParameter Parameter { get; }

        public IMySqlParameter(
            string _affect,
            string _function
            )
        {
            this.Source = _affect;
            this.Value = DBNull.Value;
            this.Affect = _function;

            this.Parameter = null;
        }
        public IMySqlParameter(
            string _source,
            object _value,
            MySqlDbType _dbtype,
            ParameterDirection _direction = ParameterDirection.Input
            )
        {
            this.Source = _source;
            this.Value = (_value == null) ? DBNull.Value : _value;
            this.Affect = string.Format("@_{0}_", this.Source);

            this.Parameter = new MySqlParameter();
            this.Parameter.ParameterName = this.Affect;
            this.Parameter.Value = this.Value;
            this.Parameter.MySqlDbType = _dbtype;
            this.Parameter.Direction = _direction;
        }
        public IMySqlParameter(
            string _source,
            object _value,
            MySqlDbType _dbtype,
            int _size,
            ParameterDirection _direction = ParameterDirection.Input
            ) : this(_source, _value, _dbtype, _direction)
        {
            this.Parameter.Size = _size;
        }
        public IMySqlParameter(
            string _source,
            object _value,
            MySqlDbType _dbtype,
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
