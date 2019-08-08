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

            //��ʼ��������������������MVC��AutoFac������
            System.Web.Mvc.IDependencyResolver autoFacResolver = Container.Init();
            //��AutoFac����������ΪϵͳDI������
            DependencyResolver.SetResolver(autoFacResolver);
        }
        public MvcApplication()
        {
            AuthorizeRequest += new EventHandler(Application_AuthenticateRequest);
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
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
                if (Context.User != null)
                {
                    Context.User = new System.Security.Principal.GenericPrincipal(Context.User.Identity, roles);
                }
            }
        }
    }
}
