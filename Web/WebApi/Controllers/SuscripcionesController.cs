using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class SuscripcionesController : ApiController
    {
        public int Get()
        {
            return new Suscripciones().ConsultarSuscripcion();
        }
        public bool Delete(int SubscripcionId)
        {
            return new Suscripciones().EliminarSuscripcion(SubscripcionId);
        }
    }
}
