using AdminConsole.Persistence;
using Dapper;
using HPSBYS.Data.Model;
using NLog;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Services
{
    public class NewOverRideService : BaseDataService
    {
        ILogger log = LogManager.GetCurrentClassLogger();
        public IList<NewOverRideDetails> GetNewOverRideDetailsService(string Action, string HospitalCode, string Fromdate, string Todate)
        {
            List<NewOverRideDetails> ob = new List<NewOverRideDetails>();
            try
            {
                using (SqlConnecton)
                {
                    //URNInformation = SqlConnecton.Query<URNInformation>("Exec USP_T_GetURN_INFO @P_Schemecode=" + Schemecode + ",@P_URN='" + URN + "'").ToList();
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("p_hospitalcode", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input, Fromdate);
                    parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input, Todate);
                    parameters.Add("CUR", OracleDbType.RefCursor, ParameterDirection.Output);


                    ob = SqlConnecton.Query<NewOverRideDetails>("SP_AUTHENTICATION_REPORTHOS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                ob = null;
                log.Error(ex);
            }
            return ob;
        }
        //public IList<NewOverRideView> GetOverRideViewService(string Action, string HospitalCode, string VARIFIEDTHROUGH, string from_date, string to_date)
        //{
        //    List<NewOverRideView> ob = new List<NewOverRideView>();
        //    try
        //    {
        //        using (SqlConnecton)
        //        {
        //            //URNInformation = SqlConnecton.Query<URNInformation>("Exec USP_T_GetURN_INFO @P_Schemecode=" + Schemecode + ",@P_URN='" + URN + "'").ToList();
        //            var parameters = new OracleDynamicParameters();
        //            parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
        //            parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
        //            parameters.Add("P_VARIFIEDTHROUGH", OracleDbType.Varchar2, ParameterDirection.Input, VARIFIEDTHROUGH);


        //            ob = SqlConnecton.Query<NewOverRideView>("SP_AUTHVIEW_REPORTHOS", parameters, commandType: CommandType.StoredProcedure).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ob = null;
        //        log.Error(ex);
        //    }
        //    return ob;
        //}
        public IList<NewOverRideView> GetOverRideViewService(string Action, string HospitalCode, string VARIFIEDTHROUGH, string from_date, string to_date)
        {
            List<NewOverRideView> ob = new List<NewOverRideView>();
            try
            {
                using (SqlConnecton)
                {
                    //URNInformation = SqlConnecton.Query<URNInformation>("Exec USP_T_GetURN_INFO @P_Schemecode=" + Schemecode + ",@P_URN='" + URN + "'").ToList();
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("P_VARIFIEDTHROUGH", OracleDbType.Varchar2, ParameterDirection.Input, VARIFIEDTHROUGH);
                    parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input, from_date);
                    parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input, to_date);


                    ob = SqlConnecton.Query<NewOverRideView>("SP_AUTHVIEW_REPORTHOS", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                ob = null;
                log.Error(ex);
            }
            return ob;
        }

        //for admin

        public IList<NewOverRideDetails> adminGetNewOverRideDetailsService(string Action, string HospitalCode, string Fromdate, string Todate, string statecode, string districtcode)
        {
            List<NewOverRideDetails> ob = new List<NewOverRideDetails>();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ACTION", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode == "null" ? "0" : HospitalCode);
                    parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input, Fromdate);
                    parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input, Todate);
                    parameters.Add("CUR", OracleDbType.RefCursor, ParameterDirection.Output);
                    parameters.Add("P_STATECODE", OracleDbType.Varchar2, ParameterDirection.Input, statecode);
                    parameters.Add("P_DISTRICTCODE", OracleDbType.Varchar2, ParameterDirection.Input, districtcode);

                    ob = SqlConnecton.Query<NewOverRideDetails>("SP_AUTHENTICATION_REPORT", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                ob = null;
                log.Error(ex);
            }
            return ob;
        }
        public IList<NewOverRideView> GetAdminAuthViewService(string Action, string HospitalCode, string statecode, string districtcode, string VARIFIEDTHROUGH, string from_date, string to_date)
        {
            List<NewOverRideView> ob = new List<NewOverRideView>();
            try
            {
                using (SqlConnecton)
                {
                    //URNInformation = SqlConnecton.Query<URNInformation>("Exec USP_T_GetURN_INFO @P_Schemecode=" + Schemecode + ",@P_URN='" + URN + "'").ToList();
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("p_action", OracleDbType.Varchar2, ParameterDirection.Input, Action);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, HospitalCode);
                    parameters.Add("P_STATECODE", OracleDbType.Varchar2, ParameterDirection.Input, statecode);
                    parameters.Add("P_DISTRICTCODE", OracleDbType.Varchar2, ParameterDirection.Input, districtcode);
                    parameters.Add("p_varifiedthrough", OracleDbType.Varchar2, ParameterDirection.Input, VARIFIEDTHROUGH);
                    parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input, from_date);
                    parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input, to_date);


                    ob = SqlConnecton.Query<NewOverRideView>("sp_authview_report", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                ob = null;
                log.Error(ex);
            }
            return ob;
        }

        //END

    }
}


