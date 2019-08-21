using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.ViewModels
{
    public class VSysInfo
    {
        public int SysInfoID { get; set; }
        public string SysName { get; set; }
        public string SysDesc { get; set; }
        public string SysIcon { get; set; }
        public string TechnicalContactPhone { get; set; }
        public string TechnicalContactPerson { get; set; }
        public string ManageContactPhone { get; set; }
        public string ManageContactPerson { get; set; }
        public string SysState { get; set; }
        public string SysType { get; set; }
        public string SysTypeSub { get; set; }
        public string SysUrl { get; set; }
        public string LoginUrl { get; set; }
        public string LoginType { get; set; }
        public Nullable<double> SysOrder { get; set; }
        public string IsLogin { get; set; }
        public string LoginCheckDataBaseIP { get; set; }
        public string LoginCheckDataBaseName { get; set; }
        public string LoginCheckDataBaseUserName { get; set; }
        public string LoginCheckDataBaseUserPwd { get; set; }
        public string LoginCheckDataBaseStoredProcedure { get; set; }
    }
}
