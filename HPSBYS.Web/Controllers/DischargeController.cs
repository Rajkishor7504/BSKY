
using HPSBYS.Data.Model;
using HPSBYS.Data.Services;
using HPSBYS.Web.Fiilters;
using HPSBYS.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HPSBYS.Web.Controllers
{
    [SessionTimeOutFilter]
    [Authorize]
    [NoDirectAccess]
    public class DischargeController : Controller
    {
        ILogger log = LogManager.GetCurrentClassLogger();
        string result = string.Empty;
        // GET: Discharge
        [HttpGet]
        public ActionResult AddDischarge()
        {
            string groupid = Convert.ToString(Session["groupid"]);
            if (groupid != "1")
            {
                return View();

            }
            else
            {
                return RedirectToAction("adminViewReferalDischarge", "Discharge");
            }
        }

        [HttpGet]
        public ViewResult adminViewReferalDischarge() //purpose for admin view
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddDischarge(FormCollection PaitentInfo)
        {
            string fname = string.Empty;
            string fileExtention = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    string TreatComCertificate = string.Empty;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        fname = file.FileName;
                        fileExtention = System.IO.Path.GetExtension(file.FileName);
                        int Year = DateTime.Now.Year;
                        string Month = DateTime.Now.ToString("MMMM");
                        string HopitalCode = Session["HospitalCode"].ToString();
                        string dirUrl = "~/UploadDocument/" + HopitalCode + "/" + Year + "/" + Month + "/DischargeDocument";
                        string dirPath = Server.MapPath(dirUrl);
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }
                        var newGuid = Guid.NewGuid();
                        string subGuid = newGuid.ToString().Substring(0, 15);
                        string ActFileName = "DIS_" + PaitentInfo["URN"] + "_" + GetTimestamp(DateTime.Now)+fileExtention;
                       // string ActFileName = subGuid + "_" + fname;
                        TreatComCertificate = ActFileName;
                        fname = Path.Combine(Server.MapPath(dirUrl), ActFileName);
                        file.SaveAs(fname);
                    }
                    PatientInfo patientInfo = new PatientInfo
                    {
                        ACTIONCODE = PaitentInfo["ACTIONCODE"],
                        BlockingInvoiceNo = PaitentInfo["BlockingInvoiceNo"],
                        DischargeDesc = PaitentInfo["DischargeDesc"],
                        DischargeUserDate = PaitentInfo["DischargeUserDate"],
                        DATEOFDISCHARGE = PaitentInfo["DATEOFDISCHARGE"],
                        Mortality = PaitentInfo["Mortality"],
                        MortalitySummary = PaitentInfo["MortalitySummary"],
                        ProcedureCode = PaitentInfo["ProcedureCode"],
                        ProcedureName = PaitentInfo["ProcedureName"],
                        PackageCode = PaitentInfo["PackageCode"],
                        PackageName = PaitentInfo["PackageName"],
                        PackageCost = PaitentInfo["PackageCost"],
                        NoofDays = PaitentInfo["NoofDays"],
                        AmoutBlocked = PaitentInfo["AmoutBlocked"],
                        NoofDaysActual = PaitentInfo["NoofDaysActual"],
                        PackageMode = PaitentInfo["PackageMode"],
                        IsMedSergical = PaitentInfo["IsMedSergical"],
                        URN = PaitentInfo["URN"],
                        TreatmentCompletionCer = TreatComCertificate,
                        Category = PaitentInfo["category"],
                        CategoryCode = PaitentInfo["categoryCode"]
                    };
                    using (PatientDataServices dataServices = new PatientDataServices())
                    {
                        result = dataServices.addPatientDischarge(patientInfo);
                    }
                    if (result == "1")
                    {
                        return Json("sucess");
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
            else
            {
                return Json("failed");
            }
        }
        [HttpGet]
        public ViewResult ViewDischarge()
        {
            return View();
        }
        public ViewResult TreatmentCompletionCertificate()
        {
            return View();
        }
        [HttpGet]
        public ViewResult AddDownWard()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddDownWardReferal(FormCollection PaitentInfo)
        {
            try
            {
                DownwardReferalInfo patientInfo = new DownwardReferalInfo
                {
                    Action="A",
                    URN = PaitentInfo["URN"],
                    BlockingInvoiceNo = PaitentInfo["BlockingInvoiceNo"],
                    IsReferalRequired = PaitentInfo["IsReferalRequired"],
                    BlockCode = PaitentInfo["BlockCode"],
                    DistrictCode = PaitentInfo["DistrictCode"],
                    PHCCode = PaitentInfo["PHCCode"],
                    SubCenterCode = PaitentInfo["SubCenterCode"]
                };
                using (PatientDataServices dataServices = new PatientDataServices())
                {
                    result = dataServices.addDownwardReferal(patientInfo);
                }
                if (result == "1")
                {
                    return Json("success");
                }
                else if (result == "2")
                {
                    return Json("exist");
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
        public JsonResult UpdateDownWardReferal(FormCollection PaitentInfo)
        {
            try
            {
                DownwardReferalInfo patientInfo = new DownwardReferalInfo
                {
                    Action = "B",
                    URN = PaitentInfo["URN"],
                    BlockingInvoiceNo = PaitentInfo["BlockingInvoiceNo"],
                    IsReferalRequired = PaitentInfo["IsReferalRequired"],
                    BlockCode = PaitentInfo["BlockCode"],
                    DistrictCode = PaitentInfo["DistrictCode"],
                    PHCCode = PaitentInfo["PHCCode"],
                    SubCenterCode = PaitentInfo["SubCenterCode"]
                };
                using (PatientDataServices dataServices = new PatientDataServices())
                {
                    result = dataServices.addDownwardReferal(patientInfo);
                }
                if (result == "1")
                {
                    return Json("update");
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
        [NonAction]
        private static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        [HttpGet]
        public ViewResult ViewReferalDischarge()
        {
            return View();
        }

        public ViewResult PatientReferalForm()
        {
            return View();
        }

        public ViewResult ViewReferalSlip()
        {
            return View();
        }

        public ViewResult ViewDischargeSlip()
        {
            return View();
        }
        public ViewResult PatientReferralView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PatientReferralRequest(PatientReferral model) // ADDED by Akshat (06-Feb-23)
        {
            List<ReturnReferalData> result = new List<ReturnReferalData>();
            string HopitalCode = Session["HospitalCode"].ToString();
            try
            {
                #region FTP 
                // FTP Server URL
                //string ftp = "ftp://192.168.10.76/";
                //byte[] fileBytes = null;
                //string ftpUserName = "bskytmsu1";
                //string ftpPassword = "csmpl@1234";
                //LIVE
                string ftp = ConfigurationManager.AppSettings["FTPURL"];
                byte[] fileBytes = null;
                string ftpUserName = ConfigurationManager.AppSettings["FTPUSERID"];
                string ftpPassword = ConfigurationManager.AppSettings["FTPPASSWORD"];
                //END LIVE
                #endregion

                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        if (Request.Files.GetKey(i) == "InvestigationDoc")
                        {
                            HttpPostedFileBase filedata = files[i];
                            var patientPhoto = files[i].FileName;
                            var fileExtention = System.IO.Path.GetExtension(patientPhoto);
                            var filename = "INV_" + model.URN.Substring(model.URN.Length - 6) + "_" + model.MemberId + Convert.ToDateTime(model.ReferralDate).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                            var filenamepath = Convert.ToDateTime(model.ReferralDate).Year + "/" + HopitalCode + "/" + "InvestigationDoc/";
                            var dirUrl = ftp + filenamepath;
                            //string dirPath = Server.MapPath(dirUrl);

                            using (BinaryReader br = new BinaryReader(filedata.InputStream))
                            {
                                fileBytes = br.ReadBytes(filedata.ContentLength);
                            }

                            if (!DoesFtpDirectoryExist(dirUrl))
                            {
                                CreateFTPDirectory(filenamepath);
                            }

                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl + filename);
                            request.Method = WebRequestMethods.Ftp.UploadFile;

                            //enter FTP Server credentials
                            request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                            request.ContentLength = fileBytes.Length;
                            request.UsePassive = true;
                            request.UseBinary = true;   // or FALSE for ASCII files
                            request.ServicePoint.ConnectionLimit = fileBytes.Length;
                            request.EnableSsl = false;

                            using (Stream requestStream = request.GetRequestStream())
                            {
                                requestStream.Write(fileBytes, 0, fileBytes.Length);
                                requestStream.Close();
                            }
                            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                            response.Close();
                            model.InvestigationDoc = filename;
                            //HttpPostedFileBase filedata = files[i];
                            //var patientPhoto = files[i].FileName;
                            //var fileExtention = System.IO.Path.GetExtension(patientPhoto);
                            //var filename = "INV_" + model.URN.Substring(model.URN.Length - 6) + "_" + model.MemberId + Convert.ToDateTime(model.ReferralDate).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                            //var filenamepath = Convert.ToDateTime(model.ReferralDate).Year + "/" + HopitalCode + "/" + "InvestigationDoc";
                            //var dirUrl = "~/BSKY/" + filenamepath;
                            //string dirPath = Server.MapPath(dirUrl);
                            //if (!Directory.Exists(dirPath))
                            //{
                            //    Directory.CreateDirectory(dirPath);
                            //}
                            //var fname = Path.Combine(Server.MapPath(dirUrl), filename);
                            //model.InvestigationDoc = filename;
                            //filedata.SaveAs(fname);
                        }
                        if (Request.Files.GetKey(i) == "ReferralDoc")
                        {
                            HttpPostedFileBase filedata = files[i];
                            var patientPhoto = files[i].FileName;
                            var fileExtention = System.IO.Path.GetExtension(patientPhoto);
                            var filename = "REF_" + model.URN.Substring(model.URN.Length - 6) + "_" + model.MemberId + Convert.ToDateTime(model.ReferralDate).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                            var filenamepath = Convert.ToDateTime(model.ReferralDate).Year + "/" + HopitalCode + "/" + "ReferralDocument/";
                            var dirUrl = ftp + filenamepath;
                            //string dirPath = Server.MapPath(dirUrl);

                            using (BinaryReader br = new BinaryReader(filedata.InputStream))
                            {
                                fileBytes = br.ReadBytes(filedata.ContentLength);
                            }

                            if (!DoesFtpDirectoryExist(dirUrl))
                            {
                                CreateFTPDirectory(filenamepath);
                            }

                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl + filename);
                            request.Method = WebRequestMethods.Ftp.UploadFile;

                            //enter FTP Server credentials
                            request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                            request.ContentLength = fileBytes.Length;
                            request.UsePassive = true;
                            request.UseBinary = true;   // or FALSE for ASCII files
                            request.ServicePoint.ConnectionLimit = fileBytes.Length;
                            request.EnableSsl = false;

                            using (Stream requestStream = request.GetRequestStream())
                            {
                                requestStream.Write(fileBytes, 0, fileBytes.Length);
                                requestStream.Close();
                            }
                            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                            response.Close();
                            model.ReferralDoc = filename;
                            //HttpPostedFileBase filedata = files[i];
                            //var patientPhoto = files[i].FileName;
                            //var fileExtention = System.IO.Path.GetExtension(patientPhoto);
                            //var filename = "REF_" + model.URN.Substring(model.URN.Length - 6) + "_" + model.MemberId + Convert.ToDateTime(model.ReferralDate).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                            //var filenamepath = Convert.ToDateTime(model.ReferralDate).Year + "/" + HopitalCode + "/" + "ReferralDocument";
                            //var dirUrl = "~/BSKY/" + filenamepath;
                            //string dirPath = Server.MapPath(dirUrl);
                            //if (!Directory.Exists(dirPath))
                            //{
                            //    Directory.CreateDirectory(dirPath);
                            //}
                            //var fname = Path.Combine(Server.MapPath(dirUrl), filename);
                            //model.ReferralDoc = filename;
                            //filedata.SaveAs(fname);
                        }
                    }
                }

                PatientReferral patientReferral = new PatientReferral
                {
                    Action = model.Action,
                    URN = model.URN,
                    MemberId = model.MemberId,
                    HospitalCode = model.HospitalCode,
                    ReferralDate = model.ReferralDate,
                    //ReferralCode = collection["ReferralCode"],
                    PatientName = model.PatientName,
                    Age = model.Age,
                    Gender = model.Gender,
                    RegdNo = model.RegdNo,
                    FromHospitalName = model.FromHospitalName,
                    FromHospitalCode = model.FromHospitalCode,
                    FromDrName = model.FromDrName,
                    FromDeptName = model.FromDeptName,
                    FromReferralDate = model.FromReferralDate,
                    ToState = model.ToState,
                    ToDistrict = model.ToDistrict,
                    ToHospital = model.ToHospital,
                    ToHospitalCode = model.ToHospitalCode,
                    ReasonForRefer = model.ReasonForRefer,
                    ToReferralDate = model.ToReferralDate,
                    Diagnosis = model.Diagnosis,
                    PatientBriefHistory = model.PatientBriefHistory,
                    TreatmentGiven = model.TreatmentGiven,
                    InvestigationRemark = model.InvestigationRemark,
                    TreatmentAdvised = model.TreatmentAdvised,
                    InvestigationDoc = model.InvestigationDoc,
                    ReferralDoc = model.ReferralDoc,
                    UserId = model.UserId,
                    VitalParameterList = model.VitalParameterList
                };
                using (PatientDataServices dataServices = new PatientDataServices())
                {
                    result = dataServices.PatientReferralService(patientReferral);
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return Json(404);
            }
        }


        private bool CreateFTPDirectory(string directory)
        {
            try
            {
                string ftpAddress = ConfigurationManager.AppSettings["FTPURL"];
                FtpWebRequest reqFTP = null;
                Stream ftpStream = null;
                string[] subDirs = directory.Split('/');
                string currentDir = ftpAddress.Remove(ftpAddress.Length - 1, 1);
                foreach (string subDir in subDirs)
                {
                    try
                    {
                        currentDir = currentDir + "/" + subDir;
                        reqFTP = (FtpWebRequest)FtpWebRequest.Create(currentDir);
                        reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                        reqFTP.UseBinary = true;
                        reqFTP.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FTPUSERID"], ConfigurationManager.AppSettings["FTPPASSWORD"]);
                        FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                        ftpStream = response.GetResponseStream();
                        ftpStream.Close();
                        response.Close();
                    }
                    catch (Exception ex)
                    {
                        //directory already exist I know that is weak but there is no way to check if a folder exist on ftp...
                    }
                }
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                    return true;
                }
                else
                {
                    response.Close();
                    return false;
                }
            }
        }

        public bool DoesFtpDirectoryExist(string dirPath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirPath);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FTPUSERID"], ConfigurationManager.AppSettings["FTPPASSWORD"]);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }

        [HttpPost]
        public JsonResult CreatePatientDischage(DischargePatientModel model)
        {
            List<DischargeFilterModel> result = new List<DischargeFilterModel>();
            string HopitalCode = Session["HospitalCode"].ToString();

            #region FTP 
            // FTP Server URL
            //string ftp = "ftp://192.168.10.76/";
            //byte[] fileBytes = null;
            //string ftpUserName = "bskytmsu1";
            //string ftpPassword = "csmpl@1234";
            //LIVE
            string ftp = ConfigurationManager.AppSettings["FTPURL"];
            byte[] fileBytes = null;
            string ftpUserName = ConfigurationManager.AppSettings["FTPUSERID"];
            string ftpPassword = ConfigurationManager.AppSettings["FTPPASSWORD"];
            //END LIVE
            #endregion

            #region :: All File Upload Code
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    if (Request.Files.GetKey(i) == "IntraSurgeryPic")
                    {
                        HttpPostedFileBase filedata = files[i];
                        var patientPhoto = files[i].FileName;
                        var fileExtention = System.IO.Path.GetExtension(patientPhoto);
                        var filename = "INTR_" + model.Urn.Substring(model.Urn.Length - 6) + "_" + model.MemberId + Convert.ToDateTime(model.DischargeDate).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                        var filenamepath = Convert.ToDateTime(model.DischargeDate).Year + "/" + HopitalCode + "/" + "surgery picture/IntraSurgeryPic/";
                        var dirUrl = ftp + filenamepath;

                        using (BinaryReader br = new BinaryReader(filedata.InputStream))
                        {
                            fileBytes = br.ReadBytes(filedata.ContentLength);
                        }

                        if (!DoesFtpDirectoryExist(dirUrl))
                        {
                            CreateFTPDirectory(filenamepath);
                        }

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl + filename);
                        request.Method = WebRequestMethods.Ftp.UploadFile;

                        //enter FTP Server credentials
                        request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                        request.ContentLength = fileBytes.Length;
                        request.UsePassive = true;
                        request.UseBinary = true;   // or FALSE for ASCII files
                        request.ServicePoint.ConnectionLimit = fileBytes.Length;
                        request.EnableSsl = false;

                        using (Stream requestStream = request.GetRequestStream())
                        {
                            requestStream.Write(fileBytes, 0, fileBytes.Length);
                            requestStream.Close();
                        }
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        response.Close();
                        model.IntraSurgeryPic = filename;
                        //string dirPath = Server.MapPath(dirUrl);
                        //if (!Directory.Exists(dirPath))
                        //{
                        //    Directory.CreateDirectory(dirPath);
                        //}
                        //var fname = Path.Combine(Server.MapPath(dirUrl), filename);
                        //model.IntraSurgeryPic = filename;
                        //filedata.SaveAs(fname);
                    }
                    if (Request.Files.GetKey(i) == "PostSurgeryPic")
                    {
                        HttpPostedFileBase filedata = files[i];
                        var patientPhoto = files[i].FileName;
                        var fileExtention = System.IO.Path.GetExtension(patientPhoto);
                        var filename = "POSTSX_" + model.Urn.Substring(model.Urn.Length - 6) + "_" + model.MemberId + Convert.ToDateTime(model.DischargeDate).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                        var filenamepath = Convert.ToDateTime(model.DischargeDate).Year + "/" + HopitalCode + "/" + "surgery picture/PostSurgery/";
                        var dirUrl = ftp + filenamepath;
                        using (BinaryReader br = new BinaryReader(filedata.InputStream))
                        {
                            fileBytes = br.ReadBytes(filedata.ContentLength);
                        }

                        if (!DoesFtpDirectoryExist(dirUrl))
                        {
                            CreateFTPDirectory(filenamepath);
                        }

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl + filename);
                        request.Method = WebRequestMethods.Ftp.UploadFile;

                        //enter FTP Server credentials
                        request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                        request.ContentLength = fileBytes.Length;
                        request.UsePassive = true;
                        request.UseBinary = true;   // or FALSE for ASCII files
                        request.ServicePoint.ConnectionLimit = fileBytes.Length;
                        request.EnableSsl = false;

                        using (Stream requestStream = request.GetRequestStream())
                        {
                            requestStream.Write(fileBytes, 0, fileBytes.Length);
                            requestStream.Close();
                        }
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        response.Close();
                        model.PostSurgeryPic = filename;
                        //string dirPath = Server.MapPath(dirUrl);
                        //if (!Directory.Exists(dirPath))
                        //{
                        //    Directory.CreateDirectory(dirPath);
                        //}
                        //var fname = Path.Combine(Server.MapPath(dirUrl), filename);
                        //model.PostSurgeryPic = filename;
                        //filedata.SaveAs(fname);
                    }
                    if (Request.Files.GetKey(i) == "PreSurgeryPic")
                    {
                        HttpPostedFileBase filedata = files[i];
                        var patientPhoto = files[i].FileName;
                        var fileExtention = System.IO.Path.GetExtension(patientPhoto);
                        var filename = "PRETSX_" + model.Urn.Substring(model.Urn.Length - 6) + "_" + model.MemberId + Convert.ToDateTime(model.DischargeDate).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                        var filenamepath = Convert.ToDateTime(model.DischargeDate).Year + "/" + HopitalCode + "/" + "surgery picture/PreSurgery/";
                        var dirUrl = ftp + filenamepath;
                        using (BinaryReader br = new BinaryReader(filedata.InputStream))
                        {
                            fileBytes = br.ReadBytes(filedata.ContentLength);
                        }

                        if (!DoesFtpDirectoryExist(dirUrl))
                        {
                            CreateFTPDirectory(filenamepath);
                        }

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl + filename);
                        request.Method = WebRequestMethods.Ftp.UploadFile;

                        //enter FTP Server credentials
                        request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                        request.ContentLength = fileBytes.Length;
                        request.UsePassive = true;
                        request.UseBinary = true;   // or FALSE for ASCII files
                        request.ServicePoint.ConnectionLimit = fileBytes.Length;
                        request.EnableSsl = false;

                        using (Stream requestStream = request.GetRequestStream())
                        {
                            requestStream.Write(fileBytes, 0, fileBytes.Length);
                            requestStream.Close();
                        }
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        response.Close();
                        model.PreSurgeryPic = filename;

                        //string dirPath = Server.MapPath(dirUrl);
                        //if (!Directory.Exists(dirPath))
                        //{
                        //    Directory.CreateDirectory(dirPath);
                        //}
                        //var fname = Path.Combine(Server.MapPath(dirUrl), filename);
                        //model.PreSurgeryPic = filename;
                        //filedata.SaveAs(fname);
                    }
                    if (Request.Files.GetKey(i) == "SpecimenRemovalPic")
                    {
                        HttpPostedFileBase filedata = files[i];
                        var patientPhoto = files[i].FileName;
                        var fileExtention = System.IO.Path.GetExtension(patientPhoto);
                        var filename = "SPS_" + model.Urn.Substring(model.Urn.Length - 6) + "_" + model.MemberId + Convert.ToDateTime(model.DischargeDate).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                        var filenamepath = Convert.ToDateTime(model.DischargeDate).Year + "/" + HopitalCode + "/" + "surgery picture/SpecimenRemovalPic/";
                        var dirUrl = ftp + filenamepath;
                        using (BinaryReader br = new BinaryReader(filedata.InputStream))
                        {
                            fileBytes = br.ReadBytes(filedata.ContentLength);
                        }

                        if (!DoesFtpDirectoryExist(dirUrl))
                        {
                            CreateFTPDirectory(filenamepath);
                        }

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl + filename);
                        request.Method = WebRequestMethods.Ftp.UploadFile;

                        //enter FTP Server credentials
                        request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                        request.ContentLength = fileBytes.Length;
                        request.UsePassive = true;
                        request.UseBinary = true;   // or FALSE for ASCII files
                        request.ServicePoint.ConnectionLimit = fileBytes.Length;
                        request.EnableSsl = false;

                        using (Stream requestStream = request.GetRequestStream())
                        {
                            requestStream.Write(fileBytes, 0, fileBytes.Length);
                            requestStream.Close();
                        }
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        response.Close();
                        model.SpecimenRemovalPic = filename;
                        //string dirPath = Server.MapPath(dirUrl);
                        //if (!Directory.Exists(dirPath))
                        //{
                        //    Directory.CreateDirectory(dirPath);
                        //}
                        //var fname = Path.Combine(Server.MapPath(dirUrl), filename);
                        //model.SpecimenRemovalPic = filename;
                        //filedata.SaveAs(fname);
                    }
                    if (Request.Files.GetKey(i) == "DischargeDoc")
                    {
                        HttpPostedFileBase filedata = files[i];
                        var patientPhoto = files[i].FileName;
                        var fileExtention = System.IO.Path.GetExtension(patientPhoto);
                        var filename = "DS_" + model.Urn.Substring(model.Urn.Length - 6) + "_" + model.MemberId + Convert.ToDateTime(model.DischargeDate).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                        var filenamepath = Convert.ToDateTime(model.DischargeDate).Year + "/" + HopitalCode + "/" + "DischargeSlip/";
                        var dirUrl = ftp + filenamepath;
                        using (BinaryReader br = new BinaryReader(filedata.InputStream))
                        {
                            fileBytes = br.ReadBytes(filedata.ContentLength);
                        }

                        if (!DoesFtpDirectoryExist(dirUrl))
                        {
                            CreateFTPDirectory(filenamepath);
                        }

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl + filename);
                        request.Method = WebRequestMethods.Ftp.UploadFile;

                        //enter FTP Server credentials
                        request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                        request.ContentLength = fileBytes.Length;
                        request.UsePassive = true;
                        request.UseBinary = true;   // or FALSE for ASCII files
                        request.ServicePoint.ConnectionLimit = fileBytes.Length;
                        request.EnableSsl = false;

                        using (Stream requestStream = request.GetRequestStream())
                        {
                            requestStream.Write(fileBytes, 0, fileBytes.Length);
                            requestStream.Close();
                        }
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        response.Close();
                        model.DischargeDoc = filename;
                        //string dirPath = Server.MapPath(dirUrl);
                        //if (!Directory.Exists(dirPath))
                        //{
                        //    Directory.CreateDirectory(dirPath);
                        //}
                        //var fname = Path.Combine(Server.MapPath(dirUrl), filename);
                        //model.DischargeDoc = filename;
                        //filedata.SaveAs(fname);
                    }
                    if (Request.Files.GetKey(i) == "MoralityDoc")
                    {
                        HttpPostedFileBase filedata = files[i];
                        var patientPhoto = files[i].FileName;
                        var fileExtention = System.IO.Path.GetExtension(patientPhoto);
                        var filename = "MOT_" + model.Urn.Substring(model.Urn.Length - 6) + "_" + model.MemberId + Convert.ToDateTime(model.DischargeDate).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                        var filenamepath = Convert.ToDateTime(model.DischargeDate).Year + "/" + HopitalCode + "/" + "MoralityDoc/";
                        var dirUrl = ftp + filenamepath;
                        using (BinaryReader br = new BinaryReader(filedata.InputStream))
                        {
                            fileBytes = br.ReadBytes(filedata.ContentLength);
                        }

                        if (!DoesFtpDirectoryExist(dirUrl))
                        {
                            CreateFTPDirectory(filenamepath);
                        }

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl + filename);
                        request.Method = WebRequestMethods.Ftp.UploadFile;

                        //enter FTP Server credentials
                        request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                        request.ContentLength = fileBytes.Length;
                        request.UsePassive = true;
                        request.UseBinary = true;   // or FALSE for ASCII files
                        request.ServicePoint.ConnectionLimit = fileBytes.Length;
                        request.EnableSsl = false;

                        using (Stream requestStream = request.GetRequestStream())
                        {
                            requestStream.Write(fileBytes, 0, fileBytes.Length);
                            requestStream.Close();
                        }
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        response.Close();
                        model.MoralityDoc = filename;
                        //string dirPath = Server.MapPath(dirUrl);
                        //if (!Directory.Exists(dirPath))
                        //{
                        //    Directory.CreateDirectory(dirPath);
                        //}
                        //var fname = Path.Combine(Server.MapPath(dirUrl), filename);
                        //model.MoralityDoc = filename;
                        //filedata.SaveAs(fname);
                    }
                    if (Request.Files.GetKey(i) == "RefaralDoc")
                    {
                        HttpPostedFileBase filedata = files[i];
                        var patientPhoto = files[i].FileName;
                        var fileExtention = System.IO.Path.GetExtension(patientPhoto);
                        var filename = "REF_" + model.Urn.Substring(model.Urn.Length - 6) + "_" + model.MemberId + Convert.ToDateTime(model.DischargeDate).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                        var filenamepath = Convert.ToDateTime(model.DischargeDate).Year + "/" + HopitalCode + "/" + "ReferralDocument/";
                        var dirUrl = ftp + filenamepath;
                        using (BinaryReader br = new BinaryReader(filedata.InputStream))
                        {
                            fileBytes = br.ReadBytes(filedata.ContentLength);
                        }

                        if (!DoesFtpDirectoryExist(dirUrl))
                        {
                            CreateFTPDirectory(filenamepath);
                        }

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl + filename);
                        request.Method = WebRequestMethods.Ftp.UploadFile;

                        //enter FTP Server credentials
                        request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                        request.ContentLength = fileBytes.Length;
                        request.UsePassive = true;
                        request.UseBinary = true;   // or FALSE for ASCII files
                        request.ServicePoint.ConnectionLimit = fileBytes.Length;
                        request.EnableSsl = false;

                        using (Stream requestStream = request.GetRequestStream())
                        {
                            requestStream.Write(fileBytes, 0, fileBytes.Length);
                            requestStream.Close();
                        }
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        response.Close();
                        model.RefaralDoc = filename;
                        //string dirPath = Server.MapPath(dirUrl);
                        //if (!Directory.Exists(dirPath))
                        //{
                        //    Directory.CreateDirectory(dirPath);
                        //}
                        //var fname = Path.Combine(Server.MapPath(dirUrl), filename);
                        //model.RefaralDoc = filename;
                        //filedata.SaveAs(fname);
                    }
                }
            }
            #endregion

            if (model.RefaralCode == null && model.RefaralStatus == "Y")
            {
                int _min = 100000;
                int _max = 999999;
                Random _rdm = new Random();
                int randomno = _rdm.Next(_min, _max);
                model.RefaralCode = randomno.ToString();
            }

            #region  :: Old Code Process Image Upload
            //if (Request.Files.Count > 0)
            //{
            //    try
            //    {
            //        HttpFileCollectionBase files = Request.Files;
            //        for (int i = 0; i < files.Count; i++)
            //        {
            //            HttpPostedFileBase file = files[i];
            //            fname = file.FileName;
            //            fileExtention = System.IO.Path.GetExtension(file.FileName);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        log.Error(ex);
            //    }
            //}
            #endregion

            DischargePatientModel patientInfo = new DischargePatientModel
            {
                Urn = model.Urn,
                BlockingInvoiceNo = model.BlockingInvoiceNo,
                TransactionId = model.TransactionId,
                TransactionDescription = model.TransactionDescription,
                MemberId = model.MemberId,
                Mortality = model.Mortality,
                DischargeDate = model.DischargeDate,
                Userid = model.Userid,
                FPVerifiedId = model.FPVerifiedId,
                RefaralCode = model.RefaralCode,
                RefaralStatus = model.RefaralStatus,
                RefarHospitalState = model.RefarHospitalState,
                RefarHospitalDistrict = model.RefarHospitalDistrict,
                RefarHospitalName = model.RefarHospitalName,
                RefarHospitalCode = model.RefarHospitalCode,
                RefaralDate = model.RefaralDate,
                RefaralReason = model.RefaralReason,
                VitalParameterList = model.VitalParameterList,
                MoralityDoc = model.MoralityDoc,
                DischargeDoc = model.DischargeDoc,
                RefaralDoc = model.RefaralDoc,
                HospitalCode = model.HospitalCode,
                RefarDoctorName = model.RefarDoctorName,
                RefarDepartment = model.RefarDepartment,
                VerifiedMemberID = model.VerifiedMemberID,
                VerifiedMemberName = model.VerifiedMemberName,
                AuthenticationMode = model.AuthenticationMode,
                IsEmpanel = model.IsEmpanel,
                Overridecode = string.IsNullOrEmpty(model.Overridecode) ? "0" : model.Overridecode,
                IntraSurgeryPic = model.IntraSurgeryPic,
                PostSurgeryPic = model.PostSurgeryPic,
                PreSurgeryPic = model.PreSurgeryPic,
                SpecimenRemovalPic = model.SpecimenRemovalPic,
                PackageClamAmountList = model.PackageClamAmountList,
                RelaxClamAmount = model.RelaxClamAmount,
                DischargeRemark = model.DischargeRemark,
            };

            using (PatientDataServices dataServices = new PatientDataServices())
            {
                result = dataServices.PatientDischargeAdd(patientInfo);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ViewResult ViewPatientSlip()
        {
            return View();
        }

        public FileResult Downloadfile(string pathName,string filename)
        {
            //FTP Server URL.
            string ftp = ConfigurationManager.AppSettings["FTPURL"];
            //FTP Folder name. Leave blank if you want to list files from root folder.
            //string ftpFolder = "Uploads/";
            //Create FTP Request.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + pathName);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            //Enter FTP Server credentials.
            request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FTPUSERID"], ConfigurationManager.AppSettings["FTPPASSWORD"]);
            request.UsePassive = true;
            request.UseBinary = true;
            request.EnableSsl = false;
            //Fetch the Response and read it into a MemoryStream object.
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            var contentType = "application/force-download";
            var ftpStream = response.GetResponseStream();
            return File(ftpStream, contentType, filename);
        }

    }
}