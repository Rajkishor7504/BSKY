using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{
    public class TreatmentInformationModel
    {
        public hospitalInfoModel hospitalInfo { get; set; }
        public patientInfoModel patientInfo { get; set; }
        public List<treatmentInfoModel> treatmentInfo { get; set; }
        public List<highEndDrugsModel> highEndDrugs { get; set; }
        public List<implantDataModel> implantData { get; set; }
        public List<costDetailsModel> costDetails { get; set; }
        public latestDocumentModel latestDocument { get; set; }
        public List<dischargedTreatmentInfoModel> dischargedTreatmentInfo { get; set; }
        public List<OldClaimInfoModel> oldClaimInfo { get; set; }
        public List<ActionHistoryModel> actionHistory { get; set; }
        public List<OnGoingTreatmentDetailModel> onGoingTreatmentDetails { get; set; }
        public List<AllPreAuthInfoModel> allPreAuthInfo { get; set; }
    }
    public class hospitalInfoModel
    {
        public string hospitalname { get; set; }
        public string hospitalcode { get; set; }
        public string hospitalcategory { get; set; }
        public string caseno { get; set; }
    }
    public class patientInfoModel
    {
        public string urn { get; set; }
        public string patientname { get; set; }
        public string patientmobileno { get; set; }
        public string verifiedmembername { get; set; }
        public string verifiedthrough { get; set; }
        public string mobilenoverfiedthroughotp { get; set; }
    }
    public class treatmentInfoModel
    {
        public string requesteddate { get; set; }
        public string procedurecode { get; set; }
        public string procedurename { get; set; }
        public string packagecode { get; set; }
        public string packagename { get; set; }
        public string packagecost { get; set; }
        public string wardname { get; set; }
        public string hospitalizationdays { get; set; }
    }
    public class costDetailsModel
    {
        public string totalpackagecost { get; set; }
        public string familyfund { get; set; }
        public string femalefund { get; set; }
        public string insufficientfund { get; set; }
        
    }
    public class latestDocumentModel
    {
        public string uploadDate { get; set; }
        public string description { get; set; }
        public string latestDoc { get; set; }
        public string latestDoclink { get; set; }
        public string year { get; set; }
        public string hospitalcode { get; set; }
        public string snaRemarkDate { get; set; }
        public string snaRemark { get; set; }
        public string snaDescription { get; set; }
    }
    public class SNARemarksModel
    {
        public string remarkDate { get; set; }
        public string remark { get; set; }
        public string description { get; set; }
    }
    public class dischargedTreatmentInfoModel
    {
        public string claimno { get; set; }
        public string urn { get; set; }
        public string packagecode { get; set; }
        public string patientname { get; set; }
        public string hospitalname { get; set; }
        public string admissiondate { get; set; }
        public string actualadmissiondate { get; set; }
        public string dischargedate { get; set; }
        public string actualdischargedate { get; set; }
        public string hospitalclaimamount { get; set; }
        public string cpdapprovedamount { get; set; }
        public string snaapprovedamount { get; set; }
        public string status { get; set; }
        public string cpdname { get; set; }
        public string claimid { get; set; }
        public string claimraisestatus { get; set; }
        public string claimstatus { get; set; }
        public string transactiondetailsid { get; set; }
    }
    public class OldClaimInfoModel
    {
        public string urn { get; set; }
        public string patientname { get; set; }
        public string hospitalname { get; set; }
        public string admissiondate { get; set; }
        public string actualadmissiondate { get; set; }
        public string dischargedate { get; set; }
        public string actualdischargedate { get; set; }
        public string claimstatus { get; set; }
        public string approvedamount { get; set; }
        public string approveddate { get; set; }
        public string snaapprovedamount { get; set; }
        public string snaapproveddate { get; set; }
        public string remark { get; set; }
        public string snaremark { get; set; }
    }
    public class ActionHistoryModel
    {
        public string actionby { get; set; }
        public string actionon { get; set; }
        public string actiontype { get; set; }
        public string actionamount { get; set; }
        public string remark { get; set; }
        public string description { get; set; }
        public string docname { get; set; }
        public string doclink { get; set; }
        public string year { get; set; }
        public string hospitalcode { get; set; }
    }
    public class OnGoingTreatmentDetailModel
    {
        public string caseno { get; set; }
        public string patientname { get; set; }
        public string procedurecode { get; set; }
        public string procedurename { get; set; }
        public string packagecode { get; set; }
        public string admissiondate { get; set; }
        public string authmode { get; set; }
        public string currentstatus { get; set; }
        public string actiondate { get; set; }
        public string blockedamount { get; set; }
    }
    public class AllPreAuthInfoModel
    {
        public string patientname { get; set; }
        public string procedurecode { get; set; }
        public string packagecode { get; set; }
        public string packagename { get; set; }
        public string requestamount { get; set; }
        public string approvedamount { get; set; }
        public string blockamount { get; set; }
        public string preauth { get; set; }
        public string status { get; set; }
        public string snaremark { get; set; }
        public string prauthrequestdate { get; set; }
        public string preauthid { get; set; }
        public string packagedetailid { get; set; }
    }
}

