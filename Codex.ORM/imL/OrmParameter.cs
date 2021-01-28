using System;
using System.Data;

using Codex.ORM.Enum;

namespace Codex.ORM
{
    public abstract class OrmParameter : IDisposable
    {
        public ECheck Comprobacion { set; get; } = ECheck.None;
        public ParameterDirection Direccion = ParameterDirection.Input;
        public String Nombre { set; get; } = "";
        public Object Valor { set; get; } = null;
        public Int32 Largo { set; get; } = 32;
        public Byte Precision { set; get; } = 0;
        public Byte Escala { set; get; } = 0;

        public OrmParameter(ECheck _c = ECheck.None,
            ParameterDirection _d = ParameterDirection.Input,
            String _n = "",
            Object _v = null,
            Int32 _l = 32,
            Byte _p = 0,
            Byte _e = 0,
            String _vc = "",
            String _donde = "")
        {
            this.Comprobacion = _c;
            this.Direccion = _d;
            this.Nombre = _n;
            this.Valor = _v;
            this.Largo = _l;
            this.Precision = _p;
            this.Escala = _e;

            if (!this.Nombre.Contains("@"))
                this.Nombre = String.Format("@{0}", this.Nombre);
            if (this.Valor == null)
                this.Valor = DBNull.Value;
        }
        public void Dispose()
        {
            this.Comprobacion = ECheck.None;
            this.Direccion = ParameterDirection.Input;
            this.Nombre = null;
            this.Valor = null;
            this.Largo = 0;
            this.Precision = 0;
            this.Escala = 0;
        }
    }
}
