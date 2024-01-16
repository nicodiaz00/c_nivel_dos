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
using negocio;

namespace presentacion
{
    public partial class frmAgregarArticulo : Form
    {
        public frmAgregarArticulo()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo articuloNuevo = new Articulo();
            ArticuloNegocio conexionNegocio = new ArticuloNegocio();
            try
            {
                articuloNuevo.CodigoArticulo = txtCodigo.Text;
                articuloNuevo.Nombre = txtNombre.Text;
                articuloNuevo.Descripcion = txtDescripcion.Text;
                articuloNuevo.Marca = (Marca)cboMarca.SelectedItem;
                articuloNuevo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articuloNuevo.Precio = decimal.Parse(txtPrecio.Text);
                articuloNuevo.UrlImagen = txtImagen.Text;

                conexionNegocio.agregarArticulo(articuloNuevo);
                MessageBox.Show("Articulo agregado exitosamente");
                Close();
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
                cboCategoria.DataSource = varCategoriaNegocio.listarCategoria();

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
    }
}
