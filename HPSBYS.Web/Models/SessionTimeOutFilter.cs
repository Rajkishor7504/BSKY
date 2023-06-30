using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HPSBYS.Web.Models
{
    public sealed class SessionTimeOutFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // check if session supported
            if (filterContext.HttpContext.Session != null)
            {
                if (Convert.ToBoolean(filterContext.HttpContext.Session["PwdUpdateStatus"]) == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Dashboard/ChangePassword");
                }
                else
                {
                    if (filterContext.HttpContext.Session["HospitalName"] == null || filterContext.HttpContext.Session["HospitalCode"] == null)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Dashboard/SessionRedirect");
                    }
                }

            }
            base.OnActionExecuting(filterContext);
        }
    }
}