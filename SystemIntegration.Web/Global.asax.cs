using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using SystemIntegration.Common;
using TenderInfo.App_Start;
using AutoMapper;
using SystemIntegration.Models;
using SystemIntegration.Service.ViewModels;
using SystemIntegration.Service.Dtos;
using System.Security.Principal;

namespace SystemIntegration.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(x => {
                x.CreateMap<LogInfo, VLogInfo>();
                x.CreateMap<NoticeInfo, NoticeInfoDto>();
                });
            //Mapper.Initialize(x => x.CreateMap<NoticeInfo, NoticeInfoDto>());

            //初始化容器，并返回适用于MVC的AutoFac解析器
            System.Web.Mvc.IDependencyResolver autoFacResolver = Container.Init();
            //将AutoFac解析器设置为系统DI解析器
            DependencyResolver.SetResolver(autoFacResolver);
        }

        public MvcApplication()
        {
            AuthorizeRequest += new EventHandler(Application_AuthenticateRequest);
        }

        public static FormsAuthenticationTicket RenewTicketIfOld(FormsAuthenticationTicket tOld)
        {
            if (tOld == null)
            {
                return null;
            }
            DateTime now = DateTime.Now;
            TimeSpan span = (TimeSpan)(now - tOld.IssueDate);
            TimeSpan span2 = (TimeSpan)(tOld.Expiration - now);
            if (span2 > span)
            {
                return tOld;
            }
            return new FormsAuthenticationTicket(tOld.Version, tOld.Name, now, now + (tOld.Expiration - tOld.IssueDate), tOld.IsPersistent, tOld.UserData, tOld.CookiePath);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies["dandian"];

            var isAjax = Context.Request.Headers.Get("x-requested-with");
            if (isAjax == "XMLHttpRequest")
            {
                var url = Context.Request.RawUrl;
                if (url == "/NoticeInfo/GetNoticeListForLogin" || url == "/NoticeInfo/GetNoticeInfoForLogin" || url == "/Home/GetTestUserList")
                {
                    return;
                }
                if (authCookie == null || authCookie.Value == "")
                {
                    Context.Response.StatusCode = 499;
                }
            }

            if (authCookie == null || authCookie.Value == "")
            {
                return;
            }
            else
            {
                FormsAuthenticationTicket authTicket = null;
                try
                {
                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                }
                catch
                {
                    return;
                }
                string[] roles = authTicket.UserData.Split(new char[] { ',' });

                #region 设置登录cookie滑动过期
                //FormsAuthenticationTicket ticket = null;
                //ticket = FormsAuthentication.RenewTicketIfOld(authTicket);

                //string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                //System.Web.HttpCookie cookie = new System.Web.HttpCookie("dandian", encryptedTicket);
                //cookie.Expires = ticket.Expiration;
                //System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                #endregion

                //Context.User = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);

                if (Context.User == null)
                {
                    Context.User = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
                }
                //if (Context.User != null)
                //{
                //    Context.User = new System.Security.Principal.GenericPrincipal(Context.User.Identity, roles);
                //}
            }

        }
    }
}
