using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Moq;
using MyTwitterApp.Controllers;
using MyTwitterApp.Models;
using MyTwitterApp.Repository;
using NUnit.Framework;

namespace MyTwitterApp.UnitTests
{
    [TestFixture]
    public class TwitterRepoTests
    {
        private TwitterRepo sut;
        private Mock<ITwitterHttpHandler> fakeHttpHandler;
        [SetUp]
        public void InitializeSetUp()
        {

            fakeHttpHandler = new Mock<ITwitterHttpHandler>();
            sut = new TwitterRepo(fakeHttpHandler.Object);
            HttpResponseMessage testResponse = new HttpResponseMessage();

           
        }

        [Test]
        public void should_return_token_when_get_access_token_repo_invoked()
        {
            string expectedToken = "Dummy Token";
            fakeHttpHandler.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).ReturnsAsync(expectedToken);
            var actualToken = sut.GetAccessToken().Result;
            fakeHttpHandler.Verify(x => x.GetAccessTokenAsync("https://api.twitter.com/oauth2/token"), Times.Once);
            Assert.That(actualToken, Is.EqualTo(expectedToken));
        }
        [Test]
        public void should_return_tweet_when_get_tweet_repo_invoked()
        {
            List<RootObject> expectedtweets = new List<RootObject> {new RootObject() { Text = "Mock Object to Test1" },new RootObject(){ Text = "Mock Object to Test2" }};
            fakeHttpHandler.Setup(x => x.GetTweetsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expectedtweets);
            string expectedToken = "Dummy Token";
            fakeHttpHandler.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).ReturnsAsync(expectedToken);


            var actualtweets = sut.GetTweets().Result;
            fakeHttpHandler.Verify(x => x.GetAccessTokenAsync("https://api.twitter.com/oauth2/token"), Times.Once);
            Assert.That(actualtweets.Count, Is.EqualTo(expectedtweets.Count));
            Assert.That(actualtweets[0].Text, Is.EqualTo(expectedtweets[0].Text));
        }
    }
}