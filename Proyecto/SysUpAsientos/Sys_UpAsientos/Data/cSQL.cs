using System;
using System.Data;
using System.Windows.Forms;
using AccBds;
using Sys_UpAsientos_Entity;


namespace Sys_UpAsientos
{
    public class cSQL
    {
        private BaseDatos conexion;
        private string tdoc;
        private string fecha;
        private int v_asiD151;

        public cSQL(cEmpresas empresa, string p_doc, bool p_asiD151){
            this.tdoc = p_doc;
            this.v_asiD151 = p_asiD151 == true ? 1 : 0;
            conexion = new BaseDatos(empresa.v_IpProd+"\\"+empresa.v_Instancia, empresa.v_Login, empresa.v_Pass, empresa.v_Codigo);

        }

        public cSQL(cEmpresas empresa, bool p_asiD151)
        {
            this.v_asiD151 = p_asiD151==true?1:0;
            conexion = new BaseDatos(empresa.v_IpProd + "\\" + empresa.v_Instancia, empresa.v_Login, empresa.v_Pass, empresa.v_Codigo);
        }

        /// <summary>
        /// El metodo Temp crea las tablas temporales de encabezado y detalle de asientos
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Temp(DataTable t)
        {
            string varMsj = "";
            bool? proceso = false;

            try
            {
                this.conexion.Conectar();

                
                if (crearTemp(conexion))
                {
                    if (InsertTempEnc(t, conexion))
                    {
                        if (InsertTempDet(t, conexion))
                        {
                            proceso = verificaTemp(ref varMsj, conexion);

                            if (proceso == true)
                            {
                                if (UpdateTemp(conexion))
                                    proceso = true;

                                if (proceso == true)
                                    InsertAsiento(conexion);
                            }
                            else
                            {
                                MessageBox.Show("No se puede ingresar el asiento contable: " + varMsj);
                                proceso = false;
                            }
                        }                        
                        else
                        {
                            MessageBox.Show("No se inserta el detalle del pre asiento, validar datos!");
                            proceso = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se inserta el encabezado del pre asiento, validar datos!");
                        proceso = false;
                    }                  
                }
                else
                {
                    MessageBox.Show("Las tablas temporales no fueron creadas, comuníquese con el administrador del sistema!");
                    proceso = false;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return false; }
            finally { this.conexion.Desconectar(); }

            return (bool)proceso;

        }      

        /// <summary>
        /// Creacion de tablas temporales donde se insertaran los datos que vienen del excel
        /// </summary>
        /// <returns>true = creacion exitosa / false = error a nivel de datos</returns>
        private bool crearTemp(BaseDatos cox )
        {
            try
            {


                cox.BeginTransaction();

                cox.CrearComando(
                    "IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'Temp_asientos') BEGIN " +
                                            "CREATE TABLE [dbo].[Temp_asientos](" +
                                            "[ASI_NUMERO] [varchar](25) NOT NULL, " +
                                            "[ASI_DESCRIPCION] [varchar](500) NOT NULL, " +
                                            "[TASI_CODIGO] [varchar](10) NOT NULL, " +
                                            "[ASI_FECHA] [datetime] NOT NULL, " +
                                            "[ASI_FECHAMODIF] [datetime] NOT NULL, " +
                                            "[USU_CODIGO] [int] NOT NULL, " +
                                            "[ASI_NUMEROREF] [varchar](50) NOT NULL, " +
                                            "[TCP_CODIGO] [varchar](25) NOT NULL, " +
                                            "[TCP_FECIPAG] [datetime] NOT NULL, " +
                                            "[ASI_NUMERODEP] [varchar](20) NOT NULL, " +
                                            "[ASI_TIPO] [varchar](15) NOT NULL, " +
                                            "[ASI_MONEDA] [char](1) NOT NULL, " +
                                            "[ASI_MONTODL] [decimal](20, 2) NOT NULL, " +
                                            "[ASI_MONTOHL] [decimal](20, 2) NOT NULL, " +
                                            "[ASI_SALDOL] [decimal](20, 2) NOT NULL, " +
                                            "[ASI_TASA] [decimal](20, 4) NOT NULL, " +
                                            "[ASI_TIPTASA] [char](1) NOT NULL, " +
                                            "[ASI_MONTODE] [decimal](20, 2) NOT NULL, " +
                                            "[ASI_MONTOHE] [decimal](20, 2) NOT NULL, " +
                                            "[ASI_SALDOE] [decimal](20, 2) NOT NULL, " +
                                            "[ASI_PROCESO] [varchar](10) NOT NULL, " +
                                            "[ASI_D151] [int] NOT NULL, " +
                                         "CONSTRAINT [PK__con_asientos__4830B4000] PRIMARY KEY CLUSTERED " +
                                        "( " +
                                        "	[ASI_NUMERO] ASC " +
                                        ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                                        ") ON [PRIMARY]" +
                                        "END " +
                  "ELSE BEGIN  " +
                                "DELETE FROM [dbo].[Temp_asientos];  " +
				                "END  " +
                    "IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'Temp_asidetalle') BEGIN " +
                            "CREATE TABLE [dbo].[Temp_asidetalle](" +
                            "[ASI_NUMERO] [varchar](25) NOT NULL, " +
                            "[DET_POSICION] [int] NOT NULL, " +
                            "[CAT_CODIGO] [varchar](50) NOT NULL, " +
                            "[CCOS_CODIGO] [varchar](50) NOT NULL, " +
                            "[DET_NOTA] [varchar](200) NOT NULL, " +
                            "[TDOC_CODIGO] [varchar](10) NOT NULL, " +
                            "[DET_NUMDOC] [varchar](50) NOT NULL, " +
                            "[DET_CODTERCERO] [varchar](50) NOT NULL, " +
                            "[DET_TIPTERCERO] [int] NOT NULL, " +
                            "[VEN_CODIGO] [int] NOT NULL, " +
                            "[COB_CODIGO] [int] NOT NULL, " +
                            "[DET_FECHA] [datetime] NOT NULL, " +
                            "[DET_FECHAVENCE] [datetime] NOT NULL, " +
                            "[DET_NUMEROREF] [varchar](50) NOT NULL, " +
                            "[DET_CONSECREF] [varchar](50) NOT NULL, " +
                            "[DET_CONCILIADO] [int] NOT NULL, " +
                            "[DET_MontoDL] [decimal](20, 2) NOT NULL, " +
                            "[DET_MontoHL] [decimal](20, 2) NOT NULL," +
                            "[DET_MontoCL] [decimal](20, 2) NOT NULL," +
                            "[DET_MontoQL] [decimal](20, 2) NOT NULL," +
                            "[DET_TASA] [decimal](20, 4) NOT NULL," +
                            "[DET_MontoDE] [decimal](20, 2) NOT NULL," +
                            "[DET_MontoHE] [decimal](20, 2) NOT NULL," +
                            "[DET_MontoCE] [decimal](20, 2) NOT NULL," +
                            "[DET_MontoQE] [decimal](20, 2) NOT NULL," +
                         "CONSTRAINT [PK__con_asidetalle__5D60DB100] PRIMARY KEY CLUSTERED " +
                        "(" +
                        "	[ASI_NUMERO] ASC," +
                        "	[DET_POSICION] ASC" +
                        ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" +
                        ") ON [PRIMARY]" +
                        "END " +
                "ELSE BEGIN  " +
				        "DELETE FROM [dbo].[Temp_asidetalle];  " +
				        "END  "
                        );

                cox.TransacBD_BT();
                cox.Commit();

                return true;
            }
            catch (Exception) {

                cox.Rollback();
                return false;
                throw;
            }

        }

        /// <summary>
        /// Crea la tabla temp de encabezado de asientos
        /// </summary>
        /// <param name="t"></param>
        /// <param name="cox"></param>
        /// <returns></returns>
        public bool InsertTempEnc(DataTable t, BaseDatos cox)
        {
            string sqlCommand = "";            
            sqlCommand = "INSERT INTO Temp_asientos (ASI_NUMERO, ASI_DESCRIPCION, TASI_CODIGO, ASI_FECHA, ASI_FECHAMODIF, USU_CODIGO, ASI_NUMEROREF, TCP_CODIGO, TCP_FECIPAG, ASI_NUMERODEP, ASI_TIPO, ASI_MONEDA, ASI_MONTODL, ASI_MONTOHL, ASI_SALDOL, ASI_TASA, ASI_TIPTASA, ASI_MONTODE, ASI_MONTOHE, ASI_SALDOE, ASI_PROCESO, ASI_D151) ";

            try
            {

                if (t.Rows.Count > 0)
                {
                    string p = t.Rows[0][0].ToString();
                    sqlCommand +=
                        "VALUES ('TEMP190001-0001','" + //ASIENTO
                        t.Rows[0][12].ToString() + "','" + //DESCRIPCION
                        this.tdoc + "','" + //TIPODOC
                        (t.Rows[0][1].ToString().Split('-')[2].ToString() + "-" + t.Rows[0][1].ToString().Split('-')[1].ToString() + "-" + t.Rows[0][1].ToString().Split('-')[0].ToString()) + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + "','" + //FECHA
                        (t.Rows[0][1].ToString().Split('-')[2].ToString() + "-" + t.Rows[0][1].ToString().Split('-')[1].ToString() + "-" + t.Rows[0][1].ToString().Split('-')[0].ToString()) + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + "','" + //FECHA VENCIMIENTO
                        "999','', '', '', '', '','" + t.Rows[0][11].ToString() + "', 0.00, 0.00, 0.00," +
                        Convert.ToDecimal(t.Rows[0][9].ToString()) + ",'" + t.Rows[0][10].ToString() + "',0.00, 0.00, 0.00, '700',"+v_asiD151+")";
                }

                cox.CrearComando(sqlCommand);
                cox.TransacBD_BT();
                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
                
            }

        }

        /// <summary>
        /// Crea la tabla temp de detalle de asientos
        /// </summary>
        /// <param name="t"></param>
        /// <param name="cox"></param>
        /// <returns></returns>
        public bool InsertTempDet(DataTable t, BaseDatos cox)
        {
            string sqlCommand = "";
            sqlCommand = "INSERT INTO Temp_asidetalle (ASI_NUMERO, DET_POSICION, CAT_CODIGO, CCOS_CODIGO, DET_NOTA, TDOC_CODIGO, DET_NUMDOC, DET_CODTERCERO, DET_TIPTERCERO, VEN_CODIGO, COB_CODIGO, DET_FECHA, DET_FECHAVENCE, DET_NUMEROREF, DET_CONSECREF, DET_CONCILIADO, DET_MontoDL, DET_MontoHL, DET_MontoCL, DET_MontoQL, DET_TASA, DET_MontoDE, DET_MontoHE, DET_MontoCE, DET_MontoQE) ";

            try
            {

                for (int i = 0; i < t.Rows.Count; i++)
                {

                    sqlCommand +=
                        " SELECT 'TEMP190001-0001'," +   // ASIENTO
                         t.Rows[i][0].ToString() + ",'" + //POSICION
                         t.Rows[i][2].ToString() + "','" + //CAT_CODIGO
                         t.Rows[i][3].ToString() + "','" + //CCOS_CODIGO
                         t.Rows[i][6].ToString() + "','" + //DET_NOTA
                         this.tdoc + "','" + //TDOC_CODIGO
                         t.Rows[i][4].ToString() + "','" + //DET_NUMDOC
                         t.Rows[i][5].ToString() + "'," + //DET_CODTERCERO
                         0 + "," + //DET_TIPTERCERO
                         0 + "," + //VEN_CODIGO
                         0 + ",'" + //COB_CODIGO
                         (t.Rows[0][1].ToString().Split('-')[2].ToString() + "-" + t.Rows[0][1].ToString().Split('-')[1].ToString() + "-" + t.Rows[0][1].ToString().Split('-')[0].ToString()) + "','" + //DET_FECHA
                         (t.Rows[0][1].ToString().Split('-')[2].ToString() + "-" + t.Rows[0][1].ToString().Split('-')[1].ToString() + "-" + t.Rows[0][1].ToString().Split('-')[0].ToString()) + "'," + //DET_FECHAVENCE
                         "'','" + //DET_NUMEROREF
                         "'," + //DET_CONSECREF
                         0 + "," + //DET_CONCILIADO
                         Convert.ToDecimal(t.Rows[i][7].ToString()) + "," + //DET_MontoDL
                         Convert.ToDecimal(t.Rows[i][8].ToString()) + "," + //DET_MontoHL
                         0 + "," + //DET_MontoCL
                         0 + "," + //DET_MontoQL
                         Convert.ToDecimal(t.Rows[i][9].ToString()) + "," + //DET_TASA
                         Convert.ToDecimal(t.Rows[i][7].ToString()) + "," + //DET_MontoDE
                         Convert.ToDecimal(t.Rows[i][8].ToString()) + "," + //DET_MontoHE
                         0 + "," + //DET_MontoCE
                         0; //DET_MontoQE
                    
                    if(i < t.Rows.Count-1){
                        sqlCommand += " UNION ALL";
                    }


                    if (i == 0)
                    {
                        this.fecha = (t.Rows[i][1].ToString().Split('-')[2].ToString() + "-" + t.Rows[i][1].ToString().Split('-')[1].ToString() + "-" + t.Rows[i][1].ToString().Split('-')[0].ToString());
                    }

                }

                cox.CrearComando(sqlCommand);
                cox.TransacBD_BT();

                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        /// <summary>
        /// Actualiza algunos datos de la temporal antes de insertar en la tabla real
        /// </summary>
        /// <param name="cox"></param>
        /// <returns></returns>
        public bool UpdateTemp(BaseDatos cox)
        {
            string sqlCommand = "";

            try
            {

                sqlCommand = "update dbo.Temp_asidetalle SET DET_MONTODE= DET_MONTODL/ DET_TASA, DET_MONTOHE= DET_MONTOHL/ DET_TASA; "+
                             "update dbo.Temp_asidetalle SET DET_TIPTERCERO = 0 WHERE DET_CODTERCERO =''; "+                                                          
                                "UPDATE t1 " +
                                "    SET t1.det_tiptercero = t2.CAT_TIPTERCERO " +
                                "    FROM dbo.Temp_asidetalle AS t1 " +
                                "INNER JOIN (	select cat_codigo,CAT_TIPTERCERO " +
                                "    from con_catalogo " +
                                "    where CAT_CODIGO in ( " +
                                "    select cat_codigo from dbo.Temp_asidetalle ) " +
                                "    and cat_tiptercero <> 3 ) AS t2 " +
                                "ON t1.CAT_CODIGO = t2.CAT_CODIGO; " +                             
                             " declare @salDebeL DECIMAL(20,2) " 
                            + " declare @salDebeE DECIMAL(20,2) "
                            + " select  "
                            + " @salDebeL =SUM(det_montodl),  "
                            + " @salDebeE= SUM(det_montode)   "
                            + " from  dbo.Temp_asidetalle     "
                            + " update dbo.Temp_asientos set  "
                            + " asi_montodl =@salDebeL,       "
                            + " asi_montohl =@salDebeL,       "
                            + " asi_montode =@salDebeE,       " 
                            + " asi_montohe =@salDebeE        "
                            + " from  dbo.Temp_asidetalle     ";


                cox.CrearComando(sqlCommand);
                cox.TransacBD_BT();

                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }


        }


        /// <summary>
        /// REaliza el select de las tabla temp preparada e inserta en la tabla de asientos
        /// </summary>
        /// <param name="cox">Objeto conexion</param>
        /// <returns></returns>
        public bool InsertAsiento(BaseDatos cox)
        {
            string sqlCommand = "";

            try
            {

                sqlCommand = 
                            "DECLARE @varAsiNumero varchar(25) " +
                            "SELECT @varAsiNumero = [dbo].[FUNC_UPASIENTOS] (tasi_codigo,asi_fecha) from dbo.Temp_asientos " +
                            "insert into con_asientos  (ASI_NUMERO, ASI_DESCRIPCION, TASI_CODIGO, ASI_FECHA, ASI_FECHAMODIF, USU_CODIGO, ASI_NUMEROREF, TCP_CODIGO, TCP_FECIPAG, " + 
                            "ASI_NUMERODEP, ASI_TIPO, ASI_MONEDA, ASI_MONTODL, ASI_MONTOHL, ASI_SALDOL, ASI_TASA, ASI_TIPTASA, ASI_MONTODE, ASI_MONTOHE,  " +
                            "ASI_SALDOE, ASI_PROCESO, ASI_D151) " +
                            "SELECT @varAsiNumero, ASI_DESCRIPCION, TASI_CODIGO, ASI_FECHA, ASI_FECHAMODIF, USU_CODIGO, ASI_NUMEROREF, TCP_CODIGO, TCP_FECIPAG,  " +
                            "ASI_NUMERODEP, ASI_TIPO, ASI_MONEDA, ASI_MONTODL, ASI_MONTOHL, ASI_SALDOL, ASI_TASA, ASI_TIPTASA, ASI_MONTODE, ASI_MONTOHE, ASI_SALDOE, " +
                            "ASI_PROCESO, ASI_D151 " +
                            "from dbo.Temp_asientos; " +
                            "insert into con_asidetalle (ASI_NUMERO, DET_POSICION, CAT_CODIGO, CCOS_CODIGO, DET_NOTA, TDOC_CODIGO, DET_NUMDOC, DET_CODTERCERO, " +
                            "DET_TIPTERCERO, VEN_CODIGO, COB_CODIGO, DET_FECHA, DET_FECHAVENCE, DET_NUMEROREF, DET_CONSECREF, DET_CONCILIADO, DET_MontoDL, " + 
                            "DET_MontoHL, DET_MontoCL, DET_MontoQL, DET_TASA, DET_MontoDE, DET_MontoHE, DET_MontoCE, DET_MontoQE) " +
                            "SELECT @varAsiNumero, DET_POSICION, CAT_CODIGO, CCOS_CODIGO, DET_NOTA, TDOC_CODIGO, DET_NUMDOC, DET_CODTERCERO, DET_TIPTERCERO,  " +
                            "VEN_CODIGO, COB_CODIGO, DET_FECHA, DET_FECHAVENCE, DET_NUMEROREF, DET_CONSECREF, DET_CONCILIADO, DET_MontoDL, DET_MontoHL,  " +
                            "DET_MontoCL, DET_MontoQL, DET_TASA, DET_MontoDE, DET_MontoHE, DET_MontoCE, DET_MontoQE " +
                            "from dbo.Temp_asidetalle; " +
                            "update dbo.con_asidetalle SET DET_NUMEROREF= ASI_NUMERO +'+' +CONVERT(VARCHAR(10),DET_POSICION)+'+'+DET_NUMDOC WHERE ASI_NUMERO =@varAsiNumero AND DET_CODTERCERO <>'';";



                cox.CrearComando(sqlCommand);
                cox.TransacBD_BT();


                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }


        }

        public bool? verificaTemp(ref string mensaje, BaseDatos cox)
        {
            string msj = "";
            bool? proc = false;

            try{

                cox.execProcedure(ref msj, ref proc);
            }
            catch (Exception) { throw; }
           

            mensaje= msj;

            return proc;
            
        }

        public bool InsertTable(DataTable t)
        {
            if (Temp(t) == true) { return true; }
            else
            {MessageBox.Show("Error al insertar el asiento contable."); return false;}
        }      

        public DataTable GetConfiguracion()
        {
            DataTable v_dtConf = new DataTable("Config");
            try
            {
                this.conexion.Conectar();
                this.conexion.CrearComando("SELECT '000' AS TASI_CODIGO,'-- Selecccionar --' AS TASI_DESCRIPCION UNION ALL SELECT TASI_CODIGO, TASI_DESCRIPCION FROM CON_TASIENTOS");

                v_dtConf = this.conexion.CargarBd();

            }
            catch (Exception){throw;}

            this.conexion.Desconectar();

            return v_dtConf;

        }

       
    }
}
