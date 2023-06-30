using HPSBYS.Data.Model;
using HPSBYS.Data.Services;
using HPSBYS.Web.Fiilters;
using HPSBYS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HPSBYS.Web.Controllers
{
    [SessionTimeOutFilter]
    [Authorize]
    [NoDirectAccess]
    public class UnblockPackageController : Controller
    {
        // GET: UnblockPackage
        [HttpGet]
        public ViewResult AddUnblockPackage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ViewUnblockPackage()
        {

            string groupid = Convert.ToString(Session["groupid"]);
            if (groupid != "1")
            {
                return View();

            }
            else
            {
                return RedirectToAction("adminViewUnblockPackageDetails", "UnblockPackage");
            }
        }

        [HttpGet]
        public ViewResult adminViewUnblockPackageDetails()          //purpose for admin view
        {
            return View();
        }

        [HttpGet]
        public ViewResult ViewUnblockPackageDetails()
        {
            return View();
        }

        // Vishal
        //[HttpGet]
        //public async Task<ActionResult> UnBlockSlip(UnBlockPackageSlip obj) //Rajkishor Patra(10-Feb-23)
        //{
        //    List<UnBlockPackageSlip> Viewunblockpackagelist = new List<UnBlockPackageSlip>();
        //    using (var patientDataServices = new PatientDataServices())
        //    {
        //        Viewunblockpackagelist = (List<UnBlockPackageSlip>)await Task.FromResult(patientDataServices.GetViewUnBlockPackageSlip(obj));
        //        return PartialView("UnBlockSlip", Viewunblockpackagelist);

        //    }
        //}
        public ActionResult UnBlockSlip()
        {
            return View();
        }
        public ActionResult UnBlockHospitalSlip()
        {
            return View();
        }
    }
}