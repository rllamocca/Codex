﻿using System;

namespace Codex.Generic
{
    public class Return
    {
        private readonly bool _EXI;
        private readonly object _RET = null;
        private readonly bool _EXC = false;
        private readonly string _MEN = "NO PROCESADO";

        public bool Success { get { return this._EXI; } }
        public object Result { get { return this._RET; } }
        public bool Exception { get { return this._EXC; } }

        public string Message { get { return this._MEN; } }

        public Return(bool _exito, object _resultado = null)
        {
            this._RET = _resultado;
            this._EXC = (this._RET is Exception);
            if (this._EXC) this._MEN = ((Exception)this._RET).Message;
            else
            {
                if (_exito)
                {
                    this._MEN = "PROCESADO";
                    this._EXI = true;
                }
                else
                {
                    if (this._RET is string)
                        this._MEN = (string)this._RET;
                }
            }
        }
        public void GatillarError()
        {
            if (this.Success == false)
                throw new Exception(this.Message);
        }
        public void GatillarExcepcion()
        {
            if (this.Exception)
                throw (Exception)this.Result;
        }
        public void GatillarErrorExcepcion()
        {
            this.GatillarError();
            this.GatillarExcepcion();
        }

        public override string ToString()
        {
            return string.Format("Exito: {0}, {1}", (this.Success ? "SI" : "NO"), this.Message);
        }
    }
}
