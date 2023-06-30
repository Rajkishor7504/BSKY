using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HPSBYS.Web.App_Start
{
    public class BundleConfig
    {
         public static void RegisterBundles(BundleCollection bundles)
         {
            bundles.Add(new ScriptBundle("~/bundles/viewreferaldischarge").Include("~/js/DischargePackageView/viewreferaldischarge.js"));
            bundles.Add(new ScriptBundle("~/bundles/viewblockpackagedetails").Include("~/js/BlockPackageView/viewblockpackagedetails.js"));
            bundles.Add(new ScriptBundle("~/bundles/overriderequest").Include("~/js/OverRideView/OverRideRequest.js"));
            bundles.Add(new ScriptBundle("~/bundles/viewunblockpackagedetails").Include("~/js/UnBlockPackageView/viewunblockpackagedetails.js"));
            bundles.Add(new ScriptBundle("~/bundles/hospitaldetailsreport").Include("~/js/Reports/hospitaldetailsreport.js"));
            bundles.Add(new ScriptBundle("~/bundles/hospitalreferralreport").Include("~/js/Reports/hospitalreferralreport.js"));
            bundles.Add(new ScriptBundle("~/bundles/knowyourstatusreport").Include("~/js/Reports/knowyourstatusreport.js"));
            //bundles.Add(new ScriptBundle("~/bundles/knowyourstatusreport").Include("~/js/Reports/knowyourstatusreport.js"));
            bundles.Add(new ScriptBundle("~/bundles/hospitalpreauthreport").Include("~/js/Reports/hospitalpreauthreport.js"));
            bundles.Add(new ScriptBundle("~/bundles/hospitalmortalityreport").Include("~/js/Reports/hospitalmortalityreport.js"));
            bundles.Add(new ScriptBundle("~/bundles/patientreferralform").Include("~/js/ReferralView/patientreferralform.js"));
            bundles.Add(new ScriptBundle("~/bundles/patientreferralview").Include("~/js/ReferralView/patientreferralview.js"));
            bundles.Add(new ScriptBundle("~/bundles/HospitalPackageReport").Include("~/js/Reports/HospitalPackageReport.js"));
            bundles.Add(new ScriptBundle("~/bundles/PMobileVerificationReport").Include("~/js/Reports/PMobileVerificationReport.js"));

            //for admin view
            bundles.Add(new ScriptBundle("~/bundles/adminDashboard").Include("~/js/Dashboard/adminDashboard.js"));
            bundles.Add(new ScriptBundle("~/bundles/adminViewBlockPackageDetails").Include("~/js/BlockPackageView/adminViewBlockPackageDetails.js"));
            bundles.Add(new ScriptBundle("~/bundles/adminViewUnblockPackageDetails").Include("~/js/UnBlockPackageView/adminViewUnblockPackageDetails.js"));
            bundles.Add(new ScriptBundle("~/bundles/adminViewReferalDischarge").Include("~/js/DischargePackageView/adminViewReferalDischarge.js"));
            bundles.Add(new ScriptBundle("~/bundles/adminHospitalDetailsReport").Include("~/js/Reports/adminHospitalDetailsReport.js"));
            bundles.Add(new ScriptBundle("~/bundles/adminHospitalReferralReport").Include("~/js/Reports/adminHospitalReferralReport.js"));
            bundles.Add(new ScriptBundle("~/bundles/adminknowyourstatusreport").Include("~/js/Reports/adminknowyourstatusreport.js"));
            bundles.Add(new ScriptBundle("~/bundles/adminhospitalpreauthreport").Include("~/js/Reports/adminhospitalpreauthreport.js"));
            bundles.Add(new ScriptBundle("~/bundles/adminhospitalmortalityreport").Include("~/js/Reports/adminhospitalmortalityreport.js"));
            bundles.Add(new ScriptBundle("~/bundles/adminNewOverRideDetails").Include("~/js/Reports/adminNewOverRideDetails.js"));
            bundles.Add(new ScriptBundle("~/bundles/adminHospitalPackageReport").Include("~/js/Reports/adminHospitalPackageReport.js"));
            bundles.Add(new ScriptBundle("~/bundles/adminPMobileVerificationReport").Include("~/js/Reports/adminPMobileVerificationReport.js"));

            //for common use
            bundles.Add(new ScriptBundle("~/bundles/AdminUtility").Include("~/js/Common/AdminUtility.js"));
            //END

            //the following creates bundles in debug mode;
            // BundleTable.EnableOptimizations = true;
        }
    }
}