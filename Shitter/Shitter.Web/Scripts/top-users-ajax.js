// load top-users-list on page load
$(document).ready(function () {
    var locationPath = window.location.pathname;
    if (locationPath == "/" || locationPath.indexOf("/Home/Dashboard") >= 0 || locationPath.indexOf("/Home/Index") >= 0) {
        $.ajax({
            url: "/Users/GetTopUsers",
            type: "GET",
            data: null,
            success: function (data) {
                $(".top-users-list-div").html(data);
                $(".top-users-list-div").show(1500);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
});
