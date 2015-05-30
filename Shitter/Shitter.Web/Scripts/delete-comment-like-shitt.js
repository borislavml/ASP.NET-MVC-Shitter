$(document).on('click', '.delete-shitt-button', function () {
    var shittId = $(this).data('id');
    $('#hidden-input-id').val(shittId);
});

// get shitt likes
$(document).on('click', 'a[href="#view-favourites"]', function () {
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

$(document).on('click', '.show-shitt-comments-link', function () {
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

$(document).on('click', '.close-shitt-comments-div', function () {
    event.preventDefault();
    var shittId = $(this).data('id');
    $('#shitt-comments-container-' + shittId).slideUp(300);
});

// large-image-modal
$(document).on('click', '.large-image-popup', function () {
    var imageDatUrl = $(this).data('image');
    $('#popup-picture-modal').attr('src', imageDatUrl);
});





