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

namespace AppPokemon
{
    public partial class Form1 : Form
    {
        private List<Pokemon> pokemonList = new List<Pokemon>();
        private List<Elemento> elementoList = new List<Elemento>(); 
        elementoNegocio elemento = new elementoNegocio();
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void dgvPoke_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void cargar()
        {
            PokemonNegocio datos = new PokemonNegocio();
            try
            {
                pokemonList = datos.listar();
                dgvPoke.DataSource = pokemonList;
                dgvPoke.Columns["urlimagen"].Visible = false;
                cargarImagen(pokemonList[0].UrlImagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPoke_SelectionChanged(object sender, EventArgs e)
        {
           Pokemon oPokemon = (Pokemon)dgvPoke.CurrentRow.DataBoundItem;// dgvPoke.CurrentRow.DataBoundItem =
                                                                        // un objeto lo tengo q hacer pokemon y
                                                                        // lo guardo en variable pokemon
            cargarImagen(oPokemon.UrlImagen); //cargamos la imagen del atributo urlimagen
        }
        //capturar excepciones con un metodo 
        //voy a recibir por parametro la imagen para cargar
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaPokemon alta = new frmAltaPokemon();
            alta.ShowDialog();
            cargar();
        }

        private void pbImagen_Click(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado;
            seleccionado = (Pokemon)dgvPoke.CurrentRow.DataBoundItem;

            frmAltaPokemon alta = new frmAltaPokemon(seleccionado);
            alta.ShowDialog();
            cargar();

        }
    }
}
