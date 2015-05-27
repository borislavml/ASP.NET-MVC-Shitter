function updateFollowersListUnfollowBtnClicked(id) {
    var updateView = userIsInTabsMenu();

    if (updateView) {
        //$("#user-mini-" + id).remove();
        var followingCount = $("#following-tab-count").text();
        var newFollowersCount = parseInt(followingCount) - 1;
        $("#following-tab-count").text(newFollowersCount);
    }
}

function updateFollowersListFollowBtnClicked(id) {
    var updateView = userIsInTabsMenu();

    if (updateView) {
        var followingCount = $("#following-tab-count").text();
        var newFollowersCount = parseInt(followingCount) + 1;
        $("#following-tab-count").text(newFollowersCount);
    }
}

function removeDeletedShitt(data) {
    $('#delete-shitt').modal('hide');
    $('#shitt-' + data).remove();
    $('#shitt-comments-container-' + data).remove();

    var pathName = window.location.pathname;
    if (pathName.indexOf('/Home/Dashboard') >= 0) {
        var shittsCount = $("#user-info-shitts-count").text();
        var newShittsCount = parseInt(shittsCount) - 1;
        $("#user-info-shitts-count").text(newShittsCount);
    } else if (pathName.indexOf("/users/profile") >= 0) {
        var shittsCount = $("#posted-shitts-tab-count").text();
        var newShittsCount = parseInt(shittsCount) - 1;
        $("#posted-shitts-tab-count").text(newShittsCount);
    }
}

function closeModalDialog() {
    $('#delete-shitt').modal('hide');
}

function decreaseFavouritesCound(id) {
    var favourotesCount = $('#shitt-favourites-count-' + id).text();
    var newFavourtiesCount = parseInt(favourotesCount) - 1;
    $('#shitt-favourites-count-' + id).text(newFavourtiesCount);

    var updateView = userIsInTabsMenu();
    if (updateView) {
        var currentFavsCount = $('#favourites-tab-count').text();
        var newFavsCount = parseInt(currentFavsCount) - 1;
        $('#favourites-tab-count').text(newFavsCount);
    }
}

function increaseFavouritesCound(id) {
    var favourotesCount = $('#shitt-favourites-count-' + id).text();
    var newFavourtiesCount = parseInt(favourotesCount) + 1;
    $('#shitt-favourites-count-' + id).text(newFavourtiesCount);

    var updateView = userIsInTabsMenu();
    if (updateView) {
        var currentFavsCount = $('#favourites-tab-count').text();
        var newFavsCount = parseInt(currentFavsCount) + 1;
        $('#favourites-tab-count').text(newFavsCount);
    }
}

function userIsInTabsMenu() {
    var userViewing = $("#current-user-name").data("name")
    var pathname = window.location.pathname;
    var params = pathname.split('/');
    var paramsUser = params[3];

    return (userViewing !== null && (userViewing == paramsUser));
}

function increseCommentsCount(id) {
    $('.shitt-comment-textarea').val('');
    var commentsCount = $('#shitt-comments-count-' + id).text();
    var newCommentsCount = parseInt(commentsCount) + 1;
    $('#shitt-comments-count-' + id).text(newCommentsCount);
}

function goToErrorPage() {
    window.location = "/Home/Error";
}