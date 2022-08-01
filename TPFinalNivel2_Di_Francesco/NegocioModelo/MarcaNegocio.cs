using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace NegocioModelo
{
    public class MarcaNegocio
    {
        public List<Marca> listado()
        {
            List<Marca> marcas = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consulta("select descripcion, id from Marcas");
                datos.leer();

                while (datos.Lector.Read())
                {
                    Marca m = new Marca();
                    m.Id=(int)datos.Lector["id"];
                    m.Descripcion = (string)datos.Lector["descripcion"];
                    marcas.Add(m);
                }
                return marcas;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrar();
            }
        }
    }
}
