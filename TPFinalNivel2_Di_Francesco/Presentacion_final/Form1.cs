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
using NegocioModelo;


namespace Presentacion_final
{
    public partial class frmCatalogo : Form
    {
        
        List<Articulos> listaArt = new List<Articulos>();
        public frmCatalogo()
        {
            InitializeComponent();
        }

        private void frmCatalogo_Load(object sender, EventArgs e)
        {
            cargar();

        }

        private void cargar()
        {
            ArticuloNegocio negocioArticulo = new ArticuloNegocio();
            try
            {
                listaArt = negocioArticulo.listado();
                dgvArticulos.DataSource = listaArt;
                dgvArticulos.Columns["imagenurl"].Visible = false;
                dgvArticulos.Columns["precio"].Visible = false;
                dgvArticulos.Columns["codigo"].Visible = false;
                dgvArticulos.Columns["id"].Visible = false;
                imagen(listaArt[0].ImagenUrl);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void imagen(string imagen)
        {
            try
            {
                pkImagen.Load(imagen);
            }
            catch (Exception ex)
            {

                pkImagen.Load("https://monstar-lab.com/global/wp-content/uploads/sites/11/2019/04/male-placeholder-image.jpeg");
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulos oArticulo = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;

            imagen(oArticulo.ImagenUrl);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmArticulo nuevo = new frmArticulo();
            nuevo.ShowDialog(); 
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Articulos seleccion = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;

            frmArticulo alta = new frmArticulo(seleccion);
            alta.ShowDialog();
            cargar();
        }
    }
}
