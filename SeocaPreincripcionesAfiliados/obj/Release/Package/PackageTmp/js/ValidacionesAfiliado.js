
function validarMomentoCuit() {
    var Cuil = document.getElementById("Cuil").value;

    if (Cuil == "") {
        document.getElementById('Cuil').placeholder = 'Ingrese Cuil';
        return false;
    }
    if (Cuil.length != 11) {
        document.getElementById('Cuil').value = "";
        document.getElementById('Cuil').placeholder = 'El CUIL debe de constar de 11 caracteres';
        return false;
    }
    if (isNaN(Cuil)) {
        document.getElementById('Cuil').value = "";
        document.getElementById('Cuil').placeholder = 'Ingrese solo numeros en el campo Cuit';
        return false;
    }
}

function validarMomentoApellido() {
    var Apellido = document.getElementById("Apellido").value;

    if (Apellido == "") {
        document.getElementById('Apellido').placeholder = 'Debe de ingresar un Apellido';
        return false;
    }

    if (!isNaN(Apellido)) {
        document.getElementById('Apellido').placeholder = 'No puede ingresar Numeros';
        return false;
    }
}

function validarMomentoNombre() {
    var Nombre = document.getElementById("Nombre").value;

    if (Nombre == "") {
        document.getElementById('Nombre').placeholder = 'Debe de ingresar un Nombre';
        return false;
    }

    if (!isNaN(Nombre)) {
        document.getElementById('Nombre').placeholder = 'No puede ingresar Numeros';
        return false;
    }
}

function validarMomentoNumDocumento() {
    var NumDoc = document.getElementById("NumDoc").value;
    var n = parseInt(NumDoc);

    if (NumDoc == "") {
        document.getElementById('NumDoc').placeholder = 'Debe de ingresar un Numero Documento';
        return false;
    }
    if (isNaN(NumDoc)) {
        document.getElementById('NumDoc').value = "";
        document.getElementById('NumDoc').placeholder = 'Ingrese solo numeros en el campo Numero Documento';
        return false;
    }
}


function validarMomentoCalle() {
    var Calle = document.getElementById("Calle").value;

    if (Calle == "") {
        document.getElementById('Calle').placeholder = 'Debe de ingresar la Calle';
        return false;
    }
}

function validarMomentoNumCalle() {
    var NumeroCalle = document.getElementById("NumeroCalle").value;

    if (NumeroCalle == "") {
        document.getElementById('NumeroCalle').placeholder = 'Debe de ingresar el N° de Calle';
        return false;
    }
}

function validarMomentoTelefono() {
    var Telefono = document.getElementById("Telefono").value;

    if (Telefono == "") {
        document.getElementById('Telefono').placeholder = 'Debe de ingresar el N° de Telefono';
        return false;
    }
    if (isNaN(Telefono)) {
        document.getElementById('Telefono').value = "";
        document.getElementById('Telefono').placeholder = 'Ingrese solo numeros en el campo Telefono';
        return false;
    }
}

function validarMomentoCelular() {
    var Celular = document.getElementById("Celular").value;

    if (Celular == "") {
        document.getElementById('Celular').placeholder = 'Debe de ingresar el N° de Celular';
        return false;
    }
}


function validarMomentoEmail() {
    var Email = document.getElementById("Email").value;

    if (Email == "") {
        document.getElementById('Email').placeholder = 'Debe de ingresar el Email';
        return false;
    }

    var emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (!emailRegex.test(Email)) {
        document.getElementById('Email').value = "";
        document.getElementById('Email').placeholder = 'Formato de Email no valido';
        return false;
    }
}
