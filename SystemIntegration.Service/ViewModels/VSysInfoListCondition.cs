using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.ViewModels
{
    /// <summary>
    /// 系统列表查询条件
    /// </summary>
    public class VSysInfoListCondition
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public string sysName { get; set; }
        public string sysType { get; set; }
    }
}
