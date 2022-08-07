codeSample.config(['$locationProvider', '$stateProvider', '$urlRouterProvider', '$urlMatcherFactoryProvider', '$compileProvider',
    function ($locationProvider, $stateProvider, $urlRouterProvider, $urlMatcherFactoryProvider, $compileProvider) {

       // remove "#!" from url
        if (window.history && window.history.pushState) {
            $locationProvider.html5Mode({
                enabled: true,
                requireBase: true
            }).hashPrefix('!');
        };
        $urlMatcherFactoryProvider.strictMode(false);
        $compileProvider.debugInfoEnabled(false);

        $stateProvider
            .state('default', {
                url: '/',
                templateUrl: './views/products.html',
                controller: 'productsController'
            })
            .state('products', {
                url: '/products',
                templateUrl: './views/products.html',
                controller: 'productsController'
            })
            .state('edit', {
                url: '/edit/:id',
                templateUrl: './views/edit.html',
                params: {id:''},
                controller: 'editController'
            })
            .state('add', {
                url: '/add',
                templateUrl: './views/add.html',
                params: { id: '' },
                controller: 'addController'
            })
        $urlRouterProvider.otherwise('/');
    }]); 
