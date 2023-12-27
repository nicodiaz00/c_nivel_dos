using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listarMarca()
        {
            List<Marca> listaDemarca = new List<Marca>();
            AccesoDatos conexionADatos = new AccesoDatos();
            try
            {
                conexionADatos.setearConsulta("select Id, Descripcion from MARCAS");
                conexionADatos.ejecutarLectura();
                while (conexionADatos.Lector.Read())
                {
                    Marca varMarca = new Marca();
                    varMarca.Id = (int)conexionADatos.Lector["Id"];
                    varMarca.DescripcionMarca = (string)conexionADatos.Lector["Descripcion"];
                    listaDemarca.Add(varMarca);
                }
                return listaDemarca;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conexionADatos.cerrarConexion();
            }
        }
    }
}
