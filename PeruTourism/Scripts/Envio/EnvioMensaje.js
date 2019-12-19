$(function () {
    $("#btnSend").click(function () {

        if (!$("#txtMessage").val()) {
            debugger
            $("#mensajeConfirmacion").hide();
            $("#mensajeConfirmacionGG").show();
            
        }
        else {
            debugger
            $.ajax({
                type: "POST",
                url: "/perutourism-new/Home/RegistrarHistorialCliente",
                //data: '{name: "' + $("#txtName").val() + '" }',
                data: '{pDesLog: "' + $("#txtMessage").val() + '" , pCodCliente: "' + $("#txtCodCliente").val() + '", pNroPedido: "' + $("#txtNroPedido").val() + '", pNroPropuesta: "' + $("#txtNroPropuesta").val() + '", pNroVersion: "' + $("#txtNroVersion").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    //alert("Enviado");
                    if (response == "ok") {
                        $("#mensajeConfirmacionGG").hide();
                        $("#mensajeConfirmacion").show();
                    }
                    //else {
                    //    $("#mensajeConfirmacion").hide();
                    //    $("#mensajeConfirmacionGG").show();
                    //}

                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });

        }


	});
});



