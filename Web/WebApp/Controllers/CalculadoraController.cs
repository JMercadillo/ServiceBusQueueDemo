using Seguridad.Common;
using System.Web.Mvc;
using WebApp.Helper;
using WebApp.Models;
using WebApp.Security;

namespace WebApp.Controllers
{
    [Authenticate]
    public class CalculadoraController : Controller
    {
        // GET: Calculadora
        public ActionResult Index()
        {
            var user = SessionHelper.CurrentUser;
            ViewBag.Message = $"{user.Nombre} ingresa unos números.";

            return View();
        }

        public ActionResult ResultadoSB(Calculo calculo)
        {
            int result = 0;

            var calHelper = new CalculadoraHelper();

            var user = SessionHelper.CurrentUser;
            ViewBag.Message = $"{user.Nombre} tu estará listo pronto.";

            calculo.Usuario = user;
            //Envia el mensaje a la cola
            result = calHelper.EnviarOperar(calculo);

            if (result > 0)
                result = calHelper.Get();

            if (result > 0)
            {
                return View(model: "En breve caerá una notificación indicandote el resultado de tu operación.");
            }
            else
                return View();
        }

        public ActionResult Resultado(Calculo calculo)
        {
            var calHelper = new CalculadoraHelper();

            var user = SessionHelper.CurrentUser;
            ViewBag.Message = $"{user.Nombre} tu estará listo pronto.";

            calculo.Usuario = user;

            var result = calHelper.Post(calculo);

            return View(result);
        }
    }
}