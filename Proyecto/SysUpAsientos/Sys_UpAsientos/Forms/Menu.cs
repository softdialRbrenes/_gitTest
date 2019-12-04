using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sys_UpAsientos
{
    public partial class Menu : Form
    {
        public string empresa;

        public Menu()
        {
            InitializeComponent();
            empresa = "";
            this.toolStripStatusLabel1.Text = "Conectado a Empresa " + this.empresa;

            ControlsEnable(false);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void cargaDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Up up = new Up(empresa);
            up.MdiParent = this;
            up.Show();
        }

        public void SetEmpresa(string emp)
        {
            this.toolStripStatusLabel1.Text = "Conectado a Empresa " + emp;
            this.empresa = emp;
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void conectarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1(this);
            f.MdiParent = this;
            f.Show();
        }

        public void ControlsEnable(bool enable)
        {
            if (!enable)
            {
                this.opcionesToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.opcionesToolStripMenuItem.Enabled = true;
            }
            
        }
    }
}
