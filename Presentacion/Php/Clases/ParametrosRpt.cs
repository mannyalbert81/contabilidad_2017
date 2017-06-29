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
        
        public int id_estado { get; set; }
       public string id_productos { get; set; }
        public string codigo_productos { get; set; }
        public string nombre_productos { get; set; }
        public int id_grupo_productos { get; set; }
        public int id_unidades_medida { get; set; }
        public string iva_productos { get; set; }


        public string operacion { get; set; }
        public string cuenta { get; set; }
        public string s { get; set; }
        public string aa_ddd { get; set; }
        public string fecha_concede { get; set; }
        public string fecha_vencimiento { get; set; }


        //para rol 3


        public int id_abogado { get; set; }
        public string juicio_referido_titulo_credito { get; set; }
        public string numero_titulo_credito { get; set; }
        public string identificacion_clientes { get; set; }
        public int id_provincias { get; set; }
        public int id_estados_procesales_juicios { get; set; }
        
        public int id_rol { get; set; }
        public int id_juicios { get; set; }


        //para rol 5
        public int id_secretario { get; set; }

        //para rol 23

        public int id_ciudad { get; set; }
        
    }
}