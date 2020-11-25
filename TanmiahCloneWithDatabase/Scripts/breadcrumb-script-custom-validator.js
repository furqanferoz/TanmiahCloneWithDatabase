$(document).ready(function () {
    $('#BreadCrumbForm').validate({
        errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page  
        errorElement: 'div',
        errorPlacement: function (error, e) {
            e.parents('.form-group > div').append(error);
        },
        highlight: function (e) {

            $(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
            $(e).closest('.help-block').remove();
        },
        success: function (e) {
            e.closest('.form-group').removeClass('has-success has-error');
            e.closest('.help-block').remove();
        },
        rules: {
            'MainLink': {
                required: true,
                minlength: 4
            },

            'Link': {
                required: true,
                minlength: 4
            },

            'SubLink': {
                required: true,
                minlength: 4
            }
        },
        messages: {
            'MainLink': {
                required: 'Please provide a Main-Link',
                minlength: 'Your input must be at least 4 characters long'
            },
            'Link': {
                required: 'Please provide a Link',
                minlength: 'Your input must be at least 4 characters long'
            },

            'SubLink': {
                required: 'Please provide a Sub-Link',
                minlength: 'Your input must be at least 4 characters long'
            }
        }
    });
});   