﻿/*
NET35
NET40
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
        private readonly bool _SUC;
        private readonly object _RES = null;
        private readonly bool _EXC;
        private readonly string _MES = "NO PROCESADO";

        public bool Success { get { return this._SUC; } }
        public object Result { get { return this._RES; } }
        public bool Exception { get { return this._EXC; } }
        public string Message { get { return this._MES; } }

        public Return(bool _suc, object _res = null)
        {
            this._SUC = _suc;
            this._RES = _res;
            this._EXC = (this._RES is Exception);
            if (this._EXC)
                this._MES = ((Exception)this._RES).Message;
            else
            {
                if (this._SUC)
                    this._MES = "PROCESADO";
                else
                {
                    if (this._RES is string)
                        this._MES = (string)this._RES;
                }
            }
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
            return string.Format("Exito: {0}, {1}", (this.Success ? "SI" : "NO"), this.Message);
        }
    }
}
