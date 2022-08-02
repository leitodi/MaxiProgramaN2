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
                ocultar();
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
            if (dgvArticulos.CurrentRow != null)
            {            
            Articulos oArticulo = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;

            imagen(oArticulo.ImagenUrl);
            }
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

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio a = new ArticuloNegocio();
             
            try
            {
                Articulos ar = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
                if (MessageBox.Show("Seguro desea eliminar?", "Eliminando", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) { 
                    a.eliminar(ar.Id);
                    cargar();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro que desa salir?", "Saliendo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            List<Articulos> filtroList;
            string filtro = txtFiltro.Text;
            if(filtro != "")
            {
                filtroList=listaArt.FindAll(a => a.Nombre.ToUpper().Contains(filtro.ToUpper())|| a.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()));

            }
            else
            {
                filtroList = listaArt;
            }
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = filtroList;
            ocultar();
        }

        private void ocultar()
        {
            dgvArticulos.Columns["imagenurl"].Visible = false;
            dgvArticulos.Columns["precio"].Visible = false;
            dgvArticulos.Columns["codigo"].Visible = false;
            dgvArticulos.Columns["id"].Visible = false;
        }
    }
}
