$('.delete-shitt-button').click(function () {
    var shittId = $(this).data('id');
    $('#hidden-input-id').val(shittId);
});

// get shitt likes
$('a[href="#view-favourites"]').click(function () {
    var shittId = $(this).data('id');

    $.ajax({
        url: "/Shitts/GetShittFavourites/",
        type: "GET",
        data: { id: shittId },
        success: function (data) {
            $("#favourites-users-modal-body").html(data);
        },
        error: function (error) {
            console.log(error);
        },    
    });
});

$('.show-shitt-comments-link').click(function () {
    // show comment form
    var shittId = $(this).data('id');
    $('#shitt-comments-container-' + shittId).slideDown(500);

    // remove currently posted shitts to avoid duplication
    $('#current-posted-comment-div-' + shittId).text('');

    // load shitt comments
    $.ajax({
        url: "/Comments/GetShittComments",
        type: "GET",
        data: { id: shittId },
        success: function (data) {
            $("#shitt-comments-list-div-" + shittId).html(data);
        },
        error: function (error) {
            console.log(error);
        }
    });
});

$('.close-shitt-comments-div').click(function (event) {
    event.preventDefault();
    var shittId = $(this).data('id');
    $('#shitt-comments-container-' + shittId).slideUp(300);
});





