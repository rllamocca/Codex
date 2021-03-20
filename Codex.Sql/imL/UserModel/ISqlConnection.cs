#if (NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)
using System.Threading;
using System.Threading.Tasks;
#endif

using Codex.Contract;

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Codex.Sql.UserModel
{
    public class ISqlConnection : IConnection
    {
        private bool _DISPOSED = false;
        private bool _STATISTICS = false;

        private SqlTransaction _TS;
        private SqlConnection _CN;

        public SqlTransaction Transaction
        {
            set { this._TS = value; }
            get { return this._TS; }
        }
        public SqlConnection Connection
        {
            get { return this._CN; }
        }

        public ISqlConnection(SqlConnection _conn)
        {
            this._STATISTICS = this._CN.StatisticsEnabled;
            this._CN = _conn;
        }
        public ISqlConnection(string _conn, bool _stat = false)
        {
            this._STATISTICS = _stat;
            this._CN = new SqlConnection(_conn)
            {
                StatisticsEnabled = this._STATISTICS
            };
        }
        public IDictionary RetrieveStatistics()
        {
            return _CN.RetrieveStatistics();
        }
        public void ResetStatistics()
        {
            this._CN.ResetStatistics();
        }

        //####
        public int TimeOut { set; get; } = 100;
        public bool Constraints { set; get; } = false;
#if (NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)
        public CancellationToken Token { set; get; } = default;
#endif

        public void Close()
        {
            if (this._CN.State != ConnectionState.Closed)
                this._CN.Close();
        }

        ~ISqlConnection()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool _managed)
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

                this._STATISTICS = false;

                this.TimeOut = 0;
                this.Constraints = false;
            }

            this._DISPOSED = true;
        }

        public void Open()
        {
            switch (this._CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    this._CN.Open();
                    this._CN.StatisticsEnabled = this._STATISTICS;
                    break;
                default:
                    break;
            }
        }
#if (NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)
        public async Task OpenAsync()
        {
            switch (this._CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    await this._CN.OpenAsync();
                    this._CN.StatisticsEnabled = this._STATISTICS;
                    break;
                default:
                    break;
            }
        }
#endif
    }
}
