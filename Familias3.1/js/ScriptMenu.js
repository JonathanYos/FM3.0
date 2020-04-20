$(document).ready(function () {
    $(".menu ul li a").addClass("menu__enlace");
    $(".menu ul ul").addClass("submenu");
    $(".menu ul ul a").removeClass("menu__enlace").addClass("submenu__enlace");


    $(".submenu").hide();
    $(".pnlMenu").hide();

    $(".mostrarMenu").click(function () {
        $(".pnlMenu").animate({
            width: "toggle"

        });
    });

    //$(".submenu").before(innerHTML = "\u25bc");
    $('.submenu')
    //despliega solo el submenu de ese menu concreto
    $('.menu__enlace').click(function (event) {
        var elem = $(this).next();

        if (elem.is('ul')) {
            event.preventDefault();
            elem.slideToggle();
        }
    });
    $('.submenu__enlace').click(function (event) {
        var elem = $(this).next();

        if (elem.is('ul')) {
            event.preventDefault();
            elem.slideToggle();
        }
    });

});//fin de la funcion ready