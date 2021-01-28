using Codex.Generic;

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Codex.ORM.Generic
{
    public class OrmSqlConnection : OrmConnection
    {
        private Boolean _DISPOSED = false;
        private SqlConnection _CN;
        private SqlTransaction _TS;

        public OrmSqlConnection(String _conexion, Boolean _estadisticas = false)
        {
            try
            {
                this.Estadisticas = _estadisticas;

                this._CN = new SqlConnection(_conexion);
                this._CN.StatisticsEnabled = this.Estadisticas;
            }
            catch (Exception _ex)
            {
                base._EXEC = new Return(false, _ex);
            }
        }
        #region PROPIEDADES
        public SqlConnection Conexion
        {
            get { return this._CN; }
        }
        public SqlTransaction Transaccion
        {
            set { this._TS = value; }
            get { return this._TS; }
        }
        #endregion
        #region METODOS OBJETO
        public override void Open()
        {
            switch (this._CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    this._CN.Open();
                    this._CN.StatisticsEnabled = this.Estadisticas;
                    break;
                default:
                    break;
            }
        }
        public override void Close()
        {
            if (this._CN.State != ConnectionState.Closed)
                this._CN.Close();
        }

        public override async Task OpenAsync()
        {
            switch (this._CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    await this._CN.OpenAsync();
                    this._CN.StatisticsEnabled = this.Estadisticas;
                    break;
                default:
                    break;
            }
        }

        //public override async Task CloseAsync()
        //{
        //    if (this._CN.State != ConnectionState.Closed)
        //        await this._CN.CloseAsync();
        //}

        //public override Return Transaccion()
        //{
        //    try
        //    {
        //        Return _cr = this.Abrir();
        //        _cr.GatillarErrorExcepcion();
        //        SqlTransaction _tra = this._CN.BeginTransaction();
        //        return new Return(true, _tra);
        //    }
        //    catch (Exception _ex)
        //    {
        //        return new Return(false, _ex);
        //    }
        //}
        public override Return RecuperarEstadisticas()
        {
            try
            {
                IDictionary _rs = _CN.RetrieveStatistics();
                return new Return(true, _rs);
            }
            catch (Exception _ex)
            {
                return new Return(false, _ex);
            }
        }
        public override void RestablecerEstadisiticas()
        {
            this._CN.ResetStatistics();
        }
        #endregion

        protected override void Dispose(Boolean _managed)
        {
            if (this._DISPOSED)
                return;

            if (_managed)
            {
                if (this._TS != null)
                {
                    this._TS.Dispose();
                    this._TS = null;
                }
                if (this._CN != null)
                {
                    this._CN.Dispose();
                    this._CN = null;
                }
            }

            this._DISPOSED = true;

            base.Dispose(_managed);
        }
    }
}
