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
                datos.setearConsulta("Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, M.Descripcion as Marca, C.Descripcion as Categoria, A.IdMarca, A.IdCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id");
                datos.ejecutarLectura();

                while(datos.Lector.Read())
                {
                    Articulo articuloAuxiliar = new Articulo();
                    articuloAuxiliar.Id = (int)datos.Lector["Id"];
                    articuloAuxiliar.CodigoArticulo = (string)datos.Lector["Codigo"];
                    articuloAuxiliar.Nombre = (string)datos.Lector["Nombre"];
                    articuloAuxiliar.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        articuloAuxiliar.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    }
                    decimal precio = (decimal)datos.Lector["Precio"];
                    decimal precioRedondeado = (Math.Round(precio, 0));
                    articuloAuxiliar.Precio = precioRedondeado;
                    articuloAuxiliar.Marca = new Marca();
                    articuloAuxiliar.Marca.Id = (int)datos.Lector["IdMarca"];
                    articuloAuxiliar.Marca.DescripcionMarca = (string)datos.Lector["Marca"];

                    articuloAuxiliar.Categoria = new Categoria();
                    articuloAuxiliar.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articuloAuxiliar.Categoria.DescripcionCategoria = (string)datos.Lector["Categoria"];


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
        public void agregarArticulo(Articulo articuloNuevo)
        {
            AccesoDatos conexionDatos = new AccesoDatos();
            try
            {
                conexionDatos.setearConsulta("insert into ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,Precio,ImagenUrl) values(@Codigo,@Nombre,@Descripcion,@idMarca,@idCategoria,@Precio,@ImagenUrl)");
                conexionDatos.setParametro("@Codigo", articuloNuevo.CodigoArticulo);
                conexionDatos.setParametro("@Nombre", articuloNuevo.Nombre);
                conexionDatos.setParametro("@Descripcion", articuloNuevo.Descripcion);
                conexionDatos.setParametro("@idMarca", articuloNuevo.Marca.Id);
                conexionDatos.setParametro("@idCategoria", articuloNuevo.Categoria.Id);
                conexionDatos.setParametro("@Precio", articuloNuevo.Precio);
                conexionDatos.setParametro("@ImagenUrl", articuloNuevo.UrlImagen);

                conexionDatos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conexionDatos.cerrarConexion();
            }
        }
        public void modificarArticulo(Articulo articuloModificado)
        {
            AccesoDatos conexDatos = new AccesoDatos();
            try
            {
                conexDatos.setearConsulta("update ARTICULOS set Codigo =@codigo, Nombre=@nombre, Descripcion =@descripcion,IdMarca =@idMarca,IdCategoria=@idCategoria,ImagenUrl=@imagenUrl,Precio =@precio where Id =@id");
                conexDatos.setParametro("@codigo",articuloModificado.CodigoArticulo);
                conexDatos.setParametro("@nombre", articuloModificado.Nombre);
                conexDatos.setParametro("@descripcion", articuloModificado.Descripcion);
                conexDatos.setParametro("@idMarca", articuloModificado.Marca.Id);
                conexDatos.setParametro("idCategoria", articuloModificado.Categoria.Id);
                conexDatos.setParametro("@imagenUrl", articuloModificado.UrlImagen);
                conexDatos.setParametro("@precio", articuloModificado.Precio);
                conexDatos.setParametro("@id", articuloModificado.Id);

                conexDatos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexDatos.cerrarConexion();
            }
        }

    }
}
