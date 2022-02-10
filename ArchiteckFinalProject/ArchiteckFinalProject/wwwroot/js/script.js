$(document).ready(function() {
    //Video 
    $('.video-play-btn').magnificPopup({
        disabledOn: 320,
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        fixedContentPos: false
    });
    $.extend(true, $.magnificPopup.defaults, {
        iframe: {
            patterns: {
                youtube: {
                    index: 'youtube.com/',
                    id: 'v=',
                    src: 'http://www.youtube.com/embed/%id%?autoplay=1'
                }
            }
        }
    });
    //Video End
    AOS.init({
        duration: 500,
        easing: 'ease-in-out',

    });

    //Client-Carousel
    $('.said-body').owlCarousel({
        items: 2,
        margin: 20,
        loop: true,
        responsiveClass: true,
        autoplay: true,
        autoplayTimeout: 3000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 1,
            },
            1000: {
                items: 2,
            },
        }

    });
    //Client-Carousel End

    //Project-Carousel 
    $('.project-body').owlCarousel({
        items: 4,
        margin: 30,
        loop: true,
        responsiveClass: true,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        responsive: {

            0: {
                items: 1,
            },
            768: {
                items: 2,
            },
            1200: {
                items: 4,
            }
        }

    });
    //Project-Carousel End

    //Team-Carousel 
    $('.persons').owlCarousel({
        items: 4,
        margin: 30,
        loop: true,
        responsiveClass: true,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1,
            },
            768: {
                items: 2,
            },
            1199: {
                items: 4,
            },
        }

    });
    //Team-Carousel End

    //Sponsor-Carousel 
    $('.client-items').owlCarousel({
        items: 6,
        margin: 10,
        loop: true,
        responsiveClass: true,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1,
            },
            1020: {
                items: 3,
            },
            1199: {
                items: 6,
            },
        }

    });
    //Sponsor-Carousel End

    $('.custom-slider').owlCarousel({
        items: 1,
        // margin: 10,
        loop: true,
        responsiveClass: true,
        autoplay: true,
        autoplayTimeout: 4000,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 1,
            },
            1000: {
                items: 1,
            },
        }

    });

    $('.selectTwo').select2();

    ClassicEditor
        .create(document.querySelector('#editor'))
        .catch(error => {
            console.error(error);
        });


    let odospan = document.querySelectorAll(".odospan");

    for (let i = 0; i < odospan.length; i++) {

        if (i == 0) {
            odospan[i].innerText = "3200";
        } else if (i == 1) {
            odospan[i].innerText = "5000";
        } else if (i == 2) {
            odospan[i].innerText = "2500";
        } else {
            odospan[i].innerText = "2800";
        }
    }
    let odospan2 = document.querySelectorAll(".odospan2");

    for (let i = 0; i < odospan2.length; i++) {

        if (i == 0) {
            odospan2[i].innerText = "95%";
        } else if (i == 1) {
            odospan2[i].innerText = "90%";
        } else if (i == 2) {
            odospan2[i].innerText = "80%";
        } else {
            odospan2[i].innerText = "85%";
        }
    }


    //GoUpBtn
    let goUpbtn = $(".circle")[0];

    var offset = 400;
    var duration = 300;
    if (goUpbtn != null || goUpbtn != undefined) {
        $(window).scroll(function() {
            if ($(this).scrollTop() > offset) {
                $('.circle')[0].style.bottom = "30px";
            } else {
                $('.circle')[0].style.bottom = "-50px";
            }
        });

        $('.circle').click(function(event) {
            event.preventDefault();
            $('html, body').animate({ scrollTop: 0 }, duration);
            return false;
        })
    }
    //GoUpBtn End


});
//Search
let search = document.querySelector(".header-search");
let box = document.querySelector(".search-box");
let xSearch = document.querySelector(".x-search");
let loopSearch = document.querySelector(".loop-search");
let activeBox = document.querySelector(".active-box");

if (search != null || search != undefined) {
    search.addEventListener("click", function(e) {
        e.preventDefault();

        box.classList.toggle("active-box");
        if (box.matches(".active-box")) {
            loopSearch.style.fontSize = "0";
            xSearch.style.fontSize = "20px";
        } else {
            xSearch.style.fontSize = "0";
            loopSearch.style.fontSize = "20px";
        }
    });
}
//Search End
//Navbar
let customNav = document.querySelector(".custom-navs");
let topHeader = document.querySelector(".top-header");
let logo = document.querySelector(".logo");


window.addEventListener("scroll", () => {
        if (window.pageYOffset > 200) {
            customNav.style.position = "fixed";
            customNav.style.right = "0";
            customNav.style.left = "0";
            customNav.style.zIndex = "999"
            customNav.classList.add("active");
            topHeader.style.display = "none";
            logo.style.padding = "6px 30px"

        } else {
            customNav.style.position = "relative";
            customNav.style.transition = "all 0.3s linear";
            topHeader.style.display = "block";
            logo.style.padding = "28px 30px"
            customNav.classList.remove("active");

        }
    })
    //Navbar end

let resLines = document.querySelector(".res-menu-bar-lines");
let linedMenu = document.querySelector(".nav-menu-ul");
let spansForLined = document.querySelector(".res-menu-bar-lines");

resLines.addEventListener("click", function(e) {
    linedMenu.classList.toggle("active-nav-menu-ul");

})


let preloader = document.querySelector(".page-loader");

window.addEventListener("load", function() {
    preloader.style.display = "none";
})



