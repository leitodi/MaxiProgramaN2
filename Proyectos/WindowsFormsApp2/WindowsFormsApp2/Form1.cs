using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class frmDatos : Form
    {
        public frmDatos()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string datos = "";
            if (validar())
            {
                datos = "Apellido y Nombre: " + txtApellido.Text +" "+ txtNombre.Text + "\n" + " Edad: " + txtEdad.Text + "\n" + " Direccion: " + txtDireccion.Text;
                txtResultado.Text = datos;
                borrar();
            }
            
        }

        private void color()
        {
            txtApellido.BackColor=Color.White;
            txtNombre.BackColor = Color.White;
            txtEdad.BackColor = Color.White;
            txtDireccion.BackColor = Color.White;
        }

        private void borrar()
        {
            txtApellido.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtEdad.Text = String.Empty;
            txtDireccion.Text = String.Empty;
        }

        private bool validar()
        {
            bool valido = true;
            if (txtApellido.Text == "")
            {
                MessageBox.Show("Debe ingresar un apellido");
                txtApellido.Focus();
                txtApellido.BackColor = Color.Red;
                valido= false;
            }
            else if (txtNombre.Text == "")
            {
                MessageBox.Show("Debe ingresar un apellido");
                txtNombre.Focus();
                txtNombre.BackColor = Color.Red;
                valido = false;
            }
            else if (txtEdad.Text == "")
            {
                MessageBox.Show("Debe ingresar una edad");
                txtEdad.Focus();
                txtEdad.BackColor = Color.Red;
                valido = false;
            }
            else if (txtDireccion.Text == "")
            {
                MessageBox.Show("Debe ingresar la direccion");
                txtDireccion.Focus();
                txtDireccion.BackColor = Color.Red;
                valido = false;
            }
            return valido;
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtEdad.BackColor == Color.Red)
            {
                txtEdad.BackColor = Color.White;
            }
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtApellido_Click(object sender, EventArgs e)
        {
            if (txtApellido.BackColor == Color.Red)
            {
                txtApellido.BackColor = Color.White;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtApellido.BackColor == Color.Red)
            {
                txtApellido.BackColor = Color.White;
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtNombre.BackColor == Color.Red)
            {
                txtNombre.BackColor = Color.White;
            }
        }

        private void txtNombre_Click(object sender, EventArgs e)
        {
            if (txtNombre.BackColor == Color.Red)
            {
                txtNombre.BackColor = Color.White;
            }
        }

        private void txtEdad_Click(object sender, EventArgs e)
        {
            if (txtEdad.BackColor == Color.Red)
            {
                txtEdad.BackColor = Color.White;
            }
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDireccion_Click(object sender, EventArgs e)
        {
            if (txtDireccion.BackColor == Color.Red)
            {
                txtDireccion.BackColor = Color.White;
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtDireccion.BackColor == Color.Red)
            {
                txtDireccion.BackColor = Color.White;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Seguro que desea salir?","saliendo",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK) 
                    this.Close();
        }
    }
}
