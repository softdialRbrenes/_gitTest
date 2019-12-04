using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sys_UpAsientos_Entity;

namespace Sys_UpAsientos
{
    public partial class Form1 : Form
    {
        private Menu menu;
        private cXML xml;
        private List<cEmpresas> lista;

        public Form1(Menu p_menu)
        {
            InitializeComponent();
            xml = new cXML();
            menu = p_menu;

            //Cargar el DDl con las empresas del xml
            lista = xml.GetEmpresas();
            comboBox1.DataSource = lista;
            comboBox1.DisplayMember = "v_Nombre";
            comboBox1.ValueMember = "v_Codigo";
        }

        private void Conectar_Click(object sender, EventArgs e)
        {
            string empresa = this.comboBox1.SelectedValue.ToString();

            if (empresa=="-1")
            {
                MessageBox.Show("Por favor selecciones una empresa!");
            }
            else
            {
                menu.SetEmpresa(empresa);
                menu.ControlsEnable(true);
                this.Close();
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

