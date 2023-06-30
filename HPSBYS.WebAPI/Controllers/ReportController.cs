using HPSBYS.Data.Model;
using HPSBYS.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HPSBYS.WebAPI.Controllers
{
    public class ReportController : ApiController
    {
        [HttpPost]
        public async Task<List<AdmissionStats>> AdmissionReport(ReportModel model) // ADDED by Akshat (16-Mar-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.AdmissionReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<BlockedStats>> BlockedReport(ReportModel model) // ADDED by Akshat (16-Mar-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.BlockedReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<DischargeStats>> DischargeReport(ReportModel model) // ADDED by Akshat (16-Mar-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.DischargeReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<PreAuthStats>> PreAuthReport(ReportModel model) // ADDED by Akshat (16-Mar-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.PreAuthReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<UnblockedStats>> UnblockedReport(ReportModel model) // ADDED by Akshat (16-Mar-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.UnblockedReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<HospitalReferralStats>> HospitalReferralReport(ReportModel model) // ADDED by Akshat (23-Mar-23)
        {
            using (var obj = new ReportDataServices())
            {
                if (model.groupid != "1")
                {
                    return await Task.FromResult(obj.HospitalReferralReportService(model));
                }
                else
                {
                    return await Task.FromResult(obj.adminHospitalReferralReportService(model));
                }

            }
        }



        [HttpPost]
        public async Task<List<KnowYourStatusModel>> GetKnowYourStatusReport(KnowYourStatusModel obj)
        {
            using (var reportDataServices = new ReportDataServices())
            {
                var gethospitalpatiantdetails = await Task.FromResult(reportDataServices.GetKnowYourStatusReportList(obj));
                return gethospitalpatiantdetails.ToList();
            }
        }


        [HttpPost]
        public async Task<List<HospitalPreAuthStats>> HospitalPreAuthReport(ReportModel model) // ADDED by Akshat (12-Apr-23)
        {
            using (var obj = new ReportDataServices())
            {
                if (model.groupid != "1")
                {
                    return await Task.FromResult(obj.HospitalPreAuthReportService(model));
                }
                else
                {
                    return await Task.FromResult(obj.adminHospitalPreAuthReportService(model));
                }

            }
        }


        [HttpPost]
        public async Task<DashboardStats> DashboardStats(ReportModel model) // ADDED by Akshat (30-Mar-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.DashboardStatsService(model));
            }
        }

        [HttpPost]
        public async Task<DashboardStats> adminDashboardStats(ReportModel model) // ADDED by Akshat (26-Jun-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.adminDashboardStatsService(model));
            }
        }

        [HttpPost]
        public async Task<List<HospitalMortalityStats>> HospitalMortalityReport(ReportModel model) // ADDED by Akshat (24-Apr-23)
        {
            using (var obj = new ReportDataServices())
            {
                if (model.groupid != "1")
                {
                    return await Task.FromResult(obj.HospitalMortalityReportService(model));
                }
                else
                {
                    return await Task.FromResult(obj.adminHospitalMortalityReportService(model));
                }

            }
        }


        [HttpPost]
        public async Task<IList<NewOverRideDetails>> GetNewOverRideDetails(AuthenticationDetalModel model)
        {
            using (var overrideservice = new NewOverRideService())
            {
                if (model.groupid != "1")
                {
                    return await Task.FromResult(overrideservice.GetNewOverRideDetailsService(model.Action, model.HospitalCode, model.fromdate, model.todate));

                }
                else
                {
                    return await Task.FromResult(overrideservice.adminGetNewOverRideDetailsService(model.Action, model.HospitalCode, model.fromdate, model.todate, model.statecode, model.districtcode));
                }
            }
        }
        //for admin 
        [HttpGet]
        public async Task<IList<NewOverRideView>> GetAdminAuthView(string Action, string HospitalCode, string statecode, string districtcode, string VARIFIEDTHROUGH, string from_date, string to_date)
        {
            using (var overrideservice = new NewOverRideService())
            {
                return await Task.FromResult(overrideservice.GetAdminAuthViewService(Action, HospitalCode, statecode, districtcode, VARIFIEDTHROUGH, from_date, to_date));
            }
        }


        //[HttpGet]
        //public async Task<IList<NewOverRideView>> GetOverRideView(string Action, string HospitalCode, string VARIFIEDTHROUGH)
        //{
        //    using (var overrideservice = new NewOverRideService())
        //    {
        //        return await Task.FromResult(overrideservice.GetOverRideViewService(Action, HospitalCode, VARIFIEDTHROUGH));
        //    }
        //}

        [HttpGet]
        public async Task<IList<NewOverRideView>> GetOverRideView(string Action, string HospitalCode, string VARIFIEDTHROUGH, string from_date, string to_date)
        {
            using (var overrideservice = new NewOverRideService())
            {
                return await Task.FromResult(overrideservice.GetOverRideViewService(Action, HospitalCode, VARIFIEDTHROUGH, from_date, to_date));
            }
        }

        [HttpPost]
        public async Task<List<HospitalPackageBlockStats>> HospitalPackageBlockReport(ReportModel model) // ADDED by Akshat (01-Jun-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.HospitalPackageBlockReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<HospitalPackageUnblockStats>> HospitalPackageUnblockReport(ReportModel model) // ADDED by Akshat (01-Jun-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.HospitalPackageUnblockReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<HospitalPackageDischargeStats>> HospitalPackageDischargeReport(ReportModel model) // ADDED by Akshat (01-Jun-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.HospitalPackageDischargeReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<HospitalPackageBlockStats>> adminHospitalPackageBlockReport(ReportModel model) // ADDED by Akshat (22-Jun-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.adminHospitalPackageBlockReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<HospitalPackageUnblockStats>> adminHospitalPackageUnblockReport(ReportModel model) // ADDED by Akshat (22-Jun-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.adminHospitalPackageUnblockReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<HospitalPackageDischargeStats>> adminHospitalPackageDischargeReport(ReportModel model) // ADDED by Akshat (22-Jun-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.adminHospitalPackageDischargeReportService(model));
            }
        }

        [HttpPost]
        public async Task<List<PatientMobileVerificationStats>> PatientMobileVerificationReport(ReportModel model) // ADDED by Akshat (01-Jun-23)
        {
            using (var obj = new ReportDataServices())
            {
                return await Task.FromResult(obj.PatientMobileVerificationReportService(model));
            }
        }
    }
}
