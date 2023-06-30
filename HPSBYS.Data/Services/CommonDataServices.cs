using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPSBYS.Data.Model;
using Dapper;
using NLog;
using System.Security.Cryptography;
using System.Data;
using AdminConsole.Persistence;
using Oracle.ManagedDataAccess.Client;
using System.Security.Claims;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;

namespace HPSBYS.Data.Services
{
    public class CommonDataServices : BaseDataService
    {
        string changePasswordStatus = string.Empty;
        ILogger log = LogManager.GetCurrentClassLogger();
        public IList<Scheme> GetScheme(int Schemecode)
        {
            List<Scheme> SchemeInformation = new List<Scheme>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("v_P_Schemecode", OracleDbType.Int32, ParameterDirection.Input, Schemecode);
                using (SqlConnecton)
                {
                    SchemeInformation = SqlConnecton.Query<Scheme>("USP_T_Scheme_INFO", param, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                SchemeInformation = null;
                log.Error(ex);
            }

            return SchemeInformation;
        }


        public IList<PACKAGECATEGORY> GetPACKAGECATEGORY(string Action)
        {
            List<PACKAGECATEGORY> GetPACKAGECATEGORYInfo = new List<PACKAGECATEGORY>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_Action", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, Action);
                using (SqlConnecton)
                {
                    //GetPACKAGECATEGORYInfo = SqlConnecton.Query<PACKAGECATEGORY>("Exec USP_MOB_GetProcedures @P_Action='" + Action + "'").ToList();
                    GetPACKAGECATEGORYInfo = SqlConnecton.Query<PACKAGECATEGORY>("USP_MOB_GetProcedures", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                GetPACKAGECATEGORYInfo = null;
                log.Error(ex);
            }

            return GetPACKAGECATEGORYInfo;
        }
        public IList<PACKAGECATEGORY> GetPACKAGECATEGORY_PackageChange(string Action)
        {
            List<PACKAGECATEGORY> GetPACKAGECATEGORYInfo = new List<PACKAGECATEGORY>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                using (SqlConnecton)
                {
                    //GetPACKAGECATEGORYInfo = SqlConnecton.Query<PACKAGECATEGORY>("Exec USP_MOB_GetProcedures @P_Action='" + Action + "'").ToList();
                    GetPACKAGECATEGORYInfo = SqlConnecton.Query<PACKAGECATEGORY>("USP_MOB_GetProcedures", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                GetPACKAGECATEGORYInfo = null;
                log.Error(ex);
            }

            return GetPACKAGECATEGORYInfo;
        }
        public IList<SUBPACKAGECATEGORY> GetSubPackageDetail(string Action, string PACKAGECATEGORY)
        {
            List<SUBPACKAGECATEGORY> GetSubPackageInfo = new List<SUBPACKAGECATEGORY>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_PackageCategoryCode", OracleDbType.Varchar2, ParameterDirection.Input, PACKAGECATEGORY);
                using (SqlConnecton)
                {
                    //GetSubPackageInfo = SqlConnecton.Query<SUBPACKAGECATEGORY>("Exec USP_MOB_GetPackages @P_Action='" + Action + "', @P_PackageCategoryCode='" + PACKAGECATEGORY + "'").ToList();

                    GetSubPackageInfo = SqlConnecton.Query<SUBPACKAGECATEGORY>("USP_MOB_GetPackages", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                GetSubPackageInfo = null;
                log.Error(ex);
            }

            return GetSubPackageInfo;
        }
        public IList<SUBPACKAGECATEGORY> GetSubPackageDetail_PackageChange(string Action, string PACKAGECATEGORY)
        {
            List<SUBPACKAGECATEGORY> GetSubPackageInfo = new List<SUBPACKAGECATEGORY>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_PackageCategoryCode", OracleDbType.Varchar2, ParameterDirection.Input, PACKAGECATEGORY);
                using (SqlConnecton)
                {
                    //GetSubPackageInfo = SqlConnecton.Query<SUBPACKAGECATEGORY>("Exec USP_MOB_GetPackages @P_Action='" + Action + "', @P_PackageCategoryCode='" + PACKAGECATEGORY + "'").ToList();
                    GetSubPackageInfo = SqlConnecton.Query<SUBPACKAGECATEGORY>("USP_MOB_GetPackages", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                GetSubPackageInfo = null;
                log.Error(ex);
            }

            return GetSubPackageInfo;
        }
        public IList<PackageInformation> GetPackageDetail_PackageChange(string Action, string PACKAGECATEGORY, string PackageSubCategoryCode)
        {
            List<PackageInformation> GetPackageInfo = new List<PackageInformation>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_PackageSubCategoryCode", OracleDbType.Varchar2, ParameterDirection.Input, PackageSubCategoryCode);
                param.Add("P_PackageCategoryCode", OracleDbType.Varchar2, ParameterDirection.Input, PACKAGECATEGORY);
                using (SqlConnecton)
                {
                    //GetPackageInfo = SqlConnecton.Query<PackageInformation>("Exec USP_MOB_GetPackages @P_Action='" + Action + "',@P_PackageSubCategoryCode='" + PackageSubCategoryCode + "', @P_PackageCategoryCode='" + PACKAGECATEGORY + "'").ToList();

                    GetPackageInfo = SqlConnecton.Query<PackageInformation>("USP_MOB_GetPackages", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                GetPackageInfo = null;
                log.Error(ex);
            }

            return GetPackageInfo;
        }
        public IList<PackageInformation> GetPackageDetail(string Action, string PACKAGECATEGORY, string PackageSubCategoryCode)
        {
            List<PackageInformation> GetPackageInfo = new List<PackageInformation>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_PackageSubCategoryCode", OracleDbType.Varchar2, ParameterDirection.Input, PackageSubCategoryCode);
                param.Add("P_PackageCategoryCode", OracleDbType.Varchar2, ParameterDirection.Input, PACKAGECATEGORY);
                using (SqlConnecton)
                {
                    //GetPackageInfo = SqlConnecton.Query<PackageInformation>("USP_MOB_GetPackages @P_Action='" + Action + "',@P_PackageSubCategoryCode='" + PackageSubCategoryCode + "', @P_PackageCategoryCode='" + PACKAGECATEGORY + "'").ToList();

                    GetPackageInfo = SqlConnecton.Query<PackageInformation>("USP_MOB_GetPackages", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                GetPackageInfo = null;
                log.Error(ex);
            }

            return GetPackageInfo;
        }
        public IList<WardDetail> GetWardDetails(string Action, string PreAuthStatus)
        {
            List<WardDetail> WardDtls = new List<WardDetail>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_Action", OracleDbType.Char, ParameterDirection.Input, Action);
                param.Add("P_PreAuthStatus", OracleDbType.Char, ParameterDirection.Input, PreAuthStatus);

                using (SqlConnecton)
                {
                    //WardDtls = SqlConnecton.Query<WardDetail>("Exec USP_T_Ward_INFO @P_Action='" + Action + "', @P_PreAuthStatus='" + PreAuthStatus + "'").ToList();
                    WardDtls = SqlConnecton.Query<WardDetail>("USP_T_Ward_INFO", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                WardDtls = null;
                log.Error(ex);
            }

            return WardDtls;
        }
        //To get Insurance Information
        public IList<PolicyDetails> GetInsuranceDetail(string URN)
        {
            List<PolicyDetails> GetPolicy = new List<PolicyDetails>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("v_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                using (SqlConnecton)
                {
                    //GetPolicy = SqlConnecton.Query<PolicyDetails>("USP_T_Policy_INFO @URN='" + URN + "'").ToList();
                    GetPolicy = SqlConnecton.Query<PolicyDetails>("USP_T_Policy_INFO", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                GetPolicy = null;
                log.Error(ex);
            }

            return GetPolicy;
        }

        //To get  UnblockingReasonl
        public IList<UnblockingReason> GetUnblockingReason(string Action)
        {
            List<UnblockingReason> GetUnblockingReason = new List<UnblockingReason>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_Action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                using (SqlConnecton)
                {
                    //GetUnblockingReason = SqlConnecton.Query<UnblockingReason>("Exec USP_T_UnblockReason_INFO @P_Action='" + Action + "'").ToList();
                    GetUnblockingReason = SqlConnecton.Query<UnblockingReason>("USP_T_UnblockReason_INFO", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                GetUnblockingReason = null;
                log.Error(ex);
            }

            return GetUnblockingReason;
        }


        public Login GetLogin(string Username,string macid) /*,string Passworrd*/
        {
            Login GetLoginobj = new Login();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("v_P_Username", OracleDbType.Varchar2, ParameterDirection.Input, Username);
                    parameters.Add("v_P_MACID", OracleDbType.Varchar2, ParameterDirection.Input, macid);
                    GetLoginobj = SqlConnecton.Query<Login>("USP_T_USERLOGIN_TMS", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                GetLoginobj = null;
                log.Error(ex);
            }
            return GetLoginobj;
        }
        

        public BalanceInfo getBalanceInfo(string Action, string URN, string FamilyId)
        {
            BalanceInfo balanceInfo = new BalanceInfo();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                using (SqlConnecton)
                {
                    balanceInfo = SqlConnecton.Query<BalanceInfo>("USP_CardBalanceInfo_TMS", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                balanceInfo = null;
                log.Error(ex);
            }
            return balanceInfo;
        }
        public IList<PatientInfo> getPatientStatus(string Action, string URN)
        {
            List<PatientInfo> info = new List<PatientInfo>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("v_P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("v_P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);

                using (SqlConnecton)
                {
                    info = SqlConnecton.Query<PatientInfo>("USP_T_GetPatientStatus", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                info = null;
                log.Error(ex);
            }

            return info;
        }
        public IList<Notice> getNotices(string Action)
        {
            List<Notice> info = new List<Notice>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("v_P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                using (SqlConnecton)
                {
                    info = SqlConnecton.Query<Notice>("USP_T_GetNotices_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                info = null;
                log.Error(ex);
            }

            return info;
        }

        public PatientStats getPatientStats(string Action, string HospitalCode) // Added by Akshat (21 Jan 23)
        {
            PatientStats patientStats = new PatientStats();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("v_P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("v_P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                using (SqlConnecton)
                {
                    patientStats = SqlConnecton.Query<PatientStats>("USP_T_GETNOTICES_TMS", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                patientStats = null;
                log.Error(ex);
            }
            return patientStats;
        }


        public string changePassword(Login login)
        {

            //object[] objArray = new object[] {
            //       "@Action",'A'
            //      ,"@intUserId",login.USERID
            //      ,"@vchUserName",login.USERNAME
            //      ,"@vchOldPassword",GenerateHash(login.Password)
            //      ,"@vchNewPassword",GenerateHash(login.NewPassword)
            //      };
            try
            {
                OracleDynamicParameters param = new OracleDynamicParameters();
                param.Add("v_Action", oracleDbType: OracleDbType.Char, ParameterDirection.Input, 'A');
                param.Add("v_intUserId", oracleDbType: OracleDbType.Int32, ParameterDirection.Input, login.USERID);
                param.Add("v_vchUserName", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, login.USERNAME);
                param.Add("v_vchOldPassword", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, GenerateHash(login.Password));
                param.Add("v_vchNewPassword", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, GenerateHash(login.NewPassword));
                param.Add("v_P_MSGOUT", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Output, "");

                //DynamicParameters param = objArray.ToDynamicParameters("@P_MSGOUT");
                var result = SqlConnecton.Execute("USP_Transaction_ChangePassword", param, commandType: CommandType.StoredProcedure);
                changePasswordStatus = "1";// param.Get("v_P_MSGOUT");//Need to check
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return changePasswordStatus;
        }
        private string GenerateHash(string SourceText)
        {
            UTF8Encoding Ue = new UTF8Encoding();
            string pwdString;
            MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();
            Byte[] ByteHash = Md5.ComputeHash(Ue.GetBytes(SourceText));
            pwdString = BitConverter.ToString(ByteHash);
            pwdString = pwdString.Replace("-", null);
            return pwdString;
        }
        public IList<Dist> getAllDist(string Action)
        {
            List<Dist> info = new List<Dist>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("v_vchAction", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                using (SqlConnecton)
                {
                    //info = SqlConnecton.Query<Dist>("Exec USP_T_GET_Districts @vchAction='" + Action + "'").ToList();
                    info = SqlConnecton.Query<Dist>("USP_T_GET_Districts", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                info = null;
                log.Error(ex);
            }

            return info;
        }
        public IList<Block> getAllBlockByDistCode(string Action, int DistCode)
        {
            List<Block> info = new List<Block>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("v_vchAction", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("v_DistrictCode", OracleDbType.Int32, ParameterDirection.Input, DistCode);
                using (SqlConnecton)
                {
                    //info = SqlConnecton.Query<Block>("Exec USP_T_GET_Districts @vchAction='" + Action + "',@DistrictCode='"+DistCode+"'").ToList();
                    info = SqlConnecton.Query<Block>("USP_T_GET_Districts", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                info = null;
                log.Error(ex);
            }

            return info;
        }
        public IList<PHC> getAllPHCByDistCodeAndBlockCode(string Action, int DistCode, int BlockCode)
        {
            List<PHC> info = new List<PHC>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("v_vchAction", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("v_DistrictCode", OracleDbType.Int32, ParameterDirection.Input, DistCode);
                param.Add("v_BlockCode", OracleDbType.Int32, ParameterDirection.Input, BlockCode);
                using (SqlConnecton)
                {
                    //info = SqlConnecton.Query<PHC>("Exec USP_T_GET_Districts @vchAction='" + Action + "',@DistrictCode='" + DistCode + "',@BlockCode='"+ BlockCode + "'").ToList();
                    info = SqlConnecton.Query<PHC>("USP_T_GET_Districts", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                info = null;
                log.Error(ex);
            }

            return info;
        }
        public IList<SubCentre> getAllSubCentreByDistBlockAndPHCCode(string Action, int DistCode, int BlockCode, int PHCCode)
        {
            List<SubCentre> info = new List<SubCentre>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("v_vchAction", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("v_DistrictCode", OracleDbType.Int32, ParameterDirection.Input, DistCode);
                param.Add("v_BlockCode", OracleDbType.Int32, ParameterDirection.Input, BlockCode);
                param.Add("v_PHCCode", OracleDbType.Int32, ParameterDirection.Input, PHCCode);
                using (SqlConnecton)
                {
                    //info = SqlConnecton.Query<SubCentre>("Exec USP_T_GET_Districts @vchAction='" + Action + "',@DistrictCode='" + DistCode + "',@BlockCode='" + BlockCode + "',@PHCCode='"+PHCCode+"'").ToList();

                    info = SqlConnecton.Query<SubCentre>("USP_T_GET_Districts", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                info = null;
                log.Error(ex);
            }

            return info;
        }
        public IList<DownwardReferalInfo> getDownwardReferalByBlockingInvoiceNo(string Action, string InvoiceNo)
        {
            List<DownwardReferalInfo> info = new List<DownwardReferalInfo>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("v_P_ActionCode", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("v_P_BlockingInvoiceNo", OracleDbType.Int32, ParameterDirection.Input, InvoiceNo);
                using (SqlConnecton)
                {
                    //info = SqlConnecton.Query<DownwardReferalInfo>("Exec USP_ViewDownwardReferal @P_ActionCode='" + Action + "',@P_BlockingInvoiceNo='" + InvoiceNo + "'").ToList();

                    info = SqlConnecton.Query<DownwardReferalInfo>("USP_ViewDownwardReferal", param, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                info = null;
                log.Error(ex);
            }
            return info;
        }
        public GetTokenDto getapitoken()
        {
            GetTokenDto model = new GetTokenDto();
            try
            {
                model.status = "true";
                model.Message = "Token fetched successfully";
                model.code = "200";
                model.token = Guid.NewGuid().ToString();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return model;
        }
        public static string GenerateToken()
        {
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            string token = ((day * 100 + month) * 700 + day * 13).ToString();
            return token;
        }
        public POCLogin GetPOSLogin(string Username, string Passworrd)
        {
            POCLogin GetLoginobj = new POCLogin();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("v_P_Username", OracleDbType.Varchar2, ParameterDirection.Input, Username);
                    //parameters.Add("v_P_Passwd", OracleDbType.Varchar2, ParameterDirection.Input, GenerateHash(Passworrd));

                    #region :: Old Code
                    //GetLoginobj = SqlConnecton.Query<POCLogin>("USP_T_UserLogin", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    #endregion 

                    #region :: Kisan code
                    GetLoginobj = SqlConnecton.Query<POCLogin>("USP_T_UserLogin_TMS", parameters, commandType: CommandType.StoredProcedure).Select(s => new POCLogin
                    {
                        userid = s.userid,
                        username = s.username,
                        designation = !string.IsNullOrEmpty(s.designation) ? s.designation : string.Empty,
                        state = !string.IsNullOrEmpty(s.state) ? s.state : string.Empty,
                        district = !string.IsNullOrEmpty(s.district) ? s.district : string.Empty,
                        Hospital = !string.IsNullOrEmpty(s.Hospital) ? s.Hospital : string.Empty,
                        Hospitalcode = !string.IsNullOrEmpty(s.Hospitalcode) ? s.Hospitalcode : string.Empty,
                        tokenkey = s.tokenkey
                    }).FirstOrDefault();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GetLoginobj = null;
                log.Error(ex);
            }
            return GetLoginobj;
        }

        public IList<URNInformation> GetURNINFormation(string URN)
        {
            List<URNInformation> URNInformation = new List<URNInformation>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    parameters.Add("P_SEARCHBY", OracleDbType.Int64, ParameterDirection.Input, null);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, null);
                    URNInformation = SqlConnecton.Query<URNInformation>("USP_T_GetURN_INFO_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                URNInformation = null;
                log.Error(ex);
            }
            return URNInformation;
        }

        //private string generateJwtTokenM()
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_options.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}


        //Added by Niranjan Poddar
        //Used for Getting Vital Parameter
        public IList<VitalParameter> GetVitalParameters(string Action)
        {
            List<VitalParameter> vitalParameters = new List<VitalParameter>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                using (SqlConnecton)
                {
                    vitalParameters = SqlConnecton.Query<VitalParameter>("USP_VITALPARAMETER_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                vitalParameters = null;
                log.Error(ex);
            }

            return vitalParameters;
        }

        public IList<PackageHeader> GetHeaderList(string Action)
        {
            List<PackageHeader> PackageHeader = new List<PackageHeader>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("P_PACKAGEHEADERCODE", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_HOSPITALCATEGORYID", OracleDbType.Int64, ParameterDirection.Input, 2);
                    parameters.Add("P_PACKAGESUBCATEGORYID", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    parameters.Add("P_GENDER", OracleDbType.Varchar2, ParameterDirection.Input, "");
                    PackageHeader = SqlConnecton.Query<PackageHeader>("USP_MOB_GetProcedures_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                PackageHeader = null;
                log.Error(ex);
            }
            return PackageHeader;
        }

        //Added by Niranjan Poddar on 23-01-2023
        //public IList<ViewModelPackageDetails> GetAllPackageDetails(string Action, string headerid, string hostype)
        //{
        //    List<ViewModelPackageDetails> getPackDetails = new List<ViewModelPackageDetails>();

        //    try
        //    {
        //        var param = new OracleDynamicParameters();
        //        param.Add("P_ACTION", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, Action);
        //        param.Add("P_PACKAGEHEADERID", oracleDbType: OracleDbType.Int16, ParameterDirection.Input, headerid);
        //        param.Add("P_HOSPITALCATEGORYID", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, hostype);
        //        using (SqlConnecton)
        //        {
        //            getPackDetails = SqlConnecton.Query<ViewModelPackageDetails>("USP_MOB_GetProcedures_TMS", param, commandType: CommandType.StoredProcedure).ToList();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        getPackDetails = null;
        //        log.Error(ex);
        //    }

        //    return getPackDetails;
        //}

        public IList<ViewModelPackageDetails> GetAllPackageDetails(string Action, string headerid, string hostype, string excptype,string hosCode, string IsEmergency)
        {
            List<ViewModelPackageDetails> getPackDetails = new List<ViewModelPackageDetails>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_PACKAGEHEADERID", oracleDbType: OracleDbType.Int16, ParameterDirection.Input, headerid);
                param.Add("P_HOSPITALCATEGORYID", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, hostype);
                param.Add("P_EXCEPTIONHOSPITAL", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, excptype);
                param.Add("P_HOSPITALCODE", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, hosCode);
                param.Add("P_ISEMERGENCY", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, IsEmergency);//Added by Rajkishor(30-May-23)
                using (SqlConnecton)
                {
                    getPackDetails = SqlConnecton.Query<ViewModelPackageDetails>("USP_MOB_GetProcedures_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                getPackDetails = null;
                log.Error(ex);
            }

            return getPackDetails;
        }
        //Added by Niranjan Poddar on 29-03-2023
        public IList<ViewPrevBlockedPackage> GetPrevBlockedPackageList(string urnno, string hoscode, string memid)
        {
            List<ViewPrevBlockedPackage> getPackDetails = new List<ViewPrevBlockedPackage>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_URN", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, urnno);
                param.Add("P_MEMBERID", oracleDbType: OracleDbType.Int16, ParameterDirection.Input, memid);
                param.Add("P_HOSPITALCODE", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, hoscode);

                using (SqlConnecton)
                {
                    getPackDetails = SqlConnecton.Query<ViewPrevBlockedPackage>("USP_GET_PREVIOUSPACKAGES_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                getPackDetails = null;
                log.Error(ex);
            }

            return getPackDetails;
        }

        public string PosInformationInsert(PosInformationDTO Dto)
        {
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, null);
                dyParam.Add("P_UIDNUMBER", OracleDbType.Varchar2, ParameterDirection.Input, Dto.aadhar);
                dyParam.Add("P_DEVICESERIALNO", OracleDbType.Varchar2, ParameterDirection.Input, Dto.deviceslno);
                dyParam.Add("P_MEMBERID", OracleDbType.Int64, ParameterDirection.Input, Dto.memberid);
                dyParam.Add("P_PATIENTMEMBERID", OracleDbType.Int64, ParameterDirection.Input, Dto.memberid);
                dyParam.Add("P_LONGITUDE", OracleDbType.Varchar2, ParameterDirection.Input, Dto.Longitude);
                dyParam.Add("P_LATITUDE", OracleDbType.Varchar2, ParameterDirection.Input, Dto.Latitute);
                dyParam.Add("P_VARIFIEDTHROUGH", OracleDbType.Varchar2, ParameterDirection.Input, null);
                dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, Dto.Hospitalcode);
                dyParam.Add("P_AUTHENTICATIONSTATUS", OracleDbType.Int64, ParameterDirection.Input, int.Parse(Dto.authstatus));
                dyParam.Add("P_MESSAGEOUT", OracleDbType.Int64, ParameterDirection.Output);
                var query = "USP_T_POSDETAILS_TMS";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return strOutput;
        }


        public List<PosInfoDto> PosVerificationStatus(string urn, string uid, int? memberid, int? selectedPatient, string verificationThrough, string Hospitalcode)
        {
            List<PosInfoDto> result = new List<PosInfoDto>();
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_ACTION", "B");
                dyParam.Add("P_URN", urn);
                dyParam.Add("P_UIDNUMBER", uid);
                dyParam.Add("P_DEVICESERIALNO", null);
                dyParam.Add("P_MEMBERID", memberid);
                dyParam.Add("P_PATIENTMEMBERID", selectedPatient);
                dyParam.Add("P_LONGITUDE", null);
                dyParam.Add("P_LATITUDE", null);
                dyParam.Add("P_VARIFIEDTHROUGH", verificationThrough);
                dyParam.Add("P_HOSPITALCODE", Hospitalcode);

                dyParam.Add("P_MESSAGEOUT", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var query = "USP_T_POSDETAILS_TMS";
                var data = SqlConnecton.Query<PosInfoDto>(query, dyParam, commandType: CommandType.StoredProcedure).ToList();
                var IsSuccess = dyParam.Get<dynamic>("P_MESSAGEOUT");
                if (data.Count() > 0)
                {
                    result = data;
                }
                else
                {
                    result = data;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return result;
        }

        public string InsertOTPVerification(string uid, string otpValue, string txn, string responseCode, string status, string verificationThrough, string memberid, string urn, string Patientid, string HospitalCode)
        {
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                dyParam.Add("P_UIDNUMBER", OracleDbType.Varchar2, ParameterDirection.Input, uid);
                //dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, urn);
                dyParam.Add("P_MEMBERID", OracleDbType.Int64, ParameterDirection.Input, int.Parse(memberid));
                dyParam.Add("P_PATIENTMEMBERID", OracleDbType.Int64, ParameterDirection.Input, int.Parse(Patientid));
                dyParam.Add("P_TXNCODE", OracleDbType.Varchar2, ParameterDirection.Input, txn);
                dyParam.Add("P_OTPVALUE", OracleDbType.Varchar2, ParameterDirection.Input, otpValue);
                dyParam.Add("P_VERIFIEDTHROUGH", OracleDbType.Varchar2, ParameterDirection.Input, verificationThrough);
                dyParam.Add("P_VERIFIEDSTATUS", OracleDbType.Varchar2, ParameterDirection.Input, "Y");
                dyParam.Add("P_CREATEDBY", OracleDbType.Int64, ParameterDirection.Input, 1);
                dyParam.Add("P_ERRORMESG", OracleDbType.Varchar2, ParameterDirection.Input, status);
                dyParam.Add("P_RESPONDCODE", OracleDbType.Varchar2, ParameterDirection.Input, responseCode);
                dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                dyParam.Add("P_MESSAGEOUT", OracleDbType.Int64, ParameterDirection.Output);

                var query = "USP_T_OTPDETAILS_TMS";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return strOutput;
        }


        public IList<ViewModelPrevPackageBooked> GetPreviousPackageDetails(string Action, string hoscode, string urnno, string memid, string trnsid)
        {
            List<ViewModelPrevPackageBooked> getPackDetails = new List<ViewModelPrevPackageBooked>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_Hospitalcode", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, hoscode);
                param.Add("P_URN", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, urnno);
                param.Add("P_MEMBERID", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, memid);
                param.Add("P_TRANSACTIONID", oracleDbType: OracleDbType.Int64, ParameterDirection.Input, trnsid);
                using (SqlConnecton)
                {
                    getPackDetails = SqlConnecton.Query<ViewModelPrevPackageBooked>("USP_T_Admission_INFO_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                getPackDetails = null;
                log.Error(ex);
            }

            return getPackDetails;
        }

        #region :: Kisan (04-02-23)
        public IList<State> GetAllState()
        {
            List<State> StateData = new List<State>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "S");
                param.Add("P_STATECODE", OracleDbType.Varchar2, ParameterDirection.Input, null);
                param.Add("P_DISTRICTCODE", OracleDbType.Varchar2, ParameterDirection.Input, null);
                using (SqlConnecton)
                {
                    StateData = SqlConnecton.Query<State>("USP_DISCHARGE_INFO_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                StateData = null;
                log.Error(ex);
            }
            return StateData;
        }
        public List<Dist> GetDistrictsByStateCode(string stateCode)
        {
            List<Dist> StateData = new List<Dist>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "D");
                param.Add("P_STATECODE", OracleDbType.Varchar2, ParameterDirection.Input, stateCode);
                param.Add("P_DISTRICTCODE", OracleDbType.Varchar2, ParameterDirection.Input, null);
                using (SqlConnecton)
                {
                    StateData = SqlConnecton.Query<Dist>("USP_DISCHARGE_INFO_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                StateData = null;
                log.Error(ex);
            }
            return StateData;
        }
        public List<HospitalDto> GetHospitalDtoByStateCodeAndDistrictCode(string stateCode, string districtCode)
        {
            List<HospitalDto> StateData = new List<HospitalDto>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "H");
                param.Add("P_STATECODE", OracleDbType.Varchar2, ParameterDirection.Input, stateCode);
                param.Add("P_DISTRICTCODE", OracleDbType.Varchar2, ParameterDirection.Input, districtCode);
                using (SqlConnecton)
                {
                    StateData = SqlConnecton.Query<HospitalDto>("USP_DISCHARGE_INFO_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                StateData = null;
                log.Error(ex);
            }
            return StateData;
        }
        #endregion

        public string InsertIRISVerificationDetails(IRISVerificationDetailsDTO Dto)
        {
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                dyParam.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, Dto.URN);
                dyParam.Add("P_MEMBERID", OracleDbType.Int64, ParameterDirection.Input, int.Parse(Dto.MemberId));
                dyParam.Add("P_PATIENTMEMBERID", OracleDbType.Int64, ParameterDirection.Input, int.Parse(Dto.patientid));
                dyParam.Add("P_UIDNUMBER", OracleDbType.Varchar2, ParameterDirection.Input, Dto.UidNumber);
                dyParam.Add("P_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input, Dto.Email);
                dyParam.Add("P_ERR", OracleDbType.Varchar2, ParameterDirection.Input, Dto.Error);
                dyParam.Add("P_ERRMSG", OracleDbType.Varchar2, ParameterDirection.Input, Dto.ErrorMsg);
                dyParam.Add("P_MOBILENUMBER", OracleDbType.Varchar2, ParameterDirection.Input, Dto.MobileNo);
                dyParam.Add("P_RESPONSECODE", OracleDbType.Varchar2, ParameterDirection.Input, Dto.ResponseCode);
                dyParam.Add("P_RET", OracleDbType.Varchar2, ParameterDirection.Input, Dto.ReturnValue);
                dyParam.Add("P_STATUS", OracleDbType.Varchar2, ParameterDirection.Input, Dto.Status);
                dyParam.Add("P_TXN", OracleDbType.Varchar2, ParameterDirection.Input, Dto.Txn);
                dyParam.Add("P_UIDTOKEN", OracleDbType.Varchar2, ParameterDirection.Input, Dto.UidToken);
                dyParam.Add("P_VERIFYTHROUGH", OracleDbType.Varchar2, ParameterDirection.Input, Dto.VerificationThrough);
                dyParam.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, Dto.Hospitalcode);
                dyParam.Add("P_MESSAGEOUT", OracleDbType.Int64, ParameterDirection.Output);
                var query = "USP_T_IRISDETAILS_TMS";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return strOutput;
        }



        //New Code Added by Niranjan Poddar on 03-Feb-2023
        public IList<ViewModelImplantDetails> GetImplantList(string Action, string proccode, string hoscatid)
        {
            List<ViewModelImplantDetails> getImplantDetails = new List<ViewModelImplantDetails>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_PROCEDURECODE", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, proccode);
                param.Add("P_HOSPITALCATEGORY", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, hoscatid);
                using (SqlConnecton)
                {
                    getImplantDetails = SqlConnecton.Query<ViewModelImplantDetails>("USP_T_GETIMPLANT_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                getImplantDetails = null;
                log.Error(ex);
            }

            return getImplantDetails;
        }

        //public IList<ViewModelHrgDrugs> GetHgDrugList(string Action)
        //{
        //    List<ViewModelHrgDrugs> getHedDetails = new List<ViewModelHrgDrugs>();
        //    try
        //    {
        //        var param = new OracleDynamicParameters();
        //        param.Add("P_ACTION", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, Action);

        //        using (SqlConnecton)
        //        {
        //            getHedDetails = SqlConnecton.Query<ViewModelHrgDrugs>("USP_T_GETIMPLANT_TMS", param, commandType: CommandType.StoredProcedure).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        getHedDetails = null;
        //        log.Error(ex);
        //    }
        //    return getHedDetails;
        //}

        public IList<ViewModelHrgDrugs> GetHgDrugList(string Action, string excpHostype)
        {
            List<ViewModelHrgDrugs> getHedDetails = new List<ViewModelHrgDrugs>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_EXCEPTIONHOSPITAL", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, excpHostype);
                using (SqlConnecton)
                {
                    getHedDetails = SqlConnecton.Query<ViewModelHrgDrugs>("USP_T_GETIMPLANT_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                getHedDetails = null;
                log.Error(ex);
            }
            return getHedDetails;
        }

        //Added by Niranjan Poddar on 09-02-2023
        public IList<ViewModelWardList> GetWardListOnProcCode(string Action, string ProcCode)
        {
            List<ViewModelWardList> getWards = new List<ViewModelWardList>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_PROCEDURECODE", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, ProcCode);
                using (SqlConnecton)
                {
                    getWards = SqlConnecton.Query<ViewModelWardList>("USP_T_GETIMPLANT_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                getWards = null;
                log.Error(ex);
            }

            return getWards;
        }

        public IList<ViewModelWardList> GetWardListOnProcCode(string Action, string ProcCode, string hoscatId)
        {
            List<ViewModelWardList> getWards = new List<ViewModelWardList>();

            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_PROCEDURECODE", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, ProcCode);
                param.Add("P_HOSPITALCATEGORY", oracleDbType: OracleDbType.Int32, ParameterDirection.Input, hoscatId);
                using (SqlConnecton)
                {
                    getWards = SqlConnecton.Query<ViewModelWardList>("USP_T_GETIMPLANT_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }

            }
            catch (Exception ex)
            {
                getWards = null;
                log.Error(ex);
            }

            return getWards;
        }

        public string getWardsPrice(string Action, string wardid, string hoscategory)
        {
            string result = string.Empty;
            try
            {
                OracleDynamicParameters param = new OracleDynamicParameters();
                param.Add("P_ACTION", oracleDbType: OracleDbType.Char, ParameterDirection.Input, Action);
                param.Add("P_WARDID", oracleDbType: OracleDbType.Int32, ParameterDirection.Input, wardid);
                param.Add("P_HOSPITALCATEGORY", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, hoscategory);

                //DynamicParameters param = objArray.ToDynamicParameters("@P_MSGOUT");
                object result1 = SqlConnecton.ExecuteScalar<string>("USP_T_GetImplant_TMS", param, commandType: CommandType.StoredProcedure);
                if (result1 != null)
                {
                    result = result1.ToString();
                }
                else
                {
                    result = "0.00";
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return result;
        }

        public OverrideInfo getOverrideInfo(string Action, string URN, string Memberid, string HospitalCode)
        {
            OverrideInfo overrideinfo = new OverrideInfo();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                param.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                param.Add("P_MEMBERID", OracleDbType.Varchar2, ParameterDirection.Input, Memberid);
                param.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                using (SqlConnecton)
                {
                    overrideinfo = SqlConnecton.Query<OverrideInfo>("USP_T_GET_OVERRIDE_REFERRAL", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                overrideinfo = null;
                log.Error(ex);
            }
            return overrideinfo;
        }

        public ViewModelWardUnitCost getWardUnitPriceDtls(string Action, string wardid, string hoscategory)
        {
            ViewModelWardUnitCost getWards = new ViewModelWardUnitCost();
            try
            {
                OracleDynamicParameters param = new OracleDynamicParameters();
                param.Add("P_ACTION", oracleDbType: OracleDbType.Char, ParameterDirection.Input, Action);
                param.Add("P_WARDID", oracleDbType: OracleDbType.Int32, ParameterDirection.Input, wardid);
                param.Add("P_HOSPITALCATEGORY", oracleDbType: OracleDbType.Varchar2, ParameterDirection.Input, hoscategory);

                //DynamicParameters param = objArray.ToDynamicParameters("@P_MSGOUT");
                getWards = SqlConnecton.Query<ViewModelWardUnitCost>("USP_T_GetImplant_TMS", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return getWards;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return getWards;
        }

        public string InsertChatbotOTPVerification(string uid, string otpValue, string txn, string responseCode, string status, string verificationThrough)
        {
            string strOutput = "";
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                dyParam.Add("P_UIDNUMBER", OracleDbType.Varchar2, ParameterDirection.Input, uid);
                dyParam.Add("P_MEMBERID", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_PATIENTMEMBERID", OracleDbType.Int64, ParameterDirection.Input, 0);
                dyParam.Add("P_TXNCODE", OracleDbType.Varchar2, ParameterDirection.Input, txn);
                dyParam.Add("P_OTPVALUE", OracleDbType.Varchar2, ParameterDirection.Input, otpValue);
                dyParam.Add("P_VERIFIEDTHROUGH", OracleDbType.Varchar2, ParameterDirection.Input, 900);
                dyParam.Add("P_VERIFIEDSTATUS", OracleDbType.Varchar2, ParameterDirection.Input, "Y");
                dyParam.Add("P_CREATEDBY", OracleDbType.Int64, ParameterDirection.Input, 1);
                dyParam.Add("P_ERRORMESG", OracleDbType.Varchar2, ParameterDirection.Input, status);
                dyParam.Add("P_RESPONDCODE", OracleDbType.Varchar2, ParameterDirection.Input, responseCode);
                dyParam.Add("P_MESSAGEOUT", OracleDbType.Int64, ParameterDirection.Output);

                var query = "USP_T_OTPDETAILS_TMS";
                strOutput = SqlConnecton.Execute(query, dyParam, commandType: CommandType.StoredProcedure).ToString();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return strOutput;
        }

        public IList<URNInformation> GetChatbotURNINFormation(string URN)
        {
            List<URNInformation> URNInformation = new List<URNInformation>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "B");
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    parameters.Add("P_SEARCHBY", OracleDbType.Int32, ParameterDirection.Input, 2);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, null);
                    URNInformation = SqlConnecton.Query<URNInformation>("USP_T_GetURN_INFO_TMS_Chatbot", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                URNInformation = null;
                log.Error(ex);
            }
            return URNInformation;
        }

        //public PreAuthDetails GetPreAuthorizationDetailsService(PreAuthModel preAuthModel) // ADDED by Akshat (28-Feb-23)
        //{
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("P_ACTION", preAuthModel.Action);
        //        param.Add("P_SNAID", preAuthModel.UserId);
        //        param.Add("P_FROMDATE", preAuthModel.Fromdate);
        //        param.Add("P_TODATE", preAuthModel.ToDate);
        //        param.Add("P_STATECODE", preAuthModel.StateCode);
        //        param.Add("P_DISTRICTCODE", preAuthModel.DistCode);
        //        param.Add("P_HOSPITALCODE", preAuthModel.HospitalCode);
        //        param.Add("P_TYPE", preAuthModel.Type);
        //        param.Add("P_PAGEINDEX", preAuthModel.PageNum);
        //        param.Add("P_PAGESIZE", preAuthModel.PerPage);

        //        var procedureName = "USP_GET_PREAUTH_STATUS_REPORT";

        //        var PreAuthDetailsDict = new Dictionary<string, PreAuthDetails>();
        //        var PreAuthListDict = new Dictionary<string, PreAuthList>();

        //        var response = SqlConnecton.Query<PreAuthDetails, PreAuthList, PreAuthDoc, PreAuthDetails>(procedureName,
        //            (objPreAuthDetails, objPreAuthList, objPreAuthDoc) =>
        //            {
        //                PreAuthDetails pad;
        //                if (!PreAuthDetailsDict.TryGetValue(objPreAuthDetails.code, out pad))
        //                {
        //                    pad = objPreAuthDetails;
        //                    pad.PreauthList = new List<PreAuthList>();
        //                    PreAuthDetailsDict.Add(pad.code, pad);
        //                }

        //                PreAuthList pal;
        //                if (!PreAuthListDict.TryGetValue(objPreAuthList.AuthorityCode, out pal))
        //                {
        //                    pal = objPreAuthList;
        //                    pal.docs = new List<PreAuthDoc>();
        //                    PreAuthListDict.Add(pal.AuthorityCode, pal);
        //                }

        //                objPreAuthDoc.DocLink = objPreAuthDoc.Addtional_Doc1 == "NA" ? "NA" : ConfigurationManager.AppSettings["ServiceURL"] + "/api/Common/viewFile" + "?encodedString=" + CommonExtension.encryptString(Convert.ToDateTime(objPreAuthList.RequestedDate).Year + "/" + int.Parse(objPreAuthList.HospitalCode) + "/PREAUTHDOC/" + objPreAuthDoc.Addtional_Doc1);
        //                pal.docs.Add(objPreAuthDoc);
        //                pad.PreauthList.Add(pal);
        //                return pad;
        //            },
        //            param, commandType: CommandType.StoredProcedure, splitOn: "UrnNo, Addtional_Doc1").FirstOrDefault();

        //        if (response == null)
        //        {
        //            response = new PreAuthDetails();
        //            response.status = "TRUE";
        //            response.message = "No data available";
        //            response.code = "200";
        //            response.PreauthList = new List<PreAuthList>();
        //        }
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //        throw ex;
        //    }
        //}
        public PreAuthDetails GetPreAuthorizationDetailsService(PreAuthModel preAuthModel) // ADDED by Akshat (28-Feb-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", preAuthModel.Action);
                param.Add("P_SNAID", preAuthModel.UserId);
                param.Add("P_FROMDATE", preAuthModel.Fromdate);
                param.Add("P_TODATE", preAuthModel.ToDate);
                param.Add("P_STATECODE", preAuthModel.StateCode);
                param.Add("P_DISTRICTCODE", preAuthModel.DistCode);
                param.Add("P_HOSPITALCODE", preAuthModel.HospitalCode);
                param.Add("P_TYPE", preAuthModel.Type);
                param.Add("P_PAGEINDEX", preAuthModel.PageNum);
                param.Add("P_PAGESIZE", preAuthModel.PerPage);

                var procedureName = "USP_GET_PREAUTH_STATUS_REPORT";

                var PreAuthDetailsDict = new Dictionary<string, PreAuthDetails>();
                var PreAuthListDict = new Dictionary<string, PreAuthList>();

                var response = SqlConnecton.Query<PreAuthDetails, PreAuthList, PreAuthDoc, PreAuthDetails>(procedureName,
                    (objPreAuthDetails, objPreAuthList, objPreAuthDoc) =>
                    {
                        PreAuthDetails pad;
                        if (!PreAuthDetailsDict.TryGetValue(objPreAuthDetails.code, out pad))
                        {
                            pad = objPreAuthDetails;
                            pad.PreauthList = new List<PreAuthList>();
                            PreAuthDetailsDict.Add(pad.code, pad);
                        }

                        PreAuthList pal;
                        if (!PreAuthListDict.TryGetValue(objPreAuthList.AuthorityCode, out pal))
                        {
                            pal = objPreAuthList;
                            pal.docs = new List<PreAuthDoc>();
                            PreAuthListDict.Add(pal.AuthorityCode, pal);
                        }

                        objPreAuthDoc.DocLink = objPreAuthDoc.Addtional_Doc1 == "NA" ? "NA" : ConfigurationManager.AppSettings["ServiceURL"] + "/api/Common/viewFile" + "?encodedString=" + CommonExtension.encryptString(Convert.ToDateTime(objPreAuthList.RequestedDate).Year + "/" + int.Parse(objPreAuthList.HospitalCode) + "/PREAUTHDOC/" + objPreAuthDoc.Addtional_Doc1);
                        pal.docs.Add(objPreAuthDoc);
                        pad.PreauthList.Add(pal);
                        return pad;
                    },
                    param, commandType: CommandType.StoredProcedure, splitOn: "UrnNo, Addtional_Doc1").FirstOrDefault();

                if (response == null)
                {
                    response = new PreAuthDetails();
                    response.status = "TRUE";
                    response.message = "No data available";
                    response.code = "200";
                    response.PreauthList = new List<PreAuthList>();
                }

                param.Add("P_ACTION", "C");
                var responseCount = SqlConnecton.Query<PreAuthStatusModel>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();

                PreAuthStatus preAuthStatus = new PreAuthStatus();
                foreach (var item in responseCount)
                {
                    switch (item.Description.ToUpperInvariant())
                    {
                        case "FRESH":
                            preAuthStatus.Fresh = item.NoCount;
                            break;
                        case "CANCEL":
                            preAuthStatus.Cancel = item.NoCount;
                            break;
                        case "REJECT":
                            preAuthStatus.Reject = item.NoCount;
                            break;
                        case "APPROVE":
                            preAuthStatus.Approve = item.NoCount;
                            break;
                        case "AUTO_APPROVE":
                            preAuthStatus.Auto_Approve = item.NoCount;
                            break;
                        case "AUTO_REJECT":
                            preAuthStatus.Auto_Reject = item.NoCount;
                            break;
                        case "QUERY":
                            preAuthStatus.Query = item.NoCount;
                            break;
                        case "EXPIRED":
                            preAuthStatus.Expired = item.NoCount;
                            break;
                        case "QUERYSENT":
                            preAuthStatus.QuerySent = item.NoCount;
                            break;
                        case "QUERY_COMPLIED":
                            preAuthStatus.Query_Complied = item.NoCount;
                            break;
                    }
                }
                response.PreAuthStatusCount = new PreAuthStatus();
                response.PreAuthStatusCount = preAuthStatus;
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public PreAuthPackageDetails GetPreAuthPackageDetailsService(PreAuthPackageModel preAuthPackageModel) // ADDED by Akshat (01-Mar-23)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_USERID", OracleDbType.Int64, ParameterDirection.Input, preAuthPackageModel.UserId);
                param.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, preAuthPackageModel.HospitalCode);
                param.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, preAuthPackageModel.Action);
                param.Add("P_PREAUTHID", OracleDbType.Int64, ParameterDirection.Input, preAuthPackageModel.preauthid);
                param.Add("P_PACKAGEDETAILID", OracleDbType.Int64, ParameterDirection.Input, preAuthPackageModel.packagedetailid);
                param.Add("P_P_MSGOUT", OracleDbType.RefCursor, ParameterDirection.Output);
                var procedureName = "USP_SNA_APPROVE_PREAUTH_MOB";

                int count = 0;
                var objResult = new PreAuthPackageDetails { Status = "Success", Message = "Request Successful", Code = "200" };

                var result = SqlConnecton.Query<PreAuthPackageDetails>(procedureName, new[] { typeof(PreAuthPackageList), typeof(PreAuthPackageSnaRemark),
                    typeof(PreAuthPackageHospitalRemark) , typeof(PreAuthPackageHospitalDoc), typeof(PreAuthPackageQueryDoc),
                    typeof(PreAuthPackageQueryDetailsDocModel), typeof(PreAuthPackageImplant), typeof(PreAuthPackageHighEndDrug)
                },
                    (objects) =>
                    {
                        var objPapList = (PreAuthPackageList)objects[0];
                        var objPapSnaRemark = (PreAuthPackageSnaRemark)objects[1];
                        var objPapHospitalRemark = (PreAuthPackageHospitalRemark)objects[2];
                        var objPapHospitalDoc = (PreAuthPackageHospitalDoc)objects[3];
                        var objPapQueryDoc = (PreAuthPackageQueryDoc)objects[4];
                        var objPapQueryDetailsDocModel = (PreAuthPackageQueryDetailsDocModel)objects[5];
                        var objPapImplant = (PreAuthPackageImplant)objects[6];
                        var objPapHighEndDrug = (PreAuthPackageHighEndDrug)objects[7];

                        objPapList.HospitalDocument = new List<PreAuthPackageHospitalDoc>();
                        objPapList.QueryDetails = new List<PreAuthPackageQueryDetails>();
                        objPapList.Implant = new List<PreAuthPackageImplant>();
                        objPapList.HighEndDrugs = new List<PreAuthPackageHighEndDrug>();
                        objPapList.PreAuthHospitalRemarks = new List<PreAuthPackageHospitalRemark>();
                        objPapList.PreAuthSnaRemarks = new List<PreAuthPackageSnaRemark>();

                        objPapHospitalDoc.QueryDocument = new List<PreAuthPackageQueryDoc>();
                        objPapQueryDoc.DocumentDate = objPapHospitalDoc.HospitalUploadDate;
                        objPapQueryDoc.Link = objPapQueryDoc.Addtional_Doc1 == "NA" ? "NA" : ConfigurationManager.AppSettings["ServiceURL"] + "/api/Common/viewFile" + "?encodedString=" + CommonExtension.encryptString(Convert.ToDateTime(objPapHospitalDoc.HospitalUploadDate).Year + "/" + int.Parse(preAuthPackageModel.HospitalCode) + "/PREAUTHDOC/" + objPapQueryDoc.Addtional_Doc1);
                        objPapHospitalDoc.QueryDocument.Add(objPapQueryDoc);
                        objPapList.HospitalDocument.Add(objPapHospitalDoc);

                        PreAuthPackageQueryDetails queryDetails = new PreAuthPackageQueryDetails();
                        PreAuthPackageQueryDetailsDoc queryDetailsDoc = new PreAuthPackageQueryDetailsDoc();

                        queryDetails.SDate = objPapQueryDetailsDocModel.DocTwoDate == null ? "NA" : objPapQueryDetailsDocModel.DocTwoDate;
                        queryDetails.QueryFromSna = objPapSnaRemark.snaremarks1;
                        queryDetails.ReplyFromHospital = objPapQueryDetailsDocModel.ReplySecond;
                        queryDetails.snaremark = objPapSnaRemark.snaremark;
                        queryDetails.queryremark = objPapSnaRemark.queryremark1;
                        queryDetails.snadescription = objPapSnaRemark.snadescription;

                        queryDetailsDoc.Document = objPapQueryDetailsDocModel.Addtional_Doc2;
                        queryDetailsDoc.DocumentDate = queryDetailsDoc.Document == "NA" ? "NA" : queryDetails.SDate;
                        //queryDetailsDoc.Link = objPapQueryDetailsDocModel.Link2;
                        queryDetailsDoc.Link = queryDetailsDoc.Document == "NA" ? "NA" : ConfigurationManager.AppSettings["ServiceURL"] + "/api/Common/viewFile" + "?encodedString=" + CommonExtension.encryptString(Convert.ToDateTime(queryDetails.SDate).Year + "/" + int.Parse(preAuthPackageModel.HospitalCode) + "/PREAUTHDOC/" + objPapQueryDetailsDocModel.Addtional_Doc2);

                        queryDetails.SnaQueryDocument = new List<PreAuthPackageQueryDetailsDoc>();
                        queryDetails.SnaQueryDocument.Add(queryDetailsDoc);
                        objPapList.QueryDetails.Add(queryDetails);

                        queryDetails = new PreAuthPackageQueryDetails();
                        queryDetailsDoc = new PreAuthPackageQueryDetailsDoc();

                        queryDetails.SDate = objPapQueryDetailsDocModel.DocThreeDate == null ? "NA" : objPapQueryDetailsDocModel.DocThreeDate;
                        queryDetails.QueryFromSna = objPapSnaRemark.snaremarks2;
                        queryDetails.ReplyFromHospital = objPapQueryDetailsDocModel.ReplyThird;
                        queryDetails.snaremark = objPapSnaRemark.snaremark;
                        queryDetails.queryremark = objPapSnaRemark.queryremark2;
                        queryDetails.snadescription = objPapSnaRemark.snadescription;

                        queryDetailsDoc.Document = objPapQueryDetailsDocModel.Addtional_Doc3;
                        queryDetailsDoc.DocumentDate = queryDetailsDoc.Document == "NA" ? "NA" : queryDetails.SDate;
                        //queryDetailsDoc.Link = objPapQueryDetailsDocModel.Link3;
                        queryDetailsDoc.Link = queryDetailsDoc.Document == "NA" ? "NA" : ConfigurationManager.AppSettings["ServiceURL"] + "/api/Common/viewFile" + "?encodedString=" + CommonExtension.encryptString(Convert.ToDateTime(queryDetails.SDate).Year + "/" + int.Parse(preAuthPackageModel.HospitalCode) + "/PREAUTHDOC/" + objPapQueryDetailsDocModel.Addtional_Doc3);
                        queryDetails.SnaQueryDocument = new List<PreAuthPackageQueryDetailsDoc>();
                        queryDetails.SnaQueryDocument.Add(queryDetailsDoc);
                        objPapList.QueryDetails.Add(queryDetails);

                        if (objPapImplant == null)
                        {
                            objPapImplant = new PreAuthPackageImplant();
                            objPapImplant.ImplantName = "NA";
                            objPapImplant.Unit = "NA";
                            objPapImplant.UnitCyclePrice = "NA";
                            objPapImplant.ImpAmount = "NA";
                        }
                        //objPapList.Implant.Add(objPapImplant);

                        if (objPapHighEndDrug == null)
                        {
                            objPapHighEndDrug = new PreAuthPackageHighEndDrug();
                            objPapHighEndDrug.HedName = "NA";
                            objPapHighEndDrug.HedUnit = "NA";
                            objPapHighEndDrug.HedPricePerUnit = "NA";
                            objPapHighEndDrug.HedPrice = "NA";
                        }
                        //objPapList.HighEndDrugs.Add(objPapHighEndDrug);

                        objPapList.PreAuthSnaRemarks.Add(objPapSnaRemark);
                        objPapList.PreAuthHospitalRemarks.Add(objPapHospitalRemark);
                        //var objResult = new PreAuthPackageDetails { Status = "Success", Message = "Request Successful", Code = "200" };
                        //objResult.PreAuthDetails = objPapList;
                        if (count > 0)
                        {
                            var findImplant = objResult.PreAuthDetails.Implant.FirstOrDefault(o => o.ImplantId == objPapImplant.ImplantId);
                            if (findImplant == null)
                            {
                                objResult.PreAuthDetails.Implant.Add(objPapImplant);
                            }
                            var findHed = objResult.PreAuthDetails.HighEndDrugs.FirstOrDefault(o => o.HedId == objPapHighEndDrug.HedId);
                            if (findHed == null)
                            {
                                objResult.PreAuthDetails.HighEndDrugs.Add(objPapHighEndDrug);
                            }
                        }
                        else
                        {
                            objPapList.Implant.Add(objPapImplant);
                            objPapList.HighEndDrugs.Add(objPapHighEndDrug);
                            objResult.PreAuthDetails = objPapList;
                        }
                        count++;
                        return objResult;
                    },
                    param, commandType: CommandType.StoredProcedure, splitOn: "SnaRemarks1, HospitalRemarks1, HospitalUploadDate, Addtional_Doc1, " +
                    "Addtional_Doc2, ImplantId, HedId").FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
        public PreAuthApprovalDetails PreAuthApprovalDetailsService(PreAuthApprovalModel preAuthApprovalModel) // ADDED by Akshat (01-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_USERID", preAuthApprovalModel.UserId);
                param.Add("P_ACTION", preAuthApprovalModel.Action);
                param.Add("P_PREAUTHID", preAuthApprovalModel.PreauthId);
                param.Add("P_PACKAGEDETAILID", preAuthApprovalModel.PackageDetailId);
                param.Add("P_REMARK", preAuthApprovalModel.Remark);
                param.Add("P_APPROVEDAMOUNT", preAuthApprovalModel.ApprovedAmount);
                param.Add("P_REMARKSID", preAuthApprovalModel.Remarkid);
                param.Add("P_SNADESCRIPTION", preAuthApprovalModel.Snadescription);
                param.Add("P_MSGOUT");
                var procedureName = "USP_APPROVE_PREAUTH_BY_SNA_MOB";

                int result = SqlConnecton.Query<int>(procedureName, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                PreAuthApprovalDetails obj;
                if (result == 0)
                {
                    string responseMessage = string.Empty;
                    switch (preAuthApprovalModel.Action)
                    {
                        case "1":
                            responseMessage = "Approved Successfully";
                            break;
                        case "2":
                            responseMessage = "Rejected Successfully";
                            break;
                        case "3":
                            responseMessage = "Queried Successfully";
                            break;
                    }
                    obj = new PreAuthApprovalDetails { Status = "true", Message = responseMessage, Code = "200" };
                }
                else
                {
                    obj = new PreAuthApprovalDetails { Status = "false", Message = "Request Failed", Code = "404" };
                }
                return obj;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public SnaRemarksDetails SnaRemarksDetailsService() // ADDED by Akshat (02-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                var procedureName = "USP_SNAREMARKS";

                SnaRemarksDetails obj;
                var result = SqlConnecton.Query<SnaRemarksList>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                if (result == null)
                {
                    obj = new SnaRemarksDetails { Status = "Error", Message = "Request failed", Code = "404" };
                }
                else
                {
                    obj = new SnaRemarksDetails { Status = "Success", Message = "Remarks retreived successfully", Code = "200" };
                    obj.Remarks = new List<SnaRemarksList>();
                    obj.Remarks = result;
                }
                return obj;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public SnaCountDetails SnaCountDetailsService(SnaCountModel snaCountModel) // ADDED by Akshat (06-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", snaCountModel.Action);
                param.Add("P_SNAID", snaCountModel.UserId);
                var procedureName = "USP_GET_PREAUTH_STATUS_REPORT";

                SnaCountDetails obj;
                var result = SqlConnecton.Query<SnaCountData>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                if (result == null)
                {
                    obj = new SnaCountDetails { Status = "Error", Message = "Request failed", Code = "404" };
                }
                else
                {
                    obj = new SnaCountDetails { Status = "Success", Message = "Data fetched successfully", Code = "200" };
                    obj.CountDetails = new SnaCountList
                    {
                        CompletedCaseCount = result.FirstOrDefault(x => x.Description?.ToUpperInvariant().Trim() == "COMPLETEDCASECOUNT")?.NoCount,
                        FreshCaseCount = result.FirstOrDefault(x => x.Description?.ToUpperInvariant().Trim() == "FRESHCASECOUNT")?.NoCount,
                        QuerySentCount = result.FirstOrDefault(x => x.Description?.ToUpperInvariant().Trim() == "QUERYSENT")?.NoCount,
                        QueryCompliedCount = result.FirstOrDefault(x => x.Description?.ToUpperInvariant().Trim() == "QUERYCOMPLIED")?.NoCount
                    };
                }
                return obj;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public IList<ChartBotURNInformation> GetChartbotURNINFormation(string URN)
        {
            List<ChartBotURNInformation> URNInformation = new List<ChartBotURNInformation>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION ", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                    parameters.Add("P_AADHAR ", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    //parameters.Add("P_MESSAGEOUT", OracleDbType.Int64, ParameterDirection.Output);
                    URNInformation = SqlConnecton.Query<ChartBotURNInformation>("USP_GETURNINFO_CHATBOT", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                URNInformation = null;
                log.Error(ex);
            }
            return URNInformation;
        }
        // START SSO
        public PSSOLogin GetSSOLogin(string Username, string Passworrd)
        {
            PSSOLogin GetLoginobj = new PSSOLogin();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("v_P_Username", OracleDbType.Varchar2, ParameterDirection.Input, Username);

                    GetLoginobj = SqlConnecton.Query<PSSOLogin>("USP_T_UserLogin_TMS", parameters, commandType: CommandType.StoredProcedure).Select(s => new PSSOLogin
                    {
                        userid = s.userid,
                        username = s.username,
                        designation = !string.IsNullOrEmpty(s.designation) ? s.designation : string.Empty,
                        state = !string.IsNullOrEmpty(s.state) ? s.state : string.Empty,
                        district = !string.IsNullOrEmpty(s.district) ? s.district : string.Empty,
                        Hospital = !string.IsNullOrEmpty(s.Hospital) ? s.Hospital : string.Empty,
                        Hospitalcode = !string.IsNullOrEmpty(s.Hospitalcode) ? s.Hospitalcode : string.Empty,
                        tokenkey = s.tokenkey
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                GetLoginobj = null;
                log.Error(ex);
            }
            return GetLoginobj;
        }
        // END SSO
        public List<AdmissionStats> AdmissionReportService(ReportModel reportModel) // ADDED by Akshat (16-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                var procedureName = "USP_T_GETREPORTSHOSPITALWISE_TMS";

                var result = SqlConnecton.Query<AdmissionStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<BlockedStats> BlockedReportService(ReportModel reportModel) // ADDED by Akshat (16-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                var procedureName = "USP_T_GETREPORTSHOSPITALWISE_TMS";

                var result = SqlConnecton.Query<BlockedStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<UnblockedStats> UnblockedReportService(ReportModel reportModel) // ADDED by Akshat (16-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                var procedureName = "USP_T_GETREPORTSHOSPITALWISE_TMS";

                var result = SqlConnecton.Query<UnblockedStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<DischargeStats> DischargeReportService(ReportModel reportModel) // ADDED by Akshat (16-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                var procedureName = "USP_T_GETREPORTSHOSPITALWISE_TMS";

                var result = SqlConnecton.Query<DischargeStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public List<PreAuthStats> PreAuthReportService(ReportModel reportModel) // ADDED by Akshat (16-Mar-23)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("P_ACTION", reportModel.Action);
                param.Add("P_HOSPITALCODE", reportModel.HospitalCode);
                param.Add("P_FROMDATE", reportModel.FromDate);
                param.Add("P_TODATE", reportModel.ToDate);
                var procedureName = "USP_T_GETREPORTSHOSPITALWISE_TMS";

                var result = SqlConnecton.Query<PreAuthStats>(procedureName, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public IList<ChartBotBskyholderHospitalModel> GetBskyHolderHospitalName(string URN, string memberid)
        {
            List<ChartBotBskyholderHospitalModel> URNInformation = new List<ChartBotBskyholderHospitalModel>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION ", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                    parameters.Add("P_URN ", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    parameters.Add("P_MEMBERID ", OracleDbType.Int64, ParameterDirection.Input, int.Parse(memberid));
                    URNInformation = SqlConnecton.Query<ChartBotBskyholderHospitalModel>("USP_STATUS_URNINFO", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                URNInformation = null;
                log.Error(ex);
            }
            return URNInformation;
        }

        #region :: Kisan  
        public Login UpdateLoginAttempt(string Username, string Action)
        {
            Login GetLoginobj = new Login();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, Username);
                    GetLoginobj = SqlConnecton.Query<Login>("USP_T_INVALIDLOGIN_TMS", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                GetLoginobj = null;
                log.Error(ex);
            }
            return GetLoginobj;
        }

        public IList<ChartBotFamilyHeadInformation> GetChartbotURNFamilyHeadInformation(string URN)
        {
            List<ChartBotFamilyHeadInformation> URNInformation = new List<ChartBotFamilyHeadInformation>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION ", OracleDbType.Varchar2, ParameterDirection.Input, "B");
                    parameters.Add("P_AADHAR ", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    URNInformation = SqlConnecton.Query<ChartBotFamilyHeadInformation>("USP_GETURNINFO_CHATBOT", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                URNInformation = null;
                log.Error(ex);
            }
            return URNInformation;
        }
        public IList<Cardbalanceinfochatbot> GetCardbalanceinfochatbot(string URN)
        {
            List<Cardbalanceinfochatbot> URNInformation = new List<Cardbalanceinfochatbot>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "A");
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    URNInformation = SqlConnecton.Query<Cardbalanceinfochatbot>("USP_CardBalanceInfo_Charbot_TMS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                URNInformation = null;
                log.Error(ex);
            }
            return URNInformation;
        }

        public IList<MOMRStatus> Check_MOMR_Package(int memberID, string urn, string hospitalcode)
        {
            List<MOMRStatus> URNInformation = new List<MOMRStatus>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_MEMBERID ", OracleDbType.Int64, ParameterDirection.Input, memberID);
                    parameters.Add("P_URN ", OracleDbType.Varchar2, ParameterDirection.Input, urn);
                    parameters.Add("P_HOSPITAL_CODE ", OracleDbType.Varchar2, ParameterDirection.Input, hospitalcode);
                    URNInformation = SqlConnecton.Query<MOMRStatus>("USP_CHECK_MOMR", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                URNInformation = null;
                log.Error(ex);
            }
            return URNInformation;
        }

        public List<MOUStatusModel> Get_MOU_Status_Check(string hospitalcode)
        {
            List<MOUStatusModel> MOUstausData = new List<MOUStatusModel>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, hospitalcode);

                using (SqlConnecton)
                {
                    MOUstausData = SqlConnecton.Query<MOUStatusModel>("USP_MOUSTATUS_TMS", param, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                MOUstausData = null;
                log.Error(ex);
            }
            return MOUstausData;
        }
        #endregion:: 

        public IList<ActionTakenByUser> GetAllActionTakenByUserService()
        {
            List<ActionTakenByUser> UserData = new List<ActionTakenByUser>();
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, "SU");
                param.Add("P_REFERRALTYPE", OracleDbType.Int16, ParameterDirection.Input, 0);
                using (SqlConnecton)
                {
                    UserData = SqlConnecton.Query<ActionTakenByUser>("USP_ADMIN_VIEWREPORT", param, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                UserData = null;
                log.Error(ex);
            }
            return UserData;
        }
    }
}

