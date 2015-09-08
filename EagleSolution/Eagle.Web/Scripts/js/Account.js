define(['bootstrap', 'TweenMax.min', 'resizeable', 'joinable', 'xenon-api', 'xenon-toggles', 'jquery.validate.min', '../toastr/toastr.min', 'xenon-custom'], function (require) {
    $(document).ready(function ($) {
        // Reveal Login form
        setTimeout(function () { $(".fade-in-effect").addClass('in'); }, 1);


        // Validation and Ajax action
        $("form#login").validate({
            rules: {
                username: {
                    required: true
                },

                passwd: {
                    required: true
                }
            },

            messages: {
                username: {
                    required: 'Please enter your username.'
                },

                passwd: {
                    required: 'Please enter your password.'
                }
            },

            // Form Processing via AJAX
            submitHandler: function (form) {
                show_loading_bar(70); // Fill progress bar to 70% (just a given value)

                var opts = {
                    "closeButton": true,
                    "debug": false,
                    "positionClass": "toast-top-full-width",
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                };

                $.ajax({
                    url: "/Rice/login",
                    method: 'POST',
                    dataType: 'json',
                    data: {
                        accountName: $(form).find('#username').val(),
                        password: $(form).find('#passwd').val(),
                    },
                    success: function (resp) {
                        show_loading_bar({
                            delay: .5,
                            pct: 100,
                            finish: function () {

                                // Redirect after successful login page (when progress bar reaches 100%)
                                if (resp.Flag) {
                                    window.location.href = '/home/index';
                                }
                                else {
                                    toastr.error(resp.Message, "Invalid Login!", opts);
                                    $('#passwd').select();
                                }
                            }
                        });

                    }
                });

            }
        });

        // Set Form focus
        $("form#login .form-group:has(.form-control):first .form-control").focus();
    });




});