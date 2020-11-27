$(document).ready(function () {
    $('#IngredientForm').validate({
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
            'TitleOne': {
                required: true,
                minlength: 4
            },

            'DiscriptionOne': {
                required: true,
                minlength: 4
            },
            'DiscriptionSec': {
                required: true,
                minlength: 4
            },

            'Protein': {
                required: true,
                minlength: 4   
            },
            'Fat': {
                required: true,
                minlength: 4
            },
            'Carbohydrate': {
                required: true,
                minlength: 4
            },
            'OrderTitle': {
                required: true,
                minlength: 4
            }

        },
        messages: {
            'Title': {
                required: 'Please provide a Title',
                minlength: 'Your input must be at least 4 characters long'
            },
            'TitleOne': {
                required: 'Please provide a TitleOne',
                minlength: 'Your input must be at least 4 characters long'
            },
            'DiscriptionOne': {
                required: 'Please provide a DiscriptionOne',
                minlength: 'Your input must be at least 4 characters long'
            },
            'DiscriptionSec': {
                required: 'Please provide a DiscriptionSec',
                minlength: 'Your input must be at least 4 characters long'
            },
            'Protein': {
                required: 'Please provide a Protein',
                minlength: 'Your input must be at least 4 characters long'
            },
            'Fat': {
                required: 'Please provide a Fat',
                minlength: 'Your input must be at least 4 characters long'
            },
            'Carbohydrate': {
                required: 'Please provide a Carbohydrate',
                minlength: 'Your input must be at least 4 characters long'
            },
            'OrderTitle': {
                required: 'Please provide a OrderTitle',
                minlength: 'Your input must be at least 4 characters long'
            }

           
        }
    });
});   