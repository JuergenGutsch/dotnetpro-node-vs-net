var app = angular.module('imageGallery', []);

app.factory('html5Upload', [function () {
    var service = {
        init: initHtml5Upload
    };

    return service;

    function initHtml5Upload(callback) {
        $('#dropTarget')
            .bind('drop', function () {
                if (event.preventDefault) {
                    event.preventDefault();
                }

                callback(this);
            })
            .bind('dragover', function () {
                if (event.preventDefault) {
                    event.preventDefault();
                }

                $(this).addClass('dragover');

            })
            .bind('dragenter', function () {
                if (event.preventDefault) {
                    event.preventDefault();
                }
            })
            .bind('dragleave', function () {
                if (event.preventDefault) {
                    event.preventDefault();
                }

                $(this).removeClass('dragover');
            })
            .bind('dragend', function () {
                if (event.preventDefault) {
                    event.preventDefault();
                }

                $(this).removeClass('dragover');
            });
    }

}]);

app.factory('apiAccess', ['$http', function ($http) {
    // Define the functions and properties to reveal.
    var service = {
        getImages: getImages,
        uploadFile: uploadFile,
        deleteImage: deleteImage,
        saveImage: saveImage
    };

    return service;

    //#region Internal Methods 

    function saveImage(image, successhandler, errorhandler) {
        $.ajax({
            type: 'POST',
            url: '/api/Metadata',
            data: image,
            success: function (data) {
                if (successhandler) {
                    successhandler(data);
                }
            },
            error: function (data) {
                if (errorhandler) {
                    errorhandler(data);
                }
            }
        });
    }

    function getImages(successhandler, errorhandler, loadInvalid) {
        $.ajax({
            type: 'GET',
            url: loadInvalid ? '/api/Metadata/Invalid' : '/api/Metadata/',
            success: function (data) {
                if (successhandler) {
                    successhandler(data);
                }
            },
            error: function (data) {
                if (errorhandler) {
                    errorhandler(data);
                }
            }
        });
    }

    function deleteImage(id, successhandler, errorhandler) {
        $.ajax({
            type: "DELETE",
            url: '/api/Metadata/' + id,
            success: function (data) {
                successhandler(id);
            },
            error: function (data) {
                errorhandler();
            }
        });
    }

    function uploadFile(file, progresshandler) {
        var data = new FormData();
        data.append('file', file);

        $.ajax({
            type: 'POST',
            url: '/api/Images',
            enctype: 'multipart/form-data',
            data: data,
            xhr: function () {  // Custom XMLHttpRequest
                var myXhr = $.ajaxSettings.xhr();
                if (myXhr.upload) { // Check if upload property exists
                    myXhr.upload.addEventListener('progress', progresshandler, false); // For handling the progress of the upload
                }
                return myXhr;
            },
            processData: false,
            contentType: false,
            success: function (data) {
                //alert("File available at: " + data.url);
            },
            error: function (data) {
                alert('error: ' + data);
            }
        });

    }

    //#endregion
}]);
