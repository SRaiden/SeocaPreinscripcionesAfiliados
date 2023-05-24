using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SeocaPreincripcionesAfiliados.Models.ViewModels
{
    public class DatosFamiliarAfiliado
    {
        [Display(Name = "Id_Afiliado")]
        public int Id_Afiliado { get; set; }

        [Display(Name = "Parentesco")]
        public string Parentesco { get; set; }

        [Display(Name = "Apellido_Nombre")]
        public string Apellido_Nombre { get; set; }

        [Display(Name = "Cert_Estudios")]
        public DateTime Cert_Estudios { get; set; }

        [Display(Name = "CE")]
        public string CE { get; set; }

        [Display(Name = "Tipo_Doc")]
        public string Tipo_Doc { get; set; }

        [Display(Name = "Num_Doc")]
        public string Num_Doc { get; set; }

        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Display(Name = "Fecha_Nac")]
        public DateTime Fecha_Nac { get; set; }

        [Display(Name = "FN")]
        public string FN { get; set; }
    }
}