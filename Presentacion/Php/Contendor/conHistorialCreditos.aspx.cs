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
    public partial class conHistorialCreditos : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            ReportDocument crystalReport = new ReportDocument();
            var dsHistorialCreditos = new Datas.dsHistorialCreditos();
            DataTable dt_Reporte1 = new DataTable();

            parametros.operacion = Request.QueryString["operacion"];
            parametros.cuenta = Request.QueryString["cuenta"];
            parametros.s = Request.QueryString["s"];
            parametros.aa_ddd = Request.QueryString["aa_ddd"];
            parametros.fecha_concede = Request.QueryString["fecha_concede"];
            parametros.fecha_vencimiento = Request.QueryString["fecha_vencimiento"];

           


            string columnas =  "historial_prestamos_sudamericano.id_historial_prestamos_sudamericano," +
            "historial_prestamos_sudamericano.nombres_clientes, " +
            "historial_prestamos_sudamericano.lg, " +
            "historial_prestamos_sudamericano.gr, " +
            "historial_prestamos_sudamericano.operacion, " +
            "historial_prestamos_sudamericano.fecha_concede, " +
            "historial_prestamos_sudamericano.fecha_vencimiento, " +
            "historial_prestamos_sudamericano.valor_prestado, " +
            "historial_prestamos_sudamericano.saldo_capital, " +
            "historial_prestamos_sudamericano.capital_pagado, " +
            "historial_prestamos_sudamericano.dv, " +
            "historial_prestamos_sudamericano.saldo_vencido, " +
            "historial_prestamos_sudamericano.interes_cobrado, " +
            "historial_prestamos_sudamericano.mora_cobrada, " +
            "historial_prestamos_sudamericano.cuenta, " +
            "historial_prestamos_sudamericano.aa_ddd, " +
            "historial_prestamos_sudamericano.s ";


            string tablas = "public.historial_prestamos_sudamericano";


            string where = "historial_prestamos_sudamericano.id_historial_prestamos_sudamericano > 0";
            

            String where_to = "";


           

            if (!String.IsNullOrEmpty(parametros.operacion) && Convert.ToString(parametros.operacion) != "")
            {

                where_to += " AND historial_prestamos_sudamericano.operacion = '" + parametros.operacion + "' ";
            }

            if (!String.IsNullOrEmpty(parametros.cuenta) && Convert.ToString(parametros.cuenta) != "")
            {

                where_to += " AND historial_prestamos_sudamericano.cuenta = '" + parametros.cuenta + "' ";
            }

            if (!String.IsNullOrEmpty(parametros.s) && Convert.ToString(parametros.s) != "")
            {

                where_to += " AND historial_prestamos_sudamericano.s ='" + parametros.s + "'";
            }

            if (Convert.ToString(parametros.aa_ddd) != "")
            {

                where_to += " AND historial_prestamos_sudamericano.aa_ddd = '" + parametros.aa_ddd + "'";
            }

            if (!String.IsNullOrEmpty(parametros.fecha_concede) && !String.IsNullOrEmpty(parametros.fecha_vencimiento))
            {

                where_to += " AND  DATE(historial_prestamos_sudamericano.fecha_concede) BETWEEN '" + parametros.fecha_concede + "' AND '" + parametros.fecha_vencimiento + "'";
            }

            


            where = where + where_to;

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);


            dsHistorialCreditos.Tables.Add(dt_Reporte1);


            string cadena = Server.MapPath("~/Php/Reporte/crHistorialCreditos.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsHistorialCreditos.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;

        }
    }
}