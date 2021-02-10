using Codex.Generic;

namespace Codex.Extension
{
    public static class ReturnExtension
    {
        public static bool Exito(this Return[] _array)
        {
            bool _return = true;
            foreach (Return _cr in _array)
                _return = _return && _cr.Success;
            return _return;
        }
        public static bool Excepcion(this Return[] _array)
        {
            bool _return = false;
            foreach (Return _cr in _array)
                _return = _return || _cr.Exception;
            return _return;
        }
        public static bool ErrorExcepcion(this Return[] _array)
        {
            bool _return = false;
            foreach (Return _cr in _array)
                _return = _return || (_cr.Success == false) || (_cr.Exception == true);
            return _return;
        }
        public static void GatillarError(this Return[] _array)
        {
            foreach (Return _cr in _array)
            {
                if (_cr.Success == false)
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
                if (_cr.Exception == true)
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
                if ((_cr.Success == false) || (_cr.Exception == true))
                {
                    _cr.GatillarErrorExcepcion();
                    break;
                }
            }
        }
    }
}
