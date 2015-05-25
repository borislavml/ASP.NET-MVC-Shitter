//read uplaoded photo content and title
function readURLedit(input) {
    var FILE_SIZE = 559959;

    $('.image-deleted').val('no');
    $('.upload-file-format-alert').hide();
    $('.upload-file-size-alert').hide();
    $('.submit-edit-disable').attr("disabled", false);

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        var file = input.files[0];

        reader.onload = function (e) {
            $('.edit-profile-photo').attr('src', e.target.result);
            $('.image-title').attr('value', file.name);
        };

        reader.readAsDataURL(input.files[0]);

        // alert user if image size is too big
        if (file.size >= FILE_SIZE) {
            $('.upload-file-size-alert').show();
            $('.submit-edit-disable').attr("disabled", true);
            $('.image-title').attr('value', file.name);
        }

        // alert user if iamge format is not allowed
        if (!file.type.match(/image\/.*/)) {
            $('.upload-file-format-alert').show();
            $('.submit-edit-disable').attr("disabled", true);
            $('.image-title').attr('value', file.name);
        }
    }
}

// delete photo
$('.delete-profile-photo-button').click(function () {
    $('.edit-profile-photo').val("deleted");
    $('.edit-profile-photo').attr('src', '/Content/Images/no-image.png');
    $('.upload-file-size-alert').hide();
    $('.upload-file-format-alert').hide();
    $('.submit-edit-disable').attr("disabled", false);
    $('.image-deleted').val('yes');
    $('.image-title').attr('value', "");

});

// alert if summary size exceeded and disable submit button
$('#edit-profile-text-area').on('keyup', function () {
    $('.summary-size-alert').hide();
    $('.submit-edit-disable').attr("disabled", false);

    var value = $(this).val();
    if (value.length > 100) {
        $('.summary-size-alert').show();
        $('.submit-edit-disable').attr("disabled", true);
    }
  
});