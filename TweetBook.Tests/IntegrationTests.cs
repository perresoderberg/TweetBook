using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TweetBook.Api;

namespace TweetBook.IntegrationTests
{
    public class IntegrationTests
    {
        protected readonly HttpClient _httpClient;
        protected IntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _httpClient = appFactory.CreateClient();
        }
        protected async Task AuthenticateAsync() 
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<string> GetJwtAsync()
        {
            return "";
        }
    }
}
