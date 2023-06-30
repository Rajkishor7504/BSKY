using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{
    public class PatientStats
    {
        public int Admission { get; set; }
        public int Block { get; set; }
        public int Unblock { get; set; }
        public int Pedingpre_auth { get; set; }
        public int Discharge { get; set; }
    }
    public class ReportModel
    {
        public string Action { get; set; }
        public string HospitalCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Urn { get; set; }
        public string TransactionId { get; set; }
        public string AdmissionDateType { get; set; }
        public string IsPreAuth { get; set; }
        public string AuthType { get; set; }
        public string DateType { get; set; }
        public string UserId { get; set; }
        public string groupid { get; set; }
        public string districtcode { get; set; }
        public string statecode { get; set; }

    }

    public class AdmissionStats
    {
        public string Urn { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string AdmissionDate { get; set; }
        public string CaseNo { get; set; }
        public string Gender { get; set; }
    }

    public class DischargeStats
    {
        public string Urn { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string DateOfDischarge { get; set; }
        public string CaseNo { get; set; }
        public string Gender { get; set; }
        public string ADMISSIONDATE { get; set; }
    }

    public class BlockedStats
    {
        public string Urn { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string PackageHeaderName { get; set; }
        public string ProcedureCode { get; set; }
        public string BlockingUserDate { get; set; }
        public string Gender { get; set; }
        public string CASENO { get; set; }
        public int AMOUNTBLOCKED { get; set; }
        public string PREAUTHSTATUS { get; set; }
        public string ADMISSIONDATE { get; set; }
    }
    public class HospitalReferralStats
    {
        public string Urn { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string FromHospitalName { get; set; }
        public string ToHospitalName { get; set; }
        public string ReferralDate { get; set; }
        public string ReferralCode { get; set; }
        public string Status { get; set; }
    }

    public class UnblockedStats : BlockedStats
    {
    }

    public class PreAuthStats : BlockedStats
    {
    }

    public class HospitalPreAuthStats
    {
        public string Urn { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string AppliedDate { get; set; }
        public string PatientContactNumber { get; set; }
        public string Package { get; set; }
        public string ApprovedAmount { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string SnaRemarks { get; set; }
        public string ActionTakenBy { get; set; }
        public string ApprovedDate { get; set; }
        public string RequestAmount { get; set; }
        public string DistrictName { get; set; }
        public string HospitalName { get; set; }
        public string StateName { get; set; }
        public string StatusDate { get; set; }
    }
    public class DashboardStats
    {
        public PatientDashboardStats patientStats { get; set; }
        public ProcedureDashboardStats procedureStats { get; set; }
        public PreAuthDashboardStats preAuthStats { get; set; }
        public OutboundcallDashboardStats outboundCallStats { get; set; }
        public ReferralAuthDashboardStats referralAuthStats { get; set; }
        public AuthModeDashboardStats authModeStats { get; set; }
        public OverrideCodeDashboardStats overrideCodeStats { get; set; }
    }

    public class PatientDashboardStats
    {
        public string Total_Admission { get; set; }
        public string Total_Ongoing { get; set; }
        public string Total_Discharge { get; set; }
        public List<PatientDashboardMonthwiseStats> patientStatsMonthwise { get; set; }
    }

    public class PatientDashboardMonthwiseStats
    {
        public string Month { get; set; }
        public string Total_Admission_Monthwise { get; set; }
        public string Total_Ongoing_Monthwise { get; set; }
        public string Total_Discharge_Monthwise { get; set; }
    }

    public class ProcedureDashboardStats
    {
        public string Total_Block { get; set; }
        public string Total_Unblock { get; set; }
        public string Total_Discharge { get; set; }
    }

    public class PreAuthDashboardStats
    {
        public string Total_Response { get; set; }
        public string Total_Approved_Amount { get; set; }
        public string Total_Reject { get; set; }
        public string Total_Approve { get; set; }
        public string Total_AutoApprove { get; set; }
        public string Total_Blocked { get; set; }
        public string Total_Pending { get; set; }
        public string Total_Expired { get; set; }
        public string Response_Awaited { get; set; }
        public string Pending_Amount { get; set; }
        public string Fresh_Cases { get; set; }
        public string Reverted_Cases { get; set; }
    }

    public class OutboundcallDashboardStats
    {
        public string TotalCallConnected { get; set; }
        public string Yes { get; set; }
        public string No { get; set; }
        public string Positive { get; set; }
        public string Negetive { get; set; }
        public string Support { get; set; }
        public string Behaviour { get; set; }
        public string Bribe { get; set; }
        public string FreshCase { get; set; }
        public string NotConnected { get; set; }
        public string TotalCallNotConnected { get; set; }
        public string Per_Positive { get; set; }
        public string Per_Negetive { get; set; }
        public string Per_Yes { get; set; }
        public string Per_No { get; set; }
        public string Per_FreshCase { get; set; }
        public string Per_NotConnected { get; set; }
    }

    public class ReferralAuthDashboardStats
    {
        public string Auth_Percent { get; set; }
        public string Dc_Action { get; set; }
        public string Authenticate { get; set; }
        public string Not_Authenticate { get; set; }
        public string Auto_Authenticate { get; set; }
    }

    public class AuthModeDashboardStats
    {
        public string Pos { get; set; }
        public string Iris { get; set; }
        public string Otp { get; set; }
        public string Override { get; set; }
    }

    public class OverrideCodeDashboardStats
    {
        public string Total_Request { get; set; }
        public string Total_Approve { get; set; }
        public string Total_Reject { get; set; }
        public string Total_Pending { get; set; }
    }

    public class HospitalMortalityStats
    {
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string AdmissionDate { get; set; }
        public string DateOfDischarge { get; set; }
        public string PatientGender { get; set; }
        public string AadharNumber { get; set; }
        public string PatientContactNumber { get; set; }
        public string CaseNo { get; set; }
        public string TransactionId { get; set; }
        public string Urn { get; set; }
        public string HospitalName { get; set; }
        public string HospitalDistrictName { get; set; }
        public string HospitalStateName { get; set; }
        public List<HighenDrug> HedDetails { get; set; }
        public List<ImplantData> ImplantDetails { get; set; }
        public List<PackageData> PackageDetails { get; set; }
    }

    public class PackageData
    {
        public string TxnPackageDetailId { get; set; }
        public string PackageHeaderName { get; set; }
        public string PackageSubCategoryName { get; set; }
        public string ProcedureCode { get; set; }
        public string ProcedureName { get; set; }
        public string BlockDate { get; set; }
        public string Days { get; set; }
        public string TreatmentCost { get; set; }
        public string AmountBlocked { get; set; }
        public string TotalPackageCost { get; set; }
        public string PreAuthStatus { get; set; }
    }

    //Adding for Package Details on dated 06-06-2023
    public class HospitalPackageBaseStats
    {
        public string CaseNo { get; set; }
        public string Urn { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string PackageName { get; set; }
        public string ProcedureCode { get; set; }
        public string IsPreAuth { get; set; }
        public string VerificationMode { get; set; }
        public string DistrictName { get; set; }
        public string HospitalName { get; set; }
        public string StateName { get; set; }
    }

    public class HospitalPackageBlockStats : HospitalPackageBaseStats
    {
        public string AdmissionDate { get; set; }
        public string BlockDateTime { get; set; }
        public string TotalPackageCost { get; set; }
        public string AmountBlocked { get; set; }
        public string InsufficientAmount { get; set; }
        public string SurgicalType { get; set; }
    }

    public class HospitalPackageUnblockStats : HospitalPackageBaseStats
    {
        public string BlockDateTime { get; set; }
        public string UnblockDateTime { get; set; }
        public string TotalPackageCost { get; set; }
        public string UnblockAmount { get; set; }
        public string UnblockingInvoiceNumber { get; set; }
    }

    public class HospitalPackageDischargeStats : HospitalPackageBaseStats
    {
        public string DischargeDate { get; set; }
        public string PackageDischargeDateTime { get; set; }
        public string AmountBlocked { get; set; }
        public string Claimed_Amount { get; set; }
        public string InsufficientAmount { get; set; }
    }
    // Ending PackageDetails

    public class PatientMobileVerificationStats
    {
        public string AdmissionDate { get; set; }
        public string DistrictName { get; set; }
        public string HospitalName { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string PatientContactNo { get; set; }
        public string StateName { get; set; }
        public string Status { get; set; }
        public string TreatmentType { get; set; }
        public string Urn { get; set; }
    }

    public class AuthenticationDetalModel
    {
        public string Action { get; set; }
        public string HospitalCode { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string groupid { get; set; }
        public string statecode { get; set; }
        public string districtcode { get; set; }
    }
}
