using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace NegocioModelo
{
    public class CategoriaNegocio
    {

        public List<Categoria> listado()
        {
            List<Categoria> categorias = new List<Categoria>();
            AccesoDatos datos=new AccesoDatos();
            try
            {
                datos.consulta("select descripcion, id from CATEGORIAS");
                datos.leer();

                while (datos.Lector.Read())
                {
                    Categoria c = new Categoria();
                    c.Descripcion = (string)datos.Lector["Descripcion"];
                    c.Id = (int)datos.Lector["id"];
                    categorias.Add(c);
                }
                return categorias;
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
