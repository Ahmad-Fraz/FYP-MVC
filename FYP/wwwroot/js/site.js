// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
	$(function () {
    var placeholder = $('#placeholder');
    $('button[data-toggle="modal"]').click(function (event) {
        var url = $(this).data('url');
        var encodedURl = decodeURIComponent(url);
        $.get(encodedURl).done(function (data) {
            placeholder.html(data);
            placeholder.find('.modal').modal('show');
        })
    })
});
