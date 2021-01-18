using System;

namespace TweetBook.Api.Contract.V1.Responses
{
    public class TweetResponse
    {
        public int TweetId { get; set; }
        public Guid GuidId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
