using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Common.OracleHelperClass
{
    class OracleAssiciativeArrayHelper
    {
        private string procedureName;
        public string ProcedureName
        {
            get { return procedureName; }
            set { procedureName = value; }
        }

        private ArrayList parameterList = new ArrayList();
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
                return Convert.ToInt32((parameterList[0] as OracleParameter).Value);
            }
        }
        public int SucessCode
        {
            get
            {
                return Convert.ToInt32((parameterList[0] as OracleParameter).Value);
            }
        }

        public OracleAssiciativeArrayHelper()
        {
            OracleParameter param = new OracleParameter("po_errorcode", OracleDbType.Decimal);
            param.Direction = ParameterDirection.Output;
            parameterList.Add(param);

            param = new OracleParameter("po_errormessage", OracleDbType.Varchar2, 200);
            param.Direction = ParameterDirection.Output;
            parameterList.Add(param);

            param = new OracleParameter("po_cursor", OracleDbType.RefCursor);
            param.Direction = ParameterDirection.Output;
            parameterList.Add(param);

        }
        public OracleAssiciativeArrayHelper(string procedureName)
        {
            this.procedureName = procedureName;
            OracleParameter param = new OracleParameter("po_errorcode", OracleDbType.Decimal);
            param.Direction = ParameterDirection.Output;
            parameterList.Add(param);

            param = new OracleParameter("po_errormessage", OracleDbType.Varchar2, 200);
            param.Direction = ParameterDirection.Output;
            parameterList.Add(param);
        }
        public OracleAssiciativeArrayHelper(string strSchemaName, string procedureName)
        {
            this.procedureName = strSchemaName + procedureName;
            OracleParameter param = new OracleParameter("po_errorcode", OracleDbType.Decimal);
            param.Direction = ParameterDirection.Output;
            parameterList.Add(param);

            param = new OracleParameter("po_errormessage", OracleDbType.Varchar2, 200);
            param.Direction = ParameterDirection.Output;
            parameterList.Add(param);
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
            else if (oracleType == OracleDbType.Decimal && paramName.EndsWith("ID") && this.procedureName.ToUpper().Contains(".SAVE_"))
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
    }
}
