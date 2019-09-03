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
using System.Security.Cryptography;
using System.Data;

namespace SystemIntegration.Service
{
    public class SysInfoService : ISysInfoService
    {
        private IGenericRepository<SysInfo> repoSys;
        private IGenericRepository<UserSys> repoUserSys;
        private IGenericRepository<LogInfo> repoLog;
        public SysInfoService(IGenericRepository<SysInfo> repositorySys,
            IGenericRepository<UserSys> repositoryUserSys,
            IGenericRepository<LogInfo> repositoryLogInfo)
        {
            repoSys = repositorySys;
            repoUserSys = repositoryUserSys;
            repoLog = repositoryLogInfo;
        }

        public List<VSysInfoList> GetUserSysInfoList(string userNum, string type)
        {
            var userSysList = repoUserSys.GetList();
            var sysList = repoSys.GetList();

            var list = from s in sysList
                       join u in userSysList on s.SysInfoID equals u.SysInfoID
                       where u.UserNum == userNum && s.SysType == type
                       select new VSysInfoList
                       {
                           SysInfoID = s.SysInfoID,
                           SysDesc = s.SysDesc,
                           SysName = s.SysName,
                           BindState = u.BindState,
                           SysIcon = s.SysIcon,
                           SysUrl=s.SysUrl,
                           IsLogin=s.IsLogin,
                           SysType = s.SysType,
                           ContactPhone = s.TechnicalContactPhone,
                           ContactPerson = s.TechnicalContactPerson,
                           SysOrder = s.SysOrder,
                           LoginUrl = s.LoginUrl
                       };
            return list.ToList();
        }

        public List<VSysInfoList> GetUserAllSysInfoList(string userNum, string type)
        {
            using (SystemIntegrationEntities db = new SystemIntegrationEntities())
            {
                var sql = @"SELECT  [SysInfoID]
      ,[SysName]
      ,[SysDesc]
      ,[SysIcon]
      ,[SysType]
      ,[LoginUrl]
      ,[SysOrder]
      ,SysUrl
      ,IsLogin
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

        public string AddUserSys(string userNum, string loginName, string loginPwd, int sysID)
        {
            var userSys = new UserSys();
            userSys.LoginName = loginName;
            userSys.LoginPwd = loginPwd;
            userSys.SysInfoID = sysID;
            userSys.UserNum = userNum;
            userSys.BindState = "已绑定";
            return repoUserSys.Insert(userSys) ? "ok" : "error";
        }

        public VLoginCheckPwd CheckSysLogin(string loginName, string loginPwd, int sysID)
        {
            //md5加密的作用：防止数据库丢失后，密码泄露
            //修改系统表字段
            //建立验证存储过程模板
            //用户登录时候，判断员工编号和考勤密码是否一致，判断员工编号在用户表中是否存在，如果不存在，自动增加用户信息。

            //select substring(sys.fn_sqlvarbasetostr(HashBytes('MD5','123' + '178DCC60-699E-49F9-BE86-02D58A86AD32')),3,32)

            //1、建立应用集中平台，避免在门口找系统，提高效率。不光可以挂载登录系统，也可挂载非登录系统，形成个人办公平台。
            //2、建立待办提醒统一接口
            //3、建立登录访问日志，用户可自行查看日志，系统可对异常访问登录进行自动提醒
            //4、解决了以往单点登录平台，系统需管理员需手工同步用户数据的问题，用户也可及时更新系统登录信息。变被动加入为用户主动添加。
            //5、在统一的系统中，提报各系统的问题和建议，完成对系统的评价，信息中心及时掌握用户的需求并加以解决。
            //6、登录随机码，在登录方法日志中生成，建立视图供系统登录访问。
            //7、实现用户自定义标签功能，可按标签查找，提供员工自己的系统使用环境
            //8、员工记事本功能，工作计划录入功能等便捷小功能。
            //9、对行政办公等系统建立接口



            var userPwd = MD5Encrypt(loginPwd + "178DCC60-699E-49F9-BE86-02D58A86AD32");

            var sysInfo = repoSys.GetByID(sysID);
            var loginUrl = sysInfo.LoginUrl;
            var loginType = sysInfo.LoginType;
            return new VLoginCheckPwd { LoginName = loginName, LoginPwd = userPwd, LoginCheckUrl = loginUrl, LoginType = loginType };
        }

        public SysInfo GetSysInfo(int sysID)
        {
            return repoSys.GetByID(sysID);
        }

        /// <summary>
        /// 获取系统信息分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public VPageBootstrapTable<SysInfo> GetSysInfoPage(VSysInfoListCondition input)
        {
            var list = repoSys.GetList();

            if (!string.IsNullOrEmpty(input.sysName))
            {
                list = list.Where(w => w.SysName.Contains(input.sysName));
            }
            if (!string.IsNullOrEmpty(input.sysType))
            {
                list = list.Where(w => w.SysType == input.sysType);
            }
            var rows = list.OrderByDescending(o => o.SysOrder).Skip(input.offset).Take(input.limit).ToList();
            var total = list.Count();
            return new VPageBootstrapTable<SysInfo> { rows = rows, total = total };
        }

        /// <summary>
        /// 添加一个系统信息
        /// </summary>
        /// <param name="sysInfo"></param>
        /// <returns></returns>
        public string InsertSysInfo(VSysInfo sysInfo)
        {
            var info = new SysInfo();
            return repoSys.Insert(MapToSysInfo(sysInfo, info)) ? "ok" : "error";
        }

        /// <summary>
        /// 更新系统信息
        /// </summary>
        /// <param name="sysInfo"></param>
        /// <returns></returns>
        public string UpdateSysInfo(VSysInfo sysInfo)
        {
            var info = repoSys.GetByID(sysInfo.SysInfoID);
            return repoSys.Update(MapToSysInfo(sysInfo, info)) ? "ok" : "error";
        }

        public SysInfo MapToSysInfo(VSysInfo v, SysInfo entity)
        {
            entity.SysInfoID = v.SysInfoID;
            entity.SysName = v.SysName;
            entity.SysDesc = v.SysDesc;
            entity.SysIcon = v.SysIcon;
            entity.SysType = v.SysType;
            entity.LoginUrl = v.LoginUrl;
            entity.TechnicalContactPerson = v.TechnicalContactPhone;
            entity.TechnicalContactPhone = v.TechnicalContactPerson;
            entity.SysState = v.SysState;
            entity.SysOrder = v.SysOrder;
            entity.IsLogin = v.IsLogin;
            entity.SysTypeSub = v.SysTypeSub;
            entity.SysUrl = v.SysUrl;
            entity.ManageContactPerson = v.ManageContactPerson;
            entity.ManageContactPhone = v.ManageContactPhone;
            entity.LoginCheckDataBaseIP = v.LoginCheckDataBaseIP;
            entity.LoginCheckDataBaseName = v.LoginCheckDataBaseName;
            entity.LoginCheckDataBaseStoredProcedure = v.LoginCheckDataBaseStoredProcedure;
            entity.LoginCheckDataBaseUserName = v.LoginCheckDataBaseUserName;
            entity.LoginCheckDataBaseUserPwd = v.LoginCheckDataBaseUserPwd;
            return entity;
        }

        /// <summary>
        /// 用MD5加密字符串
        /// </summary>
        /// <param name="password">待加密的字符串</param>
        /// <returns></returns>
        public string MD5Encrypt(string password)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(password));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            return tmp.ToString();
        }

