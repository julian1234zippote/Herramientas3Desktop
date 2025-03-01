using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wEventosSociales
{
    public class clsEvento
    {
      //  public int CodigoEvento { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Ubicacion { get; set; }
        public int InvitadosAprox { get; set; }
        public string Descripcion { get; set; }
        public string TipoEvento { get; set; } // Nueva propiedad para guardar el tipo de evento

    }
}
