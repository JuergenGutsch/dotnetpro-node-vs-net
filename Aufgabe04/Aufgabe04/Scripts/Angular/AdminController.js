
app.controller('adminController',
    ['$scope', 'apiAccess', 'html5Upload', function ($scope, apiAccess, html5Upload) {
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
                // TODO: handle sucess
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
                }, false);
        };
        //#endregion

        activate();
    }]);
