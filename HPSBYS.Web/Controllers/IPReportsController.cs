using HPSBYS.Data.Model;
using HPSBYS.Web.Fiilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HPSBYS.Web.Controllers
{
    [NoDirectAccess]
    public class IPReportsController : Controller
    {
        // GET: IPReports
        public ActionResult ViewPatientStatus()
        {
            return View();
        }
        public ActionResult HospitalDetailsReport()
        
            {
                string groupid = Convert.ToString(Session["groupid"]);
                if (groupid != "1")
                {
                    return View();

                }
                else
                {
                    return RedirectToAction("adminHospitalDetailsReport", "IPReports");
                }
        }
        public ActionResult adminHospitalDetailsReport() //for admin view
        {
            return View();
        }

        public ActionResult HospitalReferralReport()
        {
            string groupid = Convert.ToString(Session["groupid"]);
            if (groupid != "1")
            {
                return View();

            }
            else
            {
                return RedirectToAction("adminHospitalReferralReport", "IPReports");
            }
        }
        public ActionResult adminHospitalReferralReport() //for admin view
        {
            return View();
        }


        public ActionResult KnowYourStatus()
        {
            string groupid = Convert.ToString(Session["groupid"]);
            if (groupid != "1")
            {
                return View();

            }
            else
            {
                return RedirectToAction("adminKnowYourStatus", "IPReports");
            }
        }
        public ActionResult adminKnowYourStatus() //for admin view
        {
            return View();
        }

        public ActionResult HospitalPreAuthReport()
        {
            string groupid = Convert.ToString(Session["groupid"]);
            if (groupid != "1")
            {
                return View();

            }
            else
            {
                return RedirectToAction("adminHospitalPreAuthReport", "IPReports");
            }
        }
        public ActionResult adminHospitalPreAuthReport() //for admin view
        {
            return View();
        }


        public ActionResult HospitalMortalityReport()
        {
            string groupid = Convert.ToString(Session["groupid"]);
            if (groupid != "1")
            {
                return View();

            }
            else
            {
                return RedirectToAction("adminHospitalMortalityReport", "IPReports");
            }
        }
        public ActionResult adminHospitalMortalityReport() //for admin view
        {
            return View();
        }


        public ActionResult AuthenticationDetails()
        {
            string groupid = Convert.ToString(Session["groupid"]);
            if (groupid != "1")
            {
                return View();

            }
            else
            {
                return RedirectToAction("adminNewOverRideDetails", "IPReports");
            }
        }
        public ActionResult adminNewOverRideDetails() //for admin view
        {
            return View();
        }


        public ActionResult HospitalPackageReport()
        {
            string groupid = Convert.ToString(Session["groupid"]);
            if (groupid != "1")
            {
                return View();
            }
            else
            {
                return RedirectToAction("adminHospitalPackageReport", "IPReports");
            }
        }
        public ActionResult adminHospitalPackageReport() //for admin view
        {
            return View();
        }

        public ActionResult PatientMobileVerificationReport()
        {
            string groupid = Convert.ToString(Session["groupid"]);
            if (groupid != "1")
            {
                return View();
            }
            else
            {
                return RedirectToAction("adminPatientMobileVerificationReport", "IPReports");
            }
        }
        public ActionResult adminPatientMobileVerificationReport() //for admin view
        {
            return View();
        }
    }
}