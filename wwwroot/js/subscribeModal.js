$(function () {
    var modalPlaceholder = $('#modal-placeholder');

    $('body').on('click', '.js-open-subscribe-modal', function (event) {

        var blogId = $(this).data('blog-id');
        var url = '/Blog/Subscribe?id=' + blogId;

        $.get(url).done(function (data) {
            modalPlaceholder.html(data);

            modalPlaceholder.find('.modal').modal('show');
        });
    });

    modalPlaceholder.on('hidden.bs.modal', function () {
        modalPlaceholder.empty();
    });
});