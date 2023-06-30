using HPSBYS.Data.Model;
using HPSBYS.Data.Services;
using HPSBYS.Web.Models;
using BCrypt;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using HPSBYS.Web.Fiilters;
using HPSBYS.Web.Filters;
using System.Drawing;
using System.Text;
using System.Net.NetworkInformation;
using System.Management;

namespace HPSBYS.Web.Controllers
{

    public class DashboardController : Controller
    {

        HttpClient client;
        string ServiceURL = ConfigurationManager.AppSettings["ServiceURL"];
        string WebURL = ConfigurationManager.AppSettings["ShortcutKeyURL"];
        ILogger log = LogManager.GetCurrentClassLogger();
        string result = string.Empty;

        // GET: Dashboard
        [HttpGet]
        [SessionTimeOutFilter]
        [Authorize]
        public ActionResult Index()
        {
            string groupid = Convert.ToString(Session["groupid"]);
            if (groupid != "1")
            {
                return View();
            }
            else
            {
                return RedirectToAction("adminIndex", "Dashboard");
            }
        }

        [HttpGet]
        [SessionTimeOutFilter]
        [Authorize]
        public ViewResult adminIndex()  // For admin view
        {
            return View();
        }

        [HttpGet]
        public ViewResult PackageChangeIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            //string macid = GetMACAddress();
            //ViewBag.macAddress = macid;
            return View();
        }

