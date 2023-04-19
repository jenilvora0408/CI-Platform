
function ImgUpload() {
    var imgWrap = "";
    var imgArray = [];

    $('.upload__inputfile').each(function() {
    $(this).on('change', function(e) {
        imgWrap = $(this).closest('.upload__box').find('.upload__img-wrap');
        var maxLength = $(this).attr('data-max_length');
        var files = e.target.files;
        var filesArr = Array.prototype.slice.call(files);
        var iterator = 0;
        var formData = new FormData();


        filesArr.forEach(function(f, index) {
            // Check if file is an image with the correct extension
            if (!f.type.match('image.(png|jpe?g)')) {
                alert('Please select only .png, .jpg, or .jpeg images.');
                return;
            }

            // Check if file size is less than 4 MB
            if (f.size > 4 * 1024 * 1024) {
                alert('Please select an image that is less than 4 MB.');
                return;
            }

            formData.append("files", f);

            if (imgArray.length > maxLength) {
                return false;
            } else {
                var len = 0;
                for (var i = 0; i < imgArray.length; i++) {
                    if (imgArray[i] !== undefined) {
                        len++;
                    }
                }
                if (len > maxLength) {
                    return false;
                } else {
                    imgArray.push(f);

                    var reader = new FileReader();
                    reader.onload = function(e) {
                        var html = "<div class='upload__img-box'><div style='background-image: url(" + e.target.result + ")' data-number='" + $(".upload__img-close").length + "' data-file='" + f.name + "' class='img-bg'><div class='upload__img-close'></div></div></div>";
                        imgWrap.append(html);
                        iterator++;
                    }
                    reader.readAsDataURL(f);
                }
            }
        });

        // Send image data to server via AJAX
        $("#saveBtn").on("click", function(e){
              $.ajax({
            url: "/Story/UploadImages",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                console.log(response);
            },
            error: function(xhr, status, error) {
                console.log(xhr.responseText);
            }
        })
      
        });
    });
});


    $('body').on('click', ".upload__img-close", function(e) {
        var file = $(this).parent().data("file");
        for (var i = 0; i < imgArray.length; i++) {
            if (imgArray[i].name === file) {
                imgArray.splice(i, 1);
                break;
            }
        }
        $(this).parent().parent().remove();});
    };


    $('.upload__box').on('dragover', function(e) {
        e.preventDefault();
        $(this).addClass('dragover');
    });

    $('.upload__box').on('dragleave', function(e) {
        e.preventDefault();
        $(this).removeClass('dragover');
    });

    $('.upload__box').on('drop', function(e) {
        e.preventDefault();
        $(this).removeClass('dragover');
        imgWrap = $(this).closest('.upload__box').find('.upload__img-wrap');
        var maxLength = $(this).find('.upload__inputfile').attr('data-max_length');

        var files = e.originalEvent.dataTransfer.files;
        var filesArr = Array.prototype.slice.call(files);
        var iterator = 0;


       filesArr.forEach(function(f, index) {
            // Check if file is an image with the correct extension
            if (!f.type.match('image.(png|jpe?g)')) {
                alert('Please select only .png, .jpg, or .jpeg images.');
                return;
            }

            // Check if file size is less than 4 MB
            if (f.size > 4 * 1024 * 1024) {
                alert('Please select an image that is less than 4 MB.');
                return;
            }

            formData.append("files", f);

            if (imgArray.length > maxLength) {
                return false;
            } else {
                var len = 0;
                for (var i = 0; i < imgArray.length; i++) {
                    if (imgArray[i] !== undefined) {
                        len++;
                    }
                }
                if (len > maxLength) {
                    return false;
                } else {
                    imgArray.push(f);

                    var reader = new FileReader();
                    reader.onload = function(e) {
                        var html = "<div class='upload__img-box'><div style='background-image: url(" + e.target.result + ")' data-number='" + $(".upload__img-close").length + "' data-file='" + f.name + "' class='img-bg'><div class='upload__img-close'></div></div></div>";
                        imgWrap.append(html);
                        iterator++;
                    }
                    reader.readAsDataURL(f);
                }
            }
        });
        // Send image data to server via AJAX
        $("#saveBtn").on("click", function(e){
              $.ajax({
            url: "/Story/UploadImages",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                console.log(response);
            },
            error: function(xhr, status, error) {
                console.log(xhr.responseText);
             }
        })
      
        
    });
});



