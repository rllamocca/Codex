using System;

using Codex.Generic;

namespace Codex.Extension
{
    public static class ReturnExtension
    {
        public static Boolean Exito(this Return[] _array)
        {
            Boolean _return = true;
            foreach (Return _cr in _array)
                _return = _return && _cr.Exito;
            return _return;
        }
        public static Boolean Excepcion(this Return[] _array)
        {
            Boolean _return = false;
            foreach (Return _cr in _array)
                _return = _return || _cr.Excepcion;
            return _return;
        }
        public static Boolean ErrorExcepcion(this Return[] _array)
        {
            Boolean _return = false;
            foreach (Return _cr in _array)
                _return = _return || _cr.ErrorExcepcion;
            return _return;
        }
        public static void GatillarError(this Return[] _array)
        {
            foreach (Return _cr in _array)
            {
                if (_cr.Exito == false)
                {
                    _cr.GatillarError();
                    break;
                }
            }
        }
        public static void GatillarExcepcion(this Return[] _array)
        {
            foreach (Return _cr in _array)
            {
                if (_cr.Excepcion == true)
                {
                    _cr.GatillarErrorExcepcion();
                    break;
                }
            }
        }
        public static void GatillarErrorExcepcion(this Return[] _array)
        {
            foreach (Return _cr in _array)
            {
                if (_cr.ErrorExcepcion)
                {
                    _cr.GatillarErrorExcepcion();
                    break;
                }
            }
        }
    }
}
