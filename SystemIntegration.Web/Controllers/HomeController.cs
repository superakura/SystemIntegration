using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SystemIntegration.Service;
using SystemIntegration.Web.wsDemo;

namespace SystemIntegration.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserInfoService _service;
        private ILogInfoService _serviceLogInfo;
        public HomeController(IUserInfoService service,ILogInfoService serviceLogInfo)
        {
            this._service = service;
            this._serviceLogInfo = serviceLogInfo;
        }
        /// <summary>
        /// 考勤系统验证函数，员工编号、考勤系统密码
        /// </summary>
        /// <param name="userNum"></param>
        /// <param name="pwd"></param>
        /// <returns>yes</returns>
        private string KaoqinCheck(string userNum, string pwd)
        {
            string strConnection = "user id=KqLogin;password=rjkf3877;initial catalog = GM_MT; Server = 10.126.10.54";
            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand sqlComm = conn.CreateCommand())
                    {
                        sqlComm.CommandText = "CheckId";
                        sqlComm.CommandType = CommandType.StoredProcedure;

                        //工号
                        SqlParameter employeeId = sqlComm.Parameters.Add(new SqlParameter("@employeeId", SqlDbType.NVarChar, 20));
                        employeeId.Direction = ParameterDirection.Input;
                        employeeId.Value = userNum;

                        //密码
                        SqlParameter sqlPwd = sqlComm.Parameters.Add(new SqlParameter("@pwd", SqlDbType.NVarChar, 20));
                        sqlPwd.Direction = ParameterDirection.Input;
                        sqlPwd.Value = pwd;

                        //返回行数
                        SqlParameter result = sqlComm.Parameters.Add(new SqlParameter("@result", SqlDbType.NVarChar, 20));
                        result.Direction = ParameterDirection.Output;
                        sqlComm.ExecuteNonQuery();
                        var isYes = sqlComm.Parameters["@result"].Value.ToString();
                        conn.Close();
                        if (isYes == "1")
                        {
                            return "yes";
                        }
                        else
                        {
                            return "no";
                        }
                    }
                }
                catch
                {
                    return "yes";//当考勤数据库异常无法连接时，绕过用户名密码，直接登录。
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 加载登录页面
        /// </summary>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// 用户登录、加载用户权限、加载菜单、转跳
        /// </summary>
        /// <param name="userNum"></param>
        /// <param name="pwd"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous][HttpPost]
        public ActionResult Login(string userNum, string pwd, string returnUrl)
        {
            //通过考勤数据库验证员工编号、考勤密码
            var result = "yes";
            //result = KaoqinCheck(userNum, pwd);//系统测试时，注释。正式运行时，取消注释。

            if (result == "yes")
            {
                #region 判断用户是否存在，如果不存在则添加用户
                var userInfo = _service.GetUserInfoByNum(userNum);
                if (userInfo == null)
                {
                    var userName = userNum;//通过考勤系统查询员工姓名
                    _service.Insert(userNum, userName, "0", pwd, "日志,登录");//增加用户信息
                    userInfo = _service.GetUserInfoByNum(userNum);
                }
                else
                {
                    userInfo.UserPwd = pwd;
                    _service.Update(userInfo);//更新用户密码
                }
                #endregion

                //将用户的全部信息存入session，便于在其他页面调用
                //System.Web.HttpContext.Current.Session["user"] = userInfo;

                #region 加载、设置用户权限
                var userAuthorityString = userInfo.UserRole;

                //写入用户角色
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(2,
                    userNum,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(2),
                    false,
                    userAuthorityString);

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                System.Web.HttpCookie authCookie = new System.Web.HttpCookie("dandian", encryptedTicket);
                //if (authTicket.IsPersistent)
                //{
                //    authCookie.Expires = authTicket.Expiration;
                //}
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                #endregion

                #region 设置用户姓名的cookie
                var cUserName = System.Web.HttpContext.Current.Server.UrlEncode(userInfo.UserName);
                System.Web.HttpCookie userNameCookie = new System.Web.HttpCookie("dandianUserName", cUserName);
                System.Web.HttpContext.Current.Response.Cookies.Add(userNameCookie);
                #endregion

                #region 登录写入日志
                var logInfo = new Service.ViewModels.VLogInfo();
                logInfo.LogDateTime = DateTime.Now;
                logInfo.LogContent = "单点平台登录";
                logInfo.LogPersonNum = userNum;
                logInfo.LogPersonName = userNum;
                logInfo.LogType = "登录";
                logInfo.LogIP = Request.UserHostAddress;
                _serviceLogInfo.Insert(logInfo);
                #endregion

                return Redirect(returnUrl ?? Url.Action("Index", "Home"));
            }
            else
            {
                ModelState.AddModelError("", "用户名或密码错误！");
                return View();
            }
        }

        /// <summary>
        /// form登录方式注销
        /// </summary>
        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Login");
        }

        /// <summary>
        /// 浏览器版本错误提示页面
        /// </summary>
        /// <returns></returns>
        public ViewResult ErrorIE()
        {
            return View();
        }

        public ViewResult Index()
        {
            return View();
        }

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

        [AllowAnonymous][HttpGet]
        public string GetMd5(string pwd)
        {
            return MD5Encrypt(pwd + "178DCC60-699E-49F9-BE86-02D58A86AD32");
        }

        /// <summary>
        /// 调用webservice的insert
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [AllowAnonymous][HttpGet]
        public string WebServiceInsertSql(string sql)
        {
            WsInsertStringSoapClient ws = new WsInsertStringSoapClient();
            return ws.InsertSql(sql)+"--"+ws.HelloWorld();
        }
    }
}