//read uplaoded photo content and title
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        var file = input.files[0];

        reader.onload = function (e) {
          //  $('.blah').attr('src', e.target.result);
            $('.image-title').attr('value', file.name);
          //  $(".delete-picture-hidden-field").val('no');
        };

        reader.readAsDataURL(input.files[0]);
    }
}