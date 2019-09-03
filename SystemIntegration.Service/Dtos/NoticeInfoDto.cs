using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemIntegration.Service.Dtos
{
    public class NoticeInfoDto
    {
        public int NoticeInfoID { get; set; }
        public string NoticeTitle { get; set; }
        public string NoticeContent { get; set; }
        public string ContentType { get; set; }
        public Nullable<int> ContentCount { get; set; }
        public string InsertPersonNum { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
    }
}
