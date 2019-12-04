
namespace Sys_UpAsientos_Entity
{
    public class cEmpresas
    {
        private string v_codigo;
        private string v_nombre;
        private string v_tipo;
        private string v_instancia;
        private string v_ipCont;
        private string v_ipProd;
        private string v_port;
        private string v_login;
        private string v_pass;

        public string v_Codigo { get { return v_codigo; } set { v_codigo = value; } }
        public string v_Nombre { get { return v_nombre; } set { v_nombre = value; } }
        public string v_Tipo { get { return v_tipo; } set { v_tipo = value; } }
        public string v_Instancia { get { return v_instancia; } set { v_instancia = value; } }
        public string v_IpCont { get { return v_ipCont; } set { v_ipCont = value; } }
        public string v_IpProd { get { return v_ipProd; } set { v_ipProd = value; } }
        public string v_Login { get { return v_login; } set { v_login = value; } }
        public string v_Pass { get { return v_pass; } set { v_pass = value; } }

        public cEmpresas(string v_codigo, string v_nombre, string v_tipo, string v_instancia,
         string v_ipCont, string v_ipProd, string v_port, string v_login, string v_pass)
        {
            this.v_codigo = v_codigo;
            this.v_nombre = v_nombre;
            this.v_tipo = v_tipo;
            this.v_instancia = v_instancia;
            this.v_ipCont = v_ipCont;
            this.v_ipProd = v_ipProd;
            this.v_port = v_port;
            this.v_login = v_login;
            this.v_pass = v_pass;
        }

        public cEmpresas() { }
    }

}
