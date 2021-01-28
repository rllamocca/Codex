using System;
using System.Collections.Generic;

using Codex.ORM.Enum;
using Codex.ORM.Generic;

namespace Codex.ORM.Extension
{
    public static class FilterExtension
    {
        public static String[] Into_Values(this Filter[] _array)
        {
            if (_array == null) return new String[] { "", "" };

            List<String> _return0 = new List<String>();
            List<String> _return1 = new List<String>();
            foreach (Filter _a in _array)
            {
                if (_a.Columna.Identidad == false)
                {
                    _return0.Add(String.Format("[{0}]", _a.Columna.Nombre));
                    if (_a.Parametros == null || _a.Parametros[0] == null) _return1.Add(_a.Columna.Calculado);
                    else _return1.Add(String.Format(_a.Columna.Calculado, _a.Parametros[0].Nombre));
                }
            }
            return new String[] { String.Join(",", _return0.ToArray()), String.Join(",", _return1.ToArray()) };
        }

        public static String WhereKeys(this Filter[] _array)
        {
            if (_array == null) return "";
            List<String> _return = new List<String>();
            foreach (Filter _a in _array)
            {
                if (_a.Columna.LlavePr)
                    _return.Add(String.Format("[{0}]={1}", _a.Columna.Nombre, _a.Parametros[0].Nombre));
            }
            return String.Join(" AND ", _return.ToArray());
        }
        public static String[] Set_WhereKeys(this Filter[] _array)
        {
            if (_array == null) return new String[] { "", "" };

            List<String> _return0 = new List<String>();
            List<String> _return1 = new List<String>();
            foreach (Filter _a in _array)
            {
                if (_a.Columna.Identidad == false)
                    _return0.Add(String.Format("[{0}]={1}", _a.Columna, _a.Parametros[0].Nombre));
                if (_a.Columna.LlavePr)
                    _return1.Add(String.Format("[{0}]={1}", _a.Columna, _a.Parametros[0].Nombre));
            }
            return new String[] { String.Join(",", _return0.ToArray()), String.Join(",", _return1.ToArray()) };
        }

        public static String Select(this Filter[] _array)
        {
            if (_array == null) return "*";

            List<String> _return0 = new List<String>();
            foreach (Filter _a in _array)
            {
                if (_a.Columna.Alias.Length == 0) _return0.Add(String.Format("[{0}]", _a.Columna.Nombre));
            }
            return String.Join(",", _return0.ToArray());
        }
        public static String Where(this Filter[] _array)
        {
            if (_array == null) return "";

            List<String> _return1 = new List<String>();
            foreach (Filter _a in _array)
            {
                if (_a.Comprobacion.Comprobacion != ECheck.None)
                {
                    String _f = _a.Comprobacion.Formato();
                    List<String> _ps = new List<String>();
                    _ps.Add(_a.Columna.Nombre);
                    foreach (OrmParameter _p in _a.Parametros)
                        _ps.Add(_p.Nombre);
                    _return1.Add(String.Format(_f, _ps.ToArray()));
                }
            }
            return String.Join(",", _return1.ToArray());
        }
        public static String[] Select_Where(this Filter[] _array)
        {
            if (_array == null) return new String[] { "*", "" };

            List<String> _return0 = new List<String>();
            List<String> _return1 = new List<String>();

            foreach (Filter _a in _array)
            {
                if (_a.Columna.Alias.Length == 0) _return0.Add(String.Format("[{0}]", _a.Columna.Nombre));
                else _return0.Add(String.Format("[{0}] AS '{1}'", _a.Columna.Nombre, _a.Columna.Alias));
                if (_a.Comprobacion.Comprobacion != ECheck.None)
                {
                    String _f = _a.Comprobacion.Formato();
                    List<String> _ps = new List<String>();
                    _ps.Add(_a.Columna.Nombre);
                    foreach (OrmParameter _p in _a.Parametros)
                        _ps.Add(_p.Nombre);
                    _return1.Add(String.Format(_f, _ps.ToArray()));
                }
            }
            return new String[] { String.Join(",", _return0.ToArray()), String.Join(" AND ", _return1.ToArray()) };
        }
        public static String[] Select_WhereKeys(this Filter[] _array)
        {
            if (_array == null) return new String[] { "*", "" };

            List<String> _return0 = new List<String>();
            List<String> _return1 = new List<String>();

            foreach (Filter _a in _array)
            {
                if (_a.Columna.Alias.Length == 0) _return0.Add(String.Format("[{0}]", _a.Columna.Nombre));
                else _return0.Add(String.Format("[{0}] AS '{1}'", _a.Columna.Nombre, _a.Columna.Alias));
                if (_a.Columna.LlavePr)
                    _return1.Add(String.Format("[{0}]={1}", _a.Columna.Nombre, _a.Parametros[0].Nombre));
            }
            return new String[] { String.Join(",", _return0.ToArray()), String.Join(" AND ", _return1.ToArray()) };
        }
        public static String[] Select_Where_GroupBy_OrderBy(this Filter[] _array)
        {
            if (_array == null) return new String[] { "*", "", "", "" };

            List<String> _return0 = new List<String>();
            List<String> _return1 = new List<String>();
            List<String> _return2 = new List<String>();
            List<String> _return3 = new List<String>();

            foreach (Filter _a in _array)
            {
                if (_a.Columna.Alias.Length == 0) _return0.Add(String.Format("[{0}]", _a.Columna.Nombre));
                else _return0.Add(String.Format("[{0}] AS '{1}'", _a.Columna.Nombre, _a.Columna.Alias));
                if (_a.Comprobacion.Comprobacion != ECheck.None)
                {
                    String _f = _a.Comprobacion.Formato();
                    List<String> _ps = new List<String>();
                    _ps.Add(_a.Columna.Nombre);
                    foreach (OrmParameter _p in _a.Parametros)
                        _ps.Add(_p.Nombre);
                    _return1.Add(String.Format(_f, _ps.ToArray()));
                }
                if (_a.Columna.Grupo) _return2.Add(String.Format("[{0}]", _a.Columna.Nombre));
                if (_a.Columna.Orden != EOrder.None)
                {
                    String _f = String.Empty;
                    switch (_a.Columna.Orden)
                    {
                        case EOrder.None:
                            break;
                        case EOrder.Ascendente:
                            _f = "[{0}] ASC";
                            break;
                        case EOrder.Descendente:
                            _f = "[{0}] DESC";
                            break;
                        default:
                            break;
                    }
                    _return3.Add(String.Format(_f, _a.Columna.Nombre));
                }
            }
            return new String[] { String.Join(",", _return0.ToArray()), String.Join(" AND ", _return1.ToArray()), String.Join(",", _return2.ToArray()), String.Join(",", _return3.ToArray()) };
        }

        public static OrmParameter[] Parametros(this Filter[] _array)
        {
            if (_array == null) return null;

            List<OrmParameter> _return = new List<OrmParameter>();
            foreach (Filter _a in _array)
            {
                if (_a.Parametros != null)
                {
                    foreach (OrmParameter _b in _a.Parametros)
                        _return.Add(_b);
                }
            }
            return _return.ToArray();
        }


        public static Filter[] Llaves(this Filter[] _array)
        {
            List<Filter> _return = new List<Filter>();
            foreach (Filter _a in _array)
                if (_a.Columna.LlavePr) _return.Add(_a);
            return _return.ToArray();
        }
    }
}
