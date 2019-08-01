using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.Dtos
{
    public class SysInfoDto
    {
        public int SysID { get; set; }
        public string SysName { get; set; }
        public string SysDesc { get; set; }
        public string SysIcon { get; set; }
        public string ContactPhone { get; set; }
        public string ContactPerson { get; set; }
        public string SysState { get; set; }
        public string SysType { get; set; }
        public string LoginUrl { get; set; }
        public Nullable<double> SysOrder { get; set; }
    }
}
