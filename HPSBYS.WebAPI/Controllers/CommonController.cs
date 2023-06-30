using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using HPSBYS.Data.Model;
using HPSBYS.Data.Services;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace HPSBYS.WebAPI.Controllers
{
    public class CommonController : ApiController
    {
        [HttpGet]
        public async Task<IList<Scheme>> GetSchemeList(int Schemecode)
        {
            using (var SchemeInformation = new CommonDataServices())
            {
                return await Task.FromResult(SchemeInformation.GetScheme(Schemecode));
            }
        }
        [HttpGet]
        public async Task<IList<PACKAGECATEGORY>> GetPackageCategory(string Action)
        {
            using (var CATEGORYdata = new CommonDataServices())
            {
                return await Task.FromResult(CATEGORYdata.GetPACKAGECATEGORY(Action));
            }
        }
        [HttpGet]
        public async Task<IList<PACKAGECATEGORY>> GetPackageCategory_PackageChange(string Action)
        {
            using (var CATEGORYdata = new CommonDataServices())
            {
                return await Task.FromResult(CATEGORYdata.GetPACKAGECATEGORY_PackageChange(Action));
            }
        }
        [HttpGet]
        public async Task<IList<SUBPACKAGECATEGORY>> SubGetPackage(string Action, string PackageCategoryCode)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetSubPackageDetail(Action, PackageCategoryCode));
            }
        }
        [HttpGet]
        public async Task<IList<SUBPACKAGECATEGORY>> SubGetPackage_PackageChange(string Action, string PackageCategoryCode)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetSubPackageDetail_PackageChange(Action, PackageCategoryCode));
            }
        }
        [HttpGet]
        public async Task<IList<PackageInformation>> GetPackage_PackageChange(string Action, string PackageCategoryCode, string PackageSubCategoryCode)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetPackageDetail_PackageChange(Action, PackageCategoryCode, PackageSubCategoryCode));
            }
        }
        [HttpGet]
        public async Task<IList<PackageInformation>> GetPackage(string Action, string PackageCategoryCode, string PackageSubCategoryCode)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetPackageDetail(Action, PackageCategoryCode, PackageSubCategoryCode));
            }
        }
        [HttpGet]
        public async Task<IList<WardDetail>> GetWardDetails(string Action, string PreAuthStatus)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetWardDetails(Action, PreAuthStatus));
            }
        }
        //For  Policy Details

        [HttpGet]
        public async Task<IList<PolicyDetails>> GetPolicy(string URN)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetInsuranceDetail(URN));
            }
        }

        //For UnblockingReason

        [HttpGet]
        public async Task<IList<UnblockingReason>> GetUnblockingReason(string Action)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetUnblockingReason(Action));
            }
        }

        //For Login

        [HttpPost]
        public async Task<Login> GetLoginInformation(Login login)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetLogin(login.USERNAME,""));/*, login.Password*/
            }
        }

        [HttpGet]
        public async Task<BalanceInfo> GetAvailBalance(string Action, string URN, string FamilyId)
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getBalanceInfo(Action, URN, FamilyId));
            }
        }
        [HttpPost]
        public async Task<string> changePassword(Login login)
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.changePassword(login));
            }
        }

        [HttpGet]
        public async Task<IList<Notice>> getNotices(string Action)
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getNotices(Action));
            }
        }

        [HttpGet]
        public async Task<PatientStats> getPatientStats(string Action, string HospitalCode)
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getPatientStats(Action, HospitalCode));
            }
        }

        [HttpGet]
        public async Task<IList<Dist>> GetAllDist(string Action)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getAllDist(Action));
            }
        }
        [HttpGet]
        public async Task<IList<Block>> GetAllBlockByDistCode(string Action, int DistCode)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getAllBlockByDistCode(Action, DistCode));
            }
        }
        [HttpGet]
        public async Task<IList<PHC>> GetAllPHCByDistCodeAndBlockCode(string Action, int DistCode, int BlockCode)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getAllPHCByDistCodeAndBlockCode(Action, DistCode, BlockCode));
            }
        }
        [HttpGet]
        public async Task<IList<SubCentre>> GetAllSubCentreByDistBlockAndPHCCode(string Action, int DistCode, int BlockCode, int PHCCode)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getAllSubCentreByDistBlockAndPHCCode(Action, DistCode, BlockCode, PHCCode));
            }
        }
        [HttpGet]
        public async Task<IList<DownwardReferalInfo>> getDownwardReferalByBlockingInvoiceNo(string Action, string InvoiceNo)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getDownwardReferalByBlockingInvoiceNo(Action, InvoiceNo));
            }
        }

        [Route("api/Common/GetPOSLoginInformationOld")]
        [HttpGet]
        public async Task<POCLogin> GetPOSLoginInformationOld(userloginmodel login)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetPOSLogin(login.username, login.password));
            }
        }

        [Route("api/Common/GetURNInfoListOld")]
        [HttpPost]
        public async Task<IList<URNInformation>> GetURNInfoListOld(userURNmodel info)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetURNINFormation(info.URN));
            }
        }

        //public JsonResult ValidateLogin(FormCollection collection)
        //{
        //    Login result = new Login();
        //    Login LoginInfo = new Login
        //    {
        //        USERNAME = collection["UserID"],
        //        Password = collection["Password"]
        //    };
        //    using (var obj = new CommonDataServices())
        //    {
        //        result = obj.GetLogin(LoginInfo.USERNAME, LoginInfo.Password);
        //    }          
        //    FormsAuthentication.SetAuthCookie(LoginInfo.USERNAME, false);
        //    Session["UserID"] = result.USERID;
        //    Session["UserName"] = LoginInfo.USERNAME;
        //    Session["HospitalName"] = result.HospitalName;
        //    Session["HospitalDistName"] = result.HospitalDistName;
        //    Session["HospitalCode"] = result.HospitalCode;
        //    Session["RegBackMonth"] = result.RegBackMonth;

        //    if (Convert.ToInt32(result.LastUpdateDate) >= 6)
        //    {
        //        Session["PwdUpdateStatus"] = true;
        //        return Json(105);
        //    }
        //    else
        //    {
        //        Session["PwdUpdateStatus"] = false;
        //        return Json(result.STATUS);
        //    }
        //}

        [HttpGet]
        public IHttpActionResult DemoTest()
        {
            return Ok(true);
        }

        //Added by Niranjan Poddar
        //For Vital Parameter

        [HttpGet]
        public async Task<IList<VitalParameter>> GetVitalParameter(string Action)
        {
            using (var CATEGORYdata = new CommonDataServices())
            {
                return await Task.FromResult(CATEGORYdata.GetVitalParameters(Action));
            }
        }

        #region :: Kisan Code

        [Route("api/Common/GetTokenInfo")]
        [HttpGet]
        public async Task<GetTokenDto> GetTokenInfo()
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getapitoken());
            }
        }

        [Route("api/Common/GetPOSLoginInformation")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPOSLoginInformation(userloginmodel login)
        {
            IGenericResult<bool> result = new GenericResult<bool>();
            using (var obj = new CommonDataServices())
            {
                var loginData = await Task.FromResult(obj.GetPOSLogin(login.username, login.password));
                if (loginData.userid > 0)
                {
                    var jwtSecurityToken = GetToken(loginData);
                    loginData.tokenkey = jwtSecurityToken.ToString();

                    result.Status = true;
                    result.Message = "Log in successful.";
                    result.Code = 200;
                    result.Data = loginData;
                }
                else
                {
                    result.Status = false;
                    result.Message = "Log in failled.";
                    result.Code = 404;
                    result.Data = null;
                }
                return Ok(result);
            }
        }

        [Authorize]
        [Route("api/Common/GetURNInfoList")]
        [HttpPost]
        public async Task<IHttpActionResult> GetURNInfoList(userURNmodel info)
        {
            IGenericResult<IList<URNInformation>> result = new GenericResult<IList<URNInformation>>();
            using (var obj = new CommonDataServices())
            {
                var urnData = await Task.FromResult(obj.GetURNINFormation(info.URN));
                if (urnData != null)
                {
                    result.Status = true;
                    result.Message = "Card details fetched successful";
                    result.Code = 200;
                    result.Data = urnData;
                }
                else
                {
                    result.Status = false;
                    result.Message = "No record found.";
                    result.Code = 404;
                }
                return Ok(result);
            }
        }

        #region :: Token Genrerate Code
        private Object GetToken(POCLogin loginData)
        {
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "http://mysite.com";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", loginData.userid.ToString()));
            permClaims.Add(new Claim("name", loginData.username));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(7),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }
        #endregion

        #region :: TokenData Get

        [Authorize]
        [Route("api/Common/GetTokenData")]
        [HttpPost]
        public Object GetTokenData()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
                return new
                {
                    data = name
                };
            }
            return null;
        }
        #endregion

        [HttpGet]
        [Route("api/Common/GetUnblockinginformationByMember")]
        public async Task<IHttpActionResult> GetUnblockinginformationByMember(string action, string urn, string memberId, string hospitalcode, int transactionID)
        {
            using (var hospitalDetailsDataServices = new HospitalDetailsDataServices())
            {
                var data = await Task.FromResult(hospitalDetailsDataServices.GetUnblockingInformationByMember(action, urn, memberId, hospitalcode, transactionID));
                return Ok(data);
            }
        }

        #region :: POS Verification

        [HttpPost]
        [Route("api/Common/POSVerificationStatus")]
        public async Task<IHttpActionResult> POSVerificationStatus(PosInformationDTO model)
        {
            Posmesgreturn result = new Posmesgreturn();

            if (model.authstatus == "1")
            {
                using (var obj = new CommonDataServices())
                {
                    var data = await Task.FromResult(obj.PosInformationInsert(model));
                }
                result.Status = true;
                result.Message = "Sync to server";
                result.Code = 200;
            }
            else
            {
                using (var obj = new CommonDataServices())
                {
                    var data = await Task.FromResult(obj.PosInformationInsert(model));
                }
                result.Status = false;
                result.Message = "Sync Failed";
                result.Code = 200;
            }
            return Ok(result);
        }


        //[HttpGet]
        //[Route("api/Common/CheckPosVerifyOrNot")]
        //public async Task<IHttpActionResult> CheckPosVerifyOrNot(string urn, string uid, int? memberid, int? selectedPatient, string verificationThrough)
        //{
        //    using (var obj = new CommonDataServices())
        //    {
        //        var data = await Task.FromResult(obj.PosVerificationStatus(urn, uid, memberid, selectedPatient, verificationThrough));
        //        return Ok(data);
        //    }
        //}
        [HttpGet]
        [Route("api/Common/CheckPosVerifyOrNot")]
        public async Task<IHttpActionResult> CheckPosVerifyOrNot(string urn, string uid, int? memberid, int? selectedPatient, string verificationThrough, string Hospitalcode)
        {
            using (var obj = new CommonDataServices())
            {
                var data = await Task.FromResult(obj.PosVerificationStatus(urn, uid, memberid, selectedPatient, verificationThrough, Hospitalcode));
                return Ok(data);
            }
        }


        #endregion

        [HttpGet]
        [Route("api/Common/GetVital")]
        public async Task<IHttpActionResult> GetVital(string action, string urn, string memberId, string hospitalcode, int transactionID)
        {
            using (var hospitalDetailsDataServices = new HospitalDetailsDataServices())
            {
                var data = await Task.FromResult(hospitalDetailsDataServices.GetVitalListByMember(action, urn, memberId, hospitalcode, transactionID));
                return Ok(data);
            }
        }

        #region :: OTP Verification

        [HttpPost]
        [Route("api/Common/SendOTP")]
        public async Task<IHttpActionResult> SendOTP(string AadhaarNo)
        {
            object msg = new object();
            if (!string.IsNullOrEmpty(AadhaarNo))
            {
                try
                {
                    var DecryptAadhaarNo = CommonExtension.DecryptStringAES(AadhaarNo);
                    msg = await GenerateOTPByAadhar(DecryptAadhaarNo);
                }
                catch (Exception ex)
                {
                };
            }
            return Ok(msg);
        }
        private async Task<object> GenerateOTPByAadhar(string AadhaarNo)
        {
            object statusmsg = "";
            var GenerateModel = new AadhaarGenerateModel
            {
                uid = AadhaarNo,
                uidType = "A",
                subAuaCode = "PSHAS12302"
            };
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(GenerateModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://164.100.141.79/authekycprodv25/api/generateOTP"; // USER FOR DEVELOPMENT
                //var url = "http://10.150.9.44:8080/authekycprodv25/api/generateOTP";   // USED FOR LIVE SERVER.
                HttpResponseMessage response = await client.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Root resultdata = JsonConvert.DeserializeObject<Root>(result);

                    if (resultdata.ret == "y")
                    {
                        resultdata.uid = AadhaarNo;
                        statusmsg = resultdata;
                    }
                    else
                    {
                        resultdata.uid = AadhaarNo;
                        statusmsg = resultdata;
                    }
                }
            }
            return statusmsg;
        }

        [HttpPost]
        [Route("api/Common/OTPVerification")]
        public async Task<IHttpActionResult> OTPVerification(OTPVerificationModel model)
        {
            var statusmsg = "";
            var OTPVerification = new OTPVerificationDTO
            {
                uid = model.uid,
                uidType = "A",
                consent = "Y",
                //subAuaCode = "0002590000",
                subAuaCode = "PSHAS12302",
                txn = model.txn,
                isBio = "n",
                isOTP = "y",
                isPI ="n",
                bioType = string.Empty,
                rdInfo = string.Empty,
                rdData = string.Empty,
                otpValue = model.otpValue
            };
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(OTPVerification);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://164.100.141.79/authekycv4/api/authenticate";// USER FOR DEVELOPMENT
                //var url = "http://10.150.9.44:8080/authekycv4/api/authenticate";// USER FOR LIVE SERVER

                HttpResponseMessage response = await client.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Root resultdata = JsonConvert.DeserializeObject<Root>(result);

                    if (resultdata.ret == "y")
                    {
                        statusmsg = resultdata.status;
                        using (CommonDataServices obj = new CommonDataServices())
                        {
                            var dataa = await Task.FromResult(obj.InsertOTPVerification(
                                           model.uid,
                                           model.otpValue,
                                           resultdata.txn,
                                           resultdata.responseCode,
                                           resultdata.status,
                                           model.verificationThrough,
                                           model.memberid,
                                           model.urn,
                                           model.patientid,
                                           model.Hospitalcode
                                ));
                        }
                    }
                    else
                    {
                        statusmsg = resultdata.status;
                        using (CommonDataServices obj = new CommonDataServices())
                        {
                            var dataa = await Task.FromResult(obj.InsertOTPVerification(
                                           model.uid,
                                           model.otpValue,
                                           resultdata.txn,
                                           resultdata.responseCode,
                                           resultdata.status,
                                           model.verificationThrough,
                                           model.memberid,
                                           model.urn,
                                           model.patientid,
                                           model.Hospitalcode
                                ));
                        }
                    }
                }
            }
            return Ok(statusmsg);
        }

        #endregion

        #region  :: IRIS Verification
        [HttpPost]
        [Route("api/Common/IRISVerification")]
        public async Task<IHttpActionResult> IRISVerification(IRISVerificationModel model)
        {
            object statusmsg = new object();
            var OTPVerification = new IRISVerificationDTO
            {
                uid = model.uid,
                uidType = "A",
                consent = "Y",
                //subAuaCode = "0002590000",
                subAuaCode = "PSHAS12302",
                txn = string.Empty,
                isPI = "n",
                isBio = "y",
                isOTP = "n",
                bioType = "IIR",
                name = string.Empty,
                rdInfo = model.rdInfo,
                rdData = model.rdData,
                otpValue = string.Empty
            };
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(OTPVerification);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://164.100.141.79/authekycv4/api/authenticate";  // USED FOR DEVELOPMENT.
                //var url = "http://10.150.9.44:8080/authekycv4/api/authenticate";  // USED FOR LIVE SERVER.
                //var url = "http://164.100.141.79/AuthJsonMetaService/authenticate";  // NEW MANTRA IRIS DEVELOPMENT URL.
                HttpResponseMessage response = await client.PostAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Root resultdata = JsonConvert.DeserializeObject<Root>(result);

                    IRISVerificationDetailsDTO Dto = new IRISVerificationDetailsDTO()
                    {
                        URN = model.urn,
                        MemberId = model.memberid,
                        UidNumber = model.uid,
                        Error = resultdata.err,
                        ErrorMsg = resultdata.errMsg,
                        Email = resultdata.email,
                        MobileNo = resultdata.mobileNumber,
                        ResponseCode = resultdata.responseCode,
                        ReturnValue = resultdata.ret,
                        Status = resultdata.status,
                        Txn = resultdata.txn,
                        UidToken = resultdata.uidToken,
                        VerificationThrough = model.verificationThrough,
                        patientid = model.patientid,
                        Hospitalcode = model.Hospitalcode
                    };

                    if (resultdata.ret == "y")
                    {
                        statusmsg = resultdata;

                        using (CommonDataServices obj = new CommonDataServices())
                        {
                            var dataa = await Task.FromResult(obj.InsertIRISVerificationDetails(Dto));
                        }
                    }
                    else
                    {
                        statusmsg = resultdata;
                        using (CommonDataServices obj = new CommonDataServices())
                        {
                            var dataa = await Task.FromResult(obj.InsertIRISVerificationDetails(Dto));
                        }
                    }
                }
            }
            return Ok(statusmsg);
        }

        #endregion
        #endregion

        [HttpGet]
        public async Task<IList<PackageHeader>> GetPackageHeaderList(string Action)
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetHeaderList(Action));
            }
        }

        //Added by Niranjan Poddar on 23-01-2023
        //[HttpGet]
        //public async Task<IList<ViewModelPackageDetails>> AllGetPackageDetails(string Action, string headerid, string hostype)
        //{
        //    using (var CATEGORYdata = new CommonDataServices())
        //    {
        //        return await Task.FromResult(CATEGORYdata.GetAllPackageDetails(Action, headerid, hostype));
        //    }
        //}
        [HttpGet]
        public async Task<IList<ViewModelPackageDetails>> AllGetPackageDetails(string Action, string headerid, string hostype, string excpHostype,string hosCode, string IsEmergency)
        {
            using (var CATEGORYdata = new CommonDataServices())
            {
                return await Task.FromResult(CATEGORYdata.GetAllPackageDetails(Action, headerid, hostype, excpHostype,hosCode, IsEmergency));
            }
        }

        //Added by Niranjan on 29-03-2023
        [HttpGet]
        public async Task<IList<ViewPrevBlockedPackage>> GetPrevBlockedPackage(string urnno, string hoscode, string memid)
        {
            using (var CATEGORYdata = new CommonDataServices())
            {
                return await Task.FromResult(CATEGORYdata.GetPrevBlockedPackageList(urnno, hoscode, memid));
            }
        }

        [HttpGet]
        public async Task<IList<ViewModelPrevPackageBooked>> AllPrevPackageDetails(string Action, string hoscode, string urnno, string memid, string trnsid)
        {
            using (var CATEGORYdata = new CommonDataServices())
            {
                return await Task.FromResult(CATEGORYdata.GetPreviousPackageDetails(Action, hoscode, urnno, memid, trnsid));
            }
        }

        #region :: Kisan 
        [HttpGet]
        public async Task<IHttpActionResult> GetAllState()
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                var data = await Task.FromResult(obj.GetAllState());
                return Ok(data);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetDistrictsByStateCode(string stateCode)
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                var data = await Task.FromResult(obj.GetDistrictsByStateCode(stateCode));
                return Ok(data);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetHospitalByStateCodeAndDistrictCode(string stateCode, string districtCode)
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                var data = await Task.FromResult(obj.GetHospitalDtoByStateCodeAndDistrictCode(stateCode, districtCode));
                return Ok(data);
            }
        }
        #endregion

        [HttpGet]
        public async Task<IList<ViewModelImplantDetails>> GetImplantListByProcCode(string Action, string proccode, string hoscatid)
        {
            using (var CATEGORYdata = new CommonDataServices())
            {
                return await Task.FromResult(CATEGORYdata.GetImplantList(Action, proccode, hoscatid));
            }
        }

        #region Added by Niranjan Poddar on 07-02-2023
        //[HttpGet]
        //public async Task<IList<ViewModelHrgDrugs>> GetHighDrugListByProcCode(string Action)
        //{
        //    using (var CATEGORYdata = new CommonDataServices())
        //    {
        //        return await Task.FromResult(CATEGORYdata.GetHgDrugList(Action));
        //    }
        //}
        [HttpGet]
        public async Task<IList<ViewModelHrgDrugs>> GetHighDrugListByProcCode(string Action, string hosexptype)
        {
            using (var CATEGORYdata = new CommonDataServices())
            {
                return await Task.FromResult(CATEGORYdata.GetHgDrugList(Action, hosexptype));
            }
        }

        //Added by Niranjan Poddar on 09-02-2023
        [HttpGet]
        public async Task<IList<ViewModelWardList>> GetWardListByProcCode(string Action, string ProcCode, string hoscatId)
        {
            using (var CATEGORYdata = new CommonDataServices())
            {
                return await Task.FromResult(CATEGORYdata.GetWardListOnProcCode(Action, ProcCode, hoscatId));
            }
        }

        //Getting ward price based on hospital category
        [HttpGet]
        public async Task<string> getWardPrice(string Action, string wardid, string hoscategory)
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getWardsPrice(Action, wardid, hoscategory));
            }
        }
        #endregion

        [HttpGet]
        public async Task<OverrideInfo> GetOverrideRefrral(string Action, string URN, string Memberid, string HospitalCode)
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getOverrideInfo(Action, URN, Memberid, HospitalCode));
            }
        }

        [HttpGet]
        public async Task<ViewModelWardUnitCost> getWardPriceUnit(string Action, string wardid, string hoscategory)
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.getWardUnitPriceDtls(Action, wardid, hoscategory));
            }
        }

        #region Added for Chatbot app

        [HttpPost]
        [Route("api/Common/CheckURN")]
        public async Task<IHttpActionResult> CheckURN(UserAadhaar val)
        {
            IGenericResult<IList<URNInformation>> result = new GenericResult<IList<URNInformation>>();
            using (var obj = new CommonDataServices())
            {
                var urnData = await Task.FromResult(obj.GetChartbotURNINFormation(val.aadhaar));
                if (urnData != null)
                {
                    result.Status = true;
                    result.Message = "Card details fetched successful";
                    result.Code = 200;
                    result.Data = urnData;
                }
                else
                {
                    result.Status = false;
                    result.Message = "No record found.";
                    result.Code = 404;
                }
                return Ok(result);
            }
        }

        [HttpPost]
        [Route("api/Common/SendChatBotOTP")] // Kisan Modify 20-03-2023
        public async Task<IHttpActionResult> SendChatBotOTP(UserAadhaar val)
        {
            IGenericResult<string> result = new GenericResult<string>();
            using (var obj = new CommonDataServices())
            {
                var urnData = await Task.FromResult(obj.GetChartbotURNINFormation(val.aadhaar));
                if (urnData.Count() > 0)
                {
                    result.Status = true;
                    result.Message = "successful";
                    result.Code = 200;
                    if (!string.IsNullOrEmpty(val.aadhaar))
                    {
                        result.Data = await GenerateOTPByAadhar(val.aadhaar);
                    }
                }
                else
                {
                    result.Status = false;
                    result.Message = "faild";
                    result.Code = 200;
                }
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("api/Common/ChatBotOTPVerification")]
        public async Task<IHttpActionResult> ChatBotOTPVerification(OTPVerificationModelchatbot model)
        {
            var statusmsg = "";
            var OTPVerification = new OTPVerificationDTO
            {
                uid = model.uid,
                uidType = "A",
                consent = "Y",
                subAuaCode = "0002590000",
                txn = model.txn,
                isBio = "n",
                isOTP = "y",
                bioType = string.Empty,
                rdInfo = string.Empty,
                rdData = string.Empty,
                otpValue = model.otpValue
            };
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(OTPVerification);
                var data = new StringContent(json, Encoding.UTF8, "application/json");                
                var url = "http://164.100.141.79/authekycv4/api/kyc";  // USER FOR DEVELOPMENT   
                //var url = "http://10.150.9.44:8080/authekycv4/api/kyc";  // USED FOR LIVE SERVER.
                //var url = ConfigurationManager.AppSettings["urlPath"];

                HttpResponseMessage response = await client.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Root resultdata = JsonConvert.DeserializeObject<Root>(result);

                    if (resultdata.ret == "Y")
                    {
                        statusmsg = resultdata.status;
                        using (CommonDataServices obj = new CommonDataServices())
                        {
                            var dataa = await Task.FromResult(obj.InsertChatbotOTPVerification(
                                           model.uid,
                                           model.otpValue,
                                           resultdata.txn,
                                           resultdata.responseCode,
                                           resultdata.status,
                                           model.verificationThrough
                                ));
                        }
                    }
                    else
                    {
                        statusmsg = resultdata.status;
                        using (CommonDataServices obj = new CommonDataServices())
                        {
                            var dataa = await Task.FromResult(obj.InsertChatbotOTPVerification(
                                           model.uid,
                                           model.otpValue,
                                           resultdata.txn,
                                           resultdata.responseCode,
                                           resultdata.status,
                                           model.verificationThrough
                                ));
                        }
                    }
                }
            }
            return Ok(statusmsg);
        }

        [HttpPost]
        [Route("api/Common/GetURNInfoListForChatbot")]
        public async Task<IList<URNInformation>> GetURNInfoListForChatbot(UserAadhaar info)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetChatbotURNINFormation(info.aadhaar));
            }
        }
        #endregion

        //[Authorize]
        [Route("api/Common/GetPreAuthorizationDetails")]
        [HttpPost]
        public async Task<PreAuthDetails> GetPreAuthorizationDetails(PreAuthModel model) // ADDED by Akshat (28-Feb-23)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetPreAuthorizationDetailsService(model));
            }
        }

        //[Authorize]
        [Route("api/Common/GetPreAuthPackageDetails")]
        [HttpPost]
        public async Task<PreAuthPackageDetails> GetPreAuthPackageDetails(PreAuthPackageModel model) // ADDED by Akshat (28-Feb-23)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.GetPreAuthPackageDetailsService(model));
            }
        }

        //[Authorize]
        [Route("api/Common/PreAuthApprovalDetails")]
        [HttpPost]
        public async Task<PreAuthApprovalDetails> PreAuthApprovalDetails(PreAuthApprovalModel model) // ADDED by Akshat (28-Feb-23)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.PreAuthApprovalDetailsService(model));
            }
        }

        //[Authorize]
        [Route("api/Common/SnaRemarksDetails")]
        [HttpGet]
        public async Task<SnaRemarksDetails> SnaRemarksDetails() // ADDED by Akshat (02-Mar-23)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.SnaRemarksDetailsService());
            }
        }

        //[Authorize]
        [Route("api/Common/SnaCountDetails")]
        [HttpPost]
        public async Task<SnaCountDetails> SnaCountDetails(SnaCountModel model) // ADDED by Akshat (0-Mar-23)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.SnaCountDetailsService(model));
            }
        }

        //[Route("api/Common/GetSSOLoginInformation")]
        //[HttpPost]
        //public async Task<PSSOLogin> GetSSOLoginInformation(userloginmodel login)
        //{
        //    using (var obj = new CommonDataServices())
        //    {
        //        return await Task.FromResult(obj.GetSSOLogin(login.username, login.password));
        //    }
        //}
        [HttpPost]
        public async Task<List<AdmissionStats>> AdmissionReport(ReportModel model) // ADDED by Akshat (16-Mar-23)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.AdmissionReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<BlockedStats>> BlockedReport(ReportModel model) // ADDED by Akshat (16-Mar-23)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.BlockedReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<UnblockedStats>> UnblockedReport(ReportModel model) // ADDED by Akshat (16-Mar-23)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.UnblockedReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<DischargeStats>> DischargeReport(ReportModel model) // ADDED by Akshat (16-Mar-23)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.DischargeReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<PreAuthStats>> PreAuthReport(ReportModel model) // ADDED by Akshat (16-Mar-23)
        {
            using (var obj = new CommonDataServices())
            {
                return await Task.FromResult(obj.PreAuthReportService(model));
            }
        }

        #region :: Kisan 20-03-2023

        [HttpPost]
        [Route("api/Common/GetBskyHolderHospitalName")]
        public async Task<IHttpActionResult> GetBskyHolderHospitalName(BskyholderModel model)
        {
            IGenericResult<IList<URNInformation>> result = new GenericResult<IList<URNInformation>>();
            using (var obj = new CommonDataServices())
            {
                var urnData = await Task.FromResult(obj.GetBskyHolderHospitalName(model.urn, model.memberid));
                if (urnData.Count() > 0)
                {
                    if (urnData.FirstOrDefault().hospitalname == "NA")
                    {
                        result.Status = true;
                        result.Message = urnData.FirstOrDefault().description;
                        result.Code = 200;
                        result.Data = null;
                    }
                    else
                    {
                        result.Status = true;
                        result.Message = urnData.FirstOrDefault().description + " " + urnData.FirstOrDefault().hospitalname;
                        result.Code = 200;
                        result.Data = urnData.FirstOrDefault().hospitalname;
                    }

                }
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("api/Common/viewFile")]
        public HttpResponseMessage viewFile(string encodedString)
        {
            //FTP Server URL.
            string ftp = ConfigurationManager.AppSettings["FTPURL"];
            //FTP Folder name. Leave blank if you want to list files from root folder.
            string fileName = CommonExtension.DecryptString(encodedString);
            string mimeType = Path.GetExtension(fileName);
            //Create FTP Request.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + fileName);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            //Enter FTP Server credentials.
            request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FTPUSERID"], ConfigurationManager.AppSettings["FTPPASSWORD"]);
            request.UsePassive = true;
            request.UseBinary = true;
            request.EnableSsl = false;
            //Fetch the Response and read it into a MemoryStream object.
            FtpWebResponse webResponse = (FtpWebResponse)request.GetResponse();
            using (MemoryStream stream = new MemoryStream())
            {
                webResponse.GetResponseStream().CopyTo(stream);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(stream.ToArray(), 0, stream.ToArray().Length),

                };
                result.Content.Headers.Add("Content-Disposition", "inline");
                if (mimeType == ".jpg" || mimeType == ".jpeg")
                {
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                }
                else
                {
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                }
                return result;
            }
        }
        #endregion

        [HttpPost]
        [Route("api/Common/SendChatBotOTPFamilyHead")] // Kisan Modify 04-04-2023
        public async Task<IHttpActionResult> SendChatBotOTPFamilyHead(ChartBotURNInformation val)
        {
            IGenericResult<string> result = new GenericResult<string>();
            using (var obj = new CommonDataServices())
            {
                var urnData = await Task.FromResult(obj.GetChartbotURNFamilyHeadInformation(val.URN));
                if (urnData.Count() > 0)
                {
                    var aadharno = urnData.FirstOrDefault().aadharno;
                    result.Status = true;
                    result.Message = "successful";
                    result.Code = 200;
                    if (!string.IsNullOrEmpty(aadharno))
                    {
                        result.Data = await GenerateOTPByAadhar(aadharno);
                    }
                }
                else
                {
                    result.Status = false;
                    result.Message = "faild";
                    result.Code = 200;
                }
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("api/Common/Cardbalanceinfochatbot")] // Rajkishor 05-04-2023
        public async Task<IHttpActionResult> Cardbalanceinfochatbot(ChartBotURNInformation val)
        {
            IGenericResult<string> result = new GenericResult<string>();
            using (var obj = new CommonDataServices())
            {
                var urnData = await Task.FromResult(obj.GetCardbalanceinfochatbot(val.URN));
                if (urnData.Count > 0)
                {
                    result.Data = urnData;
                    result.Status = true;
                    result.Message = "successful";
                    result.Code = 200;
                    
                }
                else
                {
                    result.Status = false;
                    result.Message = "faild";
                    result.Code = 500;
                }
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("api/Common/Check_MOMR_Package")]
        public async Task<IHttpActionResult> Check_MOMR_Package(int memberID, string urn, string hospitalcode) // kisan 06-04-2023
        {
            using (var obj = new CommonDataServices())
            {
                var urnData = await Task.FromResult(obj.Check_MOMR_Package(memberID, urn, hospitalcode));
                return Ok(urnData);
            }
        }

        [HttpPost]
        [Route("api/Common/GetUnblockinginformationByMemberNew")]
        public async Task<IHttpActionResult> GetUnblockinginformationByMemberNew(UnblockSearchModel model)
        {
            using (var hospitalDetailsDataServices = new HospitalDetailsDataServices())
            {
                var data = await Task.FromResult(hospitalDetailsDataServices.GetUnblockingInformationByMemberNew(model));
                return Ok(data);
            }
        }

        #region :: Kisan Added 18-may-2023

        [HttpGet]
        [Route("api/Common/CheckMOUStatus")]
        public async Task<IHttpActionResult> CheckMOUStatus(string hospitalcode)
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                var data = await Task.FromResult(obj.Get_MOU_Status_Check(hospitalcode));
                return Ok(data);
            }
        }
        #endregion

        [HttpGet]
        public async Task<IHttpActionResult> GetAllActionTakenByUser()
        {
            using (CommonDataServices obj = new CommonDataServices())
            {
                var data = await Task.FromResult(obj.GetAllActionTakenByUserService());
                return Ok(data);
            }
        }
    }
}