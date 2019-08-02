using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.ViewModels
{
    public class VSysInfoList
    {
        public int SysInfoID { get; set; }
        public string SysName { get; set; }
        public string SysDesc { get; set; }
        public string SysIcon { get; set; }
        public string ContactPhone { get; set; }
        public string ContactPerson { get; set; }
        public string BindState { get; set; }//用户绑定状态
        public string SysType { get; set; }
        public string LoginUrl { get; set; }
        public float SysOrder { get; set; }
    }
}
