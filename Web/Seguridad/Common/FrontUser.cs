using Seguridad.Models;
using System.Linq;

namespace Seguridad.Common
{
    public class FrontUser
    {
        public static bool TienePermiso(RolesPermisos permiso)
        {
            var permi = permiso.GetHashCode();
            var usuario = FrontUser.Get();
            return !usuario.Rol.PermisosDenegadosPorRol.Where(x => x.PermisoId == permi).Any();
        }

        public static Usuario Get()
        {
            return new Usuario().Obtener(SessionHelper.CurrentUser.UsuarioId);
        }
    }
}
