﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Models;

namespace SystemIntegration.Repository
{
    public class UnitOfWork
    {
        private readonly SystemIntegrationEntities db = new SystemIntegrationEntities();
        private GenericRepository<UserSys> userSysRepo;
        private GenericRepository<SysInfo> sysInfoRepo;
        //private GenericRepository<LogInfo> logInfoRepo;
        //private GenericRepository<UserInfo> userInfoRepo;

        //public GenericRepository<UserSys> UserSysRepo
        //{
        //    get
        //    {
        //        if (this.userSysRepo==null)
        //        {
        //            this.userSysRepo = new GenericRepository<UserSys>(db);
        //        }
        //        return userSysRepo;
        //    }
        //}

        //public GenericRepository<SysInfo> SysInfoRepo
        //{
        //    get
        //    {
        //        if (this.sysInfoRepo == null)
        //        {
        //            this.sysInfoRepo = new GenericRepository<SysInfo>(db);
        //        }
        //        return sysInfoRepo;
        //    }
        //}
    }
}
