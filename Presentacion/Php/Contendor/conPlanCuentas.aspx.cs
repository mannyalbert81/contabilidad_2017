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
    public partial class conPlanCuentas : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //sdsdf
        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            ReportDocument crystalReport = new ReportDocument();
            var dsPlanCuentas = new Datas.dsPlanCuentas();
            DataTable dt_Reporte1 = new DataTable();

            parametros.id_entidades = Request.QueryString["id_entidades"];
            parametros.codigo_plan_cuentas = Request.QueryString["codigo_plan_cuentas"];
            parametros.nombre_plan_cuentas = Request.QueryString["nombre_plan_cuentas"];

            try
            {
                parametros.nivel_plan_cuentas = Convert.ToInt32(Request.QueryString["nivel_plan_cuentas"]);
            }
            catch (Exception) { parametros.nivel_plan_cuentas = 0; }

            
            parametros.t_plan_cuentas = Request.QueryString["t_plan_cuentas"];
            parametros.n_plan_cuentas = Request.QueryString["n_plan_cuentas"];

            try
            {
                parametros.id_usuarios = Convert.ToInt32(Request.QueryString["id_usuarios"]);
            }
            catch (Exception) { parametros.id_usuarios = 0; }


            string columnas =     "entidades.id_entidades," +
                                  "entidades.ruc_entidades," +
                                  "entidades.nombre_entidades," +
                                  "entidades.telefono_entidades," +
                                  "entidades.direccion_entidades," +
                                  "entidades.ciudad_entidades," +
                                  "entidades.logo_entidades," +
                                  "plan_cuentas.id_plan_cuentas," +
                                  "plan_cuentas.codigo_plan_cuentas," +
                                  "plan_cuentas.nombre_plan_cuentas," +
                                  "monedas.nombre_monedas," +
                                  "plan_cuentas.n_plan_cuentas," +
                                  "plan_cuentas.t_plan_cuentas," +
                                  "plan_cuentas.nivel_plan_cuentas";


            string tablas = "public.plan_cuentas, public.usuarios, public.entidades, public.monedas";

            string where = "plan_cuentas.id_modenas = monedas.id_monedas AND entidades.id_entidades = usuarios.id_entidades AND entidades.id_entidades = plan_cuentas.id_entidades";

            string order = " plan_cuentas.codigo_plan_cuentas";

            String where_to = "";

            if (parametros.id_usuarios > 0)
            {
                where_to += " AND usuarios.id_usuarios=" + parametros.id_usuarios + "";
            }

            if (!String.IsNullOrEmpty(parametros.id_entidades))
            {

                where_to += " AND plan_cuentas.id_entidades = " + parametros.id_entidades;
            }
            if (!String.IsNullOrEmpty(parametros.codigo_plan_cuentas))
            {
                where_to += " AND plan_cuentas.codigo_plan_cuentas = '" + parametros.codigo_plan_cuentas + "'";
            }
            if (!String.IsNullOrEmpty(parametros.nombre_plan_cuentas))
            {
                where_to += " AND plan_cuentas.nombre_plan_cuentas = '" + parametros.nombre_plan_cuentas + "'";
            }

            if (parametros.nivel_plan_cuentas > 0)
            {
                where_to += " AND plan_cuentas.nivel_plan_cuentas=" + parametros.nivel_plan_cuentas + "";
            }

           
            if (!String.IsNullOrEmpty(parametros.t_plan_cuentas))
            {
                where_to += " AND plan_cuentas.t_plan_cuentas='" + parametros.t_plan_cuentas + "'";
            }
            if (!String.IsNullOrEmpty(parametros.n_plan_cuentas))
            {
                where_to += " AND plan_cuentas.n_plan_cuentas='" + parametros.n_plan_cuentas + "'";
            }


            where = where + where_to;

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where, order);

            //dsCuentas.Cuentas= dt_Reporte;

            dsPlanCuentas.Tables.Add(dt_Reporte1);
            string cadena = Server.MapPath("~/Php/Reporte/crPlanCuentas.rpt");

            Label1.Text = parametros.id_entidades + '-' + parametros.codigo_plan_cuentas + '-' + parametros.nombre_plan_cuentas + '-' + parametros.nivel_plan_cuentas + '-' + parametros.t_plan_cuentas + '-' + parametros.n_plan_cuentas;

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsPlanCuentas.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;
        }
    }
}