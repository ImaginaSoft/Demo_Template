$(function () {
    limpiar();
    listarPasajeros(0);

    $("#btnEnviar").click(function () {
        var parametros = {};

        parametros["pDesLog"] = $("#txtNomPasajero").val();
        parametros["pApe"] = $("#txtApPasajero").val();
        parametros["pPasaporte"] = $("#txtPasaporte").val();
        parametros["pFecNac"] = $("#txtFecPasajero").val();
        parametros["pNacionalidad"] = $("#cboNacPasajero").val();
        parametros["pGenero"] = $("#cboGenPasajero").val();
        parametros["pTipo"] = $("#cboTipoPasajero").val();
        parametros["pNroPedido"] = $("#txtNroPedido").val();
        parametros["pNumPasajero"] = $("#txtNroPasajero").val();

        if ($("#txtNroPasajero").val() == "0")
            parametros["pAccion"] = "N";
        else
            parametros["pAccion"] = "M";
        
        var pregunta = confirm("¿Desea grabar la información ingresada?");

        if (pregunta)
            registrarPasajero(parametros);
    });

    $("#btnCancelar").click(function () {
        limpiar();
    });
});

function limpiar() {
    $("#txtNomPasajero").val("");
    $("#txtApPasajero").val("");
    $("#txtPasaporte").val("");
    $("#txtFecPasajero").val("");
    $("#cboNacPasajero").val("999");
    $("#cboGenPasajero").val("");
    $("#cboTipoPasajero").val("");
    $("#txtNroPasajero").val("0");

    $("#txtNomPasajero").focus();

    $(".btnAccion").prop("disabled", false);
}

function registrarPasajero(parametros) {
    $.ajax({
        type: "POST",
        url: "/perutourism-new/Home/RegistrarPasajero",
        data: JSON.stringify(parametros),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var rpta = response.indicador;
            var mensaje = response.contenido;

            if (rpta == "OK") {
                limpiar();
                listarPasajeros(0);
                alert(mensaje);
            }
            else
                alert(mensaje);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function listarPasajeros(numero) {
    var parametros = {};
    
    parametros["pNroPedido"] = $("#txtNroPedido").val();
    parametros["pNumPasajero"] = numero;

    $.ajax({
        type: "POST",
        url: "/perutourism-new/Home/ListarPasajeros",
        data: JSON.stringify(parametros),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var rpta = response.indicador;
            var mensaje = response.contenido;
            
            if (rpta == "OK") {
                if (numero == 0)
                    $("#tablaPasajeros").html(mensaje);
                else {
                    var datos = $.parseJSON(mensaje);

                    if (datos.length > 0) {
                        var fila = datos[0];
                        var fecha = formatDate(fila.FormatFchNacimiento);

                        $("#txtNomPasajero").val(fila.NomPasajero);
                        $("#txtApPasajero").val(fila.ApePasajero);
                        $("#txtPasaporte").val(fila.Pasaporte);
                        $("#txtFecPasajero").val(fecha);
                        $("#cboNacPasajero").val(fila.CodNacionalidad);
                        $("#cboGenPasajero").val(fila.CodGenero);
                        $("#cboTipoPasajero").val(fila.TipoPasajero);
                    }
                }
            }
            else
                alert(mensaje);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function editarPasajero(numero) {
    $("#txtNroPasajero").val(numero);
    $(".btnAccion").prop("disabled", true);
    listarPasajeros(numero);
}

function eliminarPasajero(numero) {
    var pregunta = confirm("¿Desea eliminar el pasajero?");

    if (pregunta) {
        var parametros = {};

        parametros["pNroPedido"] = $("#txtNroPedido").val();
        parametros["pNumPasajero"] = numero;
        parametros["pAccion"] = "E";

        registrarPasajero(parametros);
    }
}

function formatDate(date) {
    var d = date.split('/');
    
    var day = d[0],
        month = d[1],
        year = d[2];

    return [year, month, day].join('-');
}