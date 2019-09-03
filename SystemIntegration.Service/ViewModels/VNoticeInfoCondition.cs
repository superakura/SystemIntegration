using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.ViewModels
{
    public class VNoticeInfoCondition
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public string NoticeType { get; set; }
        public string InsertPersonNum { get; set; }
        public string InsertDateTime { get; set; }
        public string NoticeTitle { get; set; }
    }
}
