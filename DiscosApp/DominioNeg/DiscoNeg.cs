using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace DominioNeg
{
    public class DiscoNeg
    {

        public List<Disco> listar()
        {
            List<Disco> lista = new List<Disco>();
            ConexionDB db = new ConexionDB();
            try
            {
                db.consulta("select titulo, FechaLanzamiento,UrlImagenTapa  from DISCOS");
                db.leer();
                while (db.Reader.Read())
                {
                    Disco aux = new Disco();
                    aux.Titulo = (string)db.Reader["titulo"];
                    aux.fecha = (DateTime)db.Reader["fechalanzamiento"];
                    aux.url= (string)db.Reader["urlimagentapa"];

                    lista.Add(aux);
                }
                return lista;   
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.cerrar();
            }
            
        }
    }
}
