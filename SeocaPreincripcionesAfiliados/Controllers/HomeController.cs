using Newtonsoft.Json.Linq;
using SeocaPreincripcionesAfiliados.Models;
using SeocaPreincripcionesAfiliados.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using MimeKit;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;

namespace SeocaPreincripcionesAfiliados.Controllers
{
    public class HomeController : Controller
    {
        public static string codigo = "";

        [Route("")]
        [Route("/IniciarSesion")]
        [HttpGet]
        public ActionResult IniciarSesion()
        {
            HttpContext.Session.Remove("SessionUser");
            HttpContext.Session.Clear();

            ViewBag.Session = false;
            return View();
        }

        [HttpPost]
        [Route("/IniciarSesion")]
        public ActionResult IniciarSesion(string user, string pass)
        {
            using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
            {
                try
                {
                    var soyadmin = db.General_Delegacion_Usuarios.FirstOrDefault(d => d.Usuario == user && d.Contraseña == pass);

                    if (soyadmin != null)
                    {
                        Session["SessionUser"] = soyadmin.Id.ToString();

                        if (soyadmin.Delegacion == 0)
                        {
                            return RedirectToAction("ReporteAfiliados", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Afiliados", "Home");
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Datos Invalidos";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error de conexión";
                    return View();
                }
            }
        }

        [HttpGet]
        [Route("/CerrarSesion")]
        public ActionResult CerrarSesion()
        {
            return RedirectToAction("IniciarSesion", "Home");
        }

        //---------------------------------------//

        public ActionResult Afiliados()
        {
            using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
            {
                try
                {
                    if (codigo == "Codigo valido")
                    {
                        ViewBag.mensaje = "Se ha verificado su preinscripción.";
                        codigo = "";
                    }
                    else if (codigo == "Codigo invalido")
                    {
                        ViewBag.mensaje = "Código inválido o preinscripción no encontrada.";
                        codigo = "";
                    }

                    var session = Session["SessionUser"]?.ToString();
                    if (!string.IsNullOrEmpty(session))
                    {
                        int ids;
                        if (int.TryParse(session, out ids))
                        {
                            var dele = db.General_Delegacion_Usuarios.FirstOrDefault(d => d.Id == ids)?.Delegacion;
                            var Delegacion = db.General_Delegacion.FirstOrDefault(d => d.Id == dele)?.Nombre;
                            ViewData["Delegacion"] = Delegacion;
                            ViewBag.login = true;
                        }
                    }
                    else
                    {
                        var Delegacion = db.General_Delegacion.FirstOrDefault(d => d.Id == 0)?.Nombre;
                        ViewData["Delegacion"] = Delegacion;
                        ViewBag.LoginDelegacion = true;
                        ViewBag.login = false;
                    }

                    List<General_Localidades> Localidades = db.General_Localidades.OrderBy(d => d.Nombre_Localidad).ToList();
                    ViewData["Localidades"] = Localidades;

                    List<General_Documentos> TipoDoc = db.General_Documentos.Where(d => d.Id != 0).OrderBy(d => d.Nombre).ToList();
                    ViewData["TipoDoc"] = TipoDoc;

                    List<Empresas_Actividades> actR = db.Empresas_Actividades.Where(d => d.Id != 0).OrderBy(d => d.Descripcion).ToList();
                    ViewData["Rubro"] = actR;

                    List<General_Calificacion> CalifProf = db.General_Calificacion.Where(d => d.Id != 0).OrderBy(d => d.Nombre).ToList();
                    ViewData["CalifProf"] = CalifProf;

                    List<General_Estado_Civil> EstadoCivil = db.General_Estado_Civil.Where(d => d.Id != 0).OrderBy(d => d.Nombre).ToList();
                    ViewData["EstadoCivil"] = EstadoCivil;

                    List<General_Provincias> Provincia = db.General_Provincias.Where(d => d.Id != 0).OrderBy(d => d.Nombre).ToList();
                    ViewData["Provincia"] = Provincia;

                    List<General_Parentesco> Parentesco = db.General_Parentesco.Where(d => d.Id != 0).OrderBy(d => d.Nombre).ToList();
                    ViewData["Parentesco"] = Parentesco;

                    List<General_Sexo> Sexo = db.General_Sexo.OrderBy(d => d.Descripcion).ToList();
                    ViewData["Sexo"] = Sexo;

                    List<General_Nacionalidades> Nacionalidad = db.General_Nacionalidades.OrderBy(d => d.Descripcion).ToList();
                    ViewData["Nacionalidad"] = Nacionalidad;

                    ViewBag.diaHoy = DateTime.Now.ToString("yyyy-MM-dd");

                    return View();
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error de conexión";
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Afiliados(string matrizDatosAfiliado, string matrizEmpresa = null, string matrizFamiliares = null, 
                                        HttpPostedFileBase fileDNIFrente = null, HttpPostedFileBase fileDNIDorso = null, HttpPostedFileBase fileReciboSueldo = null, HttpPostedFileBase filePerfil = null, /*HttpPostedFileBase fileNS = null,*/
                                        HttpPostedFileBase fileFamiliar1 = null, HttpPostedFileBase fileFamiliar2 = null, HttpPostedFileBase fileFamiliar3 = null, HttpPostedFileBase fileFamiliar4 = null, HttpPostedFileBase fileFamiliar5 = null,
                                        HttpPostedFileBase fileFamiliar6 = null, HttpPostedFileBase fileFamiliar7 = null, HttpPostedFileBase fileFamiliar8 = null, HttpPostedFileBase fileFamiliar9 = null, HttpPostedFileBase fileFamiliar10 = null,
                                        HttpPostedFileBase fileFamiliarDos1 = null, HttpPostedFileBase fileFamiliarDos2 = null, HttpPostedFileBase fileFamiliarDos3 = null, HttpPostedFileBase fileFamiliarDos4 = null, HttpPostedFileBase fileFamiliarDos5 = null,
                                        HttpPostedFileBase fileFamiliarDos6 = null, HttpPostedFileBase fileFamiliarDos7 = null, HttpPostedFileBase fileFamiliarDos8 = null, HttpPostedFileBase fileFamiliarDos9 = null, HttpPostedFileBase fileFamiliarDos10 = null)
        {
            using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
            {

                string Email = "";
                string Cuil = "";

                Random numeroRandom = new Random();
                var password = numeroRandom.Next(0, 9999).ToString();
                password = password.PadLeft(4, '0');

                Boolean conf = false;

                // AFILIADO
                try
                {
                    JArray jsonPreservar = JArray.Parse(matrizDatosAfiliado);

                    Dictionary<string, string> propiedades = new Dictionary<string, string>()
                    {
                        { "Apellido", "" },
                        { "Nombre", "" },
                        { "Cuil", "" },
                        { "NumDoc", "" },
                        { "Delegacion", "" },
                        { "EstadoCivil", "" },
                        { "FechaNac", "" },
                        { "Calle", "" },
                        { "NumeroCalle", "" },
                        { "Piso", "" },
                        { "Dto", "" },
                        { "Telefono", "" },
                        { "Localidad", "" },
                        { "Provincia", "" },
                        { "Celular", "" },
                        { "SexoAfiliadoDocumento", "" },
                        { "Nacionalidad", "" },
                        { "Email", "" },
                        { "CP", "" }
                    };

                    foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                    {
                        foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                        {
                            string propiedad = jsonOPropiedades.Name;
                            if (propiedades.ContainsKey(propiedad))
                            {
                                propiedades[propiedad] = jsonOPropiedades.Value.ToString();
                            }
                        }
                    }

                    string Apellido = propiedades["Apellido"];
                    string Nombre = propiedades["Nombre"];
                    Cuil = propiedades["Cuil"];
                    string NumDoc = propiedades["NumDoc"];
                    string Delegacion = propiedades["Delegacion"];
                    string EstadoCivil = propiedades["EstadoCivil"];
                    string FechaNac = propiedades["FechaNac"];
                    string Calle = propiedades["Calle"];
                    string NumeroCalle = propiedades["NumeroCalle"];
                    string Piso = propiedades["Piso"];
                    string Dto = propiedades["Dto"];
                    string Telefono = propiedades["Telefono"];
                    string Localidad = propiedades["Localidad"];
                    string Provincia = propiedades["Provincia"];
                    string SexoAfiliadoDocumento = propiedades["SexoAfiliadoDocumento"];
                    string Nacionalidad = propiedades["Nacionalidad"];
                    string Celular = propiedades["Celular"];
                    string CP = propiedades["CP"];
                    Email = propiedades["Email"];

                    // Validar si se preinscribio anteriormente

                    Cuil = Cuil.Replace("-", "");

                    var Inscripcion = db.Afiliados_DatosPersonales.FirstOrDefault(d => d.CUIL == Cuil);
                    if (Inscripcion != null)
                    {
                        try
                        {
                            var session = Session["SessionUser"]?.ToString();
                            if (!string.IsNullOrEmpty(session) && int.TryParse(session, out int ids))
                            {
                                ViewBag.login = true;
                                return Json(new { Error = true, responseText = "Ya hay un afiliado preinscripto con mismo CUIL anteriormente." }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                ViewBag.login = false;
                                return Json(new { Error = true, responseText = "Ya hay un afiliado preinscripto con mismo CUIL anteriormente." }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.login = false;
                            return Json(new { Error = true, responseText = "Ya hay un afiliado preinscripto con mismo CUIL anteriormente." }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    byte[] bytesFrente = null;
                    byte[] bytesDorso = null;
                    byte[] bytesSueldo = null;
                    byte[] bytesPerfil = null;
                    byte[] bytesNotaSolicitud = null;

                    string extFrente = null;
                    string extDorso = null;
                    string extSueldo = null;
                    string extPerfil = null;
                    string extNotaSolicitud = null;

                    void ReadFile(HttpPostedFileBase file, out byte[] bytes, out string extension)
                    {
                        bytes = null;
                        extension = null;
                        if (file != null)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            string[] fileParts = fileName.Split('.');
                            extension = fileParts[1];
                            using (BinaryReader br = new BinaryReader(file.InputStream))
                            {
                                bytes = br.ReadBytes(file.ContentLength);
                            }
                        }
                    }

                    ReadFile(fileDNIFrente, out bytesFrente, out extFrente);
                    ReadFile(fileDNIDorso, out bytesDorso, out extDorso);
                    ReadFile(fileReciboSueldo, out bytesSueldo, out extSueldo);
                    ReadFile(filePerfil, out bytesPerfil, out extPerfil);
                    //ReadFile(fileNS, out bytesNotaSolicitud, out extNotaSolicitud);

                    int dele = 0;
                    string hoy = DateTime.Now.ToString("yyyy/MM/dd");

                    conf = false;
                    try
                    {
                        var session = Session["SessionUser"]?.ToString();
                        if (!string.IsNullOrEmpty(session) && int.TryParse(session, out int ids))
                        {
                            ViewBag.login = true;
                            dele = db.General_Delegacion.FirstOrDefault(d => d.Nombre == Delegacion)?.Id ?? 0;
                            conf = true;
                        }
                        else
                        {
                            dele = 0;
                            ViewBag.login = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        dele = 0;
                        ViewBag.login = false;
                    }

                    if (string.IsNullOrEmpty(Telefono)) Telefono = "0";

                    var emp = new Afiliados_DatosPersonales
                    {
                        ApellidoNombre = Apellido + " " + Nombre,
                        CUIL = Cuil,
                        Tipo_Doc = 1,
                        Numero_Doc = NumDoc,
                        Delegacion = dele,
                        Fecha_Solicitud = DateTime.Parse(hoy),
                        Estado_Civil = Int32.Parse(EstadoCivil),
                        Fecha_Nac = DateTime.Parse(FechaNac),
                        Calle = Calle,
                        Numero_Calle = NumeroCalle,
                        Piso = Piso,
                        Dto = Dto,
                        Telefono = Int32.Parse(Telefono),
                        Localidad = Localidad,
                        CP = CP,
                        Provincia = Provincia,
                        Sexo = Int32.Parse(SexoAfiliadoDocumento),
                        Nacionalidad = Int32.Parse(Nacionalidad),
                        Ingresado = false,
                        NroAfiliado = 0,
                        Art100 = true,
                        CuotaAfiliado = false,
                        Email = Email,
                        Celular = Celular,
                        Confirmado = conf,
                        CodigoTemporal = password,
                        Estado = "Pendiente",

                        FotoFrenteDni = bytesFrente,
                        FotoDorsoDni = bytesDorso,
                        FotoReciboSueldo = bytesSueldo,
                        FotoPerfil = bytesPerfil,
                        FotoNotaSolicitud = bytesNotaSolicitud,

                        extensionFrente = extFrente,
                        extensionDorso = extDorso,
                        extensionSueldo = extSueldo,
                        extensionPerfil = extPerfil,
                        extensionNotaSolicitud = extNotaSolicitud,
                        EnvioPDFEmail = false
                    };

                    db.Afiliados_DatosPersonales.Add(emp);
                    db.SaveChanges();
                }
                catch (Exception ex)
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

                    if (jsonPreservar != null)
                    {
                        foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                        {
                            string FechaIngresoAfiliadoEmpresa = jsonOperaciones.Value<string>("FechaIngresoAfiliadoEmpresa");
                            string NombreEmpresaAfiliadoEmpresa = jsonOperaciones.Value<string>("NombreEmpresaAfiliadoEmpresa");
                            string NombreFantasiaAfiliadoEmpresa = jsonOperaciones.Value<string>("NombreFantasiaAfiliadoEmpresa");
                            string CUITEmpresaAfiliadoEmpresa = jsonOperaciones.Value<string>("CUITEmpresaAfiliadoEmpresa");
                            string CalleAfiliadoEmpresa = jsonOperaciones.Value<string>("CalleAfiliadoEmpresa");
                            string RubroAfiliadoEmpresa = jsonOperaciones.Value<string>("RubroAfiliadoEmpresa");
                            string OtroRubroAfiliadoEmpresa = jsonOperaciones.Value<string>("OtroRubroAfiliadoEmpresa");
                            string NumeroAfiliadoEmpresa = jsonOperaciones.Value<string>("NumeroAfiliadoEmpresa");
                            string PisoAfiliadoEmpresa = jsonOperaciones.Value<string>("PisoAfiliadoEmpresa");
                            string DtoAfiliadoEmpresa = jsonOperaciones.Value<string>("DtoAfiliadoEmpresa");
                            string LocalidadAfiliadoEmpresa = jsonOperaciones.Value<string>("LocalidadAfiliadoEmpresa");
                            string TelefonoAfiliadoEmpresa = jsonOperaciones.Value<string>("TelefonoAfiliadoEmpresa");
                            string EmailAfiliadoEmpresa = jsonOperaciones.Value<string>("EmailAfiliadoEmpresa");

                            CUITEmpresaAfiliadoEmpresa = CUITEmpresaAfiliadoEmpresa.Replace("-", "");

                            if (RubroAfiliadoEmpresa == "Otro") RubroAfiliadoEmpresa = "0";

                            var emp = new Afiliados_Empresa
                            {
                                Id_Afiliado = ultimoId,
                                Fecha_Ingreso = DateTime.Parse(FechaIngresoAfiliadoEmpresa),
                                Nombre_Empresa = NombreEmpresaAfiliadoEmpresa,
                                Nombre_Fantasia = NombreFantasiaAfiliadoEmpresa,
                                Cuit_Empresa = CUITEmpresaAfiliadoEmpresa,
                                Calle = CalleAfiliadoEmpresa,
                                Rubro = Int32.Parse(RubroAfiliadoEmpresa),
                                OtroRubro = "",
                                Numero = Int32.Parse(NumeroAfiliadoEmpresa),
                                Piso = PisoAfiliadoEmpresa,
                                Dto = DtoAfiliadoEmpresa,
                                Localidad = Int32.Parse(LocalidadAfiliadoEmpresa),
                                Telefono = TelefonoAfiliadoEmpresa,
                                Email = EmailAfiliadoEmpresa
                            };

                            db.Afiliados_Empresa.Add(emp);
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception)
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

                    if (jsonPreservar != null)
                    {
                        foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                        {
                            string Parentesco = jsonOperaciones.Value<string>("Parentesco");
                            string ApellidoNombreAfiliadoFamiliar = jsonOperaciones.Value<string>("ApellidoNombreAfiliadoFamiliar");
                            //string CertEstudiosAfiliadoFamiliar = jsonOperaciones.Value<string>("CertEstudiosAfiliadoFamiliar");
                            string TipoDocAfiliadoFamiliar = jsonOperaciones.Value<string>("TipoDocAfiliadoFamiliar");
                            string NumDocAfiliadoFamiliar = jsonOperaciones.Value<string>("NumDocAfiliadoFamiliar");
                            string SexoAfiliadoFamiliar = jsonOperaciones.Value<string>("SexoAfiliadoFamiliar");
                            string FechaNacAfiliadoFamiliar = jsonOperaciones.Value<string>("FechaNacAfiliadoFamiliar");
                            string FileFamiliar = jsonOperaciones.Value<string>("FileFamiliar");
                            string FileFamiliarDos = jsonOperaciones.Value<string>("FileFamiliarDos");

                            byte[] bytesFamiliar = null;
                            string extfamiliar = null;
                            byte[] bytesFamiliarDos = null;
                            string extfamiliarDos = null;

                            void ReadFileFamiliar(HttpPostedFileBase file, out byte[] bytesf, out string extension)
                            {
                                bytesf = null;
                                extension = null;
                                if (file != null)
                                {
                                    var fileName = Path.GetFileName(file.FileName);
                                    string[] fileParts = fileName.Split('.');
                                    extension = fileParts[1];
                                    using (BinaryReader br = new BinaryReader(file.InputStream))
                                    {
                                        bytesf = br.ReadBytes(file.ContentLength);
                                    }
                                }
                            }

                            if(FileFamiliar == "1") ReadFileFamiliar(fileFamiliar1, out bytesFamiliar, out extfamiliar);
                            else if (FileFamiliar == "2") ReadFileFamiliar(fileFamiliar2, out bytesFamiliar, out extfamiliar);
                            else if (FileFamiliar == "3") ReadFileFamiliar(fileFamiliar3, out bytesFamiliar, out extfamiliar);
                            else if (FileFamiliar == "4") ReadFileFamiliar(fileFamiliar4, out bytesFamiliar, out extfamiliar);
                            else if (FileFamiliar == "5") ReadFileFamiliar(fileFamiliar5, out bytesFamiliar, out extfamiliar);
                            else if (FileFamiliar == "6") ReadFileFamiliar(fileFamiliar6, out bytesFamiliar, out extfamiliar);
                            else if (FileFamiliar == "7") ReadFileFamiliar(fileFamiliar7, out bytesFamiliar, out extfamiliar);
                            else if (FileFamiliar == "8") ReadFileFamiliar(fileFamiliar8, out bytesFamiliar, out extfamiliar);
                            else if (FileFamiliar == "9") ReadFileFamiliar(fileFamiliar9, out bytesFamiliar, out extfamiliar);
                            else if (FileFamiliar == "10") ReadFileFamiliar(fileFamiliar10, out bytesFamiliar, out extfamiliar);
                            //---------------------------------------------------------------------------------------------------//
                            if (FileFamiliarDos == "1") ReadFileFamiliar(fileFamiliarDos1, out bytesFamiliarDos, out extfamiliarDos);
                            else if (FileFamiliarDos == "2") ReadFileFamiliar(fileFamiliarDos2, out bytesFamiliarDos, out extfamiliarDos);
                            else if (FileFamiliarDos == "3") ReadFileFamiliar(fileFamiliarDos3, out bytesFamiliarDos, out extfamiliarDos);
                            else if (FileFamiliarDos == "4") ReadFileFamiliar(fileFamiliarDos4, out bytesFamiliarDos, out extfamiliarDos);
                            else if (FileFamiliarDos == "5") ReadFileFamiliar(fileFamiliarDos5, out bytesFamiliarDos, out extfamiliarDos);
                            else if (FileFamiliarDos == "6") ReadFileFamiliar(fileFamiliarDos6, out bytesFamiliarDos, out extfamiliarDos);
                            else if (FileFamiliarDos == "7") ReadFileFamiliar(fileFamiliarDos7, out bytesFamiliarDos, out extfamiliarDos);
                            else if (FileFamiliarDos == "8") ReadFileFamiliar(fileFamiliarDos8, out bytesFamiliarDos, out extfamiliarDos);
                            else if (FileFamiliarDos == "9") ReadFileFamiliar(fileFamiliarDos9, out bytesFamiliarDos, out extfamiliarDos);
                            else if (FileFamiliarDos == "10") ReadFileFamiliar(fileFamiliarDos10, out bytesFamiliarDos, out extfamiliarDos);

                            var emp = new Afiliados_Familiares
                            {
                                Id_Afiliado = ultimoId,
                                Parentesco = Parentesco,
                                Apellido_Nombre = ApellidoNombreAfiliadoFamiliar,
                                //Cert_Estudios = DateTime.Parse(CertEstudiosAfiliadoFamiliar),
                                Tipo_Doc = 1,
                                Num_Doc = NumDocAfiliadoFamiliar,
                                Sexo = Int32.Parse(SexoAfiliadoFamiliar),
                                Fecha_Nac = DateTime.Parse(FechaNacAfiliadoFamiliar),
                                DatosArchivo = bytesFamiliar,
                                ExtensionArchivo = extfamiliar,
                                DatosArchivo2 = bytesFamiliarDos,
                                ExtensionArchivo2 = extfamiliarDos
                            };

                            db.Afiliados_Familiares.Add(emp);
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception)
                {
                    return Json(new { success = true, responseText = "Error al Preinscribir Antecedente." }, JsonRequestBehavior.AllowGet);
                }


                if (!conf)
                {
                    // Enviar E-Mail de Codigo de Validacion
                    try
                    {
                        MailKit.Net.Smtp.SmtpClient smtp2 = new MailKit.Net.Smtp.SmtpClient();
                        smtp2.Connect("vps-2676239-x.dattaweb.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
                        smtp2.Authenticate("afiliaciones@seocaweb.com.ar", "Hws*7YN7kB");

                        MimeMessage email = new MimeMessage();
                        email.From.Add(MailboxAddress.Parse("afiliaciones@seocaweb.com.ar"));
                        email.To.Add(MailboxAddress.Parse(Email));
                        email.Subject = "Confirmacion de Preinscripcion Afiliado";
                        email.Body = new TextPart("html") { Text = "Haga click en este enlace para confirmar su preinscripcion. http://seoca-preafiliaciones.geosoft-web.com.ar/Home/Codigo?cuil=" + Cuil };

                        smtp2.Send(email);
                        smtp2.Disconnect(true);

                        ViewBag.mensaje = "Preinscripcion Ingresada. Confirme el Codigo que le llego a su Email para confirmar su preinscripcion.";

                    }
                    catch (Exception ex)
                    {
                        ViewBag.message = "Error al registrarse.";
                        return View("IniciarSesion");
                    }
                }
                else
                {
                    ViewBag.mensaje = "Preinscripcion Ingresada.";

                }

                return View();
            }
        }

        //---------------------------------------//

        [HttpGet]
        public ActionResult GrillaAfiliados()
        {
            try
            {
                var session = Session["SessionUser"]?.ToString();
                if (!string.IsNullOrEmpty(session))
                {
                    int ids;
                    if (int.TryParse(session, out ids))
                    {
                        using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
                        {
                            var delegacion = db.General_Delegacion_Usuarios.FirstOrDefault(d => d.Id == ids)?.Delegacion;

                            List<DatosAfiliados> empleado = db.Database.SqlQuery<DatosAfiliados>("EXEC SP_VerTodosAfiliados @apellidoNombre, @Cuil, @DNI, @Delegacion",
                                new SqlParameter("apellidoNombre", ""),
                                new SqlParameter("Cuil", ""),
                                new SqlParameter("DNI", ""),
                                new SqlParameter("Delegacion", delegacion))
                                .ToList();

                            List<DatosAfiliados> verEmpleados = new List<DatosAfiliados>();

                            foreach (var emp in empleado)
                            {
                                DatosAfiliados ver = new DatosAfiliados
                                {
                                    Codigo = Int32.Parse(emp.Codigo.ToString())
                                };

                                ver.ApellidoNombre = emp.ApellidoNombre;
                                string[] cfs = emp.Fecha_Solicitud.ToString().Split(' ');
                                ver.FS = cfs[0];
                                //ver.FS = DateTime.Parse(emp.Fecha_Solicitud.ToString("dd/MM/yyyy")).ToString();

                                string cuil = emp.CUIL; // COLOCARLE LOS GUIONES
                                cuil = cuil.Insert(2, "-").Insert(11, "-");
                                ver.CUIL = cuil;

                                ver.Numero_Doc = emp.Numero_Doc;
                                if (string.IsNullOrEmpty(emp.Estado))
                                    ver.Estado = "Pendiente";
                                else
                                    ver.Estado = emp.Estado;

                                ver.NroAfiliado = Int32.Parse(emp.NroAfiliado.ToString());

                                verEmpleados.Add(ver);
                            }

                            ViewBag.login = true;
                            ViewBag.filtroApellido = "";
                            ViewBag.filtroCUIL = "";
                            ViewBag.filtroDNI = "";
                            ViewBag.filtroEstado = "";

                            return View(verEmpleados);
                        }
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    ViewBag.login = false;
                    return RedirectToAction("IniciarSesion", "Home");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult GrillaAfiliados(FormCollection form)
        {
            try
            {
                var session = Session["SessionUser"]?.ToString();
                if (!string.IsNullOrEmpty(session))
                {
                    int ids;
                    if (int.TryParse(session, out ids))
                    {
                        string apellido = Convert.ToString(form["buscarAPELLIDO"]);
                        string CUIL = Convert.ToString(form["buscarCUIL"]);
                        string DNI = Convert.ToString(form["buscarDNI"]);
                        string estados = Convert.ToString(form["estados"]);

                        using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
                        {
                            var delegacion = db.General_Delegacion_Usuarios.FirstOrDefault(d => d.Id == ids)?.Delegacion;

                            List<DatosAfiliados> empleado = null;
                            if (estados == "0") // Todos los activos
                            {
                                empleado = db.Database.SqlQuery<DatosAfiliados>("EXEC SP_VerTodosAfiliados @apellidoNombre, @Cuil, @DNI, @Delegacion",
                                    new SqlParameter("apellidoNombre", apellido),
                                    new SqlParameter("Cuil", CUIL),
                                    new SqlParameter("DNI", DNI),
                                    new SqlParameter("Delegacion", delegacion))
                                    .ToList();
                            }
                            else // Filtrar por estado
                            {
                                empleado = db.Database.SqlQuery<DatosAfiliados>("EXEC SP_VerTodosAfiliadosSegunEstado @apellidoNombre, @Cuil, @DNI, @Estado, @Delegacion",
                                    new SqlParameter("apellidoNombre", apellido),
                                    new SqlParameter("Cuil", CUIL),
                                    new SqlParameter("DNI", DNI),
                                    new SqlParameter("Estado", estados == "1" ? "Pendiente" :
                                        estados == "2" ? "A Revisar" :
                                        estados == "3" ? "Activo" :
                                        estados == "4" ? "De Baja" : ""),
                                    new SqlParameter("Delegacion", delegacion))
                                    .ToList();
                            }

                            List<DatosAfiliados> verEmpleados = new List<DatosAfiliados>();

                            if (empleado.Count == 0) ViewBag.Mensaje = "No hay afiliados encontrados con este filtro";

                            foreach (var emp in empleado)
                            {
                                DatosAfiliados ver = new DatosAfiliados
                                {
                                    Codigo = Int32.Parse(emp.Codigo.ToString())
                                };

                                ver.ApellidoNombre = emp.ApellidoNombre;
                                string[] cfs = emp.Fecha_Solicitud.ToString().Split(' ');
                                ver.FS = cfs[0];
                                //ver.FS = DateTime.Parse(emp.Fecha_Solicitud.ToString("dd/MM/yyyy")).ToString();

                                string cuil = emp.CUIL; // COLOCARLE LOS GUIONES
                                cuil = cuil.Insert(2, "-").Insert(11, "-");
                                ver.CUIL = cuil;

                                ver.Numero_Doc = emp.Numero_Doc;
                                if (string.IsNullOrEmpty(emp.Estado))
                                    ver.Estado = "Pendiente";
                                else
                                    ver.Estado = emp.Estado;

                                ver.NroAfiliado = Int32.Parse(emp.NroAfiliado.ToString());

                                verEmpleados.Add(ver);
                            }

                            ViewBag.login = true;
                            ViewBag.filtroApellido = apellido;
                            ViewBag.filtroCUIL = CUIL;
                            ViewBag.filtroDNI = DNI;
                            ViewBag.filtroEstado = estados;

                            return View(verEmpleados);
                        }
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    ViewBag.login = false;
                    return RedirectToAction("IniciarSesion", "Home");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return View();
            }
        }

        //---------------------------------------//

        [HttpPost]
        [Route("/ObtenerRazonSocial")]
        public JsonResult ObtenerRazonSocial(string cuit)
        {
            GenericResponse<DatosEmpresa> gresponse = new GenericResponse<DatosEmpresa>();
            try
            {
                using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
                {
                    string texto = Request.Params["cuit"];
                    cuit = cuit.Replace("-", "");

                    var element = db.Secretaria_Empresas.Where(d => d.Cuit == cuit).First();

                    DatosEmpresa dt = new DatosEmpresa
                    {
                        nombreFantasia = element.NombreFantasia,
                        razonSocial = element.RazonSocial,
                        actividad = element.Actividad.ToString()
                    };

                    return Json(dt);

                }
            }
            catch (Exception ex)
            {
                DatosEmpresa dt = new DatosEmpresa
                {
                    nombreFantasia = "",
                    razonSocial = ""
                };
                return Json(dt);
            }
        }


        [HttpGet]
        [Route("/verDatoAfiliado")]
        [Route("/verDatoAfiliado/{id}")]
        public ActionResult verDatoAfiliado(int id)
        {
            try
            {
                var session = Session["SessionUser"]?.ToString();
                if (!string.IsNullOrEmpty(session))
                {
                    int ids;
                    if (int.TryParse(session, out ids))
                    {
                        using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
                        {
                            // Obtenemos los datos del empleado a modificar
                            DatoAfiliadoDetallado afiliado = db.Database.SqlQuery<DatoAfiliadoDetallado>("EXEC SP_verDatoAfiliado @Codigo", new SqlParameter("Codigo", id)).FirstOrDefault();
                            List<DatosFamiliarAfiliado> familiar = db.Database.SqlQuery<DatosFamiliarAfiliado>("EXEC SP_VerDatoFamiliar @Codigo", new SqlParameter("Codigo", afiliado?.Codigo)).ToList();

                            if (afiliado != null)
                            {
                                string[] cfs = afiliado.Fecha_Solicitud.ToString().Split(' ');

                                DatoAfiliadoDetallado verAfiliado = new DatoAfiliadoDetallado
                                {
                                    Codigo = afiliado.Codigo,
                                    ApellidoNombre = afiliado.ApellidoNombre,
                                    CUIL = afiliado.CUIL,
                                    Delegacion = afiliado.Delegacion,
                                    Numero_Doc = afiliado.Numero_Doc,
                                    Calificacion_Profesional = afiliado.Calificacion_Profesional,
                                    Estado_Civil = afiliado.Estado_Civil,
                                    FS = cfs[0],
                                    Fecha_Nac = afiliado.Fecha_Nac,
                                    Calle = afiliado.Calle,
                                    Numero_Calle = afiliado.Numero_Calle,
                                    Telefono = afiliado.Telefono,
                                    Localidad_Afiliado = afiliado.Localidad_Afiliado,
                                    NroAfiliado = afiliado.NroAfiliado,
                                    Email = afiliado.Email,
                                    Celular = afiliado.Celular,
                                    Provincia = afiliado.Provincia,
                                    Estado = afiliado.Estado,
                                    Sexo = afiliado.Sexo,
                                    Nacionalidad = afiliado.Nacionalidad,
                                    Telefono_Empresa = afiliado.Telefono_Empresa,

                                    Nombre_Empresa = afiliado.Nombre_Empresa,
                                    Cuit_Empresa = afiliado.Cuit_Empresa,
                                    Calle_Empresa = afiliado.Calle_Empresa,
                                    Numero_Empresa = afiliado.Numero_Empresa,
                                    Localidad_Empresa = afiliado.Localidad_Empresa,
                                    Fecha_Ingreso_Empresa = afiliado.Fecha_Ingreso_Empresa
                                };

                                List<DatosFamiliarAfiliado> listFamiliar = new List<DatosFamiliarAfiliado>();
                                foreach (var i in familiar)
                                {
                                    string[] ce = i.Cert_Estudios.ToString().Split(' ');
                                    string[] fn = i.Fecha_Nac.ToString().Split(' ');

                                    DatosFamiliarAfiliado dfa = new DatosFamiliarAfiliado
                                    {
                                        Apellido_Nombre = i.Apellido_Nombre,
                                        Id_Afiliado = i.Id_Afiliado,
                                        CE = ce[0],
                                        FN = fn[0],
                                        Num_Doc = i.Num_Doc,
                                        Parentesco = i.Parentesco,
                                        Sexo = i.Sexo,
                                        Tipo_Doc = i.Tipo_Doc
                                    };
                                    listFamiliar.Add(dfa);
                                }

                                ViewBag.familiar = listFamiliar.Count > 0 ? listFamiliar : null;

                                return View(verAfiliado);
                            }
                        }
                    }
                }

                ViewBag.Session = false;
                return RedirectToAction("IniciarSesion", "IniciarSesion");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return View();
            }
        }

        //---------------------------------------//

        [HttpGet]
        public ActionResult Codigo(string Cuil)
        {
            using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
            {
                try
                {
                    var val = db.Afiliados_DatosPersonales.FirstOrDefault(d => d.CUIL == Cuil);

                    if (val != null)
                    {
                        val.Confirmado = true;

                        db.Entry(val).State = EntityState.Modified;
                        db.SaveChanges();

                        codigo = "Codigo valido";
                        ViewBag.cuit = Cuil;
                    }
                    else
                    {
                        codigo = "Codigo invalido";
                        ViewBag.mensaje = "Error al enviar el email";
                        return View();
                    }

                    if (val.EnvioPDFEmail != true)
                    {
                        // crear pdf y guardarlo en servidor
                        List<ReporteAfiliado> reporteAfiliado = db.Database.SqlQuery<ReporteAfiliado>("EXEC SP_crearReporteAfiliados @Codigo", new SqlParameter("Codigo", val.Codigo)).ToList<ReporteAfiliado>();
                        ReportViewer ReportViewer = new ReportViewer();
                        ReportDataSource rdc = new ReportDataSource("DataSet1", reporteAfiliado);

                        ReportViewer.Visible = true;
                        ReportViewer.LocalReport.ReportPath = Server.MapPath("~/DatosAfiliado.rdlc");
                        ReportViewer.ShowParameterPrompts = true;
                        ReportViewer.LocalReport.DataSources.Clear();
                        ReportViewer.LocalReport.DataSources.Add(rdc);

                        ReportViewer.ProcessingMode = ProcessingMode.Local;

                        using (StreamReader rdlcSR = new StreamReader(Server.MapPath("~/DatosAfiliado.rdlc")))
                        {
                            ReportViewer.LocalReport.LoadReportDefinition(rdlcSR);
                            ReportViewer.LocalReport.Refresh();
                        }

                        ReportViewer.LocalReport.Refresh();

                        string mimeType = string.Empty;
                        string encoding = string.Empty;
                        string extension = string.Empty;

                        byte[] bytes = ReportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out string[] streamids, out Warning[] warnings);

                        // Guardar el archivo PDF en el servidor
                        string pdfFilePath = Server.MapPath("~/Pdfs/" + "InscripcionAfiliado_" + Cuil + ".pdf");
                        using (FileStream fs = new FileStream(pdfFilePath, FileMode.Create))
                        {
                            fs.Write(bytes, 0, bytes.Length);
                        }

                        // Enviar PDF por Email
                        try
                        {
                            MailKit.Net.Smtp.SmtpClient smtp2 = new MailKit.Net.Smtp.SmtpClient();
                            smtp2.Connect("vps-2676239-x.dattaweb.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
                            smtp2.Authenticate("afiliaciones@seocaweb.com.ar", "Hws*7YN7kB");

                            MimeMessage email = new MimeMessage();
                            email.From.Add(MailboxAddress.Parse("afiliaciones@seocaweb.com.ar"));
                            email.To.Add(MailboxAddress.Parse(val.Email));
                            email.Subject = "Adjunto de PDF";

                            // Crear el cuerpo del mensaje de correo electrónico
                            BodyBuilder bodyBuilder = new BodyBuilder();
                            bodyBuilder.TextBody = "Adjunto se encuentra el PDF generado.";

                            // Adjuntar el archivo PDF al mensaje de correo electrónico
                            string pdfFilePath2 = Server.MapPath("~/Pdfs/" + "InscripcionAfiliado_" + Cuil + ".pdf");
                            if (System.IO.File.Exists(pdfFilePath2))
                            {
                                bodyBuilder.Attachments.Add(pdfFilePath2);
                            }
                            else
                            {
                                return Content("El archivo PDF no existe.");
                            }

                            // Asignar el cuerpo al mensaje
                            email.Body = bodyBuilder.ToMessageBody();

                            smtp2.Send(email);
                            smtp2.Disconnect(true);

                            ViewBag.mensaje = "Preinscripcion Ingresada. Le llego un correo con el pdf adjuntado.";

                        }
                        catch (Exception ex)
                        {
                            ViewBag.message = "Error al registrarse.";
                            return View("IniciarSesion");
                        }

                        // Borrar el pdf del servidor
                        string pdfFilePath3 = Server.MapPath("~/Pdfs/" + "InscripcionAfiliado_" + Cuil + ".pdf");
                        if (System.IO.File.Exists(pdfFilePath3))
                        {
                            System.IO.File.Delete(pdfFilePath3);
                        }

                        // ENVIOPDFEMAIL
                        val.EnvioPDFEmail = true;
                        db.Entry(val).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        ViewBag.mensaje = "Ya recibio anteriormente un email con el pdf.";
                    }
                    

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    codigo = "Codigo invalido";
                }

                return View();
            }
        }
    }
}