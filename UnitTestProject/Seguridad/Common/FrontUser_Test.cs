using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seguridad.Common;

namespace UnitTestProject.Seguridad.Common
{
    [TestClass]
    public class FrontUser_Test
    {
        [TestMethod]
        public void TienePermisos()
        {
            // SessionHelper necesita contexto de solicitud web,
            // hay que ingresar un id manual.
            var result = FrontUser.TienePermiso(RolesPermisos.Usuario_Crear_Usuario);

            Assert.IsFalse(result);
        }
    }
}
