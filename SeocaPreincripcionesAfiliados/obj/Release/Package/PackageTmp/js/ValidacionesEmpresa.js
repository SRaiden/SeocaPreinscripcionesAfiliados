function validarMomentoNombreEmpresa() {
    var NombreEmpresaAfiliadoEmpresa = document.getElementById("NombreEmpresaAfiliadoEmpresa").value;

    if (NombreEmpresaAfiliadoEmpresa == "") {
        document.getElementById('NombreEmpresaAfiliadoEmpresa').placeholder = 'Debe de ingresar el Nombre de Empresa (Empresa)';
        return false;
    }

    if (!isNaN(NombreEmpresaAfiliadoEmpresa)) {
        document.getElementById('NombreEmpresaAfiliadoEmpresa').value = "";
        document.getElementById('NombreEmpresaAfiliadoEmpresa').placeholder = 'No puede ingresar numeros en Nombre Empresa)';
        return false;
    }
}

function validarMomentoCuitEmpresa() {
    var CUITEmpresaAfiliadoEmpresa = document.getElementById("CUITEmpresaAfiliadoEmpresa").value;

    if (CUITEmpresaAfiliadoEmpresa == "") {
        document.getElementById('CUITEmpresaAfiliadoEmpresa').placeholder = 'Debe de ingresar el Cuilt de Empresa (Empresa)';
        return false;
    }
    if (isNaN(CUITEmpresaAfiliadoEmpresa)) {
        document.getElementById('CUITEmpresaAfiliadoEmpresa').value = "";
        document.getElementById('CUITEmpresaAfiliadoEmpresa').placeholder = 'Ingrese solo numeros en el campo Cuit (Empresa)';
        return false;
    }
    if (CUITEmpresaAfiliadoEmpresa.length != 11) {
        document.getElementById('CUITEmpresaAfiliadoEmpresa').value = "";
        document.getElementById('CUITEmpresaAfiliadoEmpresa').placeholder = 'El CUIT de la empresa debe de constar de 11 caracteres (Empresa)';
        return false;
    }
}

function validarMomentoCalleAfiliado() {
    var CalleAfiliadoEmpresa = document.getElementById("CalleAfiliadoEmpresa").value;

    if (CalleAfiliadoEmpresa == "") {
        document.getElementById('CalleAfiliadoEmpresa').placeholder = 'Debe de ingresar la Calle de la Empresa (Empresa)';
        return false;
    }
}

function validarMomentoNumeroAfiliado() {
    var NumeroAfiliadoEmpresa = document.getElementById("NumeroAfiliadoEmpresa").value;

    if (NumeroAfiliadoEmpresa == "") {
        document.getElementById('NumeroAfiliadoEmpresa').placeholder = 'Debe de ingresar el N° de Calle de la Empresa (Empresa)';
        return false;
    }
}


function validarMomentoCPAfiliado() {
    var CPAfiliadoEmpresa = document.getElementById("CPAfiliadoEmpresa").value;

    if (CPAfiliadoEmpresa == 0) {
        document.getElementById('CPAfiliadoEmpresa').placeholder = 'Debe de seleccionar el Codigo Postal (Empresa)';
        return false;
    }
}

function validarMomentoTelefonoAfiliado() {
    var TelefonoAfiliadoEmpresa = document.getElementById("TelefonoAfiliadoEmpresa").value;

    if (TelefonoAfiliadoEmpresa == "") {
        document.getElementById('TelefonoAfiliadoEmpresa').placeholder = 'Debe de ingresar el Telefono (Empresa)';
        return false;
    }
}

function validarMomentoEmailAfiliado() {
    var EmailAfiliadoEmpresa = document.getElementById("EmailAfiliadoEmpresa").value;

    if (EmailAfiliadoEmpresa == "") {
        document.getElementById('EmailAfiliadoEmpresa').placeholder = 'Debe de ingresar el Email (Empresa)';
        return false;
    }

    var emailRegexD = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (!emailRegexD.test(EmailAfiliadoEmpresa)) {
        document.getElementById('EmailAfiliadoEmpresa').value = "";
        document.getElementById('EmailAfiliadoEmpresa').placeholder = 'Formato de Email no valido (Empresa)';
        return false;
    }
}