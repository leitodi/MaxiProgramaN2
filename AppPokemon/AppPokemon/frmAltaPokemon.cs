using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using dominioNegocio;
using System.Configuration;
using System.IO;

namespace AppPokemon
{
    public partial class frmAltaPokemon : Form
    {
        private Pokemon  pokemon =null;
        private OpenFileDialog archivo = null;
        public frmAltaPokemon()
        {
            InitializeComponent();
        }

        public frmAltaPokemon(Pokemon poke)
        {
            InitializeComponent();
            this.pokemon= poke;
            Text = "Modificar pokemon";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();   
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            PokemonNegocio negocio = new PokemonNegocio();

            try
            {
                if(pokemon==null)
                    pokemon =new Pokemon();
                pokemon.Numero = int.Parse(txtNumero.Text);
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion=txtDescripcion.Text;
                pokemon.UrlImagen = txtUrl.Text;
                pokemon.Tipo = (Elemento)cboTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cboDebilidad.SelectedItem;
                //si se levanto localmente
                if (archivo != null && !(txtUrl.Text.ToUpper().Contains("HTTP")))
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);

                if (pokemon.Id != 0)
                {
                    negocio.modificar(pokemon);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(pokemon);
                    MessageBox.Show("agregado exitosamente");
                }
                
                Close();

            }
            catch (Exception ex )
            {

                MessageBox.Show(ex.ToString());    
            }
            
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            elementoNegocio negocio  = new elementoNegocio();
            try
            {
                cboTipo.DataSource = negocio.listar();
                cboTipo.ValueMember = "id";
                cboTipo.DisplayMember = "descripcion";
                cboDebilidad.DataSource= negocio.listar();
                cboDebilidad.ValueMember = "id";
                cboDebilidad.DisplayMember = "descripcion";

                if (pokemon != null)
                {
                    txtNumero.Text = pokemon.Numero.ToString();
                    txtNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrl.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen);
                    cboTipo.SelectedValue = pokemon.Tipo.Id;
                    cboDebilidad.SelectedValue = pokemon.Debilidad.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrl.Text);
        }
        private void cargarImagen(string imagen)
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

        private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg|png|*.png";

            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtUrl.Text = archivo.FileName;
                cargarImagen(archivo.FileName);

                //guardar imagen 
                //para usar ocnfiguraton tenemos q agregar en referencia y using 
                //File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);
            }
        }
    }
}
