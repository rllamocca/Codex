/*
NET35
NET40
NET45
NETSTANDARD1_0
NETSTANDARD1_1
NETSTANDARD1_2
NETSTANDARD1_3
NETSTANDARD1_4
NETSTANDARD1_5
NETSTANDARD1_6
NETSTANDARD2_0
NETSTANDARD2_1
*/

using System;

namespace Codex
{
    public class Return
    {
        private readonly bool _SUCCESS;
        private readonly object _RESULT;
        private readonly bool _EXCEPTION;
        private readonly string _MESSAGE;

        public bool Success { get { return this._SUCCESS; } }
        public object Result { get { return this._RESULT; } }
        public bool Exception { get { return this._EXCEPTION; } }
        public string Message { get { return this._MESSAGE; } }

        public Return(bool _success, object _result = null)
        {
            this._SUCCESS = _success;
            this._RESULT = _result;

            if (this._RESULT is Exception _ex)
            {
                this._SUCCESS = false;
                this._EXCEPTION = true;
                this._MESSAGE = _ex.Message;
            }

            if (this._SUCCESS)
                this._MESSAGE = "Undefined";
        }
        public void TriggerError()
        {
            if (this.Success == false)
                throw new Exception(this.Message);
        }
        public void TriggerException()
        {
            if (this.Exception)
                throw (Exception)this.Result;
        }
        public void TriggerErrorException()
        {
            this.TriggerError();
            this.TriggerException();
        }

        public override string ToString()
        {
            return string.Format("Success: {0}, {1}", (this.Success ? "YES" : "NO"), this.Message);
        }
    }
}
