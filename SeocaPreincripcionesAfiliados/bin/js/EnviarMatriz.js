
var matrizFamiliares = new Array();

function changeCampos() {
    var Apellido = document.getElementById("Apellido").value;
    document.getElementById("Apellido").value = Apellido.toUpperCase();

    var Nombre = document.getElementById("Nombre").value;
    document.getElementById("Nombre").value = Nombre.toUpperCase();

    var Email = document.getElementById("Email").value;
    document.getElementById("Email").value = Email.toUpperCase();

    var EmailConfirmacion = document.getElementById("EmailConfirmacion").value;
    document.getElementById("EmailConfirmacion").value = EmailConfirmacion.toUpperCase();

    var Calle = document.getElementById("Calle").value;
    document.getElementById("Calle").value = Calle.toUpperCase();

    var Localidad = document.getElementById("Localidad").value;
    document.getElementById("Localidad").value = Localidad.toUpperCase();

    var ApellidoNombreAfiliadoFamiliar = document.getElementById("ApellidoNombreAfiliadoFamiliar").value;
    document.getElementById("ApellidoNombreAfiliadoFamiliar").value = ApellidoNombreAfiliadoFamiliar.toUpperCase();

    var NombreEmpresaAfiliadoEmpresa = document.getElementById("NombreEmpresaAfiliadoEmpresa").value;
    document.getElementById("NombreEmpresaAfiliadoEmpresa").value = NombreEmpresaAfiliadoEmpresa.toUpperCase();

    var NombreFantasiaAfiliadoEmpresa = document.getElementById("NombreFantasiaAfiliadoEmpresa").value;
    document.getElementById("NombreFantasiaAfiliadoEmpresa").value = NombreFantasiaAfiliadoEmpresa.toUpperCase();

    var OtroRubroAfiliadoEmpresa = document.getElementById("OtroRubroAfiliadoEmpresa").value;
    document.getElementById("OtroRubroAfiliadoEmpresa").value = OtroRubroAfiliadoEmpresa.toUpperCase();

    var CalleAfiliadoEmpresa = document.getElementById("CalleAfiliadoEmpresa").value;
    document.getElementById("CalleAfiliadoEmpresa").value = CalleAfiliadoEmpresa.toUpperCase();

    var EmailAfiliadoEmpresa = document.getElementById("EmailAfiliadoEmpresa").value;
    document.getElementById("EmailAfiliadoEmpresa").value = EmailAfiliadoEmpresa.toUpperCase();
}

