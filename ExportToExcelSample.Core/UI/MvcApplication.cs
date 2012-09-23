using System;
using System.Web.Mvc;
using System.Web.Routing;
using ExportToExcelSample.Core.Infrastructure;
using StructureMap;

namespace ExportToExcelSample.Core.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );

        }

        protected void Application_Start()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
            {
                scan.WithDefaultConventions();
                scan.AssemblyContainingType(typeof (IParameterLessQuery<>));
            }));

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }

    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (controllerType == null) 
                return null;

            return ObjectFactory.GetInstance(controllerType) as Controller;
        }
    }
}