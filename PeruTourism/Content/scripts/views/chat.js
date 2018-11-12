// JavaScript Document

jQuery(function ($) {
  "use strict";
  
  var 
    $chatWin = $(".chat-window"),
    $btnClose = $("#close_chat_window");

  $btnClose.on('click', function() {
      $chatWin.hide();
  });
  
});