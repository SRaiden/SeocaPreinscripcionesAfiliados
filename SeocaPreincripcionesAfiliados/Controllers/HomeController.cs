using Newtonsoft.Json.Linq;
using SeocaPreincripcionesAfiliados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeocaPreincripcionesAfiliados.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Afiliados()
        {
            using (Models.SeocaPreinscripcionesEntities2 db = new Models.SeocaPreinscripcionesEntities2())
            {
                List<General_Localidades> Localidades = db.General_Localidades.ToList<General_Localidades>();
                ViewData["Localidades"] = Localidades;

                List<General_Documentos> TipoDoc = db.General_Documentos.ToList<General_Documentos>();
                ViewData["TipoDoc"] = TipoDoc;

                List<General_Delegacion> Delegacion = db.General_Delegacion.ToList<General_Delegacion>();
                ViewData["Delegacion"] = Delegacion;

                List<General_Calificacion> CalifProf = db.General_Calificacion.ToList<General_Calificacion>();
                ViewData["CalifProf"] = CalifProf;

                List<General_Estado_Civil> EstadoCivil = db.General_Estado_Civil.ToList<General_Estado_Civil>();
                ViewData["EstadoCivil"] = EstadoCivil;

                List<General_Provincias> Provincia = db.General_Provincias.ToList<General_Provincias>();
                ViewData["Provincia"] = Provincia;

                List<General_Parentesco> Parentesco = db.General_Parentesco.ToList<General_Parentesco>();
                ViewData["Parentesco"] = Parentesco;


            }
            return View();
        }


        [HttpPost]
        public ActionResult Afiliados(string matrizDatosAfiliado, string matrizDocumento = null, string matrizEmpresa = null, string matrizFamiliares = null)
        {
            using (Models.SeocaPreinscripcionesEntities2 db = new Models.SeocaPreinscripcionesEntities2())
            {
                // EMPRESAS
                try
                {
                    JArray jsonPreservar = JArray.Parse(matrizDatosAfiliado);
                    string Apellido = "", Nombre = "", Cuil = "", TipoDoc = "", NumDoc = "", Delegacion = "", CalifProf = "", Fecha_Afiliacion = "", EstadoCivil = "", FechaNac = "", Calle = "", NumeroCalle = "", Piso = "", Dto = "", Telefono = "", CP = "", Localidad = "", Provincia = "";
                    foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                    {
                        foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                        {
                            string propiedad = jsonOPropiedades.Name;
                            if (propiedad.Equals("Apellido")) Apellido = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Nombre")) Nombre = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Cuil")) Cuil = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("TipoDoc")) TipoDoc = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("NumDoc")) NumDoc = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Delegacion")) Delegacion = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("CalifProf")) CalifProf = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Fecha_Afiliacion")) Fecha_Afiliacion = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("EstadoCivil")) EstadoCivil = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("FechaNac")) FechaNac = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Calle")) Calle = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("NumeroCalle")) NumeroCalle = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Piso")) Piso = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Dto")) Dto = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Telefono")) Telefono = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("CP")) CP = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Localidad")) Localidad = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Provincia")) Provincia = jsonOPropiedades.Value.ToString();
                        }
                    }

                    // Validar si se preinscribio anteriormente
                    try
                    {
                        var Inscripcion = db.Afiliados_DatosPersonales.Where(d => d.CUIL == Cuil).First();
                        if (Inscripcion != null)
                        {
                            return Json(new { success = true, responseText = "Ya hay un afiliado preinscripto anteriormente." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception)
                    {

                    }

                    // GUARDAR EN LA TABLA DE EMPRESAS
                    var emp = new Afiliados_DatosPersonales
                    {
                        Apellido = Apellido,
                        Nombre = Nombre,
                        CUIL = Cuil,
                        Tipo_Doc = TipoDoc,
                        Numero_Doc = NumDoc,
                        Delegacion = Delegacion,
                        Calificacion_Profesional = CalifProf,
                        Fecha_Afiliacion = DateTime.Parse(Fecha_Afiliacion),
                        Estado_Civil = EstadoCivil,
                        Fecha_Nac = DateTime.Parse(FechaNac),
                        Calle = Calle,
                        Numero_Calle = NumeroCalle,
                        Piso = Piso,
                        Dto = Dto,
                        Telefono = Int32.Parse(Telefono),
                        CP = Int32.Parse(CP),
                        Localidad = Localidad,
                        Provincia = Provincia
                    };

                    db.Afiliados_DatosPersonales.Add(emp);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return Json(new { success = true, responseText = "Error al Preincribir Datos del Afiliado." }, JsonRequestBehavior.AllowGet);
                }

                var ultimoId = db.Afiliados_DatosPersonales.OrderByDescending(d => d.Codigo).First().Codigo;

                // DOCUMENTO
                try
                {
                    JArray jsonPreservar = null;
                    try
                    {
                        jsonPreservar = JArray.Parse(matrizDocumento);
                    }
                    catch (Exception)
                    {

                    }

                    string SexoAfiliadoDocumento = "", Nacionalidad = "";
                    if (jsonPreservar != null)
                    {
                        foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                        {
                            foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                            {
                                string propiedad = jsonOPropiedades.Name;
                                if (propiedad.Equals("SexoAfiliadoDocumento")) SexoAfiliadoDocumento = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("Nacionalidad")) Nacionalidad = jsonOPropiedades.Value.ToString();
                            }

                            // GUARDAR EN LA TABLA DE EMPRESAS ANTECEDENTES
                            var emp = new Afiliados_Documentos
                            {
                                Id_Afiliado = ultimoId,
                                Sexo = SexoAfiliadoDocumento,
                                Nacionalidad = Nacionalidad
                            };

                            db.Afiliados_Documentos.Add(emp);
                            db.SaveChanges();
                        }

                    }

                }
                catch (Exception ex)
                {
                    return Json(new { success = true, responseText = "Error al Preinscribir Documentos." }, JsonRequestBehavior.AllowGet);
                }

                // EMPRESA
                try
                {
                    JArray jsonPreservar = null;
                    try
                    {
                        jsonPreservar = JArray.Parse(matrizEmpresa);
                    }
                    catch (Exception)
                    {

                    }

                    string FechaIngresoAfiliadoEmpresa = "", NombreEmpresaAfiliadoEmpresa = "", CUITEmpresaAfiliadoEmpresa = "", CalleAfiliadoEmpresa = "", NumeroAfiliadoEmpresa = "", LocalAfiliadoEmpresa = "", PisoAfiliadoEmpresa = "", DtoAfiliadoEmpresa = "", CPAfiliadoEmpresa = "", LocalidadAfiliadoEmpresa = "", TelefonoAfiliadoEmpresa = "";
                    if (jsonPreservar != null)
                    {
                        foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                        {
                            foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                            {
                                string propiedad = jsonOPropiedades.Name;
                                if (propiedad.Equals("FechaIngresoAfiliadoEmpresa")) FechaIngresoAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("NombreEmpresaAfiliadoEmpresa")) NombreEmpresaAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("CUITEmpresaAfiliadoEmpresa")) CUITEmpresaAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("CalleAfiliadoEmpresa")) CalleAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("NumeroAfiliadoEmpresa")) NumeroAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("LocalAfiliadoEmpresa")) LocalAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("PisoAfiliadoEmpresa")) PisoAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("DtoAfiliadoEmpresa")) DtoAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("CPAfiliadoEmpresa")) CPAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("LocalidadAfiliadoEmpresa")) LocalidadAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("TelefonoAfiliadoEmpresa")) TelefonoAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                            }

                            // GUARDAR EN LA TABLA DE EMPRESAS ANTECEDENTES
                            var emp = new Afiliados_Empresa
                            {
                                Id_Afiliado = ultimoId,
                                Fecha_Ingreso = DateTime.Parse(FechaIngresoAfiliadoEmpresa),
                                Nombre_Empresa = NombreEmpresaAfiliadoEmpresa,
                                Cuit_Empresa = CUITEmpresaAfiliadoEmpresa,
                                Calle = CalleAfiliadoEmpresa,
                                Numero = Int32.Parse(NumeroAfiliadoEmpresa),
                                Local = LocalAfiliadoEmpresa,
                                Piso = PisoAfiliadoEmpresa,
                                Dto = DtoAfiliadoEmpresa,
                                CP = Int32.Parse(CPAfiliadoEmpresa),
                                Localidad = LocalidadAfiliadoEmpresa,
                                Telefono = Int32.Parse(TelefonoAfiliadoEmpresa) 
                            };

                            db.Afiliados_Empresa.Add(emp);
                            db.SaveChanges();
                        }

                    }

                }
                catch (Exception ex)
                {
                    return Json(new { success = true, responseText = "Error al Preinscribir Datos de la Empresa." }, JsonRequestBehavior.AllowGet);
                }

                // FAMILIARES
                try
                {
                    JArray jsonPreservar = null;
                    try
                    {
                        jsonPreservar = JArray.Parse(matrizFamiliares);
                    }
                    catch (Exception)
                    {

                    }

                    string Parentesco = "", ApellidoNombreAfiliadoFamiliar = "", CertEstudiosAfiliadoFamiliar = "", TipoDocAfiliadoFamiliar = "", NumDocAfiliadoFamiliar = "", SexoAfiliadoFamiliar = "", FechaNacAfiliadoFamiliar = "", VencioAfiliadoFamiliar = "", ActualizacionAfiliadoFamiliar = "";
                    if (jsonPreservar != null)
                    {
                        foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                        {
                            foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                            {
                                string propiedad = jsonOPropiedades.Name;
                                if (propiedad.Equals("Parentesco")) Parentesco = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("ApellidoNombreAfiliadoFamiliar")) ApellidoNombreAfiliadoFamiliar = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("CertEstudiosAfiliadoFamiliar")) CertEstudiosAfiliadoFamiliar = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("TipoDocAfiliadoFamiliar")) TipoDocAfiliadoFamiliar = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("NumDocAfiliadoFamiliar")) NumDocAfiliadoFamiliar = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("SexoAfiliadoFamiliar")) SexoAfiliadoFamiliar = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("FechaNacAfiliadoFamiliar")) FechaNacAfiliadoFamiliar = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("VencioAfiliadoFamiliar")) VencioAfiliadoFamiliar = jsonOPropiedades.Value.ToString(); // !!!
                                if (propiedad.Equals("ActualizacionAfiliadoFamiliar")) ActualizacionAfiliadoFamiliar = jsonOPropiedades.Value.ToString();  // !!!
                            }

                            // GUARDAR EN LA TABLA DE EMPRESAS ANTECEDENTES
                            var emp = new Afiliados_Familiares
                            {
                                Id_Afiliado = ultimoId,
                                Parentesco = Parentesco,
                                Apellido_Nombre = ApellidoNombreAfiliadoFamiliar,
                                Cert_Estudios = DateTime.Parse(CertEstudiosAfiliadoFamiliar),
                                Tipo_Doc = TipoDocAfiliadoFamiliar,
                                Num_Doc =NumDocAfiliadoFamiliar,
                                Sexo = SexoAfiliadoFamiliar,
                                Fecha_Nac = DateTime.Parse(FechaNacAfiliadoFamiliar),
                                Venc = DateTime.Parse(VencioAfiliadoFamiliar),
                                Actualizacion = DateTime.Parse(ActualizacionAfiliadoFamiliar) 
                            };

                            db.Afiliados_Familiares.Add(emp);
                            db.SaveChanges();
                        }

                    }

                }
                catch (Exception ex)
                {
                    return Json(new { success = true, responseText = "Error al Preinscribir Antecedente." }, JsonRequestBehavior.AllowGet);
                }

            }

            ViewBag.error = "Ya se ha preinscripto";
            return View();
        }


    }
}