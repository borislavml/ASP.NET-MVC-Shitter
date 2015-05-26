$('.delete-shitt-button').click(function () {
    var shittId = $(this).data('id');
    $('#hidden-input-id').val(shittId);
});