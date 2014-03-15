
app.controller('uploadController',
    ['$scope', 'apiAccess', 'html5Upload', function ($scope, apiAccess, html5Upload) {
        // Bindable properties and functions are placed on vm.
        $scope.title = 'UploadController';
        $scope.images = [];
        $scope.delete = function (item) {
            //alert('delete: ' + item.name);
            if (confirm('Are you really want to delete "' + item.name + '"?')) {
                apiAccess.deleteImage(item.name,
                    function (imageName) {
                        removeItem(imageName);
                    },
                    function (err) {
                        alert('error: ' + err);
                    });
            }
        };
        $scope.save = function (item) {
            if (!item.title || item.title == '') {
                alert('please add a title for the image: \'' + item.name + '\'');
                // TODO: focus field for the current item
                return;
            }
            if (!item.desc || item.desc == '') {
                alert('please add a description for the image: \'' + item.name + '\'');
                // TODO: focus field for the current item
                return;
            }

            apiAccess.saveImage(item, function () {
                removeItem(item.name);
            }, function (data) {
                // TODO: handle errors
            });
        };
        $scope.saveall = function () {
            for (var i = 0; i < $scope.images.length; i++) {
                $scope.save($scope.images[i]);
            }
        };

        // #region Internal Methods       

        var removeItem = function (id) {
            var filtered = $scope.images.filter(function (item) {
                return item.name != id;
            });
            $scope.images = filtered;
            $scope.$apply();
        };

        var activate = function () {

            html5Upload.init(function () {
                if (event.dataTransfer.files.length > 0) {
                    for (var i = 0; i < event.dataTransfer.files.length; i++) {

                        var file = event.dataTransfer.files[i];

                        if (file.type.match('image.*')) {

                            if (createForm(file)) {
                                $('.progress-indicator').css('display', 'block');
                                apiAccess.uploadFile(file, function (data) {
                                    // handle progress of that file
                                    $('.progress-indicator').css('display', 'none');
                                });
                            }
                        }

                        if (file.type.match('text.*') || file.type.match('application/json')) {
                            handleTextFile(file);
                        }
                    }
                }
            });

            apiAccess.getImages(
                function (data) {
                    for (var i = 0; i < data.length; i++) {
                        data[i].dataurl = '/api/Images/' + data[i].name + '/200/500/true';
                    }
                    $scope.images = data;
                    $scope.$apply();
                },
                function (data) {
                    alert('error: ' + data);
                }, true);
         
        };

        var createForm = function (file) {

            var filtered = $scope.images.filter(function (item) {
                return item.name == file.name;
            });

            if (filtered.length == 0) {

                var reader = new FileReader();

                reader.onload = (function (thefile) {
                    return function (e) {

                        console.debug(thefile.name);
                        $scope.images.push({
                            name: thefile.name,
                            size: thefile.size,
                            type: thefile.type,
                            dataurl: e.target.result,
                            title: '',
                            desc: ''
                        });
                        console.debug($scope.images.length);
                        $scope.$apply();
                    };
                })(file);

                // Read in the image file as a data URL.
                reader.readAsDataURL(file);
                return true;
            }
            return false;
        };

        var handleTextFile = function (file) {
            var reader = new FileReader();

            reader.onload = function (e) {
                var rawJson = e.target.result;

                try {

                    var data = JSON.parse(rawJson);

                    data.forEach(function (f) {
                        $scope.images.forEach(function (g) {
                            if (f.name === g.name) {
                                g.title = f.title;
                                g.desc = f.desc;
                            }
                        });
                    });
                    $scope.$apply();

                } catch (e) {
                    console.debug('err: ' + rawJson);
                    //alert('err');
                }
            };

            // Read in the image file as a data URL.
            reader.readAsText(file);
        };

        //#endregion

        activate();
    }]);
