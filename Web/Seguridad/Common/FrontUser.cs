using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad.Common
{
    public class FrontUser
    {
        public static bool TienePermiso(RolesPermisos permiso)
        {
            var usuario = FrontUser.Get();
            return !usuario.Rol.Permisos.Where(x => x.PermisoId == permiso).Any();
        }

        public static Usuario Get()
        {
            return SessionHelper.CurrentUser;
        }
    }
}
