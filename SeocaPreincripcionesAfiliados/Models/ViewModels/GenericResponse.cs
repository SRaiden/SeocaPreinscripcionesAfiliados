using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeocaPreincripcionesAfiliados.Models.ViewModels
{
    public class GenericResponse<TObject>
    {
        public bool Estado { get; set; }
        public TObject Objeto { get; set; }
        public string Mensaje { get; set; }
    }
}