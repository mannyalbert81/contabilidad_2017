using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Php.Clases
{
    public class ParametrosRpt
    {
        //se agrega los parametros q se utilizaran en los reportes

        public string fecha_desde { get; set; }
        public string Fecha_hasta { get; set; }
        public string id_entidades { get; set; }
        public string tipo_comprobantes { get; set; }
        public string numero_comprobantes { get; set; }
        public string referencia_doc_comprobantes { get; set; }
        public string reporte { get; set; }
        public int id_usuarios { get; set; }
        public int total_registros { get; set; }
        public int anio_balance { get; set; }
        public int mes_balance { get; set; }
        public string id_ccomprobantes { get; set; }
        public string codigo_plan_cuentas { get; set; }
        public string nombre_plan_cuentas { get; set; }
        public int nivel_plan_cuentas { get; set; }
        public string t_plan_cuentas { get; set; }
        public string n_plan_cuentas { get; set; }
        public string nombre_usuarios { get; set; }
        public string cedula_usuarios { get; set; }
        public string correo_usuarios { get; set; }
        public int id_rol { get; set; }
        public int id_estado { get; set; }
        





    }
}