// JavaScript Document : Peru Tourism - Home

jQuery(function ($) {
  "use strict";

  // parallax
  // --------------------------------------------------------
	var scene = document.getElementById('scene');
	var parallax;
  
  $.runParallax = function() {
    parallax = new Parallax(scene);
  };  
  $.destroyParallax = function() {
      parallax.destroy();
      parallax = null;
  };  
  $.runParallax();
  
  //detectar 'media query' activo (mobile/desktop)
  // --------------------------------------------------------
  var mediaquery = window.matchMedia("(min-width: 768px)");
  
  $.checkParallax = function(m) {
    if (m.matches) {      
      if (!parallax) {
       $.runParallax();
      }
    } else {
      if (parallax) {
        $.destroyParallax();
      }
    }
  };
  $.checkParallax(mediaquery);
  
  
  //detectar redimension de la ventana (debounce)
  // --------------------------------------------------------
  var resizeTimer;
  
  $(window).on('resize', function(e) {
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(function() {
      $.checkParallax(mediaquery);
    }, 250);
  });    
  
  // effecto visual para menu (linea)
  // --------------------------------------------------------
  $("#siteNav").on('show.bs.collapse', function () {
    $("#header").addClass("active");
  });  
  $("#siteNav").on('hidden.bs.collapse', function () {
    $("#header").removeClass("active");
  });  
  
});