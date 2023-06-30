using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Oracle.ManagedDataAccess.Client;

namespace HPSBYS.Data.Services
{
    public class BaseDataService : IDisposable
    {
        /// <summary>
        /// The disposed
        /// </summary>
        private bool _disposed;
        protected string ConnectionString => ConfigurationManager.ConnectionStrings["HPSBYS_ORDB_Connection"].ConnectionString;
        protected IDbConnection SqlConnecton => new OracleConnection(ConnectionString);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);

        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (SqlConnecton != null)
                    {
                        SqlConnecton.Dispose();
                       
                    }
                    
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BaseDataService"/> class.
        /// </summary>
        ~BaseDataService()
        {
            Dispose(false);
        }
    }
}
