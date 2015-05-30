//remove an item by clicking the badge
$(document).on('click', '.notification-list-item', function (event) {
    event.preventDefault();

    var notificationId = $(this).data('id');
    var notificationType = $(this).data('type');
    var follower = $(this).data('follower');
    var shittId = $(this).data('shittid');

    //redirect to user profile if notification type is 'follower' ad display comment if it's  'comment'
    if (notificationType == 'follow') {
        window.location.href = '/Users/Profile/' + follower;
    } else if (notificationType == 'comment') {
        // get comment to display
        $.ajax({
            url: "/Shitts/GetShittById/",
            type: "GET",
            data: { id: shittId },
            success: function (data) {
                $("#home-page-shitts-containter").hide();
                $("#home-page-shitts-containter").html(data);
                $("#home-page-shitts-containter").slideDown(500);
                $('.show-shitt-comments-link').click();
            },
            error: function (error) {
            },
        });
    }

    //remove the notification from the list and decrease notifications count
    $('#notification-' + notificationId).remove();
    var notificationsCount = $('#notifications-count').text();
    var newNotificationsCount = parseInt(notificationsCount) - 1;
    if (newNotificationsCount == 0) {
        $('#notifications-count').remove();
    } else {
        $('#notifications-count').text(newNotificationsCount);
    }

    //send ajax post to delete notification
    $.ajax({
        url: "/Notifications/DeleteNotification",
        type: "POST",
        data: { id: notificationId },
        success: function (data) {
        },
        error: function (error) {
        }
    });
});




