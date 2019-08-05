using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Service.Dtos;
using SystemIntegration.Repository;
using SystemIntegration.Models;
using SystemIntegration.Service.ViewModels;
using System.Data.SqlClient;

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
                           SysType = s.SysType,
                           ContactPhone=s.ContactPhone,
                           ContactPerson=s.ContactPerson,
                           SysOrder=s.SysOrder,
                           LoginUrl=s.LoginUrl
                       };
            return list.ToList();
        }

        public List<VSysInfoList> GetAllSysInfoList(string userNum)
        {
            using (SystemIntegrationEntities db=new SystemIntegrationEntities())
            {
                var sql = @"SELECT  [SysInfoID]
      ,[SysName]
      ,[SysDesc]
      ,[SysIcon]
      ,[ContactPhone]
      ,[ContactPerson]
      ,[SysType]
      ,[LoginUrl]
      ,[SysOrder]
      ,(select [BindState] from [UserSys] t where t.UserNum='@userNum' and t.SysInfoID=s.SysInfoID) as BindState
  FROM [SystemIntegration].[dbo].[SysInfo] s";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@userNum", userNum);
                return db.Database.SqlQuery<VSysInfoList>(sql, parameter).ToList(); ;
            }
        }
    }
}
