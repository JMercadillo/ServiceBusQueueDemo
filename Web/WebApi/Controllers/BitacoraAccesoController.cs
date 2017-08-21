using System.Web.Http;
using WebApi.DataContext;

namespace WebApi.Controllers
{
    public class BitacoraAccesoController : ApiController
    {
        public bool Post(BitacoraIngresos bitacoraAcceso)
        {
            using (var context = new DataContext.NotificationsDemoEntities())
            {
                context.BitacoraIngresos.Add(bitacoraAcceso);
                return context.SaveChanges() > 0;
            }
        }
    }
}
