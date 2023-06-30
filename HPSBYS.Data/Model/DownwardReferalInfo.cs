using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{
   public class DownwardReferalInfo
    {
        public string Action { get; set; }
        public string URN { get; set; }
        public string BlockingInvoiceNo { get; set; }
        public string IsReferalRequired { get; set; }
        public string DistrictCode { get; set; }
        public string BlockCode { get; set; }
        public string PHCCode { get; set; }
        public string SubCenterCode { get; set; }
    }
}
