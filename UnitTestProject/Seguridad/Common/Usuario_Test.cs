using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seguridad.Models;

namespace UnitTestProject.Seguridad.Common
{
    [TestClass]
    public class Usuario_Test
    {
        [TestMethod]
        public void GetUsuario()
        {
            var _classUsuario = new Usuario();

            var user = _classUsuario.Obtener(3);

            Assert.IsNotNull(user);

        }
    }
}
