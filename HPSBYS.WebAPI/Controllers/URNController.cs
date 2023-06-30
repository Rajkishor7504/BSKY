using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HPSBYS.Data.Model;
using HPSBYS.Data.Services;
using NLog;

namespace HPSBYS.WebAPI.Controllers
{
    public class URNController : ApiController
    {
        [HttpGet]
        public async Task<IList<URNInformation>> GetURNList(int Schemecode, string URN)
        {
            using (var URNInformationDataService = new URNInformationDataService())
            {
                return await Task.FromResult(URNInformationDataService.GetURNINFormation(Schemecode, URN));
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetFamilyMemeberList(string URN)
        {
            using (var URNInformationDataService = new URNInformationDataService())
            {
                var data = await Task.FromResult(URNInformationDataService.GetFamilyMemeberList(URN));
                return Ok(data);
            }
        }
        [HttpGet]
        public async Task<IHttpActionResult> SearchFamilyMemeberList(string URN, int? searchBy, string hospitalcode)
        {
            using (var URNInformationDataService = new URNInformationDataService())
            {
                var data = await Task.FromResult(URNInformationDataService.SearchFamilyMemeberList(URN, searchBy, hospitalcode));
                return Ok(data);
            }
        }
    }
}

