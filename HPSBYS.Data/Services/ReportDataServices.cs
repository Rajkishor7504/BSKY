using AdminConsole.Persistence;
using Dapper;
using HPSBYS.Data.Model;
using NLog;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Services
{
    public class ReportDataServices : BaseDataService
    {
        ILogger log = LogManager.GetCurrentClassLogger();

        public List<AdmissionStats> AdmissionReportService(ReportModel reportModel) // ADDED by Akshat (16-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_GENDER", reportModel.Gender);
                param.Add("P_SEARCHTYPE", reportModel.AdmissionDateType);
                var procedureName = "USP_T_GETREPORTSHOSPITALWISE_TMS";

                var result = SqlConnecton.Query<AdmissionStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<BlockedStats> BlockedReportService(ReportModel reportModel) // ADDED by Akshat (16-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_GENDER", reportModel.Gender);
                param.Add("P_SEARCHTYPE", reportModel.AdmissionDateType);
                var procedureName = "USP_T_GETREPORTSHOSPITALWISE_TMS";

                var result = SqlConnecton.Query<BlockedStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<DischargeStats> DischargeReportService(ReportModel reportModel) // ADDED by Akshat (16-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_GENDER", reportModel.Gender);
                param.Add("P_SEARCHTYPE", reportModel.AdmissionDateType);
                var procedureName = "USP_T_GETREPORTSHOSPITALWISE_TMS";

                var result = SqlConnecton.Query<DischargeStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<PreAuthStats> PreAuthReportService(ReportModel reportModel) // ADDED by Akshat (16-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_GENDER", reportModel.Gender);
                param.Add("P_SEARCHTYPE", reportModel.AdmissionDateType);
                var procedureName = "USP_T_GETREPORTSHOSPITALWISE_TMS";

                var result = SqlConnecton.Query<PreAuthStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<UnblockedStats> UnblockedReportService(ReportModel reportModel) // ADDED by Akshat (16-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_GENDER", reportModel.Gender);
                param.Add("P_SEARCHTYPE", reportModel.AdmissionDateType);
                var procedureName = "USP_T_GETREPORTSHOSPITALWISE_TMS";

                var result = SqlConnecton.Query<UnblockedStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<HospitalReferralStats> HospitalReferralReportService(ReportModel reportModel) // ADDED by Akshat (23-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_REFERRALTYPE", reportModel.Type);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                var procedureName = "USP_GET_REFERRAL_REPORT";

                var result = SqlConnecton.Query<HospitalReferralStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public IList<KnowYourStatusModel> GetKnowYourStatusReportList(KnowYourStatusModel obj) //Add By Rajkishor Patra(02-March-2023)
        {
            List<KnowYourStatusModel> Viewreportlist = new List<KnowYourStatusModel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                    parameters.Add("P_MEMBERID", OracleDbType.Int64, ParameterDirection.Input, obj.memberid);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                    Viewreportlist = SqlConnecton.Query<KnowYourStatusModel>("USP_KNOWYOURSTATUS_REPORT", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Viewreportlist = null;
                log.Error(ex);
            }
            return Viewreportlist;
        }

        public List<HospitalPreAuthStats> HospitalPreAuthReportService(ReportModel reportModel) // ADDED by Akshat (12-Apr-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_GENDER", reportModel.Gender);
                param.Add("P_STATUS", reportModel.Status);
                param.Add("P_URN", reportModel.Urn);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                var procedureName = "SP_PREAUTH_DETAILS_BYHOS";

                var result = SqlConnecton.Query<HospitalPreAuthStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public DashboardStats DashboardStatsService(ReportModel reportModel) // ADDED by Akshat (30-Mar-23)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, reportModel.HospitalCode);
                param.Add("P_FROM_DATE", OracleDbType.Date, ParameterDirection.Input, Convert.ToDateTime(reportModel.FromDate));
                param.Add("P_TO_DATE", OracleDbType.Date, ParameterDirection.Input, Convert.ToDateTime(reportModel.ToDate));
                param.Add("P_PATIENT_DETAILS", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_PROCEDURE_DETAILS", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_PREAUTH_DETAILS", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_REFERRAL_AUTHENTICATION", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_AUTHENTICATION_MODE", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_OVERRIDE", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_PATIENT_DETAILS_MONTHWISE", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_OUTBOUND_CALL", OracleDbType.RefCursor, ParameterDirection.Output);
                var procedureName = "SP_TMS_PATIENT_DETAILS";

                var result = SqlConnecton.QueryMultiple(procedureName, param, commandType: CommandType.StoredProcedure);
                DashboardStats objResult = new DashboardStats();
                objResult.patientStats = result.Read<PatientDashboardStats>().FirstOrDefault();
                objResult.procedureStats = result.Read<ProcedureDashboardStats>().FirstOrDefault();
                objResult.preAuthStats = result.Read<PreAuthDashboardStats>().FirstOrDefault();
                objResult.referralAuthStats = result.Read<ReferralAuthDashboardStats>().FirstOrDefault();
                objResult.authModeStats = result.Read<AuthModeDashboardStats>().FirstOrDefault();
                objResult.overrideCodeStats = result.Read<OverrideCodeDashboardStats>().FirstOrDefault();
                objResult.patientStats.patientStatsMonthwise = result.Read<PatientDashboardMonthwiseStats>().ToList();
                objResult.outboundCallStats = result.Read<OutboundcallDashboardStats>().FirstOrDefault();
                return objResult;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public DashboardStats adminDashboardStatsService(ReportModel reportModel) // ADDED by Akshat (26-Jun-23)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, reportModel.HospitalCode);
                param.Add("P_FROM_DATE", OracleDbType.Date, ParameterDirection.Input, Convert.ToDateTime(reportModel.FromDate));
                param.Add("P_TO_DATE", OracleDbType.Date, ParameterDirection.Input, Convert.ToDateTime(reportModel.ToDate));
                param.Add("P_STATE", OracleDbType.Varchar2, ParameterDirection.Input, reportModel.statecode);
                param.Add("P_DISTRICT", OracleDbType.Varchar2, ParameterDirection.Input, reportModel.districtcode);
                param.Add("P_PATIENT_DETAILS", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_PROCEDURE_DETAILS", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_PREAUTH_DETAILS", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_REFERRAL_AUTHENTICATION", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_AUTHENTICATION_MODE", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_OVERRIDE", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_PATIENT_DETAILS_MONTHWISE", OracleDbType.RefCursor, ParameterDirection.Output);
                param.Add("P_OUTBOUND_CALL", OracleDbType.RefCursor, ParameterDirection.Output);
                var procedureName = "USP_TMS_ADMIN_DASHBOARD";

                var result = SqlConnecton.QueryMultiple(procedureName, param, commandType: CommandType.StoredProcedure);
                DashboardStats objResult = new DashboardStats();
                objResult.patientStats = result.Read<PatientDashboardStats>().FirstOrDefault();
                objResult.procedureStats = result.Read<ProcedureDashboardStats>().FirstOrDefault();
                objResult.preAuthStats = result.Read<PreAuthDashboardStats>().FirstOrDefault();
                objResult.referralAuthStats = result.Read<ReferralAuthDashboardStats>().FirstOrDefault();
                objResult.authModeStats = result.Read<AuthModeDashboardStats>().FirstOrDefault();
                objResult.overrideCodeStats = result.Read<OverrideCodeDashboardStats>().FirstOrDefault();
                objResult.patientStats.patientStatsMonthwise = result.Read<PatientDashboardMonthwiseStats>().ToList();
                objResult.outboundCallStats = result.Read<OutboundcallDashboardStats>().FirstOrDefault();
                return objResult;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<HospitalMortalityStats> HospitalMortalityReportService(ReportModel reportModel) // ADDED by Akshat (24-Apr-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_TRANSACTIONID", Convert.ToInt32(reportModel.TransactionId));
                var procedureName = "SP_MORTALITY_REPORTBYHOS_TMS";

                if (reportModel.Action == "A")
                {
                    var result = SqlConnecton.Query<HospitalMortalityStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                    return result;
                }
                else if (reportModel.Action == "B")
                {
                    var result = SqlConnecton.QueryMultiple(procedureName, param, commandType: CommandType.StoredProcedure);
                    List<PackageData> packageDataList = result.Read<PackageData>().ToList();
                    List<ImplantData> implantDataList = result.Read<ImplantData>().ToList();
                    List<HighenDrug> hedDataList = result.Read<HighenDrug>().ToList();

                    List<HospitalMortalityStats> objResult = new List<HospitalMortalityStats>();
                    HospitalMortalityStats resultDetails = new HospitalMortalityStats()
                    {
                        PackageDetails = packageDataList,
                        ImplantDetails = implantDataList,
                        HedDetails = hedDataList
                    };
                    objResult.Add(resultDetails);
                    return objResult;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        // Package Details Reports on dt: 06-06-2023

        public List<HospitalPackageBlockStats> HospitalPackageBlockReportService(ReportModel reportModel) // ADDED by Akshat (01-Jun-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_DATESEARCHBY", reportModel.DateType);
                param.Add("P_PREAUTH", reportModel.IsPreAuth);
                param.Add("P_SURGICALTYPE", reportModel.Type);
                param.Add("P_VERIFICATIONMODE", reportModel.AuthType);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                var procedureName = "USP_PACKAGEDETAILSREPORT_TMS";

                var result = SqlConnecton.Query<HospitalPackageBlockStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<HospitalPackageUnblockStats> HospitalPackageUnblockReportService(ReportModel reportModel) // ADDED by Akshat (01-Jun-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_DATESEARCHBY", reportModel.DateType);
                param.Add("P_PREAUTH", reportModel.IsPreAuth);
                param.Add("P_VERIFICATIONMODE", reportModel.AuthType);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                var procedureName = "USP_PACKAGEDETAILSREPORT_TMS";

                var result = SqlConnecton.Query<HospitalPackageUnblockStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<HospitalPackageDischargeStats> HospitalPackageDischargeReportService(ReportModel reportModel) // ADDED by Akshat (01-Jun-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_DATESEARCHBY", reportModel.DateType);
                param.Add("P_PREAUTH", reportModel.IsPreAuth);
                param.Add("P_VERIFICATIONMODE", reportModel.AuthType);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                var procedureName = "USP_PACKAGEDETAILSREPORT_TMS";

                var result = SqlConnecton.Query<HospitalPackageDischargeStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<HospitalPackageBlockStats> adminHospitalPackageBlockReportService(ReportModel reportModel) // ADDED by Akshat (22-Jun-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_DATESEARCHBY", reportModel.DateType);
                param.Add("P_PREAUTH", reportModel.IsPreAuth);
                param.Add("P_SURGICALTYPE", reportModel.Type);
                param.Add("P_VERIFICATIONMODE", reportModel.AuthType);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_STATE", reportModel.statecode);
                param.Add("P_DISTRICT", reportModel.districtcode);
                var procedureName = "USP_ADMINPACKAGEDETAILSREPORT_TMS";

                var result = SqlConnecton.Query<HospitalPackageBlockStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<HospitalPackageUnblockStats> adminHospitalPackageUnblockReportService(ReportModel reportModel) // ADDED by Akshat (22-Jun-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_DATESEARCHBY", reportModel.DateType);
                param.Add("P_PREAUTH", reportModel.IsPreAuth);
                param.Add("P_VERIFICATIONMODE", reportModel.AuthType);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_STATE", reportModel.statecode);
                param.Add("P_DISTRICT", reportModel.districtcode);
                var procedureName = "USP_ADMINPACKAGEDETAILSREPORT_TMS";

                var result = SqlConnecton.Query<HospitalPackageUnblockStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<HospitalPackageDischargeStats> adminHospitalPackageDischargeReportService(ReportModel reportModel) // ADDED by Akshat (22-Jun-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_DATESEARCHBY", reportModel.DateType);
                param.Add("P_PREAUTH", reportModel.IsPreAuth);
                param.Add("P_VERIFICATIONMODE", reportModel.AuthType);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_STATE", reportModel.statecode);
                param.Add("P_DISTRICT", reportModel.districtcode);
                var procedureName = "USP_ADMINPACKAGEDETAILSREPORT_TMS";

                var result = SqlConnecton.Query<HospitalPackageDischargeStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        // End Package Details Reports on dt: 06-06-2023

        public List<PatientMobileVerificationStats> PatientMobileVerificationReportService(ReportModel reportModel) // ADDED by Akshat (14-Jun-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_DISTRICTCODE", reportModel.districtcode);
                param.Add("P_STATECODE", reportModel.statecode);
                var procedureName = "USP_PATIENTMOBVERIFICATION_REPORT";

                var result = SqlConnecton.Query<PatientMobileVerificationStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        //For Admin View
        public List<HospitalReferralStats> adminHospitalReferralReportService(ReportModel reportModel) // ADDED by  Rajkishor (07-jun-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_REFERRALTYPE", reportModel.Type);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_STATECODE", reportModel.statecode);
                param.Add("P_DISTRICTCODE", reportModel.districtcode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_STATUS", 0);
                param.Add("P_URN", "");
                var procedureName = "USP_ADMIN_VIEWREPORT";

                var result = SqlConnecton.Query<HospitalReferralStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }


        public List<HospitalPreAuthStats> adminHospitalPreAuthReportService(ReportModel reportModel) // ADDED by  Rajkishor (07-jun-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", "B");
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                param.Add("P_GENDER", reportModel.Gender);
                param.Add("P_STATUS", reportModel.Status);
                param.Add("P_SEARCHTYPE", "");
                param.Add("P_REFERRALTYPE", 0);
                param.Add("P_URN", reportModel.Urn);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_STATECODE", reportModel.statecode);
                param.Add("P_DISTRICTCODE", reportModel.districtcode);
                param.Add("P_USERID", reportModel.UserId);
                var procedureName = "USP_ADMIN_VIEWREPORT";

                var result = SqlConnecton.Query<HospitalPreAuthStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }



        public List<HospitalMortalityStats> adminHospitalMortalityReportService(ReportModel reportModel) // ADDED by Rajkishor (07-jun-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);


                param.Add("P_GENDER", reportModel.Gender);
                param.Add("P_STATUS", reportModel.Status);
                param.Add("P_SEARCHTYPE", "");
                param.Add("P_REFERRALTYPE", 0);
                param.Add("P_URN", reportModel.Urn);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode == null ? "0" : reportModel.HospitalCode);
                param.Add("P_STATECODE", reportModel.statecode);
                param.Add("P_DISTRICTCODE", reportModel.districtcode);
                param.Add("P_TRANSACTIONID", Convert.ToInt32(reportModel.TransactionId));
                var procedureName = "USP_ADMIN_VIEWREPORT";




                //var procedureName = "SP_MORTALITY_REPORTBYHOS_TMS";

                if (reportModel.Action == "M")
                {
                    var result = SqlConnecton.Query<HospitalMortalityStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                    return result;
                }
                else if (reportModel.Action == "N")
                {
                    var result = SqlConnecton.QueryMultiple(procedureName, param, commandType: CommandType.StoredProcedure);
                    List<PackageData> packageDataList = result.Read<PackageData>().ToList();
                    List<ImplantData> implantDataList = result.Read<ImplantData>().ToList();
                    List<HighenDrug> hedDataList = result.Read<HighenDrug>().ToList();

                    List<HospitalMortalityStats> objResult = new List<HospitalMortalityStats>();
                    HospitalMortalityStats resultDetails = new HospitalMortalityStats()
                    {
                        PackageDetails = packageDataList,
                        ImplantDetails = implantDataList,
                        HedDetails = hedDataList
                    };
                    objResult.Add(resultDetails);
                    return objResult;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }


        //END


    }
}