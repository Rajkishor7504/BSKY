using HPSBYS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HPSBYS.Data.Services;
using System.Threading.Tasks;
using HPSBYS.WebAPI.Models;

namespace HPSBYS.WebAPI.Controllers
{
    public class UnblockPackageController : ApiController
    {       
        [HttpPost]
        public async Task<string> addPatientUnblockPackage(PatientInfo obj)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                return await Task.FromResult(PatientDataServices.addPatientUnblockPackage(obj));
            }

        }

        #region :: Kisan 
        [HttpPost]
        public async Task<IHttpActionResult> CreatePatientUnblockPackage(UnblockPackageModel obj)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.CreatePatientUnblockPackage(obj));
                return Ok(data);
            }
        }
        #endregion

        [HttpPost]
        public async Task<List<ViewUN_Blockpackagedetails>> GetViewUnBlockpackagedetailsList(ViewUN_Blockpackagedetails obj) // ADDED by Rajkishor patra (07-feb-23)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                if (obj.groupid != "1")
                {
                    var data = await Task.FromResult(PatientDataServices.GetViewUnBlockPackageDetails(obj));
                    return data.ToList();
                }
                else
                {
                    var data = await Task.FromResult(PatientDataServices.GetadminViewUnBlockPackageDetails(obj));
                    return data.ToList();
                }

            }
        }


        [HttpPost]
        public async Task<List<ViewUN_Blockpackagedetails>> GetViewUNBlockverifieddetailsListByID(ViewUN_Blockpackagedetails obj) // ADDED by Rajkishor patra (08-feb-23)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.GetViewUnBlockverifiedDetailsById(obj));
                return data.ToList();
            }
        }
        [HttpPost]
        public async Task<List<ViewUN_Blockpackagedetails>> GetViewUNBlockpackageddetailsListByID(ViewUN_Blockpackagedetails obj) // ADDED by Rajkishor patra (08-feb-23)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.GetViewUnBlockpackageDetailsById(obj));
                return data.ToList();
            }
        }

        [HttpPost]
        public async Task<List<ViewUN_Blockpackagedetails>> GetUnBlockAdmissionDetailsListByID(ViewUN_Blockpackagedetails obj) // ADDED by Rajkishor patra (08-feb-23)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var AdmissionDetails = await Task.FromResult(PatientDataServices.GetViewUNBlockAdmissionDetailsById(obj));
                return AdmissionDetails.ToList();
            }
        }
        [HttpPost]
        public async Task<List<ViewUN_Blockpackagedetails>> GetUnBlockVitalparameterListByID(ViewUN_Blockpackagedetails obj) // ADDED by Rajkishor patra (08-feb-23)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var vitalaparameter = await Task.FromResult(PatientDataServices.GetViewUnBlockVitalParameterById(obj));

                return vitalaparameter.ToList();
            }
        }
        [HttpPost]//Add By Rajkishor Patra(20-feb-2023)
        public async Task<List<ViewUN_Blockpackagedetails>> GetUnBlockImplantDetailsListByID(ViewUN_Blockpackagedetails obj)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var vitalaparameter = await Task.FromResult(PatientDataServices.GetViewUnBlockImplantDetailsListByID(obj));

                return vitalaparameter.ToList();
            }
        }
        [HttpPost]//Add By Rajkishor Patra(20-feb-2023)
        public async Task<List<ViewUN_Blockpackagedetails>> GetUnBlockHighendDrugDetailsListByID(ViewUN_Blockpackagedetails obj)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var vitalaparameter = await Task.FromResult(PatientDataServices.GetViewUnBlockHighendDrugDetailsListByID(obj));

                return vitalaparameter.ToList();
            }
        }

        [HttpPost]
        [Route("api/UnblockPackage/GetUnblockSlip")]
        public async Task<IHttpActionResult> GetUnblockSlip(UnBlockPackageSlip model)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.GetViewUnBlockPackageSlip(model));
                return Ok(data);
            }
        }
        [HttpPost]
        [Route("api/UnblockPackage/GetUnBlockDetailsView")]
        public async Task<IHttpActionResult> GetUnBlockDetailsView(UnblockDetailsViewParams model)
        {
            using (var PatientDataServices = new PatientDataServices())
            {
                var data = await Task.FromResult(PatientDataServices.GetUnblockDetailsView(model));
                return Ok(data);
            }
        }
    }
}
