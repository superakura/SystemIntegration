using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.ViewModels
{
    public class VLoginCheckPwd
    {
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string LoginType { get; set; }
        /// <summary>
        /// 可以为地址或存储过程
        /// </summary>
        public string LoginCheckUrl { get; set; }
    }
}
