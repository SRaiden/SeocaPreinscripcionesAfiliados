using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SeocaPreincripcionesAfiliados.Models.ViewModels
{
    public class DatoAfiliadoDetallado
    {
        [Display(Name = "Codigo")]
        public int Codigo { get; set; }

        [Display(Name = "ApellidoNombre")]
        public string ApellidoNombre { get; set; }

        [Display(Name = "CUIL")]
        public string CUIL { get; set; }

        [Display(Name = "Delegacion")]
        public string Delegacion { get; set; }

        [Display(Name = "Numero_Doc")]
        public string Numero_Doc { get; set; }

        [Display(Name = "Calificacion_Profesional")]
        public string Calificacion_Profesional { get; set; }

        [Display(Name = "Estado_Civil")]
        public string Estado_Civil { get; set; }

        [Display(Name = "Fecha_Solicitud")]
        public DateTime Fecha_Solicitud { get; set; }

        [Display(Name = "FS")]
        public string FS { get; set; }

        [Display(Name = "Fecha_Nac")]
        public DateTime Fecha_Nac { get; set; }

        [Display(Name = "Calle")]
        public string Calle { get; set; }

        [Display(Name = "Numero_Calle")]
        public string Numero_Calle { get; set; }

        [Display(Name = "Telefono")]
        public int Telefono { get; set; }

        [Display(Name = "Localidad_Afiliado")]
        public string Localidad_Afiliado { get; set; }

        [Display(Name = "NroAfiliado")]
        public int NroAfiliado { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Display(Name = "Nombre_Empresa")]
        public string Nombre_Empresa { get; set; }

        [Display(Name = "Cuit_Empresa")]
        public string Cuit_Empresa { get; set; }

        [Display(Name = "Calle_Empresa")]
        public string Calle_Empresa { get; set; }

        [Display(Name = "Numero_Empresa")]
        public int Numero_Empresa { get; set; }

        [Display(Name = "Localidad_Empresa")]
        public string Localidad_Empresa { get; set; }

        [Display(Name = "Provincia")]
        public string Provincia { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Display(Name = "Nacionalidad")]
        public string Nacionalidad { get; set; }

        [Display(Name = "Telefono_Empresa")]
        public string Telefono_Empresa { get; set; }

        [Display(Name = "Fecha_Ingreso_Empresa")]
        public DateTime Fecha_Ingreso_Empresa { get; set; }

    }
}