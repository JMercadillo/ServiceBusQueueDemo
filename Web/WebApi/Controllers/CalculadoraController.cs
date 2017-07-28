using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class CalculadoraController : ApiController
    {
        public int GET(int Numero1, int Operador, int Numero2)
        {
            int result = 0;
            switch (Operador)
            {
                case (0):
                    result = Numero1 + Numero2;
                    break;
                case (1):
                    result = Numero1 - Numero2;
                    break;
                case (2):
                    result = Numero1 * Numero2;
                    break;
                case (3):
                    result = Numero1 / Numero2;
                    break;
                default:
                    result = -1;
                    break;
            }
            return result;
        }
    }
}
