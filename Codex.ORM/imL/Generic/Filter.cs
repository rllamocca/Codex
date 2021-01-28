using System;

using Codex.ORM.Struct;

namespace Codex.ORM.Generic
{
    public class Filter : IDisposable
    {
        public SColumn Columna { set; get; } = default(SColumn);
        public SFilter Comprobacion { set; get; } = default(SFilter);
        public OrmParameter[] Parametros { set; get; } = null;

        public Filter(SColumn _c = default, SFilter _co = default, OrmParameter[] _ps = null)
        {
            this.Columna = _c;
            this.Comprobacion = _co;
            this.Parametros = _ps;
        }
        public void Dispose()
        {

        }
    }
}
