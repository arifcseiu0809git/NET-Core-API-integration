using System;
using System.Data;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using DMS.Common.Enums;
using System.Collections.Generic;

namespace DMS.Common.OracleHelperClass
{
    public class OracleProcedure
    {
        public string ProcedureName { get; set; }

        //public  ArrayList adhocparameterList = new ArrayList();
        //public OracleParameter[] AdhocparameterList
        //{
        //    get
        //    {
        //        return adhocparameterList.ToArray(typeof(OracleParameter)) as OracleParameter[];
        //    }
        //}

        //private readonly ArrayList parameterList = new ArrayList();
        public ArrayList parameterList = new ArrayList();
        public OracleParameter[] ParameterList
        {
            get
            {
                return parameterList.ToArray(typeof(OracleParameter)) as OracleParameter[];
            }
        }
        public string ReturnMessage
        {
            get
            {
                return (parameterList[1] as OracleParameter).Value.ToString();
            }
        }
        public int ErrorCode
        {
            get
            {
                return Convert.ToInt32((decimal)(OracleDecimal)((parameterList[0] as OracleParameter).Value));
            }
        }
        public int SucessCode
        {
            get
            {
                return Convert.ToInt32((decimal)(OracleDecimal)((parameterList[0] as OracleParameter).Value));
            }
        }
        public int Output
        {
            get
            {
                return Convert.ToInt32((decimal)(OracleDecimal)((parameterList[2] as OracleParameter).Value));
            }
        }

        public OracleProcedure()
        {
            OracleParameter param = new OracleParameter("po_errorcode", OracleDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);

            param = new OracleParameter("po_errormessage", OracleDbType.Varchar2, 200)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);

            param = new OracleParameter("po_cursor", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);

        }

