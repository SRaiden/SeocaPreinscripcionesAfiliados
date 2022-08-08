var matrizDatosAfiliado = new Array();
var matrizDocumento = new Array();
var matrizEmpresa = new Array();
var matrizFamiliares = new Array();

function enviar() {


    var Apellido = document.getElementById("Apellido").value;
    var Nombre = document.getElementById("Nombre").value;
    var Cuil = document.getElementById("Cuil").value;

    var TipoDoc = document.getElementById("TipoDoc").value; //select
    var NumDoc = document.getElementById("NumDoc").value;
    var Delegacion = document.getElementById("Delegacion").value; //select

    var CalifProf = document.getElementById("CalifProf").value; //select
    var Fecha_Afiliacion = document.getElementById("Fecha_Afiliacion").value;
    var EstadoCivil = document.getElementById("EstadoCivil").value; //select

    var FechaNac = document.getElementById("FechaNac").value;
    var Calle = document.getElementById("Calle").value;
    var NumeroCalle = document.getElementById("NumeroCalle").value;

    var Piso = document.getElementById("Piso").value;
    var Dto = document.getElementById("Dto").value;
    var Telefono = document.getElementById("Telefono").value;

    var CP = document.getElementById("CP").value;
    var Localidad = document.getElementById("Localidad").value;
    var Provincia = document.getElementById("Provincia").value; //select


    // Validaciones
    if (Apellido == "") {
        alert("Debe de ingresar un Apellido");
        return false;
    }
    if (Nombre == "") {
        alert("Debe de ingresar un Nombre");
        return false;
    }
    if (Cuil == "") {
        alert("Debe de ingresar el Cuil");
        return false;
    }
    if (isNaN(Cuil)) {
        alert("Ingrese solo numeros en el campo Cuit");
        return false;
    }
    if (Cuil.length != 11) {
        alert("El CUIL debe de constar de 11 caracteres");
        return false;
    }
    if (TipoDoc == 0) {
        alert("Debe de seleccionar un Tipo Documento");
        return false;
    }
    if (Actividad == 0) {
        alert("Debe de seleccionar una Actividad");
        return false;
    }
    if (NumDoc == "") {
        alert("Debe de ingresar un Numero Documento");
        return false;
    }
    if (isNaN(NumDoc)) {
        alert("Ingrese solo numeros en el campo Numero Documento");
        return false;
    }
    if (Delegacion == 0) {
        alert("Debe de seleccionar una Delegacion");
        return false;
    }
    if (CalifProf == 0) {
        alert("Debe de seleccionar una Calif. Profesional");
        return false;
    }
    if (EstadoCivil == 0) {
        alert("Debe de seleccionar un Estado Civil");
        return false;
    }
    if (Provincia == 0) {
        alert("Debe de seleccionar una Provincia");
        return false;
    }
    if (Localidad == "") {
        alert("Debe de ingresar una Localidad");
        return false;
    }
    if (CP == "") {
        alert("Debe de ingresar el CP");
        return false;
    }
    if (isNaN(CP)) {
        alert("Ingrese solo numeros en el campo CP");
        return false;
    }
    if (Fecha_Afiliacion == "") {
        alert("Debe de ingresar la Fecha de Afiliacion");
        return false;
    }
    if (FechaNac == "") {
        alert("Debe de ingresar la Fecha de Nacimiento");
        return false;
    }

    matrizDatosAfiliado.push({
        Apellido: Apellido,
        Nombre: Nombre,
        Cuil: Cuil,
        TipoDoc: TipoDoc,
        NumDoc: NumDoc,
        Delegacion: Delegacion,
        CalifProf: CalifProf,
        Fecha_Afiliacion: Fecha_Afiliacion,
        EstadoCivil: EstadoCivil,
        FechaNac: FechaNac,
        Calle: Calle,
        NumeroCalle: NumeroCalle,
        Piso: Piso,
        Dto: Dto,
        Telefono: Telefono,
        CP: CP,
        Localidad: Localidad,
        Provincia: Provincia
    });


    //-------------------------------------------------------------------------------------------------//

    var SexoAfiliadoDocumento = document.getElementById("SexoAfiliadoDocumento").value;
    var Nacionalidad = document.getElementById("Nacionalidad").value;

    //validaciones
    if (SexoAfiliadoDocumento == 0) {
        alert("Debe de seleccionar un Sexo");
        return false;
    }
    if (Nacionalidad == 0) {
        alert("Debe de seleccionar una Nacionalidad");
        return false;
    }

    matrizDocumento.push({
        SexoAfiliadoDocumento: SexoAfiliadoDocumento,
        Nacionalidad: Nacionalidad
    });

    //-------------------------------------------------------------------------------------------------//

    var FechaIngresoAfiliadoEmpresa = document.getElementById("FechaIngresoAfiliadoEmpresa").value;
    var NombreEmpresaAfiliadoEmpresa = document.getElementById("NombreEmpresaAfiliadoEmpresa").value;
    var CUITEmpresaAfiliadoEmpresa = document.getElementById("CUITEmpresaAfiliadoEmpresa").value;
    var CalleAfiliadoEmpresa = document.getElementById("CalleAfiliadoEmpresa").value;
    var NumeroAfiliadoEmpresa = document.getElementById("NumeroAfiliadoEmpresa").value;
    var LocalAfiliadoEmpresa = document.getElementById("LocalAfiliadoEmpresa").value;
    var PisoAfiliadoEmpresa = document.getElementById("PisoAfiliadoEmpresa").value;
    var DtoAfiliadoEmpresa = document.getElementById("DtoAfiliadoEmpresa").value;
    var CPAfiliadoEmpresa = document.getElementById("CPAfiliadoEmpresa").value;
    var LocalidadAfiliadoEmpresa = document.getElementById("LocalidadAfiliadoEmpresa").value;
    var TelefonoAfiliadoEmpresa = document.getElementById("TelefonoAfiliadoEmpresa").value;

    //validaciones
    if (FechaIngresoAfiliadoEmpresa == "") {
        alert("Debe de ingresar la Fecha de Ingreso");
        return false;
    }
    if (NombreEmpresaAfiliadoEmpresa == "") {
        alert("Debe de ingresar EL Nombre de la Empresa");
        return false;
    }
    if (CUITEmpresaAfiliadoEmpresa == "") {
        alert("Debe de ingresar el CUIT de la Empresa");
        return false;
    }
    if (isNaN(CUITEmpresaAfiliadoEmpresa)) {
        alert("Ingrese solo numeros en el campo CUIT de la Empresa");
        return false;
    }
    if (CUITEmpresaAfiliadoEmpresa.length != 11) {
        alert("El CUIT de la Empresa debe de constar de 11 caracteres");
        return false;
    }
    if (isNaN(CPAfiliadoEmpresa)) {
        alert("Ingrese solo numeros en el campo CP de la Empresa");
        return false;
    }
    if (LocalidadAfiliadoEmpresa == "") {
        alert("Debe de ingresar la Localidad de la empresa");
        return false;
    }

    matrizEmpresa.push({
        FechaIngresoAfiliadoEmpresa: FechaIngresoAfiliadoEmpresa,
        NombreEmpresaAfiliadoEmpresa: NombreEmpresaAfiliadoEmpresa,
        CUITEmpresaAfiliadoEmpresa: CUITEmpresaAfiliadoEmpresa,
        CalleAfiliadoEmpresa: CalleAfiliadoEmpresa,
        NumeroAfiliadoEmpresa: NumeroAfiliadoEmpresa,
        LocalAfiliadoEmpresa: LocalAfiliadoEmpresa,
        PisoAfiliadoEmpresa: PisoAfiliadoEmpresa,
        DtoAfiliadoEmpresa: DtoAfiliadoEmpresa,
        CPAfiliadoEmpresa: CPAfiliadoEmpresa,
        LocalidadAfiliadoEmpresa: LocalidadAfiliadoEmpresa,
        TelefonoAfiliadoEmpresa: TelefonoAfiliadoEmpresa
    });

    //-------------------------------------------------------------------------------------------------//


    if (matrizDocumento == null) matrizDocumento = "";
    if (matrizEmpresa == null) matrizEmpresa = "";
    if (matrizFamiliares == null) matrizFamiliares = "";

    $.ajax({
        url: '/Home/Afiliados',
        type: 'POST',
        dataType: 'json',
        success: function (response) {
            alert(response.responseText);
            window.open(response.enlace);
            location.reload();

        },
        error: function (response) {
            alert(response.responseText);
        },
        data: {
            matrizDatosAfiliado: JSON.stringify(matrizDatosAfiliado),
            matrizDocumento: JSON.stringify(matrizDocumento),
            matrizEmpresa: JSON.stringify(matrizEmpresa),
            matrizFamiliares: JSON.stringify(matrizFamiliares)
        }
    });


}