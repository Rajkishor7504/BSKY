using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPSBYS.Data.Model;
using Dapper;
using Dapper.Contrib;
using NLog;
using AdminConsole.Persistence;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using NLog.Fluent;
using System.Transactions;
using System.Security.Policy;
using System.Configuration;

namespace HPSBYS.Data.Services
{
    public class HospitalDetailsDataServices : BaseDataService
    {
        ILogger log = LogManager.GetCurrentClassLogger();
        public HospitalDetails GetHospitalInformation(string HospitalCode)
        {
            HospitalDetails GetHospInfoobj = new HospitalDetails();
            //string query = "Select 'Odisha' HospitalState,vchHospitalCode HospitalCode,NVL(( SELECT VCH_SH_NAME FROM MstSH S WHERE  S.INT_STATUS_FLAG = 0 AND S.INT_SH_ID = P.INTSHID ), 'NA') Hospitalname from msthospitalparticular P where trim(vchHospitalCode) = trim('02003007')";
            try
            {
                using (SqlConnecton)
                {
                    // return SqlConnecton.Query<HospitalDetails>("Exec USP_T_GetHospitalINFO @P_HospitalCode='" + HospitalCode + "'").FirstOrDefault();
                    OracleDynamicParameters parameters = new OracleDynamicParameters();
                    parameters.Add("P_HospitalCode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    // parameters.Add("cur", OracleDbType.RefCursor,ParameterDirection.Output);
                    GetHospInfoobj = SqlConnecton.Query<HospitalDetails>("USP_T_GetHospitalINFO_TMS", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    //GetHospInfoobj = SqlConnecton.Query<HospitalDetails>(query, parameters, commandType: CommandType.Text).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                GetHospInfoobj = null;
                log.Error(ex);
            }

            return GetHospInfoobj;
        }

        public IList<URNInformation> GetURNINFormation(int Schemecode, string URN)
        {
            List<URNInformation> URNInformation = new List<URNInformation>();
            using (SqlConnecton)
            {
                //URNInformation = SqlConnecton.Query<URNInformation>("Exec USP_T_GetURN_INFO @P_Schemecode=" + Schemecode + ",@P_URN='" + URN + "'").ToList();
                var parameters = new OracleDynamicParameters();
                parameters.Add("P_Schemecode", OracleDbType.Varchar2, ParameterDirection.Input, Schemecode);
                parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);

                URNInformation = SqlConnecton.Query<URNInformation>("USP_T_GetURN_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();


            }
            return URNInformation;
        }

        //public IList<Scheme> GetScheme(string Schemecode)
        //{
        //    List<Scheme> SchemeInformation = new List<Scheme>();
        //    ILogger log = LogManager.GetCurrentClassLogger();
        //    try
        //    {
        //        using (SqlConnecton)
        //        {
        //            SchemeInformation = SqlConnecton.Query<Scheme>("Exec USP_T_Scheme_INFO @P_Schemecode='" + Schemecode + "'").ToList();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        SchemeInformation = null;
        //        log.Error(ex);
        //    }

        //    return SchemeInformation;
        //}

        //public IList<PACKAGECATEGORY> GetPACKAGECATEGORY(string Action)
        //{
        //    List<PACKAGECATEGORY> GetPACKAGECATEGORYInfo = new List<PACKAGECATEGORY>();
        //    ILogger log = LogManager.GetCurrentClassLogger();
        //    try
        //    {
        //        using (SqlConnecton)
        //        {
        //            GetPACKAGECATEGORYInfo = SqlConnecton.Query<PACKAGECATEGORY>("Exec USP_MOB_GetProcedures @P_Action='" + Action + "'").ToList();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        GetPACKAGECATEGORYInfo = null;
        //        log.Error(ex);
        //    }

        //    return GetPACKAGECATEGORYInfo;
        //}



        //public IList<PackageInformation> GetPackageDetail(string Action, string PACKAGECATEGORY)
        //{
        //    List<PackageInformation> GetPackageInfo = new List<PackageInformation>();
        //    ILogger log = LogManager.GetCurrentClassLogger();
        //    try
        //    {
        //        using (SqlConnecton)
        //        {
        //            GetPackageInfo = SqlConnecton.Query<PackageInformation>("Exec USP_MOB_GetPackages @P_Action='" + Action + "', @P_PackageCategoryCode='" + PACKAGECATEGORY + "'").ToList();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        GetPackageInfo = null;
        //        log.Error(ex);
        //    }

        //    return GetPackageInfo;
        //}






        //public IList<Registration> GetRegistrationInformation(string Action,string HospitalCode)
        //{
        //    List<Registration> RegistrationInfo = new List<Registration>();
        //    ILogger log = LogManager.GetCurrentClassLogger();
        //    try
        //    {
        //        using (SqlConnecton)
        //        {
        //             RegistrationInfo = SqlConnecton.Query<Registration>("Exec USP_T_GetRegistration_INFO @P_Action='" + Action + "',@P_Hospitalcode='" + HospitalCode + "'").ToList();

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        RegistrationInfo = null;
        //        log.Error(ex);
        //    }

        //    return RegistrationInfo;
        //}



        public IList<PatientRegistrationInformation> GetRegistrationInformation(string Action, string HospitalCode, string URN, string BlockingINVOICENO, string TransactionID)
        {
            List<PatientRegistrationInformation> RegistrationInfo = new List<PatientRegistrationInformation>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("Exec USP_T_GetRegistration_INFO @P_Action='" + Action + "',@P_Hospitalcode='" + HospitalCode + "',@P_URN='" + URN + "',@P_BLOCKINGINVOICENO='" + BlockingINVOICENO + "',@P_TransactionID='" + TransactionID + "'").ToList();


                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("P_CaseNo", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    parameters.Add("P_BLOCKINGINVOICENO", OracleDbType.Varchar2, ParameterDirection.Input, BlockingINVOICENO);
                    parameters.Add("P_TransactionID", OracleDbType.Int64, ParameterDirection.Input, TransactionID);

                    RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("USP_T_GetRegistration_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                RegistrationInfo = null;
                log.Error(ex);
            }

            return RegistrationInfo;
        }
        public IList<Registration> GetRegistrationInformationByInvoiceNo(string Action, string HospitalCode, string URN, string BlockingINVOICENO, string TransactionID)
        {
            List<Registration> RegistrationInfo = new List<Registration>();
           // ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //RegistrationInfo = SqlConnecton.Query<Registration>("Exec USP_T_GetRegistration_INFO @P_Action='" + Action + "',@P_Hospitalcode='" + HospitalCode + "',@P_URN='" + URN + "',@P_BLOCKINGINVOICENO='" + BlockingINVOICENO + "',@P_TransactionID='" + TransactionID + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    parameters.Add("P_BLOCKINGINVOICENO", OracleDbType.Varchar2, ParameterDirection.Input, BlockingINVOICENO);
                    parameters.Add("P_TransactionID", OracleDbType.Int64, ParameterDirection.Input, TransactionID);

                    RegistrationInfo = SqlConnecton.Query<Registration>("USP_T_GetRegistration_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                RegistrationInfo = null;
                log.Error(ex);
            }

            return RegistrationInfo;
        }
        public IList<UnblockingInfo> GetUnblockingInformation(string HospitalCode, string URN)
        {
            List<UnblockingInfo> UnblockingInfo = new List<UnblockingInfo>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //UnblockingInfo = SqlConnecton.Query<UnblockingInfo>("Exec USP_T_Admission_INFO @P_Hospitalcode='" + HospitalCode + "',@URN='" + URN + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("v_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    parameters.Add("v_P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);


                    UnblockingInfo = SqlConnecton.Query<UnblockingInfo>("USP_T_Admission_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();


                }

            }
            catch (Exception ex)
            {
                UnblockingInfo = null;
                log.Error(ex);
            }

            return UnblockingInfo;
        }

        public IList<DischargeSummary> GetDischargeInformation(string Action, string HospitalCode, string URN)
        {
            List<DischargeSummary> DischargeSummaryInfo = new List<DischargeSummary>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //DischargeSummaryInfo = SqlConnecton.Query<DischargeSummary>("Exec USP_T_GetDischarge_INFO  @P_Action='" + Action + "', @P_Hospitalcode='" + HospitalCode + "', @P_URN='" + URN + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                   
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);

                    DischargeSummaryInfo = SqlConnecton.Query<DischargeSummary>("USP_T_GetDischarge_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                DischargeSummaryInfo = null;
                log.Error(ex);
            }

            return DischargeSummaryInfo;
        }

        public IList<DischargeInformation> GetDischargeDetail(string Action, string HospitalCode, string URN, string BlockingInvoiceNo)
        {
            List<DischargeInformation> DischargeSummaryInfo = new List<DischargeInformation>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //DischargeSummaryInfo = SqlConnecton.Query<DischargeInformation>("Exec USP_T_GetDischarge_INFO  @P_Action='" + Action + "', @P_Hospitalcode='" + HospitalCode + "', @P_URN='" + URN + "', @P_BLOCKINGINVOICENO='" + BlockingInvoiceNo + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    parameters.Add("P_BLOCKINGINVOICENO", OracleDbType.Varchar2, ParameterDirection.Input, BlockingInvoiceNo);

                    DischargeSummaryInfo = SqlConnecton.Query<DischargeInformation>("USP_T_GetDischarge_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                DischargeSummaryInfo = null;
                log.Error(ex);
            }

            return DischargeSummaryInfo;
        }
        //Package Extension

        public IList<PackageExtension> GetPackageExtensionDetail(string Scheme, string HospitalCode, string URN)
        {
            List<PackageExtension> PackageExtensionDetail = new List<PackageExtension>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //PackageExtensionDetail = SqlConnecton.Query<PackageExtension>("Exec USP_T_GetPackageExtenInfo  @P_Scheme='" + Scheme + "', @P_HospitalCode='" + HospitalCode + "', @P_URN='" + URN + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_Scheme", OracleDbType.Varchar2, ParameterDirection.Input, Scheme);
                    parameters.Add("P_HospitalCode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);

                    PackageExtensionDetail = SqlConnecton.Query<PackageExtension>("USP_T_GetPackageExtenInfo", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                PackageExtensionDetail = null;
                log.Error(ex);
            }
            return PackageExtensionDetail;
        }
        public IList<PatientRegistrationInformation> GetWardBlockPackageInformation(string Action, string HospitalCode, string URN, string BlockingINVOICENO, string TransactionID)
        {
            List<PatientRegistrationInformation> RegistrationInfo = new List<PatientRegistrationInformation>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("Exec USP_T_GetWardBlockPackage_INFO @P_Action='" + Action + "',@P_Hospitalcode='" + HospitalCode + "',@P_URN='" + URN + "',@P_BLOCKINGINVOICENO='" + BlockingINVOICENO + "',@P_TransactionID='" + TransactionID + "'").ToList();
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("v_P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("v_P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("v_P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    parameters.Add("v_P_BLOCKINGINVOICENO", OracleDbType.Varchar2, ParameterDirection.Input, BlockingINVOICENO);
                    parameters.Add("v_P_TransactionID", OracleDbType.Int64, ParameterDirection.Input, TransactionID);
                    parameters.Add("v_P_DischargeDate", OracleDbType.Varchar2, ParameterDirection.Input, "");

                    RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("USP_T_GetWardBlockPackage_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                RegistrationInfo = null;
                log.Error(ex);
            }

            return RegistrationInfo;
        }
        public IList<PatientRegistrationInformation> GetPackageChangeInformation(string Action, string HospitalCode, string URN, string BlockingINVOICENO, string TransactionID)
        {
            List<PatientRegistrationInformation> RegistrationInfo = new List<PatientRegistrationInformation>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    // RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("Exec USP_T_GetPackageChange_INFO @P_Action='" + Action + "',@P_Hospitalcode='" + HospitalCode + "',@P_URN='" + URN + "',@P_BLOCKINGINVOICENO='" + BlockingINVOICENO + "',@P_TransactionID='" + TransactionID + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    parameters.Add("P_BLOCKINGINVOICENO", OracleDbType.Varchar2, ParameterDirection.Input, BlockingINVOICENO);
                    parameters.Add("P_TransactionID", OracleDbType.Int64, ParameterDirection.Input, TransactionID);

                    RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("USP_T_GetPackageChange_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                RegistrationInfo = null;
                log.Error(ex);
            }

            return RegistrationInfo;
        }
        public IList<WardInfo> GetPreviousWardDetailsByTranId(string Action, string TransactionID, string DischargeDate)
        {
            List<WardInfo> RegistrationInfo = new List<WardInfo>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //RegistrationInfo = SqlConnecton.Query<WardInfo>("Exec USP_T_GetWardBlockPackage_INFO @P_Action='" + Action + "',@P_TransactionID='" + TransactionID + "',@P_DischargeDate='" + DischargeDate + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("v_P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("v_P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, DBNull.Value);
                    parameters.Add("v_P_URN", OracleDbType.Varchar2, ParameterDirection.Input, DBNull.Value);
                    parameters.Add("v_P_BLOCKINGINVOICENO", OracleDbType.Varchar2, ParameterDirection.Input, DBNull.Value);
                    parameters.Add("v_P_TransactionID", OracleDbType.Varchar2, ParameterDirection.Input, TransactionID);
                    parameters.Add("v_P_DischargeDate", OracleDbType.Varchar2, ParameterDirection.Input, DischargeDate);

                    RegistrationInfo = SqlConnecton.Query<WardInfo>("USP_T_GetWardBlockPackage_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                RegistrationInfo = null;
                log.Error(ex);
            }

            return RegistrationInfo;
        }
        public IList<AddOnInfo> GetPreviousAddOnPackageDetailsByInvoiceNo(string Action, string InvoiceNo)
        {
            List<AddOnInfo> RegistrationInfo = new List<AddOnInfo>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //RegistrationInfo = SqlConnecton.Query<AddOnInfo>("Exec USP_T_GetAddOnBlockPackage_INFO @P_Action='" + Action + "',@P_BLOCKINGINVOICENO='" + InvoiceNo + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("v_P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("v_P_BLOCKINGINVOICENO", OracleDbType.Varchar2, ParameterDirection.Input, InvoiceNo);

                    RegistrationInfo = SqlConnecton.Query<AddOnInfo>("USP_T_GetAddOnBlockPackage_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                RegistrationInfo = null;
                log.Error(ex);
            }

            return RegistrationInfo;
        }
        public IList<PatientRegistrationInformation> GetWardPreAuthInformation(string Action, string HospitalCode, string URN)

        {

            List<PatientRegistrationInformation> RegistrationInfo = new List<PatientRegistrationInformation>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {

                using (SqlConnecton)
                {
                    // RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("Exec [USP_T_GetWardBlockPackage_INFO] @P_Action='" + Action + "',@P_Hospitalcode='" + HospitalCode + "',@P_URN='" + URN + "'").ToList();
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("v_P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("v_P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("v_P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    parameters.Add("v_P_BLOCKINGINVOICENO", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("v_P_TransactionID", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("v_P_DischargeDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("USP_T_GetWardBlockPackage_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                RegistrationInfo = null;
                log.Error(ex);
            }

            return RegistrationInfo;

        }
        public IList<PatientRegistrationInformation> GetPackageChangePreAuthInformation(string Action, string HospitalCode, string URN)
        {
            List<PatientRegistrationInformation> RegistrationInfo = new List<PatientRegistrationInformation>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("Exec [USP_T_GetPackageChangePreAuth_INFO] @P_Action='" + Action + "',@P_Hospitalcode='" + HospitalCode + "',@P_URN='" + URN + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("v_P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("v_P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("v_P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("USP_T_GetPackageChangePreAuth_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                RegistrationInfo = null;
                log.Error(ex);
            }

            return RegistrationInfo;
        }
        public IList<PatientRegistrationInformation> GetAddOnPreAuthInformation(string Action, string HospitalCode, string URN)
        {
            List<PatientRegistrationInformation> RegistrationInfo = new List<PatientRegistrationInformation>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("Exec [USP_T_GetAddOnBlockPackage_INFO] @P_Action='" + Action + "',@P_Hospitalcode='" + HospitalCode + "',@P_URN='" + URN + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("v_P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("v_P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("v_P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);                   
                    RegistrationInfo = SqlConnecton.Query<PatientRegistrationInformation>("USP_T_GetAddOnBlockPackage_INFO", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                RegistrationInfo = null;
                log.Error(ex);
            }

            return RegistrationInfo;
        }

        #region :: KISAN
        public IList<UnblockingInfo> GetUnblockingInformationByMember(string action, string urn, string memberId, string hospitalcode, int transactionID)
        {
            List<UnblockingInfo> UnblockingInfo = new List<UnblockingInfo>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //UnblockingInfo = SqlConnecton.Query<UnblockingInfo>("Exec USP_T_Admission_INFO @P_Hospitalcode='" + HospitalCode + "',@URN='" + URN + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, action);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, urn);
                    parameters.Add("P_MEMBERID", OracleDbType.Varchar2, ParameterDirection.Input, memberId);
                    parameters.Add("P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, hospitalcode);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, transactionID);
                    UnblockingInfo = SqlConnecton.Query<UnblockingInfo>("USP_T_Admission_INFO_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                UnblockingInfo = null;
                log.Error(ex);
            }

            return UnblockingInfo;
        }
        #endregion
        public IList<VitalParameterModel> GetVitalListByMember(string action, string urn, string memberId, string hospitalcode, int transactionID)
        {
            List<VitalParameterModel> UnblockingInfo = new List<VitalParameterModel>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    //UnblockingInfo = SqlConnecton.Query<UnblockingInfo>("Exec USP_T_Admission_INFO @P_Hospitalcode='" + HospitalCode + "',@URN='" + URN + "'").ToList();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, action);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, urn);
                    parameters.Add("P_MEMBERID", OracleDbType.Varchar2, ParameterDirection.Input, memberId);
                    parameters.Add("P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, hospitalcode);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, transactionID);
                    UnblockingInfo = SqlConnecton.Query<VitalParameterModel>("USP_T_Admission_INFO_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                UnblockingInfo = null;
                log.Error(ex);
            }

            return UnblockingInfo;
        }

        public IList<UnblockingInfo> GetUnblockingInformationByMemberNew(UnblockSearchModel obj)
        {
            List<UnblockingInfo> UnblockingInfo = new List<UnblockingInfo>();
            //ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, obj.Action);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.Urn);
                    parameters.Add("P_MEMBERID", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberId);
                    parameters.Add("P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, obj.TransactionId);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.FromDate);
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.ToDate);
                    parameters.Add("P_SEARCHTYPE", OracleDbType.Varchar2, ParameterDirection.Input, obj.AdmissionDateType);
                    UnblockingInfo = SqlConnecton.Query<UnblockingInfo>("USP_T_Admission_INFO_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                UnblockingInfo = null;
                log.Error(ex);
            }
            return UnblockingInfo;
        }

        public CaseInformationDetails GetCaseInformationDetails(CaseInformationModel obj)
        {
            CaseInformationDetails caseInformationData = new CaseInformationDetails();
            using (SqlConnecton)
            {
                var parameters = new OracleDynamicParameters();
                parameters.Add("P_USERID", OracleDbType.Int64, ParameterDirection.Input, obj.userid);
                parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, obj.action);
                parameters.Add("P_PREAUTHID", OracleDbType.Int64, ParameterDirection.Input, obj.preauthid);
                parameters.Add("P_TXNPACKAGEDETAILID", OracleDbType.Int64, ParameterDirection.Input, obj.packagedetailid);
                parameters.Add("P_CASENO", OracleDbType.Varchar2, ParameterDirection.Input, null);
                var result = SqlConnecton.QueryMultiple("USP_SNA_PACKAGE_DETAILS_MOB", parameters, commandType: CommandType.StoredProcedure);
                caseInformationData.packageInformation = result.Read<packageInformationModel>().Select(s => new packageInformationModel()
                {
                    patientname = s.patientname,
                    patientphoto = s.patientphoto == "--" ? "--" : ConfigurationManager.AppSettings["ServiceURL"] + "/api/Common/viewFile" + "?encodedString=" + CommonExtension.encryptString(s.year + "/" + int.Parse(s.hospitalcode) + "/PatientPic/" + s.patientphoto),
                    admissiontype = s.admissiontype,
                    procedurecode = s.procedurecode,
                    procedurename = s.procedurename,
                    doctorname = s.doctorname,
                    packagecode = s.packagecode,
                    packagename = s.packagename,
                    preauthstatus = s.preauthstatus,
                    year = s.year,
                    hospitalcode = s.hospitalcode
                }).FirstOrDefault();
                caseInformationData.Blocked = result.Read<BlockedInformationModel>().Select(s => new BlockedInformationModel()
                {
                    packagecost = s.packagecost,
                    amountblocked = s.amountblocked,
                    blockdate = s.blockdate,
                    blockverificationmode = s.blockverificationmode,
                    verifiermembername = s.verifiermembername,
                    noofdays = s.noofdays,
                    overridecode = s.overridecode,
                    referralcode = s.referralcode,
                    expiredate = s.expiredate
                }).ToList();
                caseInformationData.UnBlocked = result.Read<UnBlockedInformationModel>().Select(s => new UnBlockedInformationModel()
                {
                    unblockdate = s.unblockdate,
                    unblockinginvoicenumber = s.unblockinginvoicenumber,
                    unblockingdescription = s.unblockingdescription,
                    unblockverificationmode = s.unblockverificationmode,
                    unblockingoverridecode = s.unblockingoverridecode,
                    unblockingverifiermembername = s.unblockingverifiermembername,
                }).ToList();
                caseInformationData.PreAuthDetails = result.Read<PreAuthDetailsModel>().Select(s => new PreAuthDetailsModel()
                {
                    requestdate = s.requestdate,
                    hospitalrequestamount = s.hospitalrequestamount,
                    insufficientamount = s.insufficientamount,
                    requestdescription = s.requestdescription,
                    snaapprovedamount = s.snaapprovedamount,
                    approveddate = s.approveddate,
                    snadescription = s.snadescription,
                    firstquerydate = s.firstquerydate,
                    snadescriptionforfirstquery = s.snadescriptionforfirstquery,
                    firstqueryreplydate = s.firstqueryreplydate,
                    firstqueryreplybyhospital = s.firstqueryreplybyhospital,
                    secondquerydate = s.secondquerydate,
                    snadescriptionforsecondquery = s.snadescriptionforsecondquery,
                    secondqueryreplydate = s.secondqueryreplydate,
                    secondqueryreplybyhospital = s.secondqueryreplybyhospital,
                    docName1 = s.docName1,
                    docLink1 = s.docName1 == "--" ? "--" : ConfigurationManager.AppSettings["ServiceURL"] + "/api/Common/viewFile" + "?encodedString=" + CommonExtension.encryptString(s.year + "/" + int.Parse(s.hospitalcode) + "/PREAUTHDOC/" + s.docName1),
                    docName2 = s.docName2,
                    docLink2 = s.docName2 == "--" ? "--" : ConfigurationManager.AppSettings["ServiceURL"] + "/api/Common/viewFile" + "?encodedString=" + CommonExtension.encryptString(s.year + "/" + int.Parse(s.hospitalcode) + "/PREAUTHDOC/" + s.docName2),
                    docName3 = s.docName3,
                    docLink3 = s.docName3 == "--" ? "--" : ConfigurationManager.AppSettings["ServiceURL"] + "/api/Common/viewFile" + "?encodedString=" + CommonExtension.encryptString(s.year + "/" + int.Parse(s.hospitalcode) + "/PREAUTHDOC/" + s.docName3),
                    year = s.year,
                    hospitalcode = s.hospitalcode
                }).ToList();
                caseInformationData.highEndDrugs = result.Read<highEndDrugsModel>().Select(s => new highEndDrugsModel()
                {
                    hedname = s.hedname,
                    unit = s.unit,
                    price = s.price,
                    totalPrice = s.totalPrice
                }).ToList();
                caseInformationData.implantData = result.Read<implantDataModel>().Select(s => new implantDataModel()
                {
                    implantname = s.implantname,
                    unit = s.unit,
                    price = s.price,
                    totalPrice = s.totalPrice
                }).ToList();
                caseInformationData.wardInfo = result.Read<wardInfoModel>().Select(s => new wardInfoModel()
                {
                    wardname = s.wardname,
                    wardamount = s.wardamount,
                    wardblockdate = s.wardblockdate,
                    status = s.status
                }).ToList();
                caseInformationData.vitalInformation = result.Read<vitalInformationModel>().Select(s => new vitalInformationModel()
                {
                    vitalsign = s.vitalsign,
                    vitalvalue = s.vitalvalue
                }).ToList();
                return caseInformationData;
            }
        }
        public TreatmentInformationModel getTreatmentDetails(CaseInformationModel obj)
        {
            TreatmentInformationModel TreatmentInformationData = new TreatmentInformationModel();
            using (SqlConnecton)
            {
                var parameters = new OracleDynamicParameters();
                parameters.Add("P_USERID", OracleDbType.Int64, ParameterDirection.Input, obj.userid);
                //parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                //parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, obj.action);
                //parameters.Add("P_PREAUTHID", OracleDbType.Int64, ParameterDirection.Input, obj.preauthid);
                parameters.Add("P_TXNPACKAGEDETAILID", OracleDbType.Int64, ParameterDirection.Input, obj.packagedetailid);
                //parameters.Add("P_CASENO", OracleDbType.Varchar2, ParameterDirection.Input, obj.caseno);
                var result = SqlConnecton.QueryMultiple("USP_SNA_PREAUTH_DETAILS_MOB", parameters, commandType: CommandType.StoredProcedure);
                TreatmentInformationData.hospitalInfo = result.Read<hospitalInfoModel>().Select(s => new hospitalInfoModel()
                {
                    hospitalname = s.hospitalname,
                    hospitalcode = s.hospitalcode,
                    hospitalcategory = s.hospitalcategory,
                    caseno = s.caseno
                }).FirstOrDefault();
                TreatmentInformationData.patientInfo = result.Read<patientInfoModel>().Select(s => new patientInfoModel()
                {
                    urn = s.urn,
                    patientname = s.patientname,
                    patientmobileno = s.patientmobileno,
                    verifiedmembername = s.verifiedmembername,
                    verifiedthrough = s.verifiedthrough,
                    mobilenoverfiedthroughotp = s.mobilenoverfiedthroughotp,
                }).FirstOrDefault();
                TreatmentInformationData.treatmentInfo = result.Read<treatmentInfoModel>().Select(s => new treatmentInfoModel()
                {
                    requesteddate = s.requesteddate,
                    procedurecode = s.procedurecode,
                    procedurename = s.procedurename,
                    packagecode = s.packagecode,
                    packagename = s.packagename,
                    packagecost = s.packagecost,
                    wardname = s.wardname,
                    hospitalizationdays = s.hospitalizationdays,
                }).ToList();
                TreatmentInformationData.implantData = result.Read<implantDataModel>().Select(s => new implantDataModel()
                {
                    implantname = s.implantname,
                    unit = s.unit,
                    price = s.price,
                    totalPrice = s.totalPrice
                }).ToList();
                TreatmentInformationData.highEndDrugs = result.Read<highEndDrugsModel>().Select(s => new highEndDrugsModel()
                {
                    hedname = s.hedname,
                    unit = s.unit,
                    price = s.price,
                    totalPrice = s.totalPrice
                }).ToList();
                TreatmentInformationData.costDetails = result.Read<costDetailsModel>().Select(s => new costDetailsModel()
                {
                    totalpackagecost = s.totalpackagecost,
                    familyfund = s.familyfund,
                    femalefund = s.femalefund,
                    insufficientfund = s.insufficientfund,
                }).ToList();
                TreatmentInformationData.latestDocument = result.Read<latestDocumentModel>().Select(s => new latestDocumentModel()
                {
                    uploadDate = s.uploadDate,
                    description = s.description,
                    latestDoc = s.latestDoc,
                    latestDoclink = s.latestDoc == "--" ? "--" : ConfigurationManager.AppSettings["ServiceURL"] + "/api/Common/viewFile" + "?encodedString=" + CommonExtension.encryptString(s.year + "/" + int.Parse(s.hospitalcode) + "/PREAUTHDOC/" + s.latestDoc),
                    year = s.year,
                    hospitalcode = s.hospitalcode,
                    snaRemarkDate = s.snaRemarkDate,
                    snaRemark = s.snaRemark,
                    snaDescription = s.snaDescription
                }).FirstOrDefault();
                TreatmentInformationData.actionHistory = result.Read<ActionHistoryModel>().Select(s => new ActionHistoryModel()
                {
                    actionby = s.actionby,
                    actionon = s.actionon,
                    actiontype = s.actiontype,
                    actionamount = s.actionamount,
                    remark = s.remark,
                    description = s.description,
                    docname = s.docname,
                    doclink = s.docname == "--" ? "--" : ConfigurationManager.AppSettings["ServiceURL"] + "/api/Common/viewFile" + "?encodedString=" + CommonExtension.encryptString(s.year + "/" + int.Parse(s.hospitalcode) + "/PREAUTHDOC/" + s.docname),
                    year= s.year,
                    hospitalcode =s.hospitalcode
                }).ToList();
                TreatmentInformationData.onGoingTreatmentDetails = result.Read<OnGoingTreatmentDetailModel>().Select(s => new OnGoingTreatmentDetailModel()
                {
                    caseno = s.caseno,
                    patientname = s.patientname,
                    procedurecode = s.procedurecode,
                    procedurename = s.procedurename,
                    packagecode = s.packagecode,
                    admissiondate = s.admissiondate,
                    authmode = s.authmode,
                    currentstatus = s.currentstatus,
                    actiondate = s.actiondate,
                    blockedamount = s.blockedamount,
                }).ToList();
                TreatmentInformationData.allPreAuthInfo = result.Read<AllPreAuthInfoModel>().Select(s => new AllPreAuthInfoModel()
                {
                    patientname = s.patientname,
                    procedurecode = s.procedurecode,
                    packagecode = s.packagecode,
                    packagename = s.packagename,
                    requestamount = s.requestamount,
                    approvedamount = s.approvedamount,
                    blockamount = s.blockamount,
                    preauth = s.preauth,
                    status = s.status,
                    snaremark = s.snaremark,
                    prauthrequestdate = s.prauthrequestdate,
                    packagedetailid = s.packagedetailid,
                    preauthid = s.preauthid
                }).ToList();
                TreatmentInformationData.dischargedTreatmentInfo = result.Read<dischargedTreatmentInfoModel>().Select(s => new dischargedTreatmentInfoModel()
                {

                    patientname = s.patientname,
                    claimno = s.claimno,
                    urn = s.urn,
                    packagecode = s.packagecode,
                    hospitalname = s.hospitalname,
                    admissiondate = s.admissiondate,
                    actualadmissiondate = s.actualadmissiondate,
                    dischargedate = s.dischargedate,
                    actualdischargedate = s.actualdischargedate,
                    hospitalclaimamount = s.hospitalclaimamount,
                    cpdapprovedamount = s.cpdapprovedamount,
                    snaapprovedamount = s.snaapprovedamount,
                    status = s.status,
                    cpdname = s.cpdname,
                    claimid = s.claimid,
                    claimraisestatus = s.claimraisestatus,
                    claimstatus = s.claimstatus,
                    transactiondetailsid = s.transactiondetailsid
                }).ToList();
                TreatmentInformationData.oldClaimInfo = result.Read<OldClaimInfoModel>().Select(s => new OldClaimInfoModel()
                {
                    patientname = s.patientname,
                    urn = s.urn,
                    hospitalname = s.hospitalname,
                    admissiondate = s.admissiondate,
                    actualadmissiondate = s.actualadmissiondate,
                    dischargedate = s.dischargedate,
                    actualdischargedate = s.actualdischargedate,
                    snaapprovedamount = s.snaapprovedamount,
                    claimstatus= s.claimstatus,
                    approvedamount = s.approvedamount,
                    approveddate = s.approveddate,
                    snaapproveddate = s.snaapproveddate,
                    remark = s.remark,
                    snaremark = s.snaremark,
                }).ToList();
                return TreatmentInformationData;
            }
        }
    }
}