        public OracleProcedure(string procedureName)
        {
            this.ProcedureName = procedureName;
            OracleParameter param = new OracleParameter("po_errorcode", OracleDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);

            param = new OracleParameter("po_errormessage", OracleDbType.Varchar2, 200)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);
        }
        public OracleProcedure(string procedureName, bool outPut)
        {
            this.ProcedureName = procedureName;
            OracleParameter param = new OracleParameter("po_errorcode", OracleDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);

            param = new OracleParameter("po_errormessage", OracleDbType.Varchar2, 200)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);
            if (outPut)
            {
                param = new OracleParameter("p_output", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                parameterList.Add(param);
            }
        }
        public OracleProcedure(string strSchemaName, string procedureName)
        {
            this.ProcedureName = strSchemaName + procedureName;
            OracleParameter param = new OracleParameter("po_errorcode", OracleDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);

            param = new OracleParameter("po_errormessage", OracleDbType.Varchar2, 200)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);
        }
        public OracleProcedure(string procedureName, int spType, int outPutType)
        {
            this.ProcedureName = procedureName;
            if (spType == (int)SpTypeEnum.NewType && outPutType == (int)executionOutEnum.WithParameter)
            {
                OracleParameter param = new OracleParameter("po_errorcode", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                parameterList.Add(param);

                param = new OracleParameter("po_errormessage", OracleDbType.Varchar2, 200)
                {
                    Direction = ParameterDirection.Output
                };
                parameterList.Add(param);
            }

            else if (spType == (int)SpTypeEnum.OldType && outPutType == (int)executionOutEnum.ForOutputParameter)
            {
                OracleParameter param = new OracleParameter("p_output", OracleDbType.Varchar2, 4000)
                {
                    Direction = ParameterDirection.Output
                };
                parameterList.Add(param);
            }
            else if (spType == (int)SpTypeEnum.OldType && outPutType == (int)executionOutEnum.withOutAnyParameter)
            {

            }
            else
            {

            }
        }
        public void AddInputParameter(string paramName, object Value, OracleDbType oracleType)
        {
            OracleParameter param = new OracleParameter(paramName, oracleType);
            if (oracleType == OracleDbType.Date)
            {
                if (Convert.ToDateTime(Value) == DateTime.MinValue)
                    Value = DBNull.Value;

                else if (Convert.ToDateTime(Value) == DateTime.MaxValue)
                    Value = DBNull.Value;
            }
            else if (oracleType == OracleDbType.Int32 && paramName.EndsWith("ID") && this.ProcedureName.ToUpper().Contains(".SAVE_"))
            {
                if (Convert.ToInt32(Value) <= 0)
                {
                    Value = DBNull.Value;
                }
            }
            param.Value = Value;
            parameterList.Add(param);
        }
        public void AddInputParameter(string paramName, object Value, OracleDbType oracleType, int size)
        {
            OracleParameter param = new OracleParameter(paramName, oracleType, size);
            if (oracleType == OracleDbType.Date)
            {
                if (Convert.ToDateTime(Value) == DateTime.MinValue)
                    Value = DBNull.Value;
            }
            param.Value = Value;
            parameterList.Add(param);
        }
        public void AddInputParameter(string paramName, List<string> stringItem, bool isIntValue, List<int> intItem)
        {
            if (!isIntValue)
            {
                parameterList.Add(CreateStringAssociativeArray(paramName, stringItem, ParameterDirection.Input));
            }
            else
            {
                parameterList.Add(CreateInt32AssociativeArray(paramName, intItem, ParameterDirection.Input));
            }
        }

        //public void AddInputParameter(string paramName, List<T> itemList)
        //{
        //    parameterList.Add(CreateStringAssociativeArray(paramName, itemList, ParameterDirection.Input));           
        //}

        public void ExecuteNonQuery()
        {
            DllOracleExecuteHelper _dllOracle = new DllOracleExecuteHelper();
            _dllOracle.ExecuteNonQueryStoredProcedure(this.ProcedureName, this.ParameterList);
        }        
        public void ExecuteNonQuery(DBTransaction transaction)
        {
            if (transaction == null)
            {
                ExecuteNonQuery();
            }
            else
            {
                DllOracleExecuteHelper _dllOracle = new DllOracleExecuteHelper();
                _dllOracle.ExecuteNonQueryStoredProcedure(this.ProcedureName, this.ParameterList, transaction.CurrentTransaction.Connection, transaction.CurrentTransaction);
            }
        }
        public DataTable ExecuteQueryToDataTable()
        {
            OracleParameter param = new OracleParameter("po_cursor", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);

            DllOracleExecuteHelper _dllOracle = new DllOracleExecuteHelper();
            return _dllOracle.ExecuteStoredProcedureDataTable(this.ProcedureName, this.ParameterList);
        }        
        public DataTable ExecuteQueryToDataTable(bool listSucess)
        {
            OracleParameter param = new OracleParameter("po_cursor", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);

            DllOracleExecuteHelper _dllOracle = new DllOracleExecuteHelper();
            return _dllOracle.ExecuteStoredProcedureDataTable(this.ProcedureName, this.ParameterList, listSucess);
        }

        public void ExecuteNonQuery(string connectionString)
        {
            DllOracleExecuteHelper _dllOracle = new DllOracleExecuteHelper();
            _dllOracle.ExecuteNonQueryStoredProcedure(this.ProcedureName, this.ParameterList, connectionString);
        }
        public DataTable ExecuteQueryToDataTable(string connectionString)
        {
            OracleParameter param = new OracleParameter("po_cursor", OracleDbType.RefCursor)
            {
                Direction = ParameterDirection.Output
            };
            parameterList.Add(param);

            DllOracleExecuteHelper _dllOracle = new DllOracleExecuteHelper();
            return _dllOracle.ExecuteStoredProcedureDataTable(this.ProcedureName, this.ParameterList, connectionString);
        }

        #region String List Post
        public OracleParameter CreateStringAssociativeArray(string name, List<string> values, ParameterDirection direction = ParameterDirection.Input,
            int? maxNumberOfElementsInArray = null, int maxLength = 255)
        {
            var res = CreateAssociativeArray<string, OracleString>(name, values, direction,
                       OracleDbType.Varchar2, OracleString.Null, maxNumberOfElementsInArray);



            if (direction == ParameterDirection.Output || direction == ParameterDirection.InputOutput)
            {
                int curMaxLen = maxLength;
                if (values != null)
                    values.ForEach(s => { if (curMaxLen < s.Length) curMaxLen = s.Length; });

                res.ArrayBindSize = new int[res.Size];
                for (int i = 0; i < res.Size; i++)
                    res.ArrayBindSize[i] = curMaxLen;
            }

            return res;
        }

        public OracleParameter CreateAssociativeArray<ValueType, OracleType>(string name, List<ValueType> values,
                      ParameterDirection direction, OracleDbType oracleDbType,
                      OracleType nullValue, int? maxNumberOfElementsInArray)
        {
            bool isArrayEmpty = values == null || values.Count == 0;
            OracleParameter array = new OracleParameter(name, oracleDbType, direction);
            array.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            array.Value = !isArrayEmpty ? values.ToArray() :
                                                    (object)new OracleType[1] { nullValue };
            array.Size = !isArrayEmpty ? values.Count : 999999;

            // if it's Output/InputOutput parameter, set the maximum possible number of elements.            
            if (maxNumberOfElementsInArray != null &&
               (direction == ParameterDirection.Output || direction == ParameterDirection.InputOutput))
                array.Size = Math.Max(array.Size, maxNumberOfElementsInArray.Value);

            return array;
        }
        #endregion

        #region Long List Post
        public OracleParameter CreateInt32AssociativeArray(string name, List<int> values, ParameterDirection direction = ParameterDirection.Input,
            int? maxNumberOfElementsInArray = null, int maxLength = 255)
        {
            var res = CreateAssociativeArray<int, OracleDecimal>(name, values, direction,
                       OracleDbType.Int32, OracleDecimal.Null, maxNumberOfElementsInArray);

            return res;
        }

        #endregion


    }
}
