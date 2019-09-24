using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemIntegration.Service;
using SystemIntegration.Service.ViewModels;

namespace SystemIntegration.Web.Controllers
{
    [Authorize(Roles = "登录")]
    public class SysInfoController : Controller
    {
        private ISysInfoService service;
        private IUserInfoService serviceUser;
        public SysInfoController(ISysInfoService service,IUserInfoService serviceUser)
        {
            this.service = service;
            this.serviceUser = serviceUser;
        }

        [Authorize(Roles = "系统维护")]
        public ViewResult List()
        {
            return View();
        }

        public ViewResult ListUserAll()
        {
            return View();
        }

        public ViewResult ListUser()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUserAllList()
        {
            var type = Request.Form["type"];
            return Json(service.GetUserAllSysInfoList(User.Identity.Name.ToString(), type));
        }

        [HttpPost]
        public JsonResult GetUserList()
        {
            var type = Request.Form["type"];
            return Json(service.GetUserSysInfoList(User.Identity.Name.ToString(), type));
        }

        [HttpPost]
        public JsonResult GetSysCount()
        {
            return Json(service.GetSysCount(User.Identity.Name.ToString()));
        }

        [HttpPost]
        public string BindSys()
        {
            var loginNum = Request.Form["tbxUserNum"];
            var loginPwd = Request.Form["tbxPwd"];
            var sysID = 0;
            int.TryParse(Request.Form["tbxPwd"], out sysID);
            return "ok";
        }


        [HttpPost][Authorize(Roles = "系统维护")]
        public JsonResult GetSysList()
        {
            var input = new VSysInfoListCondition();

            var limit = 0;
            int.TryParse(Request.Form["limit"], out limit);
            input.limit = limit;
            var offSet = 0;
            int.TryParse(Request.Form["offset"], out offSet);
            input.offset = offSet;

            input.sysName = Request.Form["sysName"];
            input.sysType = Request.Form["sysType"];
            return Json(service.GetSysInfoPage(input));
        }

        [HttpPost]
        public JsonResult GetSysOne()
        {
            var sysID = 0;
            int.TryParse(Request.Form["id"], out sysID);
            return Json(service.GetSysInfo(sysID));
        }

        [HttpPost][Authorize(Roles = "系统维护")]
        public string InsertSysInfo()
        {
            VSysInfo info = new VSysInfo();
            info.SysName = Request.Form["tbxSysName"];
            info.SysDesc = Request.Form["tbxSysDesc"];
            info.SysIcon = Request.Form["tbxSysIcon"];
            info.TechnicalContactPhone = Request.Form["tbxContactPhone"];
            info.TechnicalContactPerson = Request.Form["tbxContactPerson"];
            info.SysState = Request.Form["tbxSysState"];
            info.SysType = Request.Form["ddlSysType"];
            info.LoginUrl = Request.Form["tbxLoginUrl"];
            var order = 0;
            int.TryParse(Request.Form["tbxSysOrder"], out order);
            info.SysOrder = order;
            return service.InsertSysInfo(info);
        }

        [HttpPost][Authorize(Roles = "系统维护")]
        public string UpdateSysInfo()
        {
            var id = 0;
            int.TryParse(Request.Form["sysID"], out id);

            VSysInfo info = new VSysInfo();
            info.SysInfoID = id;
            info.SysName = Request.Form["tbxSysName"];
            info.SysDesc = Request.Form["tbxSysDesc"];
            info.SysIcon = Request.Form["tbxSysIcon"];
            info.TechnicalContactPhone = Request.Form["tbxTechnicalContactPhone"];
            info.TechnicalContactPerson = Request.Form["tbxTechnicalContactPerson"];
            info.SysState = Request.Form["tbxSysState"];
            info.SysType = Request.Form["ddlSysType"];
            info.LoginUrl = Request.Form["tbxLoginUrl"];
            info.LoginType= Request.Form["tbxLoginType"];
            info.SysUrl= Request.Form["tbxSysUrl"];

            var order = 0;
            int.TryParse(Request.Form["tbxSysOrder"], out order);
            info.SysOrder = order;

            return service.UpdateSysInfo(info);
        }

