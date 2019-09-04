using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Models;
using SystemIntegration.Service.ViewModels;
using SystemIntegration.Service.Dtos;
using SystemIntegration.Repository;
using AutoMapper;

namespace SystemIntegration.Service
{
    public class NoticeInfoService : INoticeInfoService
    {
        private IGenericRepository<NoticeInfo> _repoNoticeInfo;
        public NoticeInfoService(IGenericRepository<NoticeInfo> repoNoticeInfo)
        {
            this._repoNoticeInfo = repoNoticeInfo;
        }
        public bool Del(int noticeID)
        {
            return _repoNoticeInfo.Delete(noticeID);
        }

        public List<NoticeInfo> GetNoticeInfoByCount(int infoCount)
        {
            return _repoNoticeInfo.GetList().OrderByDescending(o => o.InsertDate).Take(infoCount).ToList();
        }

        public VPageBootstrapTable<NoticeInfo> GetNoticePageInfo(VNoticeInfoCondition searchInfo)
        {
            var list = _repoNoticeInfo.GetList();
            if (!string.IsNullOrEmpty(searchInfo.NoticeTitle))
            {
                list = list.Where(w => w.NoticeTitle.Contains(searchInfo.NoticeTitle));
            }
            if (!string.IsNullOrEmpty(searchInfo.InsertPersonNum))
            {
                list = list.Where(w => w.InsertPersonNum == searchInfo.InsertPersonNum);
            }
            if (!string.IsNullOrEmpty(searchInfo.NoticeType))
            {
                list = list.Where(w => w.ContentType == searchInfo.NoticeType);
            }
            var rows = list.OrderByDescending(o => o.InsertDate).Skip(searchInfo.offset).Take(searchInfo.limit).ToList();
            var total = list.Count();
            return new VPageBootstrapTable<NoticeInfo> { rows = rows, total = total };
        }

        public NoticeInfoDto GetOne(int noticeID)
        {
            var entity= _repoNoticeInfo.GetByID(noticeID);
            return Mapper.Map<NoticeInfo, NoticeInfoDto>(entity); 
        }

        public bool Save(NoticeInfoDto info)
        {
            var entity = Mapper.Map<NoticeInfoDto, NoticeInfo>(info);
            return info.NoticeInfoID == 0 ? _repoNoticeInfo.Insert(entity) : _repoNoticeInfo.Update(entity);
        }
    }
}
