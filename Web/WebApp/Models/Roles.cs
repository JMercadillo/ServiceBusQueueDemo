using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Roles
    {
        private int RolId { get; set; }
        private string Nombre { get; set; }
        private string Descripcion { get; set; }
        private System.DateTime CreadoEn { get; set; }
    }
}