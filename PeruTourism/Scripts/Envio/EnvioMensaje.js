//$(function () {
//    $("#btnSend").click(function () {

//        var dataToSend = 'pDesLog=' + $("#txtMessage").val() + '&pCodCliente=' + $("#txtCodCliente").val() + '&pNroPedido=' + $("#txtNroPedido").val() + '&nroPropuesta=' + $("#txtNroPropuesta").val();
//        debugger
//        $.ajax({
//            type: "POST",
//            url: "/perutourism-new/Home/RegistrarHistorialCliente",
//            //data: '{name: "' + $("#txtName").val() + '" }',
//            data: dataToSend,
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (response) {
//                alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
//            },
//            failure: function (response) {
//                alert(response.responseText);
//            },
//            error: function (response) {
//                alert(response.responseText);
//            }
//        });
//    });
//});


$(function () {
    $("#btnSend").click(function () {
        $.ajax({
            type: "POST",
            url: "/perutourism-new/Home/RegistrarHistorialCliente",
            //data: '{name: "' + $("#txtName").val() + '" }',
            data: '{pDesLog: "' + $("#txtMessage").val() + '" , pCodCliente: "' + $("#txtCodCliente").val() + '", pNroPedido: "' + $("#txtNroPedido").val() + '", pNroPropuesta: "' + $("#txtNroPropuesta").val() + '", pNroVersion: "' + $("#txtNroVersion").val() + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });
});

//$(function () {
//    $("#btnSend").click(function () {

//        var dataToSend = 'pDesLog=' + $("#txtMessage").val() + '&pCodCliente=' + $("#txtCodCliente").val() + '&pNroPedido=' + $("#txtNroPedido").val() + '&nroPropuesta=' + $("#txtNroPropuesta").val();
//        debugger
//        $.ajax({
//            type: "POST",
//            url: "/perutourism-new/Home/RegistrarHistorialCliente",
//            data: dataToSend,
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (result) {
//                debugger
//                //$("#detailsContainer").html(result);
//                //alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
//                //alert("Hello: ");
//            },
//            failure: function (response) {
//                debugger
//                alert(response.responseText);
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                debugger
//                alert("Req: " + jqXHR);
//                alert("Status: " + textStatus);
//                alert("Error: " + errorThrown);
//            },
//            complete: function () {
//                debugger
//                //$('#detalleHotel-1').modal('show');
//            }
//        });
//    });
//});











//function EnvioMensajeCliente(detailName) {

//    var dataToSend = 'pNroServicio=' + detailName;

//    $.ajax({
//        type: "POST",
//        url: "/perutourism-new/Home/RegistrarHistorialCliente",
//        data: dataToSend,
//        //contentType: "application/json; charset=utf-8",
//        //dataType: "json",
//        success: function (result) {

//            $("#detailsContainer").html(result);
//            //alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
//            //alert("Hello: ");
//        },
//        failure: function (response) {

//            alert(response.responseText);
//        },
//        error: function (jqXHR, textStatus, errorThrown) {

//            alert("Req: " + jqXHR);
//            alert("Status: " + textStatus);
//            alert("Error: " + errorThrown);
//        },
//        complete: function () {

//            $('#detalleHotel-1').modal('show');
//        }
//    });

//}