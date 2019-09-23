using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemIntegration.Service;
using SystemIntegration.Service.Dtos;
using SystemIntegration.Service.ViewModels;

namespace SystemIntegration.Web.Controllers
{
    [Authorize(Roles = "系统维护")]
    public class NoticeInfoController : Controller
    {
        private INoticeInfoService _serviceNotice;
        public NoticeInfoController(INoticeInfoService serviceNotice)
        {
            this._serviceNotice = serviceNotice;
        }
        // GET: NoticeInfo
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult List()
        {
            try
            {
                VNoticeInfoCondition input = new VNoticeInfoCondition();
                var limit = 0;
                int.TryParse(Request.Form["limit"], out limit);
                input.limit = limit;
                var offSet = 0;
                int.TryParse(Request.Form["offset"], out offSet);
                input.offset = offSet;
                input.NoticeTitle = Request["title"];
                input.InsertPersonNum = Request["userNum"];
                input.NoticeType = Request["type"];
                var list = _serviceNotice.GetNoticePageInfo(input);

                return Json(list);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult GetOne()
        {
            try
            {
                var id = 0;
                int.TryParse(Request.Form["id"], out id);
                var info = _serviceNotice.GetOne(id);
                return Json(info);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult GetListCount()
        {
            try
            {
                var noticeCount = 0;
                int.TryParse(Request.Form["count"], out noticeCount);
                var info = _serviceNotice.GetNoticeInfoByCount(noticeCount);
                return Json(info);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public string Save()
        {
            try
            {
                var id = 0;
                int.TryParse(Request.Form["tbxNoticeInfoID"],out id);

                var info = id == 0 ?new NoticeInfoDto():_serviceNotice.GetOne(id);
                info.InsertPersonNum = User.Identity.Name;
                info.InsertDate = DateTime.Now;
                info.NoticeContent= Request.Form["tbxNoticeContent"].ToString();
                info.NoticeTitle= Request.Form["tbxNoticeTitle"].ToString();
                info.ContentType= Request.Form["tbxContentType"].ToString();

                return _serviceNotice.Save(info) ?"ok":"error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public string Del()
        {
            try
            {
                var id = 0;
                int.TryParse(Request.Form["id"], out id);
                return _serviceNotice.Del(id)?"ok":"error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}