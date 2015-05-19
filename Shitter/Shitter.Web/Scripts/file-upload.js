//read uplaoded photo content and title
function readURL(input) {
    var FILE_SIZE = 559959;

    $('.uploadshitt-file-format-alert').hide();
    $('.uploadshitt-file-size-alert').hide();
    //$('.upload-shitt-photo-div').hide();
    //$('.uploaded-image-content').hide();
    $('.submit-post-disable').attr("disabled", false);

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        var file = input.files[0];

        reader.onload = function (e) {
            $('.upload-shitt-photo-div').show();
            $('.uploaded-image-content').show();
            $('.uploaded-image-content').attr('src', e.target.result);
            $('.image-title').attr('value', file.name);
          //  $(".delete-picture-hidden-field").val('no');
        };

        reader.readAsDataURL(input.files[0]);

        // alert user if image size is too big
        if (file.size >= FILE_SIZE) {
            $('.uploadshitt-file-size-alert').show();           
            $('.submit-post-disable').attr("disabled", true);
            $('.image-title').attr('value', file.name);
        }

        // alert user if iamge format is not allowed
        if (!file.type.match(/image\/.*/)) {
            $('.uploadshitt-file-format-alert').show();
            $('.submit-post-disable').attr("disabled", true);
            $('.image-title').attr('value', file.name);
        }       
    }
}

// delete photo
$('.delete-uploaded-shitt-photo-button').click(function () {
    $('.ImageDataUrl-photo').val("");
    $('.ImageDataUrl-photo').attr('src', '');
    $('.uploadshitt-file-size-alert').hide();
    $('.uploadshitt-file-format-alert').hide();
    $('.upload-shitt-photo-div').hide();
    $('.uploaded-image-content').hide();
    $('.submit-post-disable').attr("disabled", false);
});