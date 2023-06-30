using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{
    public class ChartBotURNInformation
    {
        public string URN { get; set; }
    }
    public  class URNInformation
    {
        public string URN { get; set; }
        public string HOH { get; set; }
        public string HeadMemberID { get; set; }
        public string MemberState { get; set; }
        public string MemberDistrict { get; set; }
        public string MemberDistrictCode { get; set; }
        public string MemberBlock { get; set; }
        public string MemberBlockCode { get; set; }
        public string MemberPanchayatCode { get; set; }
        public string MemberPanchayatName { get; set; }
        public string MemberVillageCode { get; set; }
        public string MemberVillageName { get; set; }
        public string MemberStateCode { get; set; }
        public string MemberName { get; set; }
        public int MemberID { get; set; }
        public int MemberAge { get; set; }
        public String MemberGender { get; set; }
        // public string MemberContactnumber { get; set; }
        // public string MemberRegDate { get; set; }
        // public string MemberAdmissionSlip { get; set; }
        public string Aadharnumber { get; set; }
        public string MaskAadharnumber { get; set; }
        public string RationCardNumber { get; set; }
        public string FamilyID { get; set; }
        public string policystartdate { get; set; }
        public string policyenddate { get; set; }
        public string RationCardBackDoc { get; set; }
        public string Adharcard { get; set; }
        public string Mobile { get; set; }
        public int Cardtype { get; set; }
        public string admissionstatus { get; set; }
    }
    public class PriviousBlockpackageDetails //Added By Rajkishor Patra(22-feb-2023)
    {
        public string URN { get; set; }
        public string MEMBERID { get; set; }

        public string HOSPITALCODE { get; set; }
        public string PACKAGEHEADER { get; set; }
        public string PACKAGESUBCATEGORY { get; set; }
        public string PROCEDURENAME { get; set; }
        public int TREATMENTCOST { get; set; }

    }

    public class userURNmodel
    {
        public string URN { get; set; }
        public int Cardtype { get; set; }
    }
    public class UserAadhaar
    {
        public string aadhaar { get; set; }
    }
    public class packagedetailsModel
    {
        public List<ViewBlockPackageDetailsModel> Packagedetails { get; set; }
        public List<ImplantData> implantdetails { get; set; }
        public List<HighenDrug> highenddrugsdetails { get; set; }
    }

    public class ViewBlockPackageDetailsModel
    {

        public string ADMISSIONDATE { get; set; }
        public string INVOICE { get; set; }
        public string CASENO { get; set; }
        public string PATIENTCONTACTNUMBER { get; set; }
        public string UIDNUMBER { get; set; }
        public string URN { get; set; }
        public string PATIENTGENDER { get; set; }
        public int AGE { get; set; }
        public string MEMBERNAME { get; set; }
        public string MEMBERID { get; set; }
        public string BlockingInvoiceNo { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string hospitalcode { get; set; }
        public int TRANSACTIONID { get; set; }
        public string SURGICALTYPE { get; set; }
        public string VERIFIEDMEMBERID { get; set; }
        public string VERIFIEDMEMBERNAME { get; set; }
        public string VERIFICATIONMODE { get; set; }
        public string VERIFIEDMEMBERAADHAR { get; set; }
        public string VERIFIEDMEMBERGENDER { get; set; }
        public string VERIFIEDMEMBERAGE { get; set; }
        public string PACKAGEHEADERNAME { get; set; }
        public string PACKAGESUBCATEGORYNAME { get; set; }
        public string PROCEDURECODE { get; set; }
        public string PROCEDURENAME { get; set; }
        public string BLOCKINGUSERDATE { get; set; }
        public int DAYS { get; set; }
        public string TREATMENTCOST { get; set; }
        public string PREAUTHSTATUS { get; set; }
        public string DOCTORNAME { get; set; }
        public string DOCTORPHNO { get; set; }
        public string OVERRIDECODE { get; set; }
        public string REFERALCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string VITALSIGN { get; set; }
        public string VITALVALUE { get; set; }
        public string REMARKS { get; set; }
        public string IMPLANTNAME { get; set; }
        public int UNIT { get; set; }
        public int UNITCYCLEPRICE { get; set; }
        public int AMOUNT { get; set; }

        public string HEDNAME { get; set; }
        public string PREAUTH { get; set; }
        public int HEDPRICEPERUNIT { get; set; }
        public int HEDUNIT { get; set; }
        public int HEDPRICE { get; set; }
        public int TXNPACKAGEDETAILID { get; set; }
        public int TOTALPACKAGECOST { get; set; }
        public int AMOUNTBLOCKED { get; set; }
        public string AdmissionDateType { get; set; }
        public string BlockingDate { get; set; }
        public string groupid { get; set; }
        public string districtcode { get; set; }
        public string statecode { get; set; }




    }
    public class Admissiondetailsmodel
    {
        public string OVERRIDECODE { get; set; }
        public string REFERALCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string PATIENTCONTACTNUMBER { get; set; }
        public string DOCTORNAME { get; set; }
        public string DOCTORPHNO { get; set; }
        public string ADMISSIONDATE { get; set; }
        public string URN { get; set; }
        public string PREAUTHDOC { get; set; }//Rajkishor(15-Mar-2023)
        public string PATIENTPHOTO { get; set; }//Rajkishor(15-Mar-2023)
        public string HOSPITALCODE { get; set; }//Rajkishor(15-Mar-2023)
        public string patientimagename { get; set; }//Rajkishor(15-Mar-2023)

    }
    public class ViewUN_Blockpackagedetails
    {
        public string ADMISSIONDATE { get; set; }
        public string INVOICE { get; set; }
        public string PATIENTCONTACTNUMBER { get; set; }
        public string UIDNUMBER { get; set; }
        public string URN { get; set; }
        public string PATIENTGENDER { get; set; }
        public int AGE { get; set; }
        public string MEMBERNAME { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string hospitalcode { get; set; }
        public int TRANSACTIONID { get; set; }
        public string UNBLOCKEDDATE { get; set; }
        public string UNBLOCKEDINVOICE { get; set; }
        public string MEMBERID { get; set; }
        public string BlockingInvoiceNo { get; set; }
        public string SURGICALTYPE { get; set; }
        public string VERIFIEDMEMBERID { get; set; }
        public string VERIFIEDMEMBERNAME { get; set; }
        public string VERIFICATIONMODE { get; set; }
        public string VERIFIEDMEMBERAADHAR { get; set; }
        public string VERIFIEDMEMBERGENDER { get; set; }
        public string VERIFIEDMEMBERAGE { get; set; }
        public string PACKAGEHEADERNAME { get; set; }
        public string PACKAGESUBCATEGORYNAME { get; set; }
        public string PROCEDURECODE { get; set; }
        public string PROCEDURENAME { get; set; }

        public int DAYS { get; set; }
        public string TREATMENTCOST { get; set; }
        public string PREAUTHSTATUS { get; set; }
        public string DOCTORNAME { get; set; }
        public string DOCTORPHNO { get; set; }
        public string OVERRIDECODE { get; set; }
        public string REFERALCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string VITALSIGN { get; set; }
        public string VITALVALUE { get; set; }
        public string REMARKS { get; set; }
        public string IMPLANTNAME { get; set; }
        public int UNIT { get; set; }
        public int UNITCYCLEPRICE { get; set; }
        public int AMOUNT { get; set; }

        public string HEDNAME { get; set; }
        public string PREAUTH { get; set; }
        public int HEDPRICEPERUNIT { get; set; }
        public int HEDUNIT { get; set; }
        public int HEDPRICE { get; set; }
        public int txnpackagedetailid { get; set; }
        public string caseno { get; set; }
        public string searchtype { get; set; }


        public string groupid { get; set; }
        public string districtcode { get; set; }
        public string statecode { get; set; }



    }
    public class BlockPackageSlip
    {
        public int TXNPACKAGEDETAILID { get; set; }
        public int TOTALPACKAGECOST { get; set; }

        public int TRANSACTIONID { get; set; }//Aded By Rajkishor Patra(14-Feb-2023)
        public string BlockingTransaction { get; set; }
        public string MEMBERNAME { get; set; }
        public string MEMBERID { get; set; }
        public string FAMILYRELATION { get; set; }
        public string HEADMEMBERNAME { get; set; }
        public string VERIFIEDMEMBERNAME { get; set; }
        public string VERIFIERFAMILYRELATION { get; set; }
        public string VERIFICATIONMODE { get; set; }
        public string IMPLANTDATA { get; set; }
        public string INVOICE { get; set; }
        public string URN { get; set; }
        public string CASENO { get; set; }
        public string HOSPITALNAME { get; set; }
        public string HOSPITALCODE { get; set; }
        public string HOSPITALAUTHORITYCODE { get; set; }
        public string DATETIME { get; set; }
        public string ADMISSIONDATE { get; set; }
        public string PROCEDURECODE { get; set; }
        public string PACKAGESUBCATEGORYNAME { get; set; }
        public string PROCEDURENAME { get; set; }
        public string NOOFDAYS { get; set; }
        public string AMOUNTBLOCKED { get; set; }
        public string TOTALBLOCKED { get; set; }
        public string INSUFFICIENTAMOUNT { get; set; }
        public string AVAILABLEBALNCE { get; set; }
        public string TOTALAMOUNT { get; set; }
        public string PACKAGEHEADERNAME { get; set; }
        public string HED { get; set; }
        public string WARDNAME { get; set; }
        public int PACKAGECOST { get; set; }
        public string BLOCKDATE { get; set; }
    }
    public class BlockSlipModel
    {
        public List<UnBlockPackageSlip> unblockpackageslip { get; set; }
        public List<BlockPackageSlip> blockpackageslip { get; set; }
        public List<ImplantData> implantdata { get; set; }
        public List<HighenDrug> highendrug { get; set; }
        public List<PackagedetailsSlipdata> packagedetailsdata { get; set; }
    }

    public class PackagedetailsSlipdata
    {
        public string WARDNAME { get; set; }
        public int PACKAGECOST { get; set; }
        public string BLOCKDATE { get; set; }
       
        public int TXNPACKAGEDETAILID { get; set; }
        public int TOTALPACKAGECOST { get; set; }
        public string PROCEDURECODE { get; set; }
        public string PACKAGEHEADERNAME { get; set; }
        public string PACKAGESUBCATEGORYNAME { get; set; }
        public string PROCEDURENAME { get; set; }
        public string NOOFDAYS { get; set; }
        public string AMOUNTBLOCKED { get; set; }
    }
    public class ImplantData
    {
        public int TXNPACKAGEDETAILID { get; set; }
        public string INVOICE { get; set; }
        public string URN { get; set; }
        public string IMPLANTNAME { get; set; }
        public int UNIT { get; set; }
        public int UNITCYCLEPRICE { get; set; }
        public int AMOUNT { get; set; }
        public int TOTALAMOUNT { get; set; }
    }
    //public class SendOtp
    //{
    //    public string URN { get; set; }
    //    public string HOSPITALCODE { get; set; }
    //    public string MemberID { get; set; }
    //    public string PhoneNo { get; set; }
    //    public string OTP { get; set; }
    //}
    public class SendOtp
    {
        public string action { get; set; }
        public string department_id { get; set; }
        public string template_id { get; set; }
        public string sms_content { get; set; }
        public string phonenumber { get; set; }
        public string URN { get; set; }
        public int MemberID { get; set; }
        public string OTP { get; set; }
        public string HOSPITALCODE { get; set; }

    }

    public class HighenDrug
    {
        public int TXNPACKAGEDETAILID { get; set; }
        public string INVOICE { get; set; }
        public string URN { get; set; }
        public string HEDNAME { get; set; }
        public string PREAUTH { get; set; }
        public int HEDPRICEPERUNIT { get; set; }
        public int HEDUNIT { get; set; }
        public int HEDPRICE { get; set; }
        public int TOTALHEDPRICE { get; set; }
    }
    public class UnBlockPackageSlip
    {
        public int TRANSACTIONID { get; set; }//Aded By Rajkishor Patra(14-Feb-2023)
        public string UnblockingTransaction { get; set; }
        public string UNBLOCKINGINVOICENUMBER { get; set; }
        public string MEMBERNAME { get; set; }
        public string FAMILYRELATION { get; set; }
        public string HEADMEMBERNAME { get; set; }
        public string UNBLOCKVERIFIEDMEMBERNAME { get; set; }
        public string VERIFIERFAMILYRELATION { get; set; }
        public string CASENO { get; set; }
        public string URN { get; set; }
        public string HOSPITALCODE { get; set; }
        public string HOSPITALNAME { get; set; }
        public string HOSPITALAUTHORITYCODE { get; set; }
        public string DATETIME { get; set; }
        public string ADMISSIONDATE { get; set; }
        public string PROCEDURECODE { get; set; }
        public string AMOUNTBLOCKED { get; set; }
        public string INSUFFICIENTAMOUNT { get; set; }
        public string AVAILABLEBALNCE { get; set; }
        public string IMPLANTDATA { get; set; }
        public string WARDNAME { get; set; }
        public string HED { get; set; }

    }

    public class ReportListModel
    {
        public string HOSPITALCODE { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string ADMISSION { get; set; }
        public string BLOCK { get; set; }
        public string UNBLOCK { get; set; }
        public string PEDINGPREAUTH { get; set; }
        public string DISCHARGE { get; set; }
        public string HOSPITALNAME { get; set; }
        public string Gender { get; set; }
        public string AdmissionDateType { get; set; }

        public string groupid { get; set; }
        public string statecode { get; set; }
        public string districtcode { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }

    }

    public class BskyholderModel
    {
        public string urn { get; set; }
        public string memberid { get; set; }
    }
    public class ChartBotBskyholderHospitalModel
    {
        public string urn { get; set; }
        public string hospitalname { get; set; }
        public string description { get; set; }
    }
    public class ChartBotFamilyHeadInformation
    {
        public string urn { get; set; }
        public string aadharno { get; set; }
    }
    public class MOMRStatus
    {
        public int status { get; set; }
        public string parent_id { get; set; }
    }

    public class KnowYourStatusModel //Rajkishor(14-Apr-23)
    {
        public int memberid { get; set; }
        public string MEMBERNAME { get; set; }
        public string UIDNUMBER { get; set; }
        public string PATIENTCONTACTNUMBER { get; set; }
        public string PATIENTGENDER { get; set; }
        public int AGE { get; set; }
        public string ADMISSIONSTATUS { get; set; }
        public string URN { get; set; }
        public string ADMISSIONDATE { get; set; }
        public string PACKAGENAME { get; set; }
        public string PROCEDURECODE { get; set; }
        public string HOSPITALNAME { get; set; }
       // public string ADMISSIONDATE { get; set; }

    }
}
