namespace Codex.Extension
{
    public static class ReturnExtension
    {
        public static bool Success(this Return[] _array)
        {
            if (_array == null)
                return false;

            bool _return = true;
            foreach (Return _item in _array)
                _return = _return && _item.Success;
            return _return;
        }
        public static bool Exception(this Return[] _array)
        {
            if (_array == null)
                return false;

            bool _return = false;
            foreach (Return _item in _array)
                _return = _return || _item.Exception;
            return _return;
        }
        public static bool ErrorException(this Return[] _array)
        {
            if (_array == null)
                return false;

            bool _return = false;
            foreach (Return _item in _array)
                _return = _return || (_item.Success == false) || (_item.Exception == true);
            return _return;
        }
        public static void TriggerError(this Return[] _array)
        {
            if (_array == null)
                return;

            foreach (Return _cr in _array)
            {
                if (_cr.Success == false)
                {
                    _cr.TriggerError();
                    break;
                }
            }
        }
        public static void TriggerException(this Return[] _array)
        {
            if (_array == null)
                return;

            foreach (Return _item in _array)
            {
                if (_item.Exception == true)
                {
                    _item.TriggerErrorException();
                    break;
                }
            }
        }
        public static void TriggerErrorException(this Return[] _array)
        {
            if (_array == null)
                return;

            foreach (Return _item in _array)
            {
                if ((_item.Success == false) || (_item.Exception == true))
                {
                    _item.TriggerErrorException();
                    break;
                }
            }
        }
    }
}
