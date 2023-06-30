using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{
    public class NewOverRideDetails
    {
        public string Action { get; set; }
        public string HospitalCode { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int OTP { get; set; }
        public int IRIS { get; set; }
        public int POS { get; set; }
        public int OVERRIDE { get; set; }
        public int VERIFICATIONMODE { get; set; }//aadhar verification
        public int Transactionid { get; set; }
        public string GENERATEDTHROUGH { get; set; }//purpose
        public int Deletedflag { get; set; }
        public string Dreatedon { get; set; }
        public int MEMBERID { get; set; }
        public int UNBLOCKVERIFICATIONMODE { get; set; }
        public string TRANSACTIONCODE { get; set; }
        public string UNBLOCKINGSYSTEMDATE { get; set; }
        public int DISVERIFICATIONMODE { get; set; }
        public int DISCHARGEFLAG { get; set; }
        public string DATEOFDISCHARGE { get; set; }
        public string Purpose { get; set; }
    }

    public class NewOverRideView
    {
        public string URN { get; set; } //URN
        public string FULLNAMEENGLISH { get; set; }
        public int MEMBERID { get; set; }
        //public string GENERATEDTHROUGH { get; set; }
        public string createdon { get; set; }
        public string VerificationMode { get; set; }// pos,otp,iris verification
        public string VARIFIEDTHROUGH { get; set; }
        public string VERIFYSTATUS { get; set; }
        public string VERIFIEDTHROUGH { get; set; }
        public string VERIFIEDSTATUS { get; set; }
        public string VERIFYTHROUGH { get; set; }
        public string STATUS { get; set; }
        public string Purpose { get; set; }
        public string APPROVESTATUS { get; set; }
        public string Action { get; set; }
        public string to_date { get; set; }
        public string from_date { get; set; }
        public string HospitalCode { get; set; }
        public string hospitalname { get; set; }
        public string stateCode { get; set; }
        public string statename { get; set; }
        public string districtCode { get; set; }
        public string districtname { get; set; }
    }
}