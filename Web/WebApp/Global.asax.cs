using Seguridad.Common;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string conString = ConfigurationManager.ConnectionStrings["NotificationsConnectionString"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SqlDependency.Start(conString);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            // Give the user some information, but
            // stay on the default page
            Exception exc = Server.GetLastError();
            Response.Write("<h2>Global Page Error</h2>\n");
            Response.Write(
                "<p>" + exc.Message + "</p>\n");
            Response.Write("Volver a <a href='/Home/Index'>" +
                "Inicio</a>\n");

            // Log the exception and notify system operators
            var source = Request.RawUrl;
            ExceptionUtility.LogException(exc, source, Seguridad.Common.SessionHelper.CurrentUser);
            ExceptionUtility.NotifySystemOps(exc);

            // Clear the error from the server
            Server.ClearError();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_End()
        {
            SqlDependency.Stop(conString);
        }
    }
}
