using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.ViewModels
{
    /// <summary>
    /// 统计用户各应用的访问次数
    /// </summary>
    public class VSysLoginCount
    {
        public string SysName { get; set; }
        public int? LoginCount { get; set; }
    }
}
