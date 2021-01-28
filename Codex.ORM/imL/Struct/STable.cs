using System;

namespace Codex.ORM.Struct
{
    public struct STable
    {
        public String Nombre;
        public Boolean Identidad;
        public String Alias;

        public STable(String _n, Boolean _i = false, String _a = "")
        {
            this.Nombre = _n;
            this.Identidad = _i;
            this.Alias = _a;
        }
    }
}
