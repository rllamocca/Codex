using System;

using Codex.ORM.Enum;

namespace Codex.ORM.Struct
{
    public struct SColumn
    {
        public String Nombre;
        public Boolean LlavePr;
        public Boolean Identidad;
        public String Alias;

        public Boolean LlaveFo;

        public Boolean Grupo;
        public EOrder Orden;
        public String Calculado;

        public SColumn(String _n, Boolean _l = false, Boolean _i = false, Boolean _f = false, String _a = "", Boolean _g = false, EOrder _o = EOrder.None, String _cal = "{0}")
        {
            this.Nombre = _n;
            this.LlavePr = _l;
            this.Identidad = _i;
            this.Alias = _a;

            this.LlaveFo = _f;

            this.Grupo = _g;
            this.Orden = _o;

            this.Calculado = _cal;
        }
    }
}
