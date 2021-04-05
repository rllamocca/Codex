using Codex.Extension;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Codex.Data
{
    public class Settler
    {
        private PropertyInfo[] _PROPS;
        private DataColumnCollection _DCS;

        private void Init<T>()
        {
            if (this._PROPS == null)
            {
                Type _type = typeof(T);
                this._PROPS = _type.GetProperties();
            }
        }
        private void Init2(DataRow _dr)
        {
            if (this._DCS == null)
                this._DCS = _dr.Table.Columns;
        }

        public T Populate<T>(params object[] _values)
        {
            if (_values == null)
                return default;

            this.Init<T>();
            T _return = Activator.CreateInstance<T>();

            for (int _i = 0; _i < _values.Length; _i++)
            {
                object _obj = _values[_i];
                PropertyInfo _prop = this._PROPS[_i];

                if (_obj.HasValue() && _prop.CanWrite)
                    _prop.SetValue(_return, _obj, null);
            }

            return _return;
        }

        public T Populate<T>(params KeyValuePair<string, object>[] _values)
        {
            if (_values == null)
                return default;

            this.Init<T>();
            T _return = Activator.CreateInstance<T>();

            foreach (KeyValuePair<string, object> _item in _values)
            {
                PropertyInfo _prop = this._PROPS.Where(_w => _w.Name == _item.Key).FirstOrDefault();

                if (_prop.HasValue())
                {
                    if (_item.Value.HasValue() && _prop.CanWrite)
                        _prop.SetValue(_return, _item.Value, null);
                }
            }

            return _return;
        }

        public T Populate<T>(DataRow _values, bool _byindex = false)
        {
            if (_values == null)
                return default;

            this.Init<T>();
            T _return = Activator.CreateInstance<T>();

            if (_byindex)
                return this.Populate<T>(_values.ItemArray);

            this.Init2(_values);

            foreach (DataColumn _item in this._DCS)
            {
                object _obj = _values[_item.ColumnName];
                PropertyInfo _prop = this._PROPS.Where(_w => _w.Name == _item.ColumnName).FirstOrDefault();

                if (_prop.HasValue())
                {
                    if (_obj.HasValue() && _prop.CanWrite)
                        _prop.SetValue(_return, _obj, null);
                }
            }

            return _return;
        }
    }
}
/*
        public static T Factory<T>() where T : new()
        {
            return new T();
        }
        public static T CreateInstance<T>(params object[] _args)
        {
            Type _type = typeof(T);
            //T _return = (T)Activator.CreateInstance(_type);
            T _return = (T)Activator.CreateInstance<T>();

            PropertyInfo[] _props = _type.GetProperties();

            for (int _i = 0; _i < _args.Length; _i++)
            {
                PropertyInfo _prop = _props[_i];
                object _obj = _args[_i];

                if (_obj.HasValue() && _prop.CanWrite)
                    _prop.SetValue(_return, _obj, null);
            }

            return _return;
        }
*/