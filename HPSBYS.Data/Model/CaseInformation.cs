using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{
    public class CaseInformationModel
    {
        public int userid { get; set; }
        public string hospitalcode { get; set; }
        public string action { get; set; }
        public int preauthid { get; set; }
        public int packagedetailid { get; set; }
        public string caseno { get; set; }
    }
    public class CaseInformationDetails
    {
        public packageInformationModel packageInformation { get; set; }
        public List<BlockedInformationModel> Blocked { get; set; }
        public List<UnBlockedInformationModel> UnBlocked { get; set; }
        public List<PreAuthDetailsModel> PreAuthDetails { get; set; }
        public List<highEndDrugsModel> highEndDrugs { get; set; }
        public List<implantDataModel> implantData { get; set; }
        public List<wardInfoModel> wardInfo { get; set; }
        public List<vitalInformationModel> vitalInformation { get; set; }
    }
    public class packageInformationModel
    {
        public string patientname { get; set; }
        public string patientphoto { get; set; }
        public string admissiontype { get; set; }
        public string procedurecode { get; set; }
        public string procedurename { get; set; }
        public string doctorname { get; set; }
        public string packagecode { get; set; }
        public string packagename { get; set; }
        public string preauthstatus { get; set; }
        public string year { get; set; }
        public string hospitalcode { get; set; }
    }
    public class BlockedInformationModel
    {
        public string packagecost { get; set; }
        public string amountblocked { get; set; }
        public string blockdate { get; set; }
        public string blockverificationmode { get; set; }
        public string verifiermembername { get; set; }
        public string noofdays { get; set; }
        public string overridecode { get; set; }
        public string referralcode { get; set; }
        public string expiredate { get; set; }
    }
    public class UnBlockedInformationModel
    {
        public string unblockdate { get; set; }
        public string unblockinginvoicenumber { get; set; }
        public string unblockingdescription { get; set; }
        public string unblockverificationmode { get; set; }
        public string unblockingoverridecode { get; set; }
        public string unblockingverifiermembername { get; set; }
    }
    public class PreAuthDetailsModel
    {
        public string requestdate { get; set; }
        public string hospitalrequestamount { get; set; }
        public string insufficientamount { get; set; }
        public string requestdescription { get; set; }
        public string snaapprovedamount { get; set; }
        public string approveddate { get; set; }
        public string snadescription { get; set; }
        public string firstquerydate { get; set; }
        public string snadescriptionforfirstquery { get; set; }
        public string firstqueryreplydate { get; set; }
        public string firstqueryreplybyhospital { get; set; }
        public string secondquerydate { get; set; }
        public string snadescriptionforsecondquery { get; set; }
        public string secondqueryreplydate { get; set; }
        public string secondqueryreplybyhospital { get; set; }
        public string docName1 { get; set; }
        public string docLink1 { get; set; }
        public string docName2 { get; set; }
        public string docLink2 { get; set; }
        public string docName3 { get; set; }
        public string docLink3 { get; set; }
        public string hospitalcode { get; set; }
        public string year { get; set; }
    }
    public class implantDataModel
    {
        public string implantname { get; set; }
        public string unit { get; set; }
        public string price { get; set; }
        public string totalPrice { get; set; }
    }
    public class highEndDrugsModel
    {
        public string hedname { get; set; }
        public string unit { get; set; }
        public string price { get; set; }
        public string totalPrice { get; set; }
    }
    public class wardInfoModel
    {
        public string wardname { get; set; }
        public string wardamount { get; set; }
        public string wardblockdate { get; set; }
        public string status { get; set; }
    }
    public class vitalInformationModel
    {
        public string vitalsign { get; set; }
        public string vitalvalue { get; set; }
    }

}