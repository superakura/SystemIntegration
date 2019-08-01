using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Service.Dtos;
using SystemIntegration.Models;

namespace SystemIntegration.Service
{
    public interface ISysInfoService
    {
        List<SysInfo> GetSysInfoList();
    }
}
