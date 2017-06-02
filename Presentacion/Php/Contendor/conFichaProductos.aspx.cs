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
    public partial class conFichaProductos : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {


            ReportDocument crystalReport = new ReportDocument();
            var dsFichaProductos = new Datas.dsFichaProductos();
            DataTable dt_Reporte1 = new DataTable();

            parametros.id_entidades = Request.QueryString["id_entidades"];
            parametros.id_productos = Request.QueryString["id_productos"];

         

            string columnas = " fc_productos.id_productos,"+
                                  "fc_grupo_productos.nombre_grupo_productos,"+ 
                                  "fc_grupo_productos.descripcion_grupo_productos,"+ 
                                  "entidades.ruc_entidades,"+ 
                                 "entidades.nombre_entidades,"+ 
                                  "entidades.telefono_entidades,"+ 
                                  "entidades.direccion_entidades,"+ 
                                  "entidades.ciudad_entidades,"+ 
                                  "entidades.logo_entidades,"+ 
                                  "fc_productos.nombre_productos,"+ 
                                  "fc_productos.descripcion_productos,"+ 
                                  "fc_foto_productos.archivo_foto_productos,"+ 
                                  "fc_foto_productos.descripcion_foto_productos,"+ 
                                  "fc_productos.precio_uno_productos,"+ 
                                  "fc_productos.utilidad_uno_productos,"+ 
                                  "fc_productos.precio_dos_productos,"+
                                  "fc_productos.utilidad_dos,"+ 
                                  "fc_productos.precio_tres_productos,"+ 
                                  "fc_productos.utilidad_tres,"+ 
                                  "fc_productos.observaciones_productos,"+ 
                                  "fc_productos.iva_productos,"+ 
                                  "fc_unidades_medida.nombre_unidades_medida,"+ 
                                  "fc_productos.codigo_productos,"+ 
                                  "usuarios.nombre_usuarios";

            string tablas = " public.fc_foto_productos, public.fc_productos, public.entidades, public.usuarios, public.fc_unidades_medida, public.fc_grupo_productos";

            string where = " fc_foto_productos.id_foto_productos = fc_productos.id_foto_productos AND entidades.id_entidades = fc_productos.id_entidades AND entidades.id_entidades = usuarios.id_entidades AND usuarios.id_usuarios = fc_productos.id_usuarios AND fc_unidades_medida.id_unidades_medida = fc_productos.id_unidades_medida AND fc_grupo_productos.id_grupo_productos = fc_productos.id_grupo_productos";

            //para cambiar el where

            String where_to = "";
            //
            if (!String.IsNullOrEmpty(parametros.id_entidades))
            {

                where_to += " AND fc_productos.id_entidades = " + parametros.id_entidades;
            }

            if (!String.IsNullOrEmpty(parametros.id_productos))
            {

                where_to += " AND fc_productos.id_productos = " + parametros.id_productos;
            }
         

            where = where + where_to;

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);


            dsFichaProductos.Tables.Add(dt_Reporte1);


            string cadena = Server.MapPath("~/Php/Reporte/crFichaProductos.rpt");
            Label1.Text = parametros.id_entidades + parametros.id_productos;
            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsFichaProductos.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;

        }
    }
}