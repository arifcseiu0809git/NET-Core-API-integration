using DMS.Common.Helper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace DMS.Common.OracleHelperClass
{
    public class DllOracleExecuteHelper
    {
        readonly string OracleConStr = ConfigurationHelper.GetSPConnectionString();
        public DataSet GetDataSet(string strSQL)
        {
            OracleConnection connection = null;
            OracleCommand command = null;
            try
            {
                connection = new OracleConnection(OracleConStr);
                command = new OracleCommand
                {
                    CommandText = strSQL,
                    BindByName = true,
                    Connection = connection
                };
                DataSet dataSet = new DataSet();
                OracleDataAdapter dataAdapter = new OracleDataAdapter();
                connection.Open();
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(dataSet);
                connection.Close();
                return dataSet;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (command != null) command.Dispose();
                if (connection != null && connection.State != ConnectionState.Closed) connection.Close();
            }
            return null;
        }
        public DataTable ExecuteStoredProcedureDataTable(string strProcedureName, OracleParameter[] arlParams,string connectionString)
        {

            using (OracleConnection connection = new OracleConnection(connectionString))
            using (OracleCommand command = new OracleCommand())
            {
                command.Connection = connection;
                command.CommandText = strProcedureName;
                command.BindByName = true;
                command.CommandType = CommandType.StoredProcedure;


                if (arlParams != null)
                {
                    for (int i = 0; i < arlParams.Length; i++)
                    {
                        if (arlParams[i].Value == null)
                        {
                            arlParams[i].Value = DBNull.Value;
                        }
                        command.Parameters.Add(arlParams[i]);
                    }
                }
                try
                {
                    connection.Open();
                    DataTable dt = new DataTable(strProcedureName);
                    using (OracleDataAdapter da = new OracleDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        public DataTable ExecuteStoredProcedureDataTable(string strProcedureName, OracleParameter[] arlParams)
        {

            using (OracleConnection connection = new OracleConnection(OracleConStr))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    command.Connection = connection;
                    command.CommandText = strProcedureName;
                    command.BindByName = true;
                    command.CommandType = CommandType.StoredProcedure;


                    if (arlParams != null)
                    {
                        for (int i = 0; i < arlParams.Length; i++)
                        {
                            if (arlParams[i].Value == null)
                            {
                                arlParams[i].Value = DBNull.Value;
                            }
                            command.Parameters.Add(arlParams[i]);
                        }
                    }
                    try
                    {
                        //connection.Open();
                        DataTable dt = new DataTable(strProcedureName);
                        using (OracleDataAdapter da = new OracleDataAdapter(command))
                        {
                            da.Fill(dt);
                        }
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
            }

        }
        //public DataTable ExecuteStoredProcedureDataTable(string strProcedureName, OracleParameter[] arlParams)
        //{

        //    using (OracleConnection connection = new OracleConnection(OracleConStr))
        //    {
        //        using (OracleCommand command = new OracleCommand())
        //        {
        //            command.Connection = connection;
        //            command.CommandText = strProcedureName;
        //            command.BindByName = true;
        //            command.CommandType = CommandType.StoredProcedure;


        //            if (arlParams != null)
        //            {
        //                for (int i = 0; i < arlParams.Length; i++)
        //                {
        //                    if (arlParams[i].Value == null)
        //                    {
        //                        arlParams[i].Value = DBNull.Value;
        //                    }
        //                    command.Parameters.Add(arlParams[i]);
        //                }
        //            }
        //            try
        //            {
        //                //connection.Open();
        //                DataTable dt = new DataTable(strProcedureName);
        //                using (OracleDataAdapter da = new OracleDataAdapter(command))
        //                {
        //                    da.Fill(dt);
        //                }

        //                return dt;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw (ex);
        //            }
        //        }
        //    }
            
        //}
        public DataTable ExecuteStoredProcedureDataTable(string strProcedureName, OracleParameter[] arlParams, bool listQuery)
        {
            using (OracleConnection connection = new OracleConnection(OracleConStr))
            using (OracleCommand command = new OracleCommand())
            {
                command.Connection = connection;
                command.CommandText = strProcedureName;
                command.BindByName = true;
                command.CommandType = CommandType.StoredProcedure;

                if (arlParams != null)
                {
                    for (int i = 0; i < arlParams.Length; i++)
                    {
                        command.Parameters.Add(arlParams[i]);
                    }
                }

                try
                {
                    connection.Open();
                    DataTable dt = new DataTable(strProcedureName);
                    using (OracleDataAdapter da = new OracleDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                    return dt;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        public int ExecuteNonQueryStoredProcedure(string strProcedureName, OracleParameter[] arlParams)
        {
            using (OracleConnection connection = new OracleConnection(OracleConStr))
            using (OracleCommand command = new OracleCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = strProcedureName;
                    command.BindByName = true;
                    command.Connection = connection;
                    if (arlParams != null)
                    {
                        for (int i = 0; i < arlParams.Length; i++)
                        {
                            if (arlParams[i].Value == null)
                            {
                                arlParams[i].Value = DBNull.Value;
                            }
                            command.Parameters.Add(arlParams[i]);//.ToString().Trim('@'), OracleDbType.Varchar2).Value = arlParams[i].Value;
                        }
                    }

                    int intResult = command.ExecuteNonQuery();
                    return intResult;
                }
                catch (Exception ex)
                {

                    throw (ex);
                }
            }
        }
        public int ExecuteNonQueryStoredProcedure(string strProcedureName, OracleParameter[] arlParams, OracleConnection connection, OracleTransaction objTransaction)
        {
            using (OracleCommand command = new OracleCommand())
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = strProcedureName;
                    command.BindByName = true;
                    command.Transaction = objTransaction;
                    command.Connection = connection;
                    if (arlParams != null)
                    {
                        for (int i = 0; i < arlParams.Length; i++)
                        {
                            if (arlParams[i].Value == null)
                            {
                                arlParams[i].Value = DBNull.Value;
                            }
                            command.Parameters.Add(arlParams[i]);//.ToString().Trim('@'), OracleDbType.Varchar2).Value = arlParams[i].Value;
                        }
                    }


                    int intResult = command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    return intResult;

                }
                catch (Exception ex)
                {

                    throw (ex);
                }
            }
        }
        public int ExecuteNonQueryStoredProcedure(string strProcedureName, OracleParameter[] arlParams, string connectionString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            using (OracleCommand command = new OracleCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = strProcedureName;
                    command.BindByName = true;
                    command.Connection = connection;
                    if (arlParams != null)
                    {
                        for (int i = 0; i < arlParams.Length; i++)
                        {
                            if (arlParams[i].Value == null)
                            {
                                arlParams[i].Value = DBNull.Value;
                            }
                            command.Parameters.Add(arlParams[i]);//.ToString().Trim('@'), OracleDbType.Varchar2).Value = arlParams[i].Value;
                        }
                    }

                    int intResult = command.ExecuteNonQuery();
                    return intResult;
                }
                catch (Exception ex)
                {

                    throw (ex);
                }
            }
        }
    }
}
