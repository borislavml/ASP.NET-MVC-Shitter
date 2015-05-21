
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