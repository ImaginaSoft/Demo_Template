

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
				debugger
				alert("Enviado");
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



