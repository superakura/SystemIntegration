using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemIntegration.Service;

namespace SystemIntegration.Web.Controllers
{
    [Authorize(Roles = "登录")]
    public class SysInfoController : Controller
    {
        private ISysInfoService service;
        public SysInfoController(ISysInfoService service)
        {
            this.service = service;
        }
        // GET: SysInfo
        public ViewResult List()
        {
            return View();
        }
        public ViewResult ListAll()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllList()
        {
            return Json(service.GetAllSysInfoList(User.Identity.Name.ToString()));
        }

        [HttpPost]
        public JsonResult GetUserList()
        {
            return Json(service.GetSysInfoList(User.Identity.Name.ToString()));
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
    }
}