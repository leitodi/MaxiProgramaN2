using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using DominioNeg;

namespace DiscosApp
{
    public partial class Form1 : Form
    {
        ConexionDB conexion = new ConexionDB();
        List<Disco> lista = new List<Disco>();  
        List<Tipo> listaTipo = new List<Tipo>();   
        DiscoNeg  nuevo = new DiscoNeg(); 
        TipoNeg tipNuevo=new TipoNeg();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           lista=nuevo.listar();
           dgvDiscos.DataSource=lista;
            listaTipo = tipNuevo.Listar();
            

        }
        private void Imagen(string url)
        {
            try
            {
                pbDiscos.Load(url);
            }
            catch (Exception)
            {

                pbDiscos.Load("https://monstar-lab.com/global/wp-content/uploads/sites/11/2019/04/male-placeholder-image.jpeg");
            }
        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            Disco oDisco = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
            Imagen(oDisco.url);
        }
    }
}
