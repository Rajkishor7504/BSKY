using HPSBYS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HPSBYS.Data.Services;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
//using Microsoft.AspNet.SignalR.Client.Http;

namespace HPSBYS.WebAPI.Controllers
{
    public class BlockPackageController : ApiController
    {
        [HttpPost]
        public async Task<string> addPatientBlockPackage(PatientInfo obj)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                return await Task.FromResult(PatientDataServices.addPatientBlockPackage(obj));
            }

        }

        [HttpPost]
        public async Task<string> addpreAuthPackage(PreAuthApprovedPackageBlock obj)
        {
            using (var PreAuthApprovedPackageBlock = new PatientDataServices())
            {
                return await Task.FromResult(PreAuthApprovedPackageBlock.PreAuthPackageBlock(obj));
            }

        }
        [HttpPost]
        public async Task<string> addWardpreAuthPackage(PreAuthApprovedPackageBlock obj)
        {
            using (var PreAuthApprovedPackageBlock = new PatientDataServices())
            {
                return await Task.FromResult(PreAuthApprovedPackageBlock.PreWardAuthPackageBlock(obj));
            }

        }
        [HttpPost]
        public async Task<string> addCancelPackage(CancelPackage obj)
        {
            using (var patientDataServices = new PatientDataServices())
            {
                return await Task.FromResult(patientDataServices.addCancelPackage(obj));
            }

        }
        [HttpPost]
        public async Task<string> addWardCancelPackage(CancelPackage obj)
        {
            using (var patientDataServices = new PatientDataServices())
            {
                return await Task.FromResult(patientDataServices.addWardCancelPackage(obj));
            }

        }
        [HttpPost]
        public async Task<string> addAddOnCancelPackage(CancelPackage obj)
        {
            using (var patientDataServices = new PatientDataServices())
            {
                return await Task.FromResult(patientDataServices.addAddOnCancelPackage(obj));
            }

        }
        [HttpPost]
        public async Task<string> addAddOnpreAuthPackage(PreAuthApprovedPackageBlock obj)
        {
            using (var PreAuthApprovedPackageBlock = new PatientDataServices())
            {
                return await Task.FromResult(PreAuthApprovedPackageBlock.AddOnPreAuthPackageBlock(obj));
            }

        }

        //TMS 2.0 Block Package Insertion
        [HttpPost]
        public async Task<IHttpActionResult> addPatientBlockPackageInsertion(PatientInfo obj)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.addPatientBlockPackageInsert(obj));
                return Ok(data);
            }
        }
        //END OF TMS 2.0 Block Package Insertion

        [HttpPost]
        public async Task<List<PatientInfo>> GetOverrideCodeList(PatientInfo obj) // ADDED by Akshat (25-Jan-23)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                return await Task.FromResult(PatientDataServices.GetOverrideCodeListService(obj));
            }
        }


        [HttpPost]
        public async Task<List<ViewBlockPackageDetailsModel>> GetViewBlockpackagedetailsList(ViewBlockPackageDetailsModel obj) // ADDED by Rajkishor patra (07-feb-23)
        {

            using (var PatientDataServices = new PatientDataServices())
            {
                if (obj.groupid != "1")
                {
                    var data = await Task.FromResult(PatientDataServices.GetViewBlockPackageDetails(obj));
                    return data.ToList();
                }
                else
                {
                    var data = await Task.FromResult(PatientDataServices.GetadminViewBlockPackageDetails(obj));
                    return data.ToList();
                }

            }
        }

        [HttpPost]
            public async Task<List<ViewBlockPackageDetailsModel>> GetViewBlockpackagedetailsListByID(ViewBlockPackageDetailsModel obj) // ADDED by Rajkishor patra (08-feb-23)
            {
            using (var PatientDataServices = new PatientDataServices())
            {
                if (obj.groupid != "1")
                {
                    var data = await Task.FromResult(PatientDataServices.GetViewBlockPackageDetailsById(obj));
                    return data.ToList();
                }
                else
                {
                    var data = await Task.FromResult(PatientDataServices.GetadminViewBlockPackageDetailsById(obj));
                    return data.ToList();
                }
            }
        }

        //[HttpPost]
        //public async Task<List<ViewBlockPackageDetailsModel>> GetPackageDetailsListByID(ViewBlockPackageDetailsModel obj) // ADDED by Rajkishor patra (08-feb-23)
        //{
        //    using (var PatientDataServices = new PatientDataServices())
        //    {
        //        var PackageDetails = await Task.FromResult(PatientDataServices.GetViewpackageDetailsById(obj));
        //        return PackageDetails.ToList();
        //    }
        //}
        [HttpPost]
        public async Task<IHttpActionResult> GetPackageDetailsListByID(ViewBlockPackageDetailsModel obj) // ADDED by Rajkishor patra (08-feb-23)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                if (obj.groupid != "1")
                {
                    var data = await Task.FromResult(PatientDataServices.GetViewpackageDetailsById(obj));
                    return Ok(data);
                }
                else
                {
                    var data = await Task.FromResult(PatientDataServices.GetpackageadminViewById(obj));
                    return Ok(data);
                }
            }
        }




        [HttpPost]
        public async Task<List<Admissiondetailsmodel>> GetAdmissionDetailsListByID(ViewBlockPackageDetailsModel obj) // ADDED by Rajkishor patra (08-feb-23)
        {

            using (var PatientDataServices = new PatientDataServices())
            {

                var AdmissionDetails = await Task.FromResult(PatientDataServices.GetViewAdmissionDetailsById(obj));
                return AdmissionDetails.ToList();
            }
        }
        [HttpPost]
        public async Task<List<ViewBlockPackageDetailsModel>> GetVitalparameterListByID(ViewBlockPackageDetailsModel obj) // ADDED by Rajkishor patra (08-feb-23)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                if (obj.groupid != "1")
                {
                    var vitalaparameter = await Task.FromResult(PatientDataServices.GetViewVitalParameterById(obj));

                    return vitalaparameter.ToList();
                }
                else
                {
                    var vitalaparameter = await Task.FromResult(PatientDataServices.GetadminViewVitalParameterById(obj));

                    return vitalaparameter.ToList();
                }
            }
        }

        [HttpPost]//Add By Rajkishor Patra(20-feb-2023)
        public async Task<List<ViewBlockPackageDetailsModel>> GetImplantDetailsListByID(ViewBlockPackageDetailsModel obj)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var vitalaparameter = await Task.FromResult(PatientDataServices.GetImplantDetailsListByID(obj));

                return vitalaparameter.ToList();
            }
        }
        [HttpPost]//Add By Rajkishor Patra(20-feb-2023)
        public async Task<List<ViewBlockPackageDetailsModel>> GetHighendDrugDetailsListByID(ViewBlockPackageDetailsModel obj)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var vitalaparameter = await Task.FromResult(PatientDataServices.GetHighendDrugDetailsListByID(obj));

                return vitalaparameter.ToList();
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> ViewBlockingSlip(BlockPackageSlip obj)
        {
            BlockSlipModel Viewblockpackagelist = new BlockSlipModel();
            using (var PatientDataServices = new PatientDataServices())
            {
                Viewblockpackagelist.blockpackageslip = (List<BlockPackageSlip>)await Task.FromResult(PatientDataServices.GetViewBlockPackageSlip(obj));
                Viewblockpackagelist.packagedetailsdata = (List<PackagedetailsSlipdata>)await Task.FromResult(PatientDataServices.GetViewBlockPackagedetailsSlip(obj));
                Viewblockpackagelist.implantdata = (List<ImplantData>)await Task.FromResult(PatientDataServices.GetViewBlockPackageImplantDataSlip(obj));
                Viewblockpackagelist.highendrug = (List<HighenDrug>)await Task.FromResult(PatientDataServices.GetViewBlockPackageHighendSlip(obj));
                //return PartialView("ViewBlockingSlip", Viewblockpackagelist);
                return Json(Viewblockpackagelist);
            }
        }

        private async Task<object> GenerateOTP()
        {
            string numbers = "0123456789";
            Random objrandom = new Random();
            string strrandom = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                int temp = objrandom.Next(0, numbers.Length);
                strrandom += temp;
            }
            return strrandom;
        }

        [HttpPost]
        public async Task<IHttpActionResult> SendOTPWithPatientNumber(SendOtp obj)
        {
            var data = "";
            var otp = GenerateOTP();
            obj.action = "sendOTPSMS";
            obj.department_id = "D006001";
            obj.template_id = "1007480143063815155";
            obj.OTP = otp.Result.ToString();
            //obj.sms_content = "Dear User, Your One Time Password (OTP) for Forgot Password is " + otp.Result + ". Dont share it with anyone. BSKY, Govt. of Odisha.";
            obj.sms_content = "Dear User, Your One Time Password (OTP) for patient verification is " + otp.Result + ". Dont share it with anyone. BSKY, Govt. of Odisha.";
            if (!string.IsNullOrEmpty(obj.phonenumber))
            {
                using (var PatientDataServices = new PatientDataServices())
                {
                    var Dataresult = await Task.FromResult(PatientDataServices.InsertOTP(obj));
                    if (Dataresult == "-1")
                    {
                        using (var client = new HttpClient())
                        {
                            var dataContent = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string, string>("action", "sendOTPSMS"),
                                new KeyValuePair<string, string>("department_id", "D006001"),
                                new KeyValuePair<string, string>("template_id", "1007480143063815155"),
                                new KeyValuePair<string, string>("sms_content", obj.sms_content),
                                new KeyValuePair<string, string>("phonenumber", obj.phonenumber),
                            });

                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            var url = "https://govtsms.odisha.gov.in/api/api.php";// USER FOR DEVELOPMENT  
                            HttpResponseMessage response = await client.PostAsync(url, dataContent);
                            var result = response.Content.ReadAsStringAsync().Result;
                            Root resultdata = JsonConvert.DeserializeObject<Root>(result);
                            if (resultdata.status == "1")
                            {
                                data = resultdata.status;
                            }
                            else
                            {
                                data = resultdata.status;
                            }
                        }
                    }
                }
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IHttpActionResult> VerifyOTPWithPatientNumber(SendOtp obj)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.verifypatientOTP(obj));
                return Ok(data);
            }
        }
        //public async Task<object> SendOTPWithPatientNumber(string number)
        //{
        //    var data = 0;
        //    var  otp = GenerateOTP();           
        //    string message = "Dear User, Your One Time Password (OTP) for Forgot Password is " + otp.Result + ". Don’t share it with anyone. BSKY, Govt. of Odisha";

        //    if (!string.IsNullOrEmpty(number))
        //    {
        //        using (var PatientDataServices = new PatientDataServices())
        //        {
        //            data = 1;//await Task.FromResult(PatientDataServices.InsertOTP(string hospitalcode,string ));
        //            if (data == 1)
        //            {
        //                using (var client = new HttpClient())
        //                {
        //                    //Random r = new Random();
        //                    //string OTP = r.Next(100000, 999999).ToString();

        //                    ////Send message
        //                    //string Username = "youremail@domain.com";
        //                    //string APIKey = "YourHash";
        //                    //string SenderName = "ODIGOV";
        //                    //string Number = "9778496013";
        //                    //string Message = "Dear User, Your One Time Password (OTP) for Forgot Password is " + OTP + ". Don’t share it with anyone. BSKY, Govt. of Odisha";
        //                    //string URL = "https://govtsms.odisha.gov.in/api/api.php" + Username + "&hash=" + APIKey + "&sender=" + SenderName + "&numbers=" + Number + "&message=" + Message;
        //                    //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
        //                    //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        //                    //StreamReader sr = new StreamReader(resp.GetResponseStream());
        //                    //string results = sr.ReadToEnd();
        //                    //sr.Close();

        //                    //Session["OTP"] = OTP;
        //                    //Redirect for varification
        //                    //Response.Redirect("VerifyOTP.aspx");

        //                    //try
        //                    //{
        //                    //    HttpClient clientt = new HttpClient();
        //                    //    httpo post = new HttpPost("https://govtsms.odisha.gov.in/api/api.php");
        //                    //    List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(1);
        //                    //    nameValuePairs.add(new BasicNameValuePair("action", "singleSMS"));
        //                    //    nameValuePairs.add(new BasicNameValuePair("department_id", "D006001"));
        //                    //    nameValuePairs.add(new BasicNameValuePair("template_id", "1007480143063815155"));
        //                    //    nameValuePairs.add(new BasicNameValuePair("sms_content", message));
        //                    //    nameValuePairs.add(new BasicNameValuePair("phonenumber", mobileNo));
        //                    //    post.setEntity(new UrlEncodedFormEntity(nameValuePairs));
        //                    //    HttpResponse response = client.execute(post);
        //                    //    BufferedReader bf = new BufferedReader(new InputStreamReader(response.getEntity().getContent()));
        //                    //    String line = "";
        //                    //    while ((line = bf.readLine()) != null)
        //                    //    {
        //                    //        responseString = line;

        //                    //    }
        //                    //    return responseString;
        //                    //}
        //                    //catch (UnsupportedEncodingException e)
        //                    //{
        //                    //    // TODO Auto-generated catch block
        //                    //    e.printStackTrace();
        //                    //}
        //                    //catch (ClientProtocolException e)
        //                    //{
        //                    //    // TODO Auto-generated catch block
        //                    //    e.printStackTrace();
        //                    //}
        //                    //catch (IOException e)
        //                    //{
        //                    //    // TODO Auto-generated catch block
        //                    //    e.printStackTrace();
        //                    //}
        //                    //catch (Exception e)
        //                    //{
        //                    //    // TODO Auto-generated catch block
        //                    //    e.printStackTrace();
        //                    //}
        //                    //return responseString;
        //                }
        //            }
        //        } 
        //    }
        //    return Ok(data);
        //}


        [HttpPost]
        public async Task<IHttpActionResult> ViewHospitalSlip(BlockPackageSlip obj)
        {
            BlockSlipModel Viewblockpackagelist = new BlockSlipModel();
            using (var PatientDataServices = new PatientDataServices())
            {
                Viewblockpackagelist.blockpackageslip = (List<BlockPackageSlip>)await Task.FromResult(PatientDataServices.GetViewBlockPackageSlip(obj));
                Viewblockpackagelist.packagedetailsdata = (List<PackagedetailsSlipdata>)await Task.FromResult(PatientDataServices.GetViewBlockPackagedetailsSlip(obj));
                Viewblockpackagelist.implantdata = (List<ImplantData>)await Task.FromResult(PatientDataServices.GetViewBlockPackageImplantDataSlip(obj));
                Viewblockpackagelist.highendrug = (List<HighenDrug>)await Task.FromResult(PatientDataServices.GetViewBlockPackageHighendSlip(obj));
                //return PartialView("ViewHospitalSlip", Viewblockpackagelist);
                return Json(Viewblockpackagelist);
            }
        }


        [HttpPost]
        public async Task<List<ReportListModel>> GetHospitalDetailsReport(ReportListModel obj)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                if (obj.groupid != "1")
                {
                    var gethospitalpatiantdetails = await Task.FromResult(PatientDataServices.GetViewReportList(obj));
                    return gethospitalpatiantdetails.ToList();
                }
                else
                {
                    var gethospitalpatiantdetails = await Task.FromResult(PatientDataServices.GetadminViewReportList(obj));
                    return gethospitalpatiantdetails.ToList();
                }

            }
        }

        //[HttpPost]
        //public async Task<List<ViewBlockPackageDetailsModel>> Getpreauthdetails(ViewBlockPackageDetailsModel obj) // ADDED by Rajkishor patra (05-april-23)
        //{
        //    using (var PatientDataServices = new PatientDataServices())
        //    {
        //        var data = await Task.FromResult(PatientDataServices.GetViewBlockPackageDetailsById(obj));
        //        return data.ToList();
        //    }
        //}

        [HttpPost]
        public async Task<IHttpActionResult> GetPreAuthDetailsListByID(PreauthViewdetails obj) // ADDED by Rajkishor patra (08-feb-23)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.GetPreAuthViewDetailsById(obj));
                return Ok(data);
            }
        }
    }
}
