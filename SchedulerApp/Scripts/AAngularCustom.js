(function () {
    //"use strict";
    //var app = angular.module("AppSoDetails", [])
   angular.module("AppSoDetails", [])
      //  .service("CtrlSoDetails", function () {
        .controller("CtrlSoDetails", ['$scope', function ($scope) {

            $scope.tBoxDetails = [{}];
            $scope.showRemove = false;

            $scope.addTBoxes = function (index) {
                //console.log("Add SO details");
                var newItemNo = $scope.tBoxDetails.length + 1;
                console.log(newItemNo);
                $scope.tBoxDetails.push({
                    'partNo': "",
                    'proQty': "",
                    'HDia': "",
                    'Length': "",
                    'Accessr': "",
                    'LeadLength': "",
                    'LeadProtection': "",
                    'Poting': "",
                });
                $scope.showRemove = true;
            }

            $scope.remTBoxes = function (index) {
                newItemNo = $scope.tBoxDetails.length - 1;
                $scope.tBoxDetails.splice(index, 1);
                console.log(newItemNo);
                if (newItemNo == 1) {
                    $scope.showRemove = false;                           
                }
                else if (newItemNo == 0)
                    {
                    $scope.showRemove = true;
                    }
            }

        }]);
})();