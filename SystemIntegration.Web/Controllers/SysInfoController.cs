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

        [HttpPost]
        [Authorize(Roles = "系统维护")]
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

        [HttpPost]
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
            int.TryParse(Request.Form["tbxSysOrder"], out int order);
            info.SysOrder = order;
            return service.InsertSysInfo(info);
        }

        [HttpPost]
        public string UpdateSysInfo()
        {
            var id = 0;
            int.TryParse(Request.Form["sysID"], out id);
            VSysInfo info = new VSysInfo();
            info.SysID = id;
            info.SysName = Request.Form["tbxSysName"];
            info.SysDesc = Request.Form["tbxSysDesc"];
            info.SysIcon = Request.Form["tbxSysIcon"];
            info.TechnicalContactPhone = Request.Form["tbxContactPhone"];
            info.TechnicalContactPerson = Request.Form["tbxContactPerson"];
            info.SysState = Request.Form["tbxSysState"];
            info.SysType = Request.Form["ddlSysType"];
            info.LoginUrl = Request.Form["tbxLoginUrl"];
            int.TryParse(Request.Form["tbxSysOrder"], out int order);
            info.SysOrder = order;
            return service.UpdateSysInfo(info);
        }

        [HttpPost]
        public string BindSysLogin()
        {
            var loginPwd = Request.Form["tbxPwd"];
            var loginName = Request.Form["tbxUserNum"];
            var sysID = 0;
            int.TryParse(Request.Form["SysID"], out sysID);
            var userInfo = serviceUser.GetUserInfoByNum(User.Identity.Name);
            var ip = "10.126.0.45";

            return service.BindUserSys(userInfo.UserNum,ip,userInfo.UserName,loginName,loginPwd,sysID);
        }
    }
}