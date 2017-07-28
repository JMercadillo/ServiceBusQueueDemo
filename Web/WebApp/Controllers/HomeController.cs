using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        protected readonly List<Usuario> Usuarios = new List<Usuario>()
        {
              new Usuario(){ Id=0, Nombre = "Josué Ulises", Apellido = "Mercadillo Flores", Correo = "jmercadillo@cgclatam.com", Contrasenia = "abc123." },
              new Usuario(){ Id=1, Nombre = "Nataly Paola", Apellido = "Domínguez Landaverde", Correo = "metal_uli@hotmail.com", Contrasenia = "mercally05" },
              new Usuario(){ Id=2, Nombre = "Roberto Esaú", Apellido = "Perdomo Aragón", Correo = "josuemercally@gmail.com", Contrasenia = "mercally14" }
        };

        public ActionResult Index()
        {
            ViewBag.Message = "Por favor inicie sesión.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            if (Usuarios.Where(x => x.Correo == usuario.Correo && x.Contrasenia == usuario.Contrasenia).Count() == 1)
            {
                var user = Usuarios.Where(x => x.Correo == usuario.Correo && x.Contrasenia == usuario.Contrasenia).First();
                Session["Usuario"] = user as Usuario;

                return RedirectToAction("Calculadora");
            }

            if (Usuarios.Where(x => x.Correo == usuario.Correo).Count() < 1)
                ModelState.AddModelError("Correo", "Verifique su correo.");
            if (Usuarios.Where(x => x.Contrasenia == usuario.Contrasenia).Count() < 1)
                ModelState.AddModelError("Contrasenia", "Verifique su contraseña.");

            return View();
        }

        public ActionResult Calculadora()
        {
            var user = Session["Usuario"] as Usuario;
            ViewBag.Message = $"{user.Nombre} ingresa unos números.";

            return View();
        }

        [AsyncTimeout(60)]
        public ActionResult Resultado(Calculo calculo)
        {
            int result;
            if (calculo.Operador == 3 && calculo.Numero2 == 0)
            {
                ModelState.AddModelError("Numero2", "No se puede dividir un número entre 0.");
                return View("Calculadora");
            }
            else
            {
                var calHelper = new CalculadoraHelper();

                var user = Session["Usuario"] as Usuario;
                ViewBag.Message = $"{user.Nombre} tu resultado está listo.";

                result = calHelper.Operar(calculo);

                //Envia el mensaje a la cola
                calculo.Usuario = user;
                calHelper.EnviarOperar(calculo);
            }

            return View(result);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}