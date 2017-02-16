using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

using System.IO;
using System.Drawing;
using Presentacion.Php.Clases;

namespace Presentacion.Php.Contendor
{
    public partial class conReporteUsuarios : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
            
        {
            ReportDocument crystalReport = new ReportDocument();
            var dsUsuarios = new Datas.dsUsuarios();
            DataTable dt_Reporte1 = new DataTable();

            parametros.id_entidades = Request.QueryString["id_entidades"];
            parametros.nombre_usuarios = Request.QueryString["nombre_usuarios"];
            parametros.cedula_usuarios = Request.QueryString["cedula_usuarios"];
            parametros.correo_usuarios = Request.QueryString["correo_usuarios"];

            

            try
            {
                parametros.id_rol = Convert.ToInt32(Request.QueryString["id_rol"]);
            }
            catch (Exception) { parametros.id_rol = 0; }

            try
            {
                parametros.id_estado = Convert.ToInt32(Request.QueryString["id_estado"]);
            }
            catch (Exception) { parametros.id_estado = 0; }

            
            string columnas = " usuarios.id_usuarios, " +
                                " usuarios.nombre_usuarios, " +
                                "usuarios.telefono_usuarios, usuarios.celular_usuarios, usuarios.correo_usuarios, " +
                                "rol.nombre_rol, estado.nombre_estado, usuarios.creado, " +
                                "usuarios.cedula_usuarios, " +
                                  "usuarios.imagen_usuarios, " +
                                  "entidades.id_entidades, " +
                                  "entidades.nombre_entidades, " +
                                  "entidades.telefono_entidades, " +
                                  "entidades.direccion_entidades, " +
                                  "entidades.ciudad_entidades, " +
                                  "entidades.ruc_entidades, " +
                                  "entidades.logo_entidades ";
            

            string tablas = "public.usuarios,  public.rol,  public.estado,  public.entidades";

            
            string where = "rol.id_rol = usuarios.id_rol AND estado.id_estado = usuarios.id_estado AND entidades.id_entidades = usuarios.id_entidades";

           
            //para cambiar el where

            String where_to = "";

            //
          

            if (!String.IsNullOrEmpty(parametros.id_entidades) && Convert.ToInt32(parametros.id_entidades) != 0)
            {

                where_to += " AND entidades.id_entidades = " + parametros.id_entidades;
            }

            if (!String.IsNullOrEmpty(parametros.nombre_usuarios))
            {

                where_to += " AND usuarios.nombre_usuarios = '" + parametros.nombre_usuarios + "' ";
            }

            if (!String.IsNullOrEmpty(parametros.cedula_usuarios))
            {

                where_to += " AND usuarios.cedula_usuarios ='" + parametros.cedula_usuarios + "'";
            }

            if (!String.IsNullOrEmpty(parametros.correo_usuarios))
            {

                where_to += " AND usuarios.correo_usuarios = '" + parametros.correo_usuarios + "'";
            }

            if (parametros.id_rol > 0)
            {
                where_to += " AND rol.id_rol=" + parametros.id_rol + "";
            }

            if (parametros.id_estado > 0)
            {
                where_to += " AND estado.id_estado =" + parametros.id_estado + "";
            }


           

            where = where + where_to;

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);


            dsUsuarios.Tables.Add(dt_Reporte1);


            string cadena = Server.MapPath("~/Php/Reporte/crReporteUsuarios.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsUsuarios.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;

        }
    }
}