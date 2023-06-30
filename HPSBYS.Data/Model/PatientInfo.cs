using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{
    public class PatientInfo
    {
        public string PreAuthStatus { get; set; }
        public string VchFile { get; set; }
        public string ACTIONCODE { get; set; }
        public string SchemeCode { get; set; }
        public string MemberStatecode { get; set; }
        public string DistrictCode { get; set; }
        public string MemberDistrictName { get; set; }
        public string MemberDistrictCode { get; set; }
        public string MemberBlockName { get; set; }
        public string MemberBlockCode { get; set; }
        public string MemberVillageName { get; set; }
        public string MemberVillageCode { get; set; }
        public string BlockCode { get; set; }
        public string PanchayatCode { get; set; }
        public string VillageCode { get; set; }
        public string URN { get; set; }
        public string FamilyID { get; set; }
        public string PolicyStartDate { get; set; }
        public string PolicyEndDate { get; set; }
        public string CardType { get; set; }
        public string MemberID { get; set; }
        public string PatientName { get; set; }
        public string PatientContactNumber { get; set; }
        public string Gender { get; set; }
        public string PatientCardGender { get; set; }
        public string Age { get; set; }
        public string PatientCardAge { get; set; }
        public string HeadMemberID { get; set; }
        public string HeadMemberName { get; set; }
        public string VerifiedMemberID { get; set; }
        public string VerifiedMemberName { get; set; }
        public string InsuranceCompanyCode { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public string HospitalCode { get; set; }
        public string HospitalName { get; set; }
        public string HospitalState { get; set; }
        public string HospitalDistrict { get; set; }
        public string HospitalAuthorityCode { get; set; }
        public string HospitalCategoryId { get; set; } // ADDED by Akshat (25-Jan-23)
        public string RegistrationNo { get; set; }
        public string RegistrationUserDate { get; set; }
        public string TransactionDate { get; set; }
        public string BlockingInvoiceNo { get; set; }
        public string BlockingUserDate { get; set; }
        public string DATEOFADMISSION { get; set; }
        public string UnblockingInvoiceNo { get; set; }
        public string UnblockingDesc { get; set; }
        public string UnblockingSystemDate { get; set; }
        public string DischargeInvoiceNo { get; set; }
        public string DischargeDesc { get; set; }
        public string DischargeUserDate { get; set; }
        public string DATEOFDISCHARGE { get; set; }
        public string Mortality { get; set; }
        public string MortalitySummary { get; set; }
        public string ProcedureCode { get; set; }
        public string ProcedureName { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public int WardId { get; set; }
        public string PackageCost { get; set; }
        public string NoofDays { get; set; }
        public string AmoutBlocked { get; set; }
        public string NoofDaysActual { get; set; }
        public string TotalAmountClaimed { get; set; }
        public string AvailableBalance { get; set; }
        public string TransactionCode { get; set; }
        public string TotalAmtBlockedOnCard { get; set; }
        public string InsufficientBalanceAmount { get; set; }
        public string OriginalPackageCost { get; set; }
        public string intClaimStatus { get; set; }
        public string claimid { get; set; }
        public string statusflag { get; set; }
        public string patientSlip { get; set; }
        public string TransactionID { get; set; }
        public string IsMedSergical { get; set; }
        public string PackageMode { get; set; }
        public string AuthenticationMode { get; set; }
        public string VerifiedDocumentType { get; set; }
        public string VerifiedDocumentName { get; set; }
        public string PatientPhoto { get; set; }
        public string CappedAmount { get; set; }
        public string TreatmentCompletionCer { get; set; }
        public string Category { get; set; }
        public string CategoryCode { get; set; }

        // --- Added on 19-01-2023
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string PanchayatName { get; set; }
        public string BlockName { get; set; }
        public string VillageName { get; set; }
        public string AadhaarNo { get; set; }
        public string Remarks { get; set; }
        public string Doctorname { get; set; }
        public string Doctorphno { get; set; }
        public string Description { get; set; }
        public string MemberStateName { get; set; }
        public string ReferalCode { get; set; }
        public string OverrideCode { get; set; }
        public int userID { get; set; }
        public List<VitalParameterModel> VitalParams { get; set; }
        public List<packageParameterModel> Packageparametermodel { get; set; }
        public string IsEmergency { get; set; }
        public string ApproveStatus { get; set; } // ADDED by Akshat (10-Feb-23)
        public string FromDate { get; set; } // ADDED by Akshat (10-Feb-23)
        public string ToDate { get; set; } // ADDED by Akshat (10-Feb-23)
        public string GENERATEDTHROUGH { get; set; }//Aded By Rajkishor Patra(11-Feb-2023)
        public string UploadDoc { get; set; }//Aded By Rajkishor Patra(13-Feb-2023)
        public string UploadDoc1 { get; set; }//Aded By Rajkishor Patra(14-Feb-2023)
        public List<ImplantParameterModel> Implantparams { get; set; }
        public List<HighenddrugsParameterModel> Highenddrugsparams { get; set; }
        public string Hospitalstatecode { get; set; }
        public string Hospitaldistrictcode { get; set; }
        public string PreAuthNoofDays { get; set; }
        public string HospitalauthorityId { get; set; }
        public string IsVerified { get; set; }
        public string parenttransactionid { get; set; }
    }
    public class ImplantParameterModel
    {
        public string procedureId { get; set; }
        public string procedureCode { get; set; }
        public string implantname { get; set; }
        public string implantprice { get; set; }
        public string implantquantity { get; set; }
        public string totalprice { get; set; }
        public string Ispreauth { get; set; }
        public string implantcode { get; set; }

    }
    public class HighenddrugsParameterModel
    {
        public string ProcId { get; set; }
        public string ProcCode { get; set; }
        public string HedId { get; set; }
        public string HedCode { get; set; }
        public string IsAuthRequired { get; set; }
        public string UnitPrice { get; set; }
        public string quantity { get; set; }
        public string totlprice { get; set; }
        public string HEDName { get; set; }
    }
    #region:: Kisan
    public class UnblockPackageModel
    {
        public string Action { get; set; }
        public string URN { get; set; }
        public string MemberId { get; set; }
        public string TransactionID { get; set; }
        public string HospitalCode { get; set; }
        public List<UnblockPackageDetail> RemoveUnblockDetails { get; set; }
        public string Remark { get; set; }
        public string VerificationData { get; set; }
        public string VerificationMode { get; set; }
        public string VerifiedMemberId { get; set; }
        public string VerifiedMemberName { get; set; }
        public string OverrideCode { get; set; }

    }
    public class UnblockPackageDetail
    {
        public string URN { get; set; }
        public string PackageHeaderId { get; set; }
        public string PackageSubcategoryId { get; set; }
        public string ProcedureId { get; set; }
        public string AmountTobeUnBlocked { get; set; }
        public string PackageDetailId { get; set; }
        public string TransactionCode { get; set; }
    }
    #endregion

    public class VitalParameterModel
    {
        public int VITALSIGNID { get; set; }
        public string VITALSIGN { get; set; }
        public string VITALVALUE { get; set; }
    }

    public class packageParameterModel
    {
        public int PACKAGEHEADERID { get; set; }
        public string PACKAGEHEADERCODE { get; set; }
        public string PACKAGEHEADERNAME { get; set; }
        public string PACKAGESUBCATEGORYID { get; set; }
        public string PACKAGESUBCATEGORYCODE { get; set; }
        public string PACKAGESUBCATEGORYNAME { get; set; }
        public string PROCEDUREID { get; set; }
        public string PROCEDURECODE { get; set; }
        public string PROCEDURENAME { get; set; }
        public string HOSPITALCATEGORYID { get; set; }
        public string PACKAGECATEGORYTYPE { get; set; }
        public string STAYTYPE { get; set; }
        public string DAYCARE { get; set; }
        public string NOOFDAYS { get; set; }
        public string PREAUTHSTATUS { get; set; }
        public string AMOUNTBLOCKED { get; set; }
        public string WARDID { get; set; }
        public string PACKAGECOST { get; set; }
        public string WARDCOST { get; set; }
        //public string wardName { get; set; }
        //public string PackageHeaderName { get; set; }
        public string WARDNAME { get; set; }
        public string TOTALPACKAGECOST { get; set; }
        public string TOTALIMPLCOST { get; set; }
        public string TOTALHEDCOST { get; set; }
    }

    #region :: Kisan Discharge
    public class DischargePatientModel
    {
        public string Urn { get; set; }
        public string BlockingInvoiceNo { get; set; }
        public int? TransactionId { get; set; }
        public string TransactionDescription { get; set; }
        public int? MemberId { get; set; }
        public string Mortality { get; set; }
        public string DischargeDate { get; set; }
        public int? Userid { get; set; }
        public List<VitalParameterModel> VitalParameterList { get; set; }
        public string DischargeDoc { get; set; }
        public string MoralityDoc { get; set; }
        public string RefaralCode { get; set; }
        public string RefaralStatus { get; set; }
        public string RefarHospitalState { get; set; }
        public string RefarHospitalDistrict { get; set; }
        public string RefarHospitalName { get; set; }
        public string RefarHospitalCode { get; set; }
        public string RefaralDate { get; set; }
        public string RefaralDoc { get; set; }
        public string RefaralReason { get; set; }
        public string FPVerifiedId { get; set; }
        public string HospitalCode { get; set; }
        public string VerifiedMemberID { get; set; }
        public string VerifiedMemberName { get; set; }
        public string AuthenticationMode { get; set; }
        public string RefarDoctorName { get; set; }
        public string RefarDepartment { get; set; }
        public string IsEmpanel { get; set; }
        public string Overridecode { get; set; }
        public string IntraSurgeryPic { get; set; }
        public string PostSurgeryPic { get; set; }
        public string PreSurgeryPic { get; set; }
        public string SpecimenRemovalPic { get; set; }
        public List<DischargeClamAmountModel> PackageClamAmountList { get; set; }
        public string RelaxClamAmount { get; set; }
        public string DischargeRemark { get; set; }
    }
    public class DischargeClamAmountModel
    {
        public string urn { get; set; }
        public string packageheaderid { get; set; }
        public string packagesubcategoryid { get; set; }
        public string procedureid { get; set; }
        public string packagedetailid { get; set; }
        public string transactioncode { get; set; }
        public string blockedamount { get; set; }
        public string insufficientamount { get; set; }
        public string clamamount { get; set; }
    }
    public class ReturnReferalData
    {
        public int memberid { get; set; }
        public string urn { get; set; }
    }
    public class DischargeFilterModel
    {
        public string Action { get; set; }
        public string HospitalCode { get; set; }
        public string URN { get; set; }
        public string MemberId { get; set; }
        public string Invoiceno { get; set; }
        public string SearchBy { get; set; }
        public string Formdate { get; set; }
        public string Todate { get; set; }
        public string returnmessage { get; set; }
        public string groupid { get; set; }
        public string districtcode { get; set; }
        public string statecode { get; set; }

    }

    #endregion

    public class UnblockPackageFilterModel
    {
        public int TRANSACTIONID { get; set; }//Aded By Rajkishor Patra(14-Feb-2023)
        public string UNBLOCKINGINVOICENUMBER { get; set; }//Aded By Rajkishor Patra(14-Feb-2023)
        public string URN { get; set; }//Aded By Rajkishor Patra(14-Feb-2023)

    }
    public class BlockPackageFilterModel
    {
        public int TRANSACTIONID { get; set; }//Aded By Rajkishor Patra(14-Feb-2023)
        public string INVOICE { get; set; }//Aded By Rajkishor Patra(14-Feb-2023)
        public string URN { get; set; }//Aded By Rajkishor Patra(14-Feb-2023)
        public string TOTALBLOCKED { get; set; }
        public string FAMILYAVAILABLE { get; set; }
        public string FEMALEAVAILABLE { get; set; }
        public int STATUS { get; set; }
        public string CASENO { get; set; }
        public int ADMISSIONFLAG { get; set; }

    }

    public class OverrideRequestDetails
    {
        public int exchospitaltype { get; set; }
        public string blockoverridecode { get; set; }
        public string unblockoverridecode { get; set; }
        public string dischargeoverridecode { get; set; }
        public int statecode { get; set; }
        public int output { get; set; }
        
    }

    public class PatientReferral // ADDED by Akshat (06-Feb-23)
    {
        public string Action { get; set; }
        public string URN { get; set; }
        public string MemberId { get; set; }
        public string HospitalCode { get; set; }
        public string ReferralDate { get; set; }
        public string ReferralCode { get; set; }
        public string ReferralStatus { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string RegdNo { get; set; }
        public string FromHospitalName { get; set; }
        public string FromHospitalCode { get; set; }
        public string FromDrName { get; set; }
        public string FromDeptName { get; set; }
        public string FromReferralDate { get; set; }
        public string ToState { get; set; }
        public string ToDistrict { get; set; }
        public string ToHospital { get; set; }
        public string ToHospitalCode { get; set; }
        public string ReasonForRefer { get; set; }
        public string ToReferralDate { get; set; }
        public string Diagnosis { get; set; }
        public string PatientBriefHistory { get; set; }
        public string TreatmentGiven { get; set; }
        public string InvestigationRemark { get; set; }
        public string TreatmentAdvised { get; set; }
        //public string Document { get; set; }
        public string UserId { get; set; }
        public List<VitalParameterModel> VitalParameterList { get; set; }
        public string XmlVitalParameter { get; set; }
        public string InvestigationDoc { get; set; }
        public string ReferralDoc { get; set; }
        public string referalid { get; set; }
    }
    public class PatientReferralDetailModel
    {
        public string fromhospitalname { get; set; }
        public string fromdrname { get; set; }
        public string fromdeptname { get; set; }
        public string fromreferraldate { get; set; }
        public string reasonforrefer { get; set; }
        public string tostate { get; set; }
        public string todistrict { get; set; }
        public string tohospital { get; set; }
        public string toreferraldate { get; set; }
        public string diagnosis { get; set; }
        public string briefhistory { get; set; }
        public string treatmentgiven { get; set; }
        public string investigationdoc { get; set; }
        public string treatmentadvised { get; set; }
        public string referraldoc { get; set; }
        public string hospitalcode { get; set; }
        public string investigationdocname { get; set; }
        public string referraldocname { get; set; }

    }
}
