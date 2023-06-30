using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HPSBYS.Data.Model;
using HPSBYS.Data.Services;
namespace HPSBYS.WebAPI.Controllers
{
    public class HospitalController : ApiController
    {
        [HttpGet]
        public async Task<HospitalDetails> GetHospitalInformation(string HospitalCode)
        {
            using (var hospitalDetailsDataServices = new HospitalDetailsDataServices())
            {
                return await Task.FromResult(hospitalDetailsDataServices.GetHospitalInformation(HospitalCode));
            }
        }

        //[HttpGet]
        //public async Task<IList<Scheme>> GetSchemeList(string Schemecode)
        //{
        //    using (var SchemeInformation = new HospitalDetailsDataServices())
        //    {
        //        return await Task.FromResult(SchemeInformation.GetScheme(Schemecode));
        //    }
        //}

        //[HttpGet]
        //public async Task<IList<PACKAGECATEGORY>> GetPackageCategory(string Action)
        //{
        //    using (var CATEGORYdata = new HospitalDetailsDataServices())
        //    {
        //        return await Task.FromResult(CATEGORYdata.GetPACKAGECATEGORY(Action));
        //    }
        //}

        //[HttpGet]
        //public async Task<IList<PackageInformation>> GetPackage(string Action,string PackageCategoryCode)
        //{
        //    using (var packagedatadata = new HospitalDetailsDataServices())
        //    {
        //        return await Task.FromResult(packagedatadata.GetPackageDetail(Action, PackageCategoryCode));
        //    }
        //}







        //public async Task<string> AddPatientRegistration(PatientInfo obj)
        //{
        //    using (var PatientDataServices = new PatientDataServices())
        //    {
        //        return await Task.FromResult(PatientDataServices.ManagePatientRegistration(obj));
        //    }
        //}
        //[Authorize]
        [HttpPost]
        [Route("api/Hospital/getCaseInformationDetails")]
        public async Task<IHttpActionResult> getCaseInformationDetails(CaseInformationModel model)
        {
            IGenericResult<string> result = new GenericResult<string>();
            using (var hospitalDetailsDataServices = new HospitalDetailsDataServices())
            {
                var data = await Task.FromResult(hospitalDetailsDataServices.GetCaseInformationDetails(model));
                if (data.packageInformation != null)
                {
                    result.Status = true;
                    result.Message = "Case information fetched successfully";
                    result.Code = 200;
                    result.Data = data;
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
        //[Authorize]
        [HttpPost]
        [Route("api/Hospital/getPreauthTreatmentDetails")]
        public async Task<IHttpActionResult> getPreauthTreatmentDetails(CaseInformationModel model)
        {
            IGenericResult<string> result = new GenericResult<string>();
            using (var hospitalDetailsDataServices = new HospitalDetailsDataServices())
            {
                var data = await Task.FromResult(hospitalDetailsDataServices.getTreatmentDetails(model));
                if (data.hospitalInfo != null)
                {
                    result.Status = true;
                    result.Message = "Treatment details fetched successfully.";
                    result.Code = 200;
                    result.Data = data;
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
    }
}