function enviar() {
    var matrizEmpresa = new Array();
    var matrizDatosAfiliado = new Array();

    var Apellido = document.getElementById("Apellido").value;
    var Nombre = document.getElementById("Nombre").value;
    var Cuil = document.getElementById("Cuil").value;
    var NumDoc = document.getElementById("NumDoc").value;
    var Delegacion = document.getElementById("Delegacion").value;
    var EstadoCivil = document.getElementById("EstadoCivil").value;
    var FechaNac = document.getElementById("FechaNac").value;
    var Calle = document.getElementById("Calle").value;
    var NumeroCalle = document.getElementById("NumeroCalle").value;
    var Piso = document.getElementById("Piso").value;
    var Dto = document.getElementById("Dto").value;
    var Telefono = document.getElementById("Telefono").value;
    var Celular = document.getElementById("Celular").value;
    var Email = document.getElementById("Email").value;
    var EmailConfirmacion = document.getElementById("EmailConfirmacion").value;
    var Localidad = document.getElementById("Localidad").value;
    var CP = document.getElementById("CP").value;
    var Provincia = document.getElementById("Provincia").value;
    var SexoAfiliadoDocumento = document.getElementById("SexoAfiliadoDocumento").value;
    var Nacionalidad = document.getElementById("Nacionalidad").value;

    var FechaIngresoAfiliadoEmpresa = document.getElementById("FechaIngresoAfiliadoEmpresa").value;
    var NombreEmpresaAfiliadoEmpresa = document.getElementById("NombreEmpresaAfiliadoEmpresa").value;
    var NombreFantasiaAfiliadoEmpresa = document.getElementById("NombreFantasiaAfiliadoEmpresa").value;
    var CUITEmpresaAfiliadoEmpresa = document.getElementById("CUITEmpresaAfiliadoEmpresa").value;
    var CalleAfiliadoEmpresa = document.getElementById("CalleAfiliadoEmpresa").value;
    var RubroAfiliadoEmpresa = document.getElementById("RubroAfiliadoEmpresa").value;
    var OtroRubroAfiliadoEmpresa = document.getElementById("OtroRubroAfiliadoEmpresa").value;
    var NumeroAfiliadoEmpresa = document.getElementById("NumeroAfiliadoEmpresa").value;
    var PisoAfiliadoEmpresa = document.getElementById("PisoAfiliadoEmpresa").value;
    var DtoAfiliadoEmpresa = document.getElementById("DtoAfiliadoEmpresa").value;
    var LocalidadAfiliadoEmpresa = document.getElementById("LocalidadAfiliadoEmpresa").value;
    var TelefonoAfiliadoEmpresa = document.getElementById("TelefonoAfiliadoEmpresa").value;
    var EmailAfiliadoEmpresa = document.getElementById("EmailAfiliadoEmpresa").value;

    var fileDNIFrente = document.getElementById("fileDNIFrente").value;
    var fileDNIDorso = document.getElementById("fileDNIDorso").value;
    var fileReciboSueldo = document.getElementById("fileReciboSueldo").value;
/*    var fileNotaSolicitud = document.getElementById("fileNS").value;*/

    var check = document.getElementById("checkConstancia").checked;

    if (check == false) {
        alert("Debe de activar la casilla de Incorporacion de afiliado");
        return false;
    }

    // verificar elementos matriz familiar
    for (i = 0; i < matrizFamiliares.length; i++) {
        var fileFamiliar = document.getElementById("fileFamiliar" + matrizFamiliares[i].FileFamiliar).value;
        var fileFamiliarDos = document.getElementById("fileFamiliarDos" + matrizFamiliares[i].FileFamiliarDos).value;

        if (fileFamiliar == "" && fileFamiliarDos == "") {
            alert("Debe de ingresar al menos un archivo en el familiar: " + matrizFamiliares[i].ApellidoNombreAfiliadoFamiliar);
            return false;
        }
    }

    // Validaciones
    if (!isNaN(Apellido)) {
        alert("No puede ingresar numeros en Apellido");
        return false;
    }
    if (!isNaN(Nombre)) {
        alert("No puede ingresar numeros en Nombre");
        return false;
    }
    if (Cuil.length != 13) {
        alert("El CUIL debe de constar de 11 caracteres");
        return false;
    }

    var cadena1 = Cuil.slice(0, 2);
    var cadena2 = Cuil.slice(3, 11);
    var cadena3 = Cuil.slice(12, 13);
    Cuil = cadena1 + cadena2 + cadena3;

    if (isNaN(Cuil)) {
        alert("Ingrese solo numeros en el campo Cuit");
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
    if (EstadoCivil == 0) {
        alert("Debe de seleccionar un Estado Civil");
        return false;
    }
    if (SexoAfiliadoDocumento == 0) {
        alert("Debe de seleccionar un Genero");
        return false;
    }
    if (FechaNac == "") {
        alert("Debe de ingresar la Fecha de Nacimiento");
        return false;
    }
    if (Localidad == "") {
        alert("Debe de ingresar la Localidad del afiliado");
        return false;
    }
    if (isNaN(CP)) {
        alert("Ingrese solo numeros en el campo Codigo Postal");
        return false;
    }
    if (CP < 0) {
        alert("Ingrese solo numeros positivos");
        return false;
    }
    if (Provincia == 0) {
        alert("Debe de seleccionar una Provincia");
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
    
    if (EmailConfirmacion == "") {
        alert("Debe de ingresar el Email");
        return false;
    }
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (!emailRegex.test(EmailConfirmacion)) {
        alert("Formato de Email no valido");
        return false;
    }

    if (EmailConfirmacion != Email) {
        alert("Los Mails de Confirmacion no coinciden");
        return false;
    }

    //-------------------------------------------------------------------//

    if (fileDNIFrente == "") {
        alert("Debe de ingresar el archivo de Frente DNI");
        return false;
    }
    if (fileDNIDorso == "") {
        alert("Debe de ingresar el archivo de Dorso DNI");
        return false;
    }
    if (fileReciboSueldo == "") {
        alert("Debe de ingresar el archivo de Recibo Sueldo");
        return false;
    }
    //if (fileNotaSolicitud == "") {
    //    alert("Debe de ingresar el archivo de Nota de Solicitud");
    //    return false;
    //}


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
    if (CUITEmpresaAfiliadoEmpresa.length != 13) {
        alert("El CUIT de la empresa debe de constar de 11 caracteres");
        return false;
    }

    var cadena4 = CUITEmpresaAfiliadoEmpresa.slice(0, 2);
    var cadena5 = CUITEmpresaAfiliadoEmpresa.slice(3, 11);
    var cadena6 = CUITEmpresaAfiliadoEmpresa.slice(12, 13);
    CUITEmpresaAfiliadoEmpresa = cadena4 + cadena5 + cadena6;
    if (isNaN(CUITEmpresaAfiliadoEmpresa)) {
        alert("Ingrese solo numeros en el campo Cuit");
        return false;
    }

    if (CalleAfiliadoEmpresa == "") {
        alert("Debe de ingresar la Calle de la Empresa (Empresa)");
        return false;
    }

    if (RubroAfiliadoEmpresa == "0") {
        alert("Debe de Elegir una Actividad");
        return false;
    }


    if (RubroAfiliadoEmpresa == "Otro") {
        if (OtroRubroAfiliadoEmpresa == "") {
            alert("Debe de especificar el nombre del Rubro si eligio Otro");
            return false;
        }
    }

    if (NumeroAfiliadoEmpresa == "") {
        alert("Debe de ingresar el N° de Calle de la Empresa (Empresa)");
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
        NumDoc: NumDoc,
        Delegacion: Delegacion,
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
        Email: Email,
        Celular: Celular,
        CP: CP
    });

    matrizEmpresa.push({
        FechaIngresoAfiliadoEmpresa: FechaIngresoAfiliadoEmpresa,
        NombreEmpresaAfiliadoEmpresa: NombreEmpresaAfiliadoEmpresa,
        NombreFantasiaAfiliadoEmpresa: NombreFantasiaAfiliadoEmpresa,
        CUITEmpresaAfiliadoEmpresa: CUITEmpresaAfiliadoEmpresa,
        CalleAfiliadoEmpresa: CalleAfiliadoEmpresa,
        RubroAfiliadoEmpresa: RubroAfiliadoEmpresa,
        OtroRubroAfiliadoEmpresa: OtroRubroAfiliadoEmpresa,
        NumeroAfiliadoEmpresa: NumeroAfiliadoEmpresa,
        PisoAfiliadoEmpresa: PisoAfiliadoEmpresa,
        DtoAfiliadoEmpresa: DtoAfiliadoEmpresa,
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
    var NumDocAfiliadoFamiliar = document.getElementById("NumDocAfiliadoFamiliar").value;
    var SexoAfiliadoFamiliar = document.getElementById("SexoAfiliadoFamiliar").value; // select
    var FechaNacAfiliadoFamiliar = document.getElementById("FechaNacAfiliadoFamiliar").value;

    var e = document.getElementById("Parentesco");
    var text = e.options[e.selectedIndex].text;

    //Validaciones
    if (Parentesco == 0) {
        alert("Debe de ingresar el Parentesco");
        return false;
    }
    if (ApellidoNombreAfiliadoFamiliar == "") {
        alert("Debe de ingresar el Nombre y Apellido del familiar");
        return false;
    }
    if (SexoAfiliadoFamiliar == 0) {
        alert("Debe de ingresar un Genero de Familiar");
        return false;
    }
    if (!isNaN(ApellidoNombreAfiliadoFamiliar)) {
        alert("No puede ingresar numeros en Nombre y Apellido (Familiar)");
        return false;
    }
    if (NumDocAfiliadoFamiliar == "") {
        alert("Debe de ingresar el Numero de Documento");
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

    if ((matrizFamiliares.length + 1) > 10) {
        alert("Puede ingresar hasta un maximo de 10 familiares");
        return false;
    }

    var valorActual = 1;
    var valorActualDos = 1;
    for (a = 1; a <= 10; a++) {
        for (b = 0; b < matrizFamiliares.length; b++) {
            if (matrizFamiliares[b].FileFamiliar == a || matrizFamiliares[b].FileFamiliarDos == a) {
                valorActual = 1;
                valorActualDos = 1;
                break;
            } else {
                valorActual = a;
                valorActualDos = a;
            }
        }
        if (valorActual != 1 && valorActualDos != 1) {
            break;
        }
    }

    var table = document.getElementById('bodyFamiliar');
    var x = table.insertRow(0);
    var e = table.rows.length - 1;
    var l = table.rows[e].cells.length;

    //x.innerHTML = "&nbsp;";
    table.rows[0].insertCell(0);
    table.rows[0].cells[0].innerHTML = text;
    table.rows[0].insertCell(1);
    table.rows[0].cells[1].innerHTML = ApellidoNombreAfiliadoFamiliar;
    table.rows[0].insertCell(2);
    table.rows[0].cells[2].innerHTML = NumDocAfiliadoFamiliar;
    table.rows[0].insertCell(3);
    table.rows[0].cells[3].innerHTML = '<div class="input-group">' + 
                                            '<input type = "text" class="form-control" id = "textFamiliar' + valorActual + '" placeholder = "No hay ningún archivo seleccionado..." disabled = "" >' +
                                            '<span class="input-group-btn">' + 
                                                '<label class="btn btn-success" type="button">' +
                                                    'Seleccionar<input id="fileFamiliar' + valorActual + '" name="fileFamiliar' + valorActual + '" type="file" data-size-max="10" style="display:none;" accept="image/png,image/jpeg,application/pdf" onchange="fileFamiliar(' + valorActual + ');">'+
                                                '</label>'+
                                            '</span>'+
                                        '</div>';
    table.rows[0].insertCell(4);
    table.rows[0].cells[4].innerHTML = '<div class="input-group">' +
                                            '<input type = "text" class="form-control" id = "textFamiliarDos' + valorActual + '" placeholder = "No hay ningún archivo seleccionado..." disabled = "" >' +
                                            '<span class="input-group-btn">' +
                                                '<label class="btn btn-success" type="button">' +
                                                'Seleccionar<input id="fileFamiliarDos' + valorActual + '" name="fileFamiliarDos' + valorActual + '" type="file" data-size-max="10" style="display:none;" accept="image/png,image/jpeg,application/pdf" onchange="fileFamiliarDos(' + valorActualDos + ');">' +
                                            '</label>' +
                                            '</span>' +
                                        '</div>';
    table.rows[0].insertCell(5);
    table.rows[0].cells[5].innerHTML = '<button class="btn btn-danger borrarFamiliar" type="button"  onclick="eliminarFamiliar(' + "'" + NumDocAfiliadoFamiliar + "'" + ')">Eliminar Familiar</button >';

    document.getElementById("Parentesco").value = "0"; // select
    document.getElementById("ApellidoNombreAfiliadoFamiliar").value = "";
    document.getElementById("NumDocAfiliadoFamiliar").value = "";
    document.getElementById("SexoAfiliadoFamiliar").value = "0"; // select
    document.getElementById("FechaNacAfiliadoFamiliar").value = "";

    matrizFamiliares.push({
        Parentesco: Parentesco,
        ApellidoNombreAfiliadoFamiliar: ApellidoNombreAfiliadoFamiliar,
        NumDocAfiliadoFamiliar: NumDocAfiliadoFamiliar,
        SexoAfiliadoFamiliar: SexoAfiliadoFamiliar,
        FechaNacAfiliadoFamiliar: FechaNacAfiliadoFamiliar,
        FileFamiliar: valorActual,
        FileFamiliarDos: valorActualDos,
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

function fileFamiliar(idFamiliar) {
    const fileList = event.target.files;
    document.getElementById("textFamiliar" + idFamiliar).value = fileList[0].name;
}

function fileFamiliarDos(idFamiliar) {
    const fileList = event.target.files;
    document.getElementById("textFamiliarDos" + idFamiliar).value = fileList[0].name;
}

// -------------------------------------------------------------- //

function filePerfilDNI() {
    const fileList = event.target.files;
    document.getElementById("textFotoDNI").value = fileList[0].name;
}

function fileDorsoDNI() {
    const fileList = event.target.files;
    document.getElementById("textDorsoDNI").value = fileList[0].name;
}

function fileReciboSueldos() {
    const fileList = event.target.files;
    document.getElementById("textReciboSueldo").value = fileList[0].name;
}

function fileFotoPerfil() {
    const fileList = event.target.files;
    document.getElementById("textFotoPerfil").value = fileList[0].name;
}

//function fileNotaSolicitud() {
//    const fileList = event.target.files;
//    document.getElementById("textNotaSolicitud").value = fileList[0].name;
//}

function valiCuil() {
    var cuit = document.getElementById("Cuil").value;
    var cadena1 = "";
    var cadena2 = "";
    var cadena3 = "";

    if (cuit != "") {
        if (cuit.length == 11) {
            cadena1 = cuit.slice(0, 2);
            cadena2 = cuit.slice(2, 10);
            cadena3 = cuit.slice(10, 11);
        } else if (cuit.length == 13) {
            cadena1 = cuit.slice(0, 2);
            cadena2 = cuit.slice(3, 11);
            cadena3 = cuit.slice(12, 13);
        } else {
            document.getElementById("Cuil").value = "";
            alert("Este cuit es invalido");
        }

        //----------------------------------------------------//
        var rv = false;
        var verificador;
        var resultado = 0;
        var cuit_nro = cadena1 + cadena2 + cadena3;
        cuitnro = parseInt(cuit_nro);
        var codes = "6789456789";

        verificador = cadena3;
        var x = 0;
        while (x < 10) {
            var digitovalidador = codes.slice(x, x + 1); // recorrer digito x digito de codes 
            var digito = cuit_nro.slice(x, x + 1); // recorrer digito x digito de CUIT INGRESADO

            digitovalidador = parseInt(digitovalidador);
            digito = parseInt(digito);

            var digitoValidacion = digitovalidador * digito;
            resultado += digitoValidacion;
            x = x + 1;
        }
        resultado = resultado % 11;

        if (resultado == verificador) {
            var cuit = cadena1 + "-" + cadena2 + "-" + cadena3;
            document.getElementById("Cuil").value = cuit;
            document.getElementById("NumDoc").value = cadena2;
        } else {
            document.getElementById("Cuil").value = "";
            alert("Este cuit es invalido");
        }
        
    } else {
        document.getElementById("NumDoc").value = "";
    }
}

const empresa = {
    razonSocial: "",
    nombreFantasia: ""
}

function valiCuit() {
    var cuit = document.getElementById("CUITEmpresaAfiliadoEmpresa").value;
    var cadena1 = "";
    var cadena2 = "";
    var cadena3 = "";


    if (cuit != "") {
        if (cuit.length == 11) {
            cadena1 = cuit.slice(0, 2);
            cadena2 = cuit.slice(2, 10);
            cadena3 = cuit.slice(10, 11);
        } else if (cuit.length == 13) {
            cadena1 = cuit.slice(0, 2);
            cadena2 = cuit.slice(3, 11);
            cadena3 = cuit.slice(12, 13);
        } else {
            document.getElementById("CUITEmpresaAfiliadoEmpresa").value = "";
            alert("Este cuit es invalido");
        }

        //----------------------------------------------------//
        var rv = false;
        var verificador;
        var resultado = 0;
        var cuit_nro = cadena1 + cadena2 + cadena3;
        cuitnro = parseInt(cuit_nro);
        var codes = "6789456789";

        verificador = cadena3;
        var x = 0;
        while (x < 10) {
            var digitovalidador = codes.slice(x, x + 1); // recorrer digito x digito de codes 
            var digito = cuit_nro.slice(x, x + 1); // recorrer digito x digito de CUIT INGRESADO

            digitovalidador = parseInt(digitovalidador);
            digito = parseInt(digito);

            var digitoValidacion = digitovalidador * digito;
            resultado += digitoValidacion;
            x = x + 1;
        }
        resultado = resultado % 11;
        if (resultado == verificador) {
            var cuit = cadena1 + "-" + cadena2 + "-" + cadena3;
            document.getElementById("CUITEmpresaAfiliadoEmpresa").value = cuit;

            // Busqueda de RazonSocial 

            var formdata = new FormData(); //FormData object
            formdata.append('cuit', cuit);
            //Creating an XMLHttpRequest and sending
            //var xhr = new XMLHttpRequest();
            //xhr.open('POST', '/Home/ObtenerRazonSocial');
            //xhr.send(formdata);
            //xhr.onreadystatechange = function (response) {
            //    if (xhr.readyState == 4 && xhr.status == 200) {
            //        alert("bien")
            //    } else {
            //        alert("mal")
            //    }
            //}


            $.ajax({
                url: '/Home/ObtenerRazonSocial',
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.razonSocial != "") {
                        document.getElementById("NombreEmpresaAfiliadoEmpresa").value = response.razonSocial;
                        document.getElementById("NombreFantasiaAfiliadoEmpresa").value = response.nombreFantasia;
                        document.getElementById("RubroAfiliadoEmpresa").value = response.actividad;
                    }
                },
                error: function (response) {
                    alert(response.responseText);
                },
                data: {
                    cuit: cuit
                }
            });

        } else {
            document.getElementById("CUITEmpresaAfiliadoEmpresa").value = "";
            document.getElementById("NombreEmpresaAfiliadoEmpresa").value = "";
            document.getElementById("NombreFantasiaAfiliadoEmpresa").value = "";
            document.getElementById("RubroAfiliadoEmpresa").value = "0";
            alert("Este cuit es invalido");
        }
    } else {
        document.getElementById("NombreEmpresaAfiliadoEmpresa").value = "";
        document.getElementById("NombreFantasiaAfiliadoEmpresa").value = "";
        document.getElementById("RubroAfiliadoEmpresa").value = "0";
    }
}


//------------------------------------------------------------//


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
