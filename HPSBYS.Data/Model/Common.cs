using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{
    public class PolicyDetails
    {
        public string PolicyDistrictCode { get; set; }
        public string Policynumber { get; set; }
        public string PolicyStartDate { get; set; }
        public string PolicyEnddate { get; set; }

    }
    public class Scheme
    {
        public int SchemeCode { get; set; }
        public string SchemeName { get; set; }

    }
    public class PACKAGECATEGORY
    {
        public string PACKAGECATEGORYCODE { get; set; }
        public string PROCEDURES { get; set; }
        public string CriticalCarePackage { get; set; }
        public string SpecialtyType { get; set; }
        //public int Id { get; set; }
    }
    public class SUBPACKAGECATEGORY
    {
        public string SubCat_Code { get; set; }
        public string SubCat_Name { get; set; }
        //public int Id { get; set; }
    }
    public class PackageInformation
    {
        public string PackageId { get; set; }
        public string PackageName { get; set; }
        public string PackageDescription { get; set; }
        public string Amount { get; set; }
        public string package_days { get; set; }
        public int ID { get; set; }
        public string PackageCategoryCode { get; set; }
        public string PreAuthStatus { get; set; }
        public string ClinicalDoc { get; set; }
        public string Claimdoc { get; set; }
        public string MedSergical { get; set; }
        public string PackageMode { get; set; }
        public string CappedAmount { get; set; }
        public int WardType { get; set; }
    }

    public class UnblockingReason
    {
        public int UnblockReasonId { get; set; }
        public string UnblockingReasons { get; set; }
    }

    public class Login
    {
        public int STATUS { get; set; }
        public string Hospitalstate { get; set; }
        public string HospitalDistName { get; set; }
        public string Hospitaldistrictcode { get; set; }
        public string USERID { get; set; }
        public string USERNAME { get; set; }
        public string HospitalName { get; set; }
        public string HospitalCode { get; set; }
        public string Password { get; set; } // OBSOLETE by Akshat (20-Jan-23) replaced by USERPASSWORD
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }
        public string USERPASSWORD { get; set; }  // ADDED by Akshat (20-Jan-23) 
        public decimal Latitude { get; set; }  // ADDED by Akshat (17-Jan-23) 
        public decimal Longitude { get; set; } // ADDED by Akshat (17-Jan-23)
        public int RegBackMonth { get; set; }
        public string LastUpdateDate { get; set; }        
        public string STATECODE { get; set; }//  Added by Ashutosh Pradhan(21-Jan-2023)
        public string HOSPITAL_CATEGORYID { get; set; }
        public string HOSPITALAUTHORITYID { get; set; }
        public string exceptionhospital { get; set; }
        public int?  attempted_status { get; set; }
        public bool? moustatus { get; set; }
        public int? empanelmentstatus_flag { get; set; }
        public string mou_startdate { get; set; }
        public string mou_enddate { get; set; }
        public int? mouleftdays { get; set; }
        public int? mounoticeflag { get; set; }
        public string backdate_admission { get; set; }
        public string backdate_discharge { get; set; }
        public string is_block_active { get; set; }
        public string groupid { get; set; }//view for admin purpose

    }
    public class WardDetail
    {
        public int Childid { get; set; }
        public string Ward { get; set; }
        public string Amount { get; set; }
        public int WardLevel { get; set; }
        public int wardtype { get; set; }
    }
    public class BalanceInfo
    {
        public string AvailableBalance { get; set; }
        public string AmountBlocked { get; set; }
        public string PolicyStartDate { get; set; }
        public string PolicyEnddate { get; set; }
        public string FEMALEFUND { get; set; }
        public string CLAIMEDAMOUNT { get; set; }
    }
    public class OverrideInfo
    {
        public string blockfpoverride { get; set; }
        public string unblockfpoverride { get; set; }
        public string referralcode { get; set; }
        public string dischargefpoverride { get; set; }
    }
    public class ActionTakenByUser  // ADDED by Akshat (23-Jun-23)
    {
        public string UserId { get; set; }
        public string ActionTakenBy { get; set; }
    }
    public class State  // ADDED by Akshat (02-Feb-23)
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }
    }
    public class Dist
    {
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
    }
    public class Block
    {
        public int BlockCode { get; set; }
        public string BlockName { get; set; }
    }
    public class PHC
    {
        public int PHCCode { get; set; }
        public string PHCName { get; set; }
    }
    public class SubCentre
    {
        public int SubCentreCode { get; set; }
        public string SubCentreName { get; set; }
    }
    public class POCLogin
    {
        // public GetLoginDto userDetails { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public string designation { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string Hospital { get; set; }
        public string Hospitalcode { get; set; }
        public string tokenkey { get; set; }
    }

    public class PSSOLogin
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string designation { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string Hospital { get; set; }
        public string Hospitalcode { get; set; }
        public string tokenkey { get; set; }
    }
    public class GetLoginDto
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string designation { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string Hospital { get; set; }
        public string Hospitalcode { get; set; }
        public string tokenkey { get; set; }
    }

    public class userloginmodel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string dateoftoken { get; set; }
    }

    #region :: Kisan Code
    public interface IGenericResult<T>
    {
        bool Status { get; set; }
        string Message { get; set; }
        int Code { get; set; }
        object Data { get; set; }
    }
    public class GenericResult<T> : IGenericResult<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public object Data { get; set; }
    }
    #endregion

    public class PosInformationDTO
    {
        public string deviceslno { get; set; }
        public string aadhar { get; set; }
        public string cardno { get; set; }
        public int memberid { get; set; }
        public string authstatus { get; set; }
        public string Latitute { get; set; }
        public string Longitude { get; set; }
        public string Hospitalcode { get; set; }
    }
    public class ViewPrevBlockedPackage
    {
        public string packageheadercode { get; set; }
        public string procedurecode { get; set; }
        public string packageextention { get; set; }
    }
    //Added by Niranjan on 24-01-2023
    public class ViewModelPackageDetails
    {
        public int packageheaderid { get; set; }
        public string packageheadercode { get; set; }
        public string packageheadername { get; set; }
        public string packagesubcatagoryid { get; set; }
        public string packagesubcode { get; set; }
        public string subpackagename { get; set; }
        public int procedureid { get; set; }
        public string procedurecode { get; set; }
        public string proceduredescription { get; set; }
        public decimal amount { get; set; }
        public string packagecatagorytype { get; set; }
        public int maximumdays { get; set; }
        public string multiprocedure { get; set; }
        public string mandatorypreauth { get; set; }
        public string staytype { get; set; }
        public string daycare { get; set; }
        public string preauthdocs { get; set; }
        public string claimprocessdocs { get; set; }
        public string implantexist { get; set; }
        public string PRICEEDITABLE { get; set; }
        public string packageexceptionflag { get; set; }
        public string packageextention { get; set; }
    }
    public class ViewModelPrevPackageBooked
    {
        public string hospitalcode { get; set; }
        public string transactionid { get; set; }
        public string urn { get; set; }
        public string memberid { get; set; }
        public string membername { get; set; }
        public string blockinginvoiceno { get; set; }
        public string dateofadmission { get; set; }
        public string patientcontactnumber { get; set; }
        public string transactioncode { get; set; }
        public string schemename { get; set; }
        public string headmemberid { get; set; }
        public string headmembername { get; set; }
        public string registrationdate { get; set; }
        public string packageheaderid { get; set; }
        public string packageheadercode { get; set; }
        public string packagename { get; set; }
        public string packagesubcategoryid { get; set; }
        public string packagesubcategorycode { get; set; }
        public string packagesubcategoryname { get; set; }
        public string procedureid { get; set; }
        public string procedurecode { get; set; }
        public string procedurename { get; set; }
        public string amountblocked { get; set; }
        public string txnpackagedetailid { get; set; }

    }

    public class PosInfoDto
    {
        public string DEVICESERIALNO { get; set; }
        public string URN { get; set; }
        public int MEMBERID { get; set; }
        public string LONGITUDE { get; set; }
        public string LATITUDE { get; set; }
        public string POSFLAG { get; set; }
        public string UPLOADDATE { get; set; }
        public string VERIFYSTATUS { get; set; }
        public string VARIFIEDTHROUGH { get; set; }
        public string ISACTIVE { get; set; }
        public string Hospitalcode { get; set; }
    }

    #region :: Kisan 
    public class AadhaarGenerateModel
    {
        public string uid { get; set; }
        public string uidType { get; set; }
        public string subAuaCode { get; set; }
    }
    public class Root
    {
        public string ret { get; set; }
        public object err { get; set; }
        public string status { get; set; }
        public object errMsg { get; set; }
        public string txn { get; set; }
        public string responseCode { get; set; }
        public object uidToken { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }
        public string uid { get; set; }
    }
    public class OTPVerificationModel
    {
        public string uid { get; set; }
        public string txn { get; set; }
        public string otpValue { get; set; }
        public string verificationThrough { get; set; }
        public string memberid { get; set; }
        public string urn { get; set; }
        public string patientid { get; set; }
        public string Hospitalcode { get; set; }
    }
    public class OTPVerificationModelchatbot
    {
        public string uid { get; set; }
        public string txn { get; set; }
        public string otpValue { get; set; }
        public string verificationThrough { get; set; }
    }
    public class OTPVerificationDTO
    {
        public string uid { get; set; }
        public string uidType { get; set; }
        public string consent { get; set; }
        public string subAuaCode { get; set; }
        public string txn { get; set; }
        public string isBio { get; set; }
        public string isOTP { get; set; }
        public string isPI { get; set; }
        public string bioType { get; set; }
        public string rdInfo { get; set; }
        public string rdData { get; set; }
        public string otpValue { get; set; }
    }
    public class PosAuthenticationModel
    {
        public string deviceslno { get; set; }
        public string aadhar { get; set; }
        public string cardno { get; set; }
        public string memberid { get; set; }
        public string authstatus { get; set; }
        public string Latitute { get; set; }
        public string Longitude { get; set; }
    }

    public class Posmesgreturn
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }

    #region :: Iris Model  & Dtos
    public class IRISVerificationModel
    {
        public string uid { get; set; }
        public string urn { get; set; }
        public string memberid { get; set; }
        public string rdInfo { get; set; }
        public string rdData { get; set; }
        public string verificationThrough { get; set; }
        public string patientid { get; set; }
        public string Hospitalcode { get; set; }
    }
    public class IRISVerificationDTO
    {
        public string uid { get; set; }
        public string uidType { get; set; }
        public string consent { get; set; }
        public string subAuaCode { get; set; }
        public string txn { get; set; }
        public string isPI { get; set; }
        public string isBio { get; set; }
        public string isOTP { get; set; }
        public string bioType { get; set; }
        public string name { get; set; }
        public string rdInfo { get; set; }
        public string rdData { get; set; }
        public string otpValue { get; set; }
    }
    public class IRISVerificationDetailsDTO
    {
        public string URN { get; set; }
        public string MemberId { get; set; }
        public string UidNumber { get; set; }
        public string Email { get; set; }
        public object Error { get; set; }
        public object ErrorMsg { get; set; }
        public string MobileNo { get; set; }
        public string ResponseCode { get; set; }
        public string ReturnValue { get; set; }
        public string Status { get; set; }
        public string Txn { get; set; }
        public object UidToken { get; set; }
        public string VerificationThrough { get; set; }
        public string patientid { get; set; }
        public string Hospitalcode { get; set; }
    }
    #endregion
    public class HospitalDto
    {
        public string HospitalCode { get; set; }
        public string Hospitalname { get; set; }
    }

    #endregion

    //New Added by Niranjan Poddar on 03-Feb-2023
    public class ViewModelImplantDetails
    {
        public int implantmasterid { get; set; }
        public string implantcode { get; set; }
        public string implantname { get; set; }
        public string procedurecode { get; set; }
        public string unit { get; set; }
        public string maximumunit { get; set; }
        public string unitcycleprice { get; set; }
        public string pricefixededitable { get; set; }
        public string editflag { get; set; }
        public string priceeditable { get; set; }
        public string uniteditable { get; set; }
    }
    public class ViewModelHrgDrugs
    {
        public int hed_id { get; set; }
        public string implantdetails_id { get; set; }
        public string hed_code { get; set; }
        public string hed_name { get; set; }
        public string unit { get; set; }
        public decimal price { get; set; }
        public string maximumunit { get; set; }
        public string recomendeddose { get; set; }
        public string ispreauthrequired { get; set; }
        public string priceeditable { get; set; }
        public string uniteditable { get; set; }

    }
    public class ViewModelWardList
    {
        public int id { get; set; }
        public string ward { get; set; }
        public decimal amount { get; set; }
        public string preauthstatus { get; set; }
    }
    public class ViewModelWardUnitCost
    {
        public int wardmasterid { get; set; }
        public string unitprice { get; set; }
        public string pricefixededitable { get; set; }
        public string maximumunit { get; set; }

    }

    public class PreAuthModel //ADDED by Akshat (28-Feb-23)
    {
        public string Action { get; set; }
        public string UserId { get; set; }
        public string Fromdate { get; set; }
        public string ToDate { get; set; }
        public string StateCode { get; set; }
        public string DistCode { get; set; }
        public string HospitalCode { get; set; }
        public string Type { get; set; }
        public string PageNum { get; set; }
        public string PerPage { get; set; }
    }

    public class PreAuthDetails //ADDED by Akshat (28-Feb-23)
    {
        public string status { get; set; }
        public string message { get; set; }
        public string code { get; set; }
        public List<PreAuthList> PreauthList { get; set; }
        public PreAuthStatus PreAuthStatusCount { get; set; }
    }
    public class PreAuthStatusModel
    {
        public int NoCount { get; set; }
        public string Description { get; set; }
    }

    public class PreAuthStatus
    {
        public int Fresh { get; set; }
        public int Cancel { get; set; }
        public int Reject { get; set; }
        public int Approve { get; set; }
        public int Auto_Approve { get; set; }
        public int Auto_Reject { get; set; }
        public int Query { get; set; }
        public int Expired { get; set; }
        public int QuerySent { get; set; }
        public int Query_Complied { get; set; }
    }

    public class PreAuthList //ADDED by Akshat (28-Feb-23)
    {
        public string UrnNo { get; set; } //URN
        public string MemberName { get; set; } //PatientName
        public string AuthorityCode { get; set; }
        public string HospitalCode { get; set; }
        public string Hospital_Name { get; set; } //HospitalName
        public string RequestedDate { get; set; }
        public string ProcedureCode { get; set; } //packageCode
        public string ProcedureName { get; set; } //packageName
        public string Description { get; set; }
        public string QueryCount { get; set; }
        public string Amount { get; set; } //claimAmount
        public string ApprovedAmount { get; set; }
        public string NoOfDays { get; set; }
        public string Preauthid { get; set; } // added by ashutosh pradhan
        public string Packagedetailid { get; set; } // added by ashutosh pradhan
        public string ReferralCode { get; set; } // added by ashutosh pradhan
        public string preauthstatus { get; set; } // added by ashutosh pradhan
        public string RequestFlag { get; set; } // added by ashutosh pradhan
        public List<PreAuthDoc> docs { get; set; }
    }

    public class PreAuthDoc //ADDED by Akshat (28-Feb-23)
    {
        public string Addtional_Doc1 { get; set; } // DocName
        public string DocType
        {
            get
            {
                return Addtional_Doc1.Substring(Addtional_Doc1.LastIndexOf('.') + 1);
            }
            set { DocType = value; }
        }
        public string DocLink { get; set; }
    }

    public class PreAuthPackageModel //ADDED by Akshat (01-Mar-23)
    {
        public string UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string StateCode { get; set; }
        public string DistCode { get; set; }
        public string HospitalCode { get; set; }
        public string Flag { get; set; }
        public string Action { get; set; }
        public string preauthid { get; set; }
        public string packagedetailid { get; set; }        
    }

    public class PreAuthPackageDetails //ADDED by Akshat (01-Mar-23)
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }        
        public PreAuthPackageList PreAuthDetails { get; set; }
    }

    public class PreAuthPackageList //ADDED by Akshat (01-Mar-23)
    {
        public string Amount { get; set; } // TotalCost
        public string blockamount { get; set; }
        public string insufficientamount { get; set; }
        // public List<PreAuthPackageQuery> Query { get; set; } commented by ashutosh
        public List<PreAuthPackageHospitalDoc> HospitalDocument { get; set; }
        public List<PreAuthPackageQueryDetails> QueryDetails { get; set; }
        public List<PreAuthPackageImplant> Implant { get; set; }
        public List<PreAuthPackageHighEndDrug> HighEndDrugs { get; set; }
        public List<PreAuthPackageHospitalRemark> PreAuthHospitalRemarks { get; set; } 
        public List<PreAuthPackageSnaRemark> PreAuthSnaRemarks { get; set; } 
    }
    public class PreAuthPackageHospitalRemark  
    {
        public string hospitalremarks1 { get; set; }
        public string hospitalremarks2 { get; set; }
    }
    public class PreAuthPackageSnaRemark  
    {
        public string snaremark { get; set; }
        public string snaremarks1 { get; set; }
        public string snaremarks2 { get; set; }
        public string queryremark1 { get; set; }
        public string queryremark2 { get; set; }
        public string snadescription { get; set; }

    }
    //public class PreAuthPackageQuery // ADDED by Akshat (01-Mar-23)
    //{
    //    public string SDate { get; set; } // QueryDate
    //    public string Description { get; set; }
    //    public List<PreAuthPackageQueryDoc> QueryDocs { get; set; }
    //}
    public class PreAuthPackageHospitalDoc // ADDED by Akshat (01-Mar-23)
    {
        public string HospitalUploadDate { get; set; } // QueryDate
        public string Description { get; set; }
        public List<PreAuthPackageQueryDoc> QueryDocument { get; set; }
    }

    public class PreAuthPackageQueryDoc // ADDED by Akshat (01-Mar-23)
    {
        public string Addtional_Doc1 { get; set; }
        public string DocumentDate { get; set; }
        public string DocType
        {
            get { return Addtional_Doc1.Substring(Addtional_Doc1.LastIndexOf('.') + 1); }
            set { DocType = value; }
        }
        public string Link { get; set; }
        //public string Link
        //{
        //    get { return "UploadDocument/FolderName/Year/Month/QueryDoc1"; }
        //    set { Link = value; }
        //}
    }

    public class PreAuthPackageQueryDetails
    {
        public string SDate { get; set; }
        public string QueryFromSna { get; set; }
        public string ReplyFromHospital { get; set; }
        public string snaremark { get; set; }
        public string queryremark { get; set; }
        public string snadescription { get; set; }
        public List<PreAuthPackageQueryDetailsDoc> SnaQueryDocument { get; set; }
    }

    public class PreAuthPackageQueryDetailsDocModel
    {
        public string Addtional_Doc2 { get; set; }
        public string Addtional_Doc3 { get; set; }
        public string DocTwoDate { get; set; }
        public string DocThreeDate { get; set; }
        public string ReplySecond { get; set; }
        public string ReplyThird { get; set; }
        public string Link2 { get; set; }
        //{
        //    get { return Addtional_Doc2 == "NA" ? "NA" : "UploadDocument/FolderName/Year/Month/Link2"; }
        //    set { Link2 = value; }
        //}
        public string Link3 { get; set; }
        //{
        //    get { return Addtional_Doc3 == "NA" ? "NA" : "UploadDocument/FolderName/Year/Month/Link3"; }
        //    set { Link3 = value; }
        //}
    }

    public class PreAuthPackageQueryDetailsDoc
    {
        public string Document { get; set; }
        public string DocumentDate { get; set; }
        public string DocType
        {
            get { return Document.Substring(Document.LastIndexOf('.') + 1); }
            set { DocType = value; }
        }
        public string Link { get; set; }
    }

    public class PreAuthPackageImplant // sADDED by Akshat (01-Mar-23)
    {
        public int ImplantId { get; set; }
        public string ImplantName { get; set; }
        public string Unit { get; set; }
        public string UnitCyclePrice { get; set; } // PricePerUnit
        public string ImpAmount { get; set; } // TotalPrice
    }

    public class PreAuthPackageHighEndDrug //ADDED by Akshat (01-Mar-23)
    {
        public int HedId { get; set; }
        public string HedName { get; set; } // DrugName
        public string HedUnit { get; set; } // Unit
        public string HedPricePerUnit { get; set; } // PricePerUnit
        public string HedPrice { get; set; } // TotalPrice
    }

    public class PreAuthApprovalModel //ADDED by Akshat (01-Mar-23)
    {
        public string UserId { get; set; }
        public string Action { get; set; }
        public string PreauthId { get; set; }
        public string PackageDetailId { get; set; }
        public string Remark { get; set; }
        public string ApprovedAmount { get; set; }
        public int Remarkid { get; set; }
        public string Snadescription { get; set; }
    }

    public class PreAuthApprovalDetails //ADDED by Akshat (01-Mar-23)
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
    }

    public class SnaRemarksDetails //ADDED by Akshat (02-Mar-23)
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public List<SnaRemarksList> Remarks { get; set; }
    }

    public class SnaRemarksList //ADDED by Akshat (02-Mar-23)
    {
        public string RemarkId { get; set; }
        public string Remark { get; set; }
    }

    public class SnaCountModel //ADDED by Akshat (06-Mar-23)
    {
        public string Action { get; set; }
        public string UserId { get; set; }
    }

    public class SnaCountDetails //ADDED by Akshat (06-Mar-23)
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public SnaCountList CountDetails { get; set; }
    }

    public class SnaCountList //ADDED by Akshat (06-Mar-23)
    {
        public string CompletedCaseCount { get; set; }
        public string FreshCaseCount { get; set; }
        public string QueryCompliedCount { get; set; }
        public string QuerySentCount { get; set; }
    }

    public class SnaCountData //ADDED by Akshat (06-Mar-23)
    {
        public string NoCount { get; set; }
        public string Description { get; set; }
    }

    public class UnblockSearchModel
    {
        public string Action { get; set; }
        public string Urn { get; set; }
        public string MemberId { get; set; }
        public string HospitalCode { get; set; }
        public int TransactionId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string AdmissionDateType { get; set; }
    }

    public class Cardbalanceinfochatbot
    {
        public int totalfamilyfund
        {
            get { return 500000; }
            set { totalfamilyfund = value; }
        }
        public int totalfemalefund
        {
            get { return 500000; }
            set { totalfamilyfund = value; }
        }

        public int availablebalance { get; set; }
        public int amountblocked { get; set; }
        public int claimedamount { get; set; }
        public int femalefund { get; set; }
        public string policystartdate { get; set; }
        public string policyenddate { get; set; }
       
    }
    public class MOUStatusModel
    {
        public string moustatus { get; set; }
        public string mou_startdate { get; set; }
        public string mou_enddate { get; set; }
    }

}
