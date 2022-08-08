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
            using (Models.SeocaPreinscripcionesEntities db = new Models.SeocaPreinscripcionesEntities())
            {
                List<Empresas_Actividades> Empresas_Actividades = db.Empresas_Actividades.ToList<Empresas_Actividades>();
                ViewData["Empresas_Actividades"] = Empresas_Actividades;

                List<General_Localidades> Localidades = db.General_Localidades.ToList<General_Localidades>();
                ViewData["Localidades"] = Localidades;
            }


            return View();
        }


        [HttpPost]
        public ActionResult Afiliados(string matrizDatosAfiliado, string matrizDocumento = null, string matrizEmpresa = null, string matrizFamiliares = null)
        {
            using (Models.SeocaPreinscripcionesEntities db = new Models.SeocaPreinscripcionesEntities())
            {
                // EMPRESAS
                try
                {
                    JArray jsonPreservar = JArray.Parse(matrizEmpresa);
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
                        var DatoPersonales = db.afi.Where(d => d.Cuit == Cuit).First();
                        if (Inscripcion != null)
                        {
                            ViewBag.error = "Ya hay una empresa inscripta con este mismo Cuit de Empresa";
                            return View("Empresa");
                        }
                    }
                    catch (Exception)
                    {

                    }


                    // GUARDAR EN LA TABLA DE EMPRESAS
                    var emp = new Empresas
                    {
                        RazonSocial = RazonSocial,
                        NombreFantasia = NombreFantasia,
                        Cuit = Cuit,
                        DomicilioReal = DomicilioReal,
                        LocalidadReal = Int32.Parse(LocalidadReal),
                        TelefonoReal = TelefonoReal,
                        Actividad = Int32.Parse(Actividad),
                        Email = Email,
                        PaginaWeb = PaginaWeb,
                        DomicilioLegal = DomicilioLegal,
                        LocalidadLegal = Int32.Parse(LocalidadLegal),
                        TelefonoLegal = TelefonoLegal,
                    };

                    db.Empresas.Add(emp);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    ViewBag.error = "Error" + ex.Message;
                    return View("Empresa");
                }


                // ANTECEDENTES
                try
                {
                    JArray jsonPreservar = JArray.Parse(matrizAntecedente);
                    string SucesoraAntecedente = "", NumeroEmpresaAntecedente = "", FechaTransferenciaAntecedente = "", CalleAntecedente = "", PisoAntecedente = "", LocalidadAntecedente = "", CPAntecedente = "", ProvinciaAntecedente = "", TelefonoAntecedente = "";
                    foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                    {
                        foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                        {
                            string propiedad = jsonOPropiedades.Name;
                            if (propiedad.Equals("SucesoraAntecedente")) SucesoraAntecedente = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("NumeroEmpresaAntecedente")) NumeroEmpresaAntecedente = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("FechaTransferenciaAntecedente")) FechaTransferenciaAntecedente = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("CalleAntecedente")) CalleAntecedente = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("PisoAntecedente")) PisoAntecedente = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("LocalidadAntecedente")) LocalidadAntecedente = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("CPAntecedente")) CPAntecedente = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("ProvinciaAntecedente")) ProvinciaAntecedente = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("TelefonoAntecedente")) TelefonoAntecedente = jsonOPropiedades.Value.ToString();
                        }

                        // GUARDAR EN LA TABLA DE EMPRESAS ANTECEDENTES
                        var emp = new Empresas_Antecedentes
                        {
                            Sucesora = SucesoraAntecedente,
                            NumeroEmpresa = Int32.Parse(NumeroEmpresaAntecedente),
                            FechaTransferencia = DateTime.Parse(FechaTransferenciaAntecedente),
                            Calle = CalleAntecedente,
                            Piso = PisoAntecedente,
                            Localidad = LocalidadAntecedente,
                            CodigoPostal = CPAntecedente,
                            Provincia = ProvinciaAntecedente,
                            Telefono = TelefonoAntecedente,
                        };

                        db.Empresas_Antecedentes.Add(emp);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.error = "Error" + ex.Message;
                    return View("Empresa");
                }


                // CONTADORES
                try
                {
                    JArray jsonPreservar = JArray.Parse(matrizContador);
                    string NombreEstudioContador = "", DireccionContador = "", TelefonoContador = "", EmailContador = "";
                    foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                    {
                        foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                        {
                            string propiedad = jsonOPropiedades.Name;
                            if (propiedad.Equals("NombreEstudioContador")) NombreEstudioContador = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("DireccionContador")) DireccionContador = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("TelefonoContador")) TelefonoContador = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("EmailContador")) EmailContador = jsonOPropiedades.Value.ToString();
                        }

                        // GUARDAR EN LA TABLA DE EMPRESAS ANTECEDENTES
                        var emp = new Empresas_Contadores
                        {
                            NomreEstudio = NombreEstudioContador,
                            Direccion = DireccionContador,
                            Telefono = TelefonoContador,
                            Email = EmailContador,
                        };

                        db.Empresas_Contadores.Add(emp);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.error = "Error" + ex.Message;
                    return View("Empresa");
                }


                // EMPLEADOS
                try
                {
                    JArray jsonPreservar = JArray.Parse(matrizEmpleado);
                    string ApellidoNombreEmpleado = "", CuilEmpleado = "", FechaIngresoEmpleado = "", CategoriaEmpleado = "", TotRemuneracionEmpleado = "", Aporte2ArtEmpleado = "", Aporte1SindEmpleado = "", Aporte1SepEmpleado = "", JornadaEmpleado = "";
                    foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                    {
                        foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                        {
                            string propiedad = jsonOPropiedades.Name;
                            if (propiedad.Equals("ApellidoNombreEmpleado")) ApellidoNombreEmpleado = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("CuilEmpleado")) CuilEmpleado = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("FechaIngresoEmpleado")) FechaIngresoEmpleado = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("CategoriaEmpleado")) CategoriaEmpleado = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("TotRemuneracionEmpleado")) TotRemuneracionEmpleado = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Aporte2ArtEmpleado")) Aporte2ArtEmpleado = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Aporte1SindEmpleado")) Aporte1SindEmpleado = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Aporte1SepEmpleado")) Aporte1SepEmpleado = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("JornadaEmpleado")) JornadaEmpleado = jsonOPropiedades.Value.ToString();
                        }

                        if (JornadaEmpleado == "MEDIA") JornadaEmpleado = "1/2 JORNADA";
                        else JornadaEmpleado = "JORNADA COMPLETA";

                        // GUARDAR EN LA TABLA DE EMPRESAS ANTECEDENTES
                        var emp = new Empresas_Empleados
                        {
                            ApellidoNombre = ApellidoNombreEmpleado,
                            Cuil = CuilEmpleado,
                            FechaIngreso = DateTime.Parse(FechaIngresoEmpleado),
                            Categoria = CategoriaEmpleado,
                            TotalRemuneracion = decimal.Parse(TotRemuneracionEmpleado),
                            Art_100 = decimal.Parse(Aporte2ArtEmpleado),
                            Sind = decimal.Parse(Aporte1SindEmpleado),
                            Sepelio = decimal.Parse(Aporte1SepEmpleado),
                            Jornada = JornadaEmpleado,
                        };

                        db.Empresas_Empleados.Add(emp);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.error = "Error" + ex.Message;
                    return View("Empresa");
                }


                // TITULARES
                try
                {
                    JArray jsonPreservar = JArray.Parse(matrizTitular);
                    string ApellidoNombreTitular = "", DomicilioParticularTitular = "", DocumentoTitular = "", CargoEmpresaTitular = "";
                    foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                    {
                        foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                        {
                            string propiedad = jsonOPropiedades.Name;
                            if (propiedad.Equals("ApellidoNombreTitular")) ApellidoNombreTitular = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("DomicilioParticularTitular")) DomicilioParticularTitular = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("DocumentoTitular")) DocumentoTitular = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("CargoEmpresaTitular")) CargoEmpresaTitular = jsonOPropiedades.Value.ToString();
                        }

                        // GUARDAR EN LA TABLA DE EMPRESAS ANTECEDENTES
                        var emp = new Empresas_Titulares
                        {
                            ApellidoNombre = ApellidoNombreTitular,
                            DomicilioParticular = DomicilioParticularTitular,
                            Documento = DocumentoTitular,
                            Cargo = CargoEmpresaTitular,
                        };

                        db.Empresas_Titulares.Add(emp);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.error = "Error" + ex.Message;
                    return View("Empresa");
                }
            }

            ViewBag.error = "Ya se ha preinscripto";
            return View();
        }


    }
}