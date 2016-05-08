var app = angular.module('traveller', ['ngRoute']); 

app.controller('HomeController', function ($scope, $http) {
    var OkMessage = "All is OK";

    $scope.Message = OkMessage;
    $scope.folders = {};
    $scope.up = false;
    $scope.current = false;
    //$scope.Message = $scope.Message[0];
 

    $scope.AskDisks = function () {
        $http({
            method: "GET",
            url: "api/disk"
        }).then(function mySucces(response) {
            $scope.drives = response.data;
            $scope.Message = OkMessage;
        }, function myError(response) {
            $scope.Message=response.statusText;
        });
    };

    $scope.AskFolders = function (path) {
        path = btoa(encodeURIComponent(path))
        $http({
            method: "GET",
            url: "api/disk/"+path
        }).then(function mySucces(response) {
           
            if (!angular.equals({}, response.data.Error)) {
                $scope.Message = response.data.Error.Text
            }
            else
            {
                $scope.folders = response.data.Folders;
                $scope.files = response.data.Files;
                $scope.count = response.data.Count;
                $scope.up = response.data.Up.Path;
                $scope.current = response.data.Current.Path;
                $scope.Message = OkMessage;

            }
            
        }, function myError(response) {
            $scope.Message = response.statusText;
        });
        }

    $scope.AskDisks();
})

