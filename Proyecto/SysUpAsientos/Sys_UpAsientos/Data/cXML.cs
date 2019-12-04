using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Sys_UpAsientos_Entity;

namespace Sys_UpAsientos
{
    public class cXML
    {
        private String v_PathXMLConfiguracion;
        private XmlDocument v_XmlDocument;

        public cXML() 
        {
            v_PathXMLConfiguracion = System.AppDomain.CurrentDomain.BaseDirectory + "XMLConfiguracion.xml";
            v_XmlDocument = new XmlDocument();
        }

        /// <summary>
        /// Retorna la ip del server de Bases de Datos a conectarse
        /// </summary>
        /// <returns></returns>
        public string getServerIP()
        {
            string ip = "";

            return ip;
        }

        /// <summary>
        /// Retorna una tabla con dos columnas, nombre de empresa y nombre bd
        /// </summary>
        /// <returns></returns>
        public List<cEmpresas> GetEmpresas()
        {
            //DataTable v_DataTableEmpresas;
            //DataRow v_DataRow;
            List<cEmpresas> lista = new List<cEmpresas>();

            try
            { 

                if (File.Exists(this.v_PathXMLConfiguracion))
                {
                     v_XmlDocument.Load(v_PathXMLConfiguracion);

                     foreach (XmlNode node in v_XmlDocument.DocumentElement.ChildNodes)
                     {


                        /* lista.Add(new cEmpresas(
                                node.Attributes["codigo"].InnerText
                                , node.Attributes["nombre"].InnerText
                                , node.Attributes["tipo"].InnerText
                                , node.Attributes["instancia"].InnerText
                                , node.Attributes["ipCont"].InnerText
                                , node.Attributes["ipProd"].InnerText
                                , node.Attributes["port"].InnerText
                                , node.Attributes["login"].InnerText
                                , node.Attributes["pass"].InnerText
                            ));*/

                         lista.Add(new cEmpresas(                             
                                node.Attributes["codigo"].InnerText
                                ,node.Attributes["nombre"].InnerText
                                ,node.Attributes["tipo"].InnerText
                                ,node.Attributes["instancia"].InnerText
                                ,node.Attributes["ipCont"].InnerText
                                ,node.Attributes["ipProd"].InnerText
                                ,node.Attributes["port"].InnerText
                                ,node.Attributes["login"].InnerText
                                ,"SFD2220175"
                            ));

                     }
                }
                else{}
            }
            catch (Exception){throw;}

            return lista;
        }

        public cEmpresas GetEmpresa(string codigo)
        {

            cEmpresas empresa = new cEmpresas(); 

            try
            {
                if (File.Exists(this.v_PathXMLConfiguracion))
                {
                    v_XmlDocument.Load(v_PathXMLConfiguracion);

                    foreach (XmlNode node in v_XmlDocument.DocumentElement.ChildNodes)
                    {

                        if (node.Attributes["codigo"].InnerText == codigo)
                        {
                           /* empresa = new cEmpresas(
                               node.Attributes["codigo"].InnerText
                               , node.Attributes["nombre"].InnerText
                               , node.Attributes["tipo"].InnerText
                               , node.Attributes["instancia"].InnerText
                               , node.Attributes["ipCont"].InnerText
                               , node.Attributes["ipProd"].InnerText
                               , node.Attributes["port"].InnerText
                               , node.Attributes["login"].InnerText
                               , node.Attributes["pass"].InnerText
                           );*/

                            empresa = new cEmpresas(
                               node.Attributes["codigo"].InnerText
                               , node.Attributes["nombre"].InnerText
                               , node.Attributes["tipo"].InnerText
                               , node.Attributes["instancia"].InnerText
                               , node.Attributes["ipCont"].InnerText
                               , node.Attributes["ipProd"].InnerText
                               , node.Attributes["port"].InnerText
                               , node.Attributes["login"].InnerText
                               , node.Attributes["pass"].InnerText
                           );

                            break;
                        }
                    }
                }
                else { }
            }
            catch (Exception) { throw; }

            return empresa;
        }
    }
}
