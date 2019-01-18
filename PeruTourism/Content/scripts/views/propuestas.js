// JavaScript Document : Peru Tourism - Propuestas

jQuery(function ($) {
  "use strict";
  
//  // parallax
//  // --------------------------------------------------------
//	var scene = document.getElementById('scene');
//	var parallax;
//  
//  $.runParallax = function() {
//    parallax = new Parallax(scene);
//  };  
//  $.destroyParallax = function() {
//      parallax.destroy();
//      parallax = null;
//  };  
//  $.runParallax();
//  
//  //detectar 'media query' activo (mobile/desktop)
//  // --------------------------------------------------------
//  var mediaquery = window.matchMedia("(min-width: 1200px)");
//  
//  $.checkParallax = function(m) {
//    if (m.matches) {      
//      if (!parallax) {
//       $.runParallax();
//      }
//    } else {
//      if (parallax) {
//        $.destroyParallax();
//      }
//    }
//  };
//  $.checkParallax(mediaquery);
  
//detectar 'media query' activo (mobile/desktop)
  // --------------------------------------------------------
  var mediaquery = window.matchMedia("(max-width: 767px)");
  var $galeria = $(".galleryDetalleHotel");
  var $galeriaHTML = $galeria.html();  
  $("#detailsContainer figure").append("<div class='owl-carousel owl-theme'></div>");
  
  var $carrusel = $(".owl-carousel");      
  $carrusel.html($galeriaHTML);
  $carrusel.owlCarousel({
      loop:true,
      margin:0,
      nav:true,
      items: 1,
      autoHeight:true
  });
  $carrusel.hide();  
  
  $.runCarousel = function () {
    $galeria.hide();
    $carrusel.show();    
  };
  
  $.destroyCarousel = function () {
    $carrusel.hide();
    $galeria.show();    
  };  
  
  $.checkCarousel = function(m) {
    if (m.matches) {  
     $.runCarousel();
    } else {
      $.destroyCarousel();
    }
  };
  $.checkCarousel(mediaquery);

  
  // detectar resolucion para mostrar menu
  // --------------------------------------------------------  
//  $.checkMainMenu = function(m) {
//    if (m.matches) {      
//      $("#siteNav").addClass("in");
//    } else {
//      $("#siteNav").removeClass("in");
//    }
//  };  
//  $.checkMainMenu(mediaquery); 
  
  
  //detectar redimension de la ventana (debounce)
  // --------------------------------------------------------
  var resizeTimer;
  
  $(window).on('resize', function(e) {
    clearTimeout(resizeTimer);
      resizeTimer = setTimeout(function() {
      $.checkCarousel(mediaquery);
    }, 250);
  });    
  
  // effecto visual para menu (linea)
  $("#siteNav").on('show.bs.collapse', function () {
    $("#header").addClass("active");
  });  
  $("#siteNav").on('hidden.bs.collapse', function () {
    $("#header").removeClass("active");
  });
  
  
  
  
});