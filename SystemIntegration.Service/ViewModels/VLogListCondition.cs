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
        /// <summary>
        /// 日志的类型
        /// </summary>
        public string type { get; set; }
        public string ip { get; set; }
        /// <summary>
        /// 如果是普通用户，员工编号为固定值。如果是管理员，员工编号为搜索值
        /// </summary>
        public string userNum { get; set; }
        public string content { get; set; }
        /// <summary>
        /// 查看日志的权限：普通用户查看自己，管理员查看所有日志
        /// </summary>
        public string QueryRole { get; set; }
    }
}
