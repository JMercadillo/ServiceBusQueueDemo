using Seguridad.Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp.Security
{
    public class PermissionAttribute : ActionFilterAttribute
    {
        public RolesPermisos Permiso { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!FrontUser.TienePermiso(this.Permiso))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
        }
    }
}