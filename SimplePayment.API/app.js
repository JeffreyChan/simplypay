// app.js
// create angular app
var paymentApp = angular.module('paymentApp', []);

paymentApp.value("PAYMENT_URL", {
    rootUrl: "http://localhost:1100/"
});

angular
    .module('paymentApp')
    .service('paymentService', paymentService);

paymentService.$inject = ['$http', "PAYMENT_URL"];


function paymentService($http, PAYMENT_URL) {

    this.easyPay = function (payModel) {
        return $http.post(PAYMENT_URL.rootUrl + "api/simple/pay",
            payModel,
            { headers: { 'Content-Type': 'application/json' } });
    }
}

// create angular controller
angular
    .module('paymentApp')
    .controller('mainController', mainController);

mainController.$inject = ['$scope', "paymentService"];

function mainController($scope, paymentService) {

    $scope.payModel = {
        MerchantName: '',
        ReferenceFlag: '',
        PayAmount: 0.0,
        PayWay: 1
    };

    $scope.transcationInfo = {
        hasError:null,
        messageInfo: '',
        errorCode:''
    };
    // function to submit the form after all validation has occurred            
    $scope.submitForm = function (isValid) {

        // check to make sure the form is completely valid
        if (isValid) {
            paymentService.easyPay($scope.payModel)
                .success(function (data) {
                    console.log("Yes");
                    $scope.transcationInfo.messageInfo = "your payment method was successfully";
                    $scope.transcationInfo.hasError = false;
                })
                .error(function (errorInfo) {
                    console.log("No");
                    $scope.transcationInfo.messageInfo = "your payment method was declined";
                    $scope.transcationInfo.errorCode = errorInfo.Details;
                    $scope.transcationInfo.hasError = true;
                });;
        }
    };
}
