using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NegocioModelo;
using Dominio;

namespace Presentacion_final
{
    public partial class frmArticulo : Form
    {
        private Articulos articulo = null;
        public frmArticulo()
        {
            InitializeComponent();
        }
        public frmArticulo(Articulos modificar)
        {
            InitializeComponent();
            this.articulo = modificar;
            Text = "Modificar Articulo";
        }

        private void Articulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio cate = new CategoriaNegocio();  
            MarcaNegocio marca =new MarcaNegocio();
            try
            {
                cboCategoria.DataSource = cate.listado();
                cboCategoria.ValueMember = "id";
                cboCategoria.DisplayMember = "descripcion";
                cboMarca.DataSource = marca.listado();
                cboMarca.ValueMember = "id";
                cboMarca.DisplayMember = "descripcion";
                if (articulo != null)
                {
                    txtCodigo.Text=articulo.Codigo;
                    txtNombre.Text=articulo.Nombre;
                    txtDescripcion.Text=articulo.Descripcion;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    txtURL.Text = articulo.ImagenUrl;
                    imagen(articulo.ImagenUrl);//carga una imagen vacia si es q no tiene imagen 
                    txtPrecio.Text = articulo.Precio.ToString(); 
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Articulos a = new Articulos(); //usamos ahora el atributo private de la clase
            ArticuloNegocio n = new ArticuloNegocio();
            try
            {
                if(articulo == null)
                {
                    articulo = new Articulos();
                }
                articulo.Codigo=txtCodigo.Text;
                articulo.Nombre =txtNombre.Text;
                articulo.Descripcion =txtDescripcion.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria =(Categoria)cboCategoria.SelectedItem;
                articulo.ImagenUrl = txtURL.Text;
                articulo.Precio = Convert.ToDouble(txtPrecio.Text);

                if (articulo.Id != 0) //el ID es autoincremental... por eso verifico en este momento si existe o no
                {
                    n.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    n.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }
                
                

                

                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtURL_Leave(object sender, EventArgs e)
        {
            imagen(txtURL.Text);
        }

        private void imagen(string imagen)
        {
            try
            {
               pbImagen.Load(imagen);
            }
            catch (Exception ex)
            {

                pbImagen.Load("https://monstar-lab.com/global/wp-content/uploads/sites/11/2019/04/male-placeholder-image.jpeg");
            }
        }
    }
}
