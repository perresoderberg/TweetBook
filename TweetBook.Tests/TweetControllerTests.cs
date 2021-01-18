using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using TweetBook.Api;
using TweetBook.Api.Contract.V1;
using Xunit;

namespace TweetBook.TweetControllerTests
{
    public class TweetControllerTests
    {


        public TweetControllerTests()
        {
        }

        [Fact]
        public async void TestTweetController()
        {
            //var response = await _httpClient.GetAsync(ApiRoutes.Tweets.Get.Replace("{tweetId}", "1"));

            //response.IsSuccessStatusCode


        }
    }
}
