using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Core.Data;
using TweetBook.Core.DomainModels;

namespace TweetBook.Core.Services
{


    public class TweetService : ITweetService
    {
        public TweetService(ITweetRepository postRepository)
        {
            _tweetRepository = postRepository;
        }
        private readonly ITweetRepository _tweetRepository;

        public async Task<IEnumerable<Tweet>> GetAllPostsAsync()
        {
            var posts = await _tweetRepository.GetAllAsync();

            return posts;
        }

        public async Task<Tweet> GetPostByIdAsync(int tweetId)
        {
            var post = await _tweetRepository.GetByIdAsync(tweetId);

            return post;
        }

        public async Task<int> CreatePostAsync(Tweet post)
        {
            int IdOfInsertedItem = await _tweetRepository.AddAsync(post);

            return IdOfInsertedItem;
        }

        public async Task<bool> UpdatePostAsync(Tweet updatedPost)
        {
            int rowsAffected = await _tweetRepository.UpdateAsync(updatedPost);

            return rowsAffected > 0;
        }

        public async Task<bool> DeletePostByIdAsync(int tweetId)
        {
            int rowsAffected = await _tweetRepository.DeleteByIdAsync(tweetId);

            return rowsAffected > 0;
        }

        public async Task<bool> UserOwnsThisTweet(string userid, int tweetId)
        {
            var tweet = await _tweetRepository.GetByIdAsync(tweetId);

            return tweet.UserId == userid;
        }
    }
}
