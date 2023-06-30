using HPSBYS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HPSBYS.Data.Services;
namespace HPSBYS.WebAPI.Controllers
{
    public class DischargeController : ApiController
    {
        [HttpPost]
        public async Task<string> addPatientDischarge(PatientInfo obj)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                return await Task.FromResult(PatientDataServices.addPatientDischarge(obj));
            }
        }

        [HttpPost]
        [Route("api/Discharge/GetAllDischarge")]
        public async Task<IHttpActionResult> GetAllDischarge(DischargeFilterModel model)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                if (model.groupid != "1")
                {
                    var data = await Task.FromResult(PatientDataServices.GetAllDischargeRecord(model));
                    return Ok(data);
                }
                else
                {
                    model.Action = "DS";
                    var data = await Task.FromResult(PatientDataServices.GetadminAllDischargeRecord(model));
                    return Ok(data);
                }

            }
        }


        [HttpPost]
        [Route("api/Discharge/GetDischargeSlip")]
        public async Task<IHttpActionResult> GetDischargeSlip(DischargeFilterModel model)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.GetAllDischargeSlip(model));
                return Ok(data);
            }
        }

        [HttpPost]
        public async Task<List<PatientReferral>> GetPatientReferralList(PatientReferral obj) // ADDED by Akshat (09-Feb-23)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                return await Task.FromResult(PatientDataServices.GetPatientReferralListService(obj));
            }
        }

        [HttpPost]
        [Route("api/Discharge/GetDischargeReferalSlip")]
        public async Task<IHttpActionResult> GetDischargeReferalSlip(DischargeFilterModel model)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.GetAllDischargeReferalSlip(model));
                return Ok(data);
            }
        }
        [HttpPost]
        [Route("api/Discharge/GetDischargeDetails")]
        public async Task<IHttpActionResult> GetDischargeDetails(DischargeFilterModel model)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                if (model.groupid != "1")
                {
                    var data = await Task.FromResult(PatientDataServices.GetPatientDischargeViwe(model));
                    return Ok(data);
                }
                else
                {
                    var data = await Task.FromResult(PatientDataServices.GetadminPatientDischargeViwe(model));
                    return Ok(data);
                }

            }
        }


        [HttpGet]
        [Route("api/Discharge/GetPatientReferralViewDetails")]
        public async Task<IHttpActionResult> GetPatientReferralViewDetails(string referralid)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.GetPatientReferralViewDetails(referralid));
                return Ok(data);
            }
        }
    }
}
