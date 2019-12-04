namespace Sys_UpAsientos
{
    partial class Up
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nombreEmpresa = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkImpuesto = new System.Windows.Forms.CheckBox();
            this.lblImpuesto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbConfig = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BuscaArchivo = new System.Windows.Forms.Button();
            this.Cancelar = new System.Windows.Forms.Button();
            this.Cargar = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // nombreEmpresa
            // 
            this.nombreEmpresa.AutoSize = true;
            this.nombreEmpresa.Location = new System.Drawing.Point(38, 31);
            this.nombreEmpresa.Name = "nombreEmpresa";
            this.nombreEmpresa.Size = new System.Drawing.Size(0, 13);
            this.nombreEmpresa.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(606, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Seleccionar Excel a cargar:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkImpuesto);
            this.groupBox1.Controls.Add(this.lblImpuesto);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbConfig);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.BuscaArchivo);
            this.groupBox1.Controls.Add(this.Cancelar);
            this.groupBox1.Controls.Add(this.Cargar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nombreEmpresa);
            this.groupBox1.Location = new System.Drawing.Point(22, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(879, 447);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Carga de Datos";
            // 
            // chkImpuesto
            // 
            this.chkImpuesto.AutoSize = true;
            this.chkImpuesto.Location = new System.Drawing.Point(168, 60);
            this.chkImpuesto.Name = "chkImpuesto";
            this.chkImpuesto.Size = new System.Drawing.Size(15, 14);
            this.chkImpuesto.TabIndex = 11;
            this.chkImpuesto.UseVisualStyleBackColor = true;
            // 
            // lblImpuesto
            // 
            this.lblImpuesto.AutoSize = true;
            this.lblImpuesto.Location = new System.Drawing.Point(49, 60);
            this.lblImpuesto.Name = "lblImpuesto";
            this.lblImpuesto.Size = new System.Drawing.Size(113, 13);
            this.lblImpuesto.TabIndex = 10;
            this.lblImpuesto.Text = "Afecta Impuesto D151";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(233, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Tipo de Asiento:";
            // 
            // cbConfig
            // 
            this.cbConfig.FormattingEnabled = true;
            this.cbConfig.Location = new System.Drawing.Point(323, 30);
            this.cbConfig.Name = "cbConfig";
            this.cbConfig.Size = new System.Drawing.Size(257, 21);
            this.cbConfig.TabIndex = 8;
            this.cbConfig.SelectedIndexChanged += new System.EventHandler(this.cbConfig_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(41, 85);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(800, 299);
            this.dataGridView1.TabIndex = 7;
            // 
            // BuscaArchivo
            // 
            this.BuscaArchivo.Location = new System.Drawing.Point(759, 30);
            this.BuscaArchivo.Name = "BuscaArchivo";
            this.BuscaArchivo.Size = new System.Drawing.Size(82, 23);
            this.BuscaArchivo.TabIndex = 6;
            this.BuscaArchivo.Text = "Examinar";
            this.BuscaArchivo.UseVisualStyleBackColor = true;
            this.BuscaArchivo.Click += new System.EventHandler(this.BuscaArchivo_Click);
            // 
            // Cancelar
            // 
            this.Cancelar.Location = new System.Drawing.Point(766, 408);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(75, 23);
            this.Cancelar.TabIndex = 4;
            this.Cancelar.Text = "Cancelar";
            this.Cancelar.UseVisualStyleBackColor = true;
            this.Cancelar.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // Cargar
            // 
            this.Cargar.Location = new System.Drawing.Point(648, 408);
            this.Cargar.Name = "Cargar";
            this.Cargar.Size = new System.Drawing.Size(95, 23);
            this.Cargar.TabIndex = 3;
            this.Cargar.Text = "Cargar Asiento";
            this.Cargar.UseVisualStyleBackColor = true;
            this.Cargar.Click += new System.EventHandler(this.Cargar_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Up
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 471);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Up";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Up";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label nombreEmpresa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Cancelar;
        private System.Windows.Forms.Button Cargar;
        private System.Windows.Forms.Button BuscaArchivo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox cbConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkImpuesto;
        private System.Windows.Forms.Label lblImpuesto;

    }
}