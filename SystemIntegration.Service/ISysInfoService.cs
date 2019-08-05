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
        /// 返回用户绑定的系统信息
        /// </summary>
        /// <param name="userNum"></param>
        /// <returns></returns>
        List<VSysInfoList> GetSysInfoList(string userNum);

        /// <summary>
        /// 返回所以系统的信息，同时显示用户已经绑定系统的状态
        /// </summary>
        /// <param name="userNum"></param>
        /// <returns></returns>
        List<VSysInfoList> GetAllSysInfoList(string userNum);
    }
}
