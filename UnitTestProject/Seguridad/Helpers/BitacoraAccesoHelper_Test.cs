using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seguridad.Helpers;
using Seguridad.Models;

namespace UnitTestProject.Seguridad.Helpers
{
    [TestClass]
    public class BitacoraAccesoHelper_Test
    {
        [TestMethod]
        public void Post()
        {
            var _helper = new BitacoraAccesoHelper();

            var user = new Usuario().Obtener(3);
            
            var terminal = "";
            try
            {
                terminal = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var result = _helper.Post(
                new Bitacora()
                {
                    Accion = "S",
                    BitacoraIngresosId = 0,
                    Departamento = user.Rol.Nombre,
                    Fecha = DateTime.Now,
                    NombreCompleto = user.Nombre,
                    NombreUsuario = user.Nombre,
                    UsuarioId = user.UsuarioId.ToString(),
                    Terminal = terminal
                }
                );

            Assert.IsNotNull(result);
        }
    }
}
