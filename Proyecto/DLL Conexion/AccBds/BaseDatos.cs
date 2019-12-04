using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Xml;

/// <summary>
/// Summary description for AccesoDatos
/// </summary>
/// 
    namespace AccBds
{
    public class BaseDatos
    {
        /***********************************************************************************************
        ******************* CONEXION PARA ARCHIVOS DE ACCESS UTILIZANDO OLEDB **************************
        ***********************************************************************************************/

        string[] conexionParams;

        private string user = "";
        private string pass = "";
        private string port = "";
        private string host = "";
        private string instancia = "";
        
        private SqlConnection conexion = null;
        private SqlCommand command = null;
        private SqlTransaction _Transact;
        private string cadconex;

        public SqlCommand Comando
        {
            get { return command; }
            set { command = value; }
        }

        public BaseDatos(string based)
        {
            try
            {
                SYSWEB.SYSWEB miSyswebConfig = new AccBds.SYSWEB.SYSWEB();
                conexionParams = miSyswebConfig.ObtenerConfig().Split('|');
                this.user = conexionParams[3];
                this.pass = conexionParams[4];
                this.host = conexionParams[1];
                this.instancia = conexionParams[2];
                if (conexionParams.Length == 6)
                {
                    this.port = conexionParams[5];
                    cadconex = "Data Source=" + host + "," + port + ";Network Library=DBMSSOCN;Initial Catalog=" + based + ";User ID=" + user + ";Password=" + pass + "; Connection Lifetime=10; Max Pool Size=10000";
                }
                else
                {
                    cadconex = "server=" + host + @"\" + instancia + "; user id=" + user + "; pwd=" + pass + "; database=" + based + "; Connection Lifetime=10; Max Pool Size=10000";
                }
                command = new SqlCommand();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BaseDatos(string host, string user, string pass, string based)
        {
            cadconex = @"server=" + host + "; user id=" + user + "; pwd=" + pass + "; database=" + based + "; Connection Lifetime=10; Max Pool Size=10000";
            command = new SqlCommand();
        }

        public BaseDatos(string host, string user, string pass, string based, string port)
        {
            cadconex = @"server=" + host + "; port=" + port + " ; user id=" + user + "; pwd=" + pass + "; database=" + based + "; Connection Lifetime=10; Max Pool Size=10000";
            command = new SqlCommand();
        }

        public void Conectar()
        {
            try
            {
                if (conexion == null)
                {
                    conexion = new SqlConnection();
                    conexion.ConnectionString = cadconex;
                }
                conexion.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Desconectar()
        {
            try { if (conexion.State == ConnectionState.Open) conexion.Close(); }
            catch (Exception) { throw; }
        }

        public void BeginTransaction()
        {
            _Transact = conexion.BeginTransaction();
        }

        public void Commit()
        {
            _Transact.Commit();
        }

        public void Rollback()
        {
            _Transact.Rollback();
        }

        public void CrearComando(string SQL)
        {
            command.Connection = conexion;
            command.CommandType = CommandType.Text;
            command.CommandText = SQL;
        }

        public void execProcedure(ref string msj, ref bool? proc)
        {
            string r_mensaje = "";
            bool? r_estatus = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand("PROC_VERIFICADATOS", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@salMensajeProc", SqlDbType.VarChar, 150);
                    cmd.Parameters.Add("@salEstatusProc", SqlDbType.Bit);
                    cmd.Parameters["@salMensajeProc"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@salEstatusProc"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    r_mensaje = cmd.Parameters["@salMensajeProc"].Value.ToString();
                    r_estatus = Convert.ToBoolean(cmd.Parameters["@salEstatusProc"].Value.ToString());

                }
            }
            catch (Exception) { }

            proc = r_estatus;
            msj = r_mensaje;
            
                
        }

        public SqlDataReader CargarBD()
        {
            try { return command.ExecuteReader(); }
            catch (Exception) { throw; }
        }

        public DataTable CargarBd()
        {
            try {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable datatable = new DataTable();
                sqlDataAdapter.Fill(datatable);
                return datatable;
            }
            catch (Exception) { throw; }
        }

        public XmlReader CargarBdXML()
        {
            try { return command.ExecuteXmlReader(); }
            catch (Exception) { throw; }
        }

        public int TransacBD()
        {
            try { return this.command.ExecuteNonQuery(); }
            catch (Exception) { throw; }
        }

        public void TransacBD_BT()
        {
            try
            {
                this.command.Transaction = this._Transact;
                this.command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }

}
