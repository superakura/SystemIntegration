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
        private IGenericRepository<SysInfo> _sysInfoRepo;
        
        public LogInfoService(IGenericRepository<LogInfo> repo,IGenericRepository<SysInfo> repoSysInfo)
        {
            this._logInfoRepo = repo;
            this._sysInfoRepo = repoSysInfo;
        }

        public List<VLoginIP> GetLogInfoIP(string userNum)
        {
            var list = _logInfoRepo.GetList();
            var info = from l in list
                         where l.LogPersonNum == userNum
                         group l by l.LogIP into g
                         select new VLoginIP { IP = g.Key, LoginCount = g.Count() };
            return info.ToList();
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

        public List<VSysLoginCount> GetSysLoginCount(string userNum)
        {
            var sys = _sysInfoRepo.GetList();
            var log = _logInfoRepo.GetList();

            var count = from g in log
                       join s in sys on g.LogSysID equals s.SysInfoID
                       where g.LogPersonNum == userNum && g.LogType == "系统访问"
                       group g by new {g.LogSysID,s.SysName } into c
                       select new VSysLoginCount{ SysName=c.Key.SysName, LoginCount = c.Count() };

            return count.ToList();
        }

        public List<VSysTypeCount> GetSysTypeCount(string userNum)
        {
            var listLog = _logInfoRepo.GetList();
            var listSys = _sysInfoRepo.GetList();

            var info = from l in listLog
                       join s in listSys on l.LogSysID equals s.SysInfoID
                       where l.LogPersonNum == userNum
                       group l by s.SysType into g
                       select new VSysTypeCount { SysTypeName = g.Key, SysTypeCount = g.Count() };

            return info.ToList();
        }

        public int GetUserLoginCount(string userNum)
        {
            return _logInfoRepo.GetList().Where(w => w.LogPersonNum == userNum&&w.LogType=="登录").Count();
        }

        public bool Insert(VLogInfo vLogInfo)
        {
            var info= Mapper.Map<VLogInfo,LogInfo>(vLogInfo);
            return _logInfoRepo.Insert(info);
        }
    }
}
