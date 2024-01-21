using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;


namespace presentacion
{
    public partial class frmVentanaPrincipal : Form
    {
        private List<Articulo> listaArticulo;
        public frmVentanaPrincipal()
        {
            InitializeComponent();
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pictureBoxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {

                pictureBoxArticulo.Load("https://i.postimg.cc/52mdzCM6/No-Image-Placeholder-svg.png");
            }
        }
        private void frmVentanaPrincipal_Load(object sender, EventArgs e)
        {
            cargarLista();
        }


        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo nuevoArticulo = new frmAgregarArticulo();
            nuevoArticulo.ShowDialog();
            cargarLista();
            

        }
        private void cargarLista()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {

                listaArticulo = negocio.listarArticulo();
                dgvArticulos.DataSource = listaArticulo;
                dgvArticulos.Columns["UrlImagen"].Visible = false;
                dgvArticulos.Columns["Id"].Visible = false; //oculto columna id
                cargarImagen(listaArticulo[0].UrlImagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado;
            articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAgregarArticulo articuloModificado = new frmAgregarArticulo(articuloSeleccionado);
            articuloModificado.ShowDialog();
            cargarLista();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNeg = new ArticuloNegocio();
            Articulo articuloSeleccionado;
            try
            {
                DialogResult respuestaEliminacion = MessageBox.Show("Eliminar definitivamente?", "Eliminando",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if(respuestaEliminacion == DialogResult.Yes)
                {
                    articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    articuloNeg.eliminarArticulo(articuloSeleccionado.Id);
                    MessageBox.Show("Articulo eliminado");
                    cargarLista();
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
