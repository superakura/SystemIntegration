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

        public List<VSysInfoList> GetUserSysInfoList(string userNum,string type)
        {
            var userSysList = repoUserSys.GetList();
            var sysList = repoSys.GetList();

            var list = from s in sysList
                       join u in userSysList on s.SysInfoID equals u.SysInfoID
                       where u.UserNum == userNum&&s.SysType==type
                       select new VSysInfoList
                       {
                           SysInfoID = s.SysInfoID,
                           SysDesc = s.SysDesc,
                           SysName = s.SysName,
                           BindState = u.BindState,
                           SysIcon = s.SysIcon,
                           SysType = s.SysType,
                           ContactPhone = s.ContactPhone,
                           ContactPerson = s.ContactPerson,
                           SysOrder = s.SysOrder,
                           LoginUrl = s.LoginUrl
                       };
            return list.ToList();
        }

        public List<VSysInfoList> GetUserAllSysInfoList(string userNum,string type)
        {
            using (SystemIntegrationEntities db = new SystemIntegrationEntities())
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
      ,(select [BindState] from [UserSys] t where t.UserNum=@userNum and t.SysInfoID=s.SysInfoID) as BindState
  FROM [SystemIntegration].[dbo].[SysInfo] s where s.SysType=@sysType order by SysOrder";
                SqlParameter[] parameter = new SqlParameter[2];
                parameter[0] = new SqlParameter("@userNum", userNum);
                parameter[1] = new SqlParameter("@sysType", type);
                return db.Database.SqlQuery<VSysInfoList>(sql, parameter).ToList();
            }
        }

        public VSysCount GetSysCount(string userNum)
        {
            var userSysList = repoUserSys.GetList();
            var sysList = repoSys.GetList();

            var userCount = (from s in sysList
                           join u in userSysList on s.SysInfoID equals u.SysInfoID
                           where u.UserNum == userNum
                           select s).Count();

            var allCount = repoSys.GetList().Count();
            return new VSysCount { SysAllCount = allCount, SysUserCount = userCount };
        }

        public string AddUserSys(string userNum, string loginName,string loginPwd, int sysID)
        {
            var userSys = new UserSys();
            userSys.LoginName = loginName;
            userSys.LoginPwd = loginPwd;
            userSys.SysInfoID = sysID;
            userSys.UserNum = userNum;
            userSys.BindState = "已绑定";
            return repoUserSys.Insert(userSys)?"ok":"error";
        }

        public string CheckSysLogin(int sysID, string loginName, string loginPwd, string loginType, string loginUrl)
        {
            throw new NotImplementedException();
        }

        public SysInfo GetSysInfo(int sysID)
        {
            return repoSys.GetByID(sysID);
        }

        public VPageBootstrapTable<SysInfo> GetSysInfoPage(VSysInfoListCondition input)
        {
            var list = repoSys.GetList();

            if (!string.IsNullOrEmpty(input.sysName))
            {
                list = list.Where(w => w.SysName.Contains(input.sysName));
            }
            if (!string.IsNullOrEmpty(input.sysType))
            {
                list = list.Where(w => w.SysType==input.sysType);
            }
            var rows = list.OrderByDescending(o => o.SysOrder).Skip(input.offset).Take(input.limit).ToList();
            var total = list.Count();
            return new VPageBootstrapTable<SysInfo> { rows = rows, total = total };
        }

        public string InsertSysInfo(VSysInfo sysInfo)
        {
            var info = new SysInfo();
            return repoSys.Insert(MapToSysInfo(sysInfo, info)) ?"ok":"error";
        }

        public string UpdateSysInfo(VSysInfo sysInfo)
        {
            var info = repoSys.GetByID(sysInfo.SysID);
            return repoSys.Update(MapToSysInfo(sysInfo, info)) ?"ok":"error";
        }

        public SysInfo MapToSysInfo(VSysInfo v,SysInfo entity)
        {
            entity.SysInfoID = v.SysID;
            entity.SysName = v.SysName;
            entity.SysDesc = v.SysDesc;
            entity.SysIcon = v.SysIcon;
            entity.SysType = v.SysType;
            entity.LoginUrl = v.LoginUrl;
            entity.ContactPerson = v.ContactPerson;
            entity.ContactPhone = v.ContactPhone;
            entity.SysState = v.SysState;
            entity.SysOrder = v.SysOrder;
            return entity;
        }
    }
}
