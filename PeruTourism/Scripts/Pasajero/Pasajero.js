

$(function () {
	$("#btnEnviar").click(function () {
		$.ajax({
			type: "POST",
			url: "/perutourism-new/Home/RegistrarPasajero",
			data: '{pDesLog: "' + $("#txtNomPasajero").val() + '" , pApe: "' + $("#txtApPasajero").val() + '", pNumP: "' + $("#txtNumPasajero").val() + '", pFecNac: "' + $("#txtFecPasajero").val() + '", pNacionalidad: "' + $("#txtNacPasajero").val() + '", pNroPedido: "' + $("#txtNroPedido").val() + '" }',
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {
				

				//alert("Enviado");
				if (response == "ok") {

					$("#mensajeConfirmacion").show();
				}

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


