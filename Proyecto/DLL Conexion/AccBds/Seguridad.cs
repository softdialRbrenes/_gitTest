using System;
using System.Collections.Generic;
using System.Text;

namespace AccBds
{
    /// <summary>
    /// Maneja lo referente a seguridad, encriptar / desencriptar
    /// </summary>
    /// 
    public class Seguridad
    {
        /// <summary>
        /// Funcion para encriptar antes de guardar passwords a la base
        /// </summary>
        /// <param name="encrp">texto a encriptar</param>
        /// <param name="op">0 desencripta, 1 encripta</param>
        /// <returns></returns>
        public string FuncEncripta(string encrp, int op)
        {
            string claveEncripta = "", encrypt = "", passEncripta = "Antonio", letra;
            int cantCaracteres = encrp.Length, cantCaracPass = passEncripta.Length;
            int claveEncriptada = 0;
            int j = 0;
            for (int i = 0; i < cantCaracteres; i++)
            {
                if (j < cantCaracPass)
                {
                    claveEncripta = passEncripta.Substring(j, 1);
                    claveEncriptada = toAscii(claveEncripta);
                    j++;
                }
                else j = 0;
                letra = encrp.Substring(i, 1);

                encrypt += (op == 1) ? toChar(toAscii(letra) + claveEncriptada)
                    : toChar(toAscii(letra) - claveEncriptada);
            }
            return encrypt;
        }
        /// <summary>
        /// devuelve el numero ascii de una letra
        /// </summary>
        /// <param name="letra">char en formato de string</param>
        /// <returns></returns>
        private int toAscii(string letra)
        {
            try { return Convert.ToInt32(System.Text.Encoding.GetEncoding(1252).GetBytes(new char[] { (char)Convert.ToChar(letra) })[0]); }
            catch (Exception) { throw; }
        }
        /// <summary>
        /// devuelve el char en formato string de un ascii
        /// </summary>
        /// <param name="code">el codigo ascii</param>
        /// <returns></returns>
        private string toChar(int code)
        {
            try { return System.Text.Encoding.GetEncoding(1252).GetString(new byte[] { (byte)code }); }
            catch (Exception) { throw; }
        }
    }
}