var app = angular.module('repositorySearchApp', ['ngRoute']);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider        
        .when('/searchRepositories', {
            templateUrl: 'searchRepositories.htm', controller: 'SearchRepositoryController'
        })
        .when('/fetchBookmarks', {
            templateUrl: 'fetchBookmarks.htm', controller: 'FetchBookmarksController'
        })
        .otherwise({
            redirectTo: '/searchRepositories'
        });

}]);

app.constant('configSettings', { 'baseUrl': '/api/repositories' });

app.service('SearchService', function ($http, configSettings) {  

    this.getRepositories = function (repositoryName, page) {
        if (!repositoryName) {
            return;
        }

        const url = configSettings.baseUrl + '/' + repositoryName + '/' + page;
        return $http.get(url);        
    }; 
  
});

app.service('BookmarkService', function ($http, configSettings) {
    
    this.addBookmark = function (addRepositoryBookmark) {
        if (!addRepositoryBookmark) {
            return;
        }

        const url = configSettings.baseUrl;
        var data = {
            ID: addRepositoryBookmark.id,
            RepositoryName: addRepositoryBookmark.name,
            RepositoryLink: addRepositoryBookmark.url,
            OwnerAvatar: addRepositoryBookmark.owner.avatar_url
        }
       
        return $http.post(url, data);    
    };

});

app.controller('HomeController', function ($scope) {
    $scope.message = "Home page";
});

app.controller('FetchBookmarksController', function ($scope) {
    $scope.message = "This page will be used to display all the bookmarks";
});

app.controller('SearchRepositoryController', ['$scope', 'SearchService', 'BookmarkService', '$window',
    function ($scope, SearchService, BookmarkService, $window) {

        $scope.title = 'Repositories Search';
        $scope.repositoryName = '';
        $scope.addRepositoryBookmark = undefined;
        $scope.repositories = [];
        $scope.currentPage = 0;
        $scope.pageSize = 30;
        $scope.totalCount = 0;

        $scope.getRepositoriesInit = function () {
            $scope.initContoller();
            $scope.getRepositories()
        };

        $scope.initContoller = function () {
            $scope.repositories = [];
            $scope.currentPage = 0;
            $scope.totalCount = 0;
        };

        $scope.getRepositories = function () {
            $scope.currentPage = $scope.currentPage + 1;
            
            SearchService.getRepositories($scope.repositoryName, $scope.currentPage)
                .then(function (response) {
                    $scope.repositories = response.data.items;
                    $scope.totalCount = response.data.total_count;               
                }),
                function error(response) {                    
                };
        };

        $scope.bookmarkRepository = function (repository) {
            $scope.addRepositoryBookmark = repository;
        
            BookmarkService.addBookmark($scope.addRepositoryBookmark)
                .then(function (response) {                    
                    alert($scope.addRepositoryBookmark.name + ' Bookmarked');
                }),
                function error(response) {            
                };
            };

    }]);     