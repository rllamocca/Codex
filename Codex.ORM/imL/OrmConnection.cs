using Codex.Generic;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Codex.ORM
{
    public abstract class OrmConnection : IDisposable
    {
        private Boolean _DISPOSED = false;
        protected Return _EXEC;

        public Return Constructor { get { return this._EXEC; } }
        public UInt32 Tope { set; get; } = 100;
        public Int32 Tiempo { set; get; } = 30;
        public Boolean Restricciones { set; get; } = true;
        public Boolean Estadisticas { set; get; } = false;
        public Boolean Preparar { set; get; } = false;

        public CancellationToken Token { set; get; } = default;

        #region METODOS OBJETO
        public virtual void Open() { }
        public virtual void Close() { }

        public virtual Task OpenAsync()
        {
            return null;
        }

        public virtual Task CloseAsync()
        {
            return null;
        }

        public virtual Return RecuperarEstadisticas()
        {
            return new Return(false);
        }
        public virtual void RestablecerEstadisiticas() { }
        #endregion

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~OrmConnection()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(Boolean _managed)
        {
            if (this._DISPOSED)
                return;

            if (_managed)
            {
                this.Tope = 0;
                this.Tiempo = 0;
                this.Restricciones = false;
                this.Estadisticas = false;
                this.Preparar = false;
            }

            this._DISPOSED = true;
        }
    }
}
