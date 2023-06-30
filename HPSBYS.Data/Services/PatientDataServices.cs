using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HPSBYS.Data.Model;
using NLog;
using Oracle.ManagedDataAccess.Client;
using AdminConsole.Persistence;
using Microsoft.SqlServer.Server;
using System.Linq;
using System.Configuration;

namespace HPSBYS.Data.Services
{
    public class PatientDataServices : BaseDataService
    {
        string b = string.Empty;
        string blockPkgStatus = string.Empty;
        string UnblockPkgStatus = string.Empty;
        string DischargeStatus = string.Empty;
        string CancelStatus = string.Empty;
        string NoticeStatus = string.Empty;
        ILogger log = LogManager.GetCurrentClassLogger();
        public string ManagePatientRegistration(PatientInfo obj)
        {

            //object[] objArray = new object[] {
            //"@P_ACTIONCODE", obj.ACTIONCODE
            //,"@SchemeCode", obj.SchemeCode
            //,"@MemberStatecode", obj.MemberStatecode
            //,"@DistrictCode", obj.DistrictCode
            //,"@MemberDistrictName", obj.MemberDistrictName
            //,"@BlockCode", obj.BlockCode
            //,"@PanchayatCode", obj.PanchayatCode
            //,"@VillageCode", obj.VillageCode
            //,"@URN", obj.URN
            //,"@FamilyID", obj.FamilyID
            //,"@PolicyStartDate", obj.PolicyStartDate
            //,"@PolicyEndDate", obj.PolicyEndDate
            ////,"CardType", obj.CardType
            //,"@MemberID", obj.MemberID
            //,"@PatientName", obj.PatientName
            //,"@PatientContactNumber", obj.PatientContactNumber
            //,"@Gender", obj.Gender
            //,"@PatientCardGender", obj.PatientCardGender
            //,"@Age", obj.Age
            //,"@PatientCardAge", obj.PatientCardAge
            //,"@HeadMemberID", obj.HeadMemberID
            //,"@HeadMemberName", obj.HeadMemberName
            //,"@VerifiedMemberID", obj.VerifiedMemberID
            //,"@VerifiedMemberName", obj.VerifiedMemberName
            //,"@InsuranceCompanyCode", obj.InsuranceCompanyCode
            //,"@InsurancePolicyNumber", obj.InsurancePolicyNumber
            //,"@HospitalCode", obj.HospitalCode
            //,"@HospitalName", obj.HospitalName
            //,"@HospitalState", obj.HospitalState
            //,"@HospitalDistrict", obj.HospitalDistrict
            //,"@HospitalAuthorityCode", obj.HospitalAuthorityCode
            //,"@RegistrationNo", obj.RegistrationNo
            //,"@RegistrationUserDate", obj.RegistrationUserDate
            ////,"TransactionDate", obj.TransactionDate
            ////,"BlockingInvoiceNo", obj.BlockingInvoiceNo
            ////----------------------------------------------------Used in block package
            ////,"BlockingUserDate", obj.BlockingUserDate
            ////,"DATEOFADMISSION", obj.DATEOFADMISSION
            ////___________________________________________________________________
            ////,"UnblockingInvoiceNo", obj.UnblockingInvoiceNo
            ////,"UnblockingDesc", obj.UnblockingDesc

            ////,"UnblockingSystemDate", obj.UnblockingSystemDate
            ////,"DischargeInvoiceNo", obj.DischargeInvoiceNo
            ////,"DischargeDesc", obj.DischargeDesc
            ////,"DischargeUserDate", obj.DischargeUserDate
            ////,"DATEOFDISCHARGE", obj.DATEOFDISCHARGE
            ////,"Mortality", obj.Mortality
            ////,"MortalitySummary", obj.MortalitySummary
            ////----------------------------------------------------Used in block package
            ////,"ProcedureCode", obj.ProcedureCode
            ////,"ProcedureName", obj.ProcedureName
            ////,"PackageCode", obj.PackageCode
            ////,"PackageName", obj.PackageName
            ////,"PackageCost", obj.PackageCost
            ////,"NoofDays", obj.NoofDays
            ////,"AmoutBlocked", obj.AmoutBlocked
            ////-------------------------------------------------------

            ////,"NoofDaysActual", obj.NoofDaysActual
            ////,"TotalAmountClaimed", obj.TotalAmountClaimed
            ////,"AvailableBalance", obj.AvailableBalance
            //,"@TransactionCode", obj.TransactionCode
            ////,"TotalAmtBlockedOnCard", obj.TotalAmtBlockedOnCard
            ////,"InsufficientBalanceAmount", obj.InsufficientBalanceAmount
            ////,"OriginalPackageCost", obj.OriginalPackageCost
            ////,"intClaimStatus", obj.intClaimStatus
            ////,"claimid", obj.claimid
            //,"@patientSlip", obj.patientSlip
            //,"@AuthenticationMode", obj.AuthenticationMode
            //,"@VerifiedDocumentType", obj.VerifiedDocumentType
            //,"@VerifiedDocumentName", obj.VerifiedDocumentName
            //,"@PatientPhoto", obj.PatientPhoto
            //};
            //try
            //{
            //    DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
            //    var result = SqlConnecton.Execute("USP_Transaction_AED", param, commandType: CommandType.StoredProcedure);
            //    b = param.Get<string>("P_OUTERRMSG");
            //}
            //catch (Exception ex)
            //{
            //    // throw new Exception(ex.Message);
            //    log.Error(ex);
            //}
            //return b;

            string strOutput = "";
            try
            {
                OracleParameter cdm = new OracleParameter();
                cdm.ParameterName = "v_P_OUTERRMSG";
                cdm.Direction = ParameterDirection.Output;
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("v_P_ACTIONCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.ACTIONCODE);
                dyParam.Add("v_RoundNo", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("v_SchemeCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.SchemeCode);
                dyParam.Add("v_MemberStatecode", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberStatecode);
                dyParam.Add("v_DistrictCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.DistrictCode);
                dyParam.Add("v_MemberDistrictName", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberDistrictName);
                dyParam.Add("v_BlockCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockCode);
                dyParam.Add("v_PanchayatCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PanchayatCode);
                dyParam.Add("v_VillageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.VillageCode);
                dyParam.Add("v_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                dyParam.Add("v_FamilyID", OracleDbType.Varchar2, ParameterDirection.Input, obj.FamilyID);
                dyParam.Add("v_PolicyStartDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyStartDate);
                dyParam.Add("v_PolicyEndDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyEndDate);
                dyParam.Add("v_CardType", OracleDbType.Varchar2, ParameterDirection.Input, obj.CardType);
                dyParam.Add("v_MemberID", OracleDbType.Int64, ParameterDirection.Input, obj.MemberID);
                dyParam.Add("v_PatientName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientName);
                dyParam.Add("v_PatientContactNumber", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientContactNumber);
                dyParam.Add("v_Gender", OracleDbType.Varchar2, ParameterDirection.Input, obj.Gender);
                dyParam.Add("v_PatientCardGender", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientCardGender);
                dyParam.Add("v_Age", OracleDbType.Int64, ParameterDirection.Input, obj.Age);
                dyParam.Add("v_PatientCardAge", OracleDbType.Int64, ParameterDirection.Input, obj.PatientCardAge);
                dyParam.Add("v_HeadMemberID", OracleDbType.Int64, ParameterDirection.Input, obj.HeadMemberID);
                dyParam.Add("v_HeadMemberName", OracleDbType.Varchar2, ParameterDirection.Input, obj.HeadMemberName);
                dyParam.Add("v_VerifiedMemberID", OracleDbType.Int64, ParameterDirection.Input, obj.VerifiedMemberID);
                dyParam.Add("v_VerifiedMemberName", OracleDbType.Varchar2, ParameterDirection.Input, obj.VerifiedMemberName);
                dyParam.Add("v_InsuranceCompanyCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.InsuranceCompanyCode);
                dyParam.Add("v_InsurancePolicyNumber", OracleDbType.Varchar2, ParameterDirection.Input, obj.InsurancePolicyNumber);
                dyParam.Add("v_HospitalCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode);
                dyParam.Add("v_HospitalName", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalName);
                dyParam.Add("v_HospitalState", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalState);
                dyParam.Add("v_HospitalDistrict", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalDistrict);
                dyParam.Add("v_HospitalAuthorityCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalAuthorityCode);
                dyParam.Add("v_RegistrationNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.RegistrationNo);
                dyParam.Add("v_RegistrationUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.RegistrationUserDate);
                dyParam.Add("v_TransactionDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.TransactionDate);
                dyParam.Add("iv_BlockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
                dyParam.Add("v_BlockingUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingUserDate);
                dyParam.Add("v_DATEOFADMISSION", OracleDbType.Varchar2, ParameterDirection.Input, obj.DATEOFADMISSION);
                dyParam.Add("v_UnblockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.UnblockingInvoiceNo);
                dyParam.Add("v_UnblockingDesc", OracleDbType.Varchar2, ParameterDirection.Input, obj.UnblockingDesc);
                dyParam.Add("v_UnblockingSystemDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.UnblockingSystemDate);
                dyParam.Add("v_DischargeInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeInvoiceNo);
                dyParam.Add("v_DischargeDesc", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeDesc);
                dyParam.Add("v_DischargeUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeUserDate);
                dyParam.Add("v_DATEOFDISCHARGE", OracleDbType.Varchar2, ParameterDirection.Input, obj.DATEOFDISCHARGE);
                dyParam.Add("v_Mortality", OracleDbType.Varchar2, ParameterDirection.Input, obj.Mortality);
                dyParam.Add("v_MortalitySummary", OracleDbType.Varchar2, ParameterDirection.Input, obj.MortalitySummary);
                dyParam.Add("v_ProcedureCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureCode);
                dyParam.Add("v_ProcedureName", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureName);
                dyParam.Add("v_PackageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCode);
                dyParam.Add("v_PackageName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageName);
                dyParam.Add("v_PackageCost", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCost);
                dyParam.Add("v_NoofDays", OracleDbType.Int64, ParameterDirection.Input, obj.NoofDays);
                dyParam.Add("v_AmoutBlocked", OracleDbType.Varchar2, ParameterDirection.Input, obj.AmoutBlocked);
                dyParam.Add("v_NoofDaysActual", OracleDbType.Int64, ParameterDirection.Input, obj.NoofDaysActual);
                dyParam.Add("v_TotalAmountClaimed", OracleDbType.Varchar2, ParameterDirection.Input, obj.TotalAmountClaimed);
                dyParam.Add("v_AvailableBalance", OracleDbType.Varchar2, ParameterDirection.Input, obj.AvailableBalance);
                dyParam.Add("v_TransactionCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.TransactionCode);
                dyParam.Add("v_TotalAmtBlockedOnCard", OracleDbType.Varchar2, ParameterDirection.Input, obj.TotalAmtBlockedOnCard);
                dyParam.Add("v_InsufficientBalanceAmount", OracleDbType.Varchar2, ParameterDirection.Input, obj.InsufficientBalanceAmount);
                dyParam.Add("v_OriginalPackageCost", OracleDbType.Varchar2, ParameterDirection.Input, obj.OriginalPackageCost);
                dyParam.Add("v_intClaimStatus", OracleDbType.Varchar2, ParameterDirection.Input, obj.intClaimStatus);
                dyParam.Add("v_claimid", OracleDbType.Varchar2, ParameterDirection.Input, obj.claimid);
                dyParam.Add("v_statusflag", OracleDbType.Int64, ParameterDirection.Input, obj.statusflag);
                dyParam.Add("v_patientSlip", OracleDbType.Varchar2, ParameterDirection.Input, obj.patientSlip);
                dyParam.Add("v_AuthenticationMode", OracleDbType.Int64, ParameterDirection.Input, obj.AuthenticationMode);
                dyParam.Add("v_VerifiedDocumentType", OracleDbType.Varchar2, ParameterDirection.Input, obj.VerifiedDocumentType);
                dyParam.Add("v_VerifiedDocumentName", OracleDbType.Varchar2, ParameterDirection.Input, obj.VerifiedDocumentName);
                dyParam.Add("v_PatientPhoto", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientPhoto);
                dyParam.Add("v_P_OUTERRMSG", OracleDbType.Varchar2, ParameterDirection.Output);
                var query = "USP_Transaction_AED";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();

                //SuccessMessages = SqlConnecton.Query<SuccessMessage>(query, dyParam, commandType: CommandType.StoredProcedure);
                //b = dyParam.Get<string>("v_P_OUTERRMSG");
                b = "1";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return b;
        }
        public string addPatientBlockPackage(PatientInfo obj)
        {
            //object[] objArray = new object[] {
            //"@P_ACTIONCODE", obj.ACTIONCODE
            //,"@BlockingInvoiceNo", obj.BlockingInvoiceNo
            //,"@BlockingUserDate", obj.BlockingUserDate
            //,"@DATEOFADMISSION", obj.BlockingUserDate
            //,"@ProcedureCode", obj.ProcedureCode
            //,"@ProcedureName", obj.ProcedureName
            //,"@PackageCode", obj.PackageCode
            //,"@PackageName", obj.PackageName
            //,"@WardId", obj.WardId
            //,"@PackageCost", obj.PackageCost
            //,"@NoofDays", obj.NoofDays
            //,"@AmoutBlocked", obj.AmoutBlocked
            //,"@TransactionCode", obj.TransactionCode
            //,"@PreAuthStatus", obj.PreAuthStatus
            //,"@VchFile",(obj.VchFile==""?"NA":obj.VchFile)
            //,"@CappedAmount", obj.CappedAmount
            //,"@IsMedSergical", obj.IsMedSergical
            //,"@Category", obj.Category
            //,"@CategoryCode", obj.CategoryCode
            //};
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("p_P_ACTIONCODE", OracleDbType.Char, ParameterDirection.Input, obj.ACTIONCODE);

                //dyParam.Add("p_BlockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
                //dyParam.Add("p_BlockingUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingUserDate);
                //dyParam.Add("p_DATEOFADMISSION", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingUserDate);
                //dyParam.Add("p_ProcedureCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureCode);
                //dyParam.Add("p_ProcedureName", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureName);
                //dyParam.Add("p_PackageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCode);
                //dyParam.Add("p_PackageName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageName);
                //dyParam.Add("p_PackageCost", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCost);
                //dyParam.Add("p_WardId", OracleDbType.Int64, ParameterDirection.InputOutput, obj.WardId);
                //dyParam.Add("p_NoofDays", OracleDbType.Int64, ParameterDirection.Input, (obj.NoofDays == "" ? "0" : obj.NoofDays));
                //dyParam.Add("p_AmoutBlocked", OracleDbType.Varchar2, ParameterDirection.Input, obj.AmoutBlocked);
                //dyParam.Add("p_TransactionCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.TransactionCode);
                //dyParam.Add("p_PreAuthStatus", OracleDbType.Varchar2, ParameterDirection.Input, obj.PreAuthStatus);
                //dyParam.Add("p_VchFile", OracleDbType.Varchar2, ParameterDirection.Input, (obj.VchFile == "" ? "NA" : obj.VchFile));
                //dyParam.Add("p_IsMedSergical", OracleDbType.Varchar2, ParameterDirection.Input, obj.IsMedSergical);
                //dyParam.Add("p_CappedAmount", OracleDbType.Varchar2, ParameterDirection.Input, obj.CappedAmount);
                //dyParam.Add("p_Category", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.Category);
                //dyParam.Add("p_CategoryCode", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.CategoryCode);




                dyParam.Add("p_RoundNo", OracleDbType.Int16, ParameterDirection.Input, 0);
                dyParam.Add("p_SchemeCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.SchemeCode);
                dyParam.Add("p_MemberStatecode", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberStatecode);
                dyParam.Add("p_DistrictCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.DistrictCode);
                dyParam.Add("p_MemberDistrictName", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberDistrictName);
                dyParam.Add("p_BlockCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockCode);
                dyParam.Add("p_PanchayatCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PanchayatCode);
                dyParam.Add("p_VillageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.VillageCode);
                dyParam.Add("p_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                dyParam.Add("p_FamilyID", OracleDbType.Varchar2, ParameterDirection.Input, obj.FamilyID);
                dyParam.Add("p_PolicyStartDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyStartDate);
                dyParam.Add("p_PolicyEndDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyEndDate);
                dyParam.Add("p_CardType", OracleDbType.Varchar2, ParameterDirection.Input, obj.CardType);
                dyParam.Add("p_MemberID", OracleDbType.Int64, ParameterDirection.Input, obj.MemberID);
                dyParam.Add("p_PatientName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientName);
                dyParam.Add("p_PatientContactNumber", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientContactNumber);
                dyParam.Add("p_Gender", OracleDbType.Varchar2, ParameterDirection.Input, obj.Gender);
                dyParam.Add("p_PatientCardGender", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientCardGender);
                dyParam.Add("p_Age", OracleDbType.Int64, ParameterDirection.Input, obj.Age);
                dyParam.Add("p_PatientCardAge", OracleDbType.Int64, ParameterDirection.Input, obj.PatientCardAge);
                dyParam.Add("p_HeadMemberID", OracleDbType.Int64, ParameterDirection.Input, obj.HeadMemberID);
                dyParam.Add("p_HeadMemberName", OracleDbType.Varchar2, ParameterDirection.Input, obj.HeadMemberName);
                dyParam.Add("p_VerifiedMemberID", OracleDbType.Int64, ParameterDirection.Input, obj.VerifiedMemberID);
                dyParam.Add("p_VerifiedMemberName", OracleDbType.Varchar2, ParameterDirection.Input, obj.VerifiedMemberName);
                dyParam.Add("p_InsuranceCompanyCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.InsuranceCompanyCode);
                dyParam.Add("p_InsurancePolicyNumber", OracleDbType.Varchar2, ParameterDirection.Input, obj.InsurancePolicyNumber);
                dyParam.Add("p_HospitalCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode);
                dyParam.Add("p_HospitalName", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalName);
                dyParam.Add("p_HospitalState", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalState);
                dyParam.Add("p_HospitalDistrict", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalDistrict);
                dyParam.Add("p_HospitalAuthorityCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalAuthorityCode);
                dyParam.Add("p_RegistrationNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.RegistrationNo);
                dyParam.Add("p_RegistrationUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.RegistrationUserDate);
                dyParam.Add("p_TransactionDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.TransactionDate);
                dyParam.Add("p_BlockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
                dyParam.Add("p_BlockingUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingUserDate);
                dyParam.Add("p_DATEOFADMISSION", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingUserDate);
                dyParam.Add("p_UnblockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.UnblockingInvoiceNo);
                dyParam.Add("p_UnblockingDesc", OracleDbType.Varchar2, ParameterDirection.Input, obj.UnblockingDesc);
                dyParam.Add("p_UnblockingSystemDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.UnblockingSystemDate);
                dyParam.Add("p_DischargeInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeInvoiceNo);
                dyParam.Add("p_DischargeDesc", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeDesc);
                dyParam.Add("p_DischargeUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeUserDate);
                dyParam.Add("p_DATEOFDISCHARGE", OracleDbType.Varchar2, ParameterDirection.Input, obj.DATEOFDISCHARGE);
                dyParam.Add("p_Mortality", OracleDbType.Varchar2, ParameterDirection.Input, obj.Mortality);
                dyParam.Add("p_MortalitySummary", OracleDbType.Varchar2, ParameterDirection.Input, obj.MortalitySummary);
                dyParam.Add("p_ProcedureCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureCode);
                dyParam.Add("p_ProcedureName", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureName);
                dyParam.Add("p_PackageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCode);
                dyParam.Add("p_PackageName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageName);
                dyParam.Add("p_PackageCost", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCost);
                //dyParam.Add("p_NoofDaysForWard", OracleDbType.Varchar2, ParameterDirection.Input, obj);
                dyParam.Add("p_NoofDays", OracleDbType.Int64, ParameterDirection.Input, (obj.NoofDays == "" ? "0" : obj.NoofDays));
                dyParam.Add("p_WardId", OracleDbType.Int64, ParameterDirection.InputOutput, obj.WardId);
                dyParam.Add("p_AmoutBlocked", OracleDbType.Varchar2, ParameterDirection.Input, obj.AmoutBlocked);
                dyParam.Add("p_NoofDaysActual", OracleDbType.Int64, ParameterDirection.Input, obj.NoofDaysActual);
                dyParam.Add("p_TotalAmountClaimed", OracleDbType.Varchar2, ParameterDirection.Input, obj.TotalAmountClaimed);
                dyParam.Add("p_AvailableBalance", OracleDbType.Varchar2, ParameterDirection.Input, obj.AvailableBalance);
                dyParam.Add("p_TransactionCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.TransactionCode);
                dyParam.Add("p_TotalAmtBlockedOnCard", OracleDbType.Varchar2, ParameterDirection.Input, obj.TotalAmtBlockedOnCard);
                dyParam.Add("p_InsufficientBalanceAmount", OracleDbType.Varchar2, ParameterDirection.Input, obj.InsufficientBalanceAmount);
                dyParam.Add("p_OriginalPackageCost", OracleDbType.Varchar2, ParameterDirection.Input, obj.OriginalPackageCost);
                dyParam.Add("p_intClaimStatus", OracleDbType.Varchar2, ParameterDirection.Input, obj.intClaimStatus);
                dyParam.Add("p_claimid", OracleDbType.Varchar2, ParameterDirection.Input, obj.claimid);
                dyParam.Add("p_statusflag", OracleDbType.Int32, ParameterDirection.Input, obj.statusflag);
                dyParam.Add("p_patientSlip", OracleDbType.Varchar2, ParameterDirection.Input, obj.patientSlip);
                dyParam.Add("p_PreAuthStatus", OracleDbType.Varchar2, ParameterDirection.Input, obj.PreAuthStatus);
                dyParam.Add("p_VchFile", OracleDbType.Varchar2, ParameterDirection.Input, (obj.VchFile == "" ? "NA" : obj.VchFile));
                dyParam.Add("p_id", OracleDbType.Varchar2, ParameterDirection.InputOutput, 0);
                dyParam.Add("p_IsMedSergical", OracleDbType.Varchar2, ParameterDirection.Input, obj.IsMedSergical);
                dyParam.Add("p_CappedAmount", OracleDbType.Varchar2, ParameterDirection.Input, obj.CappedAmount);
                dyParam.Add("p_Category", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.Category);
                dyParam.Add("p_CategoryCode", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.CategoryCode);
                dyParam.Add("p_P_OUTERRMSG", OracleDbType.Varchar2, ParameterDirection.Output);
                var query = "USP_Transaction_AED_Block";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();

                blockPkgStatus = "1";
                //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                //var result = SqlConnecton.Execute("USP_Transaction_AED_Block", param, commandType: CommandType.StoredProcedure);
                //blockPkgStatus = param.Get<string>("P_OUTERRMSG");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return blockPkgStatus;
        }
        //public string addPatientBlockPackage_PackageChange(PatientInfo obj)
        //{

        //    //object[] objArray = new object[] {
        //    //"@P_ACTIONCODE", obj.ACTIONCODE
        //    //,"@BlockingInvoiceNo", obj.BlockingInvoiceNo
        //    //,"@BlockingUserDate", obj.BlockingUserDate
        //    //,"@DATEOFADMISSION", obj.BlockingUserDate
        //    //,"@ProcedureCode", obj.ProcedureCode
        //    //,"@ProcedureName", obj.ProcedureName
        //    //,"@PackageCode", obj.PackageCode
        //    //,"@PackageName", obj.PackageName
        //    //,"@WardId", obj.WardId
        //    //,"@PackageCost", obj.PackageCost
        //    //,"@NoofDays", obj.NoofDays
        //    //,"@AmoutBlocked", obj.AmoutBlocked
        //    //,"@TransactionCode", obj.TransactionCode
        //    //,"@PreAuthStatus", obj.PreAuthStatus
        //    //,"@VchFile",(obj.VchFile==""?"NA":obj.VchFile)
        //    //,"@CappedAmount", obj.CappedAmount
        //    //,"@IsMedSergical", obj.IsMedSergical
        //    //,"@Category", obj.Category
        //    //,"@CategoryCode", obj.CategoryCode
        //    //};
        //    string strOutput = "";
        //    try
        //    {
        //        var dyParam = new OracleDynamicParameters();
        //        dyParam.Add("v_P_ACTIONCODE", OracleDbType.Char, ParameterDirection.Input, obj.ACTIONCODE);
        //        dyParam.Add("v_BlockingInvoiceNo", OracleDbType.NVarchar2, ParameterDirection.Input, 0);
        //        dyParam.Add("v_BlockingUserDate", OracleDbType.NVarchar2, ParameterDirection.Input, obj.BlockingUserDate);
        //        dyParam.Add("v_DATEOFADMISSION", OracleDbType.NVarchar2, ParameterDirection.Input, obj.DATEOFADMISSION);
        //        dyParam.Add("v_ProcedureCode", OracleDbType.NVarchar2, ParameterDirection.Input, obj.ProcedureCode);
        //        dyParam.Add("v_PackageCode", OracleDbType.NVarchar2, ParameterDirection.Input, obj.PackageCode);
        //        dyParam.Add("v_PackageName", OracleDbType.NVarchar2, ParameterDirection.Input, obj.PackageName);
        //        dyParam.Add("v_WardId", OracleDbType.Int16, ParameterDirection.Input, obj.WardId);
        //        dyParam.Add("v_PackageCost", OracleDbType.NVarchar2, ParameterDirection.Input, obj.PackageCost);
        //        dyParam.Add("v_NoofDaysActual", OracleDbType.Int16, ParameterDirection.Input, obj.NoofDaysActual);
        //        dyParam.Add("v_AmoutBlocked", OracleDbType.NVarchar2, ParameterDirection.Input, obj.AmoutBlocked);
        //        dyParam.Add("v_TransactionCode", OracleDbType.NVarchar2, ParameterDirection.Input, obj.TransactionCode);
        //        dyParam.Add("v_PreAuthStatus", OracleDbType.NVarchar2, ParameterDirection.Input, obj.PreAuthStatus);
        //        dyParam.Add("v_VchFile", OracleDbType.NVarchar2, ParameterDirection.Input, obj.VchFile);
        //        dyParam.Add("v_CappedAmount", OracleDbType.NVarchar2, ParameterDirection.Input, obj.CappedAmount);
        //        dyParam.Add("v_IsMedSergical", OracleDbType.NVarchar2, ParameterDirection.Input, obj.IsMedSergical);
        //        dyParam.Add("v_Category", OracleDbType.Char, ParameterDirection.Input, obj.Category);
        //        dyParam.Add("v_CategoryCode", OracleDbType.Int16, ParameterDirection.Input, obj.CategoryCode);
        //        dyParam.Add("v_P_OUTERRMSG", OracleDbType.RefCursor, ParameterDirection.Output);

        //        var query = "USP_Transaction_AED_PackageChangeBlock";
        //        strOutput = SqlConnecton.Query(query, dyParam, commandType: CommandType.StoredProcedure).ToString();

        //        //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
        //        //var result = SqlConnecton.Execute("USP_Transaction_AED_PackageChangeBlock", param, commandType: CommandType.StoredProcedure);
        //        //blockPkgStatus = param.Get<string>("P_OUTERRMSG");
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //    }
        //    return blockPkgStatus;
        //}
        public string addPatientBlockPackage_PackageChange(PatientInfo obj)
        {
            //object[] objArray = new object[] {
            //"@P_ACTIONCODE", obj.ACTIONCODE
            //,"@BlockingInvoiceNo", obj.BlockingInvoiceNo
            //,"@BlockingUserDate", obj.BlockingUserDate
            //,"@DATEOFADMISSION", obj.BlockingUserDate
            //,"@ProcedureCode", obj.ProcedureCode
            //,"@ProcedureName", obj.ProcedureName
            //,"@PackageCode", obj.PackageCode
            //,"@PackageName", obj.PackageName
            //,"@WardId", obj.WardId
            //,"@PackageCost", obj.PackageCost
            //,"@NoofDays", obj.NoofDays
            //,"@AmoutBlocked", obj.AmoutBlocked
            //,"@TransactionCode", obj.TransactionCode
            //,"@PreAuthStatus", obj.PreAuthStatus
            //,"@VchFile",(obj.VchFile==""?"NA":obj.VchFile)
            //,"@CappedAmount", obj.CappedAmount
            //,"@IsMedSergical", obj.IsMedSergical
            //,"@Category", obj.Category
            //,"@CategoryCode", obj.CategoryCode
            //};
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTIONCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.ACTIONCODE);
                dyParam.Add("P_RoundNo", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_SchemeCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.SchemeCode);
                dyParam.Add("P_MemberStatecode", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberStatecode);
                dyParam.Add("P_DistrictCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.DistrictCode);
                dyParam.Add("P_MemberDistrictName", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberDistrictName);
                dyParam.Add("P_BlockCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockCode);
                dyParam.Add("P_PanchayatCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PanchayatCode);
                dyParam.Add("P_VillageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.VillageCode);
                dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                dyParam.Add("P_FamilyID", OracleDbType.Varchar2, ParameterDirection.Input, obj.FamilyID);
                dyParam.Add("P_PolicyStartDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyStartDate);
                dyParam.Add("P_PolicyEndDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyEndDate);
                dyParam.Add("P_CardType", OracleDbType.Varchar2, ParameterDirection.Input, obj.CardType);
                dyParam.Add("P_MemberID", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_PatientName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientName);
                dyParam.Add("P_PatientContactNumber", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientContactNumber);
                dyParam.Add("P_Gender", OracleDbType.Varchar2, ParameterDirection.Input, obj.Gender);
                dyParam.Add("P_PatientCardGender", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientCardGender);
                dyParam.Add("P_Age", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_PatientCardAge", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_HeadMemberID", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_HeadMemberName", OracleDbType.Varchar2, ParameterDirection.Input, obj.HeadMemberName);
                dyParam.Add("P_VerifiedMemberID", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_VerifiedMemberName", OracleDbType.Varchar2, ParameterDirection.Input, obj.VerifiedMemberName);
                dyParam.Add("P_InsuranceCompanyCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.InsuranceCompanyCode);
                dyParam.Add("P_InsurancePolicyNumber", OracleDbType.Varchar2, ParameterDirection.Input, obj.InsurancePolicyNumber);
                dyParam.Add("P_HospitalCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode);
                dyParam.Add("P_HospitalName", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalName);
                dyParam.Add("P_HospitalState", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalState);
                dyParam.Add("P_HospitalDistrict", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalDistrict);
                dyParam.Add("P_HospitalAuthorityCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalAuthorityCode);
                dyParam.Add("P_RegistrationNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.RegistrationNo);
                dyParam.Add("P_RegistrationUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.RegistrationUserDate);
                dyParam.Add("P_TransactionDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.TransactionDate);
                dyParam.Add("P_BlockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
                dyParam.Add("P_BlockingUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingUserDate);
                dyParam.Add("P_DATEOFADMISSION", OracleDbType.Varchar2, ParameterDirection.Input, obj.DATEOFADMISSION);
                dyParam.Add("P_UnblockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.UnblockingInvoiceNo);
                dyParam.Add("P_UnblockingDesc", OracleDbType.Varchar2, ParameterDirection.Input, obj.UnblockingDesc);
                dyParam.Add("P_UnblockingSystemDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.UnblockingSystemDate);
                dyParam.Add("P_DischargeInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeInvoiceNo);
                dyParam.Add("P_DischargeDesc", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeDesc);
                dyParam.Add("P_DischargeUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeUserDate);
                dyParam.Add("P_DATEOFDISCHARGE", OracleDbType.Varchar2, ParameterDirection.Input, obj.DATEOFDISCHARGE);
                dyParam.Add("P_Mortality", OracleDbType.Varchar2, ParameterDirection.Input, obj.Mortality);
                dyParam.Add("P_MortalitySummary", OracleDbType.Varchar2, ParameterDirection.Input, obj.MortalitySummary);
                dyParam.Add("P_ProcedureCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureCode);
                dyParam.Add("P_ProcedureName", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureName);
                dyParam.Add("P_PackageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCode);
                dyParam.Add("P_PackageName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageName);
                dyParam.Add("P_PackageCost", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCost);
                dyParam.Add("P_NoofDays", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_WardId", OracleDbType.Int64, ParameterDirection.Input, obj.WardId);
                dyParam.Add("P_AmoutBlocked", OracleDbType.Varchar2, ParameterDirection.Input, obj.AmoutBlocked);
                dyParam.Add("P_NoofDaysActual", OracleDbType.Int64, ParameterDirection.Input, 0 /*(obj.NoofDaysActual == "" || obj.NoofDaysActual == null ? "0" : obj.NoofDaysActual)*/);
                dyParam.Add("P_TotalAmountClaimed", OracleDbType.Varchar2, ParameterDirection.Input, obj.TotalAmountClaimed);
                dyParam.Add("P_AvailableBalance", OracleDbType.Varchar2, ParameterDirection.Input, obj.AvailableBalance);
                dyParam.Add("P_TransactionCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.TransactionCode);
                dyParam.Add("P_TotalAmtBlockedOnCard", OracleDbType.Varchar2, ParameterDirection.Input, obj.TotalAmtBlockedOnCard);
                dyParam.Add("P_InsufficientBalanceAmount", OracleDbType.Varchar2, ParameterDirection.Input, obj.InsufficientBalanceAmount);
                dyParam.Add("P_OriginalPackageCost", OracleDbType.Varchar2, ParameterDirection.Input, obj.OriginalPackageCost);
                dyParam.Add("P_intClaimStatus", OracleDbType.Varchar2, ParameterDirection.Input, obj.intClaimStatus);
                dyParam.Add("P_claimid", OracleDbType.Varchar2, ParameterDirection.Input, obj.claimid);
                dyParam.Add("P_statusflag", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_patientSlip", OracleDbType.Varchar2, ParameterDirection.Input, obj.patientSlip);
                dyParam.Add("P_PreAuthStatus", OracleDbType.Varchar2, ParameterDirection.Input, obj.PreAuthStatus);
                dyParam.Add("P_VchFile", OracleDbType.Varchar2, ParameterDirection.Input, (obj.VchFile == "" ? "NA" : obj.VchFile));
                //dyParam.Add("P_id", OracleDbType.Varchar2, ParameterDirection.InputOutput, DBNull.Value); -- chaged by ashutosh pradhan on 311222
                dyParam.Add("P_IsMedSergical", OracleDbType.Varchar2, ParameterDirection.Input, obj.IsMedSergical);
                dyParam.Add("P_CappedAmount", OracleDbType.Varchar2, ParameterDirection.Input, obj.CappedAmount);
                dyParam.Add("P_Category", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.Category);
                dyParam.Add("P_CategoryCode", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.CategoryCode);
                dyParam.Add("P_OUTERRMSG", OracleDbType.Varchar2, ParameterDirection.Output);

                var query = "USP_Transaction_AED_PackageChangeBlock";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
                blockPkgStatus = "1";
                //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                //var result = SqlConnecton.Execute("USP_Transaction_AED_PackageChangeBlock", param, commandType: CommandType.StoredProcedure);
                //blockPkgStatus = param.Get<string>("P_OUTERRMSG");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return blockPkgStatus;
        }

        public string addRoomTypeDetails(PatientInfo obj)
        {
            //object[] objArray = new object[] {
            //"@P_ACTIONCODE", obj.ACTIONCODE
            //,"@TransactionId", obj.TransactionID
            //,"@PackageCode", obj.PackageCode
            //,"@WardId", obj.WardId
            //,"@PreAuthStatus", obj.PreAuthStatus
            //,"@PreAuthDoc",(obj.VchFile==""?"NA":obj.VchFile)
            //,"@Amount", obj.AmoutBlocked
            //,"@BlockingInvoiceNo", obj.BlockingInvoiceNo
            //,"@WardBlockingDate", obj.BlockingUserDate
            //,"@HospitalCode", obj.HospitalCode
            //};

            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("p_ACTIONCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.ACTIONCODE);
                dyParam.Add("p_TransactionId", OracleDbType.Int64, ParameterDirection.Input, obj.TransactionID);
                dyParam.Add("p_PackageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCode);
                dyParam.Add("p_WardId", OracleDbType.Int64, ParameterDirection.Input, obj.WardId);
                dyParam.Add("p_WardLogId", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("p_CancelReason", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_CancelDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_PreAuthStatus", OracleDbType.Varchar2, ParameterDirection.Input, obj.PreAuthStatus);
                dyParam.Add("p_PreAuthDoc", OracleDbType.Varchar2, ParameterDirection.Input, (obj.VchFile == "" ? "NA" : obj.VchFile));
                dyParam.Add("p_Amount", OracleDbType.Varchar2, ParameterDirection.Input, obj.AmoutBlocked);
                dyParam.Add("p_BlockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
                dyParam.Add("p_WardBlockingDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingUserDate);
                dyParam.Add("p_HospitalCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode);
                dyParam.Add("P_OUTERRMSG", OracleDbType.Varchar2, ParameterDirection.Output);
                var query = "USP_Transaction_AED_WardChange";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
                blockPkgStatus = "1";
                //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                //var result = SqlConnecton.Execute("USP_Transaction_AED_WardChange", param, commandType: CommandType.StoredProcedure);
                //blockPkgStatus = param.Get<string>("P_OUTERRMSG");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return blockPkgStatus;
        }
        public string addAddOnPackageDetails(PatientInfo obj)
        {

            //object[] objArray = new object[] {
            // "@P_ACTIONCODE", obj.ACTIONCODE
            //,"@TransactionId", obj.TransactionID
            //,"@ProcedureCode",obj.ProcedureCode
            //,"@CategoryCode",obj.CategoryCode
            //,"@PackageCode", obj.PackageCode
            //,"@PreAuthStatus", obj.PreAuthStatus
            //,"@PreAuthDoc",(obj.VchFile==""?"NA":obj.VchFile)
            //,"@BlockingAmount", obj.AmoutBlocked
            //,"@AddOnBlockingDate", obj.BlockingUserDate
            //,"@IsMedSergical",obj.IsMedSergical
            //};
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("v_P_ACTIONCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.ACTIONCODE);
                dyParam.Add("v_TransactionId", OracleDbType.Int64, ParameterDirection.Input, obj.TransactionID);
                dyParam.Add("v_AddOnLogId", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("v_CancelReason", OracleDbType.Varchar2, ParameterDirection.Input, null);
                dyParam.Add("v_CancelDate", OracleDbType.Varchar2, ParameterDirection.Input, null);
                dyParam.Add("v_ProcedureCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureCode);
                dyParam.Add("v_CategoryCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.CategoryCode);
                dyParam.Add("v_PackageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCode);
                dyParam.Add("v_PreAuthStatus", OracleDbType.Char, ParameterDirection.Input, obj.PreAuthStatus);
                dyParam.Add("v_PreAuthDoc", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCode);
                dyParam.Add("v_BlockingAmount", OracleDbType.Varchar2, ParameterDirection.Input, obj.AmoutBlocked);
                dyParam.Add("v_AddOnBlockingDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingUserDate);
                dyParam.Add("v_IsMedSergical", OracleDbType.Varchar2, ParameterDirection.Input, obj.IsMedSergical);
                dyParam.Add("v_P_OUTERRMSG", OracleDbType.Varchar2, ParameterDirection.Output);

                var query = "USP_Transaction_AED_AddOn";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
                blockPkgStatus = "1";
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return blockPkgStatus;
        }



        //public string addAddOnPackageDetails(PatientInfo obj)
        //{

        //    //object[] objArray = new object[] {
        //    // "@P_ACTIONCODE", obj.ACTIONCODE
        //    //,"@TransactionId", obj.TransactionID
        //    //,"@ProcedureCode",obj.ProcedureCode
        //    //,"@CategoryCode",obj.CategoryCode
        //    //,"@PackageCode", obj.PackageCode
        //    //,"@PreAuthStatus", obj.PreAuthStatus
        //    //,"@PreAuthDoc",(obj.VchFile==""?"NA":obj.VchFile)
        //    //,"@BlockingAmount", obj.AmoutBlocked
        //    //,"@AddOnBlockingDate", obj.BlockingUserDate
        //    //,"@IsMedSergical",obj.IsMedSergical
        //    //};
        //    string strOutput = "";
        //    try
        //    {
        //        var dyParam = new OracleDynamicParameters();
        //        dyParam.Add("v_P_ACTIONCODE", OracleDbType.NVarchar2, ParameterDirection.Input, obj.ACTIONCODE);
        //        dyParam.Add("v_TransactionId", OracleDbType.NVarchar2, ParameterDirection.Input, obj.TransactionID);
        //        dyParam.Add("v_ProcedureCode", OracleDbType.NVarchar2, ParameterDirection.Input, obj.ProcedureCode);
        //        dyParam.Add("v_CategoryCode", OracleDbType.NVarchar2, ParameterDirection.Input, obj.CategoryCode);
        //        dyParam.Add("v_PackageCode", OracleDbType.NVarchar2, ParameterDirection.Input, obj.PackageCode);
        //        dyParam.Add("v_PreAuthStatus", OracleDbType.NVarchar2, ParameterDirection.Input, obj.PreAuthStatus);
        //        dyParam.Add("v_PreAuthDoc", OracleDbType.NVarchar2, ParameterDirection.Input, obj.PackageCode);
        //        dyParam.Add("v_BlockingAmount", OracleDbType.NVarchar2, ParameterDirection.Input, obj.AmoutBlocked);
        //        dyParam.Add("v_AddOnBlockingDate", OracleDbType.NVarchar2, ParameterDirection.Input, obj.BlockingUserDate);
        //        dyParam.Add("v_IsMedSergical", OracleDbType.NVarchar2, ParameterDirection.Input, obj.IsMedSergical);
        //        dyParam.Add("v_P_OUTERRMSG", OracleDbType.RefCursor, ParameterDirection.Output);

        //        var query = "USP_Transaction_AED_AddOn";
        //        strOutput = SqlConnecton.Query(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
        //        blockPkgStatus = "1";
        //        //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
        //        //var result = SqlConnecton.Execute("USP_Transaction_AED_AddOn", param, commandType: CommandType.StoredProcedure);
        //        //blockPkgStatus = param.Get<string>("P_OUTERRMSG");
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //    }
        //    return blockPkgStatus;
        //}
        public string addPatientUnblockPackage(PatientInfo obj)
        {
            //object[] objArray = new object[] {
            //"P_ACTIONCODE", obj.ACTIONCODE
            //,"p_SchemeCode", obj.SchemeCode
            //,"p_DistrictCode", obj.DistrictCode
            //,"p_MemberDistrictName", obj.MemberDistrictName
            //,"p_URN", obj.URN
            //,"p_PolicyStartDate", obj.PolicyStartDate
            //,"p_PolicyEndDate", obj.PolicyEndDate
            //,"p_PatientName", obj.PatientName
            //,"p_HeadMemberID", obj.HeadMemberID
            //,"p_HeadMemberName", obj.HeadMemberName
            //,"p_HospitalCode", obj.HospitalCode
            //,"p_HospitalName", obj.HospitalName
            //,"p_HospitalState", obj.HospitalState
            //,"p_HospitalDistrict", obj.HospitalDistrict
            //,"p_ProcedureCode", obj.ProcedureCode
            //,"p_ProcedureName", obj.ProcedureName
            //,"p_PackageCode", obj.PackageCode
            //,"p_PackageName", obj.PackageName
            //,"p_UnblockingDesc", obj.UnblockingDesc
            //,"p_TransactionCode", obj.TransactionCode
            //,"p_TransactionID",obj.TransactionID
            //,"p_BlockingInvoiceNo",obj.BlockingInvoiceNo
            //,"p_Category",obj.Category
            //,"p_CategoryCode",obj.CategoryCode
            //};
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTIONCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.ACTIONCODE);
                dyParam.Add("p_RoundNo", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("p_SchemeCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.SchemeCode);
                dyParam.Add("p_MemberStateCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberStatecode);
                dyParam.Add("p_DistrictCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.DistrictCode);
                dyParam.Add("p_MemberDistrictName", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberDistrictName);
                dyParam.Add("p_BlockCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_PanchayatCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_VillageCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                dyParam.Add("p_FamilyID", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_PolicyStartDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyStartDate);
                dyParam.Add("p_PolicyEndDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyEndDate);
                dyParam.Add("p_CardType", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_MemberID", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("p_PatientName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientName);
                dyParam.Add("p_PatientContactNumber", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_Gender", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_PatientCardGender", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_Age", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("p_PatientCardAge", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("p_HeadMemberID", OracleDbType.Int64, ParameterDirection.Input, obj.HeadMemberID);
                dyParam.Add("p_HeadMemberName", OracleDbType.Varchar2, ParameterDirection.Input, obj.HeadMemberName);
                dyParam.Add("p_VerifiedMemberID", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("p_VerifiedMemberName", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_InsuranceCompanyCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_InsurancePolicyNumber", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_HospitalCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode);
                dyParam.Add("p_HospitalName", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalName);
                dyParam.Add("p_HospitalState", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalState);
                dyParam.Add("p_HospitalDistrict", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalDistrict);
                dyParam.Add("p_HospitalAuthorityCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_RegistrationNo", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_RegistrationUserDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_TransactionDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_BlockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
                dyParam.Add("p_BlockingUserDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_DATEOFADMISSION", OracleDbType.Varchar2, ParameterDirection.Input, "");
                //dyParam.Add("p_UnblockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.InputOutput, "");
                dyParam.Add("p_UnblockingDesc", OracleDbType.Varchar2, ParameterDirection.Input, obj.UnblockingDesc);
                dyParam.Add("p_UnblockingSystemDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_DischargeInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_DischargeDesc", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_DischargeUserDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_DATEOFDISCHARGE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_Mortality", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_MortalitySummary", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_ProcedureCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureCode);
                dyParam.Add("p_ProcedureName", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureName);
                dyParam.Add("p_PackageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCode);
                dyParam.Add("p_PackageName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageName);
                dyParam.Add("p_PackageCost", OracleDbType.Varchar2, ParameterDirection.Input, 0);
                dyParam.Add("p_NoofDays", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("p_AmoutBlocked", OracleDbType.Varchar2, ParameterDirection.Input, 0);
                dyParam.Add("p_NoofDaysActual", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("p_TotalAmountClaimed", OracleDbType.Varchar2, ParameterDirection.Input, 0);
                dyParam.Add("p_AvailableBalance", OracleDbType.Varchar2, ParameterDirection.Input, 0);
                dyParam.Add("p_TransactionCode", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.TransactionCode);
                dyParam.Add("p_TotalAmtBlockedOnCard", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_InsufficientBalanceAmount", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_OriginalPackageCost", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_intClaimStatus", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_claimid", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_statusflag", OracleDbType.Int32, ParameterDirection.Input, 0);
                dyParam.Add("p_patientSlip", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("p_TransactionID", OracleDbType.Int64, ParameterDirection.Input, obj.TransactionID);
                dyParam.Add("p_Category", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.Category);
                dyParam.Add("p_CategoryCode", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.CategoryCode);
                dyParam.Add("p_OUTERRMSG", OracleDbType.Varchar2, ParameterDirection.Output);
                var query = "USP_Transaction_AED_Unblock";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
                UnblockPkgStatus = "1";

                //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                //var result = SqlConnecton.Execute("USP_Transaction_AED_Unblock", param, commandType: CommandType.StoredProcedure);
                //UnblockPkgStatus = param.Get<string>("P_OUTERRMSG");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return UnblockPkgStatus;
        }

        //public string addPatientUnblockPackage(PatientInfo obj)
        //{
        //    //object[] objArray = new object[] {
        //    //"P_ACTIONCODE", obj.ACTIONCODE
        //    //,"p_SchemeCode", obj.SchemeCode
        //    //,"p_DistrictCode", obj.DistrictCode
        //    //,"p_MemberDistrictName", obj.MemberDistrictName
        //    //,"p_URN", obj.URN
        //    //,"p_PolicyStartDate", obj.PolicyStartDate
        //    //,"p_PolicyEndDate", obj.PolicyEndDate
        //    //,"p_PatientName", obj.PatientName
        //    //,"p_HeadMemberID", obj.HeadMemberID
        //    //,"p_HeadMemberName", obj.HeadMemberName
        //    //,"p_HospitalCode", obj.HospitalCode
        //    //,"p_HospitalName", obj.HospitalName
        //    //,"p_HospitalState", obj.HospitalState
        //    //,"p_HospitalDistrict", obj.HospitalDistrict
        //    //,"p_ProcedureCode", obj.ProcedureCode
        //    //,"p_ProcedureName", obj.ProcedureName
        //    //,"p_PackageCode", obj.PackageCode
        //    //,"p_PackageName", obj.PackageName
        //    //,"p_UnblockingDesc", obj.UnblockingDesc
        //    //,"p_TransactionCode", obj.TransactionCode
        //    //,"p_TransactionID",obj.TransactionID
        //    //,"p_BlockingInvoiceNo",obj.BlockingInvoiceNo
        //    //,"p_Category",obj.Category
        //    //,"p_CategoryCode",obj.CategoryCode
        //    //};
        //    string strOutput = "";
        //    try
        //    {
        //        var dyParam = new OracleDynamicParameters();
        //        dyParam.Add("P_ACTIONCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.ACTIONCODE);
        //        dyParam.Add("p_RoundNo", OracleDbType.Int64, ParameterDirection.Input, 0);
        //        dyParam.Add("p_SchemeCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.SchemeCode);
        //        dyParam.Add("p_MemberStateCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberStatecode);
        //        dyParam.Add("p_DistrictCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.DistrictCode);
        //        dyParam.Add("p_MemberDistrictName", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberDistrictName);
        //        dyParam.Add("p_BlockCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_PanchayatCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_VillageCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
        //        dyParam.Add("p_FamilyID", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_PolicyStartDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyStartDate);
        //        dyParam.Add("p_PolicyEndDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyEndDate);
        //        dyParam.Add("p_CardType", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_MemberID", OracleDbType.Int64, ParameterDirection.Input, 0);
        //        dyParam.Add("p_PatientName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientName);
        //        dyParam.Add("p_PatientContactNumber", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_Gender", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_PatientCardGender", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_Age", OracleDbType.Int64, ParameterDirection.Input, 0);
        //        dyParam.Add("p_PatientCardAge", OracleDbType.Int64, ParameterDirection.Input, 0);
        //        dyParam.Add("p_HeadMemberID", OracleDbType.Int64, ParameterDirection.Input, obj.HeadMemberID);
        //        dyParam.Add("p_HeadMemberName", OracleDbType.Varchar2, ParameterDirection.Input, obj.HeadMemberName);
        //        dyParam.Add("p_VerifiedMemberID", OracleDbType.Int64, ParameterDirection.Input, 0);
        //        dyParam.Add("p_VerifiedMemberName", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_InsuranceCompanyCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_InsurancePolicyNumber", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_HospitalCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode);
        //        dyParam.Add("p_HospitalName", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalName);
        //        dyParam.Add("p_HospitalState", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalState);
        //        dyParam.Add("p_HospitalDistrict", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalDistrict);
        //        dyParam.Add("p_HospitalAuthorityCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_RegistrationNo", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_RegistrationUserDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_TransactionDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_BlockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
        //        dyParam.Add("p_BlockingUserDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_DATEOFADMISSION", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        //dyParam.Add("p_UnblockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.InputOutput, "");
        //        dyParam.Add("p_UnblockingDesc", OracleDbType.Varchar2, ParameterDirection.Input, obj.UnblockingDesc);
        //        dyParam.Add("p_UnblockingSystemDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_DischargeInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_DischargeDesc", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_DischargeUserDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_DATEOFDISCHARGE", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_Mortality", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_MortalitySummary", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_ProcedureCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureCode);
        //        dyParam.Add("p_ProcedureName", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureName);
        //        dyParam.Add("p_PackageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCode);
        //        dyParam.Add("p_PackageName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageName);
        //        dyParam.Add("p_PackageCost", OracleDbType.Varchar2, ParameterDirection.Input, 0);
        //        dyParam.Add("p_NoofDays", OracleDbType.Int64, ParameterDirection.Input, 0);
        //        dyParam.Add("p_AmoutBlocked", OracleDbType.Varchar2, ParameterDirection.Input, 0);
        //        dyParam.Add("p_NoofDaysActual", OracleDbType.Int64, ParameterDirection.Input, 0);
        //        dyParam.Add("p_TotalAmountClaimed", OracleDbType.Varchar2, ParameterDirection.Input, 0);
        //        dyParam.Add("p_AvailableBalance", OracleDbType.Varchar2, ParameterDirection.Input, 0);
        //        dyParam.Add("p_TransactionCode", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.TransactionCode);
        //        dyParam.Add("p_TotalAmtBlockedOnCard", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_InsufficientBalanceAmount", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_OriginalPackageCost", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_intClaimStatus", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_claimid", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_statusflag", OracleDbType.Int32, ParameterDirection.Input, 0);
        //        dyParam.Add("p_patientSlip", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("p_TransactionID", OracleDbType.Int64, ParameterDirection.Input, obj.TransactionID);
        //        dyParam.Add("p_Category", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.Category);
        //        dyParam.Add("p_CategoryCode", OracleDbType.Varchar2, ParameterDirection.InputOutput, obj.CategoryCode);
        //        dyParam.Add("p_OUTERRMSG", OracleDbType.Varchar2, ParameterDirection.Output);
        //        var query = "USP_Transaction_AED_Unblock";
        //        strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
        //        UnblockPkgStatus = "1";

        //        //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
        //        //var result = SqlConnecton.Execute("USP_Transaction_AED_Unblock", param, commandType: CommandType.StoredProcedure);
        //        //UnblockPkgStatus = param.Get<string>("P_OUTERRMSG");
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //    }
        //    return UnblockPkgStatus;
        //}
        public string addPatientDischarge(PatientInfo obj)
        {
            //object[] objArray = new object[] {
            //"@P_ACTIONCODE", obj.ACTIONCODE
            //,"@BlockingInvoiceNo", obj.BlockingInvoiceNo
            //,"@DischargeDesc", obj.DischargeDesc
            //,"@DischargeUserDate", obj.DischargeUserDate
            //,"@DATEOFDISCHARGE", obj.DATEOFDISCHARGE
            //,"@Mortality", obj.Mortality
            //,"@MortalitySummary", obj.MortalitySummary
            //,"@ProcedureCode", obj.ProcedureCode
            //,"@ProcedureName", obj.ProcedureName
            //,"@PackageCode", obj.PackageCode
            //,"@PackageName", obj.PackageName
            //,"@PackageCost", obj.PackageCost
            //,"@NoofDays", obj.NoofDays
            //,"@AmoutBlocked", obj.AmoutBlocked
            //,"@NoofDaysActual", obj.NoofDaysActual
            //,"@packagemode", obj.PackageMode
            //,"@IsMedSergical", obj.IsMedSergical
            //,"@URN", obj.URN
            //,"@TreatmentCompletionCer",obj.TreatmentCompletionCer
            //,"@Category", obj.Category
            //,"@CategoryCode",obj.CategoryCode
            //};
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTIONCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.ACTIONCODE);
                dyParam.Add("P_RoundNo", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_SchemeCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_MemberStatecode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_DistrictCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_MemberDistrictName", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_BlockCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_PanchayatCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_VillageCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                dyParam.Add("P_PolicyStartDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_PolicyEndDate", OracleDbType.Varchar2, ParameterDirection.Input, "");

                dyParam.Add("P_CardType", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_MemberID", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_PatientName", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_PatientContactNumber", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_Gender", OracleDbType.Varchar2, ParameterDirection.Input, "");

                dyParam.Add("P_PatientCardGender", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_Age", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_PatientCardAge", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_HeadMemberID", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_HeadMemberName", OracleDbType.Varchar2, ParameterDirection.Input, "");

                dyParam.Add("P_VerifiedMemberID", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_VerifiedMemberName", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_InsuranceCompanyCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_InsurancePolicyNumber", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_HospitalCode", OracleDbType.Varchar2, ParameterDirection.Input, "");

                dyParam.Add("P_HospitalName", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_HospitalState", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_HospitalDistrict", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_HospitalAuthorityCode", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_RegistrationNo", OracleDbType.Varchar2, ParameterDirection.Input, "");

                dyParam.Add("P_RegistrationUserDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_TransactionDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_BlockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
                dyParam.Add("P_BlockingUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingUserDate);
                dyParam.Add("P_DATEOFADMISSION", OracleDbType.Varchar2, ParameterDirection.Input, obj.DATEOFADMISSION);

                dyParam.Add("P_UnblockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_UnblockingDesc", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_UnblockingSystemDate", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_DischargeInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_DischargeDesc", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeDesc);
                dyParam.Add("P_DischargeUserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeUserDate);

                dyParam.Add("P_DATEOFDISCHARGE", OracleDbType.Varchar2, ParameterDirection.Input, obj.DATEOFDISCHARGE);
                dyParam.Add("P_Mortality", OracleDbType.Varchar2, ParameterDirection.Input, obj.Mortality);
                dyParam.Add("P_MortalitySummary", OracleDbType.Varchar2, ParameterDirection.Input, obj.MortalitySummary);


                dyParam.Add("P_ProcedureCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureCode);
                dyParam.Add("P_ProcedureName", OracleDbType.Varchar2, ParameterDirection.Input, obj.ProcedureName);
                dyParam.Add("P_PackageCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCode);
                dyParam.Add("P_PackageName", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageName);

                dyParam.Add("P_PackageCost", OracleDbType.Varchar2, ParameterDirection.Input, obj.PackageCost);
                dyParam.Add("P_NoofDays", OracleDbType.Int64, ParameterDirection.Input, obj.NoofDays);
                dyParam.Add("P_AmoutBlocked", OracleDbType.Varchar2, ParameterDirection.Input, obj.AmoutBlocked);

                dyParam.Add("P_NoofDaysActual", OracleDbType.Int32, ParameterDirection.Input, obj.NoofDaysActual);
                dyParam.Add("P_TotalAmountClaimed", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_AvailableBalance", OracleDbType.Varchar2, ParameterDirection.Output, "");
                dyParam.Add("P_TransactionCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.TransactionCode);

                dyParam.Add("P_TotalAmtBlockedOnCard", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_InsufficientBalanceAmount", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_OriginalPackageCost", OracleDbType.Varchar2, ParameterDirection.Output, "");
                dyParam.Add("P_intClaimStatus", OracleDbType.Varchar2, ParameterDirection.Input, "");


                dyParam.Add("P_claimid", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_statusflag", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_patientSlip", OracleDbType.Varchar2, ParameterDirection.Output, "");
                dyParam.Add("P_packagemode", OracleDbType.Int64, ParameterDirection.Input, 0);

                dyParam.Add("P_IsMedSergical", OracleDbType.Varchar2, ParameterDirection.Input, obj.IsMedSergical);
                dyParam.Add("P_TreatmentCompletionCer", OracleDbType.Varchar2, ParameterDirection.Input, obj.TreatmentCompletionCer);
                dyParam.Add("P_Category", OracleDbType.Varchar2, ParameterDirection.Input, obj.Category);
                dyParam.Add("P_CategoryCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.CategoryCode);
                dyParam.Add("P_P_OUTERRMSG", OracleDbType.Varchar2, ParameterDirection.Output);

                var query = "USP_Transaction_AED_Discharge";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();

                //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                //var result = SqlConnecton.Execute("USP_Transaction_AED_Discharge", param, commandType: CommandType.StoredProcedure);
                //DischargeStatus = param.Get<string>("P_OUTERRMSG");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return DischargeStatus;
        }
        public string AddPreAuthRequest(PreAuth obj)
        {
            object[] objArray = new object[] {
                   "@P_ACTIONCODE", obj.Action
                  ,"@VCHURNNO", obj.VCHURNNO
                  ,"@intMemberId", obj.intMemberId
                  ,"@vchMemberName", obj.vchMemberName
                  ,"@vchPackageCategory", obj.vchPackageCategory
                  ,"@VchPackageDetail", obj.VchPackageDetail
                  //,"@dtmDate", obj.dtmDate
                  ,"@VchFile", obj.VchFile
                  //,"@VchRemarks", obj.VchRemarks
                  //,"@INT_SH_ID", obj.INT_SH_ID
                  //,"@INT_PHASE_ID", obj.INT_PHASE_ID
                  ,"@DEC_AMOUNT", obj.DEC_AMOUNT
                  //,"@INT_SCHEME_ID", obj.INT_SCHEME_ID
                  //,"@vchActionRemarks", obj.vchActionRemarks
                  //,"@intAprovedBy", obj.intAprovedBy
                  //,"@dtmAction", obj.dtmAction
                  //,"@INT_CREATED_BY", obj.INT_CREATED_BY
                  //,"@DTM_CREATED_ON", obj.DTM_CREATED_ON       
                  ,"@BlockingInvoiceNo", obj.BlockingInvoiceNo
                  };
            try
            {
                DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                var result = SqlConnecton.Execute("USP_T_PreAuth_ADD", param, commandType: CommandType.StoredProcedure);
                DischargeStatus = param.Get<string>("P_OUTERRMSG");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return DischargeStatus;
        }
        public string PreAuthPackageBlock(PreAuthApprovedPackageBlock obj)

        {

            //object[] objArray = new object[] {
            //       "@P_ACTIONCODE", obj.ACTION
            //      ,"@VCHURNNO", obj.URN
            //      ,"@BlockingInvoiceNo", obj.BlockingInvoiceNo
            //      ,"@TransactionID", obj.TransactionID
            //      ,"@VchFile",obj.VchFileName==""||obj.VchFileName==null?"NA":obj.VchFileName
            //      ,"@BlockinguserDate", obj.BlockinguserDate==""||obj.BlockinguserDate==null?"NA":obj.BlockinguserDate
            //      ,"@HospitalAuthorityCode",obj.HospitalAuthorityCode==""||obj.HospitalAuthorityCode==null?"NA":obj.HospitalAuthorityCode
            //      ,"@hospitalcode",obj.HospitalCode==""||obj.HospitalCode==null?"NA":obj.HospitalCode
            //      };
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTIONCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.ACTION);
                dyParam.Add("p_VCHURNNO", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                dyParam.Add("p_TransactionID", OracleDbType.Int64, ParameterDirection.Input, obj.TransactionID);
                dyParam.Add("p_BlockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
                dyParam.Add("p_BlockinguserDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockinguserDate == "" || obj.BlockinguserDate == null ? "NA" : obj.BlockinguserDate);
                dyParam.Add("p_VchFile", OracleDbType.Varchar2, ParameterDirection.Input, obj.VchFileName == "" || obj.VchFileName == null ? "NA" : obj.VchFileName);
                dyParam.Add("p_HospitalCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode == "" || obj.HospitalCode == null ? "NA" : obj.HospitalCode);
                dyParam.Add("p_HospitalAuthorityCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalAuthorityCode == "" || obj.HospitalAuthorityCode == null ? "NA" : obj.HospitalAuthorityCode);
                dyParam.Add("P_OUTERRMSG", OracleDbType.Varchar2, ParameterDirection.Output);
                var query = "USP_T_PreAuth_ADD";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
                DischargeStatus = "1";

                //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                //var result = SqlConnecton.Execute("USP_T_PreAuth_ADD", param, commandType: CommandType.StoredProcedure);
                //DischargeStatus = param.Get<string>("P_OUTERRMSG");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return DischargeStatus;
        }


        public string PreWardAuthPackageBlock(PreAuthApprovedPackageBlock obj)
        {
            object[] objArray = new object[] {
                   "@P_ACTIONCODE", obj.ACTION
                  ,"@TransactionID", obj.TransactionID
                  ,"@WardBlockingDate",obj.BlockinguserDate
                  ,"@WardLogId",obj.WardLogId
                  ,"@Amount",obj.Amount
                  ,"@PreAuthDoc",obj.VchFileName
                  };
            try
            {
                DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                var result = SqlConnecton.Execute("USP_Transaction_AED_WardChange", param, commandType: CommandType.StoredProcedure);
                DischargeStatus = param.Get<string>("P_OUTERRMSG");
            }

            catch (Exception ex)
            {
                log.Error(ex);
            }
            return DischargeStatus;
        }
        public string AddOnPreAuthPackageBlock(PreAuthApprovedPackageBlock obj)
        {
            object[] objArray = new object[] {
                   "@P_ACTIONCODE", obj.ACTION
                  ,"@TransactionID", obj.TransactionID
                  ,"@AddOnBlockingDate",obj.BlockinguserDate
                  ,"@AddOnLogId",obj.WardLogId
                  ,"@BlockingAmount",obj.Amount
                  ,"@PreAuthDoc",obj.VchFileName
                  };
            try
            {
                DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                var result = SqlConnecton.Execute("USP_Transaction_AED_AddOn", param, commandType: CommandType.StoredProcedure);
                DischargeStatus = param.Get<string>("P_OUTERRMSG");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return DischargeStatus;
        }
        //Insert Extension Of Stay Details
        public string AddExtensionOfStayRequest(PackageExtension obj)
        {
            //object[] objArray = new object[] {
            //       "@P_VCHURNNO", obj.URN
            //      ,"@P_HospitalCode", obj.HospitalCode
            //      ,"@P_TransactionID", obj.TransactionId
            //      ,"@P_NoofextendDays", obj.NoofextendDays
            //      ,"@P_vchPrescription", obj.VchFile
            //      ,"@P_BlockingInvoiceNo", obj.BlockingInvoiceNo
            //      };
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("v_P_VCHURNNO", OracleDbType.Char, ParameterDirection.Input, obj.URN);
                dyParam.Add("v_P_HospitalCode", OracleDbType.NVarchar2, ParameterDirection.Input, obj.HospitalCode);
                dyParam.Add("v_P_TransactionID", OracleDbType.Int16, ParameterDirection.Input, obj.TransactionId);
                dyParam.Add("v_P_NoofextendDays", OracleDbType.Int16, ParameterDirection.Input, obj.NoofextendDays);
                dyParam.Add("v_P_vchPrescription", OracleDbType.NVarchar2, ParameterDirection.Input, obj.VchFile);
                dyParam.Add("v_P_BlockingInvoiceNo", OracleDbType.NVarchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
                dyParam.Add("v_P_OUTERRMSG", OracleDbType.RefCursor, ParameterDirection.Output);
                var query = "USP_ADD_PACKAGEEXTENSIONINFO";
                strOutput = SqlConnecton.Query(query, dyParam, commandType: CommandType.StoredProcedure).ToString();

                //DynamicParameters param = objArray.ToDynamicParameters("@P_MSGOUT");
                //var result = SqlConnecton.Execute("USP_ADD_PACKAGEEXTENSIONINFO", param, commandType: CommandType.StoredProcedure);
                //DischargeStatus = param.Get<string>("P_MSGOUT");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return DischargeStatus;
        }
        public string addCancelPackage(CancelPackage obj)
        {
            //object[] objArray = new object[] {
            //       "@P_ACTIONCODE",obj.Action
            //      ,"@BlockingInvoiceNo",obj.BlockingINVOICENO
            //      ,"@TransactionID",obj.TransactionID
            //      ,"@CancelReason",obj.CancelReason==null?"":obj.CancelReason
            //      ,"@CancelDate",obj.CancelDate==null?"":obj.CancelDate
            //      };
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("v_P_ACTIONCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.Action);
                dyParam.Add("v_BlockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingINVOICENO);
                dyParam.Add("v_TransactionID", OracleDbType.Int64, ParameterDirection.Input, obj.TransactionID);
                dyParam.Add("v_CancelReason", OracleDbType.Varchar2, ParameterDirection.Input, obj.CancelReason == null ? "" : obj.CancelReason);
                dyParam.Add("v_CancelDate", OracleDbType.Varchar2, ParameterDirection.Input, obj.CancelDate == null ? "" : obj.CancelDate);
                dyParam.Add("v_P_OUTERRMSG", OracleDbType.Varchar2, ParameterDirection.Output);
                var query = "USP_Transaction_Cancel";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();

                //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                //var result = SqlConnecton.Execute("USP_Transaction_Cancel", param, commandType: CommandType.StoredProcedure);
                //CancelStatus = param.Get<string>("P_OUTERRMSG");
                CancelStatus = "1";

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return CancelStatus;
        }
        public string addWardCancelPackage(CancelPackage obj)
        {
            //object[] objArray = new object[] {
            //       "@P_ACTIONCODE",obj.Action
            //      ,"@CancelReason",obj.CancelReason
            //      ,"@CancelDate",obj.CancelDate
            //      ,"@WardLogId",obj.WardLogId                 
            //      };
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("v_P_ACTIONCODE", OracleDbType.Char, ParameterDirection.Input, obj.Action);
                dyParam.Add("v_CancelReason", OracleDbType.NVarchar2, ParameterDirection.Input, obj.CancelReason);
                dyParam.Add("v_CancelDate", OracleDbType.NVarchar2, ParameterDirection.Input, obj.CancelDate);
                dyParam.Add("v_WardLogId", OracleDbType.Int16, ParameterDirection.Input, obj.WardLogId);
                dyParam.Add("v_P_OUTERRMSG", OracleDbType.RefCursor, ParameterDirection.Output);
                var query = "USP_Transaction_AED_WardChange";
                strOutput = SqlConnecton.Query(query, dyParam, commandType: CommandType.StoredProcedure).ToString();

                //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                //var result = SqlConnecton.Execute("USP_Transaction_AED_WardChange", param, commandType: CommandType.StoredProcedure);
                //CancelStatus = param.Get<string>("P_OUTERRMSG");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return CancelStatus;
        }
        public string addAddOnCancelPackage(CancelPackage obj)
        {
            //object[] objArray = new object[] {
            //       "@P_ACTIONCODE",obj.Action
            //      ,"@CancelReason",obj.CancelReason
            //      ,"@CancelDate",obj.CancelDate
            //      ,"@AddOnLogId",obj.WardLogId
            //      };
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("v_P_ACTIONCODE", OracleDbType.Char, ParameterDirection.Input, obj.Action);
                dyParam.Add("v_CancelReason", OracleDbType.NVarchar2, ParameterDirection.Input, obj.CancelReason);
                dyParam.Add("v_CancelDate", OracleDbType.NVarchar2, ParameterDirection.Input, obj.CancelDate);
                dyParam.Add("v_AddOnLogId", OracleDbType.NVarchar2, ParameterDirection.Input, obj.WardLogId);
                dyParam.Add("v_P_OUTERRMSG", OracleDbType.RefCursor, ParameterDirection.Output);
                var query = "USP_Transaction_AED_AddOn";
                strOutput = SqlConnecton.Query(query, dyParam, commandType: CommandType.StoredProcedure).ToString();

                //DynamicParameters param = objArray.ToDynamicParameters("@P_OUTERRMSG");
                //var result = SqlConnecton.Execute("USP_Transaction_AED_AddOn", param, commandType: CommandType.StoredProcedure);
                //CancelStatus = param.Get<string>("P_OUTERRMSG");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return CancelStatus;
        }
        public string addNotice(Notice obj)
        {
            //object[] objNotice = new object[] {
            //     "@P_ACTIONCODE","A"
            //    ,"@StartDate",obj.StartDate
            //    ,"@EndDate",obj.EndDate
            //    ,"@Notice",obj.Notices
            //    ,"@NoticeDocument",obj.NoticeDocument
            //};
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("v_P_ACTIONCODE", OracleDbType.Char, ParameterDirection.Input, "A");
                dyParam.Add("v_StartDate", OracleDbType.NVarchar2, ParameterDirection.Input, obj.StartDate);
                dyParam.Add("v_EndDate", OracleDbType.NVarchar2, ParameterDirection.Input, obj.EndDate);
                dyParam.Add("v_Notice", OracleDbType.NVarchar2, ParameterDirection.Input, obj.Notices);
                dyParam.Add("v_NoticeDocument", OracleDbType.NVarchar2, ParameterDirection.Input, obj.NoticeDocument);
                dyParam.Add("v_P_OUTERRMSG", OracleDbType.RefCursor, ParameterDirection.Output);
                var query = "USP_AddNotice";
                strOutput = SqlConnecton.Query(query, dyParam, commandType: CommandType.StoredProcedure).ToString();

                //DynamicParameters param = objNotice.ToDynamicParameters("@P_OUTERRMSG");
                //var result = SqlConnecton.Execute("USP_AddNotice", param, commandType: CommandType.StoredProcedure);
                //NoticeStatus = param.Get<string>("P_OUTERRMSG");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return NoticeStatus;
        }
        public string addDownwardReferal(DownwardReferalInfo obj)
        {

            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("v_P_ActionCode", OracleDbType.Char, ParameterDirection.Input, obj.Action);
                dyParam.Add("v_P_ReferalID", OracleDbType.Int64, ParameterDirection.Input, DBNull.Value);
                dyParam.Add("v_P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                dyParam.Add("v_P_BlockingInvoiceNo", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
                dyParam.Add("v_P_IsReferalRequired", OracleDbType.Varchar2, ParameterDirection.Input, obj.IsReferalRequired);
                dyParam.Add("v_P_DistrictCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.DistrictCode);
                dyParam.Add("v_P_BlockCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockCode);
                dyParam.Add("v_P_PHCCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.PHCCode);
                dyParam.Add("v_P_SubCenterCode", OracleDbType.Varchar2, ParameterDirection.Input, obj.SubCenterCode);
                dyParam.Add("P_MESSAGEOUT", OracleDbType.Int64, ParameterDirection.Output);

                var query = "USP_AddDownwardReferal";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();

                //DynamicParameters param = objDownwardReferal.ToDynamicParameters("@P_OutPutMsg");
                //var result = SqlConnecton.Execute("USP_AddDownwardReferal", param, commandTyp0e: CommandType.StoredProcedure);
                //NoticeStatus = param.Get<string>("P_OutPutMsg");
                NoticeStatus = "1";
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return NoticeStatus;
        }
        public List<BlockPackageFilterModel> addPatientBlockPackageInsert(PatientInfo obj)
        {
            List<BlockPackageFilterModel> result = new List<BlockPackageFilterModel>();
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, obj.ACTIONCODE);
                dyParam.Add("P_MEMBERSTATECODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberStatecode);
                dyParam.Add("P_STATENAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberStateName);
                dyParam.Add("P_DISTRICTCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberDistrictCode);
                dyParam.Add("P_DISTRICTNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberDistrictName);
                dyParam.Add("P_BLOCKCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberBlockCode);
                dyParam.Add("P_BLOCKNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberBlockName);
                dyParam.Add("P_PANCHAYATCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.PanchayatCode);
                dyParam.Add("P_PANCHAYATNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.PanchayatName);
                dyParam.Add("P_VILLAGECODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.VillageCode);
                dyParam.Add("P_VILLAGENAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.VillageName);
                dyParam.Add("P_UIDNUMBER", OracleDbType.Varchar2, ParameterDirection.Input, obj.AadhaarNo);
                dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                dyParam.Add("P_POLICYSTARTDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyStartDate);
                dyParam.Add("P_POLICYENDDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyEndDate);
                dyParam.Add("P_MEMBERID", OracleDbType.Int64, ParameterDirection.Input, obj.MemberID);
                dyParam.Add("P_MEMBERNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientName);
                dyParam.Add("P_PATIENTCONTACTNUMBER", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientContactNumber);
                dyParam.Add("P_PATIENTGENDER", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientCardGender);
                dyParam.Add("P_AGE", OracleDbType.Int64, ParameterDirection.Input, obj.PatientCardAge);
                dyParam.Add("P_HEADMEMBERID", OracleDbType.Int64, ParameterDirection.Input, 0); // Hardcoded
                dyParam.Add("P_HEADMEMBERNAME", OracleDbType.Varchar2, ParameterDirection.Input, ""); // Hardcoded
                dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode);
                dyParam.Add("P_HOSPITALNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalName);
                dyParam.Add("P_HOSPITALSTATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.Hospitalstatecode); // Added by Ashutosh PradhaN on dated 200223
                dyParam.Add("P_HOSPITALDISTRICT", OracleDbType.Varchar2, ParameterDirection.Input, obj.Hospitaldistrictcode);  // Added by Ashutosh PradhaN on dated 200223
                dyParam.Add("P_HOSPITALAUTHORITYCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalAuthorityCode);
                dyParam.Add("P_MORTALITY", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_MORTALITYSUMMARY", OracleDbType.Varchar2, ParameterDirection.Input, "");
                dyParam.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input, obj.Remarks);
                dyParam.Add("P_TOTALAMOUNTCLAIMED", OracleDbType.Decimal, ParameterDirection.Input, (obj.AmoutBlocked == "" ? 0 : Convert.ToDecimal(obj.AmoutBlocked)));
                dyParam.Add("P_ISEMERGENCY", OracleDbType.Varchar2, ParameterDirection.Input, obj.IsEmergency);
                dyParam.Add("P_REFERALCODE", OracleDbType.Varchar2, ParameterDirection.Input, (obj.ReferalCode == "" ? "0" : obj.ReferalCode));
                dyParam.Add("P_ADMISSIONDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.DATEOFADMISSION);
                dyParam.Add("P_DOCTORNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.Doctorname);
                dyParam.Add("P_DOCTORPHNO", OracleDbType.Varchar2, ParameterDirection.Input, obj.Doctorphno);
                dyParam.Add("P_OVERRIDECODE", OracleDbType.Varchar2, ParameterDirection.Input, (obj.OverrideCode == "" ? "0" : obj.OverrideCode));
                dyParam.Add("P_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input, obj.Description);
                dyParam.Add("P_USERID", OracleDbType.Int64, ParameterDirection.Input, obj.userID);
                dyParam.Add("P_VERIFIEDMEMBERID", OracleDbType.Int64, ParameterDirection.Input, (obj.VerifiedMemberID == "" ? obj.MemberID : obj.VerifiedMemberID));
                dyParam.Add("P_VERIFIEDMEMBERNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.VerifiedMemberName);
                dyParam.Add("P_VERIFICATIONMODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.AuthenticationMode);
                //Vital Insertion   
                dyParam.Add("P_XMLVITALPARAMETER", OracleDbType.XmlType, ParameterDirection.Input, CommonExtension.SerializeToXMLString(obj.VitalParams));

                //Pacakges Insertion
                dyParam.Add("P_XMLPACKAGEDETAILS", OracleDbType.XmlType, ParameterDirection.Input, CommonExtension.SerializeToXMLString(obj.Packageparametermodel));

                //Implant Insertion
                dyParam.Add("P_XMLIMPLANTDETAILS", OracleDbType.XmlType, ParameterDirection.Input, CommonExtension.SerializeToXMLString(obj.Implantparams));

                //Highend drugs Insertion
                dyParam.Add("P_XMLHEDDETAILS", OracleDbType.XmlType, ParameterDirection.Input, CommonExtension.SerializeToXMLString(obj.Highenddrugsparams));

                dyParam.Add("P_ADMISSIONFILE", OracleDbType.Varchar2, ParameterDirection.Input, obj.UploadDoc);
                dyParam.Add("P_HOSPITALCATEGORYID", OracleDbType.Int64, ParameterDirection.Input, obj.HospitalCategoryId);
                dyParam.Add("P_NOOFDAYS", OracleDbType.Varchar2, ParameterDirection.Input, (obj.PreAuthNoofDays == "" ? "0" : obj.PreAuthNoofDays));
                dyParam.Add("P_PATIENTPHOTO", OracleDbType.Varchar2, ParameterDirection.Input, obj.UploadDoc1);
                dyParam.Add("P_ISPATIENTOTPVERIFIED", OracleDbType.Varchar2, ParameterDirection.Input, obj.IsVerified);
                dyParam.Add("P_PARENT_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, !string.IsNullOrEmpty(obj.parenttransactionid) ? obj.parenttransactionid : null);
                //dyParam.Add("P_ISPATIENTOTPVERIFIED", OracleDbType.Varchar2, ParameterDirection.Input, Session["exceptionhospital"]);
                dyParam.Add("P_MESSAGEOUT", OracleDbType.Int64, ParameterDirection.Output);
                var query = "USP_TRANSACTION_AED_BLOCK_TMS";
                result = SqlConnecton.Query<BlockPackageFilterModel>(query, dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return result;
        }



        //public List<BlockPackageFilterModel> addPatientBlockPackageInsert(PatientInfo obj)
        //{
        //    List<BlockPackageFilterModel> result = new List<BlockPackageFilterModel>();
        //    string strOutput = "";
        //    try
        //    {
        //        var dyParam = new OracleDynamicParameters();
        //        dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, obj.ACTIONCODE);
        //        dyParam.Add("P_MEMBERSTATECODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberStatecode);
        //        dyParam.Add("P_STATENAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberStateName);
        //        dyParam.Add("P_DISTRICTCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberDistrictCode);
        //        dyParam.Add("P_DISTRICTNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberDistrictName);
        //        dyParam.Add("P_BLOCKCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberBlockCode);
        //        dyParam.Add("P_BLOCKNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.MemberBlockName);
        //        dyParam.Add("P_PANCHAYATCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.PanchayatCode);
        //        dyParam.Add("P_PANCHAYATNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.PanchayatName);
        //        dyParam.Add("P_VILLAGECODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.VillageCode);
        //        dyParam.Add("P_VILLAGENAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.VillageName);
        //        dyParam.Add("P_UIDNUMBER", OracleDbType.Varchar2, ParameterDirection.Input, obj.AadhaarNo);
        //        dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
        //        dyParam.Add("P_POLICYSTARTDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyStartDate);
        //        dyParam.Add("P_POLICYENDDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.PolicyEndDate);
        //        dyParam.Add("P_MEMBERID", OracleDbType.Int64, ParameterDirection.Input, obj.MemberID);
        //        dyParam.Add("P_MEMBERNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientName);
        //        dyParam.Add("P_PATIENTCONTACTNUMBER", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientContactNumber);
        //        dyParam.Add("P_PATIENTGENDER", OracleDbType.Varchar2, ParameterDirection.Input, obj.PatientCardGender);
        //        dyParam.Add("P_AGE", OracleDbType.Int64, ParameterDirection.Input, obj.PatientCardAge);
        //        dyParam.Add("P_HEADMEMBERID", OracleDbType.Int64, ParameterDirection.Input, 0); // Hardcoded
        //        dyParam.Add("P_HEADMEMBERNAME", OracleDbType.Varchar2, ParameterDirection.Input, ""); // Hardcoded
        //        dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode);
        //        dyParam.Add("P_HOSPITALNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalName);
        //        dyParam.Add("P_HOSPITALSTATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.Hospitalstatecode); // Added by Ashutosh PradhaN on dated 200223
        //        dyParam.Add("P_HOSPITALDISTRICT", OracleDbType.Varchar2, ParameterDirection.Input, obj.Hospitaldistrictcode);  // Added by Ashutosh PradhaN on dated 200223
        //        dyParam.Add("P_HOSPITALAUTHORITYCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalAuthorityCode);
        //        dyParam.Add("P_MORTALITY", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("P_MORTALITYSUMMARY", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //        dyParam.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input, obj.Remarks);
        //        dyParam.Add("P_TOTALAMOUNTCLAIMED", OracleDbType.Decimal, ParameterDirection.Input, (obj.AmoutBlocked == "" ? 0 : Convert.ToDecimal(obj.AmoutBlocked)));
        //        dyParam.Add("P_ISEMERGENCY", OracleDbType.Varchar2, ParameterDirection.Input, obj.IsEmergency);
        //        dyParam.Add("P_REFERALCODE", OracleDbType.Varchar2, ParameterDirection.Input, (obj.ReferalCode == "" ? "0" : obj.ReferalCode));
        //        dyParam.Add("P_ADMISSIONDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.DATEOFADMISSION);
        //        dyParam.Add("P_DOCTORNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.Doctorname);
        //        dyParam.Add("P_DOCTORPHNO", OracleDbType.Varchar2, ParameterDirection.Input, obj.Doctorphno);
        //        dyParam.Add("P_OVERRIDECODE", OracleDbType.Varchar2, ParameterDirection.Input, (obj.OverrideCode == "" ? "0" : obj.OverrideCode));
        //        dyParam.Add("P_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input, obj.Description);
        //        dyParam.Add("P_USERID", OracleDbType.Int64, ParameterDirection.Input, obj.userID);
        //        dyParam.Add("P_VERIFIEDMEMBERID", OracleDbType.Int64, ParameterDirection.Input, (obj.VerifiedMemberID == "" ? obj.MemberID : obj.VerifiedMemberID));
        //        dyParam.Add("P_VERIFIEDMEMBERNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.VerifiedMemberName);
        //        dyParam.Add("P_VERIFICATIONMODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.AuthenticationMode);

        //        //Vital Insertion   
        //        dyParam.Add("P_XMLVITALPARAMETER", OracleDbType.XmlType, ParameterDirection.Input, CommonExtension.SerializeToXMLString(obj.VitalParams));

        //        //Pacakges Insertion
        //        dyParam.Add("P_XMLPACKAGEDETAILS", OracleDbType.XmlType, ParameterDirection.Input, CommonExtension.SerializeToXMLString(obj.Packageparametermodel));

        //        //Implant Insertion
        //        dyParam.Add("P_XMLIMPLANTDETAILS", OracleDbType.XmlType, ParameterDirection.Input, CommonExtension.SerializeToXMLString(obj.Implantparams));

        //        //Highend drugs Insertion
        //        dyParam.Add("P_XMLHEDDETAILS", OracleDbType.XmlType, ParameterDirection.Input, CommonExtension.SerializeToXMLString(obj.Highenddrugsparams));

        //        dyParam.Add("P_ADMISSIONFILE", OracleDbType.Varchar2, ParameterDirection.Input, obj.UploadDoc);
        //        dyParam.Add("P_HOSPITALCATEGORYID", OracleDbType.Int64, ParameterDirection.Input, obj.HospitalCategoryId);
        //        dyParam.Add("P_NOOFDAYS", OracleDbType.Varchar2, ParameterDirection.Input, (obj.PreAuthNoofDays == "" ? "0" : obj.PreAuthNoofDays));
        //        dyParam.Add("P_MESSAGEOUT", OracleDbType.Int64, ParameterDirection.Output);
        //        var query = "USP_TRANSACTION_AED_BLOCK_TMS";
        //        result = SqlConnecton.Query<BlockPackageFilterModel>(query, dyParam, commandType: CommandType.StoredProcedure).ToList();
        //        // blockPkgStatus = "1";
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //    }
        //    return result;
        //}

        public IList<ViewBlockPackageDetailsModel> GetImplantDetailsListByID(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(20-feb-2023)
        {
            List<ViewBlockPackageDetailsModel> Viewblockpackagelist = new List<ViewBlockPackageDetailsModel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "ID");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ViewBlockPackageDetailsModel>("USP_BLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        public IList<ViewBlockPackageDetailsModel> GetHighendDrugDetailsListByID(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(20-feb-2023)
        {
            List<ViewBlockPackageDetailsModel> Viewblockpackagelist = new List<ViewBlockPackageDetailsModel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "HD");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ViewBlockPackageDetailsModel>("USP_BLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }




        #region :: Kisan
        public List<UnblockPackageFilterModel> CreatePatientUnblockPackage(UnblockPackageModel obj)
        {
            List<UnblockPackageFilterModel> result = new List<UnblockPackageFilterModel>();
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, obj.Action);
                dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                dyParam.Add("P_BLOCKINGINVOICENUMBER", OracleDbType.Varchar2, ParameterDirection.Input, null);
                dyParam.Add("P_MEMBERID", OracleDbType.Int64, ParameterDirection.Input, obj.MemberId);
                dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode.ToString());
                dyParam.Add("P_PACKAGEDETAILS", OracleDbType.XmlType, ParameterDirection.Input, CommonExtension.SerializeToXMLString(obj.RemoveUnblockDetails));
                dyParam.Add("P_REMARK", OracleDbType.Varchar2, ParameterDirection.Input, obj.Remark);
                //dyParam.Add("P_VERIFICATIONDATA", OracleDbType.Varchar2, ParameterDirection.Input,obj.VerificationData);
                // dyParam.Add("P_MSGOUT", OracleDbType.Int64, ParameterDirection.Output);
                dyParam.Add("P_VERIFICATIONMODE", OracleDbType.Varchar2, ParameterDirection.Input, (obj.OverrideCode == "" ? obj.VerificationMode : "OVERRIDE"));
                dyParam.Add("P_OVERRIDECODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.OverrideCode);
                dyParam.Add("P_VERIFICATIONMEMBERID", OracleDbType.Varchar2, ParameterDirection.Input, (obj.VerifiedMemberId == "" ? obj.MemberId : obj.VerifiedMemberId));
                dyParam.Add("P_VERIFIEDMEMBERNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.VerifiedMemberName);
                //dyParam.Add("P_MSGOUT", OracleDbType.Int64, ParameterDirection.Output);
                var query = "USP_T_UNBLOCKPACKAGE_TMS";
                result = SqlConnecton.Query<UnblockPackageFilterModel>(query, dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        public string UpdatePackageApproval(ViewModelPackageUpdation obj)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", obj.ActionCode);
                param.Add("P_URN", obj.urn);
                param.Add("P_TRANSACTIONID", obj.p_transactionid);
                param.Add("P_TXNPACKAGEDETAILID", obj.p_txnpackagedetailid);
                param.Add("P_MEMBERID", obj.p_memberid);
                param.Add("P_PREAUTHSTATUS", obj.p_preauthstatus);
                param.Add("P_REMARK", obj.p_remark);
                param.Add("P_HOSPITALCODE", obj.p_hospitalcode);
                param.Add("P_AMOUNTTOBEBLOCKED", obj.p_amounttobeblocked);
                param.Add("P_OUTMSG", "", DbType.String, ParameterDirection.Output);
                var query = "USP_T_PREAUTH_TMS";
                var packagelist = SqlConnecton.Execute(query, param, commandType: CommandType.StoredProcedure);
                string result = param.Get<string>("P_OUTMSG");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreApprovalViewModel> GetPreapprovalPackagesList(string preauthstatus, string action, string hospitalcode, string FromDate, string ToDate)
        {
            try
            {
                string strOutput = "";
                PreApprovalViewModel pvm = new PreApprovalViewModel();
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, action);
                dyParam.Add("P_PREAUTHSTATUS", OracleDbType.Varchar2, ParameterDirection.Input, preauthstatus.ToString());
                dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, hospitalcode);
                dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, FromDate);
                dyParam.Add("P_REMARK", OracleDbType.Varchar2, ParameterDirection.Input, ToDate);
                dyParam.Add("P_OUTMSG", OracleDbType.Varchar2, ParameterDirection.Input, "");
                var query = "USP_T_PREAUTH_TMS";
                var packagelist = SqlConnecton.Query<PreApprovalViewModel>(query, dyParam, commandType: CommandType.StoredProcedure).ToList();
                return packagelist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PatientInfo> GetOverrideCodeListService(PatientInfo patientInfo) // ADDED by Akshat (25-Jan-23)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, patientInfo.ACTIONCODE);
                param.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, patientInfo.URN);
                param.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, patientInfo.HospitalCode);
                param.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, patientInfo.FromDate); // ADDED by Akshat (10-Feb-23)
                param.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, patientInfo.ToDate); // ADDED by Akshat (10-Feb-23)
                param.Add("P_STATUS", OracleDbType.Varchar2, ParameterDirection.Input, patientInfo.statusflag); // ADDED by Rajkishor (19-Apr-23)
                var query = "USP_GET_FPOVERRIDECODE_TMS";  // UPDATED by Akshat (10-Feb-23)
                var response = SqlConnecton.Query<PatientInfo>(query, param, commandType: CommandType.StoredProcedure).ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OverrideRequestDetails> GenerateOverrideRequestService(PatientInfo patientInfo) // ADDED by Akshat (25-Jan-23)
        {
            List<OverrideRequestDetails> response = new List<OverrideRequestDetails>();
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", patientInfo.ACTIONCODE);
                param.Add("P_URN", patientInfo.URN);
                param.Add("P_MEMBERID", patientInfo.MemberID);
                param.Add("P_HOSPITALCODE", patientInfo.HospitalCode);
                param.Add("P_HOSPITALCATEGORYID", patientInfo.HospitalCategoryId);
                param.Add("P_DESCRIPTION", patientInfo.Remarks);
                param.Add("P_GENERATEDTHROUGH", patientInfo.GENERATEDTHROUGH);
                param.Add("P_NOOFDAYS", patientInfo.NoofDays == "" ? 0 : Convert.ToInt32(patientInfo.NoofDays));
                param.Add("P_FILEUPLOAD", patientInfo.UploadDoc1);
                param.Add("P_MSGOUT", "", DbType.Int32, ParameterDirection.Output, 8);
                var query = "USP_FPOverridecode_TMS";
                //int output = SqlConnecton.Query<int>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                //int outputt = param.Get<int>("P_MSGOUT");
                //return outputt;
                response = SqlConnecton.Query<OverrideRequestDetails>(query, param, commandType: CommandType.StoredProcedure).ToList();
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public int GenerateOverrideRequestService(PatientInfo patientInfo) // ADDED by Akshat (25-Jan-23)
        //{
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("P_ACTION", patientInfo.ACTIONCODE);
        //        param.Add("P_URN", patientInfo.URN);
        //        param.Add("P_MEMBERID", patientInfo.MemberID);
        //        param.Add("P_HOSPITALCODE", patientInfo.HospitalCode);
        //        param.Add("P_HOSPITALCATEGORYID", patientInfo.HospitalCategoryId);
        //        param.Add("P_DESCRIPTION", patientInfo.Remarks);
        //        param.Add("P_GENERATEDTHROUGH", patientInfo.GENERATEDTHROUGH);
        //        param.Add("P_NOOFDAYS", patientInfo.NoofDays == "" ? 0 : Convert.ToInt32(patientInfo.NoofDays));
        //        param.Add("P_FILEUPLOAD", patientInfo.UploadDoc1);
        //        param.Add("P_MSGOUT", "", DbType.Int32, ParameterDirection.Output, 8);
        //        var query = "USP_FPOverridecode_TMS";
        //        int output = SqlConnecton.Query<int>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        int outputt = param.Get<int>("P_MSGOUT");
        //        return outputt;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public List<DischargeFilterModel> PatientDischargeAdd(DischargePatientModel obj)
        {
            List<DischargeFilterModel> result = new List<DischargeFilterModel>();
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.Urn);
                dyParam.Add("P_BLOCKINGINVOICENUMBER", OracleDbType.Varchar2, ParameterDirection.Input, obj.BlockingInvoiceNo);
                dyParam.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, obj.TransactionId);
                dyParam.Add("P_TRANSACTIONDESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input, obj.TransactionDescription);
                dyParam.Add("P_MEMBERID", OracleDbType.Int64, ParameterDirection.Input, obj.MemberId);
                dyParam.Add("P_MORTALITY", OracleDbType.Varchar2, ParameterDirection.Input, obj.Mortality);
                dyParam.Add("P_DISCHARGEDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeDate);
                dyParam.Add("P_USERID", OracleDbType.Int64, ParameterDirection.Input, obj.Userid);
                dyParam.Add("P_XMLVITALPARAMETER", OracleDbType.XmlType, ParameterDirection.Input, obj.VitalParameterList == null ? null : CommonExtension.SerializeToXMLString(obj.VitalParameterList));
                dyParam.Add("P_DISCHARGE_DOC", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeDoc);
                dyParam.Add("P_MORTALITY_DOC", OracleDbType.Varchar2, ParameterDirection.Input, obj.MoralityDoc);
                dyParam.Add("P_REFERRALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.RefaralCode);
                dyParam.Add("P_REFERRALSTATUS", OracleDbType.Varchar2, ParameterDirection.Input, obj.RefaralStatus);
                dyParam.Add("P_REFERHOSPITALSTATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.RefarHospitalState);
                dyParam.Add("P_REFERHOSPITALDISTRICT", OracleDbType.Varchar2, ParameterDirection.Input, obj.RefarHospitalDistrict);
                dyParam.Add("P_REFERHOSPITALNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.RefarHospitalName);
                dyParam.Add("P_REFERHOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.RefarHospitalCode);
                dyParam.Add("P_REFERDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.RefaralDate);
                dyParam.Add("P_REFERRAL_DOC", OracleDbType.Varchar2, ParameterDirection.Input, obj.RefaralDoc);
                dyParam.Add("P_REFERREASON", OracleDbType.Varchar2, ParameterDirection.Input, obj.RefaralReason);
                dyParam.Add("P_FPVERIFIERID", OracleDbType.Varchar2, ParameterDirection.Input, obj.FPVerifiedId);
                dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.HospitalCode);
                dyParam.Add("P_VERIFIEDMEMBERNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.VerifiedMemberName);
                dyParam.Add("P_VERIFICATIONMEMBERID", OracleDbType.Varchar2, ParameterDirection.Input, obj.VerifiedMemberID);
                dyParam.Add("P_VERIFICATIONMODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.AuthenticationMode);
                dyParam.Add("P_FROMDRNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.RefarDoctorName);
                dyParam.Add("P_FROMDEPTNAME", OracleDbType.Varchar2, ParameterDirection.Input, obj.RefarDepartment);
                dyParam.Add("P_ISEMPANELED", OracleDbType.Varchar2, ParameterDirection.Input, obj.IsEmpanel);
                dyParam.Add("P_OVERRIDECODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.Overridecode);
                dyParam.Add("P_INTRASURGERY", OracleDbType.Varchar2, ParameterDirection.Input, obj.IntraSurgeryPic);
                dyParam.Add("P_POSTSURGERY", OracleDbType.Varchar2, ParameterDirection.Input, obj.PostSurgeryPic);
                dyParam.Add("P_PRESURGERY", OracleDbType.Varchar2, ParameterDirection.Input, obj.PreSurgeryPic);
                dyParam.Add("P_SPECIMENREMOVAL", OracleDbType.Varchar2, ParameterDirection.Input, obj.SpecimenRemovalPic);
                dyParam.Add("P_RELAXCLAMAMOUNT", OracleDbType.Int64, ParameterDirection.Input, int.Parse(obj.RelaxClamAmount));
                dyParam.Add("P_CLAIMAMOUNTXML", OracleDbType.XmlType, ParameterDirection.Input, obj.PackageClamAmountList == null ? null : CommonExtension.SerializeToXMLString(obj.PackageClamAmountList));
                dyParam.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input, obj.DischargeRemark);
                //dyParam.Add("P_MESSAGEOUT", OracleDbType.Int64, ParameterDirection.Output);
                var query = "USP_AED_DISCHARGE_TMS";
                result = SqlConnecton.Query<DischargeFilterModel>(query, dyParam, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #region :: KISAN
        public IList<DischargeVm> GetAllDischargeRecord(DischargeFilterModel model)
        {
            List<DischargeVm> UnblockingInfo = new List<DischargeVm>();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, model.Action);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, model.HospitalCode);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, model.URN);
                    parameters.Add("P_MEMBERID ", OracleDbType.Varchar2, ParameterDirection.Input, model.MemberId);
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, model.Invoiceno);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, model.Formdate);
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, model.Todate);
                    parameters.Add("P_SEARCHBY", OracleDbType.Varchar2, ParameterDirection.Input, model.SearchBy == "0" ? "D" : model.SearchBy);
                    UnblockingInfo = SqlConnecton.Query<DischargeVm>("USP_GET_DISCHARGEDPATIENTDETAILS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                UnblockingInfo = null;
                log.Error(ex);
            }

            return UnblockingInfo;
        }

        public DischargeSlipViewModel GetAllDischargeSlip(DischargeFilterModel model)
        {
            DischargeSlipViewModel UnblockingInfo = new DischargeSlipViewModel();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, model.Action);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, model.HospitalCode);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, model.URN);
                    parameters.Add("P_MEMBERID ", OracleDbType.Int64, ParameterDirection.Input, int.Parse(model.MemberId));
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, model.Invoiceno);
                    var result = SqlConnecton.QueryMultiple("USP_GET_DISCHARGEDPATIENTDETAILS", parameters, commandType: CommandType.StoredProcedure);
                    UnblockingInfo.Dischargeslip = result.Read<DischargeSlipVm>().FirstOrDefault();
                    UnblockingInfo.DischargePackageDetails = result.Read<DischargePackageModel>().ToList();
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

        //Add By Rajkishor Patra(07-feb-2023)
        public IList<ViewBlockPackageDetailsModel> GetViewBlockPackageDetails(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(07-feb-2023)
        {
            List<ViewBlockPackageDetailsModel> Viewblockpackagelist = new List<ViewBlockPackageDetailsModel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.fromdate == null ? "0" : obj.fromdate);
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.todate == null ? "0" : obj.todate);
                    parameters.Add("P_SEARCHTYPE", OracleDbType.Varchar2, ParameterDirection.Input, obj.AdmissionDateType);
                    Viewblockpackagelist = SqlConnecton.Query<ViewBlockPackageDetailsModel>("USP_BLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        //END

        //Add By Rajkishor Patra(07-feb-2023)
        public IList<ViewBlockPackageDetailsModel> GetViewBlockPackageDetailsById(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(07-feb-2023)
        {
            List<ViewBlockPackageDetailsModel> Viewblockpackagelist = new List<ViewBlockPackageDetailsModel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "B");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ViewBlockPackageDetailsModel>("USP_BLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        //public IList<ViewBlockPackageDetailsModel> GetViewpackageDetailsById(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(08-feb-2023)
        //{
        //    List<ViewBlockPackageDetailsModel> Viewblockpackagelist = new List<ViewBlockPackageDetailsModel>();
        //    ILogger log = LogManager.GetCurrentClassLogger();
        //    try
        //    {
        //        using (SqlConnecton)
        //        {
        //            var parameters = new OracleDynamicParameters();
        //            parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "PD");
        //            parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
        //            parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //            parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
        //            parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
        //            Viewblockpackagelist = SqlConnecton.Query<ViewBlockPackageDetailsModel>("USP_BLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Viewblockpackagelist = null;
        //        log.Error(ex);
        //    }
        //    return Viewblockpackagelist;
        //}
        public packagedetailsModel GetViewpackageDetailsById(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(08-feb-2023)
        {
            packagedetailsModel Viewblockpackagelist = new packagedetailsModel();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "PD");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    //var result = SqlConnecton.Query<ViewBlockPackageDetailsModel>("USP_BLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                    var result = SqlConnecton.QueryMultiple("USP_BLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure);
                    Viewblockpackagelist.Packagedetails = result.Read<ViewBlockPackageDetailsModel>().ToList();
                    Viewblockpackagelist.implantdetails = result.Read<ImplantData>().ToList();
                    Viewblockpackagelist.highenddrugsdetails = result.Read<HighenDrug>().ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }



        public IList<Admissiondetailsmodel> GetViewAdmissionDetailsById(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(08-feb-2023)
        {
            List<Admissiondetailsmodel> Viewblockpackagelist = new List<Admissiondetailsmodel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "AD");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<Admissiondetailsmodel>("USP_BLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).Select(s => new Admissiondetailsmodel
                    {
                        ADMISSIONDATE = s.ADMISSIONDATE,
                        DOCTORNAME = s.DOCTORNAME,
                        DOCTORPHNO = s.DOCTORPHNO,
                        PATIENTCONTACTNUMBER = s.PATIENTCONTACTNUMBER,
                        OVERRIDECODE = s.OVERRIDECODE,
                        REFERALCODE = s.REFERALCODE,
                        DESCRIPTION = s.DESCRIPTION,
                        URN = s.URN,
                        HOSPITALCODE = s.HOSPITALCODE,
                        PREAUTHDOC = s.PREAUTHDOC,
                        patientimagename = s.PATIENTPHOTO,
                        PATIENTPHOTO = s.PATIENTPHOTO != null ? CommonExtension.displayImage(Convert.ToDateTime(s.ADMISSIONDATE).Year + "/" + s.HOSPITALCODE + "/" + "PatientPic/", s.PATIENTPHOTO) : s.PATIENTPHOTO
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        public IList<ViewBlockPackageDetailsModel> GetViewVitalParameterById(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(08-feb-2023)
        {
            List<ViewBlockPackageDetailsModel> Viewblockpackagelist = new List<ViewBlockPackageDetailsModel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "VP");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ViewBlockPackageDetailsModel>("USP_BLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        //END
        //Add By Rajkishor Patra(07-feb-2023)
        public IList<ViewUN_Blockpackagedetails> GetViewUnBlockPackageDetails(ViewUN_Blockpackagedetails obj) //Add By Rajkishor Patra(07-feb-2023)
        {
            List<ViewUN_Blockpackagedetails> Viewblockpackagelist = new List<ViewUN_Blockpackagedetails>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.fromdate == null ? "0" : obj.fromdate);
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.todate == null ? "0" : obj.todate);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, 0);
                    parameters.Add("P_SEARCHTYPE", OracleDbType.Varchar2, ParameterDirection.Input, obj.searchtype);
                    Viewblockpackagelist = SqlConnecton.Query<ViewUN_Blockpackagedetails>("USP_UNBLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        //END
        public IList<ViewUN_Blockpackagedetails> GetViewUnBlockverifiedDetailsById(ViewUN_Blockpackagedetails obj) //Add By Rajkishor Patra(07-feb-2023)
        {
            List<ViewUN_Blockpackagedetails> Viewblockpackagelist = new List<ViewUN_Blockpackagedetails>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "B");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    parameters.Add("P_SEARCHTYPE", OracleDbType.Varchar2, ParameterDirection.Input, null);
                    Viewblockpackagelist = SqlConnecton.Query<ViewUN_Blockpackagedetails>("USP_UNBLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        public IList<ViewUN_Blockpackagedetails> GetViewUnBlockpackageDetailsById(ViewUN_Blockpackagedetails obj) //Add By Rajkishor Patra(08-feb-2023)
        {
            List<ViewUN_Blockpackagedetails> Viewblockpackagelist = new List<ViewUN_Blockpackagedetails>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "PD");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ViewUN_Blockpackagedetails>("USP_UNBLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        public IList<ViewUN_Blockpackagedetails> GetViewUNBlockAdmissionDetailsById(ViewUN_Blockpackagedetails obj) //Add By Rajkishor Patra(08-feb-2023)
        {
            List<ViewUN_Blockpackagedetails> Viewblockpackagelist = new List<ViewUN_Blockpackagedetails>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "AD");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ViewUN_Blockpackagedetails>("USP_UNBLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        public IList<ViewUN_Blockpackagedetails> GetViewUnBlockVitalParameterById(ViewUN_Blockpackagedetails obj) //Add By Rajkishor Patra(08-feb-2023)
        {
            List<ViewUN_Blockpackagedetails> Viewblockpackagelist = new List<ViewUN_Blockpackagedetails>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "VP");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ViewUN_Blockpackagedetails>("USP_UNBLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }

        //public int PatientReferralService(PatientReferral patientReferral) // ADDED by Akshat (06-Feb-23)
        //{
        //    try
        //    {
        //        var param = new OracleDynamicParameters();
        //        param.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.Action);
        //        param.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.URN);
        //        param.Add("P_MEMBERID", OracleDbType.Int64, ParameterDirection.Input, Convert.ToInt64(patientReferral.MemberId));
        //        param.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.HospitalCode);
        //        param.Add("P_REFERRALDATE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ReferralDate);
        //        param.Add("P_REFERRALCODE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ReferralCode);
        //        param.Add("P_PATIENTNAME", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.PatientName);
        //        param.Add("P_AGE", OracleDbType.Int16, ParameterDirection.Input, Convert.ToInt16(patientReferral.Age));
        //        param.Add("P_GENDER", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.Gender);
        //        param.Add("P_REGDNO", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.RegdNo);
        //        param.Add("P_FROMHOSPITALNAME", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.FromHospitalName);
        //        param.Add("P_FROMHOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.FromHospitalCode);
        //        param.Add("P_FROMDRNAME", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.FromDrName);
        //        param.Add("P_FROMDEPTNAME", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.FromDeptName);
        //        param.Add("P_FROMREFERRALDATE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.FromReferralDate);
        //        param.Add("P_TOSTATE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ToState);
        //        param.Add("P_TODISTRICT", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ToDistrict);
        //        param.Add("P_TOHOSPITAL", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ToHospital);
        //        param.Add("P_TOHOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ToHospitalCode);
        //        param.Add("P_REASONFORREFER", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ReasonForRefer);
        //        param.Add("P_TOREFERRALDATE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ToReferralDate);
        //        param.Add("P_DIAGNOSIS", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.Diagnosis);
        //        param.Add("P_BRIEFHISTORY", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.PatientBriefHistory);
        //        param.Add("P_TREATMENTGIVEN", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.TreatmentGiven);
        //        param.Add("P_INVESTIGATIONREMARK", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.InvestigationRemark);
        //        param.Add("P_TREATMENTADVISED", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.TreatmentAdvised);
        //        param.Add("P_DOCUMENT", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.Document);
        //        param.Add("P_USERID", OracleDbType.Int64, ParameterDirection.Input, Convert.ToInt64(patientReferral.UserId));
        //        var xmlVitalData = patientReferral.VitalParameterList == null ? null : CommonExtension.SerializeToXMLString(patientReferral.VitalParameterList);
        //        param.Add("P_XMLVITALPARAMETER", OracleDbType.XmlType, ParameterDirection.Input, xmlVitalData);
        //        param.Add("P_MSGOUT", OracleDbType.Int16, ParameterDirection.Output);

        //        var query = "USP_REFERRALPROCEDURES_TMS";
        //        int output = SqlConnecton.Query<int>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        return output;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public List<ReturnReferalData> PatientReferralService(PatientReferral patientReferral) // ADDED by Akshat (06-Feb-23)
        {
            List<ReturnReferalData> result = new List<ReturnReferalData>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.Action);
                param.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.URN);
                param.Add("P_MEMBERID", OracleDbType.Int64, ParameterDirection.Input, Convert.ToInt64(patientReferral.MemberId));
                param.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.HospitalCode);
                param.Add("P_REFERRALDATE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ReferralDate);
                param.Add("P_REFERRALCODE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ReferralCode);
                param.Add("P_PATIENTNAME", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.PatientName);
                param.Add("P_AGE", OracleDbType.Int16, ParameterDirection.Input, Convert.ToInt16(patientReferral.Age));
                param.Add("P_GENDER", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.Gender);
                param.Add("P_REGDNO", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.RegdNo);
                param.Add("P_FROMHOSPITALNAME", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.FromHospitalName);
                param.Add("P_FROMHOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.FromHospitalCode);
                param.Add("P_FROMDRNAME", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.FromDrName);
                param.Add("P_FROMDEPTNAME", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.FromDeptName);
                param.Add("P_FROMREFERRALDATE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.FromReferralDate);
                param.Add("P_TOSTATE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ToState);
                param.Add("P_TODISTRICT", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ToDistrict);
                param.Add("P_TOHOSPITAL", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ToHospital);
                param.Add("P_TOHOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ToHospitalCode);
                param.Add("P_REASONFORREFER", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ReasonForRefer);
                param.Add("P_TOREFERRALDATE", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ToReferralDate);
                param.Add("P_DIAGNOSIS", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.Diagnosis);
                param.Add("P_BRIEFHISTORY", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.PatientBriefHistory);
                param.Add("P_TREATMENTGIVEN", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.TreatmentGiven);
                param.Add("P_INVESTIGATIONREMARK", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.InvestigationRemark);
                param.Add("P_TREATMENTADVISED", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.TreatmentAdvised);
                param.Add("P_INVESTIGATIONDOC", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.InvestigationDoc);
                param.Add("P_REFERRALDOC", OracleDbType.Varchar2, ParameterDirection.Input, patientReferral.ReferralDoc);
                param.Add("P_USERID", OracleDbType.Int64, ParameterDirection.Input, Convert.ToInt64(patientReferral.UserId));
                var xmlVitalData = patientReferral.VitalParameterList == null ? null : CommonExtension.SerializeToXMLString(patientReferral.VitalParameterList);
                param.Add("P_XMLVITALPARAMETER", OracleDbType.XmlType, ParameterDirection.Input, xmlVitalData);
                param.Add("P_MSGOUT", OracleDbType.Int16, ParameterDirection.Output);

                var query = "USP_REFERRALPROCEDURES_TMS";
                result = SqlConnecton.Query<ReturnReferalData>(query, param, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public List<PreApprovalViewModel> GetPreapprovalPackagesAddList(string fromdate, string todate, string hospitalcode, string action, string querystatus) //Added by Deepak Kumar Sahu(07-02-2023)
        {
            try
            {
                string strOutput = "";
                PreApprovalViewModel pvm = new PreApprovalViewModel();
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, action);
                dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, hospitalcode);
                dyParam.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, fromdate);
                dyParam.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, todate);
                dyParam.Add("P_REPLYSECOND", OracleDbType.Varchar2, ParameterDirection.Input, querystatus);
                dyParam.Add("P_OUTMSG", OracleDbType.Varchar2, ParameterDirection.Input, "");
                var query = "USP_T_PREAUTHQUERY_TMS";
                var packagelist = SqlConnecton.Query<PreApprovalViewModel>(query, dyParam, commandType: CommandType.StoredProcedure).ToList();
                return packagelist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ////For OTP Check
        //public int InsertOTP(SendOtp obj) // ADDED by Rajkishor (27-Feb-23)
        //{
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("P_URN", obj.URN);
        //        param.Add("P_MEMEBERID", obj.MemberID);
        //        param.Add("P_PATIENTPHONENO", obj.PhoneNo);
        //        param.Add("P_OTP", obj.OTP);
        //        param.Add("P_HOSPITALCODE", obj.HOSPITALCODE);
        //        var query = "USP_T_PATIENTMOBOTP";
        //        int output = SqlConnecton.Query<int>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        int outputt = param.Get<int>("P_MSGOUT");
        //        return outputt;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public List<PreApprovalViewModel> GetSNARemarkById(int packagedetailid, string action) //Added by Deepak Kumar Sahu(08-02-2023)
        {
            try
            {
                string strOutput = "";
                PreApprovalViewModel pvm = new PreApprovalViewModel();
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, action);
                dyParam.Add("P_PACKAGEDETAILID", OracleDbType.Varchar2, ParameterDirection.Input, packagedetailid);
                dyParam.Add("P_OUTMSG", OracleDbType.Varchar2, ParameterDirection.Input, "");
                var query = "USP_T_PREAUTHQUERY_TMS";
                var packagelist = SqlConnecton.Query<PreApprovalViewModel>(query, dyParam, commandType: CommandType.StoredProcedure).ToList();
                return packagelist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SubmitPreAuthReply(PreAuthAddDocuments obj) //Added by Deepak Kumar Sahu(09-02-2023)
        {
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "R");
                dyParam.Add("P_DOCUMENTSECOND", OracleDbType.Varchar2, ParameterDirection.Input, obj.document2);
                dyParam.Add("P_DOCUMENTTHIRD", OracleDbType.Varchar2, ParameterDirection.Input, obj.document3);
                dyParam.Add("P_REPLYSECOND", OracleDbType.Varchar2, ParameterDirection.Input, obj.replySecond);
                dyParam.Add("P_REPLYTHIRD", OracleDbType.Varchar2, ParameterDirection.Input, obj.replyThird);
                dyParam.Add("P_PACKAGEDETAILID", OracleDbType.Int64, ParameterDirection.Input, obj.packagedetailsid);
                dyParam.Add("P_OUTMSG", OracleDbType.Int64, ParameterDirection.Output);
                var query = "USP_T_PREAUTHQUERY_TMS";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
                strOutput = "1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strOutput;
        }
        //For Block Package Slip
        public IList<BlockPackageSlip> GetViewBlockPackageSlip(BlockPackageSlip obj) //Add By Rajkishor Patra(09-feb-2023)
        {
            List<BlockPackageSlip> Viewblockpackagelist = new List<BlockPackageSlip>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "BS");
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, obj.INVOICE);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<BlockPackageSlip>("USP_SLIPGENERATE_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist.ToList();
        }
        public IList<PackagedetailsSlipdata> GetViewBlockPackagedetailsSlip(BlockPackageSlip obj) //Add By Rajkishor Patra(09-feb-2023)
        {
            List<PackagedetailsSlipdata> Viewblockpackagelist = new List<PackagedetailsSlipdata>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "PD");
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, obj.INVOICE);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<PackagedetailsSlipdata>("USP_SLIPGENERATE_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist.ToList();
        }

        public IList<ImplantData> GetViewBlockPackageImplantDataSlip(BlockPackageSlip obj) //Add By Rajkishor Patra(09-feb-2023)
        {
            List<ImplantData> Viewblockpackagelist = new List<ImplantData>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "ID");
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, obj.INVOICE);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ImplantData>("USP_SLIPGENERATE_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist.ToList();
        }
        public IList<HighenDrug> GetViewBlockPackageHighendSlip(BlockPackageSlip obj) //Add By Rajkishor Patra(09-feb-2023)
        {
            List<HighenDrug> Viewblockpackagelist = new List<HighenDrug>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "HD");
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, obj.INVOICE);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<HighenDrug>("USP_SLIPGENERATE_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist.ToList();
        }
        //For UnBlock Package Slip
        public IList<UnBlockPackageSlip> GetViewUnBlockPackageSlip(UnBlockPackageSlip obj) //Add By Rajkishor Patra(10-feb-2023)
        {
            List<UnBlockPackageSlip> Viewunblockpackagelist = new List<UnBlockPackageSlip>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "US");
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, obj.UNBLOCKINGINVOICENUMBER);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewunblockpackagelist = SqlConnecton.Query<UnBlockPackageSlip>("USP_SLIPGENERATE_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Viewunblockpackagelist = null;
                log.Error(ex);
            }
            return Viewunblockpackagelist.ToList();
        }
        public IList<ImplantData> GetViewUnBlockImplantDataSlip(UnBlockPackageSlip obj) //Add By Rajkishor Patra(21-feb-2023)
        {
            List<ImplantData> Viewblockpackagelist = new List<ImplantData>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "UID");
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, obj.UNBLOCKINGINVOICENUMBER);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ImplantData>("USP_SLIPGENERATE_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist.ToList();
        }
        public IList<HighenDrug> GetViewUNBlockHighendSlip(UnBlockPackageSlip obj) //Add By Rajkishor Patra(21-feb-2023)
        {
            List<HighenDrug> Viewblockpackagelist = new List<HighenDrug>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "UHD");
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, obj.UNBLOCKINGINVOICENUMBER);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<HighenDrug>("USP_SLIPGENERATE_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist.ToList();
        }

        public IList<ViewUN_Blockpackagedetails> GetViewUnBlockImplantDetailsListByID(ViewUN_Blockpackagedetails obj) //Add By Rajkishor Patra(08-feb-2023)
        {
            List<ViewUN_Blockpackagedetails> Viewblockpackagelist = new List<ViewUN_Blockpackagedetails>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "ID");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ViewUN_Blockpackagedetails>("USP_UNBLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        public IList<ViewUN_Blockpackagedetails> GetViewUnBlockHighendDrugDetailsListByID(ViewUN_Blockpackagedetails obj) //Add By Rajkishor Patra(08-feb-2023)
        {
            List<ViewUN_Blockpackagedetails> Viewblockpackagelist = new List<ViewUN_Blockpackagedetails>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "HD");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ViewUN_Blockpackagedetails>("USP_UNBLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        public List<PatientReferral> GetPatientReferralListService(PatientReferral patientReferral) // ADDED by Akshat (09-Feb-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", patientReferral.Action);
                param.Add("P_USERID", patientReferral.UserId);
                param.Add("P_FROMDATE", patientReferral.FromReferralDate);
                param.Add("P_TODATE", patientReferral.ToReferralDate);
                param.Add("P_HOSPITALCODE", patientReferral.HospitalCode);
                var query = "USP_GET_REFERRALPROCEDURES";
                var response = SqlConnecton.Query<PatientReferral>(query, param, commandType: CommandType.StoredProcedure).ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<DischargeReferalSlipVm> GetAllDischargeReferalSlip(DischargeFilterModel model)
        {
            List<DischargeReferalSlipVm> UnblockingInfo = new List<DischargeReferalSlipVm>();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, model.Action);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, model.HospitalCode);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, model.URN);
                    parameters.Add("P_MEMBERID ", OracleDbType.Int64, ParameterDirection.Input, int.Parse(model.MemberId));
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, model.Invoiceno);
                    UnblockingInfo = SqlConnecton.Query<DischargeReferalSlipVm>("USP_GET_DISCHARGEDPATIENTDETAILS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                UnblockingInfo = null;
                log.Error(ex);
            }

            return UnblockingInfo;
        }
        //For Privious Package Details
        public IList<PriviousBlockpackageDetails> GetPriviouspackagedetails(PriviousBlockpackageDetails obj) //Add By Rajkishor Patra(22-feb-2023)
        {
            List<PriviousBlockpackageDetails> Viewblockpackagelist = new List<PriviousBlockpackageDetails>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "PB");
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                    parameters.Add("P_MEMBERID", OracleDbType.Varchar2, ParameterDirection.Input, obj.MEMBERID);
                    parameters.Add("P_Hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, obj.HOSPITALCODE);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, 0);

                    Viewblockpackagelist = SqlConnecton.Query<PriviousBlockpackageDetails>("USP_T_Admission_INFO_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist.ToList();
        }

        public DischargeViewDetailsModel GetPatientDischargeViwe(DischargeFilterModel model)
        {
            DischargeViewDetailsModel UnblockingInfo = new DischargeViewDetailsModel();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, model.HospitalCode);
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, model.Invoiceno);
                    var result = SqlConnecton.QueryMultiple("USP_GET_DISCHARGEDPATIENTDETAILS_BYID", parameters, commandType: CommandType.StoredProcedure);
                    UnblockingInfo.PatientDetails = result.Read<PatientDetailsModel>().Select(s => new PatientDetailsModel()
                    {
                        memberid = s.memberid,
                        membername = s.membername,
                        patientgender = s.patientgender,
                        age = s.age,
                        uidnumber = s.uidnumber,
                        treatmenttype = s.treatmenttype,
                        referralflag = s.referralflag,
                        totalpackagecost = s.totalpackagecost,
                        actualdateofdischarge = s.actualdateofdischarge,
                        dischargedoc = s.dischargedoc != null ? s.year + "/" + s.hospitalcode + "/" + "DischargeSlip/" + s.dischargedoc : s.dischargedoc,
                        intrasurgery = s.intrasurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/IntraSurgeryPic/" + s.intrasurgery : s.intrasurgery,
                        presurgery = s.presurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/PreSurgery/" + s.presurgery : s.presurgery,
                        postsurgery = s.postsurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/PostSurgery/" + s.postsurgery : s.postsurgery,
                        specimenremoval = s.specimenremoval != null ? s.year + "/" + s.hospitalcode + "" + "/surgery picture/SpecimenRemovalPic/" + s.specimenremoval : s.specimenremoval,
                        dischargedocname = s.dischargedoc,
                        intrasurgeryname = s.intrasurgery,
                        presurgeryname = s.presurgery,
                        postsurgeryname = s.postsurgery,
                        specimenremovalname = s.specimenremoval
                    }).FirstOrDefault();
                    UnblockingInfo.AdmissionDetails = result.Read<AdmissionDetailsModel>().Select(s => new AdmissionDetailsModel
                    {
                        admissiondate = s.admissiondate != null ? s.admissiondate : "",
                        doctorname = s.doctorname != null ? s.doctorname : "",
                        doctorphno = s.doctorphno != null ? s.doctorphno : "",
                        patientcontactnumber = s.patientcontactnumber != null ? s.patientcontactnumber : "",
                        overridecode = s.overridecode != null ? s.overridecode : "",
                        referalcode = s.referalcode != null ? s.referalcode : "",
                        description = s.description != null ? s.description : "",
                    }).FirstOrDefault();
                    UnblockingInfo.PackageDetails = result.Read<PackageDetailsModel>().ToList();
                    UnblockingInfo.HighAndDrugDetails = result.Read<HighAndDrugModel>().ToList();
                    UnblockingInfo.ImplantDetails = result.Read<ImpantModel>().ToList();
                    UnblockingInfo.VerifiedDetails = result.Read<VerifiedDetailModel>().Select(s => new VerifiedDetailModel
                    {
                        disverifiedmemberid = s.disverifiedmemberid != null ? s.disverifiedmemberid : "",
                        disverifiedmembername = s.disverifiedmembername != null ? s.disverifiedmembername : "",
                        urn = s.urn != null ? s.urn : "",
                        verificationmode = s.verificationmode != null ? s.verificationmode : "",
                        txnpackagedetailid = s.txnpackagedetailid
                    }).ToList();
                    UnblockingInfo.VitalDetails = result.Read<VitalDetailsModel>().ToList();
                    UnblockingInfo.RefaralDetails = result.Read<RefaralModel>().Select(s => new RefaralModel
                    {
                        fromhospitalname = !string.IsNullOrEmpty(s.fromhospitalname) ? s.fromhospitalname : "",
                        fromdrname = !string.IsNullOrEmpty(s.fromdrname) ? s.fromdrname : "",
                        fromdeptname = !string.IsNullOrEmpty(s.fromdeptname) ? s.fromdeptname : "",
                        fromreferraldate = !string.IsNullOrEmpty(s.fromreferraldate) ? s.fromreferraldate : "",
                        tohospital = !string.IsNullOrEmpty(s.tohospital) ? s.tohospital : "",
                        referralcode = !string.IsNullOrEmpty(s.referralcode) ? s.referralcode : "",
                        reasonforrefer = !string.IsNullOrEmpty(s.reasonforrefer) ? s.reasonforrefer : "",
                    }).ToList();
                    UnblockingInfo.DischargeDocDetails = result.Read<DischargeDocModel>().Select(s => new DischargeDocModel
                    {
                        dischargedoc = s.dischargedoc != null ? s.year + "/" + s.hospitalcode + "/" + "DischargeSlip/" + s.dischargedoc : s.dischargedoc,
                        intrasurgery = s.intrasurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/IntraSurgeryPic/" + s.intrasurgery : s.intrasurgery,
                        presurgery = s.presurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/PreSurgery/" + s.presurgery : s.presurgery,
                        postsurgery = s.postsurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/PostSurgery/" + s.postsurgery : s.postsurgery,
                        specimenremoval = s.specimenremoval != null ? s.year + "/" + s.hospitalcode + "" + "/surgery picture/SpecimenRemovalPic/" + s.specimenremoval : s.specimenremoval,
                        txnpackagedetailid = s.txnpackagedetailid,
                        dischargedocname = s.dischargedoc,
                        intrasurgeryname = s.intrasurgery,
                        presurgeryname = s.presurgery,
                        postsurgeryname = s.postsurgery,
                        specimenremovalname = s.specimenremoval
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                UnblockingInfo = null;
                log.Error(ex);
            }

            return UnblockingInfo;
        }

        public IList<ReportListModel> GetViewReportList(ReportListModel obj) //Add By Rajkishor Patra(02-March-2023)
        {
            List<ReportListModel> Viewreportlist = new List<ReportListModel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.HOSPITALCODE);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.fromdate);
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.todate);
                    parameters.Add("P_GENDER", OracleDbType.Varchar2, ParameterDirection.Input, obj.Gender);
                    parameters.Add("P_SEARCHTYPE", OracleDbType.Varchar2, ParameterDirection.Input, obj.AdmissionDateType);
                    Viewreportlist = SqlConnecton.Query<ReportListModel>("USP_T_GETREPORTS_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Viewreportlist = null;
                log.Error(ex);
            }
            return Viewreportlist;
        }

        public UnblockViewDetailsModel GetUnblockDetailsView(UnblockDetailsViewParams model)
        {
            UnblockViewDetailsModel UnblockingInfo = new UnblockViewDetailsModel();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "B");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, model.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, null);
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, null);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Int64, ParameterDirection.Input, model.transactionid);
                    parameters.Add("P_TXNPACKAGEDETAILID", OracleDbType.Int64, ParameterDirection.Input, model.txnpackagedetailid);
                    var result = SqlConnecton.QueryMultiple("USP_UNBLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure);
                    UnblockingInfo.PatientDetails = result.Read<UnblockPatientInformationModel>().FirstOrDefault();
                    UnblockingInfo.PackageDetails = result.Read<UnblockPackageDetailsModel>().ToList();
                    UnblockingInfo.HighAndDrugDetails = result.Read<HighAndDrugModel>().ToList();
                    UnblockingInfo.ImplantDetails = result.Read<ImpantModel>().ToList();
                }
            }
            catch (Exception ex)
            {
                UnblockingInfo = null;
                log.Error(ex);
            }
            return UnblockingInfo;
        }

        public PatientReferralDetailModel GetPatientReferralViewDetails(string referralid)
        {
            PatientReferralDetailModel PatientReferralInfo = new PatientReferralDetailModel();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "B");
                    parameters.Add("P_USERID", OracleDbType.Varchar2, ParameterDirection.Input, null);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, null);
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, null);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Int64, ParameterDirection.Input, null);
                    parameters.Add("P_REFERRALID", OracleDbType.Int64, ParameterDirection.Input, referralid);
                    PatientReferralInfo = SqlConnecton.Query<PatientReferralDetailModel>("USP_GET_REFERRALPROCEDURES", parameters, commandType: CommandType.StoredProcedure).Select(s => new PatientReferralDetailModel
                    {
                        fromhospitalname = s.fromhospitalname,
                        fromdrname = s.fromdrname,
                        fromdeptname = s.fromdeptname,
                        fromreferraldate = s.fromreferraldate,
                        reasonforrefer = s.reasonforrefer,
                        tostate = s.tostate,
                        todistrict = s.todistrict,
                        tohospital = s.tohospital,
                        toreferraldate = s.toreferraldate,
                        diagnosis = s.diagnosis,
                        briefhistory = s.briefhistory,
                        treatmentgiven = s.treatmentgiven,
                        investigationdoc = s.investigationdoc != null ? DateTime.Parse(s.fromreferraldate).Year + "/" + s.hospitalcode + "/" + "InvestigationDoc/" + s.investigationdoc : s.investigationdoc,
                        treatmentadvised = s.treatmentadvised,
                        referraldoc = s.referraldoc != null ? DateTime.Parse(s.fromreferraldate).Year + "/" + s.hospitalcode + "/" + "ReferralDocument/" + s.referraldoc : s.referraldoc,
                        hospitalcode = s.hospitalcode,
                        investigationdocname = s.investigationdoc,
                        referraldocname = s.referraldoc
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                PatientReferralInfo = null;
                log.Error(ex);
            }
            return PatientReferralInfo;
        }
        //For OTP Check
        public string InsertOTP(SendOtp obj) // ADDED by Rajkishor (27-Feb-23)
        {
            try
            {

                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                dyParam.Add("P_MEMEBERID", OracleDbType.Int64, ParameterDirection.Input, obj.MemberID);
                dyParam.Add("P_PATIENTPHONENO", OracleDbType.Varchar2, ParameterDirection.Input, obj.phonenumber);
                dyParam.Add("P_OTP", OracleDbType.Varchar2, ParameterDirection.Input, obj.OTP);
                dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.HOSPITALCODE);
                dyParam.Add("P_MSGOUT", OracleDbType.Int64, ParameterDirection.Output);
                var query = "USP_T_PATIENTMOBOTP";
                var strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
                return strOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Verify Patient Otp
        //public string verifypatientOTP(SendOtp obj) // ADDED by Rajkishor (27-Feb-23)
        //{
        //    try
        //    {

        //        var dyParam = new OracleDynamicParameters();
        //        dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "B");
        //        dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
        //        dyParam.Add("P_MEMEBERID", OracleDbType.Int64, ParameterDirection.Input, obj.MemberID);
        //        dyParam.Add("P_PATIENTPHONENO", OracleDbType.Varchar2, ParameterDirection.Input, obj.phonenumber);
        //        dyParam.Add("P_OTP", OracleDbType.Varchar2, ParameterDirection.Input, obj.OTP);
        //        dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.HOSPITALCODE);
        //        dyParam.Add("P_MSGOUT", OracleDbType.Int64, ParameterDirection.Output);
        //        var query = "USP_T_PATIENTMOBOTP";
        //        var strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
        //        return strOutput;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public string verifypatientOTP(SendOtp obj) // ADDED by Rajkishor (27-Feb-23)
        {
            try
            {

                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "B");
                dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, obj.URN);
                dyParam.Add("P_MEMEBERID", OracleDbType.Int64, ParameterDirection.Input, obj.MemberID);
                dyParam.Add("P_PATIENTPHONENO", OracleDbType.Varchar2, ParameterDirection.Input, obj.phonenumber);
                dyParam.Add("P_OTP", OracleDbType.Varchar2, ParameterDirection.Input, obj.OTP);
                dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.HOSPITALCODE);
                dyParam.Add("P_MSGOUT", OracleDbType.Int64, ParameterDirection.Output);
                var query = "USP_T_PATIENTMOBOTP";
                var output = SqlConnecton.Query<string>(query, dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                // var outputt = dyParam.Get<string>("P_MSGOUT");
                return output;
                // var strOutput = SqlConnecton.Query(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
                //  return strOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public PreauthViewDetailsModel GetPreAuthViewDetailsById(PreauthViewdetails obj) //Add By Rajkishor Patra(08-feb-2023)
        {
            PreauthViewDetailsModel Viewblockpackagelist = new PreauthViewDetailsModel();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");

                    parameters.Add("P_TXNPACKAGEDETAILID", OracleDbType.Varchar2, ParameterDirection.Input, obj.packagedetailsid);
                    var result = SqlConnecton.QueryMultiple("USP_HOS_PREAUTH_DETAILS", parameters, commandType: CommandType.StoredProcedure);
                    Viewblockpackagelist.Packagedetails = result.Read<PreauthViewdetails>().ToList();
                    Viewblockpackagelist.highenddrugsdetails = result.Read<PreauthHighenDrug>().ToList();
                    Viewblockpackagelist.implantdetails = result.Read<PreauthImplantData>().ToList();

                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }


        //for admin block package view 
        public IList<ViewBlockPackageDetailsModel> GetadminViewBlockPackageDetails(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(07-feb-2023)
        {
            List<ViewBlockPackageDetailsModel> Viewblockpackagelist = new List<ViewBlockPackageDetailsModel>();
            ILogger log = LogManager.GetCurrentClassLogger();

            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "B");
                    parameters.Add("P_STATECODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.statecode);
                    parameters.Add("P_DISTRICTCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.districtcode);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.fromdate == null ? "0" : obj.fromdate);
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.todate == null ? "0" : obj.todate);
                    parameters.Add("P_SEARCHTYPE", OracleDbType.Varchar2, ParameterDirection.Input, obj.AdmissionDateType);
                    Viewblockpackagelist = SqlConnecton.Query<ViewBlockPackageDetailsModel>("USP_ADMINDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        public IList<ViewBlockPackageDetailsModel> GetadminViewBlockPackageDetailsById(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(30-may-2023)
        {
            List<ViewBlockPackageDetailsModel> Viewblockpackagelist = new List<ViewBlockPackageDetailsModel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "BV");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ViewBlockPackageDetailsModel>("USP_ADMINDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        public packagedetailsModel GetpackageadminViewById(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(31-May-2023)
        {
            packagedetailsModel Viewblockpackagelist = new packagedetailsModel();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "PD");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    //var result = SqlConnecton.Query<ViewBlockPackageDetailsModel>("USP_BLOCKEDDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                    var result = SqlConnecton.QueryMultiple("USP_ADMINDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure);
                    Viewblockpackagelist.Packagedetails = result.Read<ViewBlockPackageDetailsModel>().ToList();
                    Viewblockpackagelist.implantdetails = result.Read<ImplantData>().ToList();
                    Viewblockpackagelist.highenddrugsdetails = result.Read<HighenDrug>().ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        public IList<ViewBlockPackageDetailsModel> GetadminViewVitalParameterById(ViewBlockPackageDetailsModel obj) //Add By Rajkishor Patra(31-May-2023)
        {
            List<ViewBlockPackageDetailsModel> Viewblockpackagelist = new List<ViewBlockPackageDetailsModel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "VP");
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, obj.TRANSACTIONID);
                    Viewblockpackagelist = SqlConnecton.Query<ViewBlockPackageDetailsModel>("USP_ADMINDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        //END

        //for admin unblock package view
        public IList<ViewUN_Blockpackagedetails> GetadminViewUnBlockPackageDetails(ViewUN_Blockpackagedetails obj) //Add By Rajkishor Patra(31-May-2023)
        {
            List<ViewUN_Blockpackagedetails> Viewblockpackagelist = new List<ViewUN_Blockpackagedetails>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "U");
                    parameters.Add("P_STATECODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.statecode);
                    parameters.Add("P_DISTRICTCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.districtcode);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.hospitalcode);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.fromdate == null ? "0" : obj.fromdate);
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.todate == null ? "0" : obj.todate);
                    parameters.Add("P_TRANSACTIONID", OracleDbType.Varchar2, ParameterDirection.Input, 0);
                    parameters.Add("P_SEARCHTYPE", OracleDbType.Varchar2, ParameterDirection.Input, obj.searchtype);
                    Viewblockpackagelist = SqlConnecton.Query<ViewUN_Blockpackagedetails>("USP_ADMINDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Viewblockpackagelist = null;
                log.Error(ex);
            }
            return Viewblockpackagelist;
        }
        //END

        //for admin Discharge package View
        public IList<DischargeVm> GetadminAllDischargeRecord(DischargeFilterModel model)
        {
            List<DischargeVm> UnblockingInfo = new List<DischargeVm>();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, model.Action);
                    parameters.Add("P_STATECODE", OracleDbType.Varchar2, ParameterDirection.Input, model.statecode);
                    parameters.Add("P_DISTRICTCODE", OracleDbType.Varchar2, ParameterDirection.Input, model.districtcode);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, model.HospitalCode);
                    //parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, model.URN);
                    // parameters.Add("P_MEMBERID ", OracleDbType.Varchar2, ParameterDirection.Input, model.MemberId);
                    // parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, model.Invoiceno);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, model.Formdate);
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, model.Todate);
                    parameters.Add("P_SEARCHTYPE", OracleDbType.Varchar2, ParameterDirection.Input, model.SearchBy == "0" ? "D" : model.SearchBy);
                    UnblockingInfo = SqlConnecton.Query<DischargeVm>("USP_ADMINDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                UnblockingInfo = null;
                log.Error(ex);
            }

            return UnblockingInfo;
        }

        public DischargeViewDetailsModel GetadminPatientDischargeViwe(DischargeFilterModel model)
        {
            DischargeViewDetailsModel UnblockingInfo = new DischargeViewDetailsModel();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, model.HospitalCode);
                    parameters.Add("P_INVOICE", OracleDbType.Varchar2, ParameterDirection.Input, model.Invoiceno);
                    var result = SqlConnecton.QueryMultiple("USP_admin_GET_DISCHARGEDPATIENTDETAILS_BYID", parameters, commandType: CommandType.StoredProcedure);
                    UnblockingInfo.PatientDetails = result.Read<PatientDetailsModel>().Select(s => new PatientDetailsModel()
                    {
                        memberid = s.memberid,
                        membername = s.membername,
                        patientgender = s.patientgender,
                        age = s.age,
                        uidnumber = s.uidnumber,
                        treatmenttype = s.treatmenttype,
                        referralflag = s.referralflag,
                        totalpackagecost = s.totalpackagecost,
                        actualdateofdischarge = s.actualdateofdischarge,
                        dischargedoc = s.dischargedoc != null ? s.year + "/" + s.hospitalcode + "/" + "DischargeSlip/" + s.dischargedoc : s.dischargedoc,
                        intrasurgery = s.intrasurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/IntraSurgeryPic/" + s.intrasurgery : s.intrasurgery,
                        presurgery = s.presurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/PreSurgery/" + s.presurgery : s.presurgery,
                        postsurgery = s.postsurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/PostSurgery/" + s.postsurgery : s.postsurgery,
                        specimenremoval = s.specimenremoval != null ? s.year + "/" + s.hospitalcode + "" + "/surgery picture/SpecimenRemovalPic/" + s.specimenremoval : s.specimenremoval,
                        dischargedocname = s.dischargedoc,
                        intrasurgeryname = s.intrasurgery,
                        presurgeryname = s.presurgery,
                        postsurgeryname = s.postsurgery,
                        specimenremovalname = s.specimenremoval
                    }).FirstOrDefault();
                    UnblockingInfo.AdmissionDetails = result.Read<AdmissionDetailsModel>().Select(s => new AdmissionDetailsModel
                    {
                        admissiondate = s.admissiondate != null ? s.admissiondate : "",
                        doctorname = s.doctorname != null ? s.doctorname : "",
                        doctorphno = s.doctorphno != null ? s.doctorphno : "",
                        patientcontactnumber = s.patientcontactnumber != null ? s.patientcontactnumber : "",
                        overridecode = s.overridecode != null ? s.overridecode : "",
                        referalcode = s.referalcode != null ? s.referalcode : "",
                        description = s.description != null ? s.description : "",
                    }).FirstOrDefault();
                    UnblockingInfo.PackageDetails = result.Read<PackageDetailsModel>().ToList();
                    UnblockingInfo.HighAndDrugDetails = result.Read<HighAndDrugModel>().ToList();
                    UnblockingInfo.ImplantDetails = result.Read<ImpantModel>().ToList();
                    UnblockingInfo.VerifiedDetails = result.Read<VerifiedDetailModel>().Select(s => new VerifiedDetailModel
                    {
                        disverifiedmemberid = s.disverifiedmemberid != null ? s.disverifiedmemberid : "",
                        disverifiedmembername = s.disverifiedmembername != null ? s.disverifiedmembername : "",
                        urn = s.urn != null ? s.urn : "",
                        verificationmode = s.verificationmode != null ? s.verificationmode : "",
                        txnpackagedetailid = s.txnpackagedetailid
                    }).ToList();
                    UnblockingInfo.VitalDetails = result.Read<VitalDetailsModel>().ToList();
                    UnblockingInfo.RefaralDetails = result.Read<RefaralModel>().Select(s => new RefaralModel
                    {
                        fromhospitalname = !string.IsNullOrEmpty(s.fromhospitalname) ? s.fromhospitalname : "",
                        fromdrname = !string.IsNullOrEmpty(s.fromdrname) ? s.fromdrname : "",
                        fromdeptname = !string.IsNullOrEmpty(s.fromdeptname) ? s.fromdeptname : "",
                        fromreferraldate = !string.IsNullOrEmpty(s.fromreferraldate) ? s.fromreferraldate : "",
                        tohospital = !string.IsNullOrEmpty(s.tohospital) ? s.tohospital : "",
                        referralcode = !string.IsNullOrEmpty(s.referralcode) ? s.referralcode : "",
                        reasonforrefer = !string.IsNullOrEmpty(s.reasonforrefer) ? s.reasonforrefer : "",
                    }).ToList();
                    UnblockingInfo.DischargeDocDetails = result.Read<DischargeDocModel>().Select(s => new DischargeDocModel
                    {
                        dischargedoc = s.dischargedoc != null ? s.year + "/" + s.hospitalcode + "/" + "DischargeSlip/" + s.dischargedoc : s.dischargedoc,
                        intrasurgery = s.intrasurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/IntraSurgeryPic/" + s.intrasurgery : s.intrasurgery,
                        presurgery = s.presurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/PreSurgery/" + s.presurgery : s.presurgery,
                        postsurgery = s.postsurgery != null ? s.year + "/" + s.hospitalcode + "/" + "surgery picture/PostSurgery/" + s.postsurgery : s.postsurgery,
                        specimenremoval = s.specimenremoval != null ? s.year + "/" + s.hospitalcode + "" + "/surgery picture/SpecimenRemovalPic/" + s.specimenremoval : s.specimenremoval,
                        txnpackagedetailid = s.txnpackagedetailid,
                        dischargedocname = s.dischargedoc,
                        intrasurgeryname = s.intrasurgery,
                        presurgeryname = s.presurgery,
                        postsurgeryname = s.postsurgery,
                        specimenremovalname = s.specimenremoval
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                UnblockingInfo = null;
                log.Error(ex);
            }

            return UnblockingInfo;
        }


        //END

        //Report Service

        public IList<ReportListModel> GetadminViewReportList(ReportListModel obj) //Add By Rajkishor Patra(05-June-2023)
        {
            List<ReportListModel> Viewreportlist = new List<ReportListModel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "AR");
                    parameters.Add("P_STATECODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.statecode);
                    parameters.Add("P_DISTRICTCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.districtcode);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, obj.HOSPITALCODE);
                    parameters.Add("P_FROMDATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.fromdate);
                    parameters.Add("P_TODATE", OracleDbType.Varchar2, ParameterDirection.Input, obj.todate);
                    parameters.Add("P_GENDER", OracleDbType.Varchar2, ParameterDirection.Input, obj.Gender);
                    parameters.Add("P_SEARCHTYPE", OracleDbType.Varchar2, ParameterDirection.Input, obj.AdmissionDateType);
                    Viewreportlist = SqlConnecton.Query<ReportListModel>("USP_ADMINDETAILSVIEW_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Viewreportlist = null;
                log.Error(ex);
            }
            return Viewreportlist;
        }


    }
}
