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
    public partial class conReporteProductos : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {

            ReportDocument crystalReport = new ReportDocument();
            var dsReporteProductos = new Datas.dsReporteProductos();
            DataTable dt_Reporte1 = new DataTable();

            



            parametros.id_entidades = Request.QueryString["id_entidades"];
            parametros.codigo_productos = Request.QueryString["codigo_productos"];
            parametros.nombre_productos = Request.QueryString["nombre_productos"];
          

            try
            {
                parametros.id_grupo_productos = Convert.ToInt32(Request.QueryString["id_grupo_productos"]);
            }
            catch (Exception) { parametros.id_grupo_productos = 0; }
            try
            {
                parametros.id_unidades_medida = Convert.ToInt32(Request.QueryString["id_unidades_medida"]);
            }
            catch (Exception) { parametros.id_unidades_medida = 0; }

            parametros.iva_productos = Request.QueryString["iva_productos"];


            string columnas = "fc_productos.id_productos," +
                              "fc_grupo_productos.nombre_grupo_productos," +
                              "fc_grupo_productos.descripcion_grupo_productos," +
                              "entidades.id_entidades," +
                              "entidades.ruc_entidades," +
                              "entidades.nombre_entidades," +
                              "entidades.telefono_entidades," +
                              "entidades.direccion_entidades," +
                              "entidades.ciudad_entidades," +
                              "entidades.logo_entidades," +
                              "fc_productos.nombre_productos," +
                              "fc_productos.descripcion_productos," +
                              "fc_foto_productos.archivo_foto_productos," +
                              "fc_foto_productos.descripcion_foto_productos," +
                              "fc_catalogos.archivo_catalogos," +
                              "fc_catalogos.descripcion_catalogos," +
                              "fc_productos.precio_uno_productos," +
                              "fc_productos.utilidad_uno_productos," +
                              "fc_productos.precio_dos_productos," +
                              "fc_productos.utilidad_dos," +
                              "fc_productos.precio_tres_productos," +
                              "fc_productos.utilidad_tres," +
                              "fc_productos.observaciones_productos," +
                              "fc_productos.iva_productos," +
                              "fc_unidades_medida.nombre_unidades_medida," +
                              "usuarios.nombre_usuarios,"+
                              "fc_productos.codigo_productos";

            string tablas = "public.fc_productos, public.fc_grupo_productos, public.entidades, public.fc_foto_productos, public.fc_catalogos, public.fc_unidades_medida, public.usuarios";

            string where = "fc_grupo_productos.id_grupo_productos = fc_productos.id_grupo_productos AND entidades.id_entidades = fc_productos.id_entidades AND fc_foto_productos.id_foto_productos = fc_productos.id_foto_productos AND fc_catalogos.id_catalogos = fc_productos.id_catalogos AND fc_unidades_medida.id_unidades_medida = fc_productos.id_unidades_medida AND usuarios.id_entidades = entidades.id_entidades";

            //para cambiar el where

            String where_to = "";

            if (!String.IsNullOrEmpty(parametros.id_entidades))
            {

                where_to += " AND entidades.id_entidades = " + parametros.id_entidades;
            }

            if (!String.IsNullOrEmpty(parametros.codigo_productos))
            {

                where_to += " AND fc_productos.codigo_productos = " + parametros.codigo_productos;
            }
            if (!String.IsNullOrEmpty(parametros.nombre_productos))
            {

                where_to += " AND fc_productos.nombre_productos = " + parametros.nombre_productos;
            }
            if (parametros.id_grupo_productos > 0)
            {

                where_to += " AND fc_grupo_productos.id_grupo_productos=" + parametros.id_grupo_productos + "";
            }
            if (parametros.id_unidades_medida > 0)
            {

                where_to += " AND fc_unidades_medida.id_unidades_medida=" + parametros.id_unidades_medida + "";
            }
            if (!String.IsNullOrEmpty(parametros.iva_productos))
            {

                where_to += " AND fc_productos.iva_productos = " + parametros.iva_productos;
            }


            where = where + where_to;

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);


            dsReporteProductos.Tables.Add(dt_Reporte1);


            string cadena = Server.MapPath("~/Php/Reporte/crReporteProductos.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsReporteProductos.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;

        }
    }
}