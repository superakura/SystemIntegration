using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Models;
using SystemIntegration.Service.ViewModels;
using SystemIntegration.Service.Dtos;

namespace SystemIntegration.Service
{
    public interface INoticeInfoService
    {
        bool Insert(NoticeInfo info);
        bool Update(NoticeInfo info);
        bool Save(NoticeInfoDto info);
        bool Del(int noticeID);
        NoticeInfoDto GetOne(int noticeID);
        VPageBootstrapTable<NoticeInfo> GetNoticePageInfo(VNoticeInfoCondition searchInfo);

        List<NoticeInfo> GetNoticeInfoByCount(int infoCount);
    }
}
