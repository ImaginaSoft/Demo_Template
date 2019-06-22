jQuery(document).ready(function () {



    $("body").on("click", (".pago"), function (e) {
        //var idHotel = jQuery(this).attr('idHotel');

        //$(".boxInfo").css("display", "none");
        var idPedido = $("#IDPedido");
        var monto = $("monto");

        $.ajax({
            url: "/PagoVisa/PagoGG",
            type: "POST",
            data: "idPedido=" + idPedido + "& monto =" + monto,
            success:

                //function (result) {

                //    $(divRooms).html(result);
                //    // waitingDialog.hide();
                //    $(".bloqueo").hide();
                //    $("#cargandoTest-" + idHotel).hide();
                //    $(".tablinks").removeAttr("disabled");
                //},
                
            error:

                //function (jqXHR, textStatus, errorThrown) {
                //    $(".bloqueo").hide();
                //    $("#cargandoTest-" + idHotel).hide();
                //    $(".tablinks").removeAttr("disabled");
                //    waitingDialog.hide();
                //    alert(errorThrown);
                //},
               
            complete:

                //function () {

                //    jQuery('.box-result .tablinks_info').removeClass('active');
                //    jQuery('.box-result .tabcontent').slideUp(200).removeClass('opencontent')

                //    jQuery("#panelOpcionHab-" + idHotel).slideDown(200).addClass('opencontent');



                //}

        })


        //VerOfertas(idHotel);

        //var este = jQuery(".tablinks_info");

        //if (jQuery(este).is('.active')) {
        //    close_accordion_section();
        //} else {
        //    close_accordion_section();



        //$(".boxInfo").removeClass("active");
        e.preventDefault();

    });


});