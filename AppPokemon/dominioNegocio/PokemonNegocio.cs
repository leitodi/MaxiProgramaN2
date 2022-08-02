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
            AccesoDatos datos = new AccesoDatos();  
            List<Pokemon> list = new List<Pokemon>();
            //SqlConnection conexion = new SqlConnection();
            //SqlCommand comando=new SqlCommand();
            //SqlDataReader lector;
            try
            {               
                datos.setearConsulta("select numero, nombre, p.Descripcion, UrlImagen, e.Descripcion tipo, d.Descripcion debilidad, p.id,p.idtipo,p.iddebilidad from POKEMONS p, ELEMENTOS e, ELEMENTOS d where p.IdTipo=e.Id and d.Id=p.IdDebilidad And P.Activo = 1");
                datos.ejecturaLectura();
                /// conexion.ConnectionString = "Data Source=DESKTOP-VUAF0M7\\SQLEXPRESS;Initial Catalog=POKEDEX_DB;Integrated Security=True";
                //comando.CommandType = System.Data.CommandType.Text;
                // comando.CommandText = "select numero, nombre, p.Descripcion, UrlImagen, e.Descripcion tipo, d.Descripcion debilidad, p.id,p.idtipo,p.iddebilidad from POKEMONS p, ELEMENTOS e, ELEMENTOS d where p.IdTipo=e.Id and d.Id=p.IdDebilidad And P.Activo = 1";
                // comando.Connection = conexion;
                // conexion.Open();
                //lector= comando.ExecuteReader();

                while (datos.Lector.Read())
                {
                    Pokemon oPoke = new Pokemon();
                    oPoke.Id = (int)datos.Lector["id"];
                    oPoke.Numero = (int)datos.Lector["numero"];
                    oPoke.Nombre = (string)datos.Lector["nombre"];
                    oPoke.Descripcion = (string)datos.Lector["descripcion"];

                    if(!(datos.Lector["urlimagen"] is DBNull))
                    oPoke.UrlImagen = (string)datos.Lector["urlimagen"];
                    oPoke.Tipo = new Elemento(); 
                    oPoke.Tipo.Id = (int)datos.Lector["idtipo"];
                    oPoke.Tipo.Descripcion = (string)datos.Lector["tipo"];
                    oPoke.Debilidad = new Elemento();
                    oPoke.Debilidad.Id= (int)datos.Lector["iddebilidad"];  
                    oPoke.Debilidad.Descripcion = (string)datos.Lector["debilidad"];

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

        public List<Pokemon> filtrar(string campo, string criterio, string filtro)
        {
           List<Pokemon> list = new List<Pokemon>();
           AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select numero, nombre, p.Descripcion, UrlImagen, e.Descripcion tipo, d.Descripcion debilidad, p.id,p.idtipo,p.iddebilidad from POKEMONS p, ELEMENTOS e, ELEMENTOS d where p.IdTipo = e.Id and d.Id = p.IdDebilidad And P.Activo = 1 and ";
                datos.setearConsulta(consulta);
                if (campo=="Numero")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Numero > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Numero < " + filtro;
                            break;
                        case "Igual a":
                            consulta += "Numero = " + filtro;
                            break;
                    }
                }
                else if (campo=="Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }else if (campo=="Descripcion")
                    {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "p.descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "p.descripcion like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "p.descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                datos.setearConsulta(consulta);
                datos.ejecturaLectura();
                while (datos.Lector.Read())
                {
                    Pokemon oPoke = new Pokemon();
                    oPoke.Id = (int)datos.Lector["id"];
                    oPoke.Numero = (int)datos.Lector["numero"];
                    oPoke.Nombre = (string)datos.Lector["nombre"];
                    oPoke.Descripcion = (string)datos.Lector["descripcion"];

                    if (!(datos.Lector["urlimagen"] is DBNull))
                        oPoke.UrlImagen = (string)datos.Lector["urlimagen"];
                    oPoke.Tipo = new Elemento();
                    oPoke.Tipo.Id = (int)datos.Lector["idtipo"];
                    oPoke.Tipo.Descripcion = (string)datos.Lector["tipo"];
                    oPoke.Debilidad = new Elemento();
                    oPoke.Debilidad.Id = (int)datos.Lector["iddebilidad"];
                    oPoke.Debilidad.Descripcion = (string)datos.Lector["debilidad"];

                    list.Add(oPoke);
                }

                return list;
            }
            catch (Exception ex)
            {

                throw ex;
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
