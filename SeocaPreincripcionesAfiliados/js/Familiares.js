

function insertarFamiliar() {
    var Parentesco = document.getElementById("Parentesco").value; // select
    var ApellidoNombreAfiliadoFamiliar = document.getElementById("ApellidoNombreAfiliadoFamiliar").value;
    var CertEstudiosAfiliadoFamiliar = document.getElementById("CertEstudiosAfiliadoFamiliar").value;
    var TipoDocAfiliadoFamiliar = document.getElementById("TipoDocAfiliadoFamiliar").value; // select
    var NumDocAfiliadoFamiliar = document.getElementById("NumDocAfiliadoFamiliar").value; 
    var SexoAfiliadoFamiliar = document.getElementById("SexoAfiliadoFamiliar").value; // select
    var FechaNacAfiliadoFamiliar = document.getElementById("FechaNacAfiliadoFamiliar").value;
    var VencioAfiliadoFamiliar = document.getElementById("VencioAfiliadoFamiliar").value;
    var ActualizacionAfiliadoFamiliar = document.getElementById("ActualizacionAfiliadoFamiliar").value;

    //validaciones
    if (ApellidoNombreAfiliadoFamiliar == "") {
        alert("Debe de ingresar el Nombre y Apellido del familiar");
        return false;
    }

    if (CertEstudiosAfiliadoFamiliar == "") {
        alert("Ingrese el certificado de Estudios");
        return false;
    }

    if (TipoDocAfiliadoFamiliar == "") {
        alert("Debe de ingresar el Tipo de Documento");
        return false;
    }

    if (NumDocAfiliadoFamiliar == "") {
        alert("Debe de ingresar el numero de Documento");
        return false;
    }

    if (SexoAfiliadoFamiliar == "") {
        alert("Debe de ingresar el Sexo del familiar");
        return false;
    }

    if (Parentesco == "") {
        alert("Debe de ingresar el Parentesco");
        return false;
    }

    if (FechaNacAfiliadoFamiliar == "") {
        alert("Debe de ingresar la Fecha de Nacimiento del familiar");
        return false;
    }


    for (i = 0; i < matrizFamiliares.length; i++) {
        if (matrizFamiliares[i].NumDocAfiliadoFamiliar == NumDocAfiliadoFamiliar) {
            alert("Este DNI ya fue cargado");
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
    table.rows[0].cells[3].innerHTML = '<button class="w3-button w3-card bg-zul w3-text-white w3-hover-blue w3-hover-border-cyan" type="button"  onclick="eliminarFamiliar(' + "'" + NumDocAfiliadoFamiliar + "'" + ')">Eliminar</button >';

    document.getElementById("Parentesco").value = "0"; // select
    document.getElementById("ApellidoNombreAfiliadoFamiliar").value = "";
    document.getElementById("CertEstudiosAfiliadoFamiliar").value = "";
    document.getElementById("TipoDocAfiliadoFamiliar").value = "0"; // select
    document.getElementById("NumDocAfiliadoFamiliar").value = "";
    document.getElementById("SexoAfiliadoFamiliar").value = "0"; // select
    document.getElementById("FechaNacAfiliadoFamiliar").value = "";
    document.getElementById("VencioAfiliadoFamiliar").value = ""; 
    document.getElementById("ActualizacionAfiliadoFamiliar").value = "";

    matrizFamiliares.push({
        Parentesco: Parentesco,
        ApellidoNombreAfiliadoFamiliar: ApellidoNombreAfiliadoFamiliar,
        CertEstudiosAfiliadoFamiliar: CertEstudiosAfiliadoFamiliar,
        TipoDocAfiliadoFamiliar: TipoDocAfiliadoFamiliar,
        NumDocAfiliadoFamiliar: NumDocAfiliadoFamiliar,
        SexoAfiliadoFamiliar: SexoAfiliadoFamiliar,
        FechaNacAfiliadoFamiliar: FechaNacAfiliadoFamiliar,
        VencioAfiliadoFamiliar: VencioAfiliadoFamiliar,
        ActualizacionAfiliadoFamiliar: ActualizacionAfiliadoFamiliar
    });

}

function eliminarFamiliar(NumDocAfiliadoFamiliar) {
    // Eliminar Fila
    var resume_table = document.getElementById("bodyFamiliar");

    for (var i = 0, row; row = resume_table.rows[i]; i++) {
        for (var j = 0, col; col = row.cells[j]; j++) {
            var valor = ${ col.innerText };

            //if ( == NombreEstudioContador) {
            //    document.getElementById("bodyContador").deleteRow(r);
            //}
        }

    }

    // Eliminar Matriz
    for (i = 0; i < matrizFamiliares.length; i++) {
        if (matrizFamiliares[i].NumDocAfiliadoFamiliar == NumDocAfiliadoFamiliar) {
            matrizFamiliares.splice(i, 1);
        }
    }
}