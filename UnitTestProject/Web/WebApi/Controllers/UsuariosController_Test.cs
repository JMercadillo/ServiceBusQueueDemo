using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;

namespace UnitTestProject.Web.WebApi.Controllers
{
    [TestClass]
    public class UsuariosController_Test
    {
        [TestMethod]
        public void GetUsuarioPorId()
        {
            var controller = new UsuariosController();

            var usuario = controller.Get(3);

            Assert.IsNotNull(usuario);
        }

        [TestMethod]
        public void GetTodosUsuarios()
        {
            var controller = new UsuariosController();

            var usuario = controller.Get();

            Assert.IsNotNull(usuario);
        }
    }
}
