using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{
    public class AddOnInfo
    {
        public int AddOnLogId { get; set; }
        public int TransactionInformationId { get; set; }
        public string BlockingInvoiceNo { get; set; }
        public string ProcedureCode { get; set; }
        public string ProcedureName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public string BlockingAmount { get; set; }
        public string BlockingDate { get; set; }
    }
}
