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
    
    public partial class Empresas
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public string Cuit { get; set; }
        public string DomicilioReal { get; set; }
        public string NroReal { get; set; }
        public string PisoReal { get; set; }
        public string DtoReal { get; set; }
        public Nullable<int> LocalidadReal { get; set; }
        public string TelefonoReal { get; set; }
        public string DomicilioLegal { get; set; }
        public string NroLegal { get; set; }
        public string PisoLegal { get; set; }
        public string DtoLegal { get; set; }
        public string LocalidadLegal { get; set; }
        public string TelefonoLegal { get; set; }
        public Nullable<int> Actividad { get; set; }
        public string Email { get; set; }
        public string PaginaWeb { get; set; }
        public Nullable<bool> Ingresada { get; set; }
        public Nullable<int> NroEmpresa { get; set; }
        public Nullable<int> Usuario { get; set; }
        public string Estado { get; set; }
        public string ComentarioEstado { get; set; }
        public Nullable<bool> Confirmada { get; set; }
        public byte[] FotoHabilitacionMunicipal { get; set; }
        public byte[] FotoComprobanteAFIP { get; set; }
        public byte[] FotoContratoSocial { get; set; }
        public byte[] FotoNotaEscrita { get; set; }
        public byte[] FotoReciboSueldo { get; set; }
        public string extensionHabilitacionMunicipal { get; set; }
        public string extensionComprobanteAFIP { get; set; }
        public string extensionContratoSocial { get; set; }
        public string extensionNotaEscrita { get; set; }
        public string extensionReciboSueldo { get; set; }
        public Nullable<bool> ActualizacionSecretaria { get; set; }
        public Nullable<int> CPLegal { get; set; }
    }
}
