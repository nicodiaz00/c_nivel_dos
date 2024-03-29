﻿using dominio;
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
using System.Runtime.CompilerServices;


namespace presentacion
{
    public partial class frmVentanaPrincipal : Form
    {
        private List<Articulo> listaArticulo;
        public frmVentanaPrincipal()
        {
            InitializeComponent();
        }
        public void ocultarColumnas()
        {
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false; //oculto columna id
        }
        //probando funciones de validacion
        
        
        public void busquedaArticulo()
        {
            List<Articulo> listaArticuloFiltro;
            string filtro = txtFiltroBusqueda.Text;
            if (filtro.Length <= 2)
            {
                listaArticuloFiltro = listaArticulo.FindAll(art => art.Nombre.ToLower().Contains(filtro.ToLower()) || art.Descripcion.ToLower().Contains(filtro.ToLower()));
            }
            else
            {
                listaArticuloFiltro = listaArticulo;
            }


            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaArticuloFiltro;
            ocultarColumnas();
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
            cboCampo.Items.Add("Codigo");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Precio");

            


        }


        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);

                labelNombre.Text = seleccionado.Nombre;
                labelCodigo.Text = seleccionado.CodigoArticulo;
                labelMarca.Text = seleccionado.Marca.DescripcionMarca;
                labelCategoria.Text = seleccionado.Categoria.DescripcionCategoria;
                labelPrecio.Text = seleccionado.Precio.ToString();
                lblDescripcion.Text = seleccionado.Descripcion;
            }
            
            
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
                ocultarColumnas();
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
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            busquedaArticulo();
        }
        private void txtFiltroBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            busquedaArticulo();
        }
        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccionado = cboCampo.SelectedItem.ToString();
            
            if(seleccionado == "Codigo")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Contiene");
                cboCriterio.Items.Add("Empieza con");
                cboCriterio.Items.Add("Termina con");
                txtAvanzado.Text = string.Empty;


            }else if(seleccionado == "Nombre")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Contiene");
                cboCriterio.Items.Add("Empieza");
                cboCriterio.Items.Add("Termina");
                txtAvanzado.Text = string.Empty;
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
                txtAvanzado.Text = string.Empty;
            }
            
        }   
        private void btnBusquedaAvanzada_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                if(cboCampo.SelectedIndex == -1 && cboCriterio.SelectedIndex == -1)
                {
                    MessageBox.Show("Ingrese Campo y criterio");
                    
                }
                 else if(cboCampo.SelectedItem.ToString()== "Precio" && txtAvanzado.Text == "")
                {                  
                        MessageBox.Show("Ingrese valor numerico");
                    
                }else if(cboCampo.SelectedIndex !=-1 && cboCriterio.SelectedIndex < 0)
                {
                    MessageBox.Show("Debe ingresar criterio");
                }
                else
                {
                    string campo = cboCampo.SelectedItem.ToString();
                    string criterio = cboCriterio.SelectedItem.ToString();
                    string filtroBusqueda = txtAvanzado.Text;

                    dgvArticulos.DataSource = articuloNegocio.filtrarArticulo(campo, criterio, filtroBusqueda);                  
                }                         
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        private void txtAvanzado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboCampo.SelectedIndex < 0 && cboCriterio.SelectedIndex< 0) {
                MessageBox.Show("elija campo y criterio antes");
                
            }
            else if (cboCampo.SelectedItem.ToString() == "Precio")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                
            }else if(cboCampo.SelectedItem.ToString() == "Nombre" || cboCampo.SelectedItem.ToString() == "Codigo")
            {
                if(!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            
        }
    }
}
