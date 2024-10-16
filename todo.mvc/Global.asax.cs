using System.Web.Hosting;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using todo.mvc.App_Start;

namespace todo.mvc;

public class MvcApplication : System.Web.HttpApplication
{
    protected void Application_Start()
    {
        AreaRegistration.RegisterAllAreas();
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        DependenciesConfig.RegisterDependencies();
    }
}
