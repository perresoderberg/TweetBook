using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TweetBook.Core.Data;
using TweetBook.Core.DomainModels;
using TweetBook.Core.Services;
using Xunit;

namespace TweetBook.Tests
{
    public class TweetServiceTests
    {
        private readonly Mock<ITweetRepository> MockTweetRepository = new Mock<ITweetRepository>();
        private TweetService _tweetservice;
        public TweetServiceTests(ITweetService tweetservice)
        {
            _tweetservice = new TweetService(MockTweetRepository.Object);
        }


        [Fact]
        public async Task GetAll_NameIsNotNull()
        {
            List<Tweet> tweets = new List<Tweet>();
            tweets.Add(new Tweet()
               { UserId = "1", Name = "kalle", DateCreated = DateTime.Now, GuidId = Guid.NewGuid(), TweetId = 1 }
            );

            MockTweetRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(tweets);

            var ret = await _tweetservice.GetAllPostsAsync();

            foreach (var item in ret)
            {

            }
        
        }

    }
}