        [Obsolete("For API based Login, Use ValidateLogin instead", true)] // (New) Added by Akshat (18 Jan 23)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ValidateUserLogin(FormCollection collection)
        {
            Login LoginInfo = new Login
            {
                USERID = collection["UserID"],
                USERNAME = collection["UserName"],

                HospitalCode = collection["HospitalCode"],
                HospitalName = collection["HospitalName"],
                HospitalDistName = collection["UserID"],
                RegBackMonth = Convert.ToInt32(collection["RegBackMonth"]),
                LastUpdateDate = collection["LastUpdateDate"],
            };

            FormsAuthentication.SetAuthCookie(LoginInfo.USERNAME, false);
            Session["UserID"] = LoginInfo.USERID;
            Session["UserName"] = LoginInfo.USERNAME;
            Session["Password"] = LoginInfo.Password;
            Session["HospitalName"] = LoginInfo.HospitalName;
            Session["HospitalDistName"] = LoginInfo.HospitalDistName;
            Session["HospitalCode"] = LoginInfo.HospitalCode;
            Session["RegBackMonth"] = LoginInfo.RegBackMonth;
            Session["Hospitaldistrictcode"] = LoginInfo.Hospitaldistrictcode;
            Session["HospitalStateCode"] = LoginInfo.STATECODE;
            Session["HospitalauthorityId"] = LoginInfo.HOSPITALAUTHORITYID;

            if (Convert.ToInt32(LoginInfo.LastUpdateDate) >= 6)
            {
                Session["PwdUpdateStatus"] = true;
                return Json(105);
            }
            else
            {
                Session["PwdUpdateStatus"] = false;
                return Json(100);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ValidateLogin(FormCollection collection) // For Db based login
        {
            //string macid = GetMACAddress();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));// Added on 23-05-2023 for security perpose. Session will refresh after every action.
            Login result = new Login();
            if (Convert.ToInt32(collection["Captcha"]) == Convert.ToInt32(Session["CAPTCHA"]))
            {
                Login LoginInfo = new Login
                {
                    USERNAME = collection["UserID"],
                    Password = collection["Password"]
                };
                using (var obj = new CommonDataServices())
                {
                    result = obj.GetLogin(LoginInfo.USERNAME, null);
                }
                //if (result.STATUS != 404)
                //{
                //    bool statusIsValid = BCrypt.Net.BCrypt.Verify(CommonExtension.DecryptStringAES(LoginInfo.Password), result.USERPASSWORD);
                //    if (statusIsValid)
                //    {
                //        return Json(result);
                //    }
                //}
                if (result.STATUS == 100)
                {
                    if (result.attempted_status < 3)
                    {
                        bool statusIsValid = BCrypt.Net.BCrypt.Verify(CommonExtension.DecryptStringAES(LoginInfo.Password), result.USERPASSWORD);
                        if (statusIsValid)
                        {
                            if (result.attempted_status > 0)
                            {
                                using (var obj = new CommonDataServices())
                                {
                                    var newresult = obj.UpdateLoginAttempt(LoginInfo.USERNAME, "C");
                                }
                            }
                            return Json(result);
                        }
                        else
                        {
                            using (var obj = new CommonDataServices())
                            {
                                var newresult = obj.UpdateLoginAttempt(LoginInfo.USERNAME, "B");
                                return Json(newresult);
                            }
                        }
                    }
                    return Json(504);
                }
                return Json(404);
            }
            else if (Convert.ToInt32(collection["Captcha"]) != Convert.ToInt32(Session["CAPTCHA"]))
            {
                return Json("InvalidCaptcha");
            }
            else
            {
                return Json("InvalidCaptcha");
            }
        }
        #region :: this functionaly hide  by kisan 
        //            if (result.STATUS != 404)
        //                {
        //                    bool statusIsValid = BCrypt.Net.BCrypt.Verify(CommonExtension.DecryptStringAES(LoginInfo.Password), result.USERPASSWORD);
        //                    if (statusIsValid)
        //                    {
        //                        return Json(result);
        //    }

        //                    if (result.attempted_status != 3)
        //                    {
        //                        bool statusIsValid = BCrypt.Net.BCrypt.Verify(CommonExtension.DecryptStringAES(LoginInfo.Password), result.USERPASSWORD);
        //                        if (statusIsValid)
        //                        {
        //                            using (var obj = new CommonDataServices())
        //                            {
        //                                var newresult = obj.UpdateLoginAttempt(LoginInfo.USERNAME, "C");
        //                                return Json(result);
        //}
        //                        }
        //                        else
        //{
        //    using (var obj = new CommonDataServices())
        //    {
        //        result = obj.UpdateLoginAttempt(LoginInfo.USERNAME, "B");
        //        return Json(result);
        //    }
        //}
        //                    }
        //                    else
        //{
        //    result.STATUS = 504;
        //    return Json(result);
        //}

        //                }
        //                else
        //{
        //    using (var obj = new CommonDataServices())
        //    {
        //        result = obj.UpdateLoginAttempt(LoginInfo.USERNAME, "B");
        //        return Json(result);
        //    }
        //}
        //                return Json(result);
        //            }
        //            else if (Convert.ToInt32(collection["Captcha"]) != Convert.ToInt32(Session["CAPTCHA"]))
        //{
        //    return Json("InvalidCaptcha");
        //}
        //else
        //{
        //    return Json("InvalidCaptcha");
        //}
        //        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SetUserLoginSession(FormCollection collection)  // For db based login, (New) Added by Akshat (20 Jan 23)
        {
            FormsAuthentication.SetAuthCookie(collection["USERNAME"], false);
            Session["UserID"] = collection["USERID"];
            Session["UserName"] = collection["USERNAME"];
            Session["Password"] = collection["Password"];
            Session["HOSPITALSTATE"] = collection["HOSPITALSTATE"];
            Session["STATECODE"] = collection["STATECODE"];
            Session["HospitalDistName"] = collection["HospitalDistName"];
            Session["Hospitaldistrictcode"] = collection["Hospitaldistrictcode"];
            Session["HospitalName"] = collection["HospitalName"];
            Session["HospitalCode"] = collection["HospitalCode"];
            Session["RegBackMonth"] = collection["RegBackMonth"];
            Session["HospitalCategoryId"] = collection["HospitalCategoryId"];
            Session["HospitalauthorityId"] = collection["HospitalauthorityId"];
            Session["exceptionhospital"] = collection["exceptionhospital"];
            Session["moustatus"] = collection["moustatus"];
            Session["empanelmentstatus_flag"] = collection["empanelmentstatus_flag"];
            Session["mou_startdate"] = collection["mou_startdate"];
            Session["mou_enddate"] = collection["mou_enddate"];
            Session["mouleftdays"] = collection["mouleftdays"];
            Session["mounoticeflag"] = collection["mounoticeflag"];
            Session["backdate_admission"] = collection["backdate_admission"];
            Session["backdate_discharge"] = collection["backdate_discharge"];
            Session["is_block_active"] = collection["is_block_active"];
            Session["groupid"] = collection["groupid"];

            if (Convert.ToInt32(collection["LastUpdateDate"]) >= 6)
            {
                Session["PwdUpdateStatus"] = true;
                return Json(105);
            }
            else
            {
                Session["PwdUpdateStatus"] = false;
                return Json(100);
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            LogoutCleanUp();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Dashboard");
        }
        [HttpGet]
        public ViewResult ChangePassword()
        {
            return View();
        }
        [HttpGet]
        public ViewResult SessionRedirect()
        {
            LogoutCleanUp();
            return View();
        }
        [NonAction]
        public void LogoutCleanUp()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }
        [HttpGet]
        public ViewResult ViewLog()
        {
            return View();
        }
        [HttpGet]
        public FileResult ReadLog(string Date, string Type)
        {
            string date = Convert.ToDateTime(Date).ToString("yyyy-MM-dd");
            string file = string.Empty;
            string fileNm = string.Empty;
            string contentType = string.Empty;
            if (Type == "W")
            {
                file = "/logs/" + date + ".txt";
                fileNm = Server.MapPath("~" + file);
                contentType = "text/plain";
            }
            return File(fileNm, contentType, Path.GetFileName(fileNm));
        }
        [HttpGet]
        public ActionResult AddNotice()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddNotice(FormCollection collection)
        {
            string ActFileName = string.Empty;
            string FileName = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    int Year = DateTime.Now.Year;
                    string Month = DateTime.Now.ToString("MMMM");
                    // string HopitalCode = Session["HospitalCode"].ToString();
                    string dirUrl = "~/UploadDocument/" + Year + "/" + Month + "/Notices";
                    string dirPath = Server.MapPath(dirUrl);
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    var fileName = Path.GetFileName(files[0].FileName);
                    var newGuid = Guid.NewGuid();
                    string SubGuid = newGuid.ToString().Substring(0, 15);
                    ActFileName = SubGuid + "_" + fileName;
                    var path = Path.Combine(Server.MapPath(dirUrl), ActFileName);
                    files[0].SaveAs(path);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    return Json("Error");

                }
            }
            Notice notice = new Notice
            {
                StartDate = collection["StartDate"],
                EndDate = collection["EndDate"],
                Notices = collection["Notice"],
                NoticeDocument = ActFileName,
            };
            using (PatientDataServices dataServices = new PatientDataServices())
            {
                result = dataServices.addNotice(notice);
            }
            if (result == "2")
            {
                return Json("sucess");
            }
            else
            {
                return Json("failed");
            }
        }
        [HttpGet]
        public ActionResult ViewNotice()
        {
            return View();
        }



