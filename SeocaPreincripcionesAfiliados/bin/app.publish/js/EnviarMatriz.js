var matrizEmpresa = new Array();
var matrizDatosAfiliado = new Array();
var matrizFamiliares = new Array();

function borrarCampos() {
    document.getElementById("Apellido").value = "";
    document.getElementById("Nombre").value = "";
    document.getElementById("Cuil").value = "";
    document.getElementById("TipoDoc").value = 0;
    document.getElementById("NumDoc").value = "";
    document.getElementById("CalifProf").value = 0;
    document.getElementById("EstadoCivil").value = 0;
    document.getElementById("FechaNac").value = "";
    document.getElementById("Calle").value = "";
    document.getElementById("NumeroCalle").value = "";
    document.getElementById("Piso").value = "";
    document.getElementById("Dto").value = "";
    document.getElementById("Telefono").value = "";
    document.getElementById("Celular").value = "";
    document.getElementById("Email").value = "";
    document.getElementById("Localidad").value = 0;
    document.getElementById("Provincia").value = 0;
    document.getElementById("SexoAfiliadoDocumento").value = 0;
    document.getElementById("Nacionalidad").value = 0;
    document.getElementById("chkCuota").checked = false;
    document.getElementById("chkSeguro").checked = false;

    document.getElementById("FechaIngresoAfiliadoEmpresa").value = "";
    document.getElementById("NombreEmpresaAfiliadoEmpresa").value = "";
    document.getElementById("NombreFantasiaAfiliadoEmpresa").value = "";
    document.getElementById("CUITEmpresaAfiliadoEmpresa").value = "";
    document.getElementById("CalleAfiliadoEmpresa").value = "";
    document.getElementById("NumeroAfiliadoEmpresa").value = "";
    document.getElementById("PisoAfiliadoEmpresa").value = "";
    document.getElementById("DtoAfiliadoEmpresa").value = "";
    document.getElementById("CPAfiliadoEmpresa").value = "";
    document.getElementById("LocalidadAfiliadoEmpresa").value = 0;
    document.getElementById("TelefonoAfiliadoEmpresa").value = "";
    document.getElementById("EmailAfiliadoEmpresa").value = "";

}


