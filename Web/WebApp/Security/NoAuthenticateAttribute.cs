using Seguridad.Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp.Security
{
    // Si estamos logeado ya no podemos acceder a la página de Login
    public class NoAuthenticate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession())
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