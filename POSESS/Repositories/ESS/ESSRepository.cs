using DMS.Common.Helper;
using DMS.Common.OracleHelperClass;
using DMS.DAL.Repositories.PromotionRepository;
using Oracle.ManagedDataAccess.Client;
using POSESS.Helper;
using POSESS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using WebAPIWT.Controllers;
using System.Linq;
using System.Collections;

namespace DMS.DAL.Repositories.ESSRepository
{
    public class ESSRepository : IESSRepository, IDisposable
    {
        public (bool Success, string Message, dynamic Data) InsertESS(List<ESSViewModel> itemlist)
        {
            List<ESSViewModelNew> validItemList = new List<ESSViewModelNew>();
            List<ESSViewModelNew> itemlistNew = new List<ESSViewModelNew>();
            List<ESSErrorResponseViewModel> itemlistError = new List<ESSErrorResponseViewModel>();

            itemlistNew = itemlist.Select((x, sl) => new ESSViewModelNew
            {
                RECEIVE_CODE = x.RECEIVE_CODE,
                receive_date = x.receive_date,
                invoice_no = x.invoice_no,
                lcpo_no = x.lcpo_no,
                po_no = x.po_no,
                warehouse_code = x.warehouse_code,
                product_code = x.product_code,
                COMMERCIALIZEDFLAG = x.COMMERCIALIZEDFLAG,
                start_serial = x.start_serial,
                end_serial = x.end_serial,
                supplier_code = x.supplier_code,
                SupplierName = x.SupplierName,
                supplier_address1 = x.supplier_address1,
                supplier_address2 = x.supplier_address2,
                supplier_telephone_no = x.supplier_telephone_no,
                supplier_contactPerson = x.supplier_contactPerson,
                supplier_contact_no = x.supplier_contact_no,
                supplier_email = x.supplier_email,
                quantity = x.quantity,
                sn = sl++,
            }).ToList();

            validItemList.AddRange(itemlistNew);

            (bool Success, string Message, dynamic Data) returnResponse;
            try
            {
                try
                {
                    if (itemlistNew.Count > 0)
                    {

                        foreach (var model in itemlistNew)
                        {
                            if (model.COMMERCIALIZEDFLAG.ToUpper().Contains("Y") || model.COMMERCIALIZEDFLAG.ToUpper().Contains("N"))
                            {
                                
                            }
                            else
                            {
                                ESSViewModelNew invalidItemNew = new ESSViewModelNew();
                                invalidItemNew = itemlistNew.FirstOrDefault(x => x.sn == model.sn);
                                validItemList.Remove(invalidItemNew);

                                itemlistError.Add(new ESSErrorResponseViewModel
                                {
                                    RECEIVE_CODE = invalidItemNew.RECEIVE_CODE,
                                    receive_date = invalidItemNew.receive_date,
                                    invoice_no = invalidItemNew.invoice_no,
                                    lcpo_no = invalidItemNew.lcpo_no,
                                    po_no = invalidItemNew.po_no,
                                    warehouse_code = invalidItemNew.warehouse_code,
                                    product_code = invalidItemNew.product_code,
                                    COMMERCIALIZEDFLAG = invalidItemNew.COMMERCIALIZEDFLAG,
                                    start_serial = invalidItemNew.start_serial,
                                    end_serial = invalidItemNew.end_serial,
                                    supplier_code = invalidItemNew.supplier_code,
                                    SupplierName = invalidItemNew.SupplierName,
                                    supplier_address1 = invalidItemNew.supplier_address1,
                                    supplier_address2 = invalidItemNew.supplier_address2,
                                    supplier_telephone_no = invalidItemNew.supplier_telephone_no,
                                    supplier_contactPerson = invalidItemNew.supplier_contactPerson,
                                    supplier_contact_no = invalidItemNew.supplier_contact_no,
                                    supplier_email = invalidItemNew.supplier_email,
                                    quantity = invalidItemNew.quantity,
                                    error = "Commercialized flag must be Y / N."
                                });
                            }
                        }
                    }

                    itemlistNew.Clear();
                    itemlistNew.AddRange(validItemList);


                }
                catch { }

                try
                {
                    if (itemlistNew.Count > 0)
                    {

                        foreach (var model in itemlistNew)
                        {
                            if (String.IsNullOrEmpty(model.start_serial.Trim()) || String.IsNullOrEmpty(model.end_serial.Trim()))
                            {
                                ESSViewModelNew invalidItemNew = new ESSViewModelNew();

                                invalidItemNew = itemlistNew.FirstOrDefault(x => x.sn == model.sn);
                                validItemList.Remove(invalidItemNew);

                                itemlistError.Add(new ESSErrorResponseViewModel
                                {
                                    RECEIVE_CODE = invalidItemNew.RECEIVE_CODE,
                                    receive_date = invalidItemNew.receive_date,
                                    invoice_no = invalidItemNew.invoice_no,
                                    lcpo_no = invalidItemNew.lcpo_no,
                                    po_no = invalidItemNew.po_no,
                                    warehouse_code = invalidItemNew.warehouse_code,
                                    product_code = invalidItemNew.product_code,
                                    COMMERCIALIZEDFLAG = invalidItemNew.COMMERCIALIZEDFLAG,
                                    start_serial = invalidItemNew.start_serial,
                                    end_serial = invalidItemNew.end_serial,
                                    supplier_code = invalidItemNew.supplier_code,
                                    SupplierName = invalidItemNew.SupplierName,
                                    supplier_address1 = invalidItemNew.supplier_address1,
                                    supplier_address2 = invalidItemNew.supplier_address2,
                                    supplier_telephone_no = invalidItemNew.supplier_telephone_no,
                                    supplier_contactPerson = invalidItemNew.supplier_contactPerson,
                                    supplier_contact_no = invalidItemNew.supplier_contact_no,
                                    supplier_email = invalidItemNew.supplier_email,
                                    quantity = invalidItemNew.quantity,
                                    error = "Start serial and end serial must not be empty."
                                });

                                //returnResponse = (false, "Start serial and end serial must not be empty..", itemlistError);
                            }
                        }

                        itemlistNew.Clear();
                        itemlistNew.AddRange(validItemList);
                    }

                }
                catch { }

                try
                {
                    if (itemlistNew.Count > 0)
                    {

                        foreach (var model in itemlistNew)
                        {
                            if (Convert.ToInt64(model.start_serial) > Convert.ToInt64(model.end_serial))
                            {
                                ESSViewModelNew invalidItemNew = new ESSViewModelNew();

                                invalidItemNew = itemlistNew.FirstOrDefault(x => x.sn == model.sn);
                                validItemList.Remove(invalidItemNew);

                                itemlistError.Add(new ESSErrorResponseViewModel
                                {
                                    RECEIVE_CODE = invalidItemNew.RECEIVE_CODE,
                                    receive_date = invalidItemNew.receive_date,
                                    invoice_no = invalidItemNew.invoice_no,
                                    lcpo_no = invalidItemNew.lcpo_no,
                                    po_no = invalidItemNew.po_no,
                                    warehouse_code = invalidItemNew.warehouse_code,
                                    product_code = invalidItemNew.product_code,
                                    COMMERCIALIZEDFLAG = invalidItemNew.COMMERCIALIZEDFLAG,
                                    start_serial = invalidItemNew.start_serial,
                                    end_serial = invalidItemNew.end_serial,
                                    supplier_code = invalidItemNew.supplier_code,
                                    SupplierName = invalidItemNew.SupplierName,
                                    supplier_address1 = invalidItemNew.supplier_address1,
                                    supplier_address2 = invalidItemNew.supplier_address2,
                                    supplier_telephone_no = invalidItemNew.supplier_telephone_no,
                                    supplier_contactPerson = invalidItemNew.supplier_contactPerson,
                                    supplier_contact_no = invalidItemNew.supplier_contact_no,
                                    supplier_email = invalidItemNew.supplier_email,
                                    quantity = invalidItemNew.quantity,
                                    error = "Start serial must be less than the end serial."
                                });

                                //returnResponse = (false, "Start serial must be less than the end serial..", itemlistError);
                            }
                        }

                        itemlistNew.Clear();
                        itemlistNew.AddRange(validItemList);
                    }

                }
                catch { }

                try
                {
                    if (itemlistNew.Count > 0)
                    {
                        foreach (var model in itemlistNew)
                        {
                            if (model.COMMERCIALIZEDFLAG == "N" && model.start_serial != model.end_serial)
                            {
                                ESSViewModelNew invalidItemNew = new ESSViewModelNew();

                                invalidItemNew = itemlistNew.FirstOrDefault(x => x.sn == model.sn);
                                validItemList.Remove(invalidItemNew);

                                itemlistError.Add(new ESSErrorResponseViewModel
                                {
                                    RECEIVE_CODE = invalidItemNew.RECEIVE_CODE,
                                    receive_date = invalidItemNew.receive_date,
                                    invoice_no = invalidItemNew.invoice_no,
                                    lcpo_no = invalidItemNew.lcpo_no,
                                    po_no = invalidItemNew.po_no,
                                    warehouse_code = invalidItemNew.warehouse_code,
                                    product_code = invalidItemNew.product_code,
                                    COMMERCIALIZEDFLAG = invalidItemNew.COMMERCIALIZEDFLAG,
                                    start_serial = invalidItemNew.start_serial,
                                    end_serial = invalidItemNew.end_serial,
                                    supplier_code = invalidItemNew.supplier_code,
                                    SupplierName = invalidItemNew.SupplierName,
                                    supplier_address1 = invalidItemNew.supplier_address1,
                                    supplier_address2 = invalidItemNew.supplier_address2,
                                    supplier_telephone_no = invalidItemNew.supplier_telephone_no,
                                    supplier_contactPerson = invalidItemNew.supplier_contactPerson,
                                    supplier_contact_no = invalidItemNew.supplier_contact_no,
                                    supplier_email = invalidItemNew.supplier_email,
                                    quantity = invalidItemNew.quantity,
                                    error = "Start serial must be same as end serial for non commercial items."
                                });

                                //returnResponse = (false, "Start serial must be same as end serial for non commercial items..", itemlistError);
                            }
                        }

                        itemlistNew.Clear();
                        itemlistNew.AddRange(validItemList);
                    }
                }
                catch { }

                try
                {
                    if (itemlistNew.Count > 0)
                    {
                        foreach (var model in itemlistNew)
                        {
                            List<ESSViewModelNew> invalidItemsNew = new List<ESSViewModelNew>();

                            invalidItemsNew = validItemList.Where(x => Convert.ToInt64(x.start_serial) <= Convert.ToInt64(model.end_serial) && Convert.ToInt64(x.end_serial) >= Convert.ToInt64(model.start_serial) && x.sn != model.sn).ToList();

                            foreach (ESSViewModelNew invalidItemNew in invalidItemsNew)
                            {
                                validItemList.Remove(invalidItemNew);

                                itemlistError.Add(new ESSErrorResponseViewModel
                                {
                                    RECEIVE_CODE = invalidItemNew.RECEIVE_CODE,
                                    receive_date = invalidItemNew.receive_date,
                                    invoice_no = invalidItemNew.invoice_no,
                                    lcpo_no = invalidItemNew.lcpo_no,
                                    po_no = invalidItemNew.po_no,
                                    warehouse_code = invalidItemNew.warehouse_code,
                                    product_code = invalidItemNew.product_code,
                                    COMMERCIALIZEDFLAG = invalidItemNew.COMMERCIALIZEDFLAG,
                                    start_serial = invalidItemNew.start_serial,
                                    end_serial = invalidItemNew.end_serial,
                                    supplier_code = invalidItemNew.supplier_code,
                                    SupplierName = invalidItemNew.SupplierName,
                                    supplier_address1 = invalidItemNew.supplier_address1,
                                    supplier_address2 = invalidItemNew.supplier_address2,
                                    supplier_telephone_no = invalidItemNew.supplier_telephone_no,
                                    supplier_contactPerson = invalidItemNew.supplier_contactPerson,
                                    supplier_contact_no = invalidItemNew.supplier_contact_no,
                                    supplier_email = invalidItemNew.supplier_email,
                                    quantity = invalidItemNew.quantity,
                                    error = "Serial number overlap found in request."
                                });
                            }
                        }
                        itemlistNew.Clear();
                        itemlistNew.AddRange(validItemList);
                    }
                }
                catch { }

                string product_codes = "";
                string warehouse_codes = "";
                string serials = "";

                string distributor_codes = "";
                string non_commercial_product_codes = "";
                string non_commercial_serials = "";

                if (validItemList.Count > 0)
                {

                    foreach (var model in validItemList)
                    {
                        if (model.COMMERCIALIZEDFLAG.ToUpper() == "Y")
                        {
                            product_codes += !String.IsNullOrWhiteSpace(model.product_code.Trim()) ? model.sn + "." + model.product_code.Trim() + "," : "";
                            warehouse_codes += !String.IsNullOrWhiteSpace(model.warehouse_code.Trim()) ? model.sn + "." + model.warehouse_code.Trim() + "," : "";

                            try
                            {
                                decimal startSN = Convert.ToDecimal(model.start_serial);
                                decimal endSN = Convert.ToDecimal(model.end_serial);

                                serials += !String.IsNullOrWhiteSpace(model.start_serial.Trim()) && !String.IsNullOrWhiteSpace(model.end_serial.Trim())
                                            ? model.sn + "." + model.start_serial.Trim() + "-" + model.end_serial.Trim() + ","
                                            : "";
                            }
                            catch
                            { }
                        }
                        else
                        {
                            distributor_codes += !String.IsNullOrWhiteSpace(model.supplier_code.Trim()) ? model.sn + "." + model.supplier_code.Trim() + "," : "";
                            non_commercial_product_codes += !String.IsNullOrWhiteSpace(model.product_code.Trim()) ? model.sn + "." + model.product_code.Trim() + "," : "";
                            non_commercial_serials += !String.IsNullOrWhiteSpace(model.start_serial.Trim()) ? model.sn + "." + model.start_serial.Trim() + "," : "";
                        }
                    }

                }

                product_codes = !String.IsNullOrWhiteSpace(product_codes.Trim()) && product_codes.Contains(",") ? product_codes.TrimEnd(',') : "";
                warehouse_codes = !String.IsNullOrWhiteSpace(warehouse_codes.Trim()) && warehouse_codes.Contains(",") ? warehouse_codes.TrimEnd(',') : "";
                distributor_codes = !String.IsNullOrWhiteSpace(distributor_codes.Trim()) && distributor_codes.Contains(",") ? distributor_codes.TrimEnd(',') : "";
                non_commercial_product_codes = !String.IsNullOrWhiteSpace(non_commercial_product_codes.Trim()) && non_commercial_product_codes.Contains(",") ? non_commercial_product_codes.TrimEnd(',') : "";
                non_commercial_serials = !String.IsNullOrWhiteSpace(non_commercial_serials.Trim()) && non_commercial_serials.Contains(",") ? non_commercial_serials.TrimEnd(',') : "";

                serials = !String.IsNullOrWhiteSpace(serials.Trim()) && serials.Contains(",") && serials.Contains("-") ? serials.TrimEnd(',') : "";

                OracleProcedure val_procedure = new OracleProcedure(SpNameHelper.validate_InsertESS);
                val_procedure.AddInputParameter("in_product_codes", product_codes, OracleDbType.Varchar2);
                val_procedure.AddInputParameter("in_warehouse_codes", warehouse_codes, OracleDbType.Varchar2);
                val_procedure.AddInputParameter("in_distributor_codes", distributor_codes, OracleDbType.Varchar2);
                val_procedure.AddInputParameter("in_non_comm_product_codes", non_commercial_product_codes, OracleDbType.Varchar2);
                val_procedure.AddInputParameter("in_non_comm_serials", non_commercial_serials, OracleDbType.Varchar2);
                val_procedure.AddInputParameter("in_serials", serials, OracleDbType.Varchar2);
                DataTable dt = val_procedure.ExecuteQueryToDataTable();

                if (dt.Rows.Count > 0)
                {

                    //List<ESSIvalidData> data = ((IEnumerable)dt.Rows).Cast<DataRow>().Select(r => new ESSIvalidData { sn = (int)r[2], reason = (string)r[1], place = (string)r[0] }).ToList();

                    List<ESSIvalidData> data = new List<ESSIvalidData>();

                    data = (from DataRow dr in dt.Rows
                            where dr[2].ToString().Trim() != ""
                            select new ESSIvalidData()
                            {
                                sn = Convert.ToInt32(dr[2]),
                                reason = dr[1].ToString(),
                                place = dr[0].ToString()
                            }).ToList();

                    List<int> sr = (List<int>)data.Select(x => Convert.ToInt32(x.sn)).ToList().Distinct().ToList();

                    string ess_msg = "";
                    string pos_msg = "";
                    string product_msg = "";
                    string warehouse_msg = "";
                    string distributor_msg = "";
                    string dms_non_comm_msg = "";
                    string pos_non_comm_msg = "";
                    string error_msg = "";

                    foreach (int i in sr)
                    {
                        error_msg = string.Empty;
                        ESSViewModelNew invalidItemNew = new ESSViewModelNew();
                        invalidItemNew = validItemList.FirstOrDefault(x => x.sn == i);

                        ess_msg = generateErrorMsg(data, "ess_receive_code");
                        error_msg += !String.IsNullOrWhiteSpace(ess_msg.Trim()) ? ess_msg + " " : "";

                        pos_msg = generateErrorMsg(data, "pos_receive_code");
                        error_msg += !String.IsNullOrWhiteSpace(pos_msg.Trim()) ? pos_msg + " " : "";

                        product_msg = generateErrorMsg(data, "product_code");
                        error_msg += !String.IsNullOrWhiteSpace(product_msg.Trim()) ? product_msg + " " : "";

                        warehouse_msg = generateErrorMsg(data, "warehouse_code");
                        error_msg += !String.IsNullOrWhiteSpace(warehouse_msg.Trim()) ? warehouse_msg + " " : "";

                        distributor_msg = generateErrorMsg(data, "distributor_code");
                        error_msg += !String.IsNullOrWhiteSpace(distributor_msg.Trim()) ? distributor_msg + " " : "";

                        dms_non_comm_msg = generateErrorMsg(data, "dms_non_comm_product_serial");
                        error_msg += !String.IsNullOrWhiteSpace(dms_non_comm_msg.Trim()) ? dms_non_comm_msg + " " : "";

                        pos_non_comm_msg = generateErrorMsg(data, "ess_non_comm_product_serial");
                        error_msg += !String.IsNullOrWhiteSpace(pos_non_comm_msg.Trim()) ? pos_non_comm_msg + " " : "";

                        //error_msg = !String.IsNullOrWhiteSpace(error_msg.Trim()) ? error_msg.TrimEnd(' ') : "";
                        validItemList.Remove(invalidItemNew);

                        if (invalidItemNew != null)
                        {
                            itemlistError.Add(new ESSErrorResponseViewModel
                            {
                                RECEIVE_CODE = invalidItemNew.RECEIVE_CODE,
                                receive_date = invalidItemNew.receive_date,
                                invoice_no = invalidItemNew.invoice_no,
                                lcpo_no = invalidItemNew.lcpo_no,
                                po_no = invalidItemNew.po_no,
                                warehouse_code = invalidItemNew.warehouse_code,
                                product_code = invalidItemNew.product_code,
                                COMMERCIALIZEDFLAG = invalidItemNew.COMMERCIALIZEDFLAG,
                                start_serial = invalidItemNew.start_serial,
                                end_serial = invalidItemNew.end_serial,
                                supplier_code = invalidItemNew.supplier_code,
                                SupplierName = invalidItemNew.SupplierName,
                                supplier_address1 = invalidItemNew.supplier_address1,
                                supplier_address2 = invalidItemNew.supplier_address2,
                                supplier_telephone_no = invalidItemNew.supplier_telephone_no,
                                supplier_contactPerson = invalidItemNew.supplier_contactPerson,
                                supplier_contact_no = invalidItemNew.supplier_contact_no,
                                supplier_email = invalidItemNew.supplier_email,
                                quantity = invalidItemNew.quantity,
                                error = error_msg
                            });
                        }

                    }

                }


                foreach (var model in validItemList)
                {
                    OracleProcedure procedure = new OracleProcedure(SpNameHelper.InsertESS);
                    procedure.AddInputParameter("in_RECEIVECODE", model.RECEIVE_CODE, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_RECEIVEDATE", Convert.ToDateTime(model.receive_date), OracleDbType.Date);
                    procedure.AddInputParameter("in_INVOICENO", model.invoice_no, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_LCPONO", model.lcpo_no, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_PONO", model.po_no, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_WAREHOUSECODE", model.warehouse_code, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_PRODUCTCODE", model.product_code, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_COMMERCIALIZEDFLAG", model.COMMERCIALIZEDFLAG.ToUpper(), OracleDbType.Char);
                    procedure.AddInputParameter("in_QTY", model.quantity, OracleDbType.Decimal);
                    procedure.AddInputParameter("in_SIMSTART", model.start_serial, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_SIMEND", model.end_serial, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_SUPPLIERCODE", model.supplier_code, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_SUPPLIERNAME", model.SupplierName, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_ADDRESSLINE1", model.supplier_address1, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_ADDRESSLINE2", model.supplier_address2, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_TELEPHONENO", model.supplier_telephone_no, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_CONTACTPERSON", model.supplier_contactPerson, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_CONTACTNO", model.supplier_contact_no, OracleDbType.Varchar2);
                    procedure.AddInputParameter("in_FLEXFIELD1", model.supplier_email, OracleDbType.Varchar2);
                    procedure.ExecuteNonQuery();

                }

                returnResponse = (true, "", null);

                if (validItemList.Count == 0)
                {
                    returnResponse = (false, "Error Occured.", itemlistError);
                }
                else if (validItemList.Count == itemlist.Count)
                {
                    returnResponse = (true, "Submitted successfully.", itemlistError);
                }
                else
                {
                    returnResponse = (false, "Submitted successfully with errors.", itemlistError);
                }

            }
            catch (Exception ex)
            {
                returnResponse = (false, ex?.Message?.ToString() ?? "", null);
            }

            return returnResponse;
        }

        private static string generateErrorMsg(List<ESSIvalidData> data, string place)
        {
            string error_msg = "";
            List<ESSIvalidData> inv = data.Where(x => x.place == place).ToList();
            if (inv.Count > 0)
            {
                if (place == "ess_receive_code")
                {
                    error_msg = "Serial number overlap found for ess receive codes - ";
                }
                else if (place == "pos_receive_code")
                {
                    error_msg = "Serial number overlap found for pos receive codes - ";
                }
                else if (place == "product_code")
                {
                    error_msg = "Product Code mismatched.";
                }
                else if (place == "warehouse_code")
                {
                    error_msg = "Warehouse Code mismatched.";
                }
                else if (place == "distributor_code")
                {
                    error_msg = "Distributor Code mismatched.";
                }
                else if (place == "dms_non_comm_product_serial")
                {
                    error_msg = "Serial number overlap found for DMS product codes -";
                }
                else if (place == "ess_non_comm_product_serial")
                {
                    error_msg = "Serial number overlap found for ESS receive codes -";
                }
                if (place == "ess_receive_code" || place == "pos_receive_code" || place == "dms_non_comm_product_serial" || place == "ess_non_comm_product_serial")
                {
                    foreach (ESSIvalidData invd in inv)
                    {
                        if (invd.place == place)
                        {
                            error_msg += invd.reason + ",";
                        }
                    }
                }

                error_msg = !String.IsNullOrWhiteSpace(error_msg.Trim()) || error_msg.Contains(",") ? error_msg.TrimEnd(',') : "";

            }

            return error_msg;
        }

        public (bool Success, string Message) InsertTransactionList(List<ESSViewModel> itemlist)
        {
            (bool Success, string Message) returnResponse;
            try
            {
                List<ESSTest> testArray = new List<ESSTest>();
                foreach (var item in itemlist)
                {
                    ESSTest model = new ESSTest();
                    model.RECEIVE_CODE = item.RECEIVE_CODE;
                    testArray.Add(model);
                }

                using (OracleConnection sqlConnection = new OracleConnection(ConfigurationHelper.GetSPConnectionString()))
                {
                    using (OracleCommand sqlCommand = new OracleCommand("sp_InsertTT", sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add(
                            new OracleParameter
                            {
                                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                                Direction = ParameterDirection.Input,
                                ParameterName = "p_EssTransaction",
                        //OracleDbType = OracleDbType.Object,
                        UdtTypeName = "tess_Test",
                                Value = testArray
                            }
                        );

                        //DataTable dt = DataTableHelper.ToDataTable(itemlist);
                        //sqlCommand.Parameters.Add(
                        //    new OracleParameter
                        //    {
                        //        CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                        //        Direction = ParameterDirection.Input,
                        //        ParameterName = "p_EssTransaction",
                        //        UdtTypeName = "tt_Test",
                        //        OracleDbType = OracleDbType.Object,
                        //        Size = testArray.Count,
                        //        Value = testArray.ToArray()
                        //    }
                        //);

                        //sqlCommand.Parameters.Add(
                        //    new OracleParameter
                        //    {
                        //        Direction = ParameterDirection.Output,
                        //        DbType = DbType.Decimal,
                        //        OracleDbType = OracleDbType.Decimal,                                
                        //        ParameterName = "po_errorcode",

                        //    }
                        //);
                        //sqlCommand.Parameters.Add(
                        //    new OracleParameter
                        //    {
                        //        Direction = ParameterDirection.Output,
                        //        DbType = DbType.String,
                        //        OracleDbType = OracleDbType.Varchar2,                               
                        //        ParameterName = "po_errormessage"
                        //    }
                        //);

                        sqlCommand.ExecuteNonQuery();
                    }
                }
                returnResponse = (true, "");
            }
            catch (Exception ex)
            {
                returnResponse = (false, ex?.Message?.ToString() ?? "");
            }
            return returnResponse;
        }
        public (bool Success, string Message) GetUserInformation(LoginRequest model)
        {
            try
            {
                OracleProcedure procedure = new OracleProcedure(SpNameHelper.GetUserInformation);
                procedure.AddInputParameter("P_UserName", model.Username, OracleDbType.Varchar2);
                procedure.AddInputParameter("P_Password", model.Password, OracleDbType.Varchar2);

                DataTable dt = procedure.ExecuteQueryToDataTable();
                if (dt.Rows.Count > 0)
                {
                    return (true, procedure.SucessCode.ToString());
                }
                return (false, procedure.SucessCode.ToString());
            }
            catch (Exception ex)
            {
                return (false, ex?.Message?.ToString() ?? "");
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
