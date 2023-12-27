using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listarCategoria()
        {
            List<Categoria> listaDeCategoria = new List<Categoria>();
            AccesoDatos conexionDatosCategoria = new AccesoDatos();
            try
            {
                conexionDatosCategoria.setearConsulta("select Id, Descripcion from CATEGORIAS");
                conexionDatosCategoria.ejecutarLectura();
                while (conexionDatosCategoria.Lector.Read())
                {
                    Categoria varCategoria = new Categoria();
                    varCategoria.Id = (int)conexionDatosCategoria.Lector["Id"];
                    varCategoria.DescripcionCategoria = (string)conexionDatosCategoria.Lector["Descripcion"];
                    listaDeCategoria.Add(varCategoria);
                }
                return listaDeCategoria;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexionDatosCategoria.cerrarConexion();
            }
        }
    }
}
