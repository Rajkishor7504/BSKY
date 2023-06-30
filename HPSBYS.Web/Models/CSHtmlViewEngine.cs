using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPSBYS.Web.Models
{
    public class CSHtmlViewEngine : RazorViewEngine
    {
        public CSHtmlViewEngine()
        {
            base.AreaViewLocationFormats =
                new string[]
                    {
                    "~/Views/{1}/{0}.cshtml",
                    "~/Views/Shared/{0}.cshtml"
                    };
        }
    }
}