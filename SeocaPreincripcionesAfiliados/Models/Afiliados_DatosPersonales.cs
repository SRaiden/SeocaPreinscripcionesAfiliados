//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeocaPreincripcionesAfiliados.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Afiliados_DatosPersonales
    {
        public int Codigo { get; set; }
        public string ApellidoNombre { get; set; }
        public string CUIL { get; set; }
        public Nullable<int> Tipo_Doc { get; set; }
        public string Numero_Doc { get; set; }
        public Nullable<int> Delegacion { get; set; }
        public Nullable<int> Calificacion_Profesional { get; set; }
        public Nullable<System.DateTime> Fecha_Solicitud { get; set; }
        public Nullable<int> Estado_Civil { get; set; }
        public Nullable<System.DateTime> Fecha_Nac { get; set; }
        public string Calle { get; set; }
        public string Numero_Calle { get; set; }
        public string Piso { get; set; }
        public string Dto { get; set; }
        public Nullable<int> Telefono { get; set; }
        public Nullable<int> Localidad { get; set; }
        public string Provincia { get; set; }
        public Nullable<int> Sexo { get; set; }
        public Nullable<int> Nacionalidad { get; set; }
        public Nullable<bool> Ingresado { get; set; }
        public Nullable<int> NroAfiliado { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public byte[] FotoFrenteDni { get; set; }
        public byte[] FotoDorsoDni { get; set; }
        public byte[] FotoReciboSueldo { get; set; }
        public Nullable<bool> Art100 { get; set; }
        public Nullable<bool> Turismo { get; set; }
        public Nullable<bool> Sepelio { get; set; }
        public string CodigoTemporal { get; set; }
        public Nullable<bool> Confirmado { get; set; }
        public string Estado { get; set; }
        public string ComentarioEstado { get; set; }
        public Nullable<int> Usuario { get; set; }
        public string FileNameFrente { get; set; }
        public byte[] DataFrenteExtension { get; set; }
        public string FileNameDorso { get; set; }
        public byte[] DataDorsoExtension { get; set; }
        public string FileNameSueldo { get; set; }
        public byte[] DataSueldoExtension { get; set; }
    }
}
