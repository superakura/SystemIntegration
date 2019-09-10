using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.ViewModels
{
    /// <summary>
    /// 统计用户不同IP的登录次数
    /// </summary>
    public class VLoginIP
    {
        public string IP { get; set; }
        public int LoginCount { get; set; }
    }
}
