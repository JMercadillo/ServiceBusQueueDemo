﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApp.Models;

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

        protected void Session_Start(object sender, EventArgs e)
        {
            NotificationComponent NC = new NotificationComponent();
            var currentTime = DateTime.Now;

            if (Session["Usuario"] != null)
            {
                var usuario = Session["Usuario"] as Usuario;
                HttpContext.Current.Session["LastUpdated"] = currentTime;
                NC.RegisterNotification(currentTime, usuario);
            }
        }

        protected void Application_End()
        {
            SqlDependency.Stop(conString);
        }
    }
}
