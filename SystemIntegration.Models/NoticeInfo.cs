//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SystemIntegration.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NoticeInfo
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
