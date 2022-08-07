var codeSample;
(
    function () {
        'use strict';
        codeSample = angular.module('CodeSample', ['ui.router']);
    }
)();

codeSample.controller('productsController', ['$scope', '$http', function ($scope, $http) {

    $scope.listProducts = null;
    $scope.errorMsg = null;
    $scope.doingWork = false;

    getallProducts();

    //******=========Get All Products=========******
    function getallProducts() {
        $scope.doingWork = true;
        $http({
            method: 'GET',
            url: '/api/Product/GetAllProductsAsync/'
        }).then(function (response) {
            $scope.listProducts = response.data;
            $scope.errorMsg = null;
            $scope.doingWork = false;
        }, function (error) {
            $scope.errorMsg = error;
            $scope.doingWork = false;
        });
    };

    /********* Delete a product *********/
    $scope.confirmationDialogConfig = {};
    
    $scope.confirmationDialog = function (prodId) {
        $scope.confirmationDialogConfig = {
            title: "Confirm",
            message: "Are you sure you want to delete?",
            prodId: prodId
        };
        
        $scope.showDialog(true);
    };

    $scope.confirmDelete = function (id) {
        if (id) {
            $scope.showDialog(false);
            delProduct(id);
        }
    };
    $scope.showDialog = function (flag) {
        $("#confirmation-dialog .modal").modal(flag ? 'show' : 'hide');
    };
    function delProduct(id) {
        $scope.doingWork = true;
        $http({
            method: 'Delete',
            url: '/api/Product/DeleteProductAsync/' + id
        }).then(function (response) {

            destroyDatatable();

            $scope.listProducts = response.data;
            $scope.errorMsg = null;
            $scope.doingWork = false;
           
        }, function (error) {
            $scope.errorMsg = error;
            $scope.doingWork = false;

        });
    }

    function destroyDatatable() {
        var table = $('#prod-table').DataTable();
        table.destroy();
    }
}]);


codeSample.controller('editController', ['$scope', '$stateParams', '$http', '$window', function ($scope, $stateParams, $http, $window) {
    var prodId = $stateParams.id;
    $scope.doingWork = false;
    $scope.product = null;
    $scope.errorMsg = null;
    $scope.warningMsg = "";

    $scope.Types = [
        { value: 'Books', text: 'Books' },
        { value: 'Electronics', text: 'Electronics' },
        { value: 'Food', text: 'Food' },
        { value: 'Furniture', text: 'Furniture' },
        { value: 'Toys', text: 'Toys' },
    ]
    
    $scope.edit = function () {

        if (!$scope.product.Name) {
            $scope.warningMsg = "Please input product name";
            $("#warning-dialog .modal").modal('show');
        }
        else if (!$scope.product.Price) {
            $scope.warningMsg = "Please input product price";
            $("#warning-dialog .modal").modal('show');
        }
        else {
            editProduct();
        }
    }

    function editProduct() {
        $scope.doingWork = true;
        $http({
            method: 'PUT',
            url: '/api/Product/UpdateProductAsync/' + $scope.product.Id,
            data: JSON.stringify($scope.product)
        }).then(function (response) {

            $scope.doingWork = false;
            $window.location.href = '/';

        }, function (error) {
            $scope.errorMsg = error;
            $scope.doingWork = false;

        });
    }


    getProduct();

    //******=========Get Product Detail=========******
    function getProduct() {
        if (!prodId) {
            return;
        }

        $scope.doingWork = true;
        $http({
            method: 'GET',
            url: '/api/Product/GetProductByIdAsync/' + prodId
        }).then(function (response) {
            $scope.product = response.data;
            $scope.errorMsg = null;
            $scope.doingWork = false;
        }, function (error) {
            $scope.errorMsg = error;
            $scope.doingWork = false;;
        });
    };
}]);

codeSample.controller('addController', ['$scope', '$http', '$window', function ($scope,  $http, $window) {
    
    $scope.doingWork = false;
    $scope.product = {
        Id: 0,
        Name: "",
        Price: 0,
        Type: "",
        Active: false
    };
    $scope.errorMsg = null;
    $scope.warningMsg = "";

    $scope.Types = [
        { value: 'Books', text: 'Books' },
        { value: 'Electronics', text: 'Electronics' },
        { value: 'Food', text: 'Food' },
        { value: 'Furniture', text: 'Furniture' },
        { value: 'Toys', text: 'Toys' },
    ]
 
    $scope.add = function () {

        if (!$scope.product.Name) {
            $scope.warningMsg = "Please input product name";
            $("#warning-dialog .modal").modal('show');
        }
        else if (!$scope.product.Price) {
            $scope.warningMsg = "Please input product price";
            $("#warning-dialog .modal").modal('show');
        }
        else if (!$scope.product.Type) {
            $scope.warningMsg = "Please choose product type";
            $("#warning-dialog .modal").modal('show');
        }
        else {
            addProduct();
        }
    }

    function addProduct() {
        $scope.doingWork = true;
        $http({
            method: 'Post',
            url: '/api/Product/AddProductAsync',
            data: JSON.stringify($scope.product)
        }).then(function (response) {

            $scope.doingWork = false;
            $window.location.href = '/';

        }, function (error) {
            $scope.errorMsg = error;
            $scope.doingWork = false;

        });
    }

}]);


codeSample.directive('myDatatableDirective', function ($timeout) {
        return function (scope, element, attrs) {
            if (scope.$last) {
                $timeout(function () {
                    $('#prod-table').DataTable({
                        lengthMenu: [
                            [5, 10, 20, -1],
                            [5, 10, 20, 'All'],
                        ],
                        order: [1, 'asc'],
                        aoColumnDefs: [{
                            bSortable: false,
                            aTargets: [5, 6]
                        }]
                    });
                }, 50);
               
            }
        };
});

codeSample.directive('decimalFormat', ['$filter', function () {
    return {
        require: 'ngModel',
        scope: {
            ngModel: '='
        },
        link: function (scope, elem, attrs, ctrl) {
            if (!ctrl) return;

            ctrl.$formatters.unshift(function (a) {
                if (!ctrl.$modelValue) {
                    ctrl.$modelValue = 0;
                }
                return parseFloat(ctrl.$modelValue).toFixed(2);
            });

            elem.bind('blur', function (event) {
                var plainNumber = elem.val().replace(/[^\d|\-+|\.+]/g, '');
                if (!plainNumber) {
                    ctrl.$setViewValue(0);
                    elem.val("0.00")
                } else {
                    ctrl.$setViewValue(parseFloat(parseFloat(plainNumber).toFixed(2)));
                    elem.val(parseFloat(plainNumber).toFixed(2));
                }
               
            });
        }
    };
}]);

