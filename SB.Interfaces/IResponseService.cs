using System.Collections.Generic;

namespace SB.Interfaces
{
    public interface IResponseService
    {
        List<string> Errores { get; set; }

        dynamic Resultado { get; set; }

        bool Estado { get; set; }

        int TotalItems { get; set; }

        void EstablecerRespuesta(bool estado, dynamic resultado = null, int totalItems = 0);
    }
}
