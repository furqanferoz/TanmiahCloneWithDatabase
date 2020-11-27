$(document).ready(function () {
    $('#HeaderForm').validate({
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
            'Title': {
                required: true,
                minlength: 4
            },
            'Name': {
                required: true,
                minlength: 4
            },

            'Discription': {
                required: true,
                minlength: 4
            },

            'Image': {
                required: true
                
            }
        },
        messages: {
            'Title': {
                required: 'Please provide a Title',
                minlength: 'Your input must be at least 4 characters long'
            },
            'Name': {
                required: 'Please provide a Title',
                minlength: 'Your input must be at least 4 characters long'
            },
            'Discription': {
                required: 'Please provide a Description',
                minlength: 'Your input must be at least 4 characters long'
            },

            'Image': {
                required: 'Please provide a Image'
            }
        }
    });
});   