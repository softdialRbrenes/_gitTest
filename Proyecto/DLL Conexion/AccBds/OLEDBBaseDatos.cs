using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace AccBds
{
    public class OLEDBBaseDatos
    {
        /***********************************************************************************************
        ******************* CONEXION PARA ARCHIVOS DE ACCESS UTILIZANDO OLEDB **************************
        ***********************************************************************************************/
        private OleDbConnection _OLDBConn;
        private OleDbCommand _OLDBCommand;
        private OleDbTransaction _Transact;
        private string _OLDBCadConex;

        public OleDbCommand OLDBCommand
        {
            get { return _OLDBCommand; }
            set { _OLDBCommand = value; }
        }

        public OLEDBBaseDatos(string BDPathDis)
           
        {
            try
            {
                _OLDBCadConex = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                                @"Data source= " + BDPathDis;
                _OLDBCommand = new OleDbCommand();
            }
            catch (Exception)
            {
                throw;
            }  
        }

        public void Conectar()
        {
            try
            {
                if (_OLDBConn == null)
                {
                    _OLDBConn = new OleDbConnection();
                    _OLDBConn.ConnectionString = _OLDBCadConex;
                }
                _OLDBConn.Open();
            }
            catch (Exception)
            {
                throw;
            }        
        }

        public void Desconectar()
        {
            try {
                if (_OLDBConn.State == ConnectionState.Open)
                {
                    _OLDBConn.Close();
                }
            }
            catch (Exception) { throw; }
        }

        public void BeginTransaction() {
            _Transact = _OLDBConn.BeginTransaction();
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
             try {
                _OLDBCommand.Connection = _OLDBConn;
                _OLDBCommand.CommandType = CommandType.Text;
                _OLDBCommand.CommandText = SQL;
             }
             catch (Exception) { throw; }
        }
       
        public OleDbDataReader CargarBD_DR()
        {
            try { return _OLDBCommand.ExecuteReader(); }
            catch (Exception) { throw; }
        }

        public DataTable CargarBD_DT()
        {
            try
            {
                OleDbDataAdapter OLEDBDataAdapter = new OleDbDataAdapter(_OLDBCommand);
                DataTable datatable = new DataTable();
                OLEDBDataAdapter.Fill(datatable);
                return datatable;
            }
            catch (Exception) { throw; }
        }

        public void TransacBD()
        {
            try
            {
                this._OLDBCommand.Transaction = this._Transact;
                this._OLDBCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
