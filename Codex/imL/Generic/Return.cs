using System;

namespace Codex.Generic
{
    public class Return
    {
        private readonly Boolean _EXI;
        private readonly Object _RET = null;
        private readonly Boolean _EXC = false;
        private readonly String _MEN = "NO PROCESADO";

        public Boolean Exito { get { return this._EXI; } }
        public Object Resultado { get { return this._RET; } }
        public Boolean Excepcion { get { return this._EXC; } }
        public Boolean ErrorExcepcion { get { return (this.Exito == false) || (this.Excepcion == true); } }

        public String Mensaje { get { return this._MEN; } }

        public Return(Boolean _exito, Object _resultado = null)
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
                    if (this._RET is String)
                        this._MEN = (String)this._RET;
                }
            }
        }
        public void GatillarError()
        {
            if (!this.Exito) 
                throw new Exception(this.Mensaje);
        }
        public void GatillarExcepcion()
        {
            if (this.Excepcion)
                throw (Exception)this.Resultado;
        }
        public void GatillarErrorExcepcion()
        {
            if (!this.Exito)
                this.GatillarError();
            else 
                this.GatillarExcepcion();
        }

        public override String ToString()
        {
            return String.Format("Exito: {0}, {1}", (this.Exito ? "SI" : "NO"), this.Mensaje);
        }
    }
}
