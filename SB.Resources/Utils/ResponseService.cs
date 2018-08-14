using SB.Interfaces;
using System.Collections.Generic;

namespace SB.Resources
{
    public class ResponseService: IResponseService
    {
        public List<string> Errores { get; set; }

        public dynamic Resultado { get; set; }

        public bool Estado { get; set; }

        public int TotalItems { get; set; }

        public ResponseService()
        {
            Estado = false;
            Errores = new List<string>();
        }

        public void EstablecerRespuesta(bool estado, dynamic resultado = null, int totalItems = 0)
        {
            Estado = estado;
            Resultado = resultado;
            TotalItems = totalItems;
        }
    }
}
