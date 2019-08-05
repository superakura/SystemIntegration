using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Models;
using SystemIntegration.Service.ViewModels;
using SystemIntegration.Repository;

namespace SystemIntegration.Service
{
    public class LogInfoService : ILogInfoService
    {
        private GenericRepository<LogInfo> _logInfoRepo;
        public VPageBootstrapTable<LogInfo> GetLogInfoList(VLogListCondition input)
        {
            var list = _logInfoRepo.GetList();
            if (input.QueryRole=="用户")
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
            if (!string.IsNullOrEmpty(input.userNum)&&input.QueryRole=="管理员")
            {
                list = list.Where(w => w.LogPersonNum==input.userNum);
            }
            var rows = list.Skip(input.offset).Take(input.limit).OrderByDescending(o => o.LogDateTime).ToList();
            var total = list.Count();
            return new VPageBootstrapTable<LogInfo> { rows =rows , total = total };
        }
    }
}
