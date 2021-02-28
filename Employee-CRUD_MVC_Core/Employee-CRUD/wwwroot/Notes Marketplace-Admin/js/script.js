/*=============================================================
                 Mobile Menu
===============================================================*/
$(function () {

    //Show mobile nav
    $("#mobile-nav-open-btn").click(function () {
        $("#mobile-nav").css("height", "100%");
    });

    //Hide mobile nav
    $("#mobile-nav-close-btn, #mobile-nav a").click(function () {
        $("#mobile-nav").css("height", "0%");
    });
});




$(".toggle-password").click(function() {

  $(this).toggleClass("fa-eye fa-eye-slash");
  var input = $(".show-pass");
  if (input.attr("type") == "password") {
    $(".show-pass").attr("type", "text");
  } else {
    $(".show-pass").attr("type", "password");
  }
});