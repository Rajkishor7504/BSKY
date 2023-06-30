using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ECSAsaApiEx;


namespace ECSAsaApiDemo
{
    public class Common
    {
        //private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string AuaCode { get; set; }
        public string SubAuaCode { get; set; }
        public string AuaLicenseKey{ get; set; }
        public string KuaLicenseKey { get; set; } // Required only for KYC Transactions
        public string UidaiSigningCertificate{ get; set; }
        public string UidaiEncryptionCertificate { get; set; }
        public string AuaSigningCertificate { get; set; }
        public string AuaSigningPassword { get; set; }
        public string MouVerifyCertificate { get; set; }
        public string KuaDecryptionCertificate { get; set; }
        public string KuaDecryptionPassword { get; set; }
        public string TerminalId { get; set; }
        public string UDC { get; set; }
        public string LocationType{ get; set; }
        public string LocationValue { get; set; }
        public string AsaUrl { get; set; }
        public string OTP { get; set; }
        public string Name { get; set; }
        public string TransactionID { get; set; }

        public static string CaptureFMR(bool ekyc)
        {
            return CaptureFMR(ekyc, null);
        }

        public static string CaptureFMR(bool ekyc, string otp)
        {

            Console.WriteLine("FIngerprint capture code not implemented");
            /*
             * Write code here to interface with RD Service for capturing biometrics and return PIDData XML
             */

            return ""; // return actual PidData XML from RD service on capturing biometrics        
        }


    }
}
