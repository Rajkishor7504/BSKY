using HPSBYS.Data.Model;
using HPSBYS.Data.Services;
using HPSBYS.Web.Fiilters;
using HPSBYS.Web.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HPSBYS.Web.Controllers
{
    [SessionTimeOutFilter]
    [Authorize]
    [NoDirectAccess]
    public class BlockPackageController : Controller
    {
        ILogger log = LogManager.GetCurrentClassLogger();
        string result = string.Empty;
        // GET: BlockPackage
        [NonAction]
        private static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        [HttpGet]
        public ViewResult AddBlockPackage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddBlockPackage(FormCollection PaitentInfo)
        {
            string fname = string.Empty;
            string ActFileName = string.Empty;
            string fileExtention = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    string PreAuthDoc = string.Empty;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        fname = file.FileName;
                        fileExtention = System.IO.Path.GetExtension(file.FileName);
                        int Year = DateTime.Now.Year;
                        string Month = DateTime.Now.ToString("MMMM");
                        string HopitalCode = Session["HospitalCode"].ToString();
                        string dirUrl = "~/UploadDocument/" + HopitalCode + "/" + Year + "/" + Month + "/PreAuthDocument";
                        string dirPath = Server.MapPath(dirUrl);
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }
                        var newGuid = Guid.NewGuid();
                        string subGuid = newGuid.ToString().Substring(0, 15);
                        ActFileName = "PREAUTH_" + PaitentInfo["URN"] + "_" + GetTimestamp(DateTime.Now) + fileExtention;
                        //ActFileName = subGuid + "_" + fname;
                        PreAuthDoc = ActFileName;
                        fname = Path.Combine(Server.MapPath(dirUrl), ActFileName);
                        file.SaveAs(fname);

                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            if (PaitentInfo != null)
            {
                string VchFileName = string.Empty;
                if (!string.IsNullOrEmpty(ActFileName))
                {
                    VchFileName = ActFileName;
                }
                else
                {
                    VchFileName = "";
                }
                PatientInfo patientInfo = new PatientInfo
                {
                    ACTIONCODE = PaitentInfo["ACTIONCODE"],
                    BlockingInvoiceNo = PaitentInfo["BlockingInvoiceNo"],
                    BlockingUserDate = PaitentInfo["BlockingUserDate"],
                    DATEOFADMISSION = PaitentInfo["DATEOFADMISSION"],
                    ProcedureCode = PaitentInfo["ProcedureCode"],
                    ProcedureName = PaitentInfo["ProcedureName"],
                    PackageCode = PaitentInfo["PackageCode"],
                    PackageName = PaitentInfo["PackageName"],
                    WardId = PaitentInfo["PackageWard"] == "null" || PaitentInfo["PackageWard"] == "undefined" ? 0 : Convert.ToInt32(PaitentInfo["PackageWard"]),
                    PackageCost = PaitentInfo["PackageCost"],
                    NoofDays = PaitentInfo["NoofDays"],
                    AmoutBlocked = PaitentInfo["AmoutBlocked"],
                    TransactionCode = PaitentInfo["TransactionCode"],
                    PreAuthStatus = PaitentInfo["PreAuthStatus"],
                    VchFile = VchFileName,
                    CappedAmount = PaitentInfo["CappedAmount"],
                    IsMedSergical = PaitentInfo["IsMedSergical"],
                    Category = PaitentInfo["Category"],
                    CategoryCode = PaitentInfo["CategoryCode"],
                    URN = PaitentInfo["URN"] // NEWLY ADDED

                };

                using (PatientDataServices dataServices = new PatientDataServices())
                {
                    result = dataServices.addPatientBlockPackage(patientInfo);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ViewBlockPackage()
        {
            string groupid = Convert.ToString(Session["groupid"]);
            if (groupid != "1")
            {
                return View();

            }
            else
            {
                return RedirectToAction("adminViewBlockPackageDetails", "BlockPackage");
            }

        }
        [HttpGet]
        public ViewResult adminViewBlockPackageDetails()     //purpose for admin view
        {
            return View();
        }


        [HttpGet]
        public ViewResult ViewBlockPackageDetails()
        {
            return View();
        }
        [HttpGet]
        public ViewResult ViewBlockRoomTypeChange()
        {
            return View();
        }
        [HttpGet]
        public ViewResult ViewPackageChange()
        {
            return View();
        }

        [HttpGet]
        public ViewResult AddCancellation()
        {
            return View();
        }
        public ActionResult ViewMultipleBlockPackage()
        {
            return View();
        }
        [HttpGet]
        public ViewResult ChangeRoomType()
        {
            return View();
        }
        [HttpGet]
        public ViewResult PackageChange()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddPackageChange(FormCollection PaitentInfo)
        {
            string fname = string.Empty;
            string ActFileName = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    string PreAuthDoc = string.Empty;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        fname = file.FileName;
                        int Year = DateTime.Now.Year;
                        string Month = DateTime.Now.ToString("MMMM");
                        string HopitalCode = Session["HospitalCode"].ToString();
                        string dirUrl = "~/UploadDocument/" + HopitalCode + "/" + Year + "/" + Month + "/PreAuthDocument";
                        string dirPath = Server.MapPath(dirUrl);
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }
                        var newGuid = Guid.NewGuid();
                        string subGuid = newGuid.ToString().Substring(0, 15);
                        // ActFileName = subGuid + "_" + fname;
                        ActFileName = "PACKAGECHANGE_" + PaitentInfo["URN"] + "_" + GetTimestamp(DateTime.Now);
                        PreAuthDoc = ActFileName;
                        fname = Path.Combine(Server.MapPath(dirUrl), ActFileName);
                        file.SaveAs(fname);

                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            if (PaitentInfo != null)
            {
                string VchFileName = string.Empty;
                if (!string.IsNullOrEmpty(ActFileName))
                {
                    VchFileName = ActFileName;
                }
                else
                {
                    VchFileName = "";
                }
                PatientInfo patientInfo = new PatientInfo
                {
                    ACTIONCODE = PaitentInfo["ACTIONCODE"],
                    BlockingInvoiceNo = PaitentInfo["BlockingInvoiceNo"],
                    BlockingUserDate = PaitentInfo["BlockingUserDate"],
                    DATEOFADMISSION = PaitentInfo["DATEOFADMISSION"],
                    ProcedureCode = PaitentInfo["ProcedureCode"],
                    ProcedureName = PaitentInfo["ProcedureName"],
                    PackageCode = PaitentInfo["PackageCode"],
                    PackageName = PaitentInfo["PackageName"],
                    WardId = PaitentInfo["PackageWard"] == "null" || PaitentInfo["PackageWard"] == "undefined" ? 0 : Convert.ToInt32(PaitentInfo["PackageWard"]),
                    PackageCost = PaitentInfo["PackageCost"],
                    NoofDays = PaitentInfo["NoofDays"],
                    AmoutBlocked = PaitentInfo["AmoutBlocked"],
                    TransactionCode = PaitentInfo["TransactionCode"],
                    PreAuthStatus = PaitentInfo["PreAuthStatus"],
                    VchFile = VchFileName,
                    CappedAmount = PaitentInfo["CappedAmount"],
                    IsMedSergical = PaitentInfo["IsMedSergical"],
                    Category = PaitentInfo["Category"],
                    CategoryCode = PaitentInfo["CategoryCode"]
                };

                using (PatientDataServices dataServices = new PatientDataServices())
                {
                    result = dataServices.addPatientBlockPackage_PackageChange(patientInfo);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ViewResult ViewPreviousWardDetails(int? TranId, string Date)
        {
            ViewBag.Id = TranId;
            ViewBag.Dt = Date;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddChangeRoomType(FormCollection PaitentInfo)
        {
            string fname = string.Empty;
            string ActFileName = string.Empty;
            string fileExtention = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    string PreAuthDoc = string.Empty;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        fname = file.FileName;
                        fileExtention = System.IO.Path.GetExtension(file.FileName);
                        int Year = DateTime.Now.Year;
                        string Month = DateTime.Now.ToString("MMMM");
                        string HopitalCode = Session["HospitalCode"].ToString();
                        string dirUrl = "~/UploadDocument/" + HopitalCode + "/" + Year + "/" + Month + "/PreAuthDocument";
                        string dirPath = Server.MapPath(dirUrl);
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }
                        var newGuid = Guid.NewGuid();
                        string subGuid = newGuid.ToString().Substring(0, 15);
                        ActFileName = "RTC_" + PaitentInfo["URN"] + "_" + GetTimestamp(DateTime.Now) + fileExtention;            //RTC Room Type Change.
                        //ActFileName = subGuid + "_" + fname;
                        PreAuthDoc = ActFileName;
                        fname = Path.Combine(Server.MapPath(dirUrl), ActFileName);
                        file.SaveAs(fname);

                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            if (PaitentInfo != null)
            {
                string VchFileName = string.Empty;
                if (!string.IsNullOrEmpty(ActFileName))
                {
                    VchFileName = ActFileName;
                }
                else
                {
                    VchFileName = "";
                }
                PatientInfo patientInfo = new PatientInfo
                {
                    ACTIONCODE = PaitentInfo["ACTIONCODE"],
                    BlockingInvoiceNo = PaitentInfo["BlockingInvoiceNo"],
                    BlockingUserDate = PaitentInfo["BlockingUserDate"],
                    TransactionID = PaitentInfo["TranId"],
                    PackageCode = PaitentInfo["PackageCode"],
                    WardId = Convert.ToInt32(PaitentInfo["WardId"]),
                    PreAuthStatus = PaitentInfo["PreAuthStatus"],
                    AmoutBlocked = PaitentInfo["AmoutBlocked"],
                    VchFile = VchFileName,
                    HospitalCode = Session["HospitalCode"].ToString()
                };

                using (PatientDataServices dataServices = new PatientDataServices())
                {
                    result = dataServices.addRoomTypeDetails(patientInfo);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ViewResult AddPackageMedicalToSurgical()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddMedicalToSurgicalPackage(FormCollection PaitentInfo)
        {
            string fname = string.Empty;
            string ActFileName = string.Empty;
            string fileExtention = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    string PreAuthDoc = string.Empty;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        fname = file.FileName;
                        fileExtention = System.IO.Path.GetExtension(file.FileName);
                        int Year = DateTime.Now.Year;
                        string Month = DateTime.Now.ToString("MMMM");
                        string HopitalCode = Session["HospitalCode"].ToString();
                        string dirUrl = "~/UploadDocument/" + HopitalCode + "/" + Year + "/" + Month + "/PreAuthDocument";
                        string dirPath = Server.MapPath(dirUrl);
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }
                        var newGuid = Guid.NewGuid();
                        string subGuid = newGuid.ToString().Substring(0, 15);
                        //ActFileName = subGuid + "_" + fname;
                        ActFileName = "PC_" + PaitentInfo["URN"] + "_" + GetTimestamp(DateTime.Now) + fileExtention;
                        PreAuthDoc = ActFileName;
                        fname = Path.Combine(Server.MapPath(dirUrl), ActFileName);
                        file.SaveAs(fname);

                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            if (PaitentInfo != null)
            {
                string VchFileName = string.Empty;
                if (!string.IsNullOrEmpty(ActFileName))
                {
                    VchFileName = ActFileName;
                }
                else
                {
                    VchFileName = "";
                }
                PatientInfo patientInfo = new PatientInfo
                {
                    ACTIONCODE = PaitentInfo["ACTIONCODE"],
                    BlockingInvoiceNo = PaitentInfo["BlockingInvoiceNo"],
                    BlockingUserDate = PaitentInfo["BlockingUserDate"],
                    DATEOFADMISSION = PaitentInfo["DATEOFADMISSION"],
                    ProcedureCode = PaitentInfo["ProcedureCode"],
                    ProcedureName = PaitentInfo["ProcedureName"],
                    PackageCode = PaitentInfo["PackageCode"],
                    PackageName = PaitentInfo["PackageName"],
                    WardId = PaitentInfo["PackageWard"] == "null" || PaitentInfo["PackageWard"] == "undefined" ? 0 : Convert.ToInt32(PaitentInfo["PackageWard"]),
                    PackageCost = PaitentInfo["PackageCost"],
                    NoofDays = PaitentInfo["NoofDays"],
                    AmoutBlocked = PaitentInfo["AmoutBlocked"],
                    TransactionCode = PaitentInfo["TransactionCode"],
                    PreAuthStatus = PaitentInfo["PreAuthStatus"],
                    VchFile = VchFileName,
                    CappedAmount = PaitentInfo["CappedAmount"],
                    IsMedSergical = PaitentInfo["IsMedSergical"],
                    Category = PaitentInfo["Category"],
                    CategoryCode = PaitentInfo["CategoryCode"],
                    URN = PaitentInfo["URN"]
                };

                using (PatientDataServices dataServices = new PatientDataServices())
                {
                    result = dataServices.addPatientBlockPackage_PackageChange(patientInfo);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ViewResult AddPackageMedicalToMedical()
        {
            return View();
        }
        [HttpGet]
        public ViewResult AddOnPackage(string Id)
        {
            ViewBag.TrId = Id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddOnPackageType(FormCollection PaitentInfo)
        {
            string fname = string.Empty;
            string fileExtention = string.Empty;
            string ActFileName = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    string PreAuthDoc = string.Empty;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        fname = file.FileName;
                        fileExtention = System.IO.Path.GetExtension(file.FileName);
                        int Year = DateTime.Now.Year;
                        string Month = DateTime.Now.ToString("MMMM");
                        string HopitalCode = Session["HospitalCode"].ToString();
                        string dirUrl = "~/UploadDocument/" + HopitalCode + "/" + Year + "/" + Month + "/PreAuthDocument";
                        string dirPath = Server.MapPath(dirUrl);
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }
                        var newGuid = Guid.NewGuid();
                        string subGuid = newGuid.ToString().Substring(0, 15);
                        ActFileName = "ADDON_" + PaitentInfo["URN"] + "_" + GetTimestamp(DateTime.Now) + fileExtention;
                        PreAuthDoc = ActFileName;
                        fname = Path.Combine(Server.MapPath(dirUrl), ActFileName);
                        file.SaveAs(fname);

                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            if (PaitentInfo != null)
            {
                string VchFileName = string.Empty;
                if (!string.IsNullOrEmpty(ActFileName))
                {
                    VchFileName = ActFileName;
                }
                else
                {
                    VchFileName = "";
                }
                PatientInfo patientInfo = new PatientInfo
                {
                    ACTIONCODE = PaitentInfo["ACTIONCODE"],
                    TransactionID = PaitentInfo["TranId"],
                    ProcedureCode = PaitentInfo["ProcedureCode"],
                    CategoryCode = PaitentInfo["CategoryCode"],
                    PackageCode = PaitentInfo["PackageCode"],
                    PreAuthStatus = PaitentInfo["PreAuthStatus"],
                    AmoutBlocked = PaitentInfo["AmoutBlocked"],
                    BlockingUserDate = PaitentInfo["BlockingUserDate"],
                    IsMedSergical = PaitentInfo["IsMedSergical"],
                    VchFile = VchFileName,
                };

                using (PatientDataServices dataServices = new PatientDataServices())
                {
                    result = dataServices.addAddOnPackageDetails(patientInfo);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ViewResult ViewAddOnPackageDetails(string InvNo)
        {
            ViewBag.InvNo = InvNo;
            return View();
        }
        [HttpGet]
        public ViewResult BiomatiquesTest()
        {
            return View();
        }

        public ActionResult BlockPreApproval()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPreApprovalRequest(string preauthstatus, string actioncode, string hospitalcode,string FromDate,string ToDate)
        {
            List<PreApprovalViewModel> objpackage = new List<PreApprovalViewModel>();
            using (PatientDataServices dataServices = new PatientDataServices())
            {
                objpackage = dataServices.GetPreapprovalPackagesList(preauthstatus, actioncode, hospitalcode, FromDate, ToDate);
            }
            if (objpackage != null)
            {
                return Json(new { data = objpackage }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = objpackage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateBlockPackage(ViewModelPackageUpdation obj)
        {
            string result = string.Empty;
            using (PatientDataServices dataServices = new PatientDataServices())
            {
                result = dataServices.UpdatePackageApproval(obj);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlockOverrideRequest()
        {
            return View();
        }

        public ActionResult BlockOverrideView() // ADDED by Akshat (25-Jan-23)
        {
            return View();
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
        //private bool CreateFTPDirectory(string directory)
        //{
        //    try
        //    {
        //        //create the directory
        //        FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(directory));
        //        requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
        //        requestDir.Credentials = new NetworkCredential("bskytmsu1", "csmpl@1234");
        //        requestDir.UsePassive = true;
        //        requestDir.UseBinary = true;
        //        requestDir.KeepAlive = false;
        //        FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
        //        Stream ftpStream = response.GetResponseStream();

        //        ftpStream.Close();
        //        response.Close();

        //        return true;
        //    }
        //    catch (WebException ex)
        //    {
        //        FtpWebResponse response = (FtpWebResponse)ex.Response;
        //        if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
        //        {
        //            response.Close();
        //            return true;
        //        }
        //        else
        //        {
        //            response.Close();
        //            return false;
        //        }
        //    }
        //}
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
        [HttpPost]
        public async Task<ActionResult> addPatientBlockPackageInsertion(HttpPostedFileBase FileData, HttpPostedFileBase PhotoFile)
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
            PatientInfo obj1 = JsonConvert.DeserializeObject<PatientInfo>(Request.Form["alldata"].ToString());
            //var smscontent = "ମହୋଦୟ,\nଆପଣଙ୍କ " + obj1.HospitalName + " ଡାକ୍ତରଖାନା ରେ ଖର୍ଚ୍ଚ ହୋଇଥିବା " + obj1.AmoutBlocked + " ଟଙ୍କା କୁ ଓଡିଶା ସରକାର ବିଜୁ ସ୍ୱାସ୍ଥ୍ୟ କଲ୍ୟାଣ ଯୋଜନା ମାଧ୍ୟମ ରେ ବହନ କଲେ | ଏହି ଯୋଜନା ଅନ୍ତର୍ଗତ ପରିବାର ପିଛା ବଳକା {#var#}  ଟଙ୍କା ଏବଂ ମହିଳାଙ୍କ କ୍ଷେତ୍ର ରେ {#var#} ଟଙ୍କା ଯେ କୌଣସି ବିଜୁ ସ୍ୱାସ୍ଥ୍ୟ କଲ୍ୟାଣ ଯୋଜନା ଦ୍ୱାରା ପରିଚାଳିତ ଡାକ୍ତରଖାନା ରେ ବ୍ୟବହାର କରିପାରିବେ |";

            string fname = string.Empty;
            string fname1 = string.Empty;
            string fileExtention = string.Empty;
            string photofileextension = string.Empty;
            string motalityFile = string.Empty;
            HttpFileCollectionBase Motalityfiles;
            HttpPostedFileBase motalityfile;
            HttpFileCollectionBase photofiles;
            HttpPostedFileBase photofile;
            string filename;
            string filenamepath;
            string photoifilename;
            string photofilenamepath;

            using (var PatientDataServices = new PatientDataServices())
            {
                if (FileData == null && PhotoFile != null)
                {
                    photofiles = Request.Files;
                    photofile = photofiles[0];
                    var Photofilename = photofiles[0].FileName;
                    photofileextension = System.IO.Path.GetExtension(Photofilename);
                    photoifilename = "PP_" + obj1.URN.Substring(obj1.URN.Length - 6) + "_" + obj1.MemberID + Convert.ToDateTime(obj1.DATEOFADMISSION).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + photofileextension;
                    photofilenamepath = Convert.ToDateTime(obj1.DATEOFADMISSION).Year + "/" + obj1.HospitalCode + "/" + "PatientPic/";
                    obj1.UploadDoc1 = /*photofilenamepath + "/" +*/ photoifilename;
                    
                        try
                        {
                            string dirUrl1 = ftp + photofilenamepath;
                            using (BinaryReader br = new BinaryReader(photofile.InputStream))
                            {
                                fileBytes = br.ReadBytes(photofile.ContentLength);
                            }

                            if (!DoesFtpDirectoryExist(dirUrl1))
                            {
                                CreateFTPDirectory(photofilenamepath);
                            }
                            string ActFileName1 = photoifilename;
                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl1 + ActFileName1);
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
                            var data = Task.FromResult(PatientDataServices.addPatientBlockPackageInsert(obj1));
                            if (data.Result.Count > 0 && data.Result[0].STATUS != 100 && data.Result[0].ADMISSIONFLAG == 1)
                            {
                                var smscontent = "ମହୋଦୟ,\nଆପଣଙ୍କ " + obj1.HospitalName + " ଡାକ୍ତରଖାନା ରେ ଖର୍ଚ୍ଚ ହୋଇଥିବା " + data.Result[0].TOTALBLOCKED + " ଟଙ୍କା କୁ ଓଡିଶା ସରକାର ବିଜୁ ସ୍ୱାସ୍ଥ୍ୟ କଲ୍ୟାଣ ଯୋଜନା ମାଧ୍ୟମ ରେ ବହନ କଲେ | ଏହି ଯୋଜନା ଅନ୍ତର୍ଗତ ପରିବାର ପିଛା ବଳକା " + data.Result[0].FAMILYAVAILABLE + "  ଟଙ୍କା ଏବଂ ମହିଳାଙ୍କ କ୍ଷେତ୍ର ରେ " + data.Result[0].FEMALEAVAILABLE + " ଟଙ୍କା ଯେ କୌଣସି ବିଜୁ ସ୍ୱାସ୍ଥ୍ୟ କଲ୍ୟାଣ ଯୋଜନା ଦ୍ୱାରା ପରିଚାଳିତ ଡାକ୍ତରଖାନା ରେ ବ୍ୟବହାର କରିପାରିବେ |";
                                using (var client = new HttpClient())
                                {
                                    var dataContent = new FormUrlEncodedContent(new[]
                                     {
                                new KeyValuePair<string, string>("action", "singlewithbulkunicodeSMS"),
                                new KeyValuePair<string, string>("department_id", "D006001"),
                                new KeyValuePair<string, string>("template_id", "1407163187446153986"),
                                new KeyValuePair<string, string>("sms_content", smscontent),
                                new KeyValuePair<string, string>("phonenumber", obj1.PatientContactNumber),
                                });

                                    ServicePointManager.Expect100Continue = true;
                                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                    var url = "https://govtsms.odisha.gov.in/api/api.php";// USER FOR DEVELOPMENT  
                                    HttpResponseMessage responsee = await client.PostAsync(url, dataContent);
                                    var result = responsee.Content.ReadAsStringAsync().Result;
                                    Root resultdata = JsonConvert.DeserializeObject<Root>(result);

                                }
                            }
                            return Json(new { data = data });
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                 
                }
                else if (FileData != null && PhotoFile == null)
                {
                    Motalityfiles = Request.Files;
                    motalityfile = Motalityfiles[0];
                    var motalityfilename = Motalityfiles[0].FileName;
                    fileExtention = System.IO.Path.GetExtension(motalityfilename);
                    filename = "PD_" + obj1.URN.Substring(obj1.URN.Length - 6) + "_" + obj1.MemberID + Convert.ToDateTime(obj1.DATEOFADMISSION).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                    filenamepath = Convert.ToDateTime(obj1.DATEOFADMISSION).Year + "/" + obj1.HospitalCode + "/" + "PREAUTHDOC/";
                    obj1.UploadDoc = /*filenamepath + "/" +*/ filename;
                    
                        try
                        {
                            string dirUrl1 = ftp + filenamepath;
                            using (BinaryReader br = new BinaryReader(motalityfile.InputStream))
                            {
                                fileBytes = br.ReadBytes(motalityfile.ContentLength);
                            }

                            if (!DoesFtpDirectoryExist(dirUrl1))
                            {
                                CreateFTPDirectory(filenamepath);
                            }
                            string ActFileName1 = filename;
                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl1 + ActFileName1);
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
                        var data = Task.FromResult(PatientDataServices.addPatientBlockPackageInsert(obj1));
                        if (data.Result.Count > 0 && data.Result[0].STATUS != 100 && data.Result[0].ADMISSIONFLAG == 1)
                        {
                            var smscontent = "ମହୋଦୟ,\nଆପଣଙ୍କ " + obj1.HospitalName + " ଡାକ୍ତରଖାନା ରେ ଖର୍ଚ୍ଚ ହୋଇଥିବା " + data.Result[0].TOTALBLOCKED + " ଟଙ୍କା କୁ ଓଡିଶା ସରକାର ବିଜୁ ସ୍ୱାସ୍ଥ୍ୟ କଲ୍ୟାଣ ଯୋଜନା ମାଧ୍ୟମ ରେ ବହନ କଲେ | ଏହି ଯୋଜନା ଅନ୍ତର୍ଗତ ପରିବାର ପିଛା ବଳକା " + data.Result[0].FAMILYAVAILABLE + "  ଟଙ୍କା ଏବଂ ମହିଳାଙ୍କ କ୍ଷେତ୍ର ରେ " + data.Result[0].FEMALEAVAILABLE + " ଟଙ୍କା ଯେ କୌଣସି ବିଜୁ ସ୍ୱାସ୍ଥ୍ୟ କଲ୍ୟାଣ ଯୋଜନା ଦ୍ୱାରା ପରିଚାଳିତ ଡାକ୍ତରଖାନା ରେ ବ୍ୟବହାର କରିପାରିବେ |";
                            using (var client = new HttpClient())
                            {
                                var dataContent = new FormUrlEncodedContent(new[]
                                 {
                                new KeyValuePair<string, string>("action", "singlewithbulkunicodeSMS"),
                                new KeyValuePair<string, string>("department_id", "D006001"),
                                new KeyValuePair<string, string>("template_id", "1407163187446153986"),
                                new KeyValuePair<string, string>("sms_content", smscontent),
                                new KeyValuePair<string, string>("phonenumber", obj1.PatientContactNumber),
                                    });

                                ServicePointManager.Expect100Continue = true;
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                var url = "https://govtsms.odisha.gov.in/api/api.php";// USER FOR DEVELOPMENT  
                                HttpResponseMessage responsee = await client.PostAsync(url, dataContent);
                                var result = responsee.Content.ReadAsStringAsync().Result;
                                Root resultdata = JsonConvert.DeserializeObject<Root>(result);

                            }
                        }
                            return Json(new { data = data });
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    
                }

                else if (FileData != null && PhotoFile != null)
                {
                    Motalityfiles = Request.Files;
                    photofiles = Request.Files;
                    motalityfile = Motalityfiles[0];
                    photofile = photofiles[1];
                    var motalityfilename = Motalityfiles[0].FileName;
                    var Photofilename = photofiles[1].FileName;
                    fileExtention = System.IO.Path.GetExtension(motalityfilename);
                    photofileextension = System.IO.Path.GetExtension(Photofilename);
                    filename = "PD_" + obj1.URN.Substring(obj1.URN.Length - 6) + "_" + obj1.MemberID + Convert.ToDateTime(obj1.DATEOFADMISSION).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
                    filenamepath = Convert.ToDateTime(obj1.DATEOFADMISSION).Year + "/" + obj1.HospitalCode + "/" + "PREAUTHDOC/";
                    photoifilename = "PP_" + obj1.URN.Substring(obj1.URN.Length - 6) + "_" + obj1.MemberID + Convert.ToDateTime(obj1.DATEOFADMISSION).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + photofileextension;
                    photofilenamepath = Convert.ToDateTime(obj1.DATEOFADMISSION).Year + "/" + obj1.HospitalCode + "/" + "PatientPic/";
                    obj1.UploadDoc = /*filenamepath + "/" +*/ filename;
                    obj1.UploadDoc1 =/* photofilenamepath + "/" +*/ photoifilename;
                    
                        try
                        {
                            //for file upload
                            string dirUrl = ftp + filenamepath;
                            using (BinaryReader br = new BinaryReader(motalityfile.InputStream))
                            {
                                fileBytes = br.ReadBytes(motalityfile.ContentLength);
                            }

                            if (!DoesFtpDirectoryExist(dirUrl))
                            {
                                CreateFTPDirectory(filenamepath);
                            }
                            string ActFileName = filename;
                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl + ActFileName);
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
                            //string dirUrl = "~/BSKY/" + filenamepath;
                            //string dirPath = Server.MapPath(dirUrl);
                            //if (!Directory.Exists(dirPath))
                            //{
                            //    Directory.CreateDirectory(dirPath);
                            //}
                            //string ActFileName = filename;
                            //fname = Path.Combine(dirPath, ActFileName);
                            //motalityfile.SaveAs(fname);
                            //for photo upload
                            string dirUrl1 = ftp + photofilenamepath;
                            using (BinaryReader br = new BinaryReader(photofile.InputStream))
                            {
                                fileBytes = br.ReadBytes(photofile.ContentLength);
                            }

                            if (!DoesFtpDirectoryExist(dirUrl1))
                            {
                                CreateFTPDirectory(photofilenamepath);
                            }
                            string ActFileName1 = photoifilename;
                            FtpWebRequest requestt = (FtpWebRequest)WebRequest.Create(dirUrl1 + ActFileName1);
                            requestt.Method = WebRequestMethods.Ftp.UploadFile;

                            //enter FTP Server credentials
                            requestt.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                            requestt.ContentLength = fileBytes.Length;
                            requestt.UsePassive = true;
                            requestt.UseBinary = true;   // or FALSE for ASCII files
                            requestt.ServicePoint.ConnectionLimit = fileBytes.Length;
                            requestt.EnableSsl = false;

                            using (Stream requestStreamm = requestt.GetRequestStream())
                            {
                                requestStreamm.Write(fileBytes, 0, fileBytes.Length);
                                requestStreamm.Close();
                            }
                            FtpWebResponse responses = (FtpWebResponse)requestt.GetResponse();
                            responses.Close();
                        var data = Task.FromResult(PatientDataServices.addPatientBlockPackageInsert(obj1));
                        if (data.Result.Count > 0 && data.Result[0].STATUS != 100 && data.Result[0].ADMISSIONFLAG == 1)
                        {
                            var smscontent = "ମହୋଦୟ,\nଆପଣଙ୍କ " + obj1.HospitalName + " ଡାକ୍ତରଖାନା ରେ ଖର୍ଚ୍ଚ ହୋଇଥିବା " + data.Result[0].TOTALBLOCKED + " ଟଙ୍କା କୁ ଓଡିଶା ସରକାର ବିଜୁ ସ୍ୱାସ୍ଥ୍ୟ କଲ୍ୟାଣ ଯୋଜନା ମାଧ୍ୟମ ରେ ବହନ କଲେ | ଏହି ଯୋଜନା ଅନ୍ତର୍ଗତ ପରିବାର ପିଛା ବଳକା " + data.Result[0].FAMILYAVAILABLE + "  ଟଙ୍କା ଏବଂ ମହିଳାଙ୍କ କ୍ଷେତ୍ର ରେ " + data.Result[0].FEMALEAVAILABLE + " ଟଙ୍କା ଯେ କୌଣସି ବିଜୁ ସ୍ୱାସ୍ଥ୍ୟ କଲ୍ୟାଣ ଯୋଜନା ଦ୍ୱାରା ପରିଚାଳିତ ଡାକ୍ତରଖାନା ରେ ବ୍ୟବହାର କରିପାରିବେ |";

                            using (var client = new HttpClient())
                            {
                                var dataContent = new FormUrlEncodedContent(new[]
                                 {
                                new KeyValuePair<string, string>("action", "singlewithbulkunicodeSMS"),
                                new KeyValuePair<string, string>("department_id", "D006001"),
                                new KeyValuePair<string, string>("template_id", "1407163187446153986"),
                                new KeyValuePair<string, string>("sms_content", smscontent),
                                new KeyValuePair<string, string>("phonenumber", obj1.PatientContactNumber),
                            });

                                ServicePointManager.Expect100Continue = true;
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                var url = "https://govtsms.odisha.gov.in/api/api.php";// USER FOR DEVELOPMENT  
                                HttpResponseMessage responseees = await client.PostAsync(url, dataContent);
                                var result = responseees.Content.ReadAsStringAsync().Result;
                                Root resultdata = JsonConvert.DeserializeObject<Root>(result);

                            }
                        }
                            return Json(new { data = data });
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    
                }
                else
                {
                    obj1.UploadDoc = null;
                    obj1.UploadDoc1 = null;
                    var data = Task.FromResult(PatientDataServices.addPatientBlockPackageInsert(obj1));
                    if (data.Result.Count > 0 && data.Result[0].STATUS!=100 && data.Result[0].ADMISSIONFLAG == 1)
                    {
                        var smscontent = "ମହୋଦୟ,\nଆପଣଙ୍କ " + obj1.HospitalName + " ଡାକ୍ତରଖାନା ରେ ଖର୍ଚ୍ଚ ହୋଇଥିବା " + data.Result[0].TOTALBLOCKED + " ଟଙ୍କା କୁ ଓଡିଶା ସରକାର ବିଜୁ ସ୍ୱାସ୍ଥ୍ୟ କଲ୍ୟାଣ ଯୋଜନା ମାଧ୍ୟମ ରେ ବହନ କଲେ | ଏହି ଯୋଜନା ଅନ୍ତର୍ଗତ ପରିବାର ପିଛା ବଳକା "+ data.Result[0].FAMILYAVAILABLE +"  ଟଙ୍କା ଏବଂ ମହିଳାଙ୍କ କ୍ଷେତ୍ର ରେ "+ data.Result[0].FEMALEAVAILABLE +" ଟଙ୍କା ଯେ କୌଣସି ବିଜୁ ସ୍ୱାସ୍ଥ୍ୟ କଲ୍ୟାଣ ଯୋଜନା ଦ୍ୱାରା ପରିଚାଳିତ ଡାକ୍ତରଖାନା ରେ ବ୍ୟବହାର କରିପାରିବେ |";

                        using (var client = new HttpClient())
                        {
                            var dataContent = new FormUrlEncodedContent(new[]
                             {
                                new KeyValuePair<string, string>("action", "singlewithbulkunicodeSMS"),
                                new KeyValuePair<string, string>("department_id", "D006001"),
                                new KeyValuePair<string, string>("template_id", "1407163187446153986"),
                                new KeyValuePair<string, string>("sms_content", smscontent),
                                new KeyValuePair<string, string>("phonenumber", obj1.PatientContactNumber),
                            });

                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            var url = "https://govtsms.odisha.gov.in/api/api.php";// USER FOR DEVELOPMENT  
                            HttpResponseMessage response = await client.PostAsync(url, dataContent);
                            var result = response.Content.ReadAsStringAsync().Result;
                            Root resultdata = JsonConvert.DeserializeObject<Root>(result);

                        }

                    }
                    //else
                    //{
                    //    return Json(new { data = data });
                    //}
                    return Json(new { data = data });
                }


            }


        }

        //public ActionResult addPatientBlockPackageInsertion(HttpPostedFileBase FileData, HttpPostedFileBase PhotoFile)
        //{
        //    PatientInfo obj1 = JsonConvert.DeserializeObject<PatientInfo>(Request.Form["alldata"].ToString());
        //    string fname = string.Empty;
        //    string fname1 = string.Empty;
        //    string fileExtention = string.Empty;
        //    string photofileextension = string.Empty;
        //    string motalityFile = string.Empty;
        //    HttpFileCollectionBase Motalityfiles;
        //    HttpPostedFileBase motalityfile;
        //    HttpFileCollectionBase photofiles;
        //    HttpPostedFileBase photofile;
        //    string filename;
        //    string filenamepath;
        //    string photoifilename;
        //    string photofilenamepath;

        //    using (var PatientDataServices = new PatientDataServices())
        //    {
        //        if (FileData == null && PhotoFile != null)
        //        {
        //            photofiles = Request.Files;
        //            photofile = photofiles[0];
        //            var Photofilename = photofiles[0].FileName;
        //            photofileextension = System.IO.Path.GetExtension(Photofilename);
        //            photoifilename = "PP_" + obj1.URN.Substring(obj1.URN.Length - 6) + "_" + obj1.MemberID + Convert.ToDateTime(obj1.DATEOFADMISSION).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + photofileextension;
        //            photofilenamepath = Convert.ToDateTime(obj1.DATEOFADMISSION).Year + "/" + obj1.HospitalCode + "/" + "PatientPic";
        //            obj1.UploadDoc1 = /*photofilenamepath + "/" +*/ photoifilename;
        //            var data = Task.FromResult(PatientDataServices.addPatientBlockPackageInsert(obj1));
        //            if (data.Result.Count > 0)
        //            {
        //                try
        //                {
        //                    string dirUrl1 = "~/BSKY/" + photofilenamepath;
        //                    string dirPath1 = Server.MapPath(dirUrl1);
        //                    if (!Directory.Exists(dirPath1))
        //                    {
        //                        Directory.CreateDirectory(dirPath1);
        //                    }
        //                    string ActFileName1 = photoifilename;

        //                    fname1 = Path.Combine(dirPath1, ActFileName1);
        //                    photofile.SaveAs(fname1);
        //                    return Json(new { data = data });
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw ex;
        //                }
        //            }
        //            else
        //            {
        //                return Json(new { data = data });
        //            }
        //        }
        //        else if (FileData != null && PhotoFile == null)
        //        {
        //            Motalityfiles = Request.Files;
        //            motalityfile = Motalityfiles[0];
        //            var motalityfilename = Motalityfiles[0].FileName;
        //            fileExtention = System.IO.Path.GetExtension(motalityfilename);
        //            filename = "PD_" + obj1.URN.Substring(obj1.URN.Length - 6) + "_" + obj1.MemberID + Convert.ToDateTime(obj1.DATEOFADMISSION).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
        //            filenamepath = Convert.ToDateTime(obj1.DATEOFADMISSION).Year + "/" + obj1.HospitalCode + "/" + "PREAUTHDOC";
        //            obj1.UploadDoc = /*filenamepath + "/" +*/ filename;
        //            var data = Task.FromResult(PatientDataServices.addPatientBlockPackageInsert(obj1));
        //            if (data.Result.Count > 0)
        //            {
        //                try
        //                {
        //                    string dirUrl = "~/BSKY/" + filenamepath;
        //                    string dirPath = Server.MapPath(dirUrl);
        //                    if (!Directory.Exists(dirPath))
        //                    {
        //                        Directory.CreateDirectory(dirPath);
        //                    }
        //                    string ActFileName = filename;

        //                    fname = Path.Combine(dirPath, ActFileName);
        //                    motalityfile.SaveAs(fname);
        //                    return Json(new { data = data });
        //                }

        //                catch (Exception ex)
        //                {
        //                    throw ex;
        //                }
        //            }
        //            else
        //            {
        //                return Json(new { data = data });
        //            }
        //        }

        //        else if (FileData != null && PhotoFile != null)
        //        {
        //            Motalityfiles = Request.Files;
        //            photofiles = Request.Files;
        //            motalityfile = Motalityfiles[0];
        //            photofile = photofiles[1];
        //            var motalityfilename = Motalityfiles[0].FileName;
        //            var Photofilename = photofiles[1].FileName;
        //            fileExtention = System.IO.Path.GetExtension(motalityfilename);
        //            photofileextension = System.IO.Path.GetExtension(Photofilename);
        //            filename = "PD_" + obj1.URN.Substring(obj1.URN.Length - 6) + "_" + obj1.MemberID + Convert.ToDateTime(obj1.DATEOFADMISSION).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
        //            filenamepath = Convert.ToDateTime(obj1.DATEOFADMISSION).Year + "/" + obj1.HospitalCode + "/" + "PREAUTHDOC";
        //            photoifilename = "PP_" + obj1.URN.Substring(obj1.URN.Length - 6) + "_" + obj1.MemberID + Convert.ToDateTime(obj1.DATEOFADMISSION).ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + photofileextension;
        //            photofilenamepath = Convert.ToDateTime(obj1.DATEOFADMISSION).Year + "/" + obj1.HospitalCode + "/" + "PatientPic";
        //            obj1.UploadDoc = /*filenamepath + "/" +*/ filename;
        //            obj1.UploadDoc1 =/* photofilenamepath + "/" +*/ photoifilename;
        //            var data = Task.FromResult(PatientDataServices.addPatientBlockPackageInsert(obj1));
        //            if (data.Result.Count > 0)
        //            {
        //                try
        //                {
        //                    string dirUrl = "~/BSKY/" + filenamepath;
        //                    string dirPath = Server.MapPath(dirUrl);
        //                    if (!Directory.Exists(dirPath))
        //                    {
        //                        Directory.CreateDirectory(dirPath);
        //                    }
        //                    string ActFileName = filename;
        //                    fname = Path.Combine(dirPath, ActFileName);
        //                    motalityfile.SaveAs(fname);
        //                    //for photo upload
        //                    string dirUrl1 = "~/BSKY/" + photofilenamepath;
        //                    string dirPath1 = Server.MapPath(dirUrl1);
        //                    if (!Directory.Exists(dirPath1))
        //                    {
        //                        Directory.CreateDirectory(dirPath1);
        //                    }
        //                    string ActFileName1 = photoifilename;

        //                    fname1 = Path.Combine(dirPath1, ActFileName1);
        //                    photofile.SaveAs(fname1);
        //                    return Json(new { data = data });
        //                }

        //                catch (Exception ex)
        //                {
        //                    throw ex;
        //                }
        //            }
        //            else
        //            {
        //                return Json(new { data = data });
        //            }
        //        }
        //        else
        //        {
        //            obj1.UploadDoc = null;
        //            obj1.UploadDoc1 = null;
        //            var data = Task.FromResult(PatientDataServices.addPatientBlockPackageInsert(obj1));
        //            return Json(new { data = data });
        //        }


        //    }

        //}


        //public ActionResult addPatientBlockPackageInsertion(HttpPostedFileBase FileData, HttpPostedFileBase PhotoFile)
        //{
        //    PatientInfo obj1 = JsonConvert.DeserializeObject<PatientInfo>(Request.Form["alldata"].ToString());
        //    string fname = string.Empty;
        //    string fname1 = string.Empty;
        //    string fileExtention = string.Empty;
        //    string photofileextension = string.Empty;
        //    string motalityFile = string.Empty;
        //    HttpFileCollectionBase Motalityfiles = Request.Files;
        //    HttpFileCollectionBase photofiles = Request.Files;
        //    HttpPostedFileBase motalityfile = Motalityfiles[0];
        //    HttpPostedFileBase photofile = photofiles[1];
        //    var motalityfilename = Motalityfiles[0].FileName;
        //    var Photofilename = photofiles[1].FileName;

        //    fileExtention = System.IO.Path.GetExtension(motalityfilename);
        //    photofileextension = System.IO.Path.GetExtension(Photofilename);
        //    string filename = obj1.MemberID + obj1.HospitalCode + DateTime.Now.ToString("ddMMyy") + DateTime.Now.ToString("hhmmss") + fileExtention;
        //    string photoifilename = obj1.MemberID + obj1.HospitalCode + DateTime.Now.ToString("ddMMyy") + DateTime.Now.ToString("hhmmss") + photofileextension;
        //    string filenamepath = Convert.ToDateTime(obj1.DATEOFADMISSION).Year + "/" + obj1.HospitalCode + "/" + "PreAuthDoc";
        //    string photofilenamepath = obj1.HospitalCode + "/" + obj1.MemberID + "/" + "PatientPhoto";
        //    obj1.UploadDoc = filenamepath + "/" + filename;
        //    using (var PatientDataServices = new PatientDataServices())
        //    {
        //        var data = Task.FromResult(PatientDataServices.addPatientBlockPackageInsert(obj1));
        //        if (data.Result.Count > 0)
        //        {
        //            try
        //            {
        //                string dirUrl = "~/UploadDocument/" + filenamepath;
        //                string dirPath = Server.MapPath(dirUrl);
        //                if (!Directory.Exists(dirPath))
        //                {
        //                    Directory.CreateDirectory(dirPath);
        //                }
        //                string ActFileName = filename;

        //                fname = Path.Combine(dirPath, ActFileName);
        //                motalityfile.SaveAs(fname);




        //            }

        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //            //for photo upload
        //            try
        //            {
        //                string dirUrl1 = "~/UploadDocument/" + photofilenamepath;
        //                string dirPath1 = Server.MapPath(dirUrl1);
        //                if (!Directory.Exists(dirPath1))
        //                {
        //                    Directory.CreateDirectory(dirPath1);
        //                }
        //                string ActFileName1 = photoifilename;

        //                fname1 = Path.Combine(dirPath1, ActFileName1);
        //                photofile.SaveAs(fname1);
        //                return Json(new { data = data });
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }

        //        }




        //        else
        //        {
        //            return Json(new { data = data });
        //        }


        //    }

        //}

        //public ActionResult addPatientBlockPackageInsertion(HttpPostedFileBase FileData)
        //{
        //    PatientInfo obj1 = JsonConvert.DeserializeObject<PatientInfo>(Request.Form["alldata"].ToString());
        //    string fname = string.Empty;
        //    string fileExtention = string.Empty;
        //    string motalityFile = string.Empty;
        //    HttpFileCollectionBase Motalityfiles = Request.Files;
        //    HttpPostedFileBase motalityfile = Motalityfiles[0];
        //    var motalityfilename = Motalityfiles[0].FileName;
        //    fileExtention = System.IO.Path.GetExtension(motalityfilename);
        //    string filename = obj1.MemberID + obj1.HospitalCode + DateTime.Now.ToString("ddMMyy") + DateTime.Now.ToString("hhmmss") + fileExtention;

        //    //string filenamepath = "~/UploadDocument/" + Convert.ToDateTime(obj1.DATEOFADMISSION).Year + "/" + Convert.ToDateTime(obj1.DATEOFADMISSION).ToString("MMMM") + "/" + obj1.HospitalCode +  "/BlockingDocument";
        //    string filenamepath = Convert.ToDateTime(obj1.DATEOFADMISSION).Year + "/" + obj1.HospitalCode + "/" + "PreAuthDoc";
        //    obj1.UploadDoc = filenamepath + "/" + filename;
        //    using (var PatientDataServices = new PatientDataServices())
        //    {
        //        var data = Task.FromResult(PatientDataServices.addPatientBlockPackageInsert(obj1));
        //        if (data.Result.Count > 0)
        //        {
        //            try
        //            {
        //                string dirUrl = "~/UploadDocument/" + filenamepath;
        //                string dirPath = Server.MapPath(dirUrl);
        //                if (!Directory.Exists(dirPath))
        //                {
        //                    Directory.CreateDirectory(dirPath);
        //                }
        //                string ActFileName = filename;

        //                fname = Path.Combine(dirPath, ActFileName);
        //                motalityfile.SaveAs(fname);

        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //            return Json(new { data = data });
        //        }
        //        else
        //        {
        //            return Json(new { data = data });
        //        }


        //    }

        //}
        [HttpPost]
        public ActionResult GenerateOverrideRequest(FormCollection collection) // ADDED by Akshat (25-Jan-23)
        {
            #region FTP 
            // FTP Server URL
            //string ftp = "ftp://192.168.10.76/";
            //byte[] fileBytes = null;
            //string ftpUserName = "bskytmsu1";
            //string ftpPassword = "csmpl@1234";
            //LIVE
           // string ftp = "ftp://10.150.68.27/";
            byte[] fileBytes = null;
            string ftpUserName = ConfigurationManager.AppSettings["FTPUSERID"] ;
            string ftpPassword = ConfigurationManager.AppSettings["FTPPASSWORD"];
            //END LIVE
            #endregion
            string fname = string.Empty;
            string fileExtention = string.Empty;
            string motalityFile = string.Empty;
            string filename = string.Empty;
            HttpFileCollectionBase Motalityfiles = Request.Files;
            if (Motalityfiles.Count != 0)
            {
                HttpPostedFileBase motalityfile = Motalityfiles[0];
                var motalityfilename = Motalityfiles[0].FileName;
                fileExtention = System.IO.Path.GetExtension(motalityfilename);
                filename = "OC_" + collection["URN"].Substring(collection["URN"].Length - 6) + "_" + collection["MemberID"] + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;

            }

            PatientInfo patientInfo = new PatientInfo
            {

                MemberID = collection["MemberID"],
                URN = collection["URN"],
                NoofDays = collection["NoofDays"],
                UploadDoc = collection["UploadDoc"],
                UploadDoc1 = /*filenamepath + "/" +*/ filename,
                Remarks = collection["Description"],
                GENERATEDTHROUGH = collection["GENERATEDTHROUGH"],
                HospitalCode = collection["HospitalCode"],
                HospitalCategoryId = collection["HospitalCategoryId"],
                ACTIONCODE = collection["ActionCode"]
            };
            using (PatientDataServices dataServices = new PatientDataServices())
            {
                var data = dataServices.GenerateOverrideRequestService(patientInfo);
                if (data[0].output == 2 && Motalityfiles.Count != 0)
                {
                    try
                    {
                        HttpPostedFileBase motalityfile = Motalityfiles[0];
                        var motalityfilename = Motalityfiles[0].FileName;
                        fileExtention = System.IO.Path.GetExtension(motalityfilename);
                        string filenamepath = DateTime.Now.ToString("yyyy") + "/" + collection["HospitalCode"] + "/OverRideCode/";

                        string dirUrl1 = ConfigurationManager.AppSettings["FTPURL"] + filenamepath;
                        using (BinaryReader br = new BinaryReader(motalityfile.InputStream))
                        {
                            fileBytes = br.ReadBytes(motalityfile.ContentLength);
                        }

                        if (!DoesFtpDirectoryExist(dirUrl1))
                        {
                            CreateFTPDirectory(filenamepath);
                        }
                        string ActFileName1 = filename;
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl1 + ActFileName1);
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
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return Json(data);
            }

        }

        //public JsonResult GenerateOverrideRequest(FormCollection collection) // ADDED by Akshat (25-Jan-23)
        //{
        //    string fname = string.Empty;
        //    string fileExtention = string.Empty;
        //    string motalityFile = string.Empty;

        //    HttpFileCollectionBase Motalityfiles = Request.Files;
        //    HttpPostedFileBase motalityfile = Motalityfiles[0];
        //    var motalityfilename = Motalityfiles[0].FileName;
        //    fileExtention = System.IO.Path.GetExtension(motalityfilename);
        //    string filenamepath = DateTime.Now.ToString("yyyy") + "/" + collection["HospitalCode"] + "/OverRideCode";

        //    string filename = "OC_" + collection["URN"].Substring(collection["URN"].Length - 6) + "_" + collection["MemberID"] + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;
        //    int result;

        //    PatientInfo patientInfo = new PatientInfo
        //    {

        //        MemberID = collection["MemberID"],
        //        URN = collection["URN"],
        //        NoofDays = collection["NoofDays"],
        //        UploadDoc = collection["UploadDoc"],
        //        UploadDoc1 = /*filenamepath + "/" +*/ filename,
        //        Remarks = collection["Description"],
        //        GENERATEDTHROUGH = collection["GENERATEDTHROUGH"],
        //        HospitalCode = collection["HospitalCode"],
        //        HospitalCategoryId = collection["HospitalCategoryId"],
        //        ACTIONCODE = collection["ActionCode"]
        //    };
        //    using (PatientDataServices dataServices = new PatientDataServices())
        //    {
        //        result = dataServices.GenerateOverrideRequestService(patientInfo);
        //        if (result == 2)
        //        {
        //            try
        //            {
        //                string dirPath = Server.MapPath("~/BSKY/" + filenamepath);
        //                if (!Directory.Exists(dirPath))
        //                {
        //                    Directory.CreateDirectory(dirPath);
        //                }
        //                string ActFileName = filename;
        //                fname = Path.Combine(dirPath, ActFileName);
        //                motalityfile.SaveAs(fname);

        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }
        //    }
        //    return Json(result);
        //}

        // [ValidateAntiForgeryToken]
        //public JsonResult GenerateOverrideRequest(FormCollection collection) // ADDED by Akshat (25-Jan-23)
        //{
        //    string fname = string.Empty;
        //    string fileExtention = string.Empty;
        //    string motalityFile = string.Empty;

        //    HttpFileCollectionBase Motalityfiles = Request.Files;
        //    HttpPostedFileBase motalityfile = Motalityfiles[0];
        //    var motalityfilename = Motalityfiles[0].FileName;
        //    fileExtention = System.IO.Path.GetExtension(motalityfilename);
        //    string filenamepath = DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MMMM") + "/" + collection["HospitalCode"] + "/OverRideRequestDocument";

        //    string filename = collection["GENERATEDTHROUGH"].Replace(",", "") + collection["MemberID"] + collection["HospitalCode"] + DateTime.Now.ToString("ddMMyy") + DateTime.Now.ToString("hhmmss") + fileExtention;
        //    int result;

        //    PatientInfo patientInfo = new PatientInfo
        //    {

        //        MemberID = collection["MemberID"],
        //        URN = collection["URN"],
        //        NoofDays = collection["NoofDays"],
        //        UploadDoc = collection["UploadDoc"],
        //        UploadDoc1 = filenamepath + "/" + filename,
        //        Remarks = collection["Description"],
        //        GENERATEDTHROUGH = collection["GENERATEDTHROUGH"],
        //        HospitalCode = collection["HospitalCode"],
        //        HospitalCategoryId = collection["HospitalCategoryId"],
        //        ACTIONCODE = collection["ActionCode"]
        //    };
        //    using (PatientDataServices dataServices = new PatientDataServices())
        //    {
        //        result = dataServices.GenerateOverrideRequestService(patientInfo);
        //        if (result == 2)
        //        {
        //            try
        //            {
        //                string dirPath = Server.MapPath("~/UploadDocument/" + filenamepath);
        //                if (!Directory.Exists(dirPath))
        //                {
        //                    Directory.CreateDirectory(dirPath);
        //                }
        //                string ActFileName = filename;
        //                fname = Path.Combine(dirPath, ActFileName);
        //                motalityfile.SaveAs(fname);

        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }
        //    }
        //    return Json(result);
        //}


        #region :: Kisan (01-02-23)
        public ActionResult OverrideRequest()
        {
            return View();
        }
        public ActionResult OverrideView()
        {
            return View();
        }

        #endregion

        public ActionResult ViewBlockingSlip()
        {
            return View();
        }

        #region "Add Pre-Auth"
        [HttpGet]
        public ActionResult BlockPreApprovalAdd(string fromdate, string todate, string hospitalcode, string actioncode,string querystatus)
        {
            if (fromdate == null && todate == null)
            {
                return View();
            }
            else
            {
                List<PreApprovalViewModel> objpackage = new List<PreApprovalViewModel>();
                using (PatientDataServices dataServices = new PatientDataServices())
                {
                    objpackage = dataServices.GetPreapprovalPackagesAddList(fromdate, todate, hospitalcode, actioncode, querystatus);
                }
                if (objpackage != null)
                {
                    return Json(new { data = objpackage }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { data = objpackage }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ShowSnaRemark(int packagedetailid)
        {
            TempData["packagedetailsid"] = packagedetailid;
            TempData.Keep();
            string action = "VQ";
            List<PreApprovalViewModel> objpackage = new List<PreApprovalViewModel>();
            using (PatientDataServices dataServices = new PatientDataServices())
            {
                objpackage = dataServices.GetSNARemarkById(packagedetailid, action);
            }
            if (objpackage != null)
            {
                objpackage[0].filepath = objpackage[0].uploadyear + "/" + objpackage[0].hospitalcode + "/PREAUTHDOC/" + objpackage[0].preauthdoc1;
               
                return Json(new { data = objpackage }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = objpackage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddDocumentPopUp(PreAuthAddDocuments model)
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
            string HopitalCode = Session["HospitalCode"].ToString();
            string fileExtention = string.Empty;
            
            HttpFileCollectionBase Motalityfiles;
            HttpPostedFileBase motalityfile;
            string filename;
            string filenamepath;
            string filename1;
            string photofilenamepath;
            Motalityfiles = Request.Files;
            if (Motalityfiles.Count != 0) {
                motalityfile = Motalityfiles[0];
                var motalityfilename = Motalityfiles[0].FileName;
                fileExtention = System.IO.Path.GetExtension(motalityfilename);
                filename = "PD_" + model.urn.Substring(model.urn.Length - 6) + "_" + model.memberid + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("hhmmssfff") + fileExtention;

                filenamepath = model.uploadyear + "/" + HopitalCode + "/" + "PREAUTHDOC/";
                if (Request.Files.Count > 0 && model.document2 != null)
                {

                    try
                    {
                        string dirUrl1 = ftp + filenamepath;
                        using (BinaryReader br = new BinaryReader(motalityfile.InputStream))
                        {
                            fileBytes = br.ReadBytes(motalityfile.ContentLength);
                        }

                        if (!DoesFtpDirectoryExist(dirUrl1))
                        {
                            CreateFTPDirectory(filenamepath);
                        }
                        string ActFileName1 = filename;
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl1 + ActFileName1);
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
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                }
                if (Request.Files.Count > 0 && model.document3 != null)
                {

                    try
                    {
                        string dirUrl1 = ftp + filenamepath;
                        using (BinaryReader br = new BinaryReader(motalityfile.InputStream))
                        {
                            fileBytes = br.ReadBytes(motalityfile.ContentLength);
                        }

                        if (!DoesFtpDirectoryExist(dirUrl1))
                        {
                            CreateFTPDirectory(filenamepath);
                        }
                        string ActFileName1 = filename;
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl1 + ActFileName1);
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
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                }
                


            //For file First(End)

                // For file First(start)

                int p_packagedetailsid = Convert.ToInt32(TempData["packagedetailsid"]);
                PreAuthAddDocuments adddetails = new PreAuthAddDocuments
                {
                    packagedetailsid = p_packagedetailsid,
                    document2 = model.document2 == null ? "" : filename,
                    document3 = model.document3 == null ? "" : filename,
                    replySecond = model.replySecond == null ? "" : model.replySecond,
                    replyThird = model.replyThird == null ? "" : model.replyThird
                };
                using (PatientDataServices dataServices = new PatientDataServices())
                {
                    result = dataServices.SubmitPreAuthReply(adddetails);
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
            else
            {
                int p_packagedetailsid = Convert.ToInt32(TempData["packagedetailsid"]);
                PreAuthAddDocuments adddetails = new PreAuthAddDocuments
                {
                    packagedetailsid = p_packagedetailsid,
                    document2 = null,
                    document3 = null,
                    replySecond = model.replySecond == null ? "" : model.replySecond,
                    replyThird = model.replyThird == null ? "" : model.replyThird
                };
                using (PatientDataServices dataServices = new PatientDataServices())
                {
                    result = dataServices.SubmitPreAuthReply(adddetails);
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
                return Json(result, JsonRequestBehavior.AllowGet);
        }
        public FileResult DownloadFile()
        {
            string webRootPath = "~/UploadDocument/QueryDocument";
            var FileVirtualPath = Path.Combine(webRootPath, "document", "document.pdf");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(FileVirtualPath);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", "document.pdf");
        }
        #endregion

        //[HttpGet]
        //public async Task<ActionResult> ViewBlockingSlip(BlockPackageSlip obj)
        //{
        //    BlockSlipModel Viewblockpackagelist = new BlockSlipModel();
        //    using (var PatientDataServices = new PatientDataServices())
        //    {
        //        Viewblockpackagelist.blockpackageslip = (List<BlockPackageSlip>)await Task.FromResult(PatientDataServices.GetViewBlockPackageSlip(obj));
        //        Viewblockpackagelist.implantdata = (List<ImplantData>)await Task.FromResult(PatientDataServices.GetViewBlockPackageImplantDataSlip(obj));
        //        Viewblockpackagelist.highendrug = (List<HighenDrug>)await Task.FromResult(PatientDataServices.GetViewBlockPackageHighendSlip(obj));
        //        return PartialView("ViewBlockingSlip", Viewblockpackagelist);

        //    }
        //}
        public ActionResult ViewHospitalSlip()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<ActionResult> PriviousBlockPackafeDtls(PriviousBlockpackageDetails obj)
        {

            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.GetPriviouspackagedetails(obj));
                return Json(data);
            }
        }
        public FileResult Downloaddocfile(string hospitalcode, string filename, string urn, string dateofadmision)
        {
            var pathname=  Convert.ToDateTime(dateofadmision).Year + "/" + hospitalcode + "/" + "PREAUTHDOC/"+ filename;

            //FTP Server URL.
            //string ftp = "ftp://192.168.10.76/";
            //Create FTP Request.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["FTPURL"] + pathname);
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
        public FileResult downloadphotofile(string hospitalcode, string filename, string urn, string dateofadmision)
        {
            var pathname = Convert.ToDateTime(dateofadmision).Year + "/" + hospitalcode + "/" + "PatientPic/" + filename;
            //FTP Server URL.
            //string ftp = "ftp://192.168.10.76/";
            //Create FTP Request.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["FTPURL"] + pathname);
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
