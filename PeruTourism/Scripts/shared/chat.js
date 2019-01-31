// JavaScript Document

jQuery(function ($) {
    "use strict";
  
    var 
    $chatWin = $(".chat-window"),
    $btnClose = $("#close_chat_window");

    $btnClose.on('click', function() {
        $chatWin.hide();
    });

    if (screen.width <= 450) {
        $(".floating-chat").removeClass("expand");
        $(".cerrarFlotante").css("display", "block");
        $(".floating-chat .chat").removeClass("enter");
        jQuery(".enter").css({ opacity: 1 });
    }
});