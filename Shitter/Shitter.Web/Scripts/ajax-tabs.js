
$(document).ready(function () {
    // AJAX call for following list
    $('#following-tab').click(function () {
        var url = "/Users/Following";
        var userId = $(this).data("id");
        $.get(url, { id: userId }, function (data) {
            $("#following").html(data);
        });
    })

    // AJAX call for followers list
    $('#followers-tab').click(function () {
        var url = "/Users/Followers";
        var userId = $(this).data("id");
        $.get(url, { id: userId }, function (data) {
            $("#followers").html(data);
        });
    })

    // AJAX call for favourites list
    $('#favourites-tab').click(function () {
        var url = "/Users/Favourites";
        var userId = $(this).data("id");
        $.get(url, { id: userId }, function (data) {
            $("#favourites").html(data);
        });
    })

    ////  enable link to tab
    //var url = document.location.toString();
    //if (url.match('#')) {
    //    $('.nav-tabs a[href=#' + url.split('#')[1] + ']').tab('show');
    //}

    //// Change hash for page-reload
    //$('.nav-tabs a').on('shown.bs.tab', function (e) {
    //    window.location.hash = e.target.hash;
    //})

    if (window.location.hash != "") {
        $(window.location.hash + '-tab').click();
        // location.reload();
    }
});

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