using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Service.Dtos;
using SystemIntegration.Models;
using SystemIntegration.Service.ViewModels;

namespace SystemIntegration.Service
{
    public interface ISysInfoService
    {
        /// <summary>
        /// 返回所以系统信息，显示用户系统绑定状态
        /// </summary>
        /// <param name="userNum"></param>
        /// <returns></returns>
        List<VSysInfoList> GetSysInfoList(string userNum);
    }
}
