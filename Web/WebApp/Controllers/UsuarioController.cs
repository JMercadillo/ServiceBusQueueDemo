using Seguridad.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Helper;
using WebApp.Security;

namespace WebApp.Controllers
{
    [Authenticate]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Ver()
        {
            var listaUsuarios = new UsuariosHelper().GetAll();

            return View(listaUsuarios);
        }
    }
}