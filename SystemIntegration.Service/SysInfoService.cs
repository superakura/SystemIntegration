using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Service.Dtos;
using SystemIntegration.Repository;
using SystemIntegration.Models;
using SystemIntegration.Service.ViewModels;

namespace SystemIntegration.Service
{
    public class SysInfoService : ISysInfoService
    {
        private IGenericRepository<SysInfo> repoSys;
        private IGenericRepository<UserSys> repoUserSys;
        public SysInfoService(IGenericRepository<SysInfo> repositorySys, IGenericRepository<UserSys> repositoryUserSys)
        {
            repoSys = repositorySys;
            repoUserSys = repositoryUserSys;
        }
        //private UnitOfWork work = new UnitOfWork();
        //public SysInfoService()
        //{
        //    repoSys = work.SysInfoRepo;
        //    repoUserSys = work.UserSysRepo;
        //}

        public List<VSysInfoList> GetSysInfoList(string userNum)
        {
            var userSysList = repoUserSys.GetList();
            var sysList = repoSys.GetList();
            var list = from s in sysList
                       join u in userSysList on s.SysInfoID equals u.SysInfoID
                       where u.UserNum == userNum
                       select new VSysInfoList
                       {
                           SysInfoID = s.SysInfoID,
                           SysDesc = s.SysDesc,
                           SysName = s.SysName,
                           BindState = u.BindState,
                           SysIcon = s.SysIcon,
                           SysType = s.SysType
                       };
            return list.ToList();
        }
    }
}
