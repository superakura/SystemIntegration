using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
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
    }
}
