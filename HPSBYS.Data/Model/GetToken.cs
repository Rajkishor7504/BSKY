using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPSBYS.Data.Model
{
    public class GetToken
    {
        public GetTokenDto tokenDetails { get; set; }       
    }
    public class GetTokenDto
    {
        public string status { get; set; }
        public string Message { get; set; }
        public string code { get; set; }
        public string token { get; set; }
    }
}
