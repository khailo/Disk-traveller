
    var app = angular.module('traveller', ['ngRoute']);  // Will use ['ng-Route'] when we will implement routing

    app.service("svc", function () {
        var msg = "";
        return {
            setMessage: function (x) {
                alert("POSTING " + x + " TO SERVER!")
                msg = x;
            },
            ask: function () {
                $http({
                    method: "GET",
                    url: "api/disk"
                }).then(function mySucces(response) {
                    return response.data;
                }, function myError(response) {
                    return response.statusText;
                });
            },
            getMessage: function () {
                return msg;
            }
        };
    })




    //Create a Controller
    app.controller('HomeController', function ($scope) {  // here $scope is used for share data between view and controller
        $scope.Message = "Yahoooo! we have successfully done our first part.";







})();

