$(document).ready(function () {

    $('#shitt-post-content').focus(function () {
        $(this).animate({ rows: 6 }, 500);
        $('#post-shitt-buttons').show();
    });

    //$('#shitt-post-content').blur(function () {
    //    $(this).animate({ rows: 2 }, 500);
    //    $('#post-shitt-buttons').hide();
    //});

});