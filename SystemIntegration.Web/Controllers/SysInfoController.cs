using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemIntegration.Service;

namespace SystemIntegration.Web.Controllers
{
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

        public JsonResult GetAllList()
        {
            var userNum = Request.Form["userNum"];
            return Json(service.GetAllSysInfoList(userNum));
        }
        public JsonResult GetUserList()
        {
            var userNum = Request.Form["userNum"];
            return Json(service.GetSysInfoList(userNum));
        }
    }
}