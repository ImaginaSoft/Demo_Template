//$(document).ready(function () {
//    $("a").click(function () {
//        alert("The paragraph was clicked.");
//    });
//});


//$(function () {
//    $("#btnGet").click(function () {

//        var dataToSend = 'pNroServicio=' + $("#txtName").val();
//        debugger
//        $.ajax({
//            type: "POST",
//            url: "/perutourism-new/Home/OpenModalDetailsHotel",
//            data: dataToSend,
//            //contentType: "application/json; charset=utf-8",
//            //dataType: "json",
//            success: function (result) {
//                debugger
//                $("#detailsContainer").html(result);
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
//                $('#detalleHotel-1').modal('show');
//            }
//        });
//    });
//});

$(document).on('click', '.panel-heading span.icon_minim', function (e) {
    var $this = $(this);
    if (!$this.hasClass('panel-collapsed')) {
        $this.parents('.panel').find('.panel-body').slideUp();
        $this.addClass('panel-collapsed');
        $this.removeClass('glyphicon-minus').addClass('glyphicon-plus');
    } else {
        $this.parents('.panel').find('.panel-body').slideDown();
        $this.removeClass('panel-collapsed');
        $this.removeClass('glyphicon-plus').addClass('glyphicon-minus');
    }
});
$(document).on('focus', '.panel-footer input.chat_input', function (e) {
    var $this = $(this);
    if ($('#minim_chat_window').hasClass('panel-collapsed')) {
        $this.parents('.panel').find('.panel-body').slideDown();
        $('#minim_chat_window').removeClass('panel-collapsed');
        $('#minim_chat_window').removeClass('glyphicon-plus').addClass('glyphicon-minus');
    }
});
$(document).on('click', '#new_chat', function (e) {
    var size = $(".chat-window:last-child").css("margin-left");
    size_total = parseInt(size) + 400;
    alert(size_total);
    var clone = $("#chat_window_1").clone().appendTo(".container");
    clone.css("margin-left", size_total);
});
$(document).on('click', '.icon_close', function (e) {
    //$(this).parent().parent().parent().parent().remove();
    $("#chat_window_1").remove();
});


function openDetailHotel(detailName) {

    var dataToSend = 'pNroServicio=' + detailName;
    
    $.ajax({
        type: "POST",
        url: "/perutourism-new/Home/OpenModalDetailsHotel",
        data: dataToSend,
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: function (result) {
        
            $("#detailsContainer").html(result);
            //alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
            //alert("Hello: ");
        },
        failure: function (response) {
            
            alert(response.responseText);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            
            alert("Req: " + jqXHR);
            alert("Status: " + textStatus);
            alert("Error: " + errorThrown);
        },
        complete: function () {
            
            $('#detalleHotel-1').modal('show');
        }
    });

}


function openBalanceModal(pCliente, pNroPedido) {
    

    var dataToSend = 'pCliente=' + pCliente + '&pNroPedido=' + pNroPedido;
    
    $.ajax({
        type: "POST",
        url: "/perutourism-new/Home/OpenModalBalance",
        data: dataToSend,
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: function (result) {
            
            $("#balanceContainer").html(result);
            //alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
            //alert("Hello: ");
        },
        failure: function (response) {

            alert(response.responseText);
        },
        error: function (jqXHR, textStatus, errorThrown) {

            alert("Req: " + jqXHR);
            alert("Status: " + textStatus);
            alert("Error: " + errorThrown);
        },
        complete: function () {

            $('#detalleBalance-1').modal('show');
        }
    });

}