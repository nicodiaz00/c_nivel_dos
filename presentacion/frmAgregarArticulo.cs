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
using negocio;

namespace presentacion
{
    public partial class frmAgregarArticulo : Form
    {
        private Articulo articulo = null;
        public frmAgregarArticulo()
        {
            InitializeComponent();
        }
        public frmAgregarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
        }
        //funcion validaciones
        public void validarPrecio(KeyPressEventArgs tecla)
        {
            
            if (char.IsDigit(tecla.KeyChar))
            {
                tecla.Handled = false;
            }
            else if (char.IsSeparator(tecla.KeyChar))
            {
                tecla.Handled = false;
            }
            else if (char.IsControl(tecla.KeyChar))
            {
                tecla.Handled = false;
            }
            else 
            {
                tecla.Handled = true;
                   
            }
            
        }
        public void soloTextoYNumeroYEspacio(KeyPressEventArgs tecla)
        {
            if (!char.IsControl(tecla.KeyChar) && !char.IsLetterOrDigit(tecla.KeyChar) && tecla.KeyChar != ' ')
            {
                tecla.Handled = true;
            }
        }
        public void soloTextoNumero(KeyPressEventArgs tecla)
        {
            if(!char.IsControl(tecla.KeyChar)&& !char.IsLetterOrDigit(tecla.KeyChar))
            {
                tecla.Handled = true;
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Articulo articuloNuevo = new Articulo();
            ArticuloNegocio conexionNegocio = new ArticuloNegocio();
            try
            {
                if(articulo == null)
                {
                    articulo = new Articulo();
                }
                
                    articulo.CodigoArticulo = txtCodigo.Text;
                    articulo.Nombre = txtNombre.Text;
                    articulo.Descripcion = txtDescripcion.Text;
                    articulo.Marca = (Marca)cboMarca.SelectedItem;
                    articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                if(txtPrecio.Text !="")
                {
                    articulo.Precio = decimal.Parse(txtPrecio.Text);



                }
                    
                    articulo.UrlImagen = txtImagen.Text;

                bool datosVacios = false;

                if(txtCodigo.Text == "" || txtNombre.Text == "" || txtDescripcion.Text == "" || txtPrecio.Text == "")
                {
                    datosVacios = true;
                }
                if(datosVacios == true)
                {
                    MessageBox.Show("Debe ingresar Codigo, Nombre, Descripcion y Precio");
                }
                else
                {
                    if (articulo.Id != 0)
                    {
                        conexionNegocio.modificarArticulo(articulo);
                        MessageBox.Show("Articulo modificado exitosamente");
                    }
                    else
                    {
                        conexionNegocio.agregarArticulo(articulo);
                        MessageBox.Show("Articulo agregado exitosamente");
                    }
                    Close();
                }
                    
                
                

                    

                    

                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio varMarcaNegocio = new MarcaNegocio();
            CategoriaNegocio varCategoriaNegocio = new CategoriaNegocio();
            try
            {
                cboMarca.DataSource = varMarcaNegocio.listarMarca();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "DescripcionMarca";
                cboCategoria.DataSource = varCategoriaNegocio.listarCategoria();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "DescripcionCategoria";

                if(articulo !=null)
                {
                    txtCodigo.Text = articulo.CodigoArticulo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text= articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtImagen.Text = articulo.UrlImagen;
                    cargarImagen(articulo.UrlImagen);
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pcbCargarImagen.Load(imagen);
            }
            catch (Exception ex)
            {
                
                pcbCargarImagen.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/6/65/No-Image-Placeholder.svg/1665px-No-Image-Placeholder.svg.png");
            }
        }

        

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarPrecio(e);
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloTextoNumero(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloTextoYNumeroYEspacio(e);
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloTextoYNumeroYEspacio(e);
        }
    }
}
