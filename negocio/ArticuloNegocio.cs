using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listarArticulo()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            

            try
            {
                datos.setearConsulta("Select A.Codigo, A.Nombre,A.Descripcion, A.ImagenUrl from ARTICULOS A");
                datos.ejecutarLectura();

                while(datos.Lector.Read())
                {
                    Articulo articuloAuxiliar = new Articulo();
                    articuloAuxiliar.CodigoArticulo = (string)datos.Lector["Codigo"];
                    articuloAuxiliar.Nombre = (string)datos.Lector["Nombre"];
                    articuloAuxiliar.Descripcion = (string)datos.Lector["Descripcion"];
                    articuloAuxiliar.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    lista.Add(articuloAuxiliar);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }


            
            

        }

    }
}
