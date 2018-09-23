// JavaScript Document : Peru Tourism - Propuestas

jQuery(function ($) {
  "use strict";
  
  // parallax
	var scene = document.getElementById('scene');
	var parallax = new Parallax(scene);
  
  // effecto visual para menu (linea)
  $("#siteNav").on('show.bs.collapse', function () {
    $("#header").addClass("active");
  });  
  $("#siteNav").on('hidden.bs.collapse', function () {
    $("#header").removeClass("active");
  });  
  
});