function enviar() {

    var Apellido = document.getElementById("Apellido").value;
    var Nombre = document.getElementById("Nombre").value;
    var Cuil = document.getElementById("Cuil").value;
    var TipoDoc = document.getElementById("TipoDoc").value;
    var NumDoc = document.getElementById("NumDoc").value;
    var Delegacion = document.getElementById("Delegacion").value;
    var CalifProf = document.getElementById("CalifProf").value;
    var EstadoCivil = document.getElementById("EstadoCivil").value;
    var FechaNac = document.getElementById("FechaNac").value;
    var Calle = document.getElementById("Calle").value;
    var NumeroCalle = document.getElementById("NumeroCalle").value;
    var Piso = document.getElementById("Piso").value;
    var Dto = document.getElementById("Dto").value;
    var Telefono = document.getElementById("Telefono").value;
    var Celular = document.getElementById("Celular").value;
    var Email = document.getElementById("Email").value;
    var Localidad = document.getElementById("Localidad").value;
    var Provincia = document.getElementById("Provincia").value;
    var SexoAfiliadoDocumento = document.getElementById("SexoAfiliadoDocumento").value;
    var Nacionalidad = document.getElementById("Nacionalidad").value;
    var chkConvenio = document.getElementById("chkConvenio").value;
    var chkCuota = document.getElementById("chkCuota").checked;
    var chkSeguro = document.getElementById("chkSeguro").checked;

    var FechaIngresoAfiliadoEmpresa = document.getElementById("FechaIngresoAfiliadoEmpresa").value;
    var NombreEmpresaAfiliadoEmpresa = document.getElementById("NombreEmpresaAfiliadoEmpresa").value;
    var NombreFantasiaAfiliadoEmpresa = document.getElementById("NombreFantasiaAfiliadoEmpresa").value;
    var CUITEmpresaAfiliadoEmpresa = document.getElementById("CUITEmpresaAfiliadoEmpresa").value;
    var CalleAfiliadoEmpresa = document.getElementById("CalleAfiliadoEmpresa").value;
    var NumeroAfiliadoEmpresa = document.getElementById("NumeroAfiliadoEmpresa").value;
    var PisoAfiliadoEmpresa = document.getElementById("PisoAfiliadoEmpresa").value;
    var DtoAfiliadoEmpresa = document.getElementById("DtoAfiliadoEmpresa").value;
    var CPAfiliadoEmpresa = document.getElementById("CPAfiliadoEmpresa").value;
    var LocalidadAfiliadoEmpresa = document.getElementById("LocalidadAfiliadoEmpresa").value;
    var TelefonoAfiliadoEmpresa = document.getElementById("TelefonoAfiliadoEmpresa").value;
    var EmailAfiliadoEmpresa = document.getElementById("EmailAfiliadoEmpresa").value;

    // Validaciones
    if (Apellido == "") {
        alert("Debe de ingresar un Apellido");
        return false;
    }
    if (!isNaN(Apellido)) {
        alert("No puede ingresar numeros en Apellido");
        return false;
    }
    if (Nombre == "") {
        alert("Debe de ingresar un Nombre");
        return false;
    }
    if (!isNaN(Nombre)) {
        alert("No puede ingresar numeros en Nombre");
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
    if (NumDoc == "") {
        alert("Debe de ingresar un Numero Documento");
        return false;
    }
    if (isNaN(NumDoc)) {
        alert("Ingrese solo numeros en el campo Numero Documento");
        return false;
    }
    if (NumDoc < 0) {
        alert("No se puede ingresar valores negativos en Numero de Documento");
        return false;
    }
    //if (Delegacion == 0) {
    //    alert("Debe de seleccionar una Delegacion");
    //    return false;
    //}
    if (CalifProf == 0) {
        alert("Debe de seleccionar una Calificacion Profesional");
        return false;
    }
    if (EstadoCivil == 0) {
        alert("Debe de seleccionar un Estado Civil");
        return false;
    }
    if (FechaNac == "") {
        alert("Debe de ingresar la Fecha de Nacimiento");
        return false;
    }
    if (Calle == "") {
        alert("Debe de ingresar la Calle");
        return false;
    }
    if (NumeroCalle == "") {
        alert("Debe de ingresar el N° de Calle");
        return false;
    }
    if (Telefono == "") {
        alert("Debe de ingresar el N° de Telefono");
        return false;
    }
    if (Localidad == 0) {
        alert("Debe de ingresar la Localidad");
        return false;
    }
    if (Provincia == 0) {
        alert("Debe de seleccionar una Provincia");
        return false;
    }
    if (SexoAfiliadoDocumento == 0) {
        alert("Debe de seleccionar un Genero (Documento)");
        return false;
    }
    if (Nacionalidad == 0) {
        alert("Debe de ingresar la Nacionalidad");
        return false;
    }
    if (Email == "") {
        alert("Debe de ingresar el Email");
        return false;
    }
    var emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (!emailRegex.test(Email)) {
        alert("Formato de Email no valido");
        return false;
    }


    //-------------------------------------------------------------------//


    if (FechaIngresoAfiliadoEmpresa == "") {
        alert("Debe de ingresar la Fecha de Ingreso (Empresa)");
        return false;
    }
    if (NombreEmpresaAfiliadoEmpresa == "") {
        alert("Debe de ingresar el Nombre de Empresa (Empresa)");
        return false;
    }
    if (CUITEmpresaAfiliadoEmpresa == "") {
        alert("Debe de ingresar el Cuilt de Empresa (Empresa)");
        return false;
    }
    if (isNaN(CUITEmpresaAfiliadoEmpresa)) {
        alert("Ingrese solo numeros en el campo Cuit");
        return false;
    }
    if (CUITEmpresaAfiliadoEmpresa.length != 11) {
        alert("El CUIT de la empresa debe de constar de 11 caracteres");
        return false;
    }
    if (CalleAfiliadoEmpresa == "") {
        alert("Debe de ingresar la Calle de la Empresa (Empresa)");
        return false;
    }
    if (NumeroAfiliadoEmpresa == "") {
        alert("Debe de ingresar el N° de Calle de la Empresa (Empresa)");
        return false;
    }
    if (CPAfiliadoEmpresa == 0) {
        alert("Debe de ingresar el Codigo Postal (Empresa)");
        return false;
    }
    if (CPAfiliadoEmpresa < 0) {
        alert("No puede ingresar numeros negativos en Codigo Postal (Empresa)");
        return false;
    }
    if (LocalidadAfiliadoEmpresa == 0) {
        alert("Debe de ingresar la Localidad (Empresa)");
        return false;
    }
    if (TelefonoAfiliadoEmpresa == "") {
        alert("Debe de ingresar el Telefono (Empresa)");
        return false;
    }
    if (EmailAfiliadoEmpresa == "") {
        alert("Debe de ingresar el Email");
        return false;
    }

    var emailRegexD = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;

    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (!emailRegexD.test(EmailAfiliadoEmpresa)) {
        alert("Formato de Email no valido");
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
        EstadoCivil: EstadoCivil,
        FechaNac: FechaNac,
        Calle: Calle,
        NumeroCalle: NumeroCalle,
        Piso: Piso,
        Dto: Dto,
        Telefono: Telefono,
        Localidad: Localidad,
        Provincia: Provincia,
        SexoAfiliadoDocumento: SexoAfiliadoDocumento,
        Nacionalidad: Nacionalidad,
        chkConvenio: chkConvenio,
        chkCuota: chkCuota,
        chkSeguro: chkSeguro,
        Email: Email,
        Celular: Celular
    });

    matrizEmpresa.push({
        FechaIngresoAfiliadoEmpresa: FechaIngresoAfiliadoEmpresa,
        NombreEmpresaAfiliadoEmpresa: NombreEmpresaAfiliadoEmpresa,
        NombreFantasiaAfiliadoEmpresa: NombreFantasiaAfiliadoEmpresa,
        CUITEmpresaAfiliadoEmpresa: CUITEmpresaAfiliadoEmpresa,
        CalleAfiliadoEmpresa: CalleAfiliadoEmpresa,
        NumeroAfiliadoEmpresa: NumeroAfiliadoEmpresa,
        PisoAfiliadoEmpresa: PisoAfiliadoEmpresa,
        DtoAfiliadoEmpresa: DtoAfiliadoEmpresa,
        CPAfiliadoEmpresa: CPAfiliadoEmpresa,
        LocalidadAfiliadoEmpresa: LocalidadAfiliadoEmpresa,
        TelefonoAfiliadoEmpresa: TelefonoAfiliadoEmpresa,
        EmailAfiliadoEmpresa: EmailAfiliadoEmpresa
    });

    //-------------------------------------------------------------------------------------------------//

    if (matrizEmpresa == "") matrizEmpresa = null;
    if (matrizFamiliares == "") matrizFamiliares = null;



    //-------------------------------------------------------------------------------------------------//

    document.getElementById("matrizEmpresa").value = JSON.stringify(matrizEmpresa);
    document.getElementById("matrizDatosAfiliado").value = JSON.stringify(matrizDatosAfiliado);
    document.getElementById("matrizFamiliares").value = JSON.stringify(matrizFamiliares);

    //$.ajax({
    //    url: '/Home/Afiliados',
    //    type: 'POST',
    //    dataType: 'json',
    //    success: function (response) {
    //        alert(response.responseText);
    //        location.reload();
    //    },
    //    error: function (response) {
    //        alert(response.responseText);
    //    },
    //    data: {
    //        matrizDatosAfiliado: JSON.stringify(matrizDatosAfiliado),
    //        matrizEmpresa: JSON.stringify(matrizEmpresa),
    //        matrizFamiliares: JSON.stringify(matrizFamiliares)
    //    }
    //});
}



