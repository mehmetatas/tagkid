$(document).ready(function(){

	"use strict";


	// Preloader
    $( window ).load(function() {
	    $(".preloader-wrap").fadeOut(4200);
   		$("#preloader").delay(3000).fadeOut(1600);
    });


    
    // Full screen header
	$(function(){

		"use strict";

		var winHeight = $(window).height();
		var winWidth = $(window).width();

		if (winWidth > 979) {
			$('.intro').css({
				'height': winHeight,
			});
			} else{
			$('.intro').css({
				'height': '536px'
			});
		};

		$(window).resize(function(){
			var winHeight = $(window).height();
			var winWidth = $(window).width();

			if (winWidth > 979) {
				$('.intro').css({
					'height': winHeight
				});
				} else{
				$('.intro').css({
					'height': '536px'
				});
			};
		});
	});    
	// Full screen header end








});