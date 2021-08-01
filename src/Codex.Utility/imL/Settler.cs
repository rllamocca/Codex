using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Codex.Utility
{
    public class Settler<T>
    {
        private PropertyInfo[] _PROPS;
        private string[] _KEYS;

        private void Init()
        {
            if (this._PROPS == null)
            {
                Type _type = typeof(T);
                this._PROPS = _type.GetProperties();
            }
        }
        private void Init2(DataRow _dr)
        {
            if (this._KEYS == null)
            {
                List<string> _tmp = new List<string>();
                foreach (DataColumn _item in _dr.Table.Columns)
                    _tmp.Add(_item.ColumnName);
                this._KEYS = _tmp.ToArray();
            }
        }
        private T CreateInstance()
        {
            this.Init();
            return Activator.CreateInstance<T>();
        }
        //private T FactoryInstance() where T : new()
        //{
        //    this.Init<T>();
        //    return new T();
        //}

        public T Instance(params object[] _values)
        {
            if (_values == null)
                return default;

            T _return = this.CreateInstance();

            for (int _i = 0; _i < _values.Length; _i++)
            {
                object _obj = _values[_i];
                PropertyInfo _prop = this._PROPS[_i];

                if (_prop.HasValue() && _prop.CanWrite)
                {
                    if (_obj.HasValue())
                        _prop.SetValue(_return, _obj, null);
                }
            }

            return _return;
        }
        public T Instance(params KeyValuePair<string, object>[] _values)
        {
            if (_values == null)
                return default;

            T _return = this.CreateInstance();

            foreach (KeyValuePair<string, object> _item in _values)
            {
                object _obj = _item.Value;
                PropertyInfo _prop = this._PROPS.Where(_w => _w.Name == _item.Key).FirstOrDefault();

                if (_prop.HasValue() && _prop.CanWrite)
                {
                    if (_obj.HasValue())
                        _prop.SetValue(_return, _obj, null);
                }
            }

            return _return;
        }
        public T Instance(DataRow _values, bool _byindex = false)
        {
            if (_values == null)
                return default;

            T _return = this.CreateInstance();

            if (_byindex)
                return this.Instance(_values.ItemArray);

            this.Init2(_values);

            foreach (string _item in this._KEYS)
            {
                object _obj = _values[_item];
                PropertyInfo _prop = this._PROPS.Where(_w => _w.Name == _item).FirstOrDefault();

                if (_prop.HasValue() && _prop.CanWrite)
                {
                    if (_obj.HasValue())
                        _prop.SetValue(_return, _obj, null);
                }
            }

            return _return;
        }
    }
}