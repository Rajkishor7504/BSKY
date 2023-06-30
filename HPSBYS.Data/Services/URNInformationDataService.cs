using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPSBYS.Data.Model;
using Dapper;
using Dapper.Contrib;
using NLog;
using AdminConsole.Persistence;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace HPSBYS.Data.Services
{
   public class URNInformationDataService : BaseDataService
    {
        public IList<URNInformation> GetURNINFormation(int Schemecode, string URN)
        {
            List<URNInformation> URNInformation = new List<URNInformation>();
            ILogger log = LogManager.GetCurrentClassLogger();
            try
            {
                using (SqlConnecton)
                {
                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_Schemecode", OracleDbType.Int64, ParameterDirection.Input, Schemecode);
                    parameters.Add("P_URN", OracleDbType.Varchar2, ParameterDirection.Input, URN);
                    parameters.Add("P_SEARCHBY", OracleDbType.Int64, ParameterDirection.Input, null);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, null);
                    URNInformation = SqlConnecton.Query<URNInformation>("USP_T_GetURN_INFO_TMS_GetURN", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {   
                URNInformation = null;
                log.Error(ex);
            }
            return URNInformation;
        }

        public IList<URNInformation> GetFamilyMemeberList(string URN)
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
                    URNInformation = SqlConnecton.Query<URNInformation>("USP_T_GetURN_INFO_TMS_getFamily", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                URNInformation = null;
                log.Error(ex);
            }
            return URNInformation;
        }
        public IList<URNInformation> SearchFamilyMemeberList(string URN, int? searchBy, string hospitalcode)
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
                    parameters.Add("P_SEARCHBY", OracleDbType.Int64, ParameterDirection.Input, searchBy);
                    parameters.Add("P_HOSPITALCODE", OracleDbType.Varchar2, ParameterDirection.Input, hospitalcode);
                    URNInformation = SqlConnecton.Query<URNInformation>("USP_T_GetURN_INFO_TMS_SrchFamily", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                URNInformation = null;
                log.Error(ex);
            }
            return URNInformation;
        }

    }
}

 