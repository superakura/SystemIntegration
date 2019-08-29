using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Models;
using SystemIntegration.Service.ViewModels;
using SystemIntegration.Repository;
using AutoMapper.Mappers;
using AutoMapper;

namespace SystemIntegration.Service
{
    public class LogInfoService : ILogInfoService
    {
        private IGenericRepository<LogInfo> _logInfoRepo;
        
        public LogInfoService(IGenericRepository<LogInfo> repo)
        {
            this._logInfoRepo = repo;
        }

        public VPageBootstrapTable<LogInfo> GetLogInfoList(VLogListCondition input)
        {
            var list = _logInfoRepo.GetList();
            if (input.QueryRole == "用户")
            {
                list = list.Where(w => w.LogPersonNum == input.userNum);
            }
            if (!string.IsNullOrEmpty(input.content))
            {
                list = list.Where(w => w.LogContent.Contains(input.content));
            }
            if (!string.IsNullOrEmpty(input.ip))
            {
                list = list.Where(w => w.LogIP.Contains(input.ip));
            }
            if (!string.IsNullOrEmpty(input.userNum) && input.QueryRole == "管理员")
            {
                list = list.Where(w => w.LogPersonNum.Contains(input.userNum));
            }
            var rows = list.OrderByDescending(o=>o.LogDateTime).Skip(input.offset).Take(input.limit).ToList();
            var total = list.Count();
            return new VPageBootstrapTable<LogInfo> { rows =rows , total = total };
        }

        public bool Insert(VLogInfo vLogInfo)
        {
            var info= Mapper.Map<VLogInfo,LogInfo>(vLogInfo);
            return _logInfoRepo.Insert(info);
        }
    }
}