        [HttpPost][Authorize(Roles = "系统维护")]
        public string SaveSysInfo()
        {
            try
            {
                var id = 0;
                int.TryParse(Request.Form["sysID"], out id);

                VSysInfo info = new VSysInfo();
                info.SysInfoID = id;
                info.SysName = Request.Form["tbxSysName"];
                info.SysDesc = Request.Form["tbxSysDesc"];
                info.SysIcon = Request.Form["tbxSysIcon"];
                info.TechnicalContactPhone = Request.Form["tbxTechnicalContactPhone"];
                info.TechnicalContactPerson = Request.Form["tbxTechnicalContactPerson"];
                info.ManageContactPhone = Request.Form["tbxManageContactPhone"];
                info.ManageContactPerson = Request.Form["tbxManageContactPerson"];

                info.SysTypeSub = Request.Form["tbxSysTypeSub"];
                info.LoginUrl = Request.Form["tbxLoginUrl"];
                info.LoginType = Request.Form["tbxLoginType"];
                info.SysUrl = Request.Form["tbxSysUrl"];

                info.LoginCheckDataBaseIP = Request.Form["tbxLoginCheckDataBaseIP"];
                info.LoginCheckDataBaseName = Request.Form["tbxLoginCheckDataBaseName"];
                info.LoginCheckDataBaseUserName = Request.Form["tbxLoginCheckDataBaseUserName"];
                info.LoginCheckDataBaseUserPwd = Request.Form["tbxLoginCheckDataBaseUserPwd"];
                info.LoginCheckDataBaseStoredProcedure = Request.Form["tbxLoginCheckDataBaseStoredProcedure"];

                info.SysState = Request.Form["ddlSysState"];
                info.SysType = Request.Form["ddlSysType"];
                info.IsLogin = Request.Form["ddlIsLogin"];

                var order = 0;
                int.TryParse(Request.Form["tbxSysOrder"], out order);
                info.SysOrder = order;

                return id==0?service.InsertSysInfo(info): service.UpdateSysInfo(info);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public string BindSysLogin()
        {
            var loginPwd = Request.Form["tbxPwd"];
            var loginName = Request.Form["tbxUserNum"];

            var sysID = 0;
            int.TryParse(Request.Form["SysID"], out sysID);

            var sysInfo = service.GetSysInfo(sysID);
            if (sysInfo.IsLogin=="考勤登录")
            {
                loginName = User.Identity.Name;
            }

            var userInfo = serviceUser.GetUserInfoByNum(User.Identity.Name);
            var userName = userInfo.UserName;//用户的姓名

            var ip = Request.UserHostAddress;

            return service.BindUserSys(userInfo.UserNum,ip, userName, loginName,loginPwd,sysID);
        }

        public string RedirecToSys()
        {
            //1、 判断系统是否需要登录
            //2、如果不需要登录，直接转跳
            //3、如果需要登录，首先根据存储过程判断用户名、密码是否正确
            //4、如果不正确，提示用户用户名、密码不正确，询问是否解除绑定。
            //5、如解除绑定，则删除用户系统表中记录
            //6、如果密码正确，添加访问日志到日志表，
            //7、日志表中写入随机访问码，同时写入用户名，密码md5，登录用户类型
            //8、将用户名，密码md5，登录用户类型，传递到该系统的转跳访问地址
            //9、创建验证用户名tokenUser,密码：rjkf@scl607
            //10、创建存储过程，GetLoginInfoByToken
            //11、创建数据库时候，logintype不能为空，否在存储过程

            var sysID = 0;
            int.TryParse(Request.Form["sysID"], out sysID);

            var userNum = User.Identity.Name;
            var userName = HttpUtility.UrlDecode(Request.Cookies["dandianUserName"].Value);
            var userIP = Request.UserHostAddress;

            return service.RedirectToSys(sysID, userNum,userName, userIP);
        }

        public string RemoveUserSys()
        {
            var sysID = 0;
            int.TryParse(Request.Form["sysID"], out sysID);

            var userNum = User.Identity.Name;
            var userIP = Request.UserHostAddress;
            var userName = HttpUtility.UrlDecode(Request.Cookies["dandianUserName"].Value);
            return service.RemoveUserSys(sysID, userNum,userName,userIP);
        }
    }
}