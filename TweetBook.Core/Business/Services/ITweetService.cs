using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Core.DomainModels;

namespace TweetBook.Core.Services
{
    public interface ITweetService
    {
        public Task<IEnumerable<Tweet>> GetAllPostsAsync();
        public Task<Tweet> GetPostByIdAsync(int id);
        public Task<int> CreatePostAsync(Tweet post);

        public Task<bool> UpdatePostAsync(Tweet updatedPost);
        public Task<bool> DeletePostByIdAsync(int id);
        Task<bool> UserOwnsThisTweet(string userid, int tweetId);
    }
}