        //For Create Captch
        [HttpPost]
        public JsonResult loadCaptcha()
        {
            System.Random rand = new Random((int)DateTime.Now.Ticks);
            int num1 = rand.Next(1, 10);
            int num2 = rand.Next(1, 10);
            int total = num1 + num2;
            Session["CAPTCHA"] = total.ToString();
            //StringBuilder randomText = new StringBuilder();
            //string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            //Random r = new Random();
            //for (int j = 0; j <= 5; j++)
            //  {
            //     randomText.Append(alphabets[r.Next(alphabets.Length)]);
            //  }
            //Session["CAPTCHA"] = randomText.ToString();
            var result = new { data = num1.ToString(), data2 = num2.ToString() };
            return Json(result, JsonRequestBehavior.AllowGet);
            // return Json();
        }

        private static string GetMACAddress()
        {
            string MacAddress = string.Empty;

            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

                // MacAddress = Convert.ToString(nics[0].GetPhysicalAddress());
                foreach (NetworkInterface adapter in nics)
                {
                    if (MacAddress == String.Empty)// only return MAC Address from first card  
                    {
                        //IPInterfaceProperties properties = adapter.GetIPProperties(); Line is not required
                        MacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }
                return MacAddress;
            }
            catch (ArgumentNullException Exc)
            {

                return MacAddress;
            }
        }


        private static string AddressBytesToString(byte[] addressBytes)
        {
            return string.Join(":", (from b in addressBytes
                                     select b.ToString("X2")).ToArray());
        }
    }
}