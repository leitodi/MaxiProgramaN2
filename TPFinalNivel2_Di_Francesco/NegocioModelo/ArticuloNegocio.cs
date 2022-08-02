using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace NegocioModelo
{
    public class ArticuloNegocio
    {
        
        public List<Articulos> listado()
        {
            
            List<Articulos> lista = new List<Articulos>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            try
            {
                conexion.ConnectionString = @"Data Source=DESKTOP-VUAF0M7\SQLEXPRESS;Initial Catalog=CATALOGO_DB;Integrated Security=True";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select a.id,codigo,nombre,a.Descripcion ,m.Descripcion Marca,c.Descripcion categoria, ImagenUrl,precio,m.id idmarca,c.id idcategoria " +
                    "from ARTICULOS a, MARCAS m, CATEGORIAS c " +
                    "where a.IdMarca=m.Id and a.IdMarca=c.Id";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulos articulo = new Articulos();
                    articulo.Id = (int)lector["id"];
                    articulo.Codigo = (string)lector["codigo"];
                    articulo.Nombre = (string)lector["nombre"];
                    articulo.Descripcion = (string)lector["descripcion"];
                    articulo.Marca = new Marca();
                    articulo.Marca.Descripcion = (string)lector["marca"];
                    articulo.Marca.Id = (int)lector["idmarca"];
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Descripcion = (string)lector["categoria"];
                    articulo.Categoria.Id = (int)lector["idcategoria"];
                    articulo.ImagenUrl = (string)lector["imagenurl"];
                    articulo.Precio = Convert.ToDouble(lector["precio"]);

                    lista.Add(articulo);

                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }           
        }
        public void agregar(Articulos objeto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
               
                datos.consulta("insert into Articulos (codigo,nombre,descripcion,idmarca,idcategoria,imagenurl,precio) " +
                    "values (@codigo,@nombre,@descripcion,@idmarca,@idcategoria,@imagenurl,@precio)");
                
                datos.parametros("@codigo", objeto.Codigo);
                datos.parametros("@nombre", objeto.Nombre);
                datos.parametros("@descripcion", objeto.Descripcion);
                datos.parametros("@idmarca", objeto.Marca.Id);
                datos.parametros("@idcategoria", objeto.Categoria.Id);
                datos.parametros("@imagenurl", objeto.ImagenUrl);
                datos.parametros("@precio", objeto.Precio);

                datos.leer();

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

        public void modificar(Articulos a)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.consulta("update articulos set codigo = @codigo, nombre = @nombre, Descripcion = @descripcion, idmarca = @idmarca, IdCategoria = @idcategoria, ImagenUrl = @imagenurl, precio = @precio " +
                    "where id = @id");
                datos.parametros("@codigo",a.Codigo);
                datos.parametros("@nombre",a.Nombre);
                datos.parametros("@descripcion",a.Descripcion);
                datos.parametros("@idmarca",a.Marca.Id);
                datos.parametros("@idCategoria",a.Categoria.Id);
                datos.parametros("@imagenurl",a.ImagenUrl);
                datos.parametros("@precio",a.Precio);
                datos.parametros("@id", a.Id);

                datos.accion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrar();
            }
        }
        public void eliminar(int id)
        {            
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.consulta("delete from ARTICULOS where id=@id");
                datos.parametros("@id",id);
                datos.accion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
