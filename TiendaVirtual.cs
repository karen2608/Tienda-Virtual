using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TALLER_2_1000897727_1007111554_1214738496
{
    public partial class TiendaVirtual : Form
    {
        public TiendaVirtual()
        {
            InitializeComponent();
            //codigo para ponerle fondo al formulario
            Bitmap img = new Bitmap(Application.StartupPath + @"\img\fondo1.jpg");
            this.BackgroundImage = img;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        //variables globales
        int can = 0;
        decimal pre = 0;
        // se creo la lista para mostrar en el dataGridView
        List<RegistroTienda> lista = new List<RegistroTienda>();
        
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Comando para salir de la aplicación 
            Application.Exit();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            //condiciones de validacion
            if (validarnombrelibro() == false)
            {
                return;
            }
            else if (validarautor() == false)
            {
                return;
            }
            else if (validarcantidad() == false)
            {
                return;
            }
            else if (validargenero() == false)
            {
                return;
            }
            else if (validarprecio() == false)
            {
                return;
            }

            var nombre_libro = txtNombre.Text;
            var autor = txtAutor.Text;
            var cantidad = can;
            var genero = cmbGenero.Text;
            var precio = pre;

            RegistroTienda NuevoRegistro = new RegistroTienda(nombre_libro, autor, cantidad, genero, precio);
            
            //codigo para imprimir la lista en el data grid
            NuevoRegistro.Nombre_Libro = txtNombre.Text;
            NuevoRegistro.Autor = txtAutor.Text;
            NuevoRegistro.Cantidad = can;
            NuevoRegistro.Genero = cmbGenero.Text;
            NuevoRegistro.Precio = pre;
            lista.Add(NuevoRegistro);
            MessageBox.Show("Se genero el registro correctamente");
            dgvRegistro.DataSource = null;
            dgvRegistro.DataSource = lista;
            limpiar();
            //se llama el metodo estatico para realizar un conteo de los registros
            RegistroTienda.NumeroDeRegistro += 1;
            txtCalcular.Text = Convert.ToString(RegistroTienda.NumeroDeRegistro);
        }
        #region Validaciones
        // Metodos para validar que los controles no esten vacios
        private bool validarprecio()
        {
            
            if (!decimal.TryParse(txtPrecio.Text, out pre) || txtPrecio.Text == "")
            {
                error.SetError(txtPrecio, "Debe ingresar un valor numerico");
                txtPrecio.Clear();
                txtPrecio.Focus();
                return false;
            }
            else
            {
                error.SetError(txtPrecio, "");
                return true;
            }
        }
        private bool validargenero()
        {
            if (String.IsNullOrEmpty(cmbGenero.Text))
            {
                error.SetError(cmbGenero, "Debe ingresar el Autor");
                return false;
            }
            else
            {
                error.SetError(cmbGenero, "");
                return true;
            }
        }

        private bool validarcantidad()
        {
            if (!int.TryParse(txtCantidad.Text, out can) || txtCantidad.Text == "")
            {
                error.SetError(txtCantidad, "Debe ingresar un valor numerico");
                txtCantidad.Clear();
                txtCantidad.Focus();
                return false;
            }
            else
            {
                error.SetError(txtCantidad, "");
                return true;
            }
        }

        private bool validarautor()
        {
            if (String.IsNullOrEmpty(txtAutor.Text))
            {
                error.SetError(txtAutor, "Debe ingresar el Autor");
                return false;
            }
            else
            {
                error.SetError(txtAutor, "");
                return true;
            }
        }

        private bool validarnombrelibro()
        {
            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                error.SetError(txtNombre, "Debe ingresar un nombre del libro");
                return false;
            }
            else
            {
                error.SetError(txtNombre, "");
                return true;
            }
            #endregion 
        }
        #region Limpiar
        // Metodo limpiar, no retorna ningun valor ya que solo se va a utilizar para limpia o borrar los controles 
        private void limpiar()
        {
            txtNombre.Clear();
            txtAutor.Clear();
            txtCantidad.Clear();
            cmbGenero.SelectedIndex = 0;
            txtPrecio.Clear();
        }
        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
           //condición para la busqueda
            if (validarnombrelibro() == false)
            {
                MessageBox.Show("Debes agregar el nombre \ndel libro para poder hacer \nla busqueda");
                return;
            }
            RegistroTienda lista = GetNombre(txtNombre.Text);
          
            if (lista == null)
            {
                error.SetError(txtNombre, "No se encontro el libro");
                limpiar();
                txtNombre.Focus();
                return;
            }
            else
            {
                error.SetError(txtNombre, "");
                txtAutor.Text = lista.Autor;
                txtNombre.Text = lista.Nombre_Libro;
                cmbGenero.SelectedItem = lista.Genero;
                txtCantidad.Text = lista.Cantidad.ToString();
                txtPrecio.Text = lista.Precio.ToString();
            }
        }
        #region Busqueda
        // metodo de busqueda
        private RegistroTienda GetNombre(string nombre)
        {
            return lista.Find(mas => mas.Nombre_Libro.Contains(nombre));
        }
        #endregion

        private void limpiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //condicion eliminar el dato registrado en el datagrid
            if (txtNombre.Text == "")
            {
                error.SetError(txtNombre, "Debe bucar para poder eliminar");
                limpiar();
                txtNombre.Focus();
                btnEliminar.Enabled = false;
            }
            else
            {
                error.SetError(txtNombre, "");
                DialogResult result = MessageBox.Show("Esta seguro que quiere eliminar", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    foreach (RegistroTienda dato in lista)
                    {
                        if (dato.Nombre_Libro == txtNombre.Text)
                        {
                            lista.Remove(dato);
                            break;
                        }
                    }
                    limpiar();
                    dgvRegistro.DataSource = null;
                    dgvRegistro.DataSource = lista;

                    RegistroTienda.NumeroDeRegistro -= 1;
                    txtCalcular.Text = Convert.ToString(RegistroTienda.NumeroDeRegistro);
                }
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            //condicion realizada para mostar el total del registro 
            decimal suma = 0;
            foreach (var item in lista)
            {
                suma += item.Precio;
            }
            txtTotal.Text = Convert.ToString(suma); 
        }

        
    }
}
