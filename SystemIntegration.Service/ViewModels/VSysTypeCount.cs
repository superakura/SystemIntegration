using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.ViewModels
{
    /// <summary>
    /// 按应用分类统计用户访问次数
    /// </summary>
    public class VSysTypeCount
    {
        public string SysTypeName { get; set; }
        public int SysTypeCount { get; set; }
    }
}
