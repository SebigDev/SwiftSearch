(function ($) {
    'use strict';
    var $window = $(window);

    /*--------------------------------------------------------*/
    // Show and Hide Search Fields
    /*--------------------------------------------------------*/
    var advanceFields = $(".advance-fields");
    var searchFeaturesBtn = $(".search-features-list .title");

    $(".advance-btn").on('click', function () {
        $(this).toggleClass("active");
        advanceFields.toggleClass("open");
    });

    searchFeaturesBtn.on('click', function () {
        var $this = $(this).find(".search-features-btn");
        $this.toggleClass("active");

        if ($this.hasClass("active")) {
            $this.html("-");
        } else {
            $this.html("+");
        }

        $this.parents(".search-features-list").find(".search-features-fields").slideToggle();
    });

    /*--------------------------------------------------------*/
    // Property Search Sticky Map
    /*--------------------------------------------------------*/
    var $propertyMapSidebar = $('#property-map-sidebar');
    var $propertyMap = $propertyMapSidebar.find("#property-google-map");

    if( $propertyMapSidebar.length ) {
        var sidebar = new StickySidebar('#property-map-sidebar', {
            minWidth: 1200,
            resizeSensor: true
        });
    }

    $window.on('load resize', function () {
        if ($(this).width() > 1199) {
            if ($(this).outerHeight() > $propertyMap.outerHeight() ) {
                $propertyMap.css("height", $(this).outerHeight());
            }
        } else {
            $propertyMap.css("height", '');
        }
    });

    /*--------------------------------------------------------*/
    // Property Compare Sidebar
    /*--------------------------------------------------------*/
    $('.property-add-to-compare').on('click', function () {
        $(this).toggleClass("added");
        $("#property-compare-container").addClass("show");
    });

    $('.close-compare-container').on('click', function () {
        $("#property-compare-container").removeClass("show");
    });

    /*--------------------------------------------------------*/
    // Slider
    /*--------------------------------------------------------*/
    function attachSliderNav() {
        var $homePropertySlider = $('.home-property-slider');
        if ($homePropertySlider.length) {
            var $sliderNav = $homePropertySlider.find('.flex-direction-nav');
            var $slideCaption = $homePropertySlider.find('.slide-caption').offset();

            $sliderNav.css({
                top: $slideCaption.top + 7,
                left: $slideCaption.left - $sliderNav.outerWidth()
            });
        }
    }

    if (jQuery().flexslider) {
        // Home Property Slider
        $('.home-property-slider').flexslider({
            controlNav: false,
            directionNav: true,
            prevText: '<i class="ion-android-arrow-back"></i>',
            nextText: '<i class="ion-android-arrow-forward"></i>',
            start: function ($slider) {
                attachSliderNav();
            }
        });

        // Single Property Slider
        $('.single-property-slider').flexslider({
            controlNav: false,
            directionNav: true,
            prevText: '<i class="fa fa-angle-left"></i>',
            nextText: '<i class="fa fa-angle-right"></i>'
        });

        $window.on('load resize', function () {
            if ($(this).width() > 1600) {
                attachSliderNav();
            }
        });
    }

    /*--------------------------------------------------------*/
    // Carousels
    /*--------------------------------------------------------*/
    if (jQuery().owlCarousel) {
        //
        // Home Carousels
        //
        var $homePropertyThumbs = $('#home-property-thumbs');
        var $homePropertyCarousel = $('.home-property-carousel');
        $homePropertyThumbs.on('click', '.property-thumb', function () {
            $(this).addClass("active").siblings('.property-thumb').removeClass("active");
        });

        $window.on('load resize', function () {
            var $item = $homePropertyCarousel.find(".item");
            if ($(this).width() > 992) {
                $item.css("height", $homePropertyThumbs.outerHeight());
            } else {
                $item.css("height", 'auto');
            }
        });

        $homePropertyCarousel.owlCarousel({
            items: 1,
            margin: 0,
            dots: false,
            smartSpeed: 1000,
            dotsContainer: '#home-property-thumbs'
        });

        var $homeCarousel = $('.home-carousel');
        var $homeNav = $(".home-nav-wrapper");
        $homeCarousel.owlCarousel({
            margin: 0,
            smartSpeed: 1000,
            dots: false,
            responsive: {
                0: {
                    items: 1
                },
                991: {
                    items: 2
                },
                1366: {
                    items: 3
                }
            }
        });
        $homeNav.find(".next-link").on('click', function (e) {
            $homeCarousel.trigger('next.owl.carousel');
            e.preventDefault();
        });
        $homeNav.find(".previous-link").on('click', function (e) {
            $homeCarousel.trigger('prev.owl.carousel');
            e.preventDefault();
        });


        //
        // Properties carousels
        //
        $('.property-carousel').owlCarousel({
            margin: 30,
            smartSpeed: 1000,
            nav: true,
            navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
            responsive: {
                0: {
                    items: 1
                },
                768: {
                    items: 2
                },
                1199: {
                    items: 3
                }
            }
        });

        $('.property-carousel-two').owlCarousel({
            margin: 30,
            smartSpeed: 1500,
            items: 1
        });

        $('.property-carousel-three').owlCarousel({
            margin: 10,
            smartSpeed: 1000,
            responsive: {
                0: {
                    items: 1
                },
                768: {
                    items: 2
                },
                991: {
                    items: 3
                },
                1199: {
                    items: 4
                }
            }
        });

        var $featuredProperties = $('.featured-properties-carousel');
        var $featuredPropertiesNav = $(".featured-properties-nav-wrapper");
        $featuredProperties.owlCarousel({
            items: 1,
            margin: 0,
            smartSpeed: 500,
            dots: false
        });
        $featuredPropertiesNav.find(".next-link").on('click', function (e) {
            $featuredProperties.trigger('next.owl.carousel');
            e.preventDefault();
        });
        $featuredPropertiesNav.find(".previous-link").on('click', function (e) {
            $featuredProperties.trigger('prev.owl.carousel');
            e.preventDefault();
        });


        //
        // Partner Carousels
        //
        $('.partner-carousel').owlCarousel({
            smartSpeed: 500,
            responsive: {
                0: {
                    items: 1,
                    margin: 0
                },
                767: {
                    items: 2
                },
                991: {
                    items: 3
                },
                1199: {
                    items: 5
                }
            }
        });


        //
        // Agent Carousels
        //
        $('.agent-carousel').owlCarousel({
            margin: 30,
            smartSpeed: 500,
            responsive: {
                0: {
                    items: 1
                },
                768: {
                    items: 2
                },
                991: {
                    items: 3
                },
                1199: {
                    items: 4
                }
            }
        });

        var $agentsCarousel = $('.agent-carousel-two');
        var $agentsNav = $(".agent-carousel-nav-wrapper");
        $agentsCarousel.owlCarousel({
            items: 1,
            margin: 0,
            dots: false,
            smartSpeed: 500,
            autoHeight: true
        });
        $agentsNav.find(".next-link").on('click', function (e) {
            $agentsCarousel.trigger('next.owl.carousel');
            e.preventDefault();
        });
        $agentsNav.find(".previous-link").on('click', function (e) {
            $agentsCarousel.trigger('prev.owl.carousel');
            e.preventDefault();
        });


        //
        // Testimonial
        //
        $('.testimonial-carousel').owlCarousel({
            margin: 0,
            smartSpeed: 800,
            items: 1
        });

        var $testimonialCarouselTwo = $('.testimonial-carousel-two');
        var $testimonialNav = $(".testimonial-nav-wrapper");
        $testimonialCarouselTwo.owlCarousel({
            items: 1,
            margin: 0,
            smartSpeed: 1000,
            rewind: true,
            dots: false
        });

        $testimonialNav.find(".next-link").on('click', function (e) {
            $testimonialCarouselTwo.trigger('next.owl.carousel');
            e.preventDefault();
        });
        $testimonialNav.find(".previous-link").on('click', function (e) {
            $testimonialCarouselTwo.trigger('prev.owl.carousel');
            e.preventDefault();
        });
    }

    /*--------------------------------------------------------*/
    // Compare Properties
    /*--------------------------------------------------------*/
    var sync1 = $("#sync1");
    var sync2 = $("#sync2");

    sync1.owlCarousel({
        margin: 0,
        smartSpeed: 750,
        dots: false,
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 2
            },
            1199: {
                items: 3
            }
        }
    }).on('changed.owl.carousel', syncPosition1);

    sync2.owlCarousel({
            margin: 0,
            smartSpeed: 750,
            dots: false,
            responsive: {
                0: {
                    items: 1
                },
                768: {
                    items: 2
                },
                1199: {
                    items: 3
                }
            }
        }).on('changed.owl.carousel', syncPosition2);

    function syncPosition1(el) {
        var number = el.item.index;
        sync2.data('owl.carousel').to(number, 100, true);
    }

    function syncPosition2(el) {
        var number = el.item.index;
        sync1.data('owl.carousel').to(number, 100, true);
    }

    function createSticky(sticky, end) {

        if (typeof sticky !== "undefined") {

            var	pos = sticky.offset().top,
                stickyHeight = sticky.outerHeight(),
                endOffsetTop = end.offset().top,
            	endPos = endOffsetTop - ( stickyHeight * 2 ),
                win = $(window);

            win.on("scroll", function() {
                if(win.scrollTop() >= pos) {
                    sticky.addClass("stick");
                    sticky.css('width',sticky.parent(".property-compare").width());
                    win.scrollTop() >= endPos ? sticky.addClass("hide-it") : sticky.removeClass("hide-it");
                }else{
                    sticky.removeClass("stick");
                    sticky.css('width','');
                }
            });
        }
    }

    createSticky($("#property-compare-head"), $("#end-stick"));

    /*--------------------------------------------------------*/
    // Form Validation
    /*--------------------------------------------------------*/
    function validateForm(form) {
        var $form = $(form),
            formStatus = $form.find('.status'),
            progressLoader = $form.find('#progress-loader');

        $form.validate({
            errorLabelContainer: formStatus,
            submitHandler: function (form) {
                $(form).ajaxSubmit({
                    beforeSubmit: function () {
                        formStatus.hide();
                        progressLoader.show();
                    },
                    success: function (result, status, xhr, form) {
                        var data = $.parseJSON(result);
                        progressLoader.hide();
                        formStatus.show();

                        if (data.response == "success") {
                            formStatus.addClass("success").html(data.message);
                        } else {
                            formStatus.addClass("error").html(data.message);
                        }
                    },
                    complete: function (xhr, status, form) {
                        $form.resetForm();
                        setTimeout(function () {
                            formStatus.text('');
                        }, 3000);
                    }
                });
            }
        });
    }

    if (jQuery().validate && jQuery().ajaxSubmit) {
        validateForm("#contact-form");
        validateForm("#edit-profile-form");
        validateForm("#change-password-form");
        validateForm("#agent-contact-form");
        validateForm("#add-property-form");
    }

    /*--------------------------------------------------------*/
    // Slick Nav
    // http://slicknav.com/
    /*--------------------------------------------------------*/
    if (jQuery().slicknav) {
        var mainNavigation = $("#main-navigation");
        mainNavigation.slicknav({
            label: "",
            prependTo: '.main-navigation-wrapper'
        });
    }

    /*--------------------------------------------------------*/
    // Select2
    /*--------------------------------------------------------*/
    if (jQuery().select2) {
        $('.advance-search select, .property-sort-select, .select-field, .widget_property_search select').select2({});
    }

    /*--------------------------------------------------------*/
    // Light Case
    // http://cornel.bopp-art.com/lightcase/documentation/
    /*--------------------------------------------------------*/
    if (jQuery().lightcase) {
        $('a[data-rel^=lightcase]').lightcase({
            swipe: true
        });
    }

    /*--------------------------------------------------------*/
    // Bootstrap Tool Tip
    /*--------------------------------------------------------*/
    $('[data-toggle="tooltip"]').tooltip();

    /*--------------------------------------------------------*/
    // Accordion
    /*--------------------------------------------------------*/
    var initAccordion = function () {
        var $accordion = $(".accordion");
        if ($accordion) {
            $accordion.find(".accordion-sections.opened .accordion-content").slideDown(200);
            $accordion.find(".accordion-header").on('click', function () {
                var self = $(this);
                var parent = self.parents(".accordion");
                parent.find(".accordion-sections").removeClass("opened");
                parent.find(".accordion-sections").find(".accordion-content").stop().slideUp(200);
                $(this).parent(".accordion-sections").addClass("opened");
                $(this).next(".accordion-content").stop().slideDown(200);
            });
        }
    };
    initAccordion();

    /*--------------------------------------------------------*/
    // Toggle
    /*--------------------------------------------------------*/
    var initToggle = function () {
        var $toggle = $(".toggle");
        if ($toggle) {
            $toggle.find(".toggle-sections.opened .toggle-content").slideToggle(200);
            $toggle.find(".toggle-header").on('click', function () {
                var self = $(this);
                $(this).parent(".toggle-sections").toggleClass("opened");
                $(this).next(".toggle-content").stop().slideToggle(200);
            });
        }
    };
    initToggle();

    /*--------------------------------------------------------*/
    // Tabs
    /*--------------------------------------------------------*/
    var initTabs = function () {
        var $tabs = $(".tabs");
        if ($tabs) {
            $tabs.find(".tabs-nav-list > li").on('click', function () {
                var self = $(this);
                var tabId = self.index();
                var parent = self.parents(".tabs");
                parent.find("li").removeClass('active');
                $(this).addClass('active');
                parent.find(".tab-content").removeClass('active').hide();
                parent.find(".tab-content").eq(tabId).show().addClass('active');
            });
        }
    };
    initTabs();

    /*--------------------------------------------------------*/
    // Scroll To Top
    /*--------------------------------------------------------*/
    var $scrollToTop = $("#scroll-to-top");
    $window.on('scroll', function () {
        if ($window.width() > 992) {
            if ($(this).scrollTop() > 220) {
                $scrollToTop.fadeIn();
                return;
            }
        }
        $scrollToTop.fadeOut();
    });
    $scrollToTop.on('click', function (e) {
        $("html, body").animate({scrollTop: 0}, 800);
        return false;
    });
})(jQuery);