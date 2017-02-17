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


            parametros.tipo_comprobantes = Request.QueryString["id_tipo_comprobantes"];
            parametros.fecha_desde = Request.QueryString["fecha_desde"];
            parametros.Fecha_hasta = Request.QueryString["fecha_hasta"];
            parametros.id_entidades = Request.QueryString["id_entidades"];
            parametros.numero_comprobantes = Request.QueryString["numero_ccomprobantes"];
            parametros.referencia_doc_comprobantes = Request.QueryString["referencia_doc_ccomprobantes"];

            try
            {
                parametros.id_usuarios = Convert.ToInt32(Request.QueryString["id_usuarios"]);
            }
            catch (Exception) { parametros.id_usuarios = 0; }

            ReportDocument crystalReport = new ReportDocument();
            var dsComprobantes = new Datas.dsComprobantes();
            DataTable dt_Reporte1 = new DataTable();


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
                              "usuarios.nombre_usuarios";

            string tablas = "public.fc_productos, public.fc_grupo_productos, public.entidades, public.fc_foto_productos, public.fc_catalogos, public.fc_unidades_medida, public.usuarios";

            string where = "fc_grupo_productos.id_grupo_productos = fc_productos.id_grupo_productos AND entidades.id_entidades = fc_productos.id_entidades AND fc_foto_productos.id_foto_productos = fc_productos.id_foto_productos AND fc_catalogos.id_catalogos = fc_productos.id_catalogos AND fc_unidades_medida.id_unidades_medida = fc_productos.id_unidades_medida AND usuarios.id_entidades = entidades.id_entidades";

            //para cambiar el where

            String where_to = "";
            //
            if (parametros.id_usuarios > 0)
            {

                where_to += " AND usuarios.id_usuarios=" + parametros.id_usuarios + "";
            }

            if (!String.IsNullOrEmpty(parametros.tipo_comprobantes) && Convert.ToInt32(parametros.tipo_comprobantes) != 0)
            {

                where_to += " AND tipo_comprobantes.id_tipo_comprobantes='" + parametros.tipo_comprobantes + "'";
            }

            if (!String.IsNullOrEmpty(parametros.fecha_desde) && !String.IsNullOrEmpty(parametros.Fecha_hasta))
            {

                where_to += " AND  ccomprobantes.fecha_ccomprobantes BETWEEN '" + parametros.fecha_desde + "' AND '" + parametros.Fecha_hasta + "'";
            }

            if (!String.IsNullOrEmpty(parametros.id_entidades))
            {

                where_to += " AND entidades.id_entidades = " + parametros.id_entidades;
            }

            if (!String.IsNullOrEmpty(parametros.numero_comprobantes))
            {

                where_to += " AND ccomprobantes.numero_ccomprobantes='" + parametros.numero_comprobantes + "' ";
            }

            if (!String.IsNullOrEmpty(parametros.referencia_doc_comprobantes))
            {

                where_to += " AND ccomprobantes.referencia_doc_ccomprobantes ='" + parametros.referencia_doc_comprobantes + "'";
            }

            where = where + where_to;

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);


            dsComprobantes.Tables.Add(dt_Reporte1);


            string cadena = Server.MapPath("~/Php/Reporte/crComprobantes.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsComprobantes.Tables[1]);

            //paso de parametros

            //en caso de estar vacios las fechas
            if (String.IsNullOrEmpty(parametros.fecha_desde) || String.IsNullOrEmpty(parametros.Fecha_hasta))
            {
                if (dt_Reporte1.Rows.Count > 0)
                {
                    parametros.fecha_desde = dt_Reporte1.Rows[0]["fecha_ccomprobantes"].ToString();
                    parametros.Fecha_hasta = dt_Reporte1.Rows[dt_Reporte1.Rows.Count - 1]["fecha_ccomprobantes"].ToString();
                }
                else
                {
                    parametros.fecha_desde = DateTime.Today.ToString("dd-MM-yyyy");
                    parametros.Fecha_hasta = DateTime.Today.ToString("dd-MM-yyyy");
                }

            }
            parametros.total_registros = 0;
            if (dt_Reporte1.Rows.Count > 0) { parametros.total_registros = dt_Reporte1.Rows.Count; }

            crystalReport.SetParameterValue("total_registros", parametros.total_registros);
            crystalReport.SetParameterValue("fecha_desde", parametros.fecha_desde);
            crystalReport.SetParameterValue("fecha_hasta", parametros.Fecha_hasta);

            CrystalReportViewer1.ReportSource = crystalReport;

        }
    }
}