// -------------------------------------------------------------- //

function insertarFamiliar() {
    var Parentesco = document.getElementById("Parentesco").value; // select
    var ApellidoNombreAfiliadoFamiliar = document.getElementById("ApellidoNombreAfiliadoFamiliar").value;
    var CertEstudiosAfiliadoFamiliar = document.getElementById("CertEstudiosAfiliadoFamiliar").value;
    var TipoDocAfiliadoFamiliar = document.getElementById("TipoDocAfiliadoFamiliar").value; // select
    var NumDocAfiliadoFamiliar = document.getElementById("NumDocAfiliadoFamiliar").value;
    var SexoAfiliadoFamiliar = document.getElementById("SexoAfiliadoFamiliar").value; // select
    var FechaNacAfiliadoFamiliar = document.getElementById("FechaNacAfiliadoFamiliar").value;

    //Validaciones
    if (Parentesco == 0) {
        alert("Debe de ingresar el Parentesco");
        return false;
    }
    if (ApellidoNombreAfiliadoFamiliar == "") {
        alert("Debe de ingresar el Nombre y Apellido del familiar");
        return false;
    }
    if (!isNaN(ApellidoNombreAfiliadoFamiliar)) {
        alert("No puede ingresar numeros en Nombre y Apellido (Familiar)");
        return false;
    }
    if (TipoDocAfiliadoFamiliar == 0) {
        alert("Debe de ingresar el Tipo de Documento");
        return false;
    }
    if (NumDocAfiliadoFamiliar == "") {
        alert("Debe de ingresar el Numero de Documento");
        return false;
    }
    if (SexoAfiliadoFamiliar == 0) {
        alert("Debe de ingresar el Sexo del familiar");
        return false;
    }
    if (FechaNacAfiliadoFamiliar == "") {
        alert("Ingrese la Fecha de Nacimiento del Familiar");
        return false;
    }

    for (i = 0; i < matrizFamiliares.length; i++) {
        if (matrizFamiliares[i].NumDocAfiliadoFamiliar == NumDocAfiliadoFamiliar) {
            alert("Ingrese un DNI distinto");
            return false;
        }
    }


    var table = document.getElementById('bodyFamiliar');
    var x = table.insertRow(0);
    var e = table.rows.length - 1;
    var l = table.rows[e].cells.length;

    //x.innerHTML = "&nbsp;";
    table.rows[0].insertCell(0);
    table.rows[0].cells[0].innerHTML = Parentesco;
    table.rows[0].insertCell(1);
    table.rows[0].cells[1].innerHTML = ApellidoNombreAfiliadoFamiliar;
    table.rows[0].insertCell(2);
    table.rows[0].cells[2].innerHTML = NumDocAfiliadoFamiliar;
    table.rows[0].insertCell(3);
    table.rows[0].cells[3].innerHTML = '<button class="w3-right w3-margin-top eliminar w3-card bg-zul w3-text-white w3-hover-red w3-hover-border-cyan borrarFamiliar" type="button"  onclick="eliminarFamiliar(' + "'" + NumDocAfiliadoFamiliar + "'" + ')">Eliminar</button >';

    document.getElementById("Parentesco").value = "0"; // select
    document.getElementById("ApellidoNombreAfiliadoFamiliar").value = "";
    document.getElementById("CertEstudiosAfiliadoFamiliar").value = "";
    document.getElementById("TipoDocAfiliadoFamiliar").value = "0"; // select
    document.getElementById("NumDocAfiliadoFamiliar").value = "";
    document.getElementById("SexoAfiliadoFamiliar").value = "0"; // select
    document.getElementById("FechaNacAfiliadoFamiliar").value = "";

    matrizFamiliares.push({
        Parentesco: Parentesco,
        ApellidoNombreAfiliadoFamiliar: ApellidoNombreAfiliadoFamiliar,
        CertEstudiosAfiliadoFamiliar: CertEstudiosAfiliadoFamiliar,
        TipoDocAfiliadoFamiliar: TipoDocAfiliadoFamiliar,
        NumDocAfiliadoFamiliar: NumDocAfiliadoFamiliar,
        SexoAfiliadoFamiliar: SexoAfiliadoFamiliar,
        FechaNacAfiliadoFamiliar: FechaNacAfiliadoFamiliar
    });


}

function eliminarFamiliar(NumDocAfiliadoFamiliar) {
    // Eliminar Matriz
    for (i = 0; i < matrizFamiliares.length; i++) {
        if (matrizFamiliares[i].NumDocAfiliadoFamiliar == NumDocAfiliadoFamiliar) {
            matrizFamiliares.splice(i, 1);
        }
    }
}

$(document).on('click', '.borrarFamiliar', function (event) {
    event.preventDefault();
    $(this).closest('tr').remove();
});

$(".btnCodigo").click(function (eve) {
    $("#modal-content").load("/Home/Codigo"); // GET
});

function cerrarId01() {
    document.getElementById('id01').style.display = 'none';
}
