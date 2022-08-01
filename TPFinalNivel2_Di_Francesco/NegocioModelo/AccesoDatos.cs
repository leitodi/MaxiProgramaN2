using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace NegocioModelo
{
    public class AccesoDatos
    {
        private SqlCommand cmd;
        private SqlConnection con;
        private SqlDataReader lector;
        public AccesoDatos()
        {
            cmd=new SqlCommand();
            con = new SqlConnection(@"Data Source=DESKTOP-VUAF0M7\SQLEXPRESS;Initial Catalog=CATALOGO_DB;Integrated Security=True");
        }
        public SqlDataReader Lector
        {
            get { return lector;  }
        }       
        public void consulta (string query)
        {
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
        }
        public void leer()
        {
            cmd.Connection = con;
            try
            {
                con.Open();
                lector= cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void accion()
        {
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                 throw ex;
            }
        }
                
        public void cerrar()
        {
            if (lector != null)
            {
                lector.Close();
            }
            con.Close();
        }

        public void parametros(string nombre, object valor)
        {
            cmd.Parameters.AddWithValue(nombre,valor);
        }
    }
}
