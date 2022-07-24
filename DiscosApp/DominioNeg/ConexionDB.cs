using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dominio;

namespace DominioNeg
{
    public class ConexionDB
    {
        private SqlCommand cmd ;
        private SqlConnection con;
        private SqlDataReader reader ;
      
        //creamos la prop para aceder al lector READER
        public SqlDataReader Reader
        {
            get { return reader; }            
        }
        //creamos contrusctor de la clase
        public ConexionDB()
        {
            con = new SqlConnection("Data Source=DESKTOP-VUAF0M7\\SQLEXPRESS;Initial Catalog=DISCOS_DB;Integrated Security=True");
            cmd= new SqlCommand();
        }

        //METODOS 
        public void consulta (string query)
        {
            cmd.CommandType=System.Data.CommandType.Text;
            cmd.CommandText = query;
        }

        public void leer()
        {
            cmd.Connection = con;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void cerrar()
        {
            //cerrar el lector q haya quedado abierto
            if(reader!=null)
                reader.Close();
            con.Close();
        }

    }
}


