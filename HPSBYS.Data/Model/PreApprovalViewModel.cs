using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{
    public class PreApprovalViewModel
    {
        public string urnno { get; set; }
        public string urn { get; set; }
        public string patientname { get; set; }
        public string invoiceno { get; set; }
        public string regdno { get; set; }
        public string contactno { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string memberid { get; set; }
        public int approvedby { get; set; }
        public int statusid { get; set; }
        public string registrationdate { get; set; }
        public string packagedetailid { get; set; }
        public decimal amount { get; set; }
        public string packageheadername { get; set; }
        public string procedurename { get; set; }
        public string hospitalcode { get; set; }
        public string snaremarks { get; set; } //Deepak Kumar Sahu(08-02-2023)
        public string snaremarkssecond { get; set; } //Deepak Kumar Sahu(08-02-2023)
        public int querycount { get; set; } //Deepak Kumar Sahu(09-02-2023)
        public string uploadyear { get; set; }//Rajkishor(14-mar-23)
        public string preauthdoc1 { get; set; }//Rajkishor(14-mar-23)
        public string preauthdoc2 { get; set; }//Rajkishor(14-mar-23)
        public string preauthdoc3 { get; set; }//Rajkishor(14-mar-23)
        public string filepath { get; set; }//Rajkishor(14-mar-23)
        public string caseno { get; set; }//Rajkishor(16-mar-23)
        public string procedurecode { get; set; }//Rajkishor(16-mar-23)
        public string statustext { get; set; }//Rajkishor(06-Apr-23)
        public int ACTUALAMOUNTAPPROVED { get; set; }
        public int AMOUNTTOBEAPPROVED { get; set; }
        public string  needmoredocs { get; set; }
        public string  actiontakenby { get; set; }
        public string QueryRemark1 { get; set; }
        public string QueryRemark2 { get; set; }

    }
    public class ViewModelPackageUpdation
    {
        public string ActionCode { get; set; }
        public string p_transactionid { get; set; }
        public string p_txnpackagedetailid { get; set; }
        public string p_memberid { get; set; }
        public string p_preauthstatus { get; set; }
        public string p_remark { get; set; }
        public string p_hospitalcode { get; set; }
        public string p_amounttobeblocked { get; set; }
        public string urn { get; set; }
    }
    public class PreAuthAddDocuments
    {
        public string memberid { get; set; }
        public string urn { get; set; }
        public string document2 { get; set; }
        public string document3 { get; set; }
        public string replySecond { get; set; }
        public string replyThird { get; set; }
        public int packagedetailsid { get; set; }
        public string uploadyear { get; set; }//Rajkishor(14-mar-23)
    }

    public class PreauthViewdetails
    {
        public int TXNPACKAGEDETAILID { get; set; }
        public int packagedetailsid { get; set; }
        public string CASENO { get; set; }
        public string URN { get; set; }
        public string PATIENTNAME { get; set; }
        public string memberid { get; set; }
        public string PATIENTGENDER { get; set; }
        public int AGE { get; set; }
        public string UIDNUMBER { get; set; }
        public string PATIENTCONTACTNUMBER { get; set; }
        public string TREATMENTTYPE { get; set; }
        public string ISPATIENTOTPVERIFIED { get; set; }
        public string VERIFIEDMEMBERID { get; set; }
        public string VERIFIEDMEMBERNAME { get; set; }
        public string VERIFICATIONMODE { get; set; }
        public string PACKAGEHEADER { get; set; }
        public string PROCEDURECODE { get; set; }
        public string PROCEDURENAME { get; set; }
        public int AMOUNT { get; set; }
        public int APPROVEDAMOUNT { get; set; }
        public int NOOFDAYS { get; set; }
        public string REQUESTDATE { get; set; }
        public string WARDNAME { get; set; }
        public string ADDTIONAL_DOC1 { get; set; }
        public string ADDTIONAL_DOC2 { get; set; }
        public string ADDTIONAL_DOC3 { get; set; }

        public string DOCTORNAME { get; set; }
        public string DOCTORPHNO { get; set; }
        public string OVERRIDECODE { get; set; }
        public string REFERALCODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string remarks { get; set; }
        public int PACKAGECOST { get; set; }
        public string sndescription { get; set; }
    }
    public class PreauthImplantData
    {
        public int TXNPACKAGEDETAILID { get; set; }
        public string IMPLANTNAME { get; set; }
        public int UNIT { get; set; }
        public int UNITPERPRICE { get; set; }
        public int AMOUNT { get; set; }
        public int TOTALAMOUNT { get; set; }
    }
    public class PreauthHighenDrug
    {
        public int TXNPACKAGEDETAILID { get; set; }
        public string HEDNAME { get; set; }
        public int HEDPRICEPERUNIT { get; set; }
        public int HEDUNIT { get; set; }
        public int HEDPRICE { get; set; }
        public int TOTALHEDPRICE { get; set; }
    }

    public class PreauthViewDetailsModel
    {
        public List<PreauthViewdetails> Packagedetails { get; set; }
        public List<PreauthHighenDrug> highenddrugsdetails { get; set; }
        public List<PreauthImplantData> implantdetails { get; set; }
        
    }
}
