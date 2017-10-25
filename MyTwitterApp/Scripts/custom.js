/// <reference path="angular.js" />


(function() {
    var app = angular.module('myApp', []);
    var TwitterController = function ($scope, $timeout, $interval, twitterFactory) {

        $scope.initData = function () {
            twitterFactory.getTwitterFeed()
                .then(function (obj) {
                    var data = obj.data;
                    $scope.tweets = [];
                    data.result.forEach(function (data, index) {

                        var transformedobj = $scope.transformData(data);
                        $scope.tweets.push(transformedobj);
                    });
                    $timeout($scope.transformTweetUserMentionedText, 100);
                    //console.log("Init called");
                    //console.log("Tweet count", $scope.tweets.length);
                    //console.log("Tweets", $scope.tweets);

                });
           

        }

        $scope.initData();
        $interval($scope.initData, 60 * 1000);
        $scope.transformTweetUserMentionedText = function () {
            $scope.textTransformed = true;
            var subobj;
            $('p[id^="tweet_text"] span').each(function (index, el) {
                var text = $(el).text();

                var user_mentioned_indices = [];
                var hashtags_indices = [];
                var url_indices = [];
                var media_url_indices = [];
                var tweet = $scope.tweets[index];
                if (tweet.user_mentions && tweet.user_mentions.length > 0) {
                    $(el).html('');
                    tweet.user_mentions.forEach(function (user_mention) {
                        user_mentioned_indices.push(user_mention.indices); // array of indices

                    });
                    var str = '';
                    var startidx;
                    var endidx;
                    for (i = 0; i < user_mentioned_indices.length; i++) {
                        

                        if (i == 0) {
                            startidx = 0;
                            endidx = user_mentioned_indices[i][0];
                            str = text.slice(startidx, endidx); //retrieve from start to first occurrence of user_mention
                            $(el).append(str);
                        } else {
                            startidx = user_mentioned_indices[i - 1][1];
                            endidx = user_mentioned_indices[i][0];
                            str = text.slice(startidx, endidx); //retrieve the in-between part before the current occurrnce of user_mention and previous occurence
                            $(el).append(str);
                        }


                        str = '<a href="https://twitter.com/' + text.slice(user_mentioned_indices[i][0], user_mentioned_indices[i][1]) + '"target="_blank">' + text.slice(user_mentioned_indices[i][0], user_mentioned_indices[i][1]) + '</a>'; //retrieve the user_mentioned string part


                        //str = '<a href="https://twitter.com/'+text.slice(indices[i][0], indices[i][1])+'"target="_blank">' + text.slice(indices[i][0], indices[i][1]) + '</a>'; //retrieve the user_mentioned string part
                        $(el).append(str);
                        if (i == user_mentioned_indices.length - 1) {
                            startidx = user_mentioned_indices[i][1];
                            endidx = text.length;
                            str = text.slice(startidx, endidx); //retrieve from end of last occurrence of user_mention to end of string
                            $(el).append(str);
                        }

                    }
                }
                if (tweet.hashtags && tweet.hashtags.length > 0) {
                    tweet.hashtags.forEach(function (hashtag) {
                        hashtags_indices.push(hashtag.indices);

                    });
                    for (i = 0; i < hashtags_indices.length; i++) {
                        var hashtag = text.slice(hashtags_indices[i][0], hashtags_indices[i][1]);
                        if (hashtag.trimLeft() != hashtag)
                            hashtag = text.slice(hashtags_indices[i][0] + 1, hashtags_indices[i][1] + 1);
                        var formattedhashtag = '<a href="https://twitter.com/hashtag/' + hashtag.slice(1) + '"target="_blank">' + hashtag + '</a>'; //retrieve the user_mentioned string part
                        var orightml = $(el).html();
                        var newhtml = orightml.replace(hashtag, formattedhashtag);
                        $(el).html(newhtml);
                    }

                }

                if (tweet.urls && tweet.urls.length > 0) {
                    var displayurl, formattedurl, orightml1, newhtml1;
                    tweet.urls.forEach(function (url) {
                        url_indices.push(url.indices);

                    });
                    for (i = 0; i < url_indices.length; i++) {
                        displayurl = text.slice(url_indices[i][0], url_indices[i][1]);
                        if (displayurl.trimLeft() != displayurl)
                            displayurl = text.slice(url_indices[i][0] + 1, url_indices[i][1] + 1);
                        formattedurl = '<a href="' + displayurl + '"target="_blank">' + displayurl + '</a>'; //retrieve the url string part from text
                        orightml1 = $(el).html();
                        newhtml1 = orightml1.replace(displayurl, formattedurl);
                        $(el).html(newhtml1);
                    }

                }


                if (tweet.medium && tweet.medium.length > 0) {
                    tweet.medium.forEach(function (medium) {
                        media_url_indices.push(medium.indices);

                    });
                    for (i = 0; i < media_url_indices.length; i++) {
                        var mediaurl = text.slice(media_url_indices[i][0], media_url_indices[i][1]);
                        if (mediaurl.trimLeft() != mediaurl)
                            mediaurl = text.slice(media_url_indices[i][0] + 1, media_url_indices[i][1] + 1);
                        if (mediaurl.trimRight() != mediaurl)
                            mediaurl = text.slice(media_url_indices[i][0] - 1, media_url_indices[i][1] - 1);
                        var formattedmediaurl = '<a href="' + mediaurl + '"target="_blank">' + mediaurl + '</a>'; //retrieve the user_mentioned string part
                        var orightml3 = $(el).html();
                        var newhtml3 = orightml3.replace(mediaurl, formattedmediaurl);
                        $(el).html(newhtml3);
                    }

                }

                //$(this).text(str);
                //console.log("Transformed " + str);
                //console.log("Original " + text);
                //console.log("User Mentioned " + user_mentioned_indices);
                //console.log("Hashtags " + hashtags_indices);


            });
            //console.log($scope.textTransformed);

        }

        $scope.transformData = function (tweet) {
            var obj = {};
            var urlRegex = /(http(s?):\/\/[^ ]*)/gi;
            obj.created_at = new Date(tweet.created_at);
            obj.profile_image_url = tweet.user.profile_image_url;
            obj.retweet_count = tweet.retweet_count;
            obj.isVisible = true;
            //obj.media = {};
            obj.user = tweet.user;
            obj.text = tweet.text;
            obj.user_mentions = [];
            obj.hashtags = [];
            obj.urls = [];

            obj.medium = [];
            if (tweet.extended_entities) {
                if (tweet.extended_entities.media.length > 0) {
                    obj.media = {}; //Create media sub object
                    obj.media.media_url = tweet.extended_entities.media[0].media_url;
                    obj.media.type = tweet.extended_entities.media[0].type;
                    obj.media.embedded_url = tweet.entities.media[0].url;
                    tweet.extended_entities.media.forEach(function (media) {
                            obj.medium.push(media);
                        });

                }
            //} else {
            //    if (tweet.user) {
            //        obj.description = tweet.user.description;
            //    }

            }

            if (tweet.entities) {

                if (tweet.entities.user_mentions && tweet.entities.user_mentions.length > 0) {
                    tweet.entities.user_mentions.forEach(function (user_mention) {
                        obj.user_mentions.push(user_mention);  //List of users mentioned in text
                    });
                }
                if (tweet.entities.hashtags && tweet.entities.hashtags.length > 0) {
                    tweet.entities.hashtags.forEach(function (hashtag) {
                        obj.hashtags.push(hashtag);
                    });
                }
                if (tweet.entities.urls && tweet.entities.urls.length > 0) {
                    tweet.entities.urls.forEach(function (url) {
                        obj.urls.push(url);
                    });
                }

                //if (tweet.entities.media && tweet.entities.media.length > 0) {
                //    obj.media = {};
                //    obj.media.media_url = tweet.entities.media[0].media_url;
                //    obj.media.type = tweet.entities.media[0].type;
                //    obj.media.embedded_url = tweet.entities.media[0].url;
                //    tweet.entities.media.forEach(function (media) {
                //        obj.medium.push(media);
                //    });
                //}
                if (tweet.user) {
                    obj.description = tweet.user.description;
                }

            }
            return obj;
        }

        $scope.textSearch = function () {
            var searchText = $scope.searchText;
            $scope.tweets.forEach(function (tweet, index) {
                if (tweet.text.includes(searchText) || tweet.description.includes(searchText)) {
                    tweet.isVisible = true;
                } else {
                    tweet.isVisible = false;
                }
            });

        }

        $scope.keyUpTextSearch = function () {
            $timeout(function () { $scope.textSearch(); }, 1000);
        }
    }

    TwitterController.$inject = ['$scope', '$timeout', '$interval', 'twitterFactory'];
    app.controller('TwitterController', TwitterController);

    var TwitterFactory = function ($http) {
        var factory = {};
        factory.getTwitterFeed = function () {
            return $http.get("/Home/GetTwitterFeed");
           
        }


        return factory;
    }

    TwitterFactory.$inject = ['$http', '$q'];
    app.factory('twitterFactory', TwitterFactory);
})();
