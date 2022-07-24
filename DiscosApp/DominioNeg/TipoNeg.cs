using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace DominioNeg
{
    public class TipoNeg
    {
        public List<Tipo> Listar()
        {
            List<Tipo> lista = new List<Tipo>();
            ConexionDB conexionDB = new ConexionDB();
            try
            {
                conexionDB.consulta("select id, descripcion  from TIPOSEDICION");
                conexionDB.leer();
                while (conexionDB.Reader.Read())
                {
                    Tipo aux = new Tipo();
                    aux.id = (int)conexionDB.Reader["id"];
                    aux.Descripcion = (string)conexionDB.Reader["descripcion"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conexionDB.cerrar();
            }
        }
    }
}
