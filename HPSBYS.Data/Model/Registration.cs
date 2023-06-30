using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{

    public class PatientRegistrationInformation
    {
        public int WardLogId { get; set; }
        public int RejectCount { get; set; }
        public string PreAuthDt { get; set; }
        public string URN { get; set; }
        public string CardNo { get; set; }
        public string MemberName { get; set; }
        public int MemberID { get; set; }
        public string PatientContactNumber { get; set; }
        public string InvoiceNo { get; set; }
        public string RegistrationDate { get; set; }
        public string WardBlockingDate { get; set; }
        public string TransactionID { get; set; }
        public string PreAuthStatus { get; set; }
        public string ProcedureName { get; set; }
        public string PackageName { get; set; }
        public string Category { get; set; }
        public string Ward { get; set; }

        public string PackageCost { get; set; }
        public string Amount { get; set; }
        public string AmoutBlocked { get; set; }
        public string HeadMemberName { get; set; }
        public string MemberDistrictName { get; set; }
        public string MemberStateName { get; set; }
        public string TPARemarks { get; set; }
        public string PreAuthDoc { get; set; }
        public int AddOnStatus { get; set; }

    }





    public class Registration
    {
        public string SchemeCode { get; set; }
        public string SchemeName { get; set; }
        public string URN { get; set; }
        public string CardNo { get; set; }
        public string MemberName { get; set; }
        public string PatientContactNumber { get; set; }
        public int MemberID { get; set; }
        public string InvoiceNo { get; set; }
        public string RegistrationDate { get; set; }
        public int HeadMemberID { get; set; }
        public string HeadMemberName { get; set; }
        public string MemberDistrictName { get; set; }
        public string MemberDistrictCode { get; set; }
        public string MemberStateName { get; set; }
        public string MemberStateCode { get; set; }
        public string MemberBlock { get; set; }
        public string MemberBlockCode { get; set; }
        public string PanchayatCode { get; set; }
        public string MemberPanchayatName { get; set; }

        public string MemberVillageName { get; set; }
        public string VillageCode { get; set; }
        public string PreAuthStatus { get; set; }
        public string ProcedureCode { get; set; }
        public string ProcedureName { get; set; }

        public string PackageName { get; set; }
        public string packageheadercode { get; set; }
        public string NoofDays { get; set; }
        public string AmountBlocked { get; set; }
        public string BlockingUserDate { get; set; }
        public string PackageCost { get; set; }
        public string TransactionID { get; set; }
        public string IsMedSergical { get; set; }
        public string FamilyID { get; set; }
        public int WardId { get; set; }
        public string Ward { get; set; }
        public int WardLevel { get; set; }
        public string Category { get; set; }
        public string CategoryCode { get; set; }
        public int WordChangeCount { get; set; }
        public int AddOnStatus { get; set; }
        public string HospitalCode { get; set; }
        public string TXNPACKAGEDETAILID { get; set; }
        public string PACKAGEHEADERID { get; set; }
        public string PACKAGESUBCATEGORYID { get; set; }
        public string PROCEDUREID { get; set; }
        public string Transactioncode { get; set; }
        public int totalblockedamount { get; set; }
        public string UIDNUMBER { get; set; }
        public string AGE { get; set; }
        public string PATIENTGENDER { get; set; }
        public string insufficientamount { get; set; }
        public string totalpackagecost { get; set; }
        public int pendingpackages { get; set; }
        public string caseno { get; set; }
        public string discharge_flag { get; set; }

    }
    //Used For Unblocking Page
    public class UnblockingInfo : Registration
    {
        //public string ProcedureCode { get; set; }
        ////public string ProcedureName { get; set; }
        //public string PackageCode { get; set; }
        //// public string PackageName { get; set; }
        //public string AmoutBlocked { get; set; }
        public string DATEOFADMISSION { get; set; }
        public string BlockingInvoiceNo { get; set; }
        public string BlockingDate { get; set; }
    }

    public class DischargeSummary
    {
        public string id { get; set; }
        public string URN { get; set; }
        public string PatientName { get; set; }
        public string BlockingInvoiceNo { get; set; }
        public string RegistrationUserDate { get; set; }
        public string PatientContactNumber { get; set; }
        public int ReferalStatus { get; set; }
    }
    public class DischargeInformation : Registration
    {
        //public string ProcedureCode { get; set; }
        //// public string ProcedureName { get; set; }
        //public string PackageCode { get; set; }
        ////public string PackageName { get; set; }
        //public string AmoutBlocked { get; set; }
        public string DATEOFADMISSION { get; set; }
        // public string BlockingInvoiceNo { get; set; }
        public string PackageMode { get; set; }
        public string CappedAmount { get; set; }
        public string ISExtensionofstay { get; set; }
        public string dtmNextRequestdate { get; set; }
        public string SumAmountBlocked { get; set; }
        public int PackageChangeStatus { get; set; }
        public string TemporaryDischargeDate { get; set; }
        public string TemporaryDiscargeAmount { get; set; }
        public string LastPackageBlockingDate { get; set; }

    }

    public class PreAuth
    {
        public string Action { get; set; }
        public string intPreAuthId { get; set; }
        public string VCHURNNO { get; set; }
        public string intMemberId { get; set; }
        public string vchMemberName { get; set; }
        public string vchPackageCategory { get; set; }
        public string VchPackageDetail { get; set; }
        public string dtmDate { get; set; }
        public string VchFile { get; set; }
        public string VchRemarks { get; set; }
        public string INT_SH_ID { get; set; }
        public string INT_PHASE_ID { get; set; }
        public string DEC_AMOUNT { get; set; }
        public string INT_SCHEME_ID { get; set; }
        public string vchActionRemarks { get; set; }
        public string intAprovedBy { get; set; }
        public string dtmAction { get; set; }
        public string INT_CREATED_BY { get; set; }
        public string DTM_CREATED_ON { get; set; }
        public string INT_STATUS_FLAG { get; set; }
        public string BlockingInvoiceNo { get; set; }

    }


    public class PreAuthApprovedPackageBlock
    {
        public string ACTION { get; set; }
        public string URN { get; set; }
        public string TransactionID { get; set; }
        public string BlockingInvoiceNo { get; set; }
        public string BlockinguserDate { get; set; }
        public string HospitalAuthorityCode { get; set; }
        public string HospitalCode { get; set; }
        public string VchFileName { get; set; }
        public int WardLogId { get; set; }
        public string Amount { get; set; }

    }

    public class PackageExtension
    {

        public string URN { get; set; }
        public string PatientName { get; set; }
        public string PackageName { get; set; }
        public string PackageCost { get; set; }
        public string DATEOFADMISSION { get; set; }
        public string HospitalCode { get; set; }
        public string BlockingInvoiceNo { get; set; }
        public string NoofextendDays { get; set; }
        public string INT_CREATED_BY { get; set; }
        public int TransactionId { get; set; }
        public string Gender { get; set; }
        public string VchFile { get; set; }
    }
    public class CancelPackage
    {
        public string Action { get; set; }
        public string BlockingINVOICENO { get; set; }
        public int TransactionID { get; set; }
        public string CancelReason { get; set; }
        public string CancelDate { get; set; }
        public int WardLogId { get; set; }
    }
    public class DischargeVm
    {
        public string URN { get; set; }
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string InvoiceNo { get; set; }
        public string PatientContactNumber { get; set; }
        public string DateofDischarge { get; set; }
        public string SYSDISCHARGEDATE { get; set; }
        public string ReferralStatus { get; set; }
        public string caseno { get; set; }
        public string status { get; set; }
    }
    public class DischargeReferalSlipVm
    {
        public string PatientName { get; set; }
        public string MemberID { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string RegdNo { get; set; }
        public string urn { get; set; }
        public string referralcodeanddate { get; set; }
        public string FromHospitalName { get; set; }
        public string FromDrName { get; set; }
        public string fromreferraldate { get; set; }
        public string tostate { get; set; }
        public string todistrict { get; set; }
        public string tohospital { get; set; }
        public string reasonforrefer { get; set; }
        public string toreferraldate { get; set; }
        public string diagnosis { get; set; }
        public string briefhistory { get; set; }
        public string treatmentgiven { get; set; }
        public string investigationremark { get; set; }
        public string treatmentadvised { get; set; }
        public string document { get; set; }
        public string vitalsign { get; set; }
        public string vitalvalue { get; set; }
    }

    public class DischargeSlipVm
    {
        public string urn { get; set; }
        public string caseno { get; set; }
        public string transactiondate { get; set; }
        public string datetime { get; set; }
        public string membername { get; set; }
        public string headmembername { get; set; }
        public string familyrelation { get; set; }
        public string verifiedname { get; set; }
        public string verifiedrelation { get; set; }
        public string treatments { get; set; }
        public string package { get; set; }
        public string hospitalclaimamount { get; set; }
        public string amount { get; set; }
        public string insufficientamount { get; set; }
        public string hospitalcode { get; set; }
        public string hospitalname { get; set; }
        public string hospitalauthoritycode { get; set; }
        public string familyfund { get; set; }
        public string femalefund { get; set; }
        public string totalblockedamount { get; set; }
        public string totalclaimedamount { get; set; }
    }

    public class DischargeViewDetailsModel
    {
        public PatientDetailsModel PatientDetails { get; set; }
        public AdmissionDetailsModel AdmissionDetails { get; set; }
        public List<PackageDetailsModel> PackageDetails { get; set; }
        public List<HighAndDrugModel> HighAndDrugDetails { get; set; }
        public List<ImpantModel> ImplantDetails { get; set; }
        public List<VerifiedDetailModel> VerifiedDetails { get; set; }
        public List<VitalDetailsModel> VitalDetails { get; set; }
        public List<RefaralModel> RefaralDetails { get; set; }
        public List<DischargeDocModel> DischargeDocDetails { get; set; }
    }
    public class PatientDetailsModel
    {
        public string membername { get; set; }
        public string memberid { get; set; }
        public string patientgender { get; set; }
        public string age { get; set; }
        public string uidnumber { get; set; }
        public string treatmenttype { get; set; }
        public string dischargedoc { get; set; }
        public string intrasurgery { get; set; }
        public string presurgery { get; set; }
        public string postsurgery { get; set; }
        public string specimenremoval { get; set; }
        public string referralflag { get; set; }
        public string totalpackagecost { get; set; }
        public string actualdateofdischarge { get; set; }
        public string hospitalcode { get; set; }
        public string year { get; set; }
        public string dischargedocname { get; set; }
        public string intrasurgeryname { get; set; }
        public string presurgeryname { get; set; }
        public string postsurgeryname { get; set; }
        public string specimenremovalname { get; set; }
    }
    public class AdmissionDetailsModel
    {
        public string admissiondate { get; set; }
        public string doctorname { get; set; }
        public string doctorphno { get; set; }
        public string patientcontactnumber { get; set; }
        public string overridecode { get; set; }
        public string referalcode { get; set; }
        public string description { get; set; }
    }
    public class PackageDetailsModel
    {
        public string txnpackagedetailid { get; set; }
        public string packageheadername { get; set; }
        public string packagesubcategoryname { get; set; }
        public string procedurecode { get; set; }
        public string totalpackagecost { get; set; }
        public string amountblocked { get; set; }
        public string insufficientamount { get; set; }
        public string noofdays { get; set; }
        public string preauthstatus { get; set; }
        public string blockinguserdate { get; set; }
    }
    public class HighAndDrugModel
    {
        public string txnpackagedetailid { get; set; }
        public string hedname { get; set; }
        public string hedunit { get; set; }
        public string hedpriceperunit { get; set; }
        public string hedprice { get; set; }
        public string preauth { get; set; }
        public string totalhedprice { get; set; }
    }
    public class ImpantModel
    {
        public string txnpackagedetailid { get; set; }
        public string implantname { get; set; }
        public string unit { get; set; }
        public string unitcycleprice { get; set; }
        public string amount { get; set; }
        public string totalamount { get; set; }
    }
    public class VerifiedDetailModel
    {
        public string disverifiedmemberid { get; set; }
        public string disverifiedmembername { get; set; }
        public string urn { get; set; }
        public string verificationmode { get; set; }
        public string txnpackagedetailid { get; set; }
    }
    public class VitalDetailsModel
    {
        public string vitalsign { get; set; }
        public string vitalvalue { get; set; }
    }
    public class RefaralModel
    {
        public string fromhospitalname { get; set; }
        public string fromdrname { get; set; }
        public string fromdeptname { get; set; }
        public string fromreferraldate { get; set; }
        public string tohospital { get; set; }
        public string referralcode { get; set; }
        public string reasonforrefer { get; set; }
    }
    public class DischargeDocModel
    {
        public string package { get; set; }
        public string actualdateofdischarge { get; set; }
        public string year { get; set; }
        public string hospitalcode { get; set; }
        public string intrasurgery { get; set; }
        public string postsurgery { get; set; }
        public string presurgery { get; set; }
        public string specimenremoval { get; set; }
        public string dischargedoc { get; set; }
        public string mortalitydoc { get; set; }
        public string txnpackagedetailid { get; set; }
        public string dischargedocname { get; set; }
        public string intrasurgeryname { get; set; }
        public string presurgeryname { get; set; }
        public string postsurgeryname { get; set; }
        public string specimenremovalname { get; set; }
    }

    #region :: UnblockPackageDetailsView
    public class UnblockDetailsViewParams
    {
        public string hospitalcode { get; set; }
        public string transactionid { get; set; }
        public string txnpackagedetailid { get; set; }

    }
    public class UnblockViewDetailsModel
    {
        public UnblockPatientInformationModel PatientDetails { get; set; }
        public List<UnblockPackageDetailsModel> PackageDetails { get; set; }
        public List<HighAndDrugModel> HighAndDrugDetails { get; set; }
        public List<ImpantModel> ImplantDetails { get; set; }
    }
    public class UnblockPatientInformationModel
    {
        public string memberid { get; set; }
        public string membername { get; set; }
        public string uidnumber { get; set; }
        public string invoice { get; set; }
        public string patientgender { get; set; }
        public string age { get; set; }
        public string surgicaltype { get; set; }
        public string urn { get; set; }
        public string verifiedmemberid { get; set; }
        public string verifiedmembername { get; set; }
        public string verificationmode { get; set; }
        public string verifiedmemberaadhar { get; set; }
        public string verifiedmembergender { get; set; }
        public string verifiedmemberage { get; set; }
        public string remarks { get; set; }
        public string admissiondate { get; set; }
        public string doctorname { get; set; }
        public string doctorphno { get; set; }
        public string patientcontactnumber { get; set; }
        public string overridecode { get; set; }
        public string referalcode { get; set; }
        public string description { get; set; }
        public string txnpackagedetailid { get; set; }
    }
    public class UnblockPackageDetailsModel
    {
        public string packageheadername { get; set; }
        public string packagesubcategoryname { get; set; }
        public string procedurecode { get; set; }
        public string procedurename { get; set; }
        public string blockinguserdate { get; set; }
        public string unblockeddate { get; set; }
        public string days { get; set; }
        public string treatmentcost { get; set; }
        public string wardcost { get; set; }
        public string totalpackagecost { get; set; }
        public string preauthstatus { get; set; }
        public string txnpackagedetailid { get; set; }
    }
    #endregion

    public class DischargeSlipViewModel
    {
        public DischargeSlipVm Dischargeslip { get; set; }
        public List<DischargePackageModel> DischargePackageDetails { get; set; }
    }
    public class DischargePackageModel
    {
        public string treatments { get; set; }
        public string package { get; set; }
        public string transactiondate { get; set; }
        public string actualdateofdischarge { get; set; }
        public string amount { get; set; }
        public string hospitalclaimamount { get; set; }
        public string verifiedname { get; set; }
        public string verifiedthrough { get; set; }
    }
}
