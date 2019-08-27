using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.ViewModels
{
    public class VLogInfo
    {
        public int LogInfoID { get; set; }
        public string LogType { get; set; }
        public string LogContent { get; set; }
        public Nullable<int> LogSysID { get; set; }
        public string LogIP { get; set; }
        public Nullable<System.DateTime> LogDateTime { get; set; }
        public string LogPersonNum { get; set; }
        public string LogPersonName { get; set; }
        public string LogCol1 { get; set; }
        public string LogCol2 { get; set; }
        public string LogCol3 { get; set; }
        public string LogCol4 { get; set; }
        public string LogCol5 { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string LoginType { get; set; }
        public string Token { get; set; }
    }
}
