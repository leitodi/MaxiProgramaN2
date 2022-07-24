using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace dominioNegocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar()
        {
            List<Pokemon> list = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando=new SqlCommand();
            SqlDataReader lector;
            try
            {
                conexion.ConnectionString = "Data Source=DESKTOP-VUAF0M7\\SQLEXPRESS;Initial Catalog=POKEDEX_DB;Integrated Security=True";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select numero, nombre, p.Descripcion, UrlImagen, e.Descripcion tipo, d.Descripcion debilidad from POKEMONS p, ELEMENTOS e, ELEMENTOS d where p.IdTipo=e.Id and d.Id=p.IdDebilidad";
                comando.Connection = conexion;

                conexion.Open();
                lector= comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon oPoke = new Pokemon();
                    oPoke.Numero = (int)lector["numero"];
                    oPoke.Nombre = (string)lector["nombre"];
                    oPoke.Descripcion = (string)lector["descripcion"];

                    if(!(lector["urlimagen"] is DBNull))
                    oPoke.UrlImagen = (string)lector["urlimagen"];
                    oPoke.Tipo = new Elemento(); 
                    oPoke.Tipo.Descripcion = (string)lector["tipo"];
                    oPoke.Debilidad = new Elemento();
                    oPoke.Debilidad.Descripcion = (string)lector["debilidad"];

                    list.Add(oPoke);
                }

                return list;//si todo esta bien retorna lista 
            }
            catch (Exception e)
            {

                throw e; // si hay algun error retorna exepcion
            }



             
        }
        public void agregar (Pokemon nuevo)
        {
            AccesoDatos acceso = new AccesoDatos();
            try
            {
                acceso.setearConsulta("Insert into POKEMONS (Numero, Nombre, Descripcion, Activo, IdTipo, IdDebilidad,urlimagen)values(" + nuevo.Numero + ", '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "', 1, @idTipo, @idDebilidad,@urlimagen)");
                acceso.setearParametro("@idTipo", nuevo.Tipo.Id);
                acceso.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
                acceso.setearParametro("@urlimagen", nuevo.UrlImagen);
                acceso.ejecturaLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                acceso.cerrarConexion();
            }
        }
        public void modificar(Pokemon modificar)
        {

        }
    }
}
