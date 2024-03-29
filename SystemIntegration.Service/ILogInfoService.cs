﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Models;
using SystemIntegration.Service.ViewModels;

namespace SystemIntegration.Service
{
    public interface ILogInfoService
    {
        VPageBootstrapTable<LogInfo> GetLogInfoList(VLogListCondition input);

        bool Insert(VLogInfo logInfo);

        List<VLoginIP> GetLogInfoIP(string userNum);

        int GetUserLoginCount(string userNum);

        List<VSysLoginCount> GetSysLoginCount(string userNum);

        List<VSysTypeCount> GetSysTypeCount(string userNum);
    }
}
