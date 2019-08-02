using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.ViewModels
{
    /// <summary>
    /// 日志列表查询条件
    /// </summary>
    public class VLogListCondition
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public DateTime dateTimeStart { get; set; }
        public DateTime dateTimeEnd { get; set; }
        public string type { get; set; }
        public string ip { get; set; }
        public string userNum { get; set; }
        public string content { get; set; }
    }
}
