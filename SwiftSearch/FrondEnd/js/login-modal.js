(function ($) {
    'use strict';

    // Tabs
    var initTabs = function () {
        var $tabs = $(".tabs");
        if ($tabs) {
            $tabs.find(".tabs-nav-list > li").on('click', function () {
                var self = $(this);
                var parent = self.parents(".tabs");
                parent.find(".tabs-nav-list > li").removeClass('active');
                $(this).addClass('active');
                var tabContent = parent.find(".tab-content");
                tabContent.removeClass('active');
                tabContent.eq(self.index()).addClass('active');
            });
        }
    };
    initTabs();

    $(".sign-in-form").on('click', function () {
        $(".tabs-nav-list > li.sign-in").trigger("click");
        return false;
    });

    $(".reset-password").on('click', function () {
        $(".tabs-nav-list > li.lost-password").trigger("click");
        return false;
    });

    // Validation
    function validateForm(form) {
        var $form = $(form),
            formStatus = $form.find('.status'),
            progressLoader = $form.find('.progress-loader');
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
        validateForm("#signin-form");
        validateForm("#register-form");
        validateForm("#lost-password-form");
    }
})(jQuery);