        /// <summary>
        /// 用户系统绑定
        /// </summary>
        /// <param name="userNum"></param>
        /// <param name="userIP"></param>
        /// <param name="userName"></param>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <param name="sysID"></param>
        /// <returns></returns>
        public string BindUserSys(string userNum, string userIP, string userName, string loginName, string loginPwd, int sysID)
        {
            var loginPwdMD5 = MD5Encrypt(loginPwd + "178DCC60-699E-49F9-BE86-02D58A86AD32");
            var dbResult = false;//判断数据库操作是否执行成功

            var userSys = new UserSys();
            userSys.UserNum = userNum;
            userSys.BindState = "已绑定";
            userSys.SysInfoID = sysID;

            var sysInfo = repoSys.GetByID(sysID);
            if (sysInfo.IsLogin=="是")
            {
                var resultLoginCheck = CheckLoginByStoredProcedure(
                sysInfo.LoginCheckDataBaseIP,
                sysInfo.LoginCheckDataBaseName,
                sysInfo.LoginCheckDataBaseUserName,
                sysInfo.LoginCheckDataBaseUserPwd,
                sysInfo.LoginCheckDataBaseStoredProcedure,
                loginName,
                loginPwdMD5,
                sysInfo.LoginType
                );

                if (resultLoginCheck == "yes")
                {//如果用户名、密码正确，将该系统绑定赋值给用户
                    //添加UserSys表记录
                    //写入操作到日志
                    //返回“ok”到前端
                    userSys.LoginType = sysInfo.LoginType;
                    userSys.LoginName = loginName;
                    userSys.LoginPwd = loginPwdMD5;
                    
                }
                else
                {
                    //系统认证失败，如果该系统已经绑定，则解除该系统绑定信息，前端提示用户
                    //向前端返回“no”
                    return "no";
                }
            }

            dbResult = repoUserSys.Insert(userSys);

            var logInfo = new LogInfo();
            logInfo.LogIP = userIP;
            logInfo.LogDateTime = DateTime.Now;
            logInfo.LogContent = "成功绑定系统：" + sysInfo.SysName;
            logInfo.LogPersonNum = userNum;
            logInfo.LogType = "系统绑定";
            logInfo.LogSysID = sysID;
            dbResult = repoLog.Insert(logInfo);

            return dbResult ? "ok" : "dbError";
        }

        /// <summary>
        /// 通过存储过程验证用户名密码
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="db"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <param name="proc"></param>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <param name="loginType"></param>
        /// <returns></returns>
        public string CheckLoginByStoredProcedure(string ip, string db, string user, string pwd, string proc, string loginName, string loginPwd, string loginType)
        {
            //return "yes";//测试时候用
            string strConnection = "user id=" + user + ";password=" + pwd + ";initial catalog = " + db + "; Server = " + ip + "";
            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand sqlComm = conn.CreateCommand())
                    {
                        sqlComm.CommandText = proc;
                        sqlComm.CommandType = CommandType.StoredProcedure;

                        //用户名
                        SqlParameter sqlParameterLoginName = sqlComm.Parameters.Add(new SqlParameter("@loginName", SqlDbType.NVarChar, 50));
                        sqlParameterLoginName.Direction = ParameterDirection.Input;
                        sqlParameterLoginName.Value = loginName;

                        //密码
                        SqlParameter sqlParameterPwd = sqlComm.Parameters.Add(new SqlParameter("@loginPwd", SqlDbType.NVarChar, 50));
                        sqlParameterPwd.Direction = ParameterDirection.Input;
                        sqlParameterPwd.Value = loginPwd;

                        //登录类型
                        SqlParameter sqlParameterLoginType = sqlComm.Parameters.Add(new SqlParameter("@loginType", SqlDbType.NVarChar, 50));
                        sqlParameterLoginType.Direction = ParameterDirection.Input;
                        sqlParameterLoginType.Value = loginType;

                        //存储过程返回数据
                        SqlParameter sqlResult = sqlComm.Parameters.Add(new SqlParameter("@result", SqlDbType.NVarChar, 20));
                        sqlResult.Direction = ParameterDirection.Output;
                        sqlComm.ExecuteNonQuery();
                        return sqlComm.Parameters["@result"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 用户系统转跳
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="userNum"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public string RedirectToSys(int sysID,string userNum,string ip)
        {
            var sysInfo = repoSys.GetByID(sysID);
            var sysUserInfo = repoUserSys.GetList().Where(w => w.SysInfoID == sysID && w.UserNum == userNum).FirstOrDefault();
            if (sysInfo.IsLogin == "是")
            {
                var resultLoginCheck = CheckLoginByStoredProcedure(
                sysInfo.LoginCheckDataBaseIP,
                sysInfo.LoginCheckDataBaseName,
                sysInfo.LoginCheckDataBaseUserName,
                sysInfo.LoginCheckDataBaseUserPwd,
                sysInfo.LoginCheckDataBaseStoredProcedure,
                sysUserInfo.LoginName,
                sysUserInfo.LoginPwd,
                sysInfo.LoginType
                );
                if (resultLoginCheck=="yes")
                {
                    var token = Guid.NewGuid().ToString();

                    var logInfo = new LogInfo();
                    logInfo.LogIP = ip;
                    logInfo.LogDateTime = DateTime.Now;
                    logInfo.LogContent = "访问系统：" + sysInfo.SysName;
                    logInfo.LogPersonNum = userNum;
                    logInfo.LogPersonName = userNum;
                    logInfo.LogType = "系统访问";
                    logInfo.LogSysID = sysID;

                    logInfo.LoginName = sysUserInfo.LoginName;
                    logInfo.LoginPwd = sysUserInfo.LoginPwd;
                    logInfo.LoginType = sysInfo.LoginType;
                    logInfo.Token = token;

                    var returnUrl = sysInfo.LoginUrl + "?token=" + token;
                    var dbResult = repoLog.Insert(logInfo);

                    return dbResult?returnUrl:"dbError";
                }
                else
                {
                    return "userOrPwdError";
                }
            }
            else
            {
                return sysInfo.SysUrl;
            }
        }

        /// <summary>
        /// 解除用户系统绑定
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="userNum"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public string RemoveUserSys(int sysID, string userNum, string ip)
        {
            try
            {
                var sysInfo = repoSys.GetByID(sysID);
                var sysUserInfo = repoUserSys.GetList().Where(w => w.SysInfoID == sysID && w.UserNum == userNum).FirstOrDefault();
                var dbResult = false;
                dbResult = repoUserSys.Delete(sysUserInfo.UserSysID);

                var logInfo = new LogInfo();
                logInfo.LogIP = ip;
                logInfo.LogDateTime = DateTime.Now;
                logInfo.LogContent = "解除绑定系统：" + sysInfo.SysName;
                logInfo.LogPersonNum = userNum;
                logInfo.LogPersonName = userNum;
                logInfo.LogType = "解除绑定";
                logInfo.LogSysID = sysID;
                dbResult = repoLog.Insert(logInfo);

                return dbResult? "ok":"error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
