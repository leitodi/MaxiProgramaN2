using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Disco
    {
        public string Titulo { get; set; }
        public DateTime fecha { get; set; }
        public string url { get; set; }        
        public Tipo Estilo { get; set; }
        public Tipo Formato { get; set; }

    }
}
