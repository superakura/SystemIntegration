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
        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetList()
        {
            return Json(service.GetSysInfoList());
        }
    }
}