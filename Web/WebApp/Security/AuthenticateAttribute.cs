using Seguridad.Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp.Security
{
    // Si no estamos logeado, regresamos al login
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!SessionHelper.ExistUserInSession())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Common",
                    action = "Login"
                }));
            }
        }
    }
}