using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Sys_UpAsientos_Entity;

namespace Sys_UpAsientos
{
    public partial class Up : Form
    {
        private cEmpresas empresa;

        public Up()
        {
            InitializeComponent();           
        }

        public Up(string p_empresa)
        {
            InitializeComponent();
            cXML xml = new cXML();
            empresa = xml.GetEmpresa(p_empresa);
            this.nombreEmpresa.Text = empresa.v_Nombre;
            GetConfiguracion();

            BuscaArchivo.Enabled = false;
            Cargar.Enabled = false;
            
        }

        private void BuscaArchivo_Click(object sender, EventArgs e)
        {
            string []name = {"Asiento"};
            cbConfig.Enabled = false;
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string connectionString = String.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", openFileDialog1.FileName);                
                string query = String.Format("select * from [{0}$]", name);                
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);                
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cargar_Click(object sender, System.EventArgs e)
        {                        
            DataTable t = GetTable();

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                if (dataGridView1.Rows[i].Cells[0].Value.ToString() != "")
                {
                    t.Rows.Add(
                        dataGridView1.Rows[i].Cells[0].Value.ToString(),
                          Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value.ToString()).Day + "-" + Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value.ToString()).Month + "-" + Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value.ToString()).Year,
                          dataGridView1.Rows[i].Cells[2].Value.ToString(),
                          dataGridView1.Rows[i].Cells[3].Value.ToString(),
                          dataGridView1.Rows[i].Cells[4].Value.ToString(),
                          dataGridView1.Rows[i].Cells[5].Value.ToString(),
                          dataGridView1.Rows[i].Cells[6].Value.ToString(),
                          dataGridView1.Rows[i].Cells[7].Value.ToString(),
                          dataGridView1.Rows[i].Cells[8].Value.ToString(),
                          dataGridView1.Rows[i].Cells[9].Value.ToString(),
                          dataGridView1.Rows[i].Cells[10].Value.ToString(),
                          dataGridView1.Rows[i].Cells[11].Value.ToString(),
                          dataGridView1.Rows[i].Cells[12].Value.ToString());
                }

            }

            cSQL sql = new cSQL(empresa, cbConfig.SelectedValue.ToString(),chkImpuesto.Checked);

            try
            {
                if (sql.InsertTable(t) == true) { MessageBox.Show("Carga de asiento contable exitosa."); }

                else { MessageBox.Show("Error al insertar el asiento contable."); }
            }
            catch(Exception ex ) {
                MessageBox.Show(ex.ToString()); 

            }

                this.Close();

        }

        private DataTable GetTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("linea", typeof(string));
            table.Columns.Add("fecha", typeof(string));
            table.Columns.Add("cuenta", typeof(string));
            table.Columns.Add("centrocosto", typeof(string));
            table.Columns.Add("numdoc", typeof(string));
            table.Columns.Add("tercero", typeof(string));
            table.Columns.Add("nota", typeof(string));
            table.Columns.Add("debe", typeof(string));
            table.Columns.Add("haber", typeof(string));
            table.Columns.Add("tasa", typeof(string));
            table.Columns.Add("tiptasa", typeof(string));
            table.Columns.Add("moneda", typeof(string));
            table.Columns.Add("descripcion", typeof(string));

            return table;
        }

        private void GetConfiguracion()
        {
            cSQL sql = new cSQL(empresa, false);
            DataTable t = new DataTable();

            try
            {
                t = sql.GetConfiguracion();
                this.cbConfig.DataSource = t;
                this.cbConfig.ValueMember = "TASI_CODIGO";
                this.cbConfig.DisplayMember = "TASI_DESCRIPCION";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Por favor intente ingresando de nuevo, si el problema continua comuníquese con el administrador del sistema!!");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cbConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscaArchivo.Enabled = true;
            Cargar.Enabled = true;
        }


    }
}
