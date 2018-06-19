(function ($) {
    'use strict';
    var $body = $("body");

    $body.on('click', '#theme-switcher-icon', function () {
        $("#theme-switcher").toggleClass("opened");
    });

    $body.on('click', '#switcher li', function () {
        var $this = $(this);
        var $theme = $this.data("theme");

        $this.siblings('li').removeClass("current");
        $this.addClass("current");
        switchTheme($theme);
    });

    function switchTheme(theme) {
        if (typeof (Storage) !== "undefined") {
            localStorage.setItem("theme", theme);
            toggleTheme(theme);
        }
    }

    function currentTheme() {
        if (typeof (Storage) !== "undefined") {
            var theme = localStorage.getItem("theme");
            toggleTheme(theme);
            $("#switcher").find("li").removeClass("current");
        }
    }

    function toggleTheme(theme) {
        var $current_theme = $("#theme-switcher-css");
        window.pearlPropertyIcon = theme;
        if ('default' != theme) {
            if ($current_theme.length > 0) {
                $current_theme.attr("href", "css/theme-" + theme + ".css");
            } else {
                var $stylesheet = '<link id="theme-switcher-css" href="css/theme-' + theme + '.css" rel="stylesheet">';
                $("html head").append($stylesheet);
            }
        } else {
            $current_theme.remove();
        }
    }

    function createThemeSwitcher() {
       var theme_switcher = '<div id="theme-switcher" class="theme-switcher">';
        theme_switcher += '<div class="theme-switcher-inner">';
        theme_switcher += '<div id="theme-switcher-icon" class="theme-switcher-icon"><i class="fa fa-cog fa-spin"></i></div>';
        theme_switcher += '<h4 class="title">Preset Skins</h4>';
        theme_switcher += '<ul id="switcher" class="clearfix">';
        theme_switcher += '<li data-theme="default" style="background-color:darkblue"></li>';
        theme_switcher += '<li data-theme="red" style="background-color:red"></li>';
        theme_switcher += '<li data-theme="green" style="background-color:green"></li>';
        theme_switcher += '<li data-theme="orange" style="background-color:orange"></li>';
        theme_switcher += '</ul>';
        theme_switcher += '</div>';
        theme_switcher += '</div>';

        var themeSwitcher = $("#theme-switcher");
        if (! themeSwitcher.length ) {
            $body.append(theme_switcher);
        }
    }

    currentTheme();
    createThemeSwitcher();
})(jQuery);