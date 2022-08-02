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
                comando.CommandText = "select numero, nombre, p.Descripcion, UrlImagen, e.Descripcion tipo, d.Descripcion debilidad, p.id,p.idtipo,p.iddebilidad from POKEMONS p, ELEMENTOS e, ELEMENTOS d where p.IdTipo=e.Id and d.Id=p.IdDebilidad And P.Activo = 1";
                comando.Connection = conexion;

                conexion.Open();
                lector= comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon oPoke = new Pokemon();
                    oPoke.Id = (int)lector["id"];
                    oPoke.Numero = (int)lector["numero"];
                    oPoke.Nombre = (string)lector["nombre"];
                    oPoke.Descripcion = (string)lector["descripcion"];

                    if(!(lector["urlimagen"] is DBNull))
                    oPoke.UrlImagen = (string)lector["urlimagen"];
                    oPoke.Tipo = new Elemento(); 
                    oPoke.Tipo.Id = (int)lector["idtipo"];
                    oPoke.Tipo.Descripcion = (string)lector["tipo"];
                    oPoke.Debilidad = new Elemento();
                    oPoke.Debilidad.Id= (int)lector["iddebilidad"];  
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
                acceso.setearConsulta("Insert into POKEMONS (Numero, Nombre, Descripcion, IdTipo, IdDebilidad,urlimagen, Activo)" +
                    "values(@numero,@nombre,@descripcion,@idTipo, @idDebilidad,@urlimagen,1)");
                acceso.setearParametro("@numero", nuevo.Numero);
                acceso.setearParametro("@nombre", nuevo.Nombre);
                acceso.setearParametro("@descripcion", nuevo.Descripcion);
                
                acceso.setearParametro("@idTipo", nuevo.Tipo.Id);
                acceso.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
                acceso.setearParametro("@urlimagen", nuevo.UrlImagen);

                acceso.ejecturaAccion();
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
            AccesoDatos acceso = new AccesoDatos();
            try
            {
                acceso.setearConsulta("update POKEMONS set Numero = @numero, Nombre = @nombre, Descripcion = @desc, UrlImagen = @img, IdTipo = @idTipo, IdDebilidad = @idDebilidad " +
                    "Where Id = @id");
                acceso.setearParametro("@numero", modificar.Numero);
                acceso.setearParametro("@nombre", modificar.Nombre);
                acceso.setearParametro("@desc", modificar.Descripcion);
                acceso.setearParametro("@img", modificar.UrlImagen);
                acceso.setearParametro("@idtipo", modificar.Tipo.Id);
                acceso.setearParametro("@idDebilidad", modificar.Debilidad.Id);
                acceso.setearParametro("@id", modificar.Id);

                acceso.ejecturaAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }finally
            {
                acceso.cerrarConexion();
            }
        }
        public void eliminacionFi(int id)
        {
            try
            {
                AccesoDatos accion = new AccesoDatos();
                accion.setearConsulta("delete from pokemons where id = @id");
                accion.setearParametro("@id", id);
                accion.ejecturaAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminacionLo(int id)
        {
            try
            {
                AccesoDatos accion = new AccesoDatos();
                accion.setearConsulta("update POKEMONS set Activo = 0 Where id = @id");
                accion.setearParametro("@id", id);
                accion.ejecturaAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
