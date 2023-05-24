using DMS.Common.Helper;
using Oracle.ManagedDataAccess.Client;
using System;

namespace DMS.Common.OracleHelperClass
{
    public class DBTransaction : IDisposable
    {
        public OracleTransaction CurrentTransaction = null;
        public OracleConnection con = new OracleConnection(ConfigurationHelper.GetSPConnectionString().ToString());

        public void Begin()
        {
            con.Open();
            CurrentTransaction = con.BeginTransaction();
        }

        public void Commit()
        {
            CurrentTransaction.Commit();
        }
        public void RollBack()
        {
            CurrentTransaction.Rollback();
        }
        public void Dispose()
        {
            CurrentTransaction.Dispose();
            con.Dispose();
        }

        #region IDisposable Members
        void IDisposable.Dispose()
        {
            CurrentTransaction.Dispose();
            con.Dispose();
        }
        #endregion
    }
}