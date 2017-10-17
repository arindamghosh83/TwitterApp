/// <reference path="angular.js" />



var app = angular.module('myApp', []);
//app.controller('Twitter', function ($scope,$http) {
//    $scope.fname = 'Arindam';
//    $scope.lname = 'Ghosh';
//    $http.get("/Home/GetTwitterFeed").success(function(data) {
//                $scope.tweet = data.Result;
//                    });
//});

var TwitterController = function ($scope, twitterFactory) {
    $scope.fname = 'Arindam';
    $scope.lname = 'Ghosh';
    function init() {
        twitterFactory.getTwitterFeed()
            .success(function (data) {
                var obj = transformData(data.Result);
                $scope.tweet = obj; //data.Result;
                //$scope.tweet.createdDate = new Date($scope.tweet.created_at);
                var urlRegex = /(http(s?))\:\/\//gi;
                var urls = $scope.tweet.text.match(urlRegex);
                //$scope.tweet.text = "<a>" + urls[0] + "</a>";
            })
            .error(function(data, status, headers, config) {

            });
    }

    init();

    function transformData(tweet) {
        var obj = {};
        var urlRegex = /(http(s?):\/\/[^ ]*)/gi;
        obj.created_at = new Date(tweet.created_at);
        obj.profile_image_url = tweet.user.profile_image_url;
        obj.retweet_count = tweet.retweet_count;
        obj.media = {};
        obj.user = {};
        obj.text = tweet.text.replace(urlRegex, '');
        var text = tweet.text;
        var textLength = text.length;
        var str = "";
        var str2 = "";
        var hashtagstrarr = [];
        var urlrstrarr = [];
        var indices = [];
        if (tweet.extended_entities) {
            if (tweet.extended_entities.media.length > 0) {
                obj.media_url = tweet.extended_entities.media[0].media_url;
                obj.media.type = tweet.extended_entities.media[0].type;
            }
        }
        obj.user = {}
        //if (tweet.user && tweet.user.entities && tweet.user.entities.url && tweet.user.entities.url.urls.length > 0) {
        //    obj.user.url = tweet.user.entities.url.urls[0].url;
        //}
        if (tweet.entities) {
            if (tweet.entities.hashtags && tweet.entities.hashtags.length > 0) {
                tweet.entities.hashtags.forEach(function (hashtag, index) {
                    indices.push(hashtag.indices[0]);
                    indices.push(hashtag.indices[1]);
                   //str += text.substring(0, hashtag.indices[0]);
                   //str += text.substring(hashtag[1], textLength);
                   hashtagstrarr.push({
                       link: 'https://twitter.com/hashtag/'+ hashtag.text,
                       text: hashtag.text
                   });
                    
                });
                //var hashtagstr = hashtagstrarr.join();
                //str += hashtagstr;
            }

            if (tweet.entities.urls && tweet.entities.urls.length > 0) {
                tweet.entities.urls.forEach(function (url, index) {
                    indices.push(url.indices[0]);
                    indices.push(url.indices[1]);
                    //str2 += text.substring(0, url.indices[0]);
                    //str2 += text.substring(url[1], textLength);
                    urlrstrarr.push(url.url);
                });
                indices.sort(function(a, b) {
                    return a - b;
                });
                indices.forEach(function(item, index) {
                    if (index == 0) {
                        str += text.substring(0, indices[index]);
                    } else if (index !== 0 && index != indices.length - 1) {
                        str += text.substring(indices[index], indices[index + 1]);
                    } else if (index == indices.length - 1) {
                        str += text.substring(indices[index], text.length-1);
                    }
                });
                //var urlstr = urlrstrarr.join();
                //str += urlstr;
            }

           // obj.text = str;
            obj.hashtagstrarr = hashtagstrarr;
            obj.urlrstrarr = urlrstrarr;
            return obj;
        }
    }
}

TwitterController.$inject = ['$scope', 'twitterFactory'];
app.controller('TwitterController', TwitterController);

var TwitterFactory = function($http) {
    var factory = {};
    factory.getTwitterFeed = function() {
        return $http.get("/Home/GetTwitterFeed");
    }

    return factory;
}

TwitterFactory.$inject = ['$http'];
app.factory('twitterFactory', TwitterFactory);