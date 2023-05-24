using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SeocaPreincripcionesAfiliados.Models.ViewModels
{
    public class DatosAfiliados
    {
        [Display(Name = "Codigo")]
        public int Codigo { get; set; }

        [Display(Name = "ApellidoNombre")]
        public string ApellidoNombre { get; set; }

        [Display(Name = "CUIL")]
        public string CUIL { get; set; }

        [Display(Name = "Numero_Doc")]
        public string Numero_Doc { get; set; }

        [Display(Name = "Fecha_Solicitud")]
        public DateTime Fecha_Solicitud { get; set; }

        [Display(Name = "FS")]
        public string FS { get; set; }

        [Display(Name = "NroAfiliado")]
        public int NroAfiliado { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }


    }
}