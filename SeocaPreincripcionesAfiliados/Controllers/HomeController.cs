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

                    var soyadmin = db.General_Delegacion_Usuarios.Where(d => d.Usuario == user).Where(d => d.Contraseña == pass).First();
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
                catch (Exception ex)
                {
                    ViewBag.Error = "Datos Invalidos";
                    return View();
                }
            }
        }

        [HttpGet]
        [Route("/CerrarSesion")]
        public ActionResult CerrarSesion()
        {
            return RedirectToAction("Home", "IniciarSesion");
        }

        //---------------------------------------//

        public ActionResult Afiliados()
        {
            using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
            {
                if (codigo == "Codigo valido")
                {
                    ViewBag.mensaje = "Se ha verificado su preinscripcion.";
                    codigo = "";
                }
                else if (codigo == "Codigo invalido")
                {
                    ViewBag.mensaje = "Codigo invalido o preinscripcion no encontrada.";
                    codigo = "";
                }

                List<General_Localidades> Localidades = db.General_Localidades.ToList<General_Localidades>();
                ViewData["Localidades"] = Localidades;

                List<General_Documentos> TipoDoc = db.General_Documentos.Where(d => d.Id != 0).ToList<General_Documentos>();
                ViewData["TipoDoc"] = TipoDoc;

                try
                {
                    var session = Session["SessionUser"].ToString();
                    int ids = Int32.Parse(session.ToString());

                    var dele = db.General_Delegacion_Usuarios.Where(d => d.Id == ids).First().Delegacion;

                    var Delegacion = db.General_Delegacion.Where(d => d.Id == dele).First().Nombre;
                    ViewData["Delegacion"] = Delegacion;
                    ViewBag.login = true;
                }
                catch (Exception ex)
                {
                    var Delegacion = db.General_Delegacion.Where(d=> d.Id == 0).First().Nombre;
                    ViewData["Delegacion"] = Delegacion;
                    ViewBag.LoginDelegacion = true;
                    ViewBag.login = false;
                }

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
        public ActionResult Afiliados(string matrizDatosAfiliado, string matrizEmpresa = null, string matrizFamiliares = null, HttpPostedFileBase fileDNIFrente = null, HttpPostedFileBase fileDNIDorso = null, HttpPostedFileBase fileReciboSueldo = null, HttpPostedFileBase filePerfil = null)
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
                    string Apellido = "", Nombre = "", TipoDoc = "", Celular = "", NumDoc = "", Delegacion = "", CalifProf = "", EstadoCivil = "", FechaNac = "", Calle = "", NumeroCalle = "", Piso = "", Dto = "", Telefono = "", Localidad = "", Provincia = "", SexoAfiliadoDocumento = "", Nacionalidad = "", chkConvenio = "", chkCuota = "", chkSeguro = "";
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
                            if (propiedad.Equals("chkConvenio")) chkConvenio = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("chkCuota")) chkCuota = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("chkSeguro")) chkSeguro = jsonOPropiedades.Value.ToString();

                            if (propiedad.Equals("Email")) Email = jsonOPropiedades.Value.ToString();
                            if (propiedad.Equals("Celular")) Celular = jsonOPropiedades.Value.ToString();
                        }
                    }

                    // Validar si se preinscribio anteriormente

                    try
                    {
                        var Inscripcion = db.Afiliados_DatosPersonales.Where(d => d.CUIL == Cuil).First();
                        return Json(new { Error = true, responseText = "Ya hay un afiliado preinscripto con mismo CUIL anteriormente." }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception)
                    {
                    }

                    byte[] bytesFrente = null;
                    byte[] bytesDorso = null;
                    byte[] bytesSueldo = null;
                    byte[] bytesPerfil = null;

                    string extFrente = null;
                    string extDorso = null;
                    string extSueldo = null;
                    string extPerfil = null;

                    if (fileDNIFrente != null)
                    {
                        var FileNameFrente = System.IO.Path.GetFileName(fileDNIFrente.FileName);
                        string[] FF = FileNameFrente.Split('.');
                        extFrente = FF[1];
                        using (BinaryReader br = new BinaryReader(fileDNIFrente.InputStream))
                        {
                            bytesFrente = br.ReadBytes(fileDNIFrente.ContentLength);
                        }
                    }

                    if (fileDNIDorso != null)
                    {
                        var FileNameDorso = System.IO.Path.GetFileName(fileDNIDorso.FileName);
                        string[] FD = FileNameDorso.Split('.');
                        extDorso = FD[1];
                        using (BinaryReader br = new BinaryReader(fileDNIDorso.InputStream))
                        {
                            bytesDorso = br.ReadBytes(fileDNIDorso.ContentLength);
                        }
                    }

                    if (fileReciboSueldo != null)
                    {
                        var FileNameSueldo = System.IO.Path.GetFileName(fileReciboSueldo.FileName);
                        string[] FS = FileNameSueldo.Split('.');
                        extSueldo = FS[1];
                        using (BinaryReader br = new BinaryReader(fileReciboSueldo.InputStream))
                        {
                            bytesSueldo = br.ReadBytes(fileReciboSueldo.ContentLength);
                        }
                    }

                    if (filePerfil != null)
                    {
                        var FileNamePerfil = System.IO.Path.GetFileName(filePerfil.FileName);
                        string[] FP = FileNamePerfil.Split('.');
                        extPerfil = FP[1];
                        using (BinaryReader br = new BinaryReader(filePerfil.InputStream))
                        {
                            bytesPerfil = br.ReadBytes(filePerfil.ContentLength);
                        }
                    }

                    int dele;
                    string hoy = DateTime.Now.ToString("yyyy/MM/dd");

                    try
                    {
                        var session = Session["SessionUser"].ToString();
                        int ids = Int32.Parse(session.ToString());

                        dele = db.General_Delegacion.Where(d => d.Nombre == Delegacion).First().Id;
                        conf = true;
                    }
                    catch (Exception ex)
                    {
                        dele = Int32.Parse(Delegacion);
                    }

                    // GUARDAR EN LA TABLA DE EMPRESAS
                    var emp = new Afiliados_DatosPersonales
                    {
                        ApellidoNombre = Apellido + " " + Nombre,
                        CUIL = Cuil,
                        Tipo_Doc = Int32.Parse(TipoDoc),
                        Numero_Doc = NumDoc,
                        Delegacion = dele,
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
                        Ingresado = false,
                        NroAfiliado = 0,
                        Art100 = true,
                        Sepelio = Boolean.Parse(chkSeguro),
                        Turismo = Boolean.Parse(chkCuota),
                        Email = Email,
                        Celular = Celular,
                        Confirmado = conf,
                        CodigoTemporal = password,
                        Estado = "Pendiente",

                        FotoFrenteDni = bytesFrente,
                        FotoDorsoDni = bytesDorso,
                        FotoReciboSueldo = bytesSueldo,
                        FotoPerfil = bytesPerfil,

                        extensionFrente = extFrente,
                        extensionDorso = extDorso,
                        extensionSueldo = extSueldo,
                        extensionPerfil = extPerfil
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

                    string FechaIngresoAfiliadoEmpresa = "", NombreEmpresaAfiliadoEmpresa = "", NombreFantasiaAfiliadoEmpresa = "", CUITEmpresaAfiliadoEmpresa = "", CalleAfiliadoEmpresa = "", NumeroAfiliadoEmpresa = "", PisoAfiliadoEmpresa = "", DtoAfiliadoEmpresa = "", CPAfiliadoEmpresa = "", LocalidadAfiliadoEmpresa = "", TelefonoAfiliadoEmpresa = "", EmailAfiliadoEmpresa = "";
                    if (jsonPreservar != null)
                    {
                        foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                        {
                            foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                            {
                                string propiedad = jsonOPropiedades.Name;
                                if (propiedad.Equals("FechaIngresoAfiliadoEmpresa")) FechaIngresoAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("NombreEmpresaAfiliadoEmpresa")) NombreEmpresaAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("NombreFantasiaAfiliadoEmpresa")) NombreFantasiaAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("CUITEmpresaAfiliadoEmpresa")) CUITEmpresaAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("CalleAfiliadoEmpresa")) CalleAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("NumeroAfiliadoEmpresa")) NumeroAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("PisoAfiliadoEmpresa")) PisoAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("DtoAfiliadoEmpresa")) DtoAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("CPAfiliadoEmpresa")) CPAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("LocalidadAfiliadoEmpresa")) LocalidadAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("TelefonoAfiliadoEmpresa")) TelefonoAfiliadoEmpresa = jsonOPropiedades.Value.ToString();
                                if (propiedad.Equals("EmailAfiliadoEmpresa")) EmailAfiliadoEmpresa = jsonOPropiedades.Value.ToString();

                            }

                            // GUARDAR EN LA TABLA DE EMPRESAS ANTECEDENTES
                            var emp = new Afiliados_Empresa
                            {
                                Id_Afiliado = ultimoId,
                                Fecha_Ingreso = DateTime.Parse(FechaIngresoAfiliadoEmpresa),
                                Nombre_Empresa = NombreEmpresaAfiliadoEmpresa,
                                Nombre_Fantasia = NombreFantasiaAfiliadoEmpresa,
                                Cuit_Empresa = CUITEmpresaAfiliadoEmpresa,
                                Calle = CalleAfiliadoEmpresa,
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
                                    Sexo = Int32.Parse(SexoAfiliadoFamiliar),
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
                                    Sexo = Int32.Parse(SexoAfiliadoFamiliar),
                                    Fecha_Nac = DateTime.Parse(FechaNacAfiliadoFamiliar)
                                };

                                db.Afiliados_Familiares.Add(emp);
                                db.SaveChanges();
                            }


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
                        MailMessage omail = new MailMessage("seoca_noresponder@geosoft-web.com.ar", Email, "Clave de Preinscripcion", "Ingrese este codigo para confirmar su Preinscripcion de Afiliado. Su codigo es: " + password)
                        {
                            IsBodyHtml = true
                        };

                        //Attachment _attachment = new Attachment(@Archivo);
                        //omail.Attachments.Add(_attachment);


                        SmtpClient smtp = new SmtpClient("mail.geosoft-web.com.ar")
                        {
                            EnableSsl = false,
                            UseDefaultCredentials = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Port = 25,
                            Credentials = new System.Net.NetworkCredential("seoca_noresponder@geosoft-web.com.ar", "$a1b2c3d4e5$")
                        };

                        smtp.Send(omail);
                        smtp.Dispose();
                    }
                    catch (Exception ex)
                    {

                    }
                }

                // Creacion de PDF
                try
                {

                    // Obtener ultimo id de Liquidacion
                    //string ultID = db.Afiliados_DatosPersonales.Where(d => d.IdEmpresa == ids).OrderByDescending(d => d.IdLiquidacion).First().IdLiquidacion.ToString();
                    List<ReporteAfiliado> reporteAfiliado = db.Database.SqlQuery<ReporteAfiliado>("EXEC SP_crearReporteAfiliados @Codigo", new SqlParameter("Codigo", ultimoId)).ToList<ReporteAfiliado>();

                    ReportViewer ReportViewer = new ReportViewer();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", reporteAfiliado);

                    ReportViewer.Visible = true;
                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/DatosAfiliado.rdlc");
                    //ReportViewer.LocalReport.ReportPath = "~/ReporteLiquidacion.rdlc";
                    ReportViewer.ShowParameterPrompts = true;
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.DataSources.Add(rdc);

                    ReportViewer.ProcessingMode = ProcessingMode.Local;

                    using (StreamReader rdlcSR = new StreamReader(Server.MapPath("~/DatosAfiliado.rdlc")))
                    {
                        ReportViewer.LocalReport.LoadReportDefinition(rdlcSR);
                        ReportViewer.LocalReport.Refresh();
                    }

                    ReportParameter parameter = new ReportParameter("ultimoID", HttpUtility.HtmlDecode(ultimoId.ToString()));
                    ReportViewer.LocalReport.SetParameters(parameter);

                    ReportViewer.LocalReport.Refresh();

                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    //ReportViewer.ShowParameterPrompts = true;

                    byte[] bytes = ReportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out string[] streamids, out Warning[] warnings);

                    string file = "DatoAfiliado_" + Cuil + ".pdf";
                    string XLPath = Server.MapPath("~\\PDFs");

                    //FileStream fs = new FileStream(XLPath + "\\" + file.Replace("/", "_"), FileMode.Create, FileAccess.Write);
                    //fs.Write(bytes, 0, bytes.Length);
                    //fs.Close();

                    Response.Clear();
                    Response.ContentType = "application/octet-stream";

                    Response.AddHeader("content-disposition", "attachment;filename=" + file);
                    Response.Buffer = true;

                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.OutputStream.Flush();


                    ViewBag.message = "Preinscripcion Ingresada. Su id es " + ultimoId + ". Confirme el Codigo que le llego a su Email para confirmar su preinscripcion.";
                }
                catch (Exception ex)
                {
                    ViewBag.message = "Error en la creacion del pdf";
                    return RedirectToAction("Liquidacion");
                }

                // Limpiar Planilla
                ViewBag.vacio = "";
                ViewBag.comboVacio = 0;

                ViewBag.mensaje = "Preinscripcion Ingresada. Su id es " + ultimoId + ". Confirme el Codigo que le llego a su Email para confirmar su preinscripcion.";
                return View();
            }
        }

        //---------------------------------------//

        [HttpGet]
        public ActionResult GrillaAfiliados()
        {
            try
            {
                var session = Session["SessionUser"].ToString();
                int ids = Int32.Parse(session.ToString());

                try
                {
                    using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
                    {
                        //List<DatosAfiliados> empleado = db.Database.SqlQuery<DatosAfiliados>("EXEC SP_VerTablaAfiliados").ToList<DatosAfiliados>();
                        List<DatosAfiliados> empleado = db.Database.SqlQuery<DatosAfiliados>("EXEC SP_VerTodosAfiliados @apellidoNombre, @Cuil, @DNI", new SqlParameter("apellidoNombre", ""), new SqlParameter("Cuil", ""), new SqlParameter("DNI", "")).ToList<DatosAfiliados>();

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
                            if (string.IsNullOrEmpty(emp.Estado)) ver.Estado = "Pendiente";
                            else ver.Estado = emp.Estado;

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
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.login = false;
                return RedirectToAction("IniciarSesion", "Home");
            }
        }

        [HttpPost]
        public ActionResult GrillaAfiliados(FormCollection form)
        {
            try
            {
                var session = Session["SessionUser"].ToString();
                int ids = Int32.Parse(session.ToString());

                string apellido = Convert.ToString(form["buscarAPELLIDO"]);
                string CUIL = Convert.ToString(form["buscarCUIL"]);
                string DNI = Convert.ToString(form["buscarDNI"]);
                string estados = Convert.ToString(form["estados"]);

                try
                {
                    using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
                    {
                        List<DatosAfiliados> empleado = null;
                        //TODOS LOS ACTIVOS
                        if (estados == "0")
                        {
                            empleado = db.Database.SqlQuery<DatosAfiliados>("EXEC SP_VerTodosAfiliados @apellidoNombre, @Cuil, @DNI", new SqlParameter("apellidoNombre", apellido), new SqlParameter("Cuil", CUIL), new SqlParameter("DNI", DNI)).ToList<DatosAfiliados>();
                        }
                        else if (estados == "1") // Pendiente
                        {
                            empleado = db.Database.SqlQuery<DatosAfiliados>("EXEC SP_VerTodosAfiliadosSegunEstado @apellidoNombre, @Cuil, @DNI, @Estado", new SqlParameter("apellidoNombre", apellido), new SqlParameter("Cuil", CUIL), new SqlParameter("DNI", DNI), new SqlParameter("Estado", "Pendiente")).ToList<DatosAfiliados>();
                        }
                        else if (estados == "2") // A revisar
                        {
                            empleado = db.Database.SqlQuery<DatosAfiliados>("EXEC SP_VerTodosAfiliadosSegunEstado @apellidoNombre, @Cuil, @DNI, @Estado", new SqlParameter("apellidoNombre", apellido), new SqlParameter("Cuil", CUIL), new SqlParameter("DNI", DNI), new SqlParameter("Estado", "A Revisar")).ToList<DatosAfiliados>();
                        }
                        else if (estados == "3") // Activo
                        {
                            empleado = db.Database.SqlQuery<DatosAfiliados>("EXEC SP_VerTodosAfiliadosSegunEstado @apellidoNombre, @Cuil, @DNI, @Estado", new SqlParameter("apellidoNombre", apellido), new SqlParameter("Cuil", CUIL), new SqlParameter("DNI", DNI), new SqlParameter("Estado", "Activo")).ToList<DatosAfiliados>();
                        }
                        else if (estados == "4") // De Baja
                        {
                            empleado = db.Database.SqlQuery<DatosAfiliados>("EXEC SP_VerTodosAfiliadosSegunEstado @apellidoNombre, @Cuil, @DNI, @Estado", new SqlParameter("apellidoNombre", apellido), new SqlParameter("Cuil", CUIL), new SqlParameter("DNI", DNI), new SqlParameter("Estado", "De Baja")).ToList<DatosAfiliados>();
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
                            if (string.IsNullOrEmpty(emp.Estado)) ver.Estado = "Pendiente";
                            else ver.Estado = emp.Estado;

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
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.login = false;
                return RedirectToAction("IniciarSesion", "Home");
            }
        }

        //---------------------------------------//

        [HttpGet]
        [Route("/verDatoAfiliado")]
        [Route("/verDatoAfiliado/{id}")]
        public ActionResult verDatoAfiliado(int id)
        {
            try
            {
                var session = Session["SessionUser"].ToString();
                int ids = Int32.Parse(session.ToString());

                using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities())
                {
                    // Obtenemos los datos del empleado a modificar
                    DatoAfiliadoDetallado afiliado = db.Database.SqlQuery<DatoAfiliadoDetallado>("EXEC SP_verDatoAfiliado @Codigo", new SqlParameter("Codigo", id)).First<DatoAfiliadoDetallado>();
                    List<DatosFamiliarAfiliado> familiar = db.Database.SqlQuery<DatosFamiliarAfiliado>("EXEC SP_VerDatoFamiliar @Codigo", new SqlParameter("Codigo", afiliado.Codigo)).ToList<DatosFamiliarAfiliado>();

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
                    foreach(var i in familiar)
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


                    if(familiar.Count != 0) ViewBag.familiar = listFamiliar;
                    else ViewBag.familiar = null;

                    return View(verAfiliado);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Session = false;
                return RedirectToAction("IniciarSesion", "IniciarSesion");
            }
        }

        //---------------------------------------//


        public ActionResult Codigo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Codigo(string Cuil, string Codigo)
        {
            using (Models.geosoftw_seocapreinscripcionesEntities db = new Models.geosoftw_seocapreinscripcionesEntities()) {
                try
                {
                    var val = db.Afiliados_DatosPersonales.Where(d => d.CUIL == Cuil && d.CodigoTemporal == Codigo).First();

                    Models.Afiliados_DatosPersonales adp = db.Afiliados_DatosPersonales.Find(val.Codigo);
                    adp.Confirmado = true;

                    db.Afiliados_DatosPersonales.Attach(adp);
                    db.Entry(adp).State = EntityState.Modified;//this is for modiying/update existing entry
                    db.SaveChanges();
                    
                    codigo = "Codigo valido";
                }
                catch (Exception)
                {
                    codigo = "Codigo invalido";
                }
            }

            return RedirectToAction("Afiliados", "Home");
        }
    }
}