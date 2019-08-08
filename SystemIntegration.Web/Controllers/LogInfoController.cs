using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemIntegration.Service;
using SystemIntegration.Service.ViewModels;

namespace SystemIntegration.Web.Controllers
{
    public class LogInfoController : Controller
    {
        private ILogInfoService _service;
        public LogInfoController(ILogInfoService service)
        {
            this._service = service;
        }
        // GET: LogInfo
        public ViewResult List()
        {
            return View();
        }

        public JsonResult GetLogInfoList()
        {
            var input = new VLogListCondition();
            input.content = Request.Form["content"];
            var limit = 0;
            int.TryParse(Request.Form["limit"],out limit);
            input.limit = limit;
            var offSet=0;
            int.TryParse(Request.Form["offset"],out offSet);
            input.offset = offSet;
            input.ip = Request.Form["ip"];
            if (User.IsInRole("全部日志"))
            {
                input.userNum = Request.Form["userNum"];
                input.QueryRole = "管理员";
            }
            else
            {
                input.userNum = User.Identity.Name;
                input.QueryRole = "用户";
            }
            return Json(_service.GetLogInfoList(input));
        }
    }
}