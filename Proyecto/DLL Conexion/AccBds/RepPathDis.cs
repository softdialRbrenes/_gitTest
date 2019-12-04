using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;

namespace AccBds
{
    public class RepPathDis
    {
        #region DECLARACION DE INSTANCIAS
            BaseDatos Conexion;
        #endregion

        #region METODOS PUBLICOS
            /// <summary>
            /// Funcion para conectar a la base datos y encontrar la direccion en donde se encuentran los reportes o formatos generados.
            /// </summary>
            /// <param name="BDActual">Base Datos a la cual esta conectado actualmente.</param>
            /// <returns>Cadena de la direccion en donde se debe ir a buscar el reporte o formato</returns>
            public string SacarRepPathDis(string BDActual)
            {
                try
                {
                    Conexion = new BaseDatos(BDActual);
                    string RepPathDis = string.Empty;
                    string StrSQL = string.Empty;
                    SqlDataReader dr;

                    StrSQL = "SELECT CONF_DATO FROM emp_configuracion WHERE (CONF_DESCRIPCION = 'RepPathDis')";

                    Conexion.Conectar();
                    Conexion.CrearComando(StrSQL);
                    dr = Conexion.CargarBD();

                    if (dr.Read())
                    {
                        RepPathDis = dr.GetString(0).ToString();
                    }
                    return RepPathDis;
                }
                catch (Exception)
                {
                    throw;
                }
        #endregion

        }
    }
}
