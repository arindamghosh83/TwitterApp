describe('TwitterControllerTests',
    function() {
        var $controller,scope,twitterFactory,timeout,interval,$q,$rootScope,
            dummyModels = {
                data: {
                    //Result: ['Dummy Tweet #1', 'Dummy Tweet #2']
                    result: ['Dummy Tweet #1', 'Dummy Tweet #2']
                }
            };

        beforeEach(function() {
        angular.mock.module('myApp');
            inject(function(_$controller_, _$q_, _$timeout_, _$interval_, _$rootScope_, _twitterFactory_) {
                scope = _$rootScope_.$new();
                timeout = _$timeout_;
                interval = _$interval_;
                $controller = _$controller_;
                twitterFactory = _twitterFactory_;
                $rootScope = _$rootScope_;
                $q = _$q_;

            });
            spyOn(twitterFactory, 'getTwitterFeed')
            .and.callFake(function () {
                var deferred = $q.defer();
                deferred.resolve(dummyModels);
                return deferred.promise;
            });

            var ctrl = $controller('TwitterController',
                 {
                     $scope: scope,
                     $timeout: timeout,
                     $interval: interval,
                     twitterFactory: twitterFactory
                 });
        });


        it('should invoke the fake getTwitterFeed', function () {
     
            spyOn(scope, 'transformData')
               .and.callFake(function () {
                   return dummyModels[0];
               });
        spyOn(scope, 'transformTweetUserMentionedText')
            .and.callFake(function () {
                
            });
            
            scope.$digest();
            timeout.flush();
   
        expect(twitterFactory.getTwitterFeed).toHaveBeenCalled();
        expect(scope.transformData).toHaveBeenCalled();
        expect(scope.transformTweetUserMentionedText).toHaveBeenCalled();
        //expect(stub.called).toBe(true);

        //expect(scope.tweets).toBe(dummyModels);
        });

        it('should invoke the fake transformData', function () {

            spyOn(scope, 'transformData')
               .and.callFake(function () {
                   return dummyModels[0];
               });
            scope.$digest();
            

            
            expect(scope.transformData).toHaveBeenCalled();
            
            //expect(stub.called).toBe(true);

            //expect(scope.tweets).toBe(dummyModels);
        });

        it('should invoke the fake transformTweetUserMentionedText', function () {


            spyOn(scope, 'transformData')
               .and.callFake(function () {
                   return dummyModels[0];
               });
            spyOn(scope, 'transformTweetUserMentionedText')
                .and.callFake(function () {

                });

            scope.$digest();
            timeout.flush();         
            expect(scope.transformTweetUserMentionedText).toHaveBeenCalled();
 
        });

});
describe('TwitterApiTests',
    function () {
        var tweets = ['hi','hello','ni hao'];
        var response;
       
        it('should test twitter api factory', function() {
            var $httpBackend, twitterFactory;
            angular.mock.module('myApp');
    
           angular.mock.inject(function (_$httpBackend_, _twitterFactory_) {
              $httpBackend = _$httpBackend_;
               twitterFactory = _twitterFactory_;
           });
           $httpBackend.when('GET', '/Home/GetTwitterFeed').respond(tweets);
            twitterFactory.getTwitterFeed()
                .then(function(data) {
                    response = data;
                });
            $httpBackend.flush();
            expect(response.data).toEqual(tweets);
        });
    });