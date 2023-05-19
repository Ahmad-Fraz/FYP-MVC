// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
	$(function () {
        var placeholder = $('#placeHoderHere');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            placeholder.html(data);
            placeholder.find('.modal').modal('show');
        })
    })

        placeholder.on('click', '[data-save="modal"]', function (event) {
            event.preventDefault();
            var form = $(this).parents('.modal').find('form');
            var actionUrl = form.attr('action');
            var sendData = form.serialize();
            $.post(actionUrl, sendData).done(function (data) {
                placeholder.find('.modal').modal('hide');
            })
        })

});



function ShowPreview(input) {
    if (input.files && input.files[0]) {
        var ImageDir = new FileReader();
        ImageDir.onload = function (e) {
            $('#img').attr('src', e.target.result);
        }
        ImageDir.readAsDataURL(input.files[0]);
    }
}
    