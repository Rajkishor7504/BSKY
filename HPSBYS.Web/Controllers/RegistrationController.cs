using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using NLog;
using HPSBYS.Data.Model;
using HPSBYS.Web.Models;
using HPSBYS.Data.Services;
using System.Xml;
using ECSAsaApiDemo;
using ECSAsaApiEx.com.ecs.asa.utils;
using ECSAsaApiEx;
using In.gov.uidai.authentication.otp._1;
using System.Xml.Linq;
using HPSBYS.Web.Fiilters;

namespace HPSBYS.Web.Controllers
{
    [NoDirectAccess]
    [SessionTimeOutFilter]
    [Authorize]
    public class RegistrationController : Controller
    {
        ILogger log = LogManager.GetCurrentClassLogger();
        string ServiceURL = ConfigurationManager.AppSettings["ServiceURL"];
        HttpClient client;
        string result = string.Empty;
        string AadhaarMsg = string.Empty;
        string AuthenticateOtpURL = ConfigurationManager.AppSettings["AuthenticateOtpURL"];
        string AuthenticateURL = ConfigurationManager.AppSettings["AuthenticateURL"];
        string CertificatePathURL = ConfigurationManager.AppSettings["CertificatePathURL"];
        // GET: Registration
        [HttpGet]
        public ViewResult AdmissionList()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddRegisterDocument(FormCollection PaitentInfo)
        {
            string fname = string.Empty;
            string fileExtention = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    List<object> obj = new List<object>();
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        fname = file.FileName;
                        fileExtention = System.IO.Path.GetExtension(file.FileName);
                        int Year = DateTime.Now.Year;
                        string Month = DateTime.Now.ToString("MMMM");
                        string HopitalCode = Session["HospitalCode"].ToString();
                        string dirUrl = "~/UploadDocument/" + HopitalCode + "/" + Year + "/" + Month + "/RegisterDocument";
                        string dirPath = Server.MapPath(dirUrl);
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }
                        var newGuid = Guid.NewGuid();
                        string subGuid = newGuid.ToString().Substring(0, 15);
                        //string ActFileName = subGuid + "_" + fname;
                        string ActFileName = "PF_" + PaitentInfo["URN"] + "_" + GetTimestamp(DateTime.Now) + fileExtention;
                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath(dirUrl), ActFileName);
                        file.SaveAs(fname);
                        obj.Add(new { Message = "Sucess", File = ActFileName });
                    }
                    // Returns message that successfully uploaded  
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    return Json("Error");

                }

            }
            else
            {
                return Json("NoFiles");
            }
        }
        [HttpGet]
        public ViewResult Admission()
        {
            return View();
        }
        [NonAction]
        private static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult addPatientRegistration(HttpPostedFileBase file, FormCollection PaitentInfo)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileExtention = string.Empty;
                    int Year = DateTime.Now.Year;
                    string Month = DateTime.Now.ToString("MMMM");
                    string HopitalCode = Session["HospitalCode"].ToString();
                    string dirUrl = "~/UploadDocument/" + HopitalCode + "/" + Year + "/" + Month + "/AdmissionSlip";
                    string dirPath = Server.MapPath(dirUrl);
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    var fileName = Path.GetFileName(file.FileName);
                    fileExtention = Path.GetExtension(file.FileName);
                    var newGuid = Guid.NewGuid();
                    string SubGuid = newGuid.ToString().Substring(0, 15);
                    string ActFileName="REG" + PaitentInfo["URN"] + "_" + GetTimestamp(DateTime.Now) + fileExtention;
                    //string ActFileName = SubGuid + "_" + fileName;
                    var path = Path.Combine(Server.MapPath(dirUrl), ActFileName);
                    file.SaveAs(path);
                    PatientInfo patientInfo = new PatientInfo
                    {
                        ACTIONCODE = "R",
                        MemberDistrictName = PaitentInfo["district"],
                        DistrictCode = PaitentInfo["districtCode"],
                        MemberStatecode = PaitentInfo["stateCode"],
                        BlockCode = PaitentInfo["memberBlockCode"],
                        PanchayatCode = PaitentInfo["memberPanchayatCode"],
                        VillageCode = PaitentInfo["memberVillageCode"],
                        HeadMemberName = PaitentInfo["headOfFam"],
                        HeadMemberID = PaitentInfo["memberBlockCode"],
                        URN = PaitentInfo["urnNo"],
                        FamilyID = PaitentInfo["familyID"],
                        SchemeCode = PaitentInfo["SchemeCode"],
                        InsurancePolicyNumber = PaitentInfo["PolicyNum"],
                        PolicyStartDate = PaitentInfo["startDate"],
                        PolicyEndDate = PaitentInfo["endDate"],
                        MemberID = Convert.ToString(PaitentInfo["memberID"]),
                        PatientName = PaitentInfo["PName"],
                        PatientCardGender = PaitentInfo["CardGender"],
                        Gender = PaitentInfo["Gender"],
                        PatientCardAge = Convert.ToString(PaitentInfo["CardAge"]),
                        Age = Convert.ToString(PaitentInfo["txtAge"]),
                        PatientContactNumber = PaitentInfo["txtContactNo"],
                        HospitalCode = PaitentInfo["HdCode"],
                        HospitalName = PaitentInfo["HdName"],
                        HospitalState = PaitentInfo["HdState"],
                        HospitalDistrict = PaitentInfo["HdDistrict"],
                        TransactionCode = "300",
                        patientSlip = ActFileName,
                        VerifiedMemberID = PaitentInfo["VerMembID"],
                        VerifiedMemberName = PaitentInfo["VerifiedMemberName"],
                        HospitalAuthorityCode = PaitentInfo["HdCode"],//Not define HospitalAuthorityCode So For Temporay HospitalCode Assign
                        InsuranceCompanyCode = "0",
                        RegistrationNo = "0",
                        RegistrationUserDate = PaitentInfo["txtRDate"],
                        AuthenticationMode = PaitentInfo["AuthMode"],
                        VerifiedDocumentType = PaitentInfo["DocType"],
                        VerifiedDocumentName = PaitentInfo["Doc"],
                        PatientPhoto = PaitentInfo["Photo"]
                    };
                    using (PatientDataServices dataServices = new PatientDataServices())
                    {
                        result = dataServices.ManagePatientRegistration(patientInfo);
                        //return await Task.FromResult(PatientDataServices.ManagePatientRegistration(obj));
                    }
                    //client = new HttpClient();
                    //client.BaseAddress = new Uri(ServiceURL);
                    //client.DefaultRequestHeaders.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //var sucSender = client.PostAsJsonAsync("/api/Registration/PatientRegistration", patientInfo).Result;
                    //if (sucSender.IsSuccessStatusCode == true)
                    //{
                    //    result = sucSender.Content.ReadAsAsync<string>().Result;
                    //}
                }
                if (result == "1")
                {
                    return Json("sucess");
                }
                else if (result == "3")
                {
                    return Json("duplicate");
                }
                else
                {
                    return Json("failed");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json("failed");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult generateOTP(FormCollection AadhaarInfo)
        {
            string msg = string.Empty;
            if (AadhaarInfo["AadhaarNo"] != null || AadhaarInfo["AadhaarNo"] != "")
            {
                Session["Aadhaar"] = AadhaarInfo["AadhaarNo"];
                msg=GenerateOTPRequsetXml(AadhaarInfo["AadhaarNo"].ToString());
            }

            return Json(msg,JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        private string GenerateOTPRequsetXml(string AadharNo)
        {         
            string TS = System.DateTime.Now.Year.ToString("0000") + "-" + System.DateTime.Now.Month.ToString("00") + "-" + System.DateTime.Now.Day.ToString("00") + "T" + System.DateTime.Now.Hour.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00") + ":" + System.DateTime.Now.Second.ToString("00.000") + "+05:30";
            string TXN = "ORI:OCAC:ILFS:" + System.DateTime.Now.Year.ToString("0000") + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00") + System.DateTime.Now.Millisecond.ToString("000");
            Session["TXN"] = TXN;
            Session["TS"] = TS;
            XmlDocument XDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = XDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            XDoc.AppendChild(xmlDeclaration);
            XmlElement XElemRoot = XDoc.CreateElement("Otp");
            XElemRoot.SetAttribute("xmlns", "http://www.uidai.gov.in/authentication/otp/1.0");
            XElemRoot.SetAttribute("uid", AadharNo.Trim());
            //XElemRoot.SetAttribute("tid", "public");
            XElemRoot.SetAttribute("ac", "0000540000");
            XElemRoot.SetAttribute("sa", "0000540000");
            XElemRoot.SetAttribute("ver", "2.5");
            XElemRoot.SetAttribute("txn", TXN);
            XElemRoot.SetAttribute("ts", TS);
            XElemRoot.SetAttribute("lk", "MJ5m2MhDMB1pujFwcsL_bWm0JOiv6OLouMUXQFWN6Y1TrWKeHSmfPVQ");
            XElemRoot.SetAttribute("type", "A");           
            XDoc.AppendChild(XElemRoot);
            XmlElement Xsource = XDoc.CreateElement("Opts");
            Xsource.SetAttribute("ch", "00");
            XElemRoot.AppendChild(Xsource);           
            Common settings = new Common();
            settings.AsaUrl = AuthenticateOtpURL;           
            string responseXml = HttpConnector.Instance.PostData(settings.AsaUrl, XDoc.OuterXml.ToString());
            try
            {
                if (!string.IsNullOrEmpty(responseXml))
                {
                    XmlDocument xd = new XmlDocument();
                    xd.LoadXml(responseXml);
                    XmlNode node = xd.GetElementsByTagName("OtpRes").Item(0);
                    string res = node.Attributes["ret"].Value;
                    if (res == "y")
                    {
                        AadhaarMsg = "Sucess";
                    }
                    else
                    {
                        AadhaarMsg = "Failed"; 
                    }
                }                
            }
            catch (Exception ex)
            {
                log.Error(ex);               
                AadhaarMsg = "Failed";
            }
            return AadhaarMsg;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OTPAuthRequest(FormCollection OtpInfo)
        {
            string AuthMsg = string.Empty;
            if (OtpInfo["Otp"] != null && OtpInfo["Otp"] != "" && OtpInfo["Otp"].Length == 6)
            {
               AuthMsg= CreateOTPAuthRequest(OtpInfo["Otp"].ToString());
            }
            return Json(AuthMsg,JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        private string CreateOTPAuthRequest(string Otp)
        {
            try
            {
                Common settings = new Common();
                settings.AuaCode = "0000540000";
                settings.SubAuaCode = "0000540000";
                settings.AuaLicenseKey = "MJ5m2MhDMB1pujFwcsL_bWm0JOiv6OLouMUXQFWN6Y1TrWKeHSmfPVQ";
                settings.KuaLicenseKey = "MK1XWLvDJZRktu9qJ6NM-5TzZdP2jnyTzTh4mzxQbW7su4XeShNmr3w";
                settings.TerminalId = "public";
                settings.UDC = "UIDAIADGYASH";
                settings.UidaiSigningCertificate = @""+CertificatePathURL+"uidai_auth_sign_prod.cer";
                settings.UidaiEncryptionCertificate = @"" + CertificatePathURL + "uidai_auth_prod.cer";
                settings.MouVerifyCertificate = @"" + CertificatePathURL + "uidai_mou_verify.cer";
                settings.LocationType = "P"; // Auth 1.6 Only
                settings.LocationValue = "753001"; // Auth 1.6 Only
                settings.AuaSigningCertificate = @"" + CertificatePathURL + "Staging_Signature_PrivateKey.p12";
                settings.AuaSigningPassword = "public";
                settings.OTP = Otp.Trim();
                settings.TransactionID = Session["TXN"].ToString();
                settings.AsaUrl = AuthenticateURL;             
                ECSAsaApiEx.AuthProcessor obj = new AuthProcessor();
                OtpAuthenticationTest otpAuth = new OtpAuthenticationTest();
                bool Response = otpAuth.PerformOtpAuthentication(settings, Session["Aadhaar"].ToString().Trim());
                if (Response == true)
                {
                    AadhaarMsg = "Sucess";
                }
                else
                {
                    AadhaarMsg = "Failed"; 
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                AadhaarMsg = "Failed";
            }
            return AadhaarMsg;
        }
    }
}