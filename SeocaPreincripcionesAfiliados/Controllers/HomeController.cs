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

                List<General_Documentos> TipoDoc = db.General_Documentos.Where(d=>d.Id != 0).ToList<General_Documentos>();
                ViewData["TipoDoc"] = TipoDoc;

                List<General_Delegacion> Delegacion = db.General_Delegacion.Where(d => d.Id != 0).ToList<General_Delegacion>();
                ViewData["Delegacion"] = Delegacion;

                List<General_Calificacion> CalifProf = db.General_Calificacion.Where(d => d.Id != 0).ToList<General_Calificacion>();
                ViewData["CalifProf"] = CalifProf;

                List<General_Estado_Civil> EstadoCivil = db.General_Estado_Civil.Where(d => d.Id != 0).ToList<General_Estado_Civil>();
                ViewData["EstadoCivil"] = EstadoCivil;

                List<General_Provincias> Provincia = db.General_Provincias.Where(d => d.Id != 0).ToList<General_Provincias>();
                ViewData["Provincia"] = Provincia;

                List<General_Parentesco> Parentesco = db.General_Parentesco.Where(d => d.Id != 0).ToList<General_Parentesco>();
                ViewData["Parentesco"] = Parentesco;

                List<General_Sexo> Sexo = db.General_Sexo.ToList<General_Sexo>();
                ViewData["Sexo"] = Sexo;

                List<General_Nacionalidades> Nacionalidad = db.General_Nacionalidades.ToList<General_Nacionalidades>();
                ViewData["Nacionalidad"] = Nacionalidad;

                ViewBag.diaHoy = DateTime.Now.ToString("yyyy-MM-dd");

            }
            return View();
        }


        [HttpPost]
        public ActionResult Afiliados(string matrizDatosAfiliado, string matrizEmpresa = null, string matrizFamiliares = null)
        {
            using (Models.SeocaPreinscripcionesEntities2 db = new Models.SeocaPreinscripcionesEntities2())
            {
                // EMPRESAS
                try
                {
                    JArray jsonPreservar = JArray.Parse(matrizDatosAfiliado);
                    string Apellido = "", Nombre = "", Cuil = "", TipoDoc = "", NumDoc = "", Delegacion = "", CalifProf = "", EstadoCivil = "", FechaNac = "", Calle = "", NumeroCalle = "", Piso = "", Dto = "", Telefono = "", Localidad = "", Provincia = "", SexoAfiliadoDocumento = "", Nacionalidad = "";
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
                            if (propiedad.Equals("EstadoCivil")) EstadoCivil = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("FechaNac")) FechaNac = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Calle")) Calle = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("NumeroCalle")) NumeroCalle = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Piso")) Piso = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Dto")) Dto = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Telefono")) Telefono = jsonOPropiedades.Value.ToString();

                            if (propiedad.Equals("Localidad")) Localidad = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Provincia")) Provincia = jsonOPropiedades.Value.ToString();

                            if (propiedad.Equals("SexoAfiliadoDocumento")) SexoAfiliadoDocumento = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Nacionalidad")) Nacionalidad = jsonOPropiedades.Value.ToString();
                        }
                    }

                    // Validar si se preinscribio anteriormente
                    try
                    {
                        var Inscripcion = db.Afiliados_DatosPersonales.Where(d => d.CUIL == Cuil).First();
                        if (Inscripcion != null)
                        {
                            return Json(new { success = true, responseText = "Ya hay un afiliado preinscripto con mismo CUIT anteriormente." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception)
                    {

                    }

                    string hoy = DateTime.Now.ToString("yyyy/MM/dd");

                    // GUARDAR EN LA TABLA DE EMPRESAS
                    var emp = new Afiliados_DatosPersonales
                    {
                        ApellidoNombre = Apellido + " " + Nombre,
                        CUIL = Cuil,
                        Tipo_Doc = Int32.Parse(TipoDoc),
                        Numero_Doc = NumDoc,
                        Delegacion = Int32.Parse(Delegacion),
                        Calificacion_Profesional = Int32.Parse(CalifProf),
                        Fecha_Solicitud = DateTime.Parse(hoy),
                        Estado_Civil = Int32.Parse(EstadoCivil),
                        Fecha_Nac = DateTime.Parse(FechaNac),
                        Calle = Calle,
                        Numero_Calle = NumeroCalle,
                        Piso = Piso,
                        Dto = Dto,
                        Telefono = Int32.Parse(Telefono),
                        Localidad = Int32.Parse(Localidad),
                        Provincia = Provincia,
                        Sexo = Int32.Parse(SexoAfiliadoDocumento),
                        Nacionalidad = Int32.Parse(Nacionalidad),
                        Ingresado = false
                    };

                    db.Afiliados_DatosPersonales.Add(emp);
                    db.SaveChanges();

                }
                catch (Exception)
                {
                    return Json(new { success = true, responseText = "Error al Preincribir Datos del Afiliado." }, JsonRequestBehavior.AllowGet);
                }


                var ultimoId = db.Afiliados_DatosPersonales.OrderByDescending(d => d.Codigo).First().Codigo;

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
                                Piso = PisoAfiliadoEmpresa,
                                Dto = DtoAfiliadoEmpresa,
                                Localidad = Int32.Parse(LocalidadAfiliadoEmpresa),
                                Telefono = TelefonoAfiliadoEmpresa
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

                    string Parentesco = "", ApellidoNombreAfiliadoFamiliar = "", CertEstudiosAfiliadoFamiliar = "", TipoDocAfiliadoFamiliar = "", NumDocAfiliadoFamiliar = "", SexoAfiliadoFamiliar = "", FechaNacAfiliadoFamiliar = "";
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
                            }

                            if (string.IsNullOrEmpty(CertEstudiosAfiliadoFamiliar))
                            {
                                // GUARDAR EN LA TABLA DE EMPRESAS ANTECEDENTES
                                var emp = new Afiliados_Familiares
                                    {
                                        Id_Afiliado = ultimoId,
                                        Parentesco = Parentesco,
                                        Apellido_Nombre = ApellidoNombreAfiliadoFamiliar,
                                        Tipo_Doc = TipoDocAfiliadoFamiliar,
                                        Num_Doc = NumDocAfiliadoFamiliar,
                                        Sexo = SexoAfiliadoFamiliar,
                                        Fecha_Nac = DateTime.Parse(FechaNacAfiliadoFamiliar)
                                    };

                                db.Afiliados_Familiares.Add(emp);
                                db.SaveChanges();
                            }
                            else
                            {
                                // GUARDAR EN LA TABLA DE EMPRESAS ANTECEDENTES
                                var emp = new Afiliados_Familiares
                                {
                                    Id_Afiliado = ultimoId,
                                    Parentesco = Parentesco,
                                    Apellido_Nombre = ApellidoNombreAfiliadoFamiliar,
                                    Cert_Estudios = DateTime.Parse(CertEstudiosAfiliadoFamiliar),
                                    Tipo_Doc = TipoDocAfiliadoFamiliar,
                                    Num_Doc = NumDocAfiliadoFamiliar,
                                    Sexo = SexoAfiliadoFamiliar,
                                    Fecha_Nac = DateTime.Parse(FechaNacAfiliadoFamiliar)
                                };

                                db.Afiliados_Familiares.Add(emp);
                                db.SaveChanges();
                            }


                        }

                    }

                }
                catch (Exception ex)
                {
                    return Json(new { success = true, responseText = "Error al Preinscribir Antecedente." }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, responseText = "Preinscripcion Exitosa." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}