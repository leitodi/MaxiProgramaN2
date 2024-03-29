﻿using System;
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
            cboCampo.Items.Add("Numero");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripcion");
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
                ocultar();
                cargarImagen(pokemonList[0].UrlImagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ocultar()
        {
            dgvPoke.Columns["urlimagen"].Visible = false;
            dgvPoke.Columns["id"].Visible = false;
            dgvPoke.Columns["urlimagen"].Visible = false;
        }

        private void dgvPoke_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPoke.CurrentRow != null) 
            { 
           Pokemon oPokemon = (Pokemon)dgvPoke.CurrentRow.DataBoundItem;// dgvPoke.CurrentRow.DataBoundItem =
                                                                        // un objeto lo tengo q hacer pokemon y
                                                                        // lo guardo en variable pokemon
            cargarImagen(oPokemon.UrlImagen); //cargamos la imagen del atributo urlimagen
            }
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

            frmAltaPokemon modificar = new frmAltaPokemon(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        private void btnEliminarF_Click(object sender, EventArgs e)
        {
            eliminar();
        }
        public void eliminar(bool logico = false)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            Pokemon seleccion;
            try
            {
                if(MessageBox.Show("Seguro desea eliminar?","Eliminando",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
                { 
                    seleccion= (Pokemon)dgvPoke.CurrentRow.DataBoundItem;

                if (logico)

                    negocio.eliminacionLo(seleccion.Id);
                else negocio.eliminacionFi(seleccion.Id);
                }
                cargar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            PokemonNegocio poke = new PokemonNegocio();
            try
            {
                if (validar()) { 
                string campo = cboCampo.SelectedItem.ToString();
                string criterio=cboCriterio.SelectedItem.ToString();
                string filtro =txtFiltro2.Text;
                dgvPoke.DataSource = poke.filtrar(campo, criterio, filtro);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool validar()
        {
            bool retorno = true;
            if (cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor eljia un campo");
                retorno = false;
                cboCampo.Focus();
            }
            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor eljia un criterio");
                retorno = false;
                cboCriterio.Focus();
            }
            if (cboCampo.SelectedItem.ToString() == "Numero")
            {
                if (!numeros(txtFiltro2.Text))
                {
                    MessageBox.Show("Por favor solo numeros");
                    retorno = false;
                    txtFiltro2.Focus();
                }
            }
            return retorno;
        }

        private bool numeros(string text)
        {
            bool retorno= true;
            foreach (char caracter in text)
            {
                if (!char.IsNumber(caracter))
                {
                    retorno = false;
                }
            }
            return retorno;
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Pokemon> listFiltro;
            string filtro = txtFiltro.Text;
            if (filtro.Length>4)
            {
                listFiltro = pokemonList.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Tipo.Descripcion.ToUpper().Contains(filtro.ToUpper()));//requiere exprecion lambda p=>
                //contains metodo de cadenar que devuelve lo que contiene 
            }
            else
            {
                listFiltro = pokemonList;
            }
            dgvPoke.DataSource = null;
            dgvPoke.DataSource = listFiltro;//se debe limpiar la lista por q no se pisa 
            ocultar();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if(opcion == "Numero")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }

        private void txtFiltro2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
