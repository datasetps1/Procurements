// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//const indicator = document.querySelector('.nav-indicator');
//const items = document.querySelectorAll('.nav-item');

//function handleIndicator(el) {
//    items.forEach(item => {
//        item.classList.remove('is-active');
//        item.removeAttribute('style');
//    });

//    indicator.style.width = `${el.offsetWidth}px`;
//    indicator.style.left = `${el.offsetLeft}px`;
//    indicator.style.backgroundColor = el.getAttribute('active-color');

//    el.classList.add('is-active');
//    el.style.color = el.getAttribute('active-color');
//}


//items.forEach((item, index) => {
//    item.addEventListener('click', (e) => { handleIndicator(e.target) });
//    item.classList.contains('is-active') && handleIndicator(item);
//});
(function ($) {
    "use strict";

    // Add active state to sidbar nav links
    var path = window.location.href; // because the 'href' property of the DOM element is the absolute path
    $("#layoutSidenav_nav .sb-sidenav a.nav-link").each(function () {
        if (this.href === path) {
            $(this).addClass("active");
        }
    });

    // Toggle the side navigation
    $("#sidebarToggle").on("click", function (e) {
        e.preventDefault();
        $("body").toggleClass("sb-sidenav-toggled");
    });
})(jQuery);
