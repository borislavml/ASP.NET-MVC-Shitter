function updateFollowersListUnfollowBtnClicked(id) {
    var userViewing = $("#current-user-name").data("name")
    var pathname = window.location.pathname;
    var params = pathname.split('/');
    var paramsUser = params[3];

    if (userViewing !== null && (userViewing == paramsUser)) {
        //$("#user-mini-" + id).remove();
        var followingCount = $("#following-tab-count").text();
        var newFollowersCount = parseInt(followingCount) - 1;
        $("#following-tab-count").text(newFollowersCount);
    }
}

function updateFollowersListFollowBtnClicked(id) {
    var userViewing = $("#current-user-name").data("name")
    var pathname = window.location.pathname;
    var params = pathname.split('/');
    var paramsUser = params[3];

    if (userViewing !== null && (userViewing == paramsUser)) {
        var followingCount = $("#following-tab-count").text();
        var newFollowersCount = parseInt(followingCount) + 1;
        $("#following-tab-count").text(newFollowersCount);
    }
}