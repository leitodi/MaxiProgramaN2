using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace dominioNegocio
{
    internal class AccesoDatos
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader lector;       

        public SqlDataReader Lector
        {
            get { return lector; }
            
        }
        public AccesoDatos()
        {           
            con = new SqlConnection("Data Source=DESKTOP-VUAF0M7\\SQLEXPRESS;Initial Catalog=POKEDEX_DB;Integrated Security=True");
            cmd = new SqlCommand();   
        }

        public void setearConsulta(string query)
        {
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
        }
        public void ejecturaLectura()
        {
            cmd.Connection = con;
            try
            {
                con.Open();
                lector = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public void ejecturaAccion()
        {
            cmd.Connection = con;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }

        public void cerrarConexion()
        {
            if (lector != null)
            {
                lector.Close();
            }
            con.Close();
        }
        public void setearParametro(string nombre, object valor)
        {
            cmd.Parameters.AddWithValue(nombre, valor);
        }

    }
}
