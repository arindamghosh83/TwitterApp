using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moq;
using NUnit.Framework;
using MyTwitterApp.Controllers;
using MyTwitterApp.Models;
using MyTwitterApp.Repository;

namespace MyTwitterApp.UnitTests
{
    [TestFixture]

    public class HomeControllerTests
    {
        private HomeController sut;
        private Mock<ITwitterRepo> repo;
        [SetUp]
        public void InitializeSetUp()
        {
            
            repo = new Mock<ITwitterRepo>();
            sut = new HomeController(repo.Object);
            List<RootObject> expectedtweets = new List<RootObject> { new RootObject() { text = "Mock Object to Test1" }, new RootObject() { text = "Mock Object to Test2" } };


            repo.Setup(x => x.GetTwitts()).ReturnsAsync(expectedtweets);
        }

        [Test]
        public void then_get_tweets_should_return_expected_tweets()
        {
            var actualtweets =sut.GetTwitterFeed();
            repo.Verify(x => x.GetTwitts(),Times.Once);
            
        }
